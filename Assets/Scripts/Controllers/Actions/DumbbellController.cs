using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DumbbellController : MonoBehaviour
{
    public GameObject startLiftingModalPanel;
    public GameObject stoppedLiftingModalPanel;

    private Coroutine stopLiftingCoroutine;
    public int numberOfDumbbellsLifted;

    private O2AndRBCLevelsController o2AndRBCLevelsController;

    private void Start()
    {
        o2AndRBCLevelsController = FindObjectOfType<O2AndRBCLevelsController>();
        if (o2AndRBCLevelsController == null)
            Debug.LogError("O2AndRBCLevelsController not found! Make sure it's in the scene.");
    }

    public void StartLiftingDumbbell()
    {
        numberOfDumbbellsLifted += 1;
        if (GlobalVariables.Instance.isLifting) return; // don't re-run script if the player is already in a lifting state

        if (stopLiftingCoroutine != null) StopCoroutine(stopLiftingCoroutine); // stop stopLiftingCoroutine

        Debug.Log("[Arms] Start lifting");
        GlobalVariables.Instance.isLifting = true;
        GlobalVariables.Instance.canRun = false; // disable running

        GlobalVariables.Instance.arms.ToggleToLiftingOxygenDemand();

        startLiftingModalPanel.SetActive(true);
        stoppedLiftingModalPanel.SetActive(false);

        // Update Arms O2 Levels Modal UI
        o2AndRBCLevelsController.SetArmsActiveUI(true);
    }

    public void StopLiftingDumbbell()
    {
        GlobalVariables.Instance.isLifting = false;
        numberOfDumbbellsLifted -= 1;

        // Stop the existing coroutine if one is running
        if (stopLiftingCoroutine != null) StopCoroutine(stopLiftingCoroutine);

        // wait for a few seconds to see if the player has truly stopped lifing
        stopLiftingCoroutine = StartCoroutine(WaitForXSecondsThenCheckLiftingStatus(5f));
    }

    IEnumerator WaitForXSecondsThenCheckLiftingStatus(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (!GlobalVariables.Instance.isLifting && numberOfDumbbellsLifted == 0)
        {
            Debug.Log("[Arms] Stop lifting");
            GlobalVariables.Instance.canRun = true; // enable running
            GlobalVariables.Instance.arms.ToggleToDefaultOxygenDemand();

            startLiftingModalPanel.SetActive(false);
            stoppedLiftingModalPanel.SetActive(true);

            // Update Arms O2 Levels Modal UI
            o2AndRBCLevelsController.SetArmsActiveUI(false);

            yield return new WaitForSeconds(seconds);
            stoppedLiftingModalPanel.SetActive(false);
        }
    }
}
