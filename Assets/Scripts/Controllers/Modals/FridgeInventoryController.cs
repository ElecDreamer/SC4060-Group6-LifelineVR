using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FridgeInventoryController : MonoBehaviour
{
    public GameObject FridgeInventoryPanel;

    public TMP_Text SuperFreshBloodPackText;
    public TMP_Text FreshBloodPackText;
    public TMP_Text SlightlyStaleBloodPackText;
    public TMP_Text StaleBloodPackText;
    public TMP_Text SpoiltBloodPackText;

    private int numOfSuperFreshBloodPack;
    private int numOfFreshBloodPack;
    private int numOfSlightlyStaleBloodPack;
    private int numOfStaleBloodPack;
    private int numOfSpoiltBloodPack;

    /**
     * Open Fridge
     */
    public void OpenFridgeInventory()
    {
        if (FridgeInventoryPanel.activeSelf == true)
        {
            Debug.Log("Fridge Inventory already opened");
            return;
        }

        Debug.Log("Fridge Inventory Opened");
        Time.timeScale = 0; // Pause game time

        // Get Blood Pack counts
        GetBloodPackCounts();

        // Update Display Texts
        UpdateDisplayTexts();

        // Show Fridge Inventory
        FridgeInventoryPanel.SetActive(true);
    }

    /**
     * Close Fridge
     */
    public void CloseFridgeInventory()
    {
        Debug.Log("Fridge Inventory Closed");
        FridgeInventoryPanel.SetActive(false);
        Time.timeScale = 1; // Resume game time
    }

    /**
     * Consume Blood Packs
     */
    private void RemoveBloodPack(DataEntities.BloodPack.BloodPackState selectedState)
    {
        // Find the first blood pack that matches the selected category
        List<DataEntities.BloodPack> bloodPacksOfSelectedState = GlobalVariables.bloodPacks.FindAll(bp => bp.state == selectedState);

        // Sort the blood packs by time (Least time first)
        bloodPacksOfSelectedState.Sort((a, b) => a.timeLeft.CompareTo(b.timeLeft));

        DataEntities.BloodPack bloodPackToRemove = bloodPacksOfSelectedState[0];

        GlobalVariables.bloodPacks.Remove(bloodPackToRemove);

        // Update Counts and Display Text
        GetBloodPackCounts();
        UpdateDisplayTexts();
    }

    private void UpdateRedBloodCellLevels()
    {
        // TODO: Update Blood Cell Levels
    }

    public void ConsumeSuperFreshBloodPack()
    {
        if (numOfSuperFreshBloodPack <= 0)
        {
            Debug.Log("No Super Fresh Blood Pack!");
            return;
        }
        Debug.Log("Consume 1x Super Fresh Blood Pack");
        RemoveBloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh);
        UpdateRedBloodCellLevels();
    }

    public void ConsumeFreshBloodPack()
    {
        if (numOfFreshBloodPack <= 0)
        {
            Debug.Log("No Fresh Blood Pack!");
            return;
        }
        Debug.Log("Consume 1x Fresh Blood Pack");
        RemoveBloodPack(DataEntities.BloodPack.BloodPackState.Fresh);
        UpdateRedBloodCellLevels();
    }

    public void ConsumeSlightlyStaleBloodPack()
    {
        if (numOfSlightlyStaleBloodPack <= 0)
        {
            Debug.Log("No Slightly Stale Blood Pack!");
            return;
        }
        Debug.Log("Consume 1x Slightly Stale Blood Pack");
        RemoveBloodPack(DataEntities.BloodPack.BloodPackState.SlightlyStale);
        UpdateRedBloodCellLevels();
    }

    public void ConsumeStaleBloodPack()
    {
        if (numOfStaleBloodPack <= 0)
        {
            Debug.Log("No Stale Blood Pack!");
            return;
        }
        Debug.Log("Consume 1x Stale Blood Pack");
        RemoveBloodPack(DataEntities.BloodPack.BloodPackState.Stale);
        UpdateRedBloodCellLevels();
    }

    public void DiscardAllSpoiltBloodPacks()
    {
        if (numOfSpoiltBloodPack <= 0)
        {
            Debug.Log("No Spoilt Blood Pack to discard!");
            return;
        }
        int removedCount = GlobalVariables.bloodPacks.RemoveAll(bp => bp.state == DataEntities.BloodPack.BloodPackState.Spoilt);
        Debug.Log($"Discard {removedCount} Spoilt Blood Packs");

        // Update Counts and Display Text
        GetBloodPackCounts();
        UpdateDisplayTexts();
    }

    /**
     * Texts
     */
    public void UpdateDisplayTexts()
    {
        SuperFreshBloodPackText.SetText("Super Fresh Blood Pack x" + numOfSuperFreshBloodPack);
        FreshBloodPackText.SetText("Fresh Blood Pack x" + numOfFreshBloodPack);
        SlightlyStaleBloodPackText.SetText("Slightly Stale Blood Pack x" + numOfSlightlyStaleBloodPack);
        StaleBloodPackText.SetText("Stale Blood Pack x" + numOfStaleBloodPack);
        SpoiltBloodPackText.SetText("Spoilt Blood Pack x" + numOfSpoiltBloodPack);
    }

    /**
     * Helper Methods
     */
    private void GetBloodPackCounts()
    {
        Dictionary<DataEntities.BloodPack.BloodPackState, int> counts = DataEntities.BloodPack.CalculateNumberOfBloodPacksOfEachState(GlobalVariables.bloodPacks);
        numOfSuperFreshBloodPack = counts[DataEntities.BloodPack.BloodPackState.SuperFresh];
        numOfFreshBloodPack = counts[DataEntities.BloodPack.BloodPackState.Fresh];
        numOfSlightlyStaleBloodPack = counts[DataEntities.BloodPack.BloodPackState.SlightlyStale];
        numOfStaleBloodPack = counts[DataEntities.BloodPack.BloodPackState.Stale];
        numOfSpoiltBloodPack = counts[DataEntities.BloodPack.BloodPackState.Spoilt];
    }
}
