using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanModeSceneController : MonoBehaviour
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

    public void NavigateToRedBloodCellMode()
    {
        GlobalVariables.Instance.gameMode = Enums.GameMode.RedBloodCell;
        SceneManager.LoadScene("RedBloodCellModeScene");
    }
}
