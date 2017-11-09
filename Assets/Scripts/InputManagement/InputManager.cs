using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager<ActionType> : MonoBehaviour where ActionType : struct, IConvertible
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Maintains a list of the mapped actions.
    /// </summary>
    private List<ActionType> _mappedActions = new List<ActionType>();

    /// <summary>
    /// Maintains the list of key mappings.
    /// </summary>
    private Dictionary<ActionType, KeyCode[]> _keyMappings = new Dictionary<ActionType, KeyCode[]>();

    /// <summary>
    /// Maintains a list of the key states.
    /// </summary>
    private Dictionary<ActionType, KeyState> _keyStates = new Dictionary<ActionType, KeyState>();

    /// <summary>
    /// Maintains a list of key state checks.
    /// </summary>
    private List<KeyState[]> _keyStateChecks = new List<KeyState[]>();

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Unity Update loop.
    /// </summary>
    protected virtual void Update()
    {
        foreach( ActionType actionType in this._mappedActions )
        {
            this._keyStates[ actionType ] = GetKeysState( this._keyMappings[ actionType ] );
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Checks whether the action type keys are in a pressed state.
    /// </summary>
    /// <param name="actionType">The action type.</param>
    public bool IsInputActionPressed( ActionType actionType )
    {
        return ( this._keyStates[ actionType ] == KeyState.Pressed );
    }

    /// <summary>
    /// Checks whether the action type keys are in a released state.
    /// </summary>
    /// <param name="actionType">The action type.</param>
    public bool IsInputActionReleased( ActionType actionType )
    {
        return ( this._keyStates[ actionType ] == KeyState.Released );
    }

    /// <summary>
    /// Checks whether the action type keys are down.
    /// </summary>
    /// <param name="actionType">The action type.</param>
    public bool IsInputActionDown( ActionType actionType )
    {
        return ( this._keyStates[ actionType ] == KeyState.Down );
    }

    /// <summary>
    /// Checks whether the action type keys are up.
    /// </summary>
    /// <param name="actionType">The action type.</param>
    public bool IsInputActionUp( ActionType actionType )
    {
        return ( this._keyStates[ actionType ] == KeyState.Up );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    /// <summary>
    /// Adds a key mapping.
    /// </summary>
    /// <param name="actionType">The action type.</param>
    /// <param name="keyCode">The key associated with the mapping.</param>
    protected void AddKeyMapping( ActionType actionType, KeyCode keyCode )
    {
        AddKeyMapping( actionType, new KeyCode[] { keyCode } );
    }

    /// <summary>
    /// Adds a key mapping.
    /// </summary>
    /// <param name="actionType">The action type.</param>
    /// <param name="keyCodes">The keys associated with the mapping.</param>
    protected void AddKeyMapping( ActionType actionType, KeyCode[] keyCodes )
    {
        this._keyMappings.Add( actionType, keyCodes );
        this._keyStates.Add( actionType, KeyState.Up ); // Everything always starts in the up state.
        this._mappedActions.Add( actionType );

        int count = keyCodes.Length;

        for ( int i = 1 ; i <= count && this._keyStateChecks.Count <= count ; i++ )
        {
            this._keyStateChecks.Add( new KeyState[ i ] );
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyCodes">The key codes to get the state of.</param>
    private KeyState GetKeysState( KeyCode[] keyCodes )
    {
        int upCount = 0;
        int downCount = 0;
        int pressedCount = 0;
        int releasedCount = 0;
        int numOfKeys = keyCodes.Length;
        KeyState[] keyStates = this._keyStateChecks[ numOfKeys - 1 ];

        for ( int i = 0 ; i < numOfKeys ; i++ )
        {
            keyStates[ i ] = GetKeyState( keyCodes[ i ] );
        }

        for ( int i = 0 ; i < numOfKeys ; i++ )
        {
            switch( keyStates[ i ] )
            {
                case KeyState.Pressed:
                    ++pressedCount;
                    break;
                case KeyState.Released:
                    ++releasedCount;
                    break;
                case KeyState.Up:
                    ++upCount;
                    break;
                case KeyState.Down:
                    ++downCount;
                    break;
            }
        }

        if ( pressedCount > 0 && downCount + pressedCount == numOfKeys )
        {
            return KeyState.Pressed;
        }
        
        if ( releasedCount > 0 && upCount + releasedCount == numOfKeys )
        {
            return KeyState.Released;
        }

        if ( downCount == numOfKeys )
        {
            return KeyState.Down;
        }

        return KeyState.Up;
    }

    /// <summary>
    /// Gets the key state of an individual key.
    /// </summary>
    /// <param name="keyCode">The key code to get the state of.</param>
    private KeyState GetKeyState( KeyCode keyCode )
    {
        if ( Input.GetKeyDown( keyCode ) )
        {
            return KeyState.Pressed;
        }

        if ( Input.GetKeyUp( keyCode ) )
        {
            return KeyState.Released;
        }

        if ( Input.GetKey( keyCode ) )
        {
            return KeyState.Down;
        }

        return KeyState.Up;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
