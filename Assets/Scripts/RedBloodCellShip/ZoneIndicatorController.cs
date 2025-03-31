using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ZoneIndicatorController : MonoBehaviour
{
    public RedBloodCellShipManager shipManager;

    public GameObject armsZonePanel;
    public GameObject legsZonePanel;
    public GameObject brainZonePanel;
    public GameObject lungsZonePanel;
    public GameObject emptyZonePanel;

    private void FixedUpdate()
    {
        string currentZone = shipManager.playerRBCModeZoneTracker.currentZone;

        armsZonePanel.SetActive(currentZone == "Arms Zone");
        legsZonePanel.SetActive(currentZone == "Legs Zone");
        brainZonePanel.SetActive(currentZone == "Brain Zone");
        lungsZonePanel.SetActive(currentZone == "Lungs Zone");
        emptyZonePanel.SetActive(currentZone == "");
    }
}
