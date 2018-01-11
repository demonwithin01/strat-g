using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MagicDefence : Stat
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public MagicDefence( float initialValue )
        : base( initialValue )
    {
        base.ApplyLanguageSettings( LanguageSettings.Current.Stats.MagicDefence.Settings );
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
