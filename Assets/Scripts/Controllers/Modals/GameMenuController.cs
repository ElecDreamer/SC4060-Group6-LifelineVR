using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenuController : MonoBehaviour
{
    public GameObject GameMenuPanel;
    public TMP_Text GameDifficultyText;
    public Toggle RedBloodCellModeToggle;
    public Toggle HumanModeToggle;

    /**
     * Menu Modal Methods
     */
    public void OpenGameMenu()
    {
        if (GameMenuPanel.activeSelf == true)
        {
            Debug.Log("GameMenu already opened");
            return;
        }

        Debug.Log("GameMenu Opened");
        Time.timeScale = 0; // Pause game time

        // Set variables values
        GameDifficultyText.SetText(GlobalVariables.Instance.gameDifficulty.ToString());
        UpdateGameModeToggle(GlobalVariables.Instance.gameMode);

        // Show Game Menu
        GameMenuPanel.SetActive(true);
    }

    public void CloseGameMenu()
    {
        Debug.Log("GameMenu Closed");
        GameMenuPanel.SetActive(false);
        Time.timeScale = 1; // Resume game time
    }

    /**
     * Quit Game
     */
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Will close application
    }

    /**
     * Toggle Game Modes
     */
    public void ToggleHumanMode()
    {
        Debug.Log("Human Mode selected");
        Enums.GameMode selectedGameMode = Enums.GameMode.Human;
        if (GlobalVariables.Instance.gameMode == selectedGameMode)
        {
            Debug.Log("Player already in Human Mode");
            return;
        }

        // Toggle to new game mode
        UpdateGameModeToggle(selectedGameMode);

        // Close Game Menu
        CloseGameMenu();

        // Load new game mode scene
        SceneManager.LoadScene("HumanModeScene");
    }

    public void ToggleRedBloodCellMode()
    {
        Debug.Log("Red Blood Cell Mode selected");
        Enums.GameMode selectedGameMode = Enums.GameMode.RedBloodCell;
        if (GlobalVariables.Instance.gameMode == selectedGameMode)
        {
            Debug.Log("Player already in Red Blood Cell Mode");
            return;
        }

        // Toggle to new game mode
        UpdateGameModeToggle(selectedGameMode);

        // Close Game Menu
        CloseGameMenu();

        // Load new game mode scene
        SceneManager.LoadScene("RedBloodCellModeScene");
    }

    private void UpdateGameModeToggle(Enums.GameMode gameMode)
    {
        GlobalVariables.Instance.gameMode = gameMode;
        switch (gameMode)
        {
            case Enums.GameMode.Human:
                HumanModeToggle.SetIsOnWithoutNotify(true);
                RedBloodCellModeToggle.SetIsOnWithoutNotify(false);
                break;
            case Enums.GameMode.RedBloodCell:
                HumanModeToggle.SetIsOnWithoutNotify(false);
                RedBloodCellModeToggle.SetIsOnWithoutNotify(true);
                break;
        }
    }
}
