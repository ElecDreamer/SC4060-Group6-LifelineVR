using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataEntities
{
    /// <summary>
    /// A body part. Note: This is a Skeleton class
    /// </summary>
    public class Arms : MonoBehaviour
    {
        public float oxygenLevel;
        public float oxygenRateofDemand;

        public static int MAXIMUM_OXYGEN_LEVEL = 100;
        public static int MIMUMUM_OXYGEN_LEVEL = 0;

        // Decrease by 1 (default) for every 5 seconds
        public readonly float DEFAULT_OXYGEN_RATE_OF_DEMAND = 1f;
        public readonly float LIFTING_OXYGEN_RATE_OF_DEMAND = 5f;
        private readonly float timeBetweenDecrement = 5f; // in seconds

        private RedBloodCellNPCsManager redBloodCellNPCsManager;

        void Start()
        {
            Debug.Log("Init Arms");
            oxygenLevel = 100f;
            oxygenRateofDemand = DEFAULT_OXYGEN_RATE_OF_DEMAND;

            // Find the Controllers in the scene
            redBloodCellNPCsManager = FindObjectOfType<RedBloodCellNPCsManager>();
            if (redBloodCellNPCsManager == null)
                Debug.LogError("RedBloodCellNPCsManager not found! Make sure it's in the scene.");
        }

        /**
         * Skeleton instance
         */
        private static Arms instance; // Singleton instance

        public static Arms Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new("ArmsManager");
                    instance = obj.AddComponent<Arms>();
                    DontDestroyOnLoad(obj); // Prevent destruction when loading a new scene
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject); // Keep this GameObject alive across scenes

                // Start the coroutine
                Debug.Log("Start Arms Oxygen Coroutine");
                StartCoroutine(DecreaseOxygenLevelRoutine());
                StartCoroutine(IncreaseOxygenLevelRoutine());
            }
            else
            {
                Destroy(gameObject); // Prevent duplicate managers
            }
        }
        /**
         * Coroutine
         */
        private IEnumerator DecreaseOxygenLevelRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenDecrement);
                DecrementOxygenLevelByAmount(amountToDecrease: oxygenRateofDemand);
            }
        }

        private IEnumerator IncreaseOxygenLevelRoutine()
        {
            while (true)
            {
                float timeBetweenIncrement = timeBetweenDecrement;
                yield return new WaitForSeconds(timeBetweenIncrement);
                IncrementOxygenLevelByAmount(amountToIncrease: redBloodCellNPCsManager.GetRateOfOxygenIncreaseBasedOnRBCLevel());
            }
        }

        /**
         * Methods
         */
        public void DecrementOxygenLevelByAmount(float amountToDecrease)
        {
            if (oxygenLevel > MIMUMUM_OXYGEN_LEVEL)
            {
                oxygenLevel -= amountToDecrease;
                oxygenLevel = Mathf.Max(oxygenLevel, MIMUMUM_OXYGEN_LEVEL);
                // Debug.Log($"Arms Oxygen level decreased to: {oxygenLevel}");
            }
        }

        public void IncrementOxygenLevelByAmount(float amountToIncrease)
        {
            oxygenLevel += amountToIncrease;
            oxygenLevel = Mathf.Min(oxygenLevel, MAXIMUM_OXYGEN_LEVEL);
            // Debug.Log($"Arms Oxygen level increased by {amountToIncrease} to {oxygenLevel}");
        }

        // Increases/decreases the oxygen rate of demand
        public void ToggleToLiftingOxygenDemand()
        {
            Debug.Log("[Arms] Lifting Oxygen Demand triggered");
            oxygenRateofDemand = LIFTING_OXYGEN_RATE_OF_DEMAND;
        }

        public void ToggleToDefaultOxygenDemand()
        {
            Debug.Log("[Arms] Default Oxygen Demand triggered");
            oxygenRateofDemand = DEFAULT_OXYGEN_RATE_OF_DEMAND;
        }
    }
}
