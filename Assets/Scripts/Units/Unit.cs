using System;
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
