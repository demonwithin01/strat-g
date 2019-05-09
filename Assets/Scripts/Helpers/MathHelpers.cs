using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class MathHelpers
    {

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    public static float Round( float value )
    {
        return RoundTo( value, 0 );
    }

    public static float RoundTo( float value, int decimalPlaces )
    {
        return (float)Math.Round( value, decimalPlaces );
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
