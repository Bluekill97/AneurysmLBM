using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace HemeSimulation {
    public static class Vector3Extensions {

        /// <summary>
        /// Parses a string to a Vector.
        /// </summary>
        /// <param name="str"> String to parse </param>
        /// <returns> Parsed Vector, zero Vector if parsing failed </returns>
        public static Vector3 Vec3FromString(string str) {
            string[] temp = str.Substring(1, str.Length - 2).Split(',');
            float x, y, z;

            try {
                x = float.Parse(temp[0], CultureInfo.InvariantCulture);
                y = float.Parse(temp[1], CultureInfo.InvariantCulture);
                z = float.Parse(temp[2], CultureInfo.InvariantCulture);
            } catch(System.Exception e) {
                Utilities.ErrorLog.LogError(e);

                return Vector3.zero;
            }
            Vector3 rValue = new Vector3(x, y, z);
            return rValue;
        }

        /// <summary>
        /// Converts a Vector3 to string without rounding the numbers
        /// </summary>
        /// <param name="vec"> Vector to convert </param>
        /// <returns> Vector as string without rounded numbers </returns>
        public static string Vec3ToString(Vector3 vec) {
            return vec.ToString("#########0.0#########");
        }

        /// <summary>
        /// Converts a Vector3 to string without rounding the numbers
        /// </summary>
        /// <param name="vec"> Vector to convert </param>
        /// <returns> Vector as string without rounded numbers </returns>
        public static string Vec3ToString(Vector3Double vec) {
            return vec.ToString();
        }
    }
}