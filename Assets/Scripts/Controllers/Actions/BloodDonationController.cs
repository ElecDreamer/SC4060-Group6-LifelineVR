using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDonationController : MonoBehaviour
{
    private FridgeInventoryController fridgeInventoryController;

    public float BLOOD_DONATION_PROBABILITY = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        fridgeInventoryController = FindObjectOfType<FridgeInventoryController>();
        if (fridgeInventoryController == null)
            Debug.LogError("FridgeInventoryController not found! Make sure it's in the scene.");
    }

    public void AskForBloodDonation()
    {
        float randomValue = (float) new System.Random().NextDouble();
        if (randomValue <= BLOOD_DONATION_PROBABILITY) // success
        {
            // TODO: Success
            fridgeInventoryController.AddBloodPack();
        }
        else // fail
        {
             // TODO: Fail
        }

    }
}
