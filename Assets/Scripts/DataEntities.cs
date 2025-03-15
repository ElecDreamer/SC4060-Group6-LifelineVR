using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataEntities : MonoBehaviour
{
    public class BloodPack
    {
        public BloodPackState state;

        public int timeLeft; // In seconds (whole number)
        private float rawTimeLeft; // Raw Time Left
        private readonly int MAX_TIME_LEFT = 252; // In seconds

        /**
         * Constructors
         */
        public BloodPack()
        {
            timeLeft = MAX_TIME_LEFT; // In seconds (4.2 mins)
            rawTimeLeft = (float) MAX_TIME_LEFT;
            state = BloodPackState.SuperFresh;
        }

        public BloodPack(int timeLeft)
        {
            this.timeLeft = timeLeft;
            rawTimeLeft = (float) timeLeft;
            state = TimeLeftToState(timeLeft);
        }

        public BloodPack(BloodPackState bloodPackState)
        {
            timeLeft = StateToTimeLeft(bloodPackState);
            rawTimeLeft = (float) timeLeft;
            state = bloodPackState;
        }

        public void UpdateTimeAndState()
        {
            if (rawTimeLeft > 0)
            {
                rawTimeLeft -= Time.deltaTime;

                // Update timeLeft and state
                timeLeft = (int) rawTimeLeft;
                state = TimeLeftToState(timeLeft);
            }
        }
        private BloodPackState TimeLeftToState(int timeLeft)
        {
            if (timeLeft >= 180) return BloodPackState.SuperFresh;
            else if (timeLeft >= 120) return BloodPackState.Fresh;
            else if (timeLeft >= 60) return BloodPackState.SlightlyStale;
            else if (timeLeft > 0) return BloodPackState.Stale;
            else return BloodPackState.Spoilt;
        }
        private int StateToTimeLeft(BloodPackState bloodPackState)
        {
            switch (bloodPackState)
            {
                case BloodPackState.SuperFresh:
                    return MAX_TIME_LEFT;
                case BloodPackState.Fresh:
                    return 179;
                case BloodPackState.SlightlyStale:
                    return 119;
                case BloodPackState.Stale:
                    return 59;
                case BloodPackState.Spoilt:
                    return 0;
                default:
                    return 0;
            }
        }

        /**
         * Instance Methods
         */
        public bool IsSpoilt()
        {
            return timeLeft <= 0 && state == BloodPackState.Spoilt;
        }
        
        public double GetTimeLeftInMinutes()
        {
            return Math.Round(rawTimeLeft/60, 2);
        }

        public double GetTimeLeftInSeconds()
        {
            return timeLeft;
        }

        /**
         * Static Methods
         */
        public static int NumberOfBloodPacksOfCertainState(List<BloodPack> bloodPacks, BloodPackState bloodPackState)
        {
            int count = 0;
            foreach (BloodPack bloodPack in bloodPacks)
            {
                if (bloodPack.state == bloodPackState)
                {
                    count += 1;
                }
            }
            return count;
        }
        public static Dictionary<BloodPackState, int> CalculateNumberOfBloodPacksOfEachState(List<BloodPack> bloodPacks)
        {
            Dictionary<BloodPackState, int> counts = new Dictionary<BloodPackState, int>();
            int countSuperFresh = 0;
            int countFresh = 0;
            int countSlightlyStale= 0;
            int countStale= 0;
            int countSpoilt= 0;

            foreach (BloodPack bloodPack in bloodPacks)
            {
                if (bloodPack.state == BloodPackState.SuperFresh) countSuperFresh += 1;
                else if (bloodPack.state == BloodPackState.Fresh) countFresh += 1;
                else if (bloodPack.state == BloodPackState.SlightlyStale) countSlightlyStale += 1;
                else if (bloodPack.state == BloodPackState.Stale) countStale += 1;
                else if (bloodPack.state == BloodPackState.Spoilt) countSpoilt += 1;
            }

            counts.Add(BloodPackState.SuperFresh, countSuperFresh);
            counts.Add(BloodPackState.Fresh, countFresh);
            counts.Add(BloodPackState.SlightlyStale, countSlightlyStale);
            counts.Add(BloodPackState.Stale, countStale);
            counts.Add(BloodPackState.Spoilt, countSpoilt);

            return counts;
        }

        /**
         * Enum
         */
        public enum BloodPackState
        {
            SuperFresh,     // 3mins to 4.2mins
            Fresh,          // 2mins to 3mins
            SlightlyStale,  // 1min to 2mins
            Stale,          // Less than 1min
            Spoilt          // 0min
        }
    }
}
