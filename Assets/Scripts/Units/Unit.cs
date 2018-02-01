using System;
using System.Collections.Generic;
using DEnt.Language;
using UnityEngine;

/// <summary>
/// The base unit object that will define all common functionality between 
/// the battle map and the strategy map.
/// </summary>
public abstract class Unit : MonoBehaviour
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds the type of this unit.
    /// </summary>
    private readonly UnitType _unitType;
    
    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Events

    /// <summary>
    /// Raised whenever the unit is marked as destroyed.
    /// </summary>
    public event Action<Unit> Destroyed;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Initialises the unit base.
    /// </summary>
    /// <param name="unitType">The type of unit being created.</param>
    public Unit( UnitType unitType )
    {
        this._unitType = unitType;

        this.ApplyLanguageSettings( LanguageSettings.Current.Units.Units[ unitType ].Settings );

        this.InitialiseStats();
    }

    /// <summary>
    /// Provides an area where all stats can be initialised as part of the construction process.
    /// The values provided to the stats here should be base values, not adjusted values for research etc.
    /// </summary>
    protected abstract void InitialiseStats();

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Marks the unit as destroyed.
    /// </summary>
    public void Destroy()
    {
        base.gameObject.SetActive( false );

        if ( this.Destroyed != null )
        {
            this.Destroyed.Invoke( this );
        }
    }

    /// <summary>
    /// Gets all the stats that are considered valid.
    /// </summary>
    /// <remarks>
    /// A stat is considered valid if it is not null.
    /// </remarks>
    public List<Stat> ValidStats()
    {
        List<Stat> stats = new List<Stat>();

        if ( this.Endurance != null ) stats.Add( this.Endurance );
        if ( this.Health != null ) stats.Add( this.Health );
        if ( this.Initiative != null ) stats.Add( this.Initiative );
        if ( this.Morale != null ) stats.Add( this.Morale );
        if ( this.Movement != null ) stats.Add( this.Movement );
        if ( this.MeleeAttack != null ) stats.Add( this.MeleeAttack );
        if ( this.MeleeDefence != null ) stats.Add( this.MeleeDefence );
        if ( this.MeleePrecision != null ) stats.Add( this.MeleePrecision );
        if ( this.MeleeEvasion != null ) stats.Add( this.MeleeEvasion );
        if ( this.RangedAttack != null ) stats.Add( this.RangedAttack );
        if ( this.RangedDefence != null ) stats.Add( this.RangedDefence );
        if ( this.RangedPrecision != null ) stats.Add( this.RangedPrecision );
        if ( this.RangedEvasion != null ) stats.Add( this.RangedEvasion );
        if ( this.MagicAttack != null ) stats.Add( this.MagicAttack );
        if ( this.MagicDefence != null ) stats.Add( this.MagicDefence );
        if ( this.MagicPrecision != null ) stats.Add( this.MagicPrecision );
        if ( this.MagicEvasion != null ) stats.Add( this.MagicEvasion );

        return stats;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    /// <summary>
    /// Called when the unit is awakened.
    /// </summary>
    protected virtual void Awake()
    {
        
    }

    /// <summary>
    /// Called during the start cycle.
    /// </summary>
    protected virtual void Start()
    {
        
    }

    /// <summary>
    /// Called during the update cycle.
    /// </summary>
    protected virtual void Update()
    {

    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Applies the language settings for the unit.
    /// </summary>
    /// <param name="settings">The unit settings to apply.</param>
    private void ApplyLanguageSettings( UnitSettings settings )
    {
        this.Name = settings.Name;
        this.Description = settings.Description;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the name of the unit.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the unit.
    /// </summary>
    public string Description { get; private set; }

    #region Stats

    /** IMPORTANT: When accessing stats, they should always be checked for null values first! **/

    /// <summary>
    /// Gets/Sets the endurance settings for the unit.
    /// </summary>
    public Endurance Endurance { get; protected set; }

    /// <summary>
    /// Gets/Sets the health settings for the unit.
    /// </summary>
    public Health Health { get; protected set; }

    /// <summary>
    /// Gets/Sets the initiative settings for the unit.
    /// </summary>
    public Initiative Initiative { get; protected set; }

    /// <summary>
    /// Gets/Sets the morale settings for the unit.
    /// </summary>
    public Morale Morale { get; protected set; }

    /// <summary>
    /// Gets/Sets the movement settings for the unit.
    /// </summary>
    public Movement Movement { get; protected set; }

    /// <summary>
    /// Gets/Sets the melee attack settings for the unit.
    /// </summary>
    public MeleeAttack MeleeAttack { get; protected set; }

    /// <summary>
    /// Gets/Sets the melee defence settings for the unit.
    /// </summary>
    public MeleeDefence MeleeDefence { get; protected set; }

    /// <summary>
    /// Gets/Sets the melee precision settings for the unit.
    /// </summary>
    public MeleePrecision MeleePrecision { get; protected set; }

    /// <summary>
    /// Gets/Sets the melee evasion settings for the unit.
    /// </summary>
    public MeleeEvasion MeleeEvasion { get; protected set; }

    /// <summary>
    /// Gets/Sets the ranged attack settings for the unit.
    /// </summary>
    public RangedAttack RangedAttack { get; protected set; }

    /// <summary>
    /// Gets/Sets the ranged defence settings for the unit.
    /// </summary>
    public RangedDefence RangedDefence { get; protected set; }

    /// <summary>
    /// Gets/Sets the ranged precision settings for the unit.
    /// </summary>
    public RangedPrecision RangedPrecision { get; protected set; }

    /// <summary>
    /// Gets/Sets the ranged evasion settings for the unit.
    /// </summary>
    public RangedEvasion RangedEvasion { get; protected set; }

    /// <summary>
    /// Gets/Sets the magic attack settings for the unit.
    /// </summary>
    public MagicAttack MagicAttack { get; protected set; }

    /// <summary>
    /// Gets/Sets the magic defence settings for the unit.
    /// </summary>
    public MagicDefence MagicDefence { get; protected set; }

    /// <summary>
    /// Gets/Sets the magic precision settings for the unit.
    /// </summary>
    public MagicPrecision MagicPrecision { get; protected set; }

    /// <summary>
    /// Gets/Sets the magic evasion settings for the unit.
    /// </summary>
    public MagicEvasion MagicEvasion { get; protected set; }

    #endregion

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets the unit type.
    /// </summary>
    public UnitType Type { get { return this._unitType; } }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
