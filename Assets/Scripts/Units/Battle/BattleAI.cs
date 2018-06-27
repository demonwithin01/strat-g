using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// <summary>
/// The AI player object.
/// </summary>
public class BattleAI : IDisposable
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Maintains it's own value for the difficulty setting.
    /// </summary>
    private Difficulty _difficulty;

    /// <summary>
    /// Maintains a reference to the turn manager.
    /// </summary>
    private readonly BattleTurnManager _turnManager;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Initialises the Battle AI object.
    /// </summary>
    /// <param name="turnManager">The reference to the turn manager.</param>
    public BattleAI( BattleTurnManager turnManager )
    {
        this._turnManager = turnManager;

        this._difficulty = GlobalSettings.GameDifficulty;
        GlobalSettings.DifficultyChanged += DifficultyChanged;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Tells the AI player that it is their turn.
    /// </summary>
    /// <param name="turn">The AI players turn.</param>
    public void ActivateTurn( Turn turn )
    {
        BattleUnit unit = turn.Unit;

        if ( this._difficulty == Difficulty.Hard && this.ShouldWait( turn, unit ) )
        {
            this._turnManager.Wait();

            return;
        }

        List<BattleHex> hexesWithinDistance = unit.HexesWithinReach;
        BattleHex endTile = null;

        for ( int i = 0 ; i < hexesWithinDistance.Count ; i++ )
        {
            BattleHex tile = hexesWithinDistance[ i ];

            if ( tile.DoesNeighbourHaveUnit )
            {
                for ( int j = 0 ; j < tile.Neighbours.Count ; j++ )
                {
                    BattleHex neighbour = tile.Neighbours[ j ];
                    if ( neighbour.HasUnit && neighbour.Unit.IsAIControlled == false )
                    {
                        endTile = tile;
                        break;
                    }
                }

                if ( endTile != null )
                {
                    break;
                }
            }
        }

        if ( endTile == null )
        {
            if ( hexesWithinDistance.Count == 0 )
            {
                endTile = unit.CurrentHexTile;
            }
            else
            {
                int tileIndex = ( (int)( UnityEngine.Random.value * 1000 ) ) % hexesWithinDistance.Count;
                endTile = hexesWithinDistance[ tileIndex ];
            }
        }

        Path<BattleHex> path = PathFinder.FindPath( unit.CurrentHexTile, endTile );

        BattleUnit attackTarget = null;

        for ( int i = 0 ; i < endTile.Neighbours.Count ; i++ )
        {
            BattleHex neighbour = endTile.Neighbours[ i ];

            if ( neighbour.HasUnit && neighbour.Unit.IsAIControlled == false )
            {
                attackTarget = neighbour.Unit;
            }
        }

        if ( attackTarget != null )
        {
            unit.TravelPathAndAttack( path.GetPath(), attackTarget );
        }
        else
        {
            unit.TravelPath( path.GetPath() );
        }
    }

    /// <summary>
    /// Detaches from the difficulty change event.
    /// </summary>
    public void Dispose()
    {
        GlobalSettings.DifficultyChanged -= DifficultyChanged;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods
        
    /// <summary>
    /// Handles when the difficulty is changed.
    /// </summary>
    /// <param name="difficulty">The new difficulty.</param>
    private void DifficultyChanged( Difficulty difficulty )
    {
        this._difficulty = difficulty;
    }

    /// <summary>
    /// Gets whether or not the unit should wait.
    /// </summary>
    /// <param name="turn">The current turn.</param>
    /// <param name="unit">The unit who's turn it is.</param>
    private bool ShouldWait( Turn turn, BattleUnit unit )
    {
        if ( turn.CanWait == false )
        {
            return false;
        }

        ReadOnlyCollection<Turn> remainingTurns = this._turnManager.RemainingTurns;

        int unitMaxTravelDistance = unit.MaxTravelDistance;
        bool isAUnitWithinAttackDistance = false;
        bool isAUnitWithinAttackDistanceIfWait = false;

        foreach ( Turn remainingTurn in remainingTurns )
        {
            if ( remainingTurn.Unit.IsAIControlled )
            {
                continue;
            }

            Path<BattleHex> path = PathFinder.FindPath( unit.CurrentHexTile, remainingTurn.Unit.CurrentHexTile );

            if ( path == null )
            {
                continue;
            }

            int pathLength = path.GetPath().Count;

            if ( pathLength < unitMaxTravelDistance )
            {
                isAUnitWithinAttackDistance = true;
            }
            else if ( pathLength < ( unitMaxTravelDistance + remainingTurn.Unit.MaxTravelDistance ) )
            {
                isAUnitWithinAttackDistanceIfWait = true;
            }
        }

        return ( isAUnitWithinAttackDistanceIfWait == true && isAUnitWithinAttackDistance == false );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties
        
    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
