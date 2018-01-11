namespace DEnt.Language
{
    public sealed class StatLanguageSettings : LanguageSettingsBase<StatSettings>
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members
            
        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates/Loads all the stat settings.
        /// </summary>
        public StatLanguageSettings()
            : base( "Stats" )
        {
            this.Endurance = Create( "Endurance" );
            this.Health = Create( "Health" );
            this.Initiative = Create( "Initiative" );
            this.MagicAttack = Create( "Magic Attack" );
            this.MagicDefence = Create( "Magic Defence" );
            this.MagicEvasion = Create( "Magic Evasion" );
            this.MagicPrecision = Create( "Magic Precision" );
            this.MeleeAttack = Create( "Melee Attack" );
            this.MeleeDefence = Create( "Melee Defence" );
            this.MeleeEvasion = Create( "Melee Evasion" );
            this.MeleePrecision = Create( "Melee Precision" );
            this.Morale = Create( "Morale" );
            this.Movement = Create( "Movement" );
            this.RangedAttack = Create( "Ranged Attack" );
            this.RangedDefence = Create( "Ranged Defence" );
            this.RangedEvasion = Create( "Ranged Evasion" );
            this.RangedPrecision = Create( "Ranged Precision" );
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
        /// Gets the stat settings for Endurance.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> Endurance { get; private set; }

        /// <summary>
        /// Gets the stat settings for Health.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> Health { get; private set; }

        /// <summary>
        /// Gets the stat settings for Initiative.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> Initiative { get; private set; }

        /// <summary>
        /// Gets the stat settings for Magic Attack.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MagicAttack { get; private set; }

        /// <summary>
        /// Gets the stat settings for Magic Defence.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MagicDefence { get; private set; }

        /// <summary>
        /// Gets the stat settings for Magic Evasion.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MagicEvasion { get; private set; }

        /// <summary>
        /// Gets the stat settings for Magic Precision.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MagicPrecision { get; private set; }

        /// <summary>
        /// Gets the stat settings for Melee Attack.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MeleeAttack { get; private set; }

        /// <summary>
        /// Gets the stat settings for Melee Defence.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MeleeDefence { get; private set; }

        /// <summary>
        /// Gets the stat settings for Melee Evasion.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MeleeEvasion { get; private set; }

        /// <summary>
        /// Gets the stat settings for Melee Precision.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> MeleePrecision { get; private set; }

        /// <summary>
        /// Gets the stat settings for Morale.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> Morale { get; private set; }

        /// <summary>
        /// Gets the stat settings for Movement.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> Movement { get; private set; }

        /// <summary>
        /// Gets the stat settings for Ranged Attack.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> RangedAttack { get; private set; }

        /// <summary>
        /// Gets the stat settings for Ranged Defence.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> RangedDefence { get; private set; }

        /// <summary>
        /// Gets the stat settings for Ranged Evasion.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> RangedEvasion { get; private set; }

        /// <summary>
        /// Gets the stat settings for Ranged Precision.
        /// </summary>
        public LanguageSettingsDetails<StatSettings> RangedPrecision { get; private set; }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}