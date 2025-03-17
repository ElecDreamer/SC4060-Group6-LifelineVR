using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayModalsButtonsController : MonoBehaviour
{
    public GameObject FridgeInventoryPanelButton;

    private void Start()
    {
        Debug.Log($"[DisplayModalsButtonsController] GameDifficulty: {GlobalVariables.Instance.gameDifficulty}");
        DisplayFridgeInventoryPannelButtonOnlyInHardDifficulty();
    }
    

    private void DisplayFridgeInventoryPannelButtonOnlyInHardDifficulty()
    {
        // Display FridgeInventoryPanelButton if GameDifficulty is "Hard"
        if (GlobalVariables.Instance.gameDifficulty == Enums.GameDifficulty.Hard)
            FridgeInventoryPanelButton.SetActive(true);
    }

}
