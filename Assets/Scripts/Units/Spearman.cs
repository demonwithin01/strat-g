
/// <summary>
/// Defines everything unique to the Spearman unit.
/// </summary>
public class Spearman : Unit
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Initialise the spearman unit.
    /// </summary>
    public Spearman()
        : base( UnitType.Spearman )
    {
        
    }

    /// <summary>
    /// Provides an area where all stats can be initialised as part of the construction process.
    /// The values provided to the stats here should be base values, not adjusted values for research etc.
    /// </summary>
    protected override void InitialiseStats()
    {
        base.Endurance = new Endurance( 30f );
        base.Health = new Health( 100f );
        base.Initiative = new Initiative( 10f );
        base.Morale = new Morale( 100f );
        base.Movement = new Movement( 1f );
        base.MeleeAttack = new MeleeAttack( 10f );
        base.MeleeDefence = new MeleeDefence( 10f );
        base.MeleePrecision = new MeleePrecision( 10f );
        base.MeleeEvasion = new MeleeEvasion( 10f );

        base.RangedDefence = new RangedDefence( 3f );
        base.RangedEvasion = new RangedEvasion( 10f );

        base.MagicDefence = new MagicDefence( 0f );
        base.MagicEvasion = new MagicEvasion( 0f );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
