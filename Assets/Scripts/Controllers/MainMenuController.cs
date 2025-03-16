using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartEasyGameBtn()
    {
        Debug.Log("Starting Easy Game");
        GlobalVariables.gameDifficulty = Enums.GameDifficulty.Easy;
        SceneManager.LoadScene("HumanModeScene");
        GlobalVariables.InitEasyDifficultyGameConfiguration();
    }

    public void StartHardGameBtn()
    {
        Debug.Log("Starting Hard Game");
        GlobalVariables.gameDifficulty = Enums.GameDifficulty.Hard;
        SceneManager.LoadScene("HumanModeScene");
        GlobalVariables.InitHardDifficultyGameConfiguration();
    }
}
