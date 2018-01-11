using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class RangedEvasion : Stat
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public RangedEvasion( float initialValue )
        : base( initialValue )
    {
        base.ApplyLanguageSettings( LanguageSettings.Current.Stats.RangedEvasion.Settings );
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

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
