using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartEasyGameBtn()
    {
        Debug.Log("Starting Easy Game");
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Easy;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitEasyDifficultyGameConfiguration();

        SceneManager.LoadScene("HumanModeScene");
    }

    public void StartHardGameBtn()
    {
        Debug.Log("Starting Hard Game");
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Hard;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitHardDifficultyGameConfiguration();

        SceneManager.LoadScene("RedBloodCellModeScene");
    }
}
