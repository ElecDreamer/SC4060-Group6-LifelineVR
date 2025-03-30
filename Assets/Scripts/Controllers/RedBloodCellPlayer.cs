using UnityEngine;

public class RedBloodCellPlayer : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 angularVelocity;
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 smoothedCurrentPathSegmentForward;
    private VirtualCamera virtualCamera;

    public PathSegment currentPathSegment;
    public RedBloodCellShipManager shipManager;

    [Range(0, 1)]
    public float forwardAcceleration;
    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    public float radius = 1f;
    public float acceleration = 5f;
    public float angularAcceleration = 5f;
    public float friction = 5f;
    public float angularFriction = 5f;
    public float maxSpeed = 10f;
    public float maxAngularSpeed = 360f;
    public float forwardMaxAngle = 45f;
    public float smoothCurrentPathSegmentForwardSpeed = 0.1f;
    public float nudgeRotationToFaceEndOfPathSpeed = 5f;
    public float nudgeRotationToFaceEndOfPathDeadZone = 2f;

    public void Start()
    {
        InitializeVirtualCamera();
        InitializePositionAndRotation();
        InitializeSmoothedPathSegmentForward();
    }

    private void InitializeVirtualCamera()
    {
        virtualCamera = FindObjectOfType<VirtualCamera>();
    }

    private void InitializePositionAndRotation()
    {
        position = transform.position;
        rotation = transform.rotation;
    }

    private void InitializeSmoothedPathSegmentForward()
    {
        smoothedCurrentPathSegmentForward = currentPathSegment.GetComponent<VirtualTransform>().Forward;
    }

    public void FixedUpdate()
    {
        UpdateVelocity();
        UpdatePositionAndRotation();
        UpdateAfterMovement();
    }

    private void UpdateVelocity()
    {
        UpdateFromPlayerControls();
        ApplyMovementForces();
        ApplyFriction();
        RestrictWithinPath();
        LimitVelocity();
    }

    private void UpdatePositionAndRotation()
    {
        IntegrateVelocity();
        NudgeRotationToFaceEndOfPath();
        RestrictRotationToForward();
    }

    private void UpdateAfterMovement()
    {
        HandlePathConnectivity();
        SmoothPathSegmentForward();
    }

    private void UpdateFromPlayerControls()
    {
        if (shipManager == null)
        {
            return;
        }
        forwardAcceleration = shipManager.GetForwardSpeedFromType(shipManager.currentForwardSpeedType);
        yaw = (shipManager.xrSteeringWheelYaw.value - 0.5f) * 2f;
        pitch = (shipManager.xrSteeringWheelPitch.value - 0.5f) * 2f;
    }

    private void ApplyMovementForces()
    {
        velocity += rotation * Vector3.forward * forwardAcceleration * acceleration * Time.fixedDeltaTime;
        angularVelocity += new Vector3(-pitch, yaw, 0) * angularAcceleration * Time.fixedDeltaTime;
    }

    private void ApplyFriction()
    {
        Vector3 newVelocity = velocity - velocity * friction * Time.fixedDeltaTime;
        Vector3 newAngularVelocity = angularVelocity - angularVelocity * angularFriction * Time.fixedDeltaTime;

        if (Vector3.Dot(velocity, newVelocity) < 0)
        {
            newVelocity = Vector3.zero;
        }
        if (Vector3.Dot(angularVelocity, newAngularVelocity) < 0)
        {
            newAngularVelocity = Vector3.zero;
        }

        velocity = newVelocity;
        angularVelocity = newAngularVelocity;
    }

    private void RestrictWithinPath()
    {
        Vector3 pathCenter = currentPathSegment.GetComponent<VirtualTransform>().position;
        Vector3 toPosition = position - pathCenter;

        Vector3 closestPointOnPath = CalculateClosestPointOnPath(pathCenter, toPosition);
        Vector3 radialOffset = position - closestPointOnPath;

        float effectiveRadius = currentPathSegment.radius - radius;

        if (radialOffset.magnitude > effectiveRadius)
        {
            Vector3 radialVelocity = Vector3.Project(velocity, radialOffset.normalized);
            if (Vector3.Dot(radialVelocity, radialOffset) > 0)
            {
                velocity -= radialVelocity;
            }
        }
    }

    private Vector3 CalculateClosestPointOnPath(Vector3 pathCenter, Vector3 toPosition)
    {
        Vector3 pathDirection = currentPathSegment.GetComponent<VirtualTransform>().Forward;
        Vector3 projection = Vector3.Project(toPosition, pathDirection);
        return pathCenter + projection;
    }

    private void LimitVelocity()
    {
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        angularVelocity = Vector3.ClampMagnitude(angularVelocity, maxAngularSpeed);
    }

    private void NudgeRotationToFaceEndOfPath()
    {
        Vector3 endOfPath = currentPathSegment.GetComponent<VirtualTransform>().position + currentPathSegment.GetComponent<VirtualTransform>().Forward * currentPathSegment.distance;
        Vector3 toEndOfPath = endOfPath - position;
        if (toEndOfPath.magnitude < nudgeRotationToFaceEndOfPathDeadZone)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(toEndOfPath, rotation * Vector3.up);
        float angleDifference = Quaternion.Angle(rotation, targetRotation);
        rotation = Quaternion.RotateTowards(rotation, targetRotation, Mathf.Clamp01(angleDifference / 180f) * nudgeRotationToFaceEndOfPathSpeed * Time.fixedDeltaTime);
    }

    private void RestrictRotationToForward()
    {
        Vector3 pathForward = smoothedCurrentPathSegmentForward;
        float angleDifference = Vector3.Angle(rotation * Vector3.forward, pathForward);

        if (angleDifference > forwardMaxAngle)
        {
            Quaternion targetRotation = Quaternion.LookRotation(pathForward, rotation * Vector3.up);
            rotation = Quaternion.RotateTowards(rotation, targetRotation, angleDifference - forwardMaxAngle);
        }
    }

    private void IntegrateVelocity()
    {
        position += velocity * Time.fixedDeltaTime;
        rotation *= Quaternion.Euler(angularVelocity * Time.fixedDeltaTime);
    }

    private void HandlePathConnectivity()
    {
        if (currentPathSegment.IsBeyondPathEnd(position))
        {
            SwitchToNextPathSegment();
        }
    }

    private void SwitchToNextPathSegment()
    {
        PathSegment[] nextSegments = currentPathSegment.nextSegments;
        if (nextSegments != null && nextSegments.Length > 0)
        {
            PathSegment bestSegment = nextSegments[0];
            float bestDotProduct = Vector3.Dot(rotation * Vector3.forward, bestSegment.GetComponent<VirtualTransform>().Forward);

            foreach (PathSegment segment in nextSegments)
            {
                Vector3 segmentForward = segment.GetComponent<VirtualTransform>().Forward;
                float dotProduct = Vector3.Dot(rotation * Vector3.forward, segmentForward);

                if (dotProduct > bestDotProduct)
                {
                    bestDotProduct = dotProduct;
                    bestSegment = segment;
                }
            }

            currentPathSegment = bestSegment;
        }
    }

    private void SmoothPathSegmentForward()
    {
        Vector3 pathForward = currentPathSegment.GetComponent<VirtualTransform>().Forward;
        smoothedCurrentPathSegmentForward = Vector3.Slerp(smoothedCurrentPathSegmentForward, pathForward, Time.fixedDeltaTime * smoothCurrentPathSegmentForwardSpeed);
    }

    public void Update()
    {
        if (virtualCamera != null)
        {
            virtualCamera.position = position;
            virtualCamera.Rotation = rotation;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
