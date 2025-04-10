using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataEntities
{
    /// <summary>
    /// Singleton
    /// </summary>
    public class RedBloodCellLevel: MonoBehaviour
    {
        public enum LevelStatus
        {
            Safe,
            Warn,
            Danger
        }

        public static int MAXIMUM_LEVEL = 100;
        public static int MIMUMUM_LEVEL = 0;
        public float level = 100f; // Maximum of 100

        // Decrease by 0.5 for every 5 seconds
        public static float rateOfDecrement = 0.5f; 
        private readonly float timeBetweenDecrement = 5f; // in seconds

        // Spike decrease by 5-10 for every 14mins
        public static float rateOfDecrementSpikeMin = 15;
        public static float rateOfDecrementSpikeMax = 30;
        private readonly float timeBetweenDecrementSpike = 840f; // 14mins (in seconds)

        private static RedBloodCellLevel instance; // Singleton instance

        public static RedBloodCellLevel Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new("RedBloodCellLevelManager");
                    instance = obj.AddComponent<RedBloodCellLevel>();
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
                Debug.Log("Start Red Blood Cell Level Coroutine");
                StartCoroutine(DecreaseRedBloodCellLevelRoutine());
                StartCoroutine(SpikeDecreaseRedBloodCellLevelRoutine());
            }
            else
            {
                Destroy(gameObject); // Prevent duplicate managers
            }
        }

        private IEnumerator DecreaseRedBloodCellLevelRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenDecrement);
                DecrementLevel(amountToDecrease: rateOfDecrement);
            }
        }

        private IEnumerator SpikeDecreaseRedBloodCellLevelRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(timeBetweenDecrementSpike);
                DecrementLevel(amountToDecrease: Random.Range(rateOfDecrementSpikeMin, rateOfDecrementSpikeMax));
            }
        }

        public void DecrementLevel(float amountToDecrease)
        {
            if (level > MIMUMUM_LEVEL)
            {
                level -= amountToDecrease;
                level = Mathf.Max(level, MIMUMUM_LEVEL);
                // Debug.Log($"Red Blood Cell Level decreased to: {level}");
            }
        }

        public void IncrementLevelByAmount(float amountToIncrease)
        {
            level += amountToIncrease;
            level = Mathf.Min(level, MAXIMUM_LEVEL);
            Debug.Log($"Red Blood Cell Level increased by {amountToIncrease} to {level}");
        }
    }
}
