using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDonationController : MonoBehaviour
{
    private FridgeInventoryController fridgeInventoryController;

    public float BLOOD_DONATION_PROBABILITY_NPC_1 = 0.2f;
    public float BLOOD_DONATION_PROBABILITY_NPC_2 = 0.4f;
    public float BLOOD_DONATION_PROBABILITY_NPC_3 = 0.5f;
    public float BLOOD_DONATION_PROBABILITY_NPC_4 = 0.6f;
    public float BLOOD_DONATION_PROBABILITY_NPC_5 = 0.8f;

    public GameObject InfoPanel_NPC_1;
    public GameObject SuccessPanel_NPC_1;
    public GameObject FailPanel_NPC_1;

    public GameObject InfoPanel_NPC_2;
    public GameObject SuccessPanel_NPC_2;
    public GameObject FailPanel_NPC_2;

    public GameObject InfoPanel_NPC_3;
    public GameObject SuccessPanel_NPC_3;
    public GameObject FailPanel_NPC_3;

    public GameObject InfoPanel_NPC_4;
    public GameObject SuccessPanel_NPC_4;
    public GameObject FailPanel_NPC_4;

    public GameObject InfoPanel_NPC_5;
    public GameObject SuccessPanel_NPC_5;
    public GameObject FailPanel_NPC_5;

    private Coroutine Coroutine_NPC_1;
    private Coroutine Coroutine_NPC_2;
    private Coroutine Coroutine_NPC_3;
    private Coroutine Coroutine_NPC_4;
    private Coroutine Coroutine_NPC_5;

    // Start is called before the first frame update
    void Start()
    {
        fridgeInventoryController = FindObjectOfType<FridgeInventoryController>();
        if (fridgeInventoryController == null)
            Debug.LogError("FridgeInventoryController not found! Make sure it's in the scene.");
    }

    public void AskForBloodDonation(int NPC_Number)
    {
        Debug.Log("Ask for Blood Donation from NPC #" + NPC_Number);

        float bloodDonationProbability = NPC_Number switch
        {
            1 => BLOOD_DONATION_PROBABILITY_NPC_1,
            2 => BLOOD_DONATION_PROBABILITY_NPC_2,
            3 => BLOOD_DONATION_PROBABILITY_NPC_3,
            4 => BLOOD_DONATION_PROBABILITY_NPC_4,
            5 => BLOOD_DONATION_PROBABILITY_NPC_5,
            _ => 0.5f,
        };
        GameObject infoPanel = NPC_Number switch
        {
            1 => InfoPanel_NPC_1,
            2 => InfoPanel_NPC_2,
            3 => InfoPanel_NPC_3,
            4 => InfoPanel_NPC_4,
            5 => InfoPanel_NPC_5,
            _ => null,
        };
        GameObject successPanel = NPC_Number switch
        {
            1 => SuccessPanel_NPC_1,
            2 => SuccessPanel_NPC_2,
            3 => SuccessPanel_NPC_3,
            4 => SuccessPanel_NPC_4,
            5 => SuccessPanel_NPC_5,
            _ => null,
        };
        GameObject failPanel = NPC_Number switch
        {
            1 => FailPanel_NPC_1,
            2 => FailPanel_NPC_2,
            3 => FailPanel_NPC_3,
            4 => FailPanel_NPC_4,
            5 => FailPanel_NPC_5,
            _ => null,
        };
        Coroutine npcCoroutine = NPC_Number switch
        {
            1 => Coroutine_NPC_1,
            2 => Coroutine_NPC_2,
            3 => Coroutine_NPC_3,
            4 => Coroutine_NPC_4,
            5 => Coroutine_NPC_5,
            _ => null,
        };

        if (infoPanel == null || successPanel == null || failPanel == null)
        {
            Debug.LogError("Invalid NPC Number: " + NPC_Number);
            return;
        }

        if (npcCoroutine != null)
        {
            // Make sure that the NPC selected has fully reseted (finished its cooldown period)
            Debug.LogWarning($"NPC #{NPC_Number} is still reseting. Please try again later");
            return;
        }

        float randomValue = (float) new System.Random().NextDouble();
        if (randomValue <= bloodDonationProbability) // success
        {
            fridgeInventoryController.AddBloodPack();

            infoPanel.SetActive(false);
            successPanel.SetActive(true);

            Coroutine coroutine = StartCoroutine(ResetNPCCoroutine(infoPanel, successPanel, NPC_Number));
            switch (NPC_Number)
            {
                case 1: Coroutine_NPC_1 = coroutine; break;
                case 2: Coroutine_NPC_2 = coroutine; break;
                case 3: Coroutine_NPC_3 = coroutine; break;
                case 4: Coroutine_NPC_4 = coroutine; break;
                case 5: Coroutine_NPC_5 = coroutine; break;
            }
        }
        else // fail
        {
            infoPanel.SetActive(false);
            failPanel.SetActive(true);

            Coroutine coroutine = StartCoroutine(ResetNPCCoroutine(infoPanel, failPanel, NPC_Number));
            switch (NPC_Number)
            {
                case 1: Coroutine_NPC_1 = coroutine; break;
                case 2: Coroutine_NPC_2 = coroutine; break;
                case 3: Coroutine_NPC_3 = coroutine; break;
                case 4: Coroutine_NPC_4 = coroutine; break;
                case 5: Coroutine_NPC_5 = coroutine; break;
            }
        }
    }

    /**
     * Coroutines
     */
    IEnumerator ResetNPCCoroutine(GameObject panelToDisplay, GameObject panelToHide, int npcNumber)
    {
        yield return new WaitForSeconds(3);

        panelToDisplay.SetActive(true);
        panelToHide.SetActive(false);

        // Clear the coroutine reference
        switch (npcNumber)
        {
            case 1: Coroutine_NPC_1 = null; break;
            case 2: Coroutine_NPC_2 = null; break;
            case 3: Coroutine_NPC_3 = null; break;
            case 4: Coroutine_NPC_4 = null; break;
            case 5: Coroutine_NPC_5 = null; break;
        }
    }
}
