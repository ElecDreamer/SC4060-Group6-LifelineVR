using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public PathSegment currentPathSegment;
    public float brownianMotionAcceleration = 1f;
    public float pushAcceleration = 2f;
    public float pullAcceleration = 0.5f;
    public float maxSpeed = 1f;
    public float radius = 0.9f;
    public bool disallowGoingToOtherPathSegments = false;

    private Vector3 velocity;
    private VirtualTransform virtualTransform;
    private VirtualTransformShifter virtualTransformShifter;

    public void Awake()
    {
        virtualTransform = GetComponent<VirtualTransform>();
        virtualTransformShifter = GetComponent<VirtualTransformShifter>();
    }

    public void FixedUpdate()
    {
        HandleMotion();
        HandleConnectivity();
    }

    private void HandleMotion()
    {
        AddBrownianMotion();
        PushTowardsEndOfPathSegment();
        PullTowardsMiddleOfPathSegment();
        DisallowGoingOutSidePathSegment();
        DisallowGoingToOtherPathSegments();
        LimitVelocity();
        ApplyVelocity();
    }

    private void AddBrownianMotion()
    {
        Vector3 randomAcceleration = Random.onUnitSphere * brownianMotionAcceleration;
        velocity += randomAcceleration * Time.fixedDeltaTime;
    }

    private void PushTowardsEndOfPathSegment()
    {
        ApplyAcceleration(
            currentPathSegment.GetComponent<VirtualTransform>().Forward,
            pushAcceleration
        );
    }

    private void PullTowardsMiddleOfPathSegment()
    {
        Vector3 closestPoint = GetClosestPointOnPathCenterLine();
        Vector3 direction = closestPoint - virtualTransform.position;
        ApplyAcceleration(direction, pullAcceleration);
    }

    private Vector3 GetClosestPointOnPathCenterLine()
    {
        return CalculateClosestPointOnPath(
            currentPathSegment.GetComponent<VirtualTransform>().position,
            virtualTransform.position - currentPathSegment.GetComponent<VirtualTransform>().position
        );
    }

    private void DisallowGoingOutSidePathSegment()
    {
        Vector3 pathCenter = currentPathSegment.GetComponent<VirtualTransform>().position;
        Vector3 toPosition = virtualTransform.position - pathCenter;

        Vector3 closestPointOnPath = CalculateClosestPointOnPath(pathCenter, toPosition);
        Vector3 radialOffset = CalculateRadialOffset(closestPointOnPath);

        float effectiveRadius = CalculateEffectiveRadius();

        if (IsOutsideEffectiveRadius(radialOffset, effectiveRadius))
        {
            AdjustVelocityToStayWithinRadius(radialOffset);
        }
    }

    private void DisallowGoingToOtherPathSegments()
    {
        if (!disallowGoingToOtherPathSegments)
        {
            return;
        }

        Vector3 pathStart = currentPathSegment.GetComponent<VirtualTransform>().position;
        Vector3 pathEnd = CalculatePathEndPosition();

        Vector3 toStart = virtualTransform.position - pathStart;
        Vector3 toEnd = virtualTransform.position - pathEnd;

        Vector3 pathDirection = currentPathSegment.GetComponent<VirtualTransform>().Forward;

        if (Vector3.Dot(toStart, pathDirection) < 0)
        {
            Vector3 startVelocity = Vector3.Project(velocity, -pathDirection);
            if (Vector3.Dot(startVelocity, toStart) > 0)
            {
                velocity -= startVelocity;
            }
        }
        else if (Vector3.Dot(toEnd, pathDirection) > 0)
        {
            Vector3 endVelocity = Vector3.Project(velocity, pathDirection);
            if (Vector3.Dot(endVelocity, toEnd) > 0)
            {
                velocity -= endVelocity;
            }
        }
    }

    private Vector3 CalculateRadialOffset(Vector3 closestPointOnPath)
    {
        return virtualTransform.position - closestPointOnPath;
    }

    private float CalculateEffectiveRadius()
    {
        return currentPathSegment.radius - radius;
    }

    private bool IsOutsideEffectiveRadius(Vector3 radialOffset, float effectiveRadius)
    {
        return radialOffset.magnitude > effectiveRadius;
    }

    private void AdjustVelocityToStayWithinRadius(Vector3 radialOffset)
    {
        Vector3 radialVelocity = Vector3.Project(velocity, radialOffset.normalized);
        if (Vector3.Dot(radialVelocity, radialOffset) > 0)
        {
            velocity -= radialVelocity;
        }
    }

    private Vector3 CalculateClosestPointOnPath(Vector3 pathCenter, Vector3 toPosition)
    {
        Vector3 pathDirection = currentPathSegment.GetComponent<VirtualTransform>().Forward;
        Vector3 projection = Vector3.Project(toPosition, pathDirection);
        return pathCenter + projection;
    }

    private void ApplyAcceleration(Vector3 direction, float acceleration)
    {
        velocity += acceleration * direction.normalized * Time.fixedDeltaTime;
    }

    private void LimitVelocity()
    {
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
    }

    private void ApplyVelocity()
    {
        virtualTransform.position += velocity * Time.fixedDeltaTime;
    }

    private void HandleConnectivity()
    {
        if (IsBeyondPathEnd() && !disallowGoingToOtherPathSegments)
        {
            SwitchToNextPathSegment();
        }
    }

    private bool IsBeyondPathEnd()
    {
        Vector3 pathEndPosition = CalculatePathEndPosition();
        Vector3 pathEndPositionToFollowerVector = virtualTransform.position - pathEndPosition;
        float dotProduct = Vector3.Dot(
            pathEndPositionToFollowerVector,
            currentPathSegment.GetComponent<VirtualTransform>().Forward
        );

        return dotProduct > 0;
    }

    private Vector3 CalculatePathEndPosition()
    {
        return currentPathSegment.GetComponent<VirtualTransform>().position
            + currentPathSegment.GetComponent<VirtualTransform>().Forward
                * currentPathSegment.distance;
    }

    private void SwitchToNextPathSegment()
    {
        PathSegment[] nextSegments = currentPathSegment.nextSegments;
        if (nextSegments != null && nextSegments.Length > 0)
        {
            int randomIndex = Random.Range(0, nextSegments.Length);
            currentPathSegment = nextSegments[randomIndex];
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            transform.position,
            radius * (virtualTransformShifter ? virtualTransformShifter.InverseScale : 1)
        );
    }
}
