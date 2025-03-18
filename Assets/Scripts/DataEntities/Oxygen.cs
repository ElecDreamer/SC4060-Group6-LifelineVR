using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataEntities
{
    public class Oxygen
    {
        public float amount;

        private readonly static float DEFAULT_AMOUNT = 1;

        public Oxygen()
        {
            amount = DEFAULT_AMOUNT;
        }
    }
}
