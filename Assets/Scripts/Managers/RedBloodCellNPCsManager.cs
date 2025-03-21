using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBloodCellNPCsManager : MonoBehaviour
{
    public float currentRedBloodCellLevel;
    public int numberOfRedBloodCellsNPCs;

    // Start is called before the first frame update
    void Start()
    {
        currentRedBloodCellLevel = DataEntities.RedBloodCellLevel.MAXIMUM_LEVEL;
    }

    // Update is called once per frame
    void Update()
    {
        float newRedBloodCellLevel = GlobalVariables.Instance.redBloodCellLevel.level;
        if (currentRedBloodCellLevel != newRedBloodCellLevel)
        {
            currentRedBloodCellLevel = newRedBloodCellLevel;
            UpdateNumberOfRedBloodCellsNPCs();
            UpdateRedBloodCellsNPCsGameUI();
        }
    }

    private void UpdateNumberOfRedBloodCellsNPCs()
    {
        // Every 2 RBC level = 1 RBC NPC
        numberOfRedBloodCellsNPCs = Mathf.FloorToInt(currentRedBloodCellLevel / 2);
    }

    public void UpdateRedBloodCellsNPCsGameUI()
    {
        // TODO: Update Game UI for RBC NPCs. If RBC falls belows a level, show less RBCs in the bloodstream
        // DataEntities.RedBloodCellNPC redBloodCellNPC = new();
    }
}
