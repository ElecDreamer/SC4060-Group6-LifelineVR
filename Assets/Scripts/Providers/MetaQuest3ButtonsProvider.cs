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

    private GameMenuController gameMenuController;
    private FridgeInventoryController fridgeInventoryController;
    private QuestHandControllersMenuController questHandControllersMenuController;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * Right Button A
     */
    void RightButtonAWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Button A pressed");

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
    }

    /**
     * Right Button B
     */
    void RightButtonBWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Right Button B pressed");
        Debug.Log("No action");
    }

    void RightButtonBWasReleased(InputAction.CallbackContext context)
    {
        Debug.Log("Right Button B released");
    }

    /**
     * Left Button X
     */
    void LeftButtonXWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Button X pressed");

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
    }

    /**
     * Left Button Y
     */
    void LeftButtonYWasPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Left Button Y pressed");

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
    }

}
