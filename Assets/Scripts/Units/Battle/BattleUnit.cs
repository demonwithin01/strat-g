using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StrategyGame.Units;
using DEnt;

public class BattleUnit : MonoBehaviour
{
    
    /* --------------------------------------------------------------------- */

    #region Editable Fields

    /// <summary>
    /// The maximum number of hex tiles that this unit can travel.
    /// </summary>
    [SerializeField]
    private int _maxTileDistance = 1;

    /// <summary>
    /// The speed in which this unit travels to the next tile.
    /// </summary>
    [SerializeField]
    internal float _walkSpeed = 1f;

    /// <summary>
    /// The speed in which the unit rotates when standing still.
    /// </summary>
    [SerializeField]
    internal float _standingRotationSpeed = 2f;

    /// <summary>
    /// The health associated with this unit.
    /// </summary>
    [SerializeField]
    protected float _health = 100;

    /// <summary>
    /// The attack damage of this unit.
    /// </summary>
    [SerializeField]
    private float _attackDamage = 50;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Maintains a reference to the battle grid.
    /// </summary>
    private BattleGrid _battleGrid;

    /// <summary>
    /// The current tile that this unit is sitting on.
    /// </summary>
    private BattleHex _currentHex;

    /// <summary>
    /// The current actions to be undertaken.
    /// </summary>
    private Queue<UnitAction> _actions;

    /// <summary>
    /// The current action being undertaken.
    /// </summary>
    private UnitAction _currentAction;
    
    /// <summary>
    /// Whether or not this unit is AI controlled or player controlled.
    /// </summary>
    private bool _isAIControlled = false;

    /// <summary>
    /// Holds whether or not this is the currently selected unit.
    /// </summary>
    private bool _isCurrentUnit = false;

    /// <summary>
    /// Holds the rotation of the current unit.
    /// </summary>
    private Vector3 _rotation;

    /// <summary>
    /// Holds the current direction that the unit is facing.
    /// </summary>
    private HexDirection _facingDirection;

    /// <summary>
    /// The current tiles that are within walking distance of this unit.
    /// </summary>
    protected List<BattleHex> hexesWithinDistance;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    /// <summary>
    /// Constructor for creating a new instance.
    /// </summary>
    public BattleUnit()
    {
        hexesWithinDistance = new List<BattleHex>();
        _actions = new Queue<UnitAction>();
        _rotation = new Vector3( 270f, 0f, 0f );
        _facingDirection = HexDirection.East;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods
        
    /// <summary>
    /// Unity Awake event handler.
    /// </summary>
    protected virtual void Awake()
    {
        
    }

    /// <summary>
    /// Unity Update loop.
    /// </summary>
    protected virtual void FixedUpdate()
    {
        if ( this._currentAction != null )
        {
            this._currentAction.Perform();
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Finds all the nodes that are within walking distance of the unit.
    /// </summary>
    /// <param name="grid">A list of all the tiles in the map</param>
    public void FindNodesWithinDistance( List<BattleHex> grid )
    {
        hexesWithinDistance = new List<BattleHex>();

        foreach ( BattleHex hex in grid )
        {
            if ( _currentHex == hex || !hex.IsPassable || hex.HasUnit )
            {
                continue;
            }

            Path<BattleHex> path = PathFinder.FindPath( _currentHex, hex );

            if ( path != null && path.GetPath().Count <= _maxTileDistance )
            {
                hexesWithinDistance.Add( hex );
            }
        }
    }

    /// <summary>
    /// Highlights the hex tiles that are currently within range of this unit.
    /// </summary>
    public void HighlightNodes()
    {
        foreach ( BattleHex hex in hexesWithinDistance )
        {
            if ( hex.DoesNeighbourHaveUnit )
            {
                hex.SetColour( Colour.RGB( 255, 255, 0 ) );
            }
            else
            {
                hex.SetColour( Color.red );
            }
        }
    }

    /// <summary>
    /// Gets whether or not the current unit can travel to the hex tile specified.
    /// </summary>
    /// <param name="hex">The hex tile to test</param>
    public bool CanTravelTo( BattleHex hex )
    {
        return hexesWithinDistance.Contains( hex );
    }

    /// <summary>
    /// Gets whether or not the current unit is already standing on the provided hex tile.
    /// </summary>
    /// <param name="hex">The hex tile to test</param>
    public bool IsOnTile( BattleHex hex )
    {
        return ( _currentHex == hex );
    }

    /// <summary>
    /// Tells the unit to follow the specified path.
    /// </summary>
    /// <param name="path">The path to be followed by this unit</param>
    public HexDirection TravelPath( List<BattleHex> path )
    {
        if ( path.Count == 1 ) return _facingDirection;

        float currentRotation = Mathf.Round( base.transform.eulerAngles.y );

        Vector3 startPosition = Vector3.zero;
        Vector3 endPosition = this.CurrentHexTile.WorldPosition;

        HexDirection facingDirection = _facingDirection;
        
        for ( var i = 0 ; i < path.Count - 1 ; i++ )
        {
            BattleHex current = path[ i ];
            BattleHex next = path[ i + 1 ];

            HexDirection requiredDirection = HexHelpers.GetTargetDirection( current, next );

            float requiredRotation = HexHelpers.GetTargetDegrees( requiredDirection );

            if ( i == 0 && currentRotation != requiredRotation )
            {
                _actions.Enqueue( new UnitRotation( this, currentRotation, requiredRotation ) );
                currentRotation = requiredRotation;
                facingDirection = requiredDirection;
            }

            float distance = current.TileLength;
            float speedModifier = 1f;

            if ( i == 0 )
            {
                distance /= 2f;
                speedModifier = 2f;
            }

            startPosition = endPosition;

            endPosition = next.GetEdge( requiredDirection );
            
            HexDirection requiredFacingDirection = HexHelpers.ReverseDirection( requiredDirection );

            _actions.Enqueue( new UnitMovement( this, startPosition, endPosition, current, facingDirection, requiredFacingDirection, currentRotation, requiredRotation, speedModifier ) );

            facingDirection = requiredFacingDirection;
            currentRotation = requiredRotation;
        }

        BattleHex endTile = path[ path.Count - 1 ];

        _actions.Enqueue( new UnitMovement( this, endPosition, endTile.transform.position, endTile, facingDirection, facingDirection, 0f, 0f, 2f ) );
        
        this._currentAction = this._actions.Dequeue();

        _battleGrid.UnhighlightNodes();

        return facingDirection;
    }

    /// <summary>
    /// Tells the unit to follow the specified path, then attack the unit at the end of the path.
    /// </summary>
    /// <param name="path">The path to be followed by this unit</param>
    /// <param name="unit">The unit to be attacked at the end of the path</param>
    public void TravelPathAndAttack( List<BattleHex> path, BattleUnit unit )
    {
        HexDirection finalFacingDirection = TravelPath( path );
        BattleHex unitTile;

        //this._actions.la
        if ( path.Count > 1 )
        {
            unitTile = path[ path.Count - 1 ];
        }
        else
        {
            unitTile = _currentHex;
        }

        HexDirection requiredDirection = HexHelpers.GetTargetDirection( unitTile, unit.CurrentHexTile );

        if ( requiredDirection != finalFacingDirection )
        {
            this._actions.Enqueue( new UnitRotation( this, HexHelpers.GetTargetDegrees( HexHelpers.ReverseDirection( finalFacingDirection ) ), HexHelpers.GetTargetDegrees( requiredDirection ) ) );
        }

        this._actions.Enqueue( new UnitAttack( this, unit ) );

        if ( this._currentAction == null )
        {
            this._currentAction = this._actions.Dequeue();
        }
    }

    /// <summary>
    /// Recieves a set amount of damage.
    /// </summary>
    /// <param name="damage">The damage taken</param>
    public void ReceiveDamage( float damage )
    {
        Debug.Log( "Taken " + damage + " points of damage" );

        _health -= damage;
        
        if ( _health < 1 )
        {
            UnitDestroyed();
        }
    }

    public void ReceiveAttacks( AttackDamage[] attacks )
    {
        for ( int i = 0 ; i < attacks.Length ; i++ )
        {
            AttackDamage attack = attacks[ i ];

            switch( attack.Type )
            {
                case AttackType.Melee:
                    this.ApplyAttack( attack, null, null );
                    break;
                case AttackType.Ranged:
                    this.ApplyAttack( attack, null, null );
                    break;
                case AttackType.Magic:
                    this.ApplyAttack( attack, null, null );
                    break;
            }
        }
    }

    /// <summary>
    /// Called when the unit starts its turn.
    /// </summary>
    public virtual void StartTurn()
    {

    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #region Abstract Methods

    /// <summary>
    /// Callback raised for when the unit is destroyed.
    /// </summary>
    protected virtual void UnitDestroyed()
    {
        Debug.Log( "Object destroyed" );

        _currentHex.Unit = null;

        FindObjectOfType<TurnManager>().UnitDestroyed( this );

        Destroy( gameObject );
    }

    #endregion
    
    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    /// <summary>
    /// Initialises the unit with the provided tile and sets the starting rotation.
    /// </summary>
    /// <param name="battleGrid">The battle grid that manages all tiles.</param>
    /// <param name="tile">The starting tile.</param>
    /// <param name="yRotation">The starting rotation.</param>
    internal void InitialiseWithTile( BattleGrid battleGrid, BattleHex tile, float yRotation )
    {
        this.BattleGrid = battleGrid;
        this.CurrentHexTile = tile;
        this.transform.position = tile.transform.position;

        this._rotation.y = yRotation;

        _facingDirection = HexHelpers.GetRotationDirection( yRotation );

        this.transform.rotation = Quaternion.Euler( this._rotation );

        tile.Unit = this;
    }

    /// <summary>
    /// Selects this unit as the primary/unit for the player.
    /// Automatically deselects the current unit.
    /// </summary>
    internal void SelectUnit()
    {
        _isCurrentUnit = true;
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    /// <summary>
    /// Deselects the current unit.
    /// </summary>
    internal void DeselectUnit()
    {
        if ( _isCurrentUnit == false )
        {
            return;
        }

        _isCurrentUnit = false;
        GetComponent<MeshRenderer>().material.color = Color.white;
    }

    /// <summary>
    /// Raised when an action has reached its completion.
    /// </summary>
    /// <param name="action">The action that was just completed.</param>
    internal void FinishAction( UnitAction action )
    {
        if ( this._currentAction == action )
        {
            do
            {
                if ( this._actions.Count == 0 )
                {
                    this._currentAction = null;
                }
                else
                {
                    this._currentAction = this._actions.Dequeue(); 
                }
            }
            while ( this._currentAction != null && this._currentAction.PerformStart() == false );

            if ( this._currentAction == null )
            {
                this.FinishPath();
            }
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Applies the details of an attack to the unit.
    /// </summary>
    /// <param name="attack">The attack information.</param>
    /// <param name="defenceStat">The stat that governs defence for the attack type.</param>
    /// <param name="evasionStat">The stat that governs evasion for the attack type.</param>
    private void ApplyAttack( AttackDamage attack, Stat defenceStat, Stat evasionStat )
    {

    }
    
    /// <summary>
    /// Ends the current path and moves on to the next unit.
    /// </summary>
    private void FinishPath()
    {
        this.FindNodesWithinDistance( _battleGrid.HexTiles );
        this.HighlightNodes();

        _battleGrid.PathFinished();
    }
    
    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the current hex tile that this unit is standing on.
    /// </summary>
    public BattleHex CurrentHexTile { get { return _currentHex; } internal set { _currentHex = value; } }

    /// <summary>
    /// Gets the reference to the battle grid.
    /// </summary>
    public BattleGrid BattleGrid { get { return _battleGrid; } internal set { _battleGrid = value; } }

    /// <summary>
    /// Gets whether or not this unit is AI controlled.
    /// </summary>
    public bool IsAIControlled { get { return _isAIControlled; } internal set { _isAIControlled = value; } }

    /// <summary>
    /// Gets the facing direction of the unit.
    /// </summary>
    public HexDirection FacingDirection { get { return _facingDirection; } internal set { _facingDirection = value; } }

    /// <summary>
    /// Gets whether or not there are any hexes that the unit can move to.
    /// </summary>
    public bool CanMove { get { return hexesWithinDistance.Count > 0; } }

    /// <summary>
    /// Gets the attack damage of this unit.
    /// </summary>
    public float AttackDamage { get { return _attackDamage; } }

    #endregion

    /* --------------------------------------------------------------------- */

}
