using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningController : MonoBehaviour
{
    public GameObject tunnelingVigenette;

    private readonly float DURATION_TO_CHANGE_STATE = 1.5f;
    private readonly float RUN_GRACE_DURATION = 0.3f;

    private float runningTimer = 0f;
    private float stoppedTimer = 0f;
    private float runGraceTimer = 0f;

    private O2AndRBCLevelsController o2AndRBCLevelsController;

    private void Start()
    {
        // Init controllers
        o2AndRBCLevelsController = FindObjectOfType<O2AndRBCLevelsController>();
        if (o2AndRBCLevelsController == null)
            Debug.LogError("O2AndRBCLevelsController not found! Make sure it's in the scene.");
    }

    public void UpdateIsRunningState(bool currentlyRunning)
    {
        if (currentlyRunning)
        {
            runningTimer += Time.deltaTime;
            stoppedTimer = 0f;
            runGraceTimer = 0f;

            if (!GlobalVariables.Instance.isRunning && runningTimer >= DURATION_TO_CHANGE_STATE)
            {
                Debug.Log($"Player has been running for {(int)DURATION_TO_CHANGE_STATE} seconds");
                GlobalVariables.Instance.isRunning = true;

                // Toggle Oxygen Demand
                GlobalVariables.Instance.legs.ToggleToRunningOxygenDemand();

                // Activate tunnelingVigenette
                tunnelingVigenette.SetActive(true);

                // Update Legs O2 Levels Modal UI
                o2AndRBCLevelsController.SetLegsActiveUI(true);
            }
        }
        else
        {
            runGraceTimer += Time.deltaTime;

            if (runGraceTimer >= RUN_GRACE_DURATION)
            {
                stoppedTimer += Time.deltaTime;
                runningTimer = 0f;

                if (GlobalVariables.Instance.isRunning && stoppedTimer >= DURATION_TO_CHANGE_STATE)
                {
                    Debug.Log($"Player has stopped running for {(int)DURATION_TO_CHANGE_STATE} seconds");
                    GlobalVariables.Instance.isRunning = false;    

                    // Toggle Oxygen Demand
                    GlobalVariables.Instance.legs.ToggleToDefaultOxygenDemand();

                    // Disable tunnelingVigenette
                    tunnelingVigenette.SetActive(false);

                    // Update Legs O2 Levels Modal UI
                    o2AndRBCLevelsController.SetLegsActiveUI(false);
                }
            }
        }
    }
}
