using System.Collections.Generic;

namespace DEnt.Language
{
    public sealed class UnitLanguageSettings : LanguageSettingsBase<UnitSettings>
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates/Loads all the unit language settings.
        /// </summary>
        public UnitLanguageSettings()
            : base( "Units" )
        {
            this.Units = new Dictionary<UnitType, LanguageSettingsDetails<UnitSettings>>();

            this.Units.Add( UnitType.Spearman, Create( "Spearman" ) );
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
        /// Holds the unit language settings for all unit types.
        /// </summary>
        public Dictionary<UnitType, LanguageSettingsDetails<UnitSettings>> Units { get; private set; }
        
        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}