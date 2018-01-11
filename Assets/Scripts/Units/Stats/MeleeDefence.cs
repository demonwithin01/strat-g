using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MeleeDefence : Stat
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public MeleeDefence( float initialValue )
        : base( initialValue )
    {
        base.ApplyLanguageSettings( LanguageSettings.Current.Stats.MeleeDefence.Settings );
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
