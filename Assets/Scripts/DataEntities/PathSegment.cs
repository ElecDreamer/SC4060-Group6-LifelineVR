using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
            pathSegment.transform.rotation = Quaternion.LookRotation(-pathSegment.transform.forward);
            pathSegment.transform.position -= pathSegment.transform.forward * pathSegment.distance;
        }
    }
}
#endif