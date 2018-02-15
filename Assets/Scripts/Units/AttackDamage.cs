
/// <summary>
/// Responsible for holding values for an attack before the attack is completed.
/// </summary>
public class AttackDamage
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates a new attack damage object based off a melee attack.
    /// </summary>
    /// <param name="meleeAttack">The attack stat, must have a value.</param>
    /// <param name="meleePrecision">The precision stat, must have a value.</param>
    public AttackDamage( MeleeAttack meleeAttack, MeleePrecision meleePrecision )
    {
        this.SetAttack( AttackType.Melee, meleeAttack, meleePrecision );
    }

    /// <summary>
    /// Creates a new attack damage object based off a ranged attack.
    /// </summary>
    /// <param name="rangedAttack">The attack stat, must have a value.</param>
    /// <param name="rangedPrecision">The precision stat, must have a value.</param>
    public AttackDamage( RangedAttack rangedAttack, RangedPrecision rangedPrecision )
    {
        this.SetAttack( AttackType.Ranged, rangedAttack, rangedPrecision );
    }

    /// <summary>
    /// Creates a new attack damage object based off a magic attack.
    /// </summary>
    /// <param name="magicAttack">The attack stat, must have a value.</param>
    /// <param name="magicPrecision">The precision stat, must have a value.</param>
    public AttackDamage( MagicAttack magicAttack, MagicAttack magicPrecision )
    {
        this.SetAttack( AttackType.Magic, magicAttack, magicPrecision );
    }

    /// <summary>
    /// Sets the attack information.
    /// </summary>
    /// <param name="type">The type of attack.</param>
    /// <param name="attack">The attack stat, must have a value.</param>
    /// <param name="precision">The precision stat, must have a value.</param>
    private void SetAttack( AttackType type, Stat attack, Stat precision )
    {
        this.Type = type;
        this.Damage = attack.NextValue();
        this.Precision = precision.NextValue();
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    public bool MissCheck( Stat evasionStat )
    {
        float evasion = ( evasionStat == null ? 0f : evasionStat.NextValue() );

        return ( this.Precision < evasion );
    }

    public float FinalDamage( Stat defenceStat )
    {
        float defence = ( defenceStat == null ? 0f : defenceStat.NextValue() );

        return ( this.Damage - defence );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the damage value for the attack.
    /// </summary>
    public float Damage { get; private set; }

    /// <summary>
    /// Gets the precision value for the attack.
    /// </summary>
    public float Precision { get; private set; }

    /// <summary>
    /// Gets the attack type.
    /// </summary>
    public AttackType Type { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
