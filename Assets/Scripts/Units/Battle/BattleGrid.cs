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
        _hexes = new List<BattleHex>();

        _battleMouse = GetComponent<BattleMouse>();

        float halfLength = 1;// / (float)Math.Cos( 30 * Math.PI / 180d );
        float fullLength = 2 * halfLength;
        
        Point2D gridSize = new Point2D( 12, 9 );
        Dictionary<Point2D, BattleHex> hexGrid = new Dictionary<Point2D, BattleHex>();

        _turnManager = FindObjectOfType<TurnManager>();
        
        _units = FindObjectsOfType<ControllableUnit>().ToList();
        _player2Units = FindObjectsOfType<AIControlledUnit>().ToList();

        for ( int y = 0 ; y < gridSize.Y ; y++ )
        {
            int sizeX = gridSize.X;

            if ( y % 2 != 0 && ( gridSize.X + 0.5f ) * fullLength > 10f )
            {
                --sizeX;
            }

            for ( int x = 0 ; x < sizeX ; x++ )
            {
                GameObject hex = (GameObject)Instantiate( _hexTemplate );
                BattleHex hexComponent = hex.GetComponent<BattleHex>();
                hexComponent.Configure( x, y, gridSize, fullLength, transform.gameObject );

                hexGrid.Add( new Point2D( x, y ), hexComponent );
                _hexes.Add( hexComponent );
            }
        }

        foreach ( var hexItem in hexGrid )
        {
            hexItem.Value.FindNeighbours( hexGrid );
        }

        int index = 0;
        for ( int i = 0 ; i < _units.Count ; i++ )
        {
            ControllableUnit unit = _units[ i ];

            unit.InitialiseWithTile( this, _hexes[ index ], 90f );
            
            index += gridSize.X - i % 2;
        }

        for ( int i = 0 ; i < _player2Units.Count ; i++ )
        {
            _player2Units[ i ].InitialiseWithTile( this, _hexes[ _hexes.Count - i - 1 ], 270f );
        }

        _turnManager.FinishTurn();
    }

    /// <summary>
    /// Unity Update loop.
    /// </summary>
    void Update()
    {
        
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public void UnhighlightNodes()
    {
        foreach ( var hex in _hexes )
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
        _currentUnit.FindNodesWithinDistance( _hexes );
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
    public BattleUnit CurrentUnit { get { return _currentUnit; } }
    
    /// <summary>
    /// Gets all the hex tiles in the grid.
    /// </summary>
    public List<BattleHex> HexTiles { get { return _hexes; } }

    #endregion

    /* --------------------------------------------------------------------- */

}
