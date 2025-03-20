using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2AndRBCLevelsController : MonoBehaviour
{
    public GameObject RBCLevelBarPanel;

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
}
