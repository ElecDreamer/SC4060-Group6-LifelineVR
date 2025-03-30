using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartEasyGameBtn()
    {
        if (!canClick) return; // workaround for single click triggering twice
        canClick = false;

        Debug.Log("Starting Easy Game");
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Easy;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitEasyDifficultyGameConfiguration();

        SceneManager.LoadScene("HumanModeScene");

        // Reset after short delay; workaround for single click triggering twice
        Invoke(nameof(ResetAddOxygenClick), 0.2f);
    }

    public void StartHardGameBtn()
    {
        if (!canClick) return; // workaround for single click triggering twice
        canClick = false;

        Debug.Log("Starting Hard Game");
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Hard;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitHardDifficultyGameConfiguration();

        SceneManager.LoadScene("HumanModeScene");

        // Reset after short delay; workaround for single click triggering twice
        Invoke(nameof(ResetAddOxygenClick), 0.2f);
    }

    private bool canClick = true; // workaround for single click triggering twice
    private void ResetAddOxygenClick() // workaround for single click triggering twice
    {
        canClick = true;
    }
}
