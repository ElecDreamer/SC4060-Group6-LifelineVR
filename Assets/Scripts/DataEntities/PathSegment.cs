using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PathSegment : MonoBehaviour
{
    public float radius = 5;
    public float distance = 10;
    public PathSegment[] nextSegments;

    private VirtualTransformShifter virtualTransformShifter;

    public void Awake()
    {
        virtualTransformShifter = FindObjectOfType<VirtualTransformShifter>();
    }

    public void Update()
    {
        DebugExtension.DrawCylinder(
            transform.position,
            Quaternion.LookRotation(transform.forward),
            distance * virtualTransformShifter.InverseScale,
            radius * virtualTransformShifter.InverseScale,
            Color.green
        );
    }

    public bool IsBeyondPathEnd(Vector3 position)
    {
        Vector3 pathEndPosition = CalculatePathEndPosition();
        Vector3 pathEndPositionToPositionVector = position - pathEndPosition;
        float dotProduct = Vector3.Dot(
            pathEndPositionToPositionVector,
            GetComponent<VirtualTransform>().Forward
        );
        return dotProduct > 0;
    }

    private Vector3 CalculatePathEndPosition()
    {
        return GetComponent<VirtualTransform>().position
            + GetComponent<VirtualTransform>().Forward * distance;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(PathSegment))]
public class PathSegmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        PathSegment pathSegment = (PathSegment)target;

        if (GUILayout.Button("Reverse Path Segment"))
        {
            Undo.RecordObject(pathSegment.transform, "Reverse Path Segment");
            pathSegment.transform.rotation = Quaternion.LookRotation(
                -pathSegment.transform.forward
            );
            pathSegment.transform.position -= pathSegment.transform.forward * pathSegment.distance;
        }
    }
}
#endif
