using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PathCreator : MonoBehaviour
{
    public Transform rootQuad;
    public Transform endQuad;
    public string pathPrefabPath;
    public float angle;

    [HideInInspector]
    public PathCreator createdInstance;

    // private void OnDrawGizmos()
    // {
    //     if (rootQuad != null && endQuad != null)
    //     {
    //         Gizmos.color = Color.green;
    //         Gizmos.DrawLine(rootQuad.transform.position, endQuad.transform.position);
    //     }
    // }
}

[CustomEditor(typeof(PathCreator))]
public class PathCreatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        PathCreator pathCreator = (PathCreator)target;

        GameObject pathPrefab = null;

        if (!string.IsNullOrEmpty(pathCreator.pathPrefabPath))
        {
            pathPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(pathCreator.pathPrefabPath);
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
}
