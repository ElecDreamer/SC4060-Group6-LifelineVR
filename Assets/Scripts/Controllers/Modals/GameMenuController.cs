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
    public Toggle WhiteBloodCellModeToggle;
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
        GameDifficultyText.SetText(GlobalVariables.gameDifficulty.ToString());
        UpdateGameModeToggle(GlobalVariables.gameMode);

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
     * Open Fridge
     */
    public void OpenFridgeInventory()
    {
        Debug.Log("Open Fridge Inventory");

        // Close Game Menu
        CloseGameMenu();
    }

    /**
     * Toggle Game Modes
     */
    public void ToggleHumanMode()
    {
        Debug.Log("Human Mode selected");
        Enums.GameMode selectedGameMode = Enums.GameMode.Human;

        // Toggle to new game mode
        UpdateGameModeToggle(selectedGameMode);

        // Close Game Menu
        CloseGameMenu();

        if (GlobalVariables.gameMode == selectedGameMode)
        {
            Debug.Log("Player already in Human Mode");
        } else
        {
            // Load new game mode scene
            SceneManager.LoadScene("HumanModeScene");
        }
    }

    public void ToggleRedBloodCellMode()
    {
        Debug.Log("Red Blood Cell Mode selected");
        Enums.GameMode selectedGameMode = Enums.GameMode.RedBloodCell;

        // Toggle to new game mode
        UpdateGameModeToggle(selectedGameMode);

        // Close Game Menu
        CloseGameMenu();

        if (GlobalVariables.gameMode == selectedGameMode)
        {
            Debug.Log("Player already in Red Blood Cell Mode");
        }
        else
        {
            // Load new game mode scene
            SceneManager.LoadScene("RedBloodCellModeScene");
        }
    }

    public void ToggleWhiteBloodCellMode()
    {
        Debug.Log("White Blood Cell Mode selected");
        Enums.GameMode selectedGameMode = Enums.GameMode.WhiteBloodCell;

        // Toggle to new game mode
        UpdateGameModeToggle(selectedGameMode);

        // Close Game Menu
        CloseGameMenu();

        if (GlobalVariables.gameMode == selectedGameMode)
        {
            Debug.Log("Player already in White Blood Cell Mode");
        }
        else
        {
            // Load new game mode scene
            SceneManager.LoadScene("WhiteBloodCellModeScene");
        }
    }

    private void UpdateGameModeToggle(Enums.GameMode gameMode)
    {
        GlobalVariables.gameMode = gameMode;
        switch (gameMode)
        {
            case Enums.GameMode.Human:
                HumanModeToggle.SetIsOnWithoutNotify(true);
                RedBloodCellModeToggle.SetIsOnWithoutNotify(false);
                WhiteBloodCellModeToggle.SetIsOnWithoutNotify(false);
                break;
            case Enums.GameMode.RedBloodCell:
                HumanModeToggle.SetIsOnWithoutNotify(false);
                RedBloodCellModeToggle.SetIsOnWithoutNotify(true);
                WhiteBloodCellModeToggle.SetIsOnWithoutNotify(false);
                break;
            case Enums.GameMode.WhiteBloodCell:
                HumanModeToggle.SetIsOnWithoutNotify(false);
                RedBloodCellModeToggle.SetIsOnWithoutNotify(false);
                WhiteBloodCellModeToggle.SetIsOnWithoutNotify(true);
                break;
        }
    }
}
