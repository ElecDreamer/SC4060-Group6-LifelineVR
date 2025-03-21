using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandControllersMenuController : MonoBehaviour
{
    public GameObject QuestControllerMenuContainerPanel;
    public GameObject EasyDifficultyQuestControllerMenuPanel;
    public GameObject HardDifficultyQuestControllerMenuPanel;

    // Start is called before the first frame update
    private void SelectMenuToDisplay()
    {
        Enums.GameDifficulty gameDifficulty = GlobalVariables.Instance.gameDifficulty;
        switch (gameDifficulty)
        {
            case Enums.GameDifficulty.Easy:
                EasyDifficultyQuestControllerMenuPanel.SetActive(true);
                HardDifficultyQuestControllerMenuPanel.SetActive(false);
                break;
            case Enums.GameDifficulty.Hard:
                EasyDifficultyQuestControllerMenuPanel.SetActive(false);
                HardDifficultyQuestControllerMenuPanel.SetActive(true);
                break;
        }
    }
    
    public void OpenQuestHandControllersMenu()
    {
        if (QuestControllerMenuContainerPanel.activeSelf == true)
        {
            Debug.Log("Quest Hand Controllers Menu is already opened");
            return;
        }

        Debug.Log("Quest Hand Controllers Menu Opened");
        //Time.timeScale = 0; // Pause game time

        SelectMenuToDisplay();
        QuestControllerMenuContainerPanel.SetActive(true);
    }

    public void CloseQuestHandControllersMenu()
    {
        Debug.Log("Quest Hand Controllers Menu Closed");
        QuestControllerMenuContainerPanel.SetActive(false);
        //Time.timeScale = 1; // Resume game time
    }
}
