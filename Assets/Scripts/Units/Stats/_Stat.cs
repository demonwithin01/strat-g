using DEnt;
using DEnt.Language;
using UnityEngine;

public abstract class Stat
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds the value of the stat.
    /// </summary>
    private float _value;

    /// <summary>
    /// Holds the modifications value of the stat.
    /// </summary>
    private float _modificationsValue;

    /// <summary>
    /// Holds the maximum range variance of the stat.
    /// </summary>
    private float _maxVariance;

    /// <summary>
    /// Holds the minimum range variance of the stat.
    /// </summary>
    private float _minVariance;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates a new stat instance using the provided initial value.
    /// </summary>
    /// <param name="initialValue">The initial value of the stat.</param>
    public Stat( float initialValue )
        : this( initialValue, 0f, 0f )
    {
        this._value = initialValue;
    }

    /// <summary>
    /// Creates a new stat instance using the provided initial value.
    /// </summary>
    /// <param name="initialValue">The initial value of the stat.</param>
    /// <param name="maxVariance">The maximum range variance of the stat.</param>
    /// <param name="minVariance">The minimum range variance of the stat.</param>
    public Stat( float initialValue, float maxVariance, float minVariance )
    {
        this._value = initialValue;
        this._maxVariance = maxVariance;
        this._minVariance = minVariance;

        this.Modifications = new ManagedList<StatModification>();
        this.Modifications.CollectionChanged += ModificationsChanged;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Modifies the value of the stat, raising events when required.
    /// </summary>
    /// <param name="modifyBy">The amount to modify the stat value by.</param>
    public virtual void ModifyValue( float modifyBy )
    {
        this._value += modifyBy;

        if ( this.CanDropBelowZero == false )
        {
            this._value = Mathf.Max( 0f, this._value );
        }
    }

    /// <summary>
    /// Gets the next value to use, takes random variance into account.
    /// </summary>
    /// <returns>The next value to use with random variance.</returns>
    public float NextValue()
    {
        float totalRange = ( this._maxVariance - this._minVariance );

        float value = this._value;

        if ( totalRange > 0f )
        {
            value += Random.Range( -this._minVariance, this._maxVariance );
        }

        value += this._modificationsValue;

        return value;
    }

    /// <summary>
    /// Applies the language settings for the statistic.
    /// </summary>
    /// <param name="settings">The stat settings to apply.</param>
    public void ApplyLanguageSettings( StatSettings settings )
    {
        this.Name = settings.Name;
        this.Description = settings.Description;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Event to when the modifications list is changed.
    /// </summary>
    private void ModificationsChanged( ManagedList<StatModification> modifications )
    {
        this._modificationsValue = 0f;

        for ( int i = 0 ; i < this.Modifications.Count ; i++ )
        {
            this._modificationsValue += this.Modifications[ i ].Value;
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the name of the stat.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the description of the stat.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets whether or not this stat is allow to drop below zero.
    /// </summary>
    public bool CanDropBelowZero { get; protected set; }

    /// <summary>
    /// Gets the modifications against this stat.
    /// </summary>
    public ManagedList<StatModification> Modifications { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets the vlaue of the stat.
    /// </summary>
    public float Value
    {
        get
        {
            return this._value;
        }
    }

    /// <summary>
    /// Gets the maximum range variance of the stat.
    /// </summary>
    public float MaxVariance
    {
        get
        {
            return this._maxVariance;
        }
    }

    /// <summary>
    /// Gets the minimum range variance of the stat.
    /// </summary>
    public float MinVariance
    {
        get
        {
            return this._minVariance;
        }
    }

    /// <summary>
    /// Gets the max possible value of the stat.
    /// </summary>
    public float MaxValue
    {
        get
        {
            return ( this._value + this._maxVariance );
        }
    }

    /// <summary>
    /// Gets the min possible value of the stat.
    /// </summary>
    public float MinValue
    {
        get
        {
            return ( this._value + this._minVariance );
        }
    }

    /// <summary>
    /// Gets the total modifications value of the stat.
    /// </summary>
    public float TotalModificationsValue
    {
        get
        {
            return this._modificationsValue;
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
