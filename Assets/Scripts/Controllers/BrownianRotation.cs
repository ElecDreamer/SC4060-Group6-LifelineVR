using UnityEngine;

public class BrownianRotation : MonoBehaviour
{
    public float angularAcceleration = 1.0f;
    private Quaternion angularVelocity = Quaternion.identity;
    private VirtualTransform virtualTransform;

    public void Awake()
    {
        virtualTransform = GetComponent<VirtualTransform>();
    }

    public void FixedUpdate()
    {
        ApplyRandomAngularAcceleration();
        ApplyRotation();
    }

    private void ApplyRandomAngularAcceleration()
    {
        Quaternion randomAngularAcceleration = GenerateRandomAngularAcceleration();
        angularVelocity = Quaternion.Slerp(
            angularVelocity,
            angularVelocity * randomAngularAcceleration,
            angularAcceleration * Time.fixedDeltaTime
        );
    }

    private Quaternion GenerateRandomAngularAcceleration()
    {
        return Random.rotationUniform;
    }

    private void ApplyRotation()
    {
        virtualTransform.rotation = Quaternion.Slerp(
            virtualTransform.rotation,
            virtualTransform.rotation * angularVelocity,
            Time.fixedDeltaTime
        );
    }
}
