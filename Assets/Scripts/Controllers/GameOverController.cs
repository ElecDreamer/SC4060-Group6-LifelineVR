using System.Collections;
using System.Collections.Generic;
using DataEntities;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;

    private string gameOverMessage;
    private bool gameOverTriggered;

    private GameMenuController gameMenuController;
    private FridgeInventoryController fridgeInventoryController;
    private QuestHandControllersMenuController questHandControllersMenuController;
    private O2AndRBCLevelsController o2AndRBCLevelsController;

    private void Start()
    {
        gameOverTriggered = false;

        // Find the Controllers in the scene
        fridgeInventoryController = FindObjectOfType<FridgeInventoryController>();
        if (fridgeInventoryController == null)
            Debug.LogError("FridgeInventoryController not found! Make sure it's in the scene.");

        gameMenuController = FindObjectOfType<GameMenuController>();
        if (gameMenuController == null)
            Debug.LogError("GameMenuController not found! Make sure it's in the scene.");

        questHandControllersMenuController = FindObjectOfType<QuestHandControllersMenuController>();
        if (questHandControllersMenuController == null)
            Debug.LogError("QuestHandControllersMenuController not found! Make sure it's in the scene.");

        o2AndRBCLevelsController = FindObjectOfType<O2AndRBCLevelsController>();
        if (o2AndRBCLevelsController == null)
            Debug.LogError("O2AndRBCLevelsController not found! Make sure it's in the scene.");
    }

    void Update()
    {
        if (!gameOverTriggered)
        {
            if (IsGameOver())
            {
                TriggerGameOver();
                gameOverTriggered = true;
            }
        }
    }

    private bool IsGameOver()
    {
        if (GlobalVariables.Instance.redBloodCellLevel.level <= 0f)
        {
            Debug.Log("GAME OVER! Red Blood Cell Level has reached 0!");
            gameOverMessage = "Red Blood Cell Level has reached 0!";
            return true;
        }
        if (GlobalVariables.Instance.arms.oxygenLevel <= 0f)
        {
            Debug.Log("GAME OVER! Arms Oxygen Level has reached 0!");
            gameOverMessage = "Arms Oxygen Level has reached 0!";
            return true;
        }
        if (GlobalVariables.Instance.legs.oxygenLevel <= 0f)
        {
            Debug.Log("GAME OVER! Legs Oxygen Level has reached 0!");
            gameOverMessage = "Legs Oxygen Level has reached 0!";
            return true;
        }
        if (GlobalVariables.Instance.brain.oxygenLevel <= 0f)
        {
            Debug.Log("GAME OVER! Brain Oxygen Level has reached 0!");
            gameOverMessage = "Brain Oxygen Level has reached 0!";
            return true;
        }

        return false;
    }

    private void TriggerGameOver()
    {
        GlobalVariables.Instance.gameOver = true;
        gameOverText.text = gameOverMessage;
        gameOverPanel.SetActive(true);

        // Hide all Modals
        gameMenuController.CloseGameMenu();
        fridgeInventoryController.CloseFridgeInventory();
        questHandControllersMenuController.CloseQuestHandControllersMenu();
        o2AndRBCLevelsController.Hide();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Will close application
    }

    public void NavigateToMainMenu()
    {
        if (!canClick) return; // workaround for single click triggering twice
        canClick = false;

        // Reset variables
        GlobalVariables.Instance.gameStarted = false;
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Easy;
        GlobalVariables.Instance.gameMode = Enums.GameMode.Human;
        GlobalVariables.Instance.bloodPacks = null;

        Destroy(GlobalVariables.Instance.arms);
        Destroy(GlobalVariables.Instance.legs);
        Destroy(GlobalVariables.Instance.brain);
        Destroy(GlobalVariables.Instance.oxygenPlayerStorage);

        Destroy(GlobalVariables.Instance.redBloodCellLevel);

        GlobalVariables.Instance.canRun = true;
        GlobalVariables.Instance.isRunning = false;
        GlobalVariables.Instance.isLifting = false;

        GlobalVariables.Instance.gameOver = false;

        // Load Main Menu Scene
        SceneManager.LoadScene("MainMenu");

        // Reset after short delay; workaround for single click triggering twice
        Invoke(nameof(ResetClick), 0.2f);
    }

    private bool canClick = true; // workaround for single click triggering twice
    private void ResetClick() // workaround for single click triggering twice
    {
        canClick = true;
    }
}
