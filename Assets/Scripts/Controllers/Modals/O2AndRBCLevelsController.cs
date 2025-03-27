using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2AndRBCLevelsController : MonoBehaviour
{
    public GameObject O2AndRBCLevelsBarPanel;

    public Image maskRBCLevel;
    public Image maskArmsO2Level;
    public Image maskLegsO2Level;
    public Image maskBrainO2Level;

    // Update is called once per frame
    void Update()
    {
        if (!GlobalVariables.Instance.gameStarted) return;

        if (GlobalVariables.Instance.gameDifficulty == Enums.GameDifficulty.Hard)
        {
            GetCurrentFillRBCLevel();
        }
        GetCurrentFillOxygenLevels();
    }

    void GetCurrentFillRBCLevel()
    {
        float current = GlobalVariables.Instance.redBloodCellLevel.level;
        float maximum = DataEntities.RedBloodCellLevel.MAXIMUM_LEVEL;
        float fillAmount = current / maximum;
        maskRBCLevel.fillAmount = fillAmount;
    }

    void GetCurrentFillOxygenLevels()
    {
        // Arms
        float current = GlobalVariables.Instance.arms.oxygenLevel;
        float maximum = DataEntities.Arms.MAXIMUM_OXYGEN_LEVEL;
        float fillAmount = current / maximum;
        maskArmsO2Level.fillAmount = fillAmount;

        // Legs
        current = GlobalVariables.Instance.legs.oxygenLevel;
        maximum = DataEntities.Legs.MAXIMUM_OXYGEN_LEVEL;
        fillAmount = current / maximum;
        maskLegsO2Level.fillAmount = fillAmount;

        // Brain
        current = GlobalVariables.Instance.brain.oxygenLevel;
        maximum = DataEntities.Brain.MAXIMUM_OXYGEN_LEVEL;
        fillAmount = current / maximum;
        maskBrainO2Level.fillAmount = fillAmount;
    }
    public void Display()
    {
        if (O2AndRBCLevelsBarPanel.activeSelf == true)
        {
            Debug.Log("O2 And RBC Levels Menu is already opened");
            return;
        }

        Debug.Log("O2 And RBC Levels Menu Opened");
        //Time.timeScale = 0; // Pause game time

        O2AndRBCLevelsBarPanel.SetActive(true);
    }

    public void Hide()
    {
        Debug.Log("O2 And RBC Levels Menu Closed");
        O2AndRBCLevelsBarPanel.SetActive(false);
        //Time.timeScale = 1; // Resume game time
    }
}
