using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataEntities
{
    public class OxygenPlayerStorage : MonoBehaviour
    {
        public readonly static int MAXIMUM = 100;
        public readonly static int MINIMUM = 0;
        public int numberOfOxygen;

        /**
         * Skeleton instance
         */
        private static OxygenPlayerStorage instance; // Singleton instance

        public static OxygenPlayerStorage Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new("OxygenPlayerStorageManager");
                    instance = obj.AddComponent<OxygenPlayerStorage>();
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
            }
            else
            {
                Destroy(gameObject); // Prevent duplicate managers
            }
        }

        public void IncrementNumberOfOxygenInStorage(int amountToIncrease)
        {
            numberOfOxygen += amountToIncrease;
            numberOfOxygen = Mathf.Min(numberOfOxygen, MAXIMUM);
            Debug.Log($"Number of Oxygen in storage increased by {amountToIncrease} to {numberOfOxygen}");
        }


        public void DecrementNumberOfOxygenInStorage(int amountToDecrease)
        {
            numberOfOxygen -= amountToDecrease;
            numberOfOxygen = Mathf.Max(numberOfOxygen, MINIMUM);
            Debug.Log($"Number of Oxygen in storage decreased by {amountToDecrease} to {numberOfOxygen}");
        }
    }
}
