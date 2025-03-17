using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedBloodCellModeSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"GameMode: {GlobalVariables.Instance.gameMode}; GameDifficulty: {GlobalVariables.Instance.gameDifficulty}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NavigateToHumanMode()
    {
        GlobalVariables.Instance.gameMode = Enums.GameMode.Human;
        SceneManager.LoadScene("HumanModeScene");
    }
}
