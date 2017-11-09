using UnityEngine;

namespace DEnt
{
    /// <summary>
    /// Provides useful shortcuts for getting Color objects.
    /// </summary>
    public static class Colour
    {
        /// <summary>
        /// Creates a Color object from integer based RGB values.
        /// </summary>
        /// <param name="r">The red channel</param>
        /// <param name="g">The green channel</param>
        /// <param name="b">The blue channel</param>
        /// <returns>A Color object</returns>
        public static Color RGB( int r, int g, int b )
        {
            return RGBA( r, g, b, 1 );
        }

        /// <summary>
        /// Creates a Color object from integer based RGB values.
        /// </summary>
        /// <param name="r">The red channel</param>
        /// <param name="g">The green channel</param>
        /// <param name="b">The blue channel</param>
        /// <param name="a">The alpha channel</param>
        /// <returns>A Color object</returns>
        public static Color RGBA( int r, int g, int b, float a )
        {
            float divisor = 255f;

            return new Color( r / divisor, g / divisor, b / divisor, a );
        }
    }
}