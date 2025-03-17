using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMainMenuController : MonoBehaviour
{
    /**
     * For testing purposes
     */
    public void TestStartEasyGameBtnWithoutLoadingScene()
    {
        Debug.Log("Starting Easy Game");
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Easy;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitEasyDifficultyGameConfiguration();
    }

    public void TestStartHardGameBtnWithoutLoadingScene()
    {
        Debug.Log("Starting Hard Game");
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Hard;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitHardDifficultyGameConfiguration();
    }
}
