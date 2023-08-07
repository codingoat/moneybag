using System;
using UnityEngine;

namespace Moneybag.Misc
{
    public static class Layers
    {
        public static LayerMask CreateMask(params int[] layers)
        {
            int bitmask = 0;
            foreach (int layer in layers) bitmask |= 1 << layer;
            return bitmask;
        }

        public static bool Contains(this LayerMask mask, int layer) => (mask & (1 << layer)) == (1 << layer);
        
        /// <summary>
        /// Method to convert an integer to a string containing the number in binary. A negative 
        /// number will be formatted as a 32-character binary number in two's compliment.
        /// </summary>
        /// <param name="value">input number to be converted</param>
        /// <param name="minimumDigits">if binary number contains fewer characters leading zeros are added</param>
        /// <returns>string as described above</returns>
        public static string ToBinary(int value, int minimumDigits)
        {
            return Convert.ToString(value, 2).PadLeft(minimumDigits, '0');
        }

        private static int? hero;
        public static int Hero => hero ?? (hero = LayerMask.NameToLayer("Hero")).Value;
        
        private static int? ground;
        public static int Ground => ground ?? (ground = LayerMask.NameToLayer("Ground")).Value;
    }
}