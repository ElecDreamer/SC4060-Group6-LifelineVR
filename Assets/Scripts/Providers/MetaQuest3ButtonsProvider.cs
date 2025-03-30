using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MetaQuest3ButtonsProvider: MonoBehaviour
{
    public InputActionReference rightButtonAAction;
    public InputActionReference rightButtonBAction;
    public InputActionReference leftButtonXAction;
    public InputActionReference leftButtonYAction;

    public InputActionReference rightSelectAction;
    public InputActionReference leftSelectAction;

    private GameMenuController gameMenuController;
    private FridgeInventoryController fridgeInventoryController;
    private QuestHandControllersMenuController questHandControllersMenuController;
    private O2AndRBCLevelsController o2AndRBCLevelsController;

    // Start is called before the first frame update
    void Start()
    {
        rightButtonAAction.action.started += RightButtonAWasPressed;
        rightButtonAAction.action.canceled += RightButtonAWasReleased;

        rightButtonBAction.action.started += RightButtonBWasPressed;
        rightButtonBAction.action.canceled += RightButtonBWasReleased;

        leftButtonXAction.action.started += LeftButtonXWasPressed;
        leftButtonXAction.action.canceled += LeftButtonXWasReleased;

        leftButtonYAction.action.started += LeftButtonYWasPressed;
        leftButtonYAction.action.canceled += LeftButtonYWasReleased;

        rightSelectAction.action.started += RightSelectActionWasPressed;
        rightSelectAction.action.canceled += RightSelectActionWasReleased;
        leftSelectAction.action.started += LeftSelectActionWasPressed;
        leftSelectAction.action.canceled += LeftSelectActionWasReleased;

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

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        // Unsubscribing from the input actions ensures Unity won’t try to call your method after the scene unloads and objects are gone.
        rightButtonAAction.action.started -= RightButtonAWasPressed;
        rightButtonAAction.action.canceled -= RightButtonAWasReleased;

        rightButtonBAction.action.started -= RightButtonBWasPressed;
        rightButtonBAction.action.canceled -= RightButtonBWasReleased;

        leftButtonXAction.action.started -= LeftButtonXWasPressed;
        leftButtonXAction.action.canceled -= LeftButtonXWasReleased;

        leftButtonYAction.action.started -= LeftButtonYWasPressed;
        leftButtonYAction.action.canceled -= LeftButtonYWasReleased;

        rightSelectAction.action.started -= RightSelectActionWasPressed;
        rightSelectAction.action.canceled -= RightSelectActionWasReleased;
        leftSelectAction.action.started -= LeftSelectActionWasPressed;
        leftSelectAction.action.canceled -= LeftSelectActionWasReleased;
    }

    /**
     * Right Button A
     */
    void RightButtonAWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Button A pressed");
        if (GlobalVariables.Instance.gameOver) return;

        if (GlobalVariables.Instance.gameDifficulty != Enums.GameDifficulty.Hard) return;

        if (!fridgeInventoryController.FridgeInventoryPanel.activeSelf)
        {
            Debug.Log("Open Fridge");
            fridgeInventoryController.OpenFridgeInventory();

            // Close Other Menus
            if (gameMenuController.GameMenuPanel.activeSelf) gameMenuController.CloseGameMenu();
            if (questHandControllersMenuController.QuestControllerMenuContainerPanel.activeSelf) questHandControllersMenuController.CloseQuestHandControllersMenu();
        } else
        {
            Debug.Log("Close Fridge");
            fridgeInventoryController.CloseFridgeInventory();
        }
    }

    void RightButtonAWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Right Button A released");
        if (GlobalVariables.Instance.gameOver) return;
    }

    /**
     * Right Button B
     */
    void RightButtonBWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Button B pressed");
        if (GlobalVariables.Instance.gameOver) return;

        if (!o2AndRBCLevelsController.O2AndRBCLevelsBarPanel.activeSelf)
        {
            Debug.Log("Open Fridge");
            o2AndRBCLevelsController.Display();
            
            // Note: Don't need to close other menus
        }
        else
        {
            Debug.Log("Close Fridge");
            o2AndRBCLevelsController.Hide();
        }
    }

    void RightButtonBWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Right Button B released");
        if (GlobalVariables.Instance.gameOver) return;
    }

    /**
     * Left Button X
     */
    void LeftButtonXWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Button X pressed");
        if (GlobalVariables.Instance.gameOver) return;

        if (!gameMenuController.GameMenuPanel.activeSelf)
        {
            Debug.Log("Open Game Menu");
            gameMenuController.OpenGameMenu();

            // Close Other Menus
            if (fridgeInventoryController.FridgeInventoryPanel.activeSelf) fridgeInventoryController.CloseFridgeInventory();
            if (questHandControllersMenuController.QuestControllerMenuContainerPanel.activeSelf) questHandControllersMenuController.CloseQuestHandControllersMenu();
        }
        else
        {
            Debug.Log("Close Game Menu");
            gameMenuController.CloseGameMenu();
        }
    }

    void LeftButtonXWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Left Button X released");
        if (GlobalVariables.Instance.gameOver) return;
    }

    /**
     * Left Button Y
     */
    void LeftButtonYWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Button Y pressed");
        if (GlobalVariables.Instance.gameOver) return;

        if (!questHandControllersMenuController.QuestControllerMenuContainerPanel.activeSelf)
        {
            Debug.Log("Open Controllers Menu");
            questHandControllersMenuController.OpenQuestHandControllersMenu();

            // Close Other Menus
            if (fridgeInventoryController.FridgeInventoryPanel.activeSelf) fridgeInventoryController.CloseFridgeInventory();
            if (gameMenuController.GameMenuPanel.activeSelf) gameMenuController.CloseGameMenu();
        }
        else
        {
            Debug.Log("Close Controllers Menu");
            questHandControllersMenuController.CloseQuestHandControllersMenu();
        }
    }

    void LeftButtonYWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Left Button Y released");
        if (GlobalVariables.Instance.gameOver) return;
    }

    /**
     * Right Select Action
     */
    void RightSelectActionWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Select Action pressed");
        if (GlobalVariables.Instance.gameOver) return;
    }

    void RightSelectActionWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Right Select Action released");
        if (GlobalVariables.Instance.gameOver) return;
    }

    /**
     * Left Select Action
     */
    void LeftSelectActionWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Select Action pressed");
        if (GlobalVariables.Instance.gameOver) return;
    }

    void LeftSelectActionWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Left Select Action released");
        if (GlobalVariables.Instance.gameOver) return;
    }

}
