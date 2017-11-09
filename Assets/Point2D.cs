using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DEnt
{
    /// <summary>
    /// A '2D' point object that is similar to Vector2, however uses integer values instead of floating point values.
    /// </summary>
    public struct Point2D
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates a new instance of the Point2D object with the provided coordinates.
        /// </summary>
        /// <param name="x">The X coordinate of the point.</param>
        /// <param name="y">The Y coordinate of the point.</param>
        public Point2D( int x, int y )
        {
            X = x;
            Y = y;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        /// <summary>
        /// Gets the X coordinate of the point.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Gets the Y coordinate of the point.
        /// </summary>
        public int Y { get; private set; }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}
