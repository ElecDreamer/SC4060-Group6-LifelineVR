using UnityEngine;
using UnityEngine.UIElements;

public class PathFollower : MonoBehaviour
{
    public PathSegment currentPathSegment;
    public float brownianMotionAcceleration = 1f;
    public float pushAcceleration = 2f;
    public float pullAcceleration = 0.5f;
    public float maxSpeed = 1f;
    public float radius = 0.9f;

    private Vector3 velocity;
    private VirtualTransform virtualTransform;

    public void Awake()
    {
        virtualTransform = GetComponent<VirtualTransform>();
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
        if (IsBeyondPathEnd())
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
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
