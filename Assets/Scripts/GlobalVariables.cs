using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariables : MonoBehaviour
{
    public bool gameStarted;

    public Enums.GameDifficulty gameDifficulty;
    public Enums.GameMode gameMode;
    public List<DataEntities.BloodPack> bloodPacks;
    public DataEntities.RedBloodCellLevel redBloodCellLevel;

    // Oxygen-related body parts
    public DataEntities.Arms arms;
    public DataEntities.Legs legs;
    public DataEntities.Brain brain;

    /**
     * Skeleton Instance
     */
    private static GlobalVariables instance; // Singleton instance
    public static GlobalVariables Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new("GlobalVariablesManager");
                instance = obj.AddComponent<GlobalVariables>();
                DontDestroyOnLoad(obj); // Prevent destruction when loading a new scene
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this GameObject alive across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Init default GlobalVariables");
        gameStarted = false;
        gameDifficulty = Enums.GameDifficulty.Easy;
        gameMode = Enums.GameMode.Human;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene changes
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe to avoid memory leaks
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene Loaded: {scene.name}");
        // You can reinitialize scene-specific UI elements if needed
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameStarted || Time.timeScale == 0) return; // Return if the game has not started or is paused

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
    public void InitEasyDifficultyGameConfiguration()
    {
        if (gameDifficulty != Enums.GameDifficulty.Easy)
        {
            Debug.LogError("[Init] Game Difficulty is not set to 'Easy'!");
            return;
        }
        
        // Common Game Configuration
        InitCommonGameConfiguration();

        Debug.Log("Init Easy Difficulty Game Configuration");
    }

    public void InitHardDifficultyGameConfiguration()
    {
        if (gameDifficulty != Enums.GameDifficulty.Hard)
        {
            Debug.LogError("[Init] Game Difficulty is not set to 'Hard'!");
            return;
        }

        // Common Game Configuration
        InitCommonGameConfiguration();

        Debug.Log("Init Hard Difficulty Game Configuration");

        // Blood Packs - Populate with some Super Fresh Blood Packs to start the game
        Debug.Log("\tInit Blood Packs");
        bloodPacks = new List<DataEntities.BloodPack>
        {
            new DataEntities.BloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh),
            new DataEntities.BloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh)
        };

        // Initialize RedBloodCellLevel
        Debug.Log("\tInit RedBloodCellLevel");
        redBloodCellLevel = DataEntities.RedBloodCellLevel.Instance;
    }

    private void InitCommonGameConfiguration()
    {
        Debug.Log("Init Common Game Configuration");

        // Initialize Body Parts
        Debug.Log("\tInit Arms, Legs, and Brain");
        arms = DataEntities.Arms.Instance;
        legs = DataEntities.Legs.Instance;
        brain = DataEntities.Brain.Instance;
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

        // Update Blood Pack time and state
        foreach (DataEntities.BloodPack bloodPack in bloodPacks)
        {
            bloodPack.UpdateTimeAndState();
        }
    }
}
