using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static Enums.GameDifficulty gameDifficulty;
    public static Enums.GameMode gameMode;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Init GlobalVariables");
        gameDifficulty = Enums.GameDifficulty.Easy;
        gameMode = Enums.GameMode.Human;
    }
}
