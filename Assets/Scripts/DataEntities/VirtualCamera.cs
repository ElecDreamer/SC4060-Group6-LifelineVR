using UnityEngine;

public class VirtualCamera : MonoBehaviour
{
    public Vector3 position;
    public Vector3 eulerRotation;

    [Range(0.0001f, 1)]
    public float scale = 1;

    public Quaternion Rotation
    {
        get { return Quaternion.Euler(eulerRotation); }
        set { eulerRotation = value.eulerAngles; }
    }
}
