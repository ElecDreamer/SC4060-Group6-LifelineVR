using System.Collections.Generic;
using UnityEngine;

public class VirtualTransformShifter : MonoBehaviour
{
    public VirtualCamera cameraTransform;
    private List<VirtualTransform> virtualTransforms;

    public float InverseScale
    {
        get { return 1 / cameraTransform.scale; }
    }

    public void Start()
    {
        virtualTransforms = new List<VirtualTransform>(FindObjectsOfType<VirtualTransform>());
    }

    public void LateUpdate()
    {
        ShiftWorld();
    }

    private void ShiftWorld()
    {
        if (cameraTransform == null)
            return;

        Vector3 inversePosition = CalculateInversePosition();
        Quaternion inverseRotation = CalculateInverseRotation();
        Vector3 inverseScale = CalculateInverseScale();
        ApplyInverseTransformations(inversePosition, inverseRotation, inverseScale);
    }

    private Vector3 CalculateInversePosition()
    {
        return -cameraTransform.position;
    }

    private Quaternion CalculateInverseRotation()
    {
        return Quaternion.Inverse(cameraTransform.Rotation);
    }

    private Vector3 CalculateInverseScale()
    {
        return Vector3.one / cameraTransform.scale;
    }

    private void ApplyInverseTransformations(
        Vector3 inversePosition,
        Quaternion inverseRotation,
        Vector3 inverseScale
    )
    {
        virtualTransforms.Sort(
            (a, b) =>
                (a.transform.parent == null ? 0 : a.transform.parent.GetInstanceID()).CompareTo(
                    b.transform.parent == null ? 0 : b.transform.parent.GetInstanceID()
                )
        );

        foreach (var vt in virtualTransforms)
        {
            vt.transform.position = Vector3.Scale(
                inverseRotation * (vt.position + inversePosition),
                inverseScale
            );
            vt.transform.rotation = inverseRotation * vt.rotation;
            vt.transform.localScale = Vector3.Scale(vt.scale, inverseScale);
        }
    }
}
