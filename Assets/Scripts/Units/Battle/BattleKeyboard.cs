using UnityEngine;

public class BattleKeyboard : InputManager<BattleInputAction>
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members
        
    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public BattleKeyboard()
    {
        base.AddKeyMapping( BattleInputAction.MoveCameraUp, KeyCode.W );
        base.AddKeyMapping( BattleInputAction.MoveCameraRight, KeyCode.D );
        base.AddKeyMapping( BattleInputAction.MoveCameraDown, KeyCode.S );
        base.AddKeyMapping( BattleInputAction.MoveCameraLeft, KeyCode.A );

        base.AddKeyMapping( BattleInputAction.RotateCameraRight, KeyCode.E );
        base.AddKeyMapping( BattleInputAction.RotateCameraLeft, KeyCode.Q );

        base.AddKeyMapping( BattleInputAction.CameraSpeedMultiplier, KeyCode.LeftShift );

        base.AddKeyMapping( BattleInputAction.ShowUnitDetails, KeyCode.BackQuote );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Unity Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
