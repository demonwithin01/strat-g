using UnityEngine;

public class Turn
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private BattleUnit _unit;

    internal float randomisedValue;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public Turn( BattleUnit unit )
    {
        _unit = unit;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets whether or not this unit is able to wait.
    /// </summary>
    public bool CanWait { get { return WaitEnabled == false; } }

    /// <summary>
    /// Gets whether or not this unit has had wait enabled.
    /// </summary>
    public bool WaitEnabled { get; internal set; }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Derived Properties

    public BattleUnit Unit { get { return _unit; } }

    #endregion

    /* --------------------------------------------------------------------- */

}
