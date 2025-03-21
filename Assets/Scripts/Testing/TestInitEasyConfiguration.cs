using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInitEasyConfiguration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Easy;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitEasyDifficultyGameConfiguration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
