using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCellModeSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game mode: " + GlobalVariables.gameMode);
        Debug.Log("Game difficulty: " + GlobalVariables.gameDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
