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

    public Image maskArmsO2LevelFill;
    public Image maskLegsO2LevelFill;

    public GameObject armsActiveImage;
    public GameObject legsActiveImage;

    private Color INACTIVE_COLOR = new Color(0.6f, 0.8431373f, 0.9333333f, 1);
    private Color ACTIVE_COLOR = new Color(0.9372549f, 0.8588235f, 0.827451f, 1);

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

    public void SetArmsActiveUI(bool active)
    {
        armsActiveImage.SetActive(active);
        maskArmsO2LevelFill.color = active ? ACTIVE_COLOR : INACTIVE_COLOR;
    }

    public void SetLegsActiveUI(bool active)
    {
        legsActiveImage.SetActive(active);
        maskLegsO2LevelFill.color = active ? ACTIVE_COLOR : INACTIVE_COLOR;
    }

    /**
     * Open/Close Modal
     */
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
