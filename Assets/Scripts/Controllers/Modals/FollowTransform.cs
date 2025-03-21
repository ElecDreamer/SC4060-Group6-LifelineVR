using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;  // The player's transform
    [SerializeField] private float followSpeed = 1f;  // The speed of movement
    [SerializeField] private float distanceInFront = 2f;  // The distance to stay in front of the player
    [SerializeField] private float heightOffset = 0f;  // Height offset for the UI (higher or lower)

    private Transform _thisTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Cache this lookup
        _thisTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the position directly in front of the player
        Vector3 direction = playerTransform.forward;  // Direction the player is facing
        Vector3 desiredPosition = playerTransform.position + direction * distanceInFront;

        // Apply the height offset (make the UI lower by adjusting the Y position)
        desiredPosition.y = playerTransform.position.y + heightOffset;

        // Smoothly move the UI towards the target position (in front of the player)
        _thisTransform.position = Vector3.Lerp(_thisTransform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Make the UI face the player
        _thisTransform.LookAt(playerTransform);  // Keep looking at the player
        _thisTransform.Rotate(0f, 180f, 0f);
    }
}
