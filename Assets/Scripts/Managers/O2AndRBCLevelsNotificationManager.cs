using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2AndRBCLevelsNotificationManager : MonoBehaviour
{
    private NotificationsController notificationController;
    private readonly float NOTIFICATION_DURATION = 2f;

    private bool warnedRedBloodCellLevel;
    private bool warnedArmsO2Level;
    private bool warnedLegsO2Level;
    private bool warnedBrainO2Level;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Init O2AndRBCLevelsNotificationManager");

        // Find the NotificationsController in the scene
        notificationController = FindObjectOfType<NotificationsController>();

        if (notificationController == null)
        {
            Debug.LogError("NotificationsController not found! Make sure it's in the scene.");
        }

        warnedRedBloodCellLevel = true;
        warnedArmsO2Level = true;
        warnedLegsO2Level = true;
        warnedBrainO2Level = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalVariables.Instance.gameStarted) return;

        if (GlobalVariables.Instance.gameDifficulty == Enums.GameDifficulty.Hard)
            WarnRedBloodCellLevel();
        
        WarnOxygenLevels();
    }

    private void WarnRedBloodCellLevel()
    {
        if (GlobalVariables.Instance.gameDifficulty != Enums.GameDifficulty.Hard) return;

        // Only warn once when the level reach below 50 (will only warn again if the level ever goes back above 50)
        if (GlobalVariables.Instance.redBloodCellLevel.level < 50 && !warnedRedBloodCellLevel)
        {
            warnedRedBloodCellLevel = true;
            notificationController.DisplayRBCLevelWarningNotification(
                "Warning! Red Blood Cell Level is below 50!",
                NOTIFICATION_DURATION);
        } else if (GlobalVariables.Instance.redBloodCellLevel.level >= 50 && warnedRedBloodCellLevel)
        {
            warnedRedBloodCellLevel = false;
        }
    }

    private void WarnOxygenLevels()
    {
        // Only warn once when the level reach below 50 (will only warn again if the level ever goes back above 50)
        // Arms
        if (GlobalVariables.Instance.arms.oxygenLevel < 50 && !warnedArmsO2Level)
        {
            warnedArmsO2Level = true;
            notificationController.DisplayArmsO2LevelWarningNotification(
                "Warning! Arms Oxygen Level is below 50!",
                NOTIFICATION_DURATION);
        }
        else if (GlobalVariables.Instance.arms.oxygenLevel >= 50 && warnedArmsO2Level)
        {
            warnedArmsO2Level = false;
        }

        // Legs
        if (GlobalVariables.Instance.legs.oxygenLevel < 50 && !warnedLegsO2Level)
        {
            warnedLegsO2Level = true;
            notificationController.DisplayLegsO2LevelWarningNotification(
                "Warning! Legs Oxygen Level is below 50!",
                NOTIFICATION_DURATION);
        }
        else if (GlobalVariables.Instance.legs.oxygenLevel >= 50 && warnedLegsO2Level)
        {
            warnedLegsO2Level = false;
        }

        // Brain
        if (GlobalVariables.Instance.brain.oxygenLevel < 50 && !warnedBrainO2Level)
        {
            warnedBrainO2Level = true;
            notificationController.DisplayBrainO2LevelWarningNotification(
                "Warning! Brain Oxygen is below 50!",
                NOTIFICATION_DURATION);
        }
        else if (GlobalVariables.Instance.brain.oxygenLevel >= 50 && warnedBrainO2Level)
        {
            warnedBrainO2Level = false;
        }
    }
}
