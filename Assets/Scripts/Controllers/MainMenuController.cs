using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartEasyGameBtn()
    {
        // TODO: Easy Game Configuration
        Debug.Log("Starting Easy Game");
        GlobalVariables.gameDifficulty = Enums.GameDifficulty.Easy;
        SceneManager.LoadScene("HumanModeScene");
    }

    public void StartHardGameBtn()
    {
        // TODO: Hard Game Configuration
        Debug.Log("Starting Hard Game");
        GlobalVariables.gameDifficulty = Enums.GameDifficulty.Hard;
        SceneManager.LoadScene("HumanModeScene");
    }
}
