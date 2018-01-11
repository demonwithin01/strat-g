using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Initiative : Stat
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public Initiative( float initialValue )
        : base( initialValue )
    {
        base.ApplyLanguageSettings( LanguageSettings.Current.Stats.Initiative.Settings );
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
