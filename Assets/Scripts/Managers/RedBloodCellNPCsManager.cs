using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCellNPCsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalVariables.Instance.gameDifficulty != Enums.GameDifficulty.Hard) return;
    }

    public float GetRateOfOxygenIncreaseBasedOnRBCLevel()
    {
        if (GlobalVariables.Instance.gameDifficulty == Enums.GameDifficulty.Easy) return 0.75f; // Easy mode default rate of oxygen increase

        float rbcLevel = GlobalVariables.Instance.redBloodCellLevel.level;
        if (rbcLevel >= 66f)
        {
            return 0.75f;
        } else if (rbcLevel >= 33f)
        {
            return 0.5f;
        } else
        {
            return 0.25f;
        }
    }
}
