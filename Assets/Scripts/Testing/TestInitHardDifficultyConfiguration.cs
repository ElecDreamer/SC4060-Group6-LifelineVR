using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInitHardDifficultyConfiguration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GlobalVariables.Instance.gameDifficulty = Enums.GameDifficulty.Hard;
        GlobalVariables.Instance.gameStarted = true;
        GlobalVariables.Instance.InitHardDifficultyGameConfiguration();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
