using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The battle turn manager.
/// TODO: Rename
/// </summary>
public class TurnManager : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    /// <summary>
    /// Holds the panel that is responsible for rendering the current and future turns.
    /// </summary>
    [SerializeField]
    private GameObject _turnPanel = null;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds a list of all the units that are currently still alive.
    /// </summary>
    private List<BattleUnit> _livingUnits;

    /// <summary>
    /// Holds the current turn order.
    /// </summary>
    private Queue<Turn> _turnOrder;

    /// <summary>
    /// Holds the wait turn order.
    /// </summary>
    private Queue<Turn> _waitTurnOrder;

    /// <summary>
    /// Holds the current turn.
    /// </summary>
    private Turn _currentTurn;

    /// <summary>
    /// Holds the reference to the battle grid object.
    /// </summary>
    private BattleGrid _battleGrid;

    /// <summary>
    /// Holds the reference to the battle turn GUI manager.
    /// </summary>
    private BattleTurnGUI _battleTurnGui;
    
    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods
        
    /// <summary>
    /// Unity Start method, used for initialization.
    /// </summary>
    private void Start()
    {
        this._livingUnits = FindObjectsOfType<BattleUnit>().ToList();
        this._turnOrder = new Queue<Turn>();
        this._waitTurnOrder = new Queue<Turn>();

        this._battleGrid = FindObjectOfType<BattleGrid>();

        this._battleTurnGui = new BattleTurnGUI( this._turnPanel, this );

        GenerateTurnOrder();
        
        if ( this.TurnOrderChanged != null )
        {
            this.TurnOrderChanged.Invoke( this._turnOrder, this._waitTurnOrder );
        }
    }
    
    /// <summary>
    /// Unity Update method, called once per frame.
    /// </summary>
    private void Update()
    {

    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Called when the unit wants to wait.
    /// </summary>
    public void Wait()
    {
        if ( this._currentTurn.CanWait )
        {
            this._currentTurn.WaitEnabled = true;
            this._waitTurnOrder.Enqueue( this._currentTurn );

            TurnEnd();
        }
    }

    /// <summary>
    /// Called when a unit finishes its turn.
    /// </summary>
    public void FinishTurn()
    {
        if ( this._currentTurn != null )
        {
            this._currentTurn.Unit.UIButton.SetActiveState( false );
        }

        TurnEnd();
    }

    /// <summary>
    /// Called when a unit is destroyed to remove it from the turn list.
    /// </summary>
    /// <param name="unit">The unit that has been destroyed.</param>
    public void UnitDestroyed( BattleUnit unit )
    {
        this._livingUnits.Remove( unit );

        Turn deadTurn = this._turnOrder.FirstOrDefault( s => s.Unit == unit );

        if ( deadTurn != null )
        {
            Queue<Turn> restructure = new Queue<Turn>();
            
            while ( this._turnOrder.Count > 0 )
            {
                Turn turn = this._turnOrder.Dequeue();

                if ( turn != deadTurn )
                {
                    restructure.Enqueue( turn );
                }
            }

            this._turnOrder = restructure;
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Handles when the current turn is completely terminated.
    /// </summary>
    private void TurnEnd()
    {
        if ( this._currentTurn != null )
        {
            this._currentTurn.IsCurrentTurn = false;
        }

        if ( this._turnOrder.Count == 0 )
        {
            this._turnOrder = this._waitTurnOrder;
            this._waitTurnOrder = new Queue<Turn>();
        }

        if ( _turnOrder.Count == 0 )
        {
            GenerateTurnOrder();
        }

        if ( this.TurnOrderChanged != null )
        {
            this.TurnOrderChanged.Invoke( this._turnOrder, this._waitTurnOrder );
        }

        this._currentTurn = this._turnOrder.Dequeue();

        this._battleGrid.SelectUnit( this._currentTurn.Unit );
        this._currentTurn.IsCurrentTurn = true;
        this._currentTurn.Unit.StartTurn();
    }

    /// <summary>
    /// Generates the turn order of the units.
    /// </summary>
    /// <remarks>
    /// Currently this is randomised, but will evolve over time.
    /// </remarks>
    private void GenerateTurnOrder()
    {
        List<Turn> turns = new List<Turn>();

        foreach ( BattleUnit unit in this._livingUnits )
        {
            Turn turn = new Turn( unit );
            turn.randomisedValue = UnityEngine.Random.value;

            turns.Add( turn );
        }

        turns = turns.OrderBy( s => s.randomisedValue ).ToList();

        foreach ( Turn turn in turns )
        {
            turn.Unit.UIButton.SetActiveState( true );
            turn.Unit.UIButton.SetTurn( turn );
            this._turnOrder.Enqueue( turn );
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Events
        
    /// <summary>
    /// The event for when the turn order is changed.
    /// </summary>
    public event Action<Queue<Turn>, Queue<Turn>> TurnOrderChanged;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets the current turn order for the current set of turns.
    /// </summary>
    public Queue<Turn> TurnOrder { get { return this._turnOrder; } }

    #endregion

    /* --------------------------------------------------------------------- */

}