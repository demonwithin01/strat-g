using UnityEngine;
using System.Collections;

public class AIControlledUnit : BattleUnit
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    // Use this for initialization
    void Start()
    {
        base.IsAIControlled = true;

        Endurance stat = new Endurance( 5f );
    }

    // Update is called once per frame
    void Update()
    {

    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public override void StartTurn()
    {
        BattleHex endTile = null;

        for ( int i = 0 ; i < hexesWithinDistance.Count ; i++ )
        {
            BattleHex tile = hexesWithinDistance[ i ];

            if ( tile.DoesNeighbourHaveUnit )
            {
                for( int j = 0 ; j < tile.Neighbours.Count ;j++ )
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
            int tileIndex = ( (int)( Random.value * 1000 ) ) % hexesWithinDistance.Count;
            endTile = hexesWithinDistance[ tileIndex ];
        }

        Path<BattleHex> path = PathFinder.FindPath( CurrentHexTile, endTile );

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
            TravelPathAndAttack( path.GetPath(), attackTarget );
        }
        else
        {
            TravelPath( path.GetPath() );
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

}
