using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

    // Use this for initialization
    void Start()
    {
        _livingUnits = FindObjectsOfType<BattleUnit>().ToList(); ;
        _turnOrder = new Queue<Turn>();
        _waitTurnOrder = new Queue<Turn>();

        _battleGrid = FindObjectOfType<BattleGrid>();

        _battleTurnGui = new BattleTurnGUI( _turnPanel );

        GenerateTurnOrder();
    }

    // Update is called once per frame
    void Update()
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
        _currentTurn.WaitEnabled = true;
        _waitTurnOrder.Enqueue( _currentTurn );

        FinishTurn();
    }

    /// <summary>
    /// Called when a unit finishes its turn.
    /// </summary>
    public void FinishTurn()
    {
        if ( _turnOrder.Count == 0 )
        {
            _turnOrder = _waitTurnOrder;
            _waitTurnOrder = new Queue<Turn>();
        }

        if ( _turnOrder.Count == 0)
        {
            GenerateTurnOrder();
        }

        _currentTurn = _turnOrder.Dequeue();
        _battleGrid.SelectUnit( _currentTurn.Unit );
        _currentTurn.Unit.StartTurn();
    }

    /// <summary>
    /// Called when a unit is destroyed to remove it from the turn list.
    /// </summary>
    /// <param name="unit">The unit that has been destroyed.</param>
    public void UnitDestroyed( BattleUnit unit )
    {
        _livingUnits.Remove( unit );

        Turn deadTurn = _turnOrder.FirstOrDefault( s => s.Unit == unit );

        if ( deadTurn != null )
        {
            Queue<Turn> restructure = new Queue<Turn>();
            
            while ( _turnOrder.Count > 0 )
            {
                Turn turn = _turnOrder.Dequeue();

                if ( turn != deadTurn )
                {
                    restructure.Enqueue( turn );
                }
            }

            _turnOrder = restructure;
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Generates the turn order of the units.
    /// </summary>
    /// <remarks>
    /// Currently this is randomised, but will evolve over time.
    /// </remarks>
    private void GenerateTurnOrder()
    {
        List<Turn> turns = new List<Turn>();

        foreach ( BattleUnit unit in _livingUnits )
        {
            Turn turn = new Turn( unit );
            turn.randomisedValue = Random.value;

            turns.Add( turn );
        }

        turns = turns.OrderBy( s => s.randomisedValue ).ToList();

        foreach ( Turn turn in turns )
        {
            _turnOrder.Enqueue( turn );
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

}