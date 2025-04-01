using UnityEngine;

public class VirtualTransform : MonoBehaviour
{
    [HideInInspector]
    public Vector3 position;

    [HideInInspector]
    public Quaternion rotation;

    [HideInInspector]
    public Vector3 scale;

    [HideInInspector]
    public bool isChildTransform;

    public Vector3 Forward
    {
        get { return rotation * Vector3.forward; }
    }

    public void Awake()
    {
        if (
            transform.parent != null
            && transform.parent.GetComponentInParent<VirtualTransform>() != null
        )
        {
            Debug.LogWarning(
                "This VirtualTransform is a child of another VirtualTransform. Setting the child's VirtualTransform's positions, rotations or scale is not yet tested."
            );
            isChildTransform = true;
        }
        else
        {
            isChildTransform = false;
        }

        position = transform.position;
        rotation = transform.rotation;
        scale = transform.localScale;
    }
}
