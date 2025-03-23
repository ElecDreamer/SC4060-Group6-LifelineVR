using UnityEngine;

public class BrownianRotation : MonoBehaviour
{
    public float angularAcceleration = 1.0f;
    private Quaternion angularVelocity = Quaternion.identity;

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
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            transform.rotation * angularVelocity,
            Time.fixedDeltaTime
        );
    }
}
