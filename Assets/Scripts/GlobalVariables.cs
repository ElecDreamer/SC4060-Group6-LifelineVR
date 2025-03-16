using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static Enums.GameDifficulty gameDifficulty;
    public static Enums.GameMode gameMode;
    public static List<DataEntities.BloodPack> bloodPacks;
    public static DataEntities.RedBloodCellLevel redBloodCellLevel;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Init GlobalVariables");
        gameDifficulty = Enums.GameDifficulty.Easy;
        gameMode = Enums.GameMode.Human;

        // Blood Packs - Populate with some Super Fresh Blood Packs to start the game
        Debug.Log("\tInit Blood Packs");
        bloodPacks = new List<DataEntities.BloodPack>();
        bloodPacks.Add(new DataEntities.BloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh));
        bloodPacks.Add(new DataEntities.BloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh));

        // Initialize RedBloodCellLevel
        Debug.Log("\tInit RedBloodCellLevel");
        redBloodCellLevel = DataEntities.RedBloodCellLevel.Instance;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeScale == 0) return; // Return if the game is paused

        // Update Blood Pack time and state
        foreach (DataEntities.BloodPack bloodPack in bloodPacks)
        {
            bloodPack.UpdateTimeAndState();
        }
    }
}
