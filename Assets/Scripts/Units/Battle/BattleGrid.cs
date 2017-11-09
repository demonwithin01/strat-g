using UnityEngine;
using DEnt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleGrid : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

#pragma warning disable 0649
    [SerializeField]
    private GameObject _hexTemplate;
#pragma warning restore 0649

    /// <summary>
    /// The camera movement speed.
    /// </summary>
    [SerializeField]
    private float _cameraSpeed = 0.2f;

    /// <summary>
    /// The minimum camera movement limits.
    /// </summary>
    [SerializeField]
    private Vector2 _minCameraLimit = new Vector2( -15f, -15f );

    /// <summary>
    /// The maximum camera movement limits.
    /// </summary>
    [SerializeField]
    private Vector2 _maxCameraLimit = new Vector2( 15f, 15f );

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private List<ControllableUnit> _units;

    private List<AIControlledUnit> _player2Units;
    
    /// <summary>
    /// Maintains the list of hex tiles.
    /// </summary>
    private List<BattleHex> _hexes;

    /// <summary>
    /// The current unit that has been selected.
    /// </summary>
    private BattleUnit _currentUnit;

    /// <summary>
    /// A reference to the mouse input handler.
    /// </summary>
    private BattleMouse _battleMouse;

    /// <summary>
    /// A reference to the keyboard input handler.
    /// </summary>
    private BattleKeyboard _battleKeyboard;

    /// <summary>
    /// A reference to the battle turn manager.
    /// </summary>
    private TurnManager _turnManager;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Unity Start event handler.
    /// </summary>
    void Start()
    {
        this._hexes = new List<BattleHex>();

        this._battleMouse = GetComponent<BattleMouse>();
        this._battleKeyboard = GetComponent<BattleKeyboard>();

        float halfLength = 1;// / (float)Math.Cos( 30 * Math.PI / 180d );
        float fullLength = 2 * halfLength;
        
        Point2D gridSize = new Point2D( 12, 9 );
        Dictionary<Point2D, BattleHex> hexGrid = new Dictionary<Point2D, BattleHex>();

        this._turnManager = FindObjectOfType<TurnManager>();

        this._units = FindObjectsOfType<ControllableUnit>().ToList();
        this._player2Units = FindObjectsOfType<AIControlledUnit>().ToList();

        for ( int y = 0 ; y < gridSize.Y ; y++ )
        {
            int sizeX = gridSize.X;

            if ( y % 2 != 0 && ( gridSize.X + 0.5f ) * fullLength > 10f )
            {
                --sizeX;
            }

            for ( int x = 0 ; x < sizeX ; x++ )
            {
                GameObject hex = (GameObject)Instantiate( this._hexTemplate );
                BattleHex hexComponent = hex.GetComponent<BattleHex>();
                hexComponent.Configure( x, y, gridSize, fullLength, transform.gameObject );

                hexGrid.Add( new Point2D( x, y ), hexComponent );
                this._hexes.Add( hexComponent );
            }
        }

        foreach ( var hexItem in hexGrid )
        {
            hexItem.Value.FindNeighbours( hexGrid );
        }

        int index = 0;
        for ( int i = 0 ; i < this._units.Count ; i++ )
        {
            ControllableUnit unit = _units[ i ];

            unit.InitialiseWithTile( this, _hexes[ index ], 90f );
            
            index += gridSize.X - i % 2;
        }

        for ( int i = 0 ; i < this._player2Units.Count ; i++ )
        {
            this._player2Units[ i ].InitialiseWithTile( this, this._hexes[ this._hexes.Count - i - 1 ], 270f );
        }

        this._turnManager.FinishTurn();
    }

    /// <summary>
    /// Unity Update loop.
    /// </summary>
    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        if ( this._battleKeyboard.IsInputActionDown( BattleInputAction.MoveCameraUp ) )
        {
            cameraPosition.z = Mathf.Min( cameraPosition.z + this._cameraSpeed, this._maxCameraLimit.y );
        }

        if ( this._battleKeyboard.IsInputActionDown( BattleInputAction.MoveCameraDown ) )
        {
            cameraPosition.z = Mathf.Max( cameraPosition.z - this._cameraSpeed, this._minCameraLimit.y );
        }

        if ( this._battleKeyboard.IsInputActionDown( BattleInputAction.MoveCameraRight ) )
        {
            cameraPosition.x = Mathf.Min( cameraPosition.x + this._cameraSpeed, this._maxCameraLimit.x );
        }

        if ( this._battleKeyboard.IsInputActionDown( BattleInputAction.MoveCameraLeft ) )
        {
            cameraPosition.x = Mathf.Max( cameraPosition.x - this._cameraSpeed, this._minCameraLimit.x );
        }

        Camera.main.transform.position = cameraPosition;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public void UnhighlightNodes()
    {
        foreach ( var hex in this._hexes )
        {
            if ( hex.DoesNeighbourHaveUnit )
            {
                hex.SetColour( Color.green );
            }
            else
            {
                hex.ResetColour();
            }
        }
    }
    
    /// <summary>
    /// Selects the specified unit.
    /// </summary>
    /// <param name="unit">The unit to be marked as selcted.</param>
    public void SelectUnit( BattleUnit unit )
    {
        if ( _currentUnit == unit ) return;

        if ( _currentUnit != null )
        {
            _currentUnit.DeselectUnit();
        }

        _currentUnit = unit;
        
        _currentUnit.SelectUnit();

        UnhighlightNodes();
        _currentUnit.FindNodesWithinDistance( this._hexes );
        _currentUnit.HighlightNodes();
    }

    /// <summary>
    /// Event that is raised when the unit finishes it's movement and attack actions.
    /// </summary>
    public void PathFinished()
    {
        _turnManager.FinishTurn();
        //ControllableUnit startUnit = this._currentUnit;
        //do
        //{
        //    SelectUnit( _units[ ( _currentIndex + 1 ) % _units.Count ] );
        //}
        //while ( this._currentUnit.CanMove == false && startUnit != this._currentUnit );

        _battleMouse.Unlock();
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties
    
    /// <summary>
    /// Gets the currently selected unit.
    /// </summary>
    public BattleUnit CurrentUnit { get { return this._currentUnit; } }
    
    /// <summary>
    /// Gets all the hex tiles in the grid.
    /// </summary>
    public List<BattleHex> HexTiles { get { return this._hexes; } }

    #endregion

    /* --------------------------------------------------------------------- */

}
