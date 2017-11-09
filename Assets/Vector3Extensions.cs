using UnityEngine;

namespace DEnt
{
    /// <summary>
    /// Provides useful extensions to the Vector3 class.
    /// </summary>
    public static class Vector3Extensions
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        /// <summary>
        /// Rotates the vector around a pivot point by a number of degrees.
        /// </summary>
        /// <param name="point">The point to rotate.</param>
        /// <param name="pivot">The point to rotate around.</param>
        /// <param name="degrees">The number of degrees to rotate around the pivot.</param>
        /// <returns></returns>
        public static Vector3 RotateAroundPivot( this Vector3 point, Vector3 pivot, float degrees )
        {
            Vector3 direction = point - pivot;

            direction = Quaternion.Euler( 0f, degrees, 0f ) * direction;

            point = direction + pivot;

            return point;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}
