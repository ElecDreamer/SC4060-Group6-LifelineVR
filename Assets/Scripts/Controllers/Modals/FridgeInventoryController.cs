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

    private NotificationsController notificationController;
    private readonly float SUCCESS_NOTIFICATION_DURATION = 1f;
    private readonly float Error_NOTIFICATION_DURATION = 1f;

    private void Start()
    {
        // Find the NotificationsController in the scene
        notificationController = FindObjectOfType<NotificationsController>();

        if (notificationController == null)
        {
            Debug.LogError("NotificationsController not found! Make sure it's in the scene.");
        }
    }

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
        //Time.timeScale = 0; // Pause game time

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
        //Time.timeScale = 1; // Resume game time
    }

    /**
     * Update Read Blood Cell Levels
     */
    private void UpdateRedBloodCellLevels()
    {
        GlobalVariables.Instance.redBloodCellLevel.IncrementLevelByAmount(amountToIncrease: DataEntities.BloodPack.AMOUNT_OF_RED_BLOOD_CELLS);
    }

    /**
     * Consume Blood Packs
     */
    private bool CheckIfBloodPackAvailable(DataEntities.BloodPack.BloodPackState selectedState)
    {
        bool available = false;
        switch (selectedState)
        {
            case DataEntities.BloodPack.BloodPackState.SuperFresh:
                available = numOfSuperFreshBloodPack > 0;
                break;
            case DataEntities.BloodPack.BloodPackState.Fresh:
                available = numOfFreshBloodPack > 0;
                break;
            case DataEntities.BloodPack.BloodPackState.SlightlyStale:
                available = numOfSlightlyStaleBloodPack > 0;
                break;
            case DataEntities.BloodPack.BloodPackState.Stale:
                available = numOfStaleBloodPack > 0;
                break;
            case DataEntities.BloodPack.BloodPackState.Spoilt:
                available = numOfSpoiltBloodPack > 0;
                break;

        }
        if (!available)
        {
            Debug.Log($"No {selectedState} Blood Pack!");
            notificationController.DisplayNotification(
                Enums.NotificationType.Error,
                $"No {selectedState} Blood Pack left!",
                Error_NOTIFICATION_DURATION);
            return false;
        }

        return true;
    }

    private void ConsumeBloodPack(DataEntities.BloodPack.BloodPackState selectedState)
    {
        // Find the first blood pack that matches the selected category
        List<DataEntities.BloodPack> bloodPacksOfSelectedState = GlobalVariables.Instance.bloodPacks.FindAll(bp => bp.state == selectedState);

        // Sort the blood packs by time (Least time first)
        bloodPacksOfSelectedState.Sort((a, b) => a.timeLeft.CompareTo(b.timeLeft));

        DataEntities.BloodPack bloodPackToRemove = bloodPacksOfSelectedState[0];

        GlobalVariables.Instance.bloodPacks.Remove(bloodPackToRemove);

        // Update Counts and Display Text
        GetBloodPackCounts();
        UpdateDisplayTexts();

        // Update Red Bood Cell Levels
        UpdateRedBloodCellLevels();

        // Display Success Notification
        Debug.Log($"Consumed 1x {selectedState} Blood Pack");
        notificationController.DisplayNotification(
            Enums.NotificationType.Success,
            $"1x {selectedState} Blood Pack consumed. {DataEntities.BloodPack.AMOUNT_OF_RED_BLOOD_CELLS} Red Blood Cells are regained!",
            SUCCESS_NOTIFICATION_DURATION);

    }

    public void ConsumeSuperFreshBloodPack()
    {
        if (CheckIfBloodPackAvailable(DataEntities.BloodPack.BloodPackState.SuperFresh))
            ConsumeBloodPack(DataEntities.BloodPack.BloodPackState.SuperFresh);
    }

    public void ConsumeFreshBloodPack()
    {
        if (CheckIfBloodPackAvailable(DataEntities.BloodPack.BloodPackState.Fresh))
            ConsumeBloodPack(DataEntities.BloodPack.BloodPackState.Fresh);
    }

    public void ConsumeSlightlyStaleBloodPack()
    {
        if (CheckIfBloodPackAvailable(DataEntities.BloodPack.BloodPackState.SlightlyStale))
            ConsumeBloodPack(DataEntities.BloodPack.BloodPackState.SlightlyStale);
    }

    public void ConsumeStaleBloodPack()
    {
        if (CheckIfBloodPackAvailable(DataEntities.BloodPack.BloodPackState.Stale))
            ConsumeBloodPack(DataEntities.BloodPack.BloodPackState.Stale);
    }

    public void DiscardAllSpoiltBloodPacks()
    {
        if (CheckIfBloodPackAvailable(DataEntities.BloodPack.BloodPackState.Spoilt))
        {
            int removedCount = GlobalVariables.Instance.bloodPacks.RemoveAll(bp => bp.state == DataEntities.BloodPack.BloodPackState.Spoilt);

            // Update Counts and Display Text
            GetBloodPackCounts();
            UpdateDisplayTexts();

            // Notification
            Debug.Log($"Discarded {removedCount} Spoilt Blood Packs");
            notificationController.DisplayNotification(
                Enums.NotificationType.Success,
                $"{removedCount}x Spoilt Blood Pack discarded",
                SUCCESS_NOTIFICATION_DURATION);
        }
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
        Dictionary<DataEntities.BloodPack.BloodPackState, int> counts = DataEntities.BloodPack.CalculateNumberOfBloodPacksOfEachState(GlobalVariables.Instance.bloodPacks);
        numOfSuperFreshBloodPack = counts[DataEntities.BloodPack.BloodPackState.SuperFresh];
        numOfFreshBloodPack = counts[DataEntities.BloodPack.BloodPackState.Fresh];
        numOfSlightlyStaleBloodPack = counts[DataEntities.BloodPack.BloodPackState.SlightlyStale];
        numOfStaleBloodPack = counts[DataEntities.BloodPack.BloodPackState.Stale];
        numOfSpoiltBloodPack = counts[DataEntities.BloodPack.BloodPackState.Spoilt];
    }
}
