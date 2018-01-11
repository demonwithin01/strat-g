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
    
    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates a new stat instance using the provided initial value.
    /// </summary>
    /// <param name="initialValue">The initial value of the stat.</param>
    public Stat( float initialValue )
    {
        this._value = initialValue;
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

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
