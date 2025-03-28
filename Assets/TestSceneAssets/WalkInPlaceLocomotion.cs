using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkInPlaceLocomotion : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject leftHand, rightHand;

    Vector3 previousPosLeft, previousPosRight, direction;
    Vector3 gravity = new Vector3(0, -9.8f, 0);

    [SerializeField] float speed = 4;

    private readonly float RUNNING_THRESHOLD = 0.05f;

    private RunningController runningController;

    // Start is called before the first frame update
    void Start()
    {
        SetPreviousPos();

        // Init controllers
        runningController = FindObjectOfType<RunningController>();
        if (runningController == null)
            Debug.LogError("RunningController not found! Make sure it's in the scene.");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalVariables.Instance.gameStarted || !GlobalVariables.Instance.canRun) return;

        Vector3 leftHandVelocity = leftHand.transform.position - previousPosLeft;
        Vector3 rightHandVelocity = rightHand.transform.position - previousPosRight;
        float totalVelocity = +leftHandVelocity.magnitude * 0.8f + rightHandVelocity.magnitude * 0.8f;

        bool currentlyRunning = totalVelocity >= RUNNING_THRESHOLD;
        if (currentlyRunning)
        {
            direction = Camera.main.transform.forward;
            characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up));
        }

        characterController.Move(gravity * Time.deltaTime);
        SetPreviousPos();

        // Update isRunning State
        runningController.UpdateIsRunningState(currentlyRunning);
    }

    void SetPreviousPos()
    {
        previousPosLeft = leftHand.transform.position;
        previousPosRight = rightHand.transform.position;
    }
}
