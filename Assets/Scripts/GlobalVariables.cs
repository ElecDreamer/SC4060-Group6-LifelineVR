using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public GameObject FridgeInventoryPanelButton;

    public static Enums.GameDifficulty gameDifficulty;
    public static Enums.GameMode gameMode;
    public static List<DataEntities.BloodPack> bloodPacks;
    public static DataEntities.RedBloodCellLevel redBloodCellLevel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Init default GlobalVariables");
        gameDifficulty = Enums.GameDifficulty.Easy;
        gameMode = Enums.GameMode.Human;

        // TODO: Temporary for testing (to remove)
        gameDifficulty = Enums.GameDifficulty.Hard;
        InitHardDifficultyGameConfiguration();
        // END OF TODO
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale == 0) return; // Return if the game is paused

        switch (gameDifficulty)
        {
            case Enums.GameDifficulty.Easy:
                UpdateEasyDifficultyGameObjects();
                break;
            case Enums.GameDifficulty.Hard:
                UpdateHardDifficultyGameObjects();
                break;
        }
    }

    /**
     * Game Difficulty Game Configuration Initialisation
     */
    public static void InitEasyDifficultyGameConfiguration()
    {
        if (gameDifficulty != Enums.GameDifficulty.Easy)
        {
            Debug.LogError("[Init] Game Difficulty is not set to 'Easy'!");
            return;
        }
        Debug.Log("Init Easy Difficulty Game Configuration");
    }

    public static void InitHardDifficultyGameConfiguration()
    {
        if (gameDifficulty != Enums.GameDifficulty.Hard)
        {
            Debug.LogError("[Init] Game Difficulty is not set to 'Hard'!");
            return;
        }
        Debug.Log("Init Hard Difficulty Game Configuration");

        // Blood Packs - Populate with some Super Fresh Blood Packs to start the game
        Debug.Log("\tInit Blood Packs");
        bloodPacks = new List<DataEntities.BloodPack>();
        bloodPacks.Add(new DataEntities.BloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh));
        bloodPacks.Add(new DataEntities.BloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh));

        // Initialize RedBloodCellLevel
        Debug.Log("\tInit RedBloodCellLevel");
        redBloodCellLevel = DataEntities.RedBloodCellLevel.Instance;
    }

    /**
     * Updates
     */
    private void UpdateEasyDifficultyGameObjects()
    {

    }

    private void UpdateHardDifficultyGameObjects()
    {
        UpdateEasyDifficultyGameObjects();

        // Display Fridge Inventory Button
        FridgeInventoryPanelButton.SetActive(true);

        // Update Blood Pack time and state
        foreach (DataEntities.BloodPack bloodPack in bloodPacks)
        {
            bloodPack.UpdateTimeAndState();
        }
    }
}
