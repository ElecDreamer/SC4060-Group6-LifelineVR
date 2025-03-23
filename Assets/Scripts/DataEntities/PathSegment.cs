using UnityEngine;

public class PathSegment : MonoBehaviour
{
    public float radius = 5;
    public float distance = 10;
    public PathSegment[] nextSegments;

    public void Update()
    {
        DebugExtension.DrawCylinder(
            transform.position,
            Quaternion.LookRotation(transform.forward),
            distance,
            radius,
            Color.green
        );
    }
}
