using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PathCreator : MonoBehaviour
{
    public Transform rootQuad;
    public Transform endQuad;
    public string splitPathPrefabPath;
    public string mergeBPathPrefabPath;
    public string mergeCPathPrefabPath;
    public float angle;

    public enum PathType
    {
        Split,
        MergeB,
        MergeC
    }

    public PathType selectedPathType;

    [HideInInspector]
    public PathCreator createdInstance;

    public PathSegment rootSegment;
    public PathSegment endSegment;
}

#if UNITY_EDITOR
[CustomEditor(typeof(PathCreator))]
public class PathCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PathCreator pathCreator = (PathCreator)target;

        GameObject pathPrefab = null;

        // Display dropdown for selecting path type
        pathCreator.selectedPathType = (PathCreator.PathType)EditorGUILayout.EnumPopup(
            "Path Type",
            pathCreator.selectedPathType
        );

        // Determine which prefab path to use based on the selected path type
        string selectedPathPrefabPath = GetSelectedPathPrefabPath(pathCreator);

        if (!string.IsNullOrEmpty(selectedPathPrefabPath))
        {
            pathPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(selectedPathPrefabPath);
            if (pathPrefab == null)
            {
                EditorGUILayout.HelpBox(
                    "Invalid path to prefab. Please ensure the path is correct.",
                    MessageType.Error
                );
            }
        }
        else
        {
            EditorGUILayout.HelpBox(
                "Path to prefab is empty. Please provide a valid path.",
                MessageType.Warning
            );
        }

        if (pathCreator.createdInstance != null)
        {
            EditorGUILayout.HelpBox("A new path is being edited.", MessageType.Info);
        }

        DrawDefaultInspector();

        if (GUILayout.Button("Create Path"))
        {
            if (pathPrefab != null && pathCreator.endQuad != null)
            {
                if (pathCreator.createdInstance != null)
                {
                    DestroyImmediate(pathCreator.createdInstance.gameObject);
                }

                pathCreator.createdInstance = PrefabUtility
                    .InstantiatePrefab(pathPrefab)
                    .GetComponent<PathCreator>();

                SetRotationAndPosition();
                ConnectPathSegments();
            }
            else
            {
                Debug.LogWarning("PathPrefab or EndQuad is not assigned.");
            }
        }

        if (pathCreator.createdInstance != null)
        {
            // SetRotationAndPosition();
        }

        if (GUILayout.Button("Confirm"))
        {
            if (pathCreator.createdInstance != null)
            {
                Undo.RegisterCreatedObjectUndo(
                    pathCreator.createdInstance,
                    "Confirm Path Creation"
                );
                pathCreator.createdInstance = null;
            }
            else
            {
                Debug.LogWarning("No created instance to confirm.");
            }
        }
    }

    private string GetSelectedPathPrefabPath(PathCreator pathCreator)
    {
        switch (pathCreator.selectedPathType)
        {
            case PathCreator.PathType.Split:
                return pathCreator.splitPathPrefabPath;
            case PathCreator.PathType.MergeB:
                return pathCreator.mergeBPathPrefabPath;
            case PathCreator.PathType.MergeC:
                return pathCreator.mergeCPathPrefabPath;
            default:
                return null;
        }
    }

    private void SetRotationAndPosition()
    {
        PathCreator pathCreator = (PathCreator)target;

        if (pathCreator.createdInstance != null && pathCreator.endQuad != null)
        {
            Transform createdRoot = pathCreator.createdInstance.rootQuad;
            if (createdRoot != null)
            {
                Quaternion rootRotationOffset =
                    Quaternion.Inverse(pathCreator.createdInstance.transform.rotation)
                    * createdRoot.rotation;

                pathCreator.createdInstance.transform.rotation =
                    pathCreator.endQuad.transform.rotation
                    * Quaternion.Inverse(rootRotationOffset)
                    * Quaternion.AngleAxis(180, pathCreator.createdInstance.rootQuad.right)
                    * Quaternion.AngleAxis(
                        pathCreator.angle,
                        pathCreator.createdInstance.rootQuad.forward
                    );

                Vector3 rootOffset =
                    createdRoot.position - pathCreator.createdInstance.transform.position;

                pathCreator.createdInstance.transform.position =
                    pathCreator.endQuad.transform.position - rootOffset;
            }
            else
            {
                Debug.LogWarning("Root transform not found in the instantiated prefab.");
            }
        }
        else
        {
            Debug.LogWarning("CreatedInstance or EndQuad is not assigned.");
        }
    }

    private void ConnectPathSegments()
    {
        PathCreator pathCreator = (PathCreator)target;

        if (pathCreator.createdInstance != null)
        {
            PathSegment endSegment = pathCreator.endSegment;
            PathSegment rootSegment = pathCreator.createdInstance.rootSegment;

            if (endSegment != null && rootSegment != null)
            {
                var nextSegmentsList = new System.Collections.Generic.List<PathSegment>(endSegment.nextSegments);
                nextSegmentsList.RemoveAll(segment => segment == null);
                nextSegmentsList.Add(rootSegment);
                endSegment.nextSegments = nextSegmentsList.ToArray();
                EditorUtility.SetDirty(endSegment);
            }
            else
            {
                Debug.LogWarning("EndSegment or RootSegment is not assigned.");
            }
        }
        else
        {
            Debug.LogWarning("CreatedInstance is not assigned.");
        }
    }
}
#endif