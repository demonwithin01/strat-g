using System.Collections.Generic;
using DEnt;
using UnityEngine;

/// <summary>
/// The object responsible for determining a path across hex tiles.
/// </summary>
public static class PathFinder
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Finds a valid path, if any, between two hex grid points.
    /// </summary>
    /// <param name="start">The hex tile to start from.</param>
    /// <param name="destination">The hex tile to end on.</param>
    /// <returns>A valid path, otherwise null.</returns>
    public static Path<BattleHex> FindPath( BattleHex start, BattleHex destination )
    {
        var closed = new HashSet<BattleHex>();
        var queue = new PriorityQueue<double, Path<BattleHex>>();

        if ( destination.IsPassable == false || destination.HasUnit )
        {
            return null;
        }

        queue.Enqueue( 0, new Path<BattleHex>( start ) );
        
        while ( !queue.IsEmpty )
        {
            var path = queue.Dequeue();
            
            if ( closed.Contains( path.LastStep ) )
            {
                continue;
            }

            if ( path.LastStep.Equals( destination ) )
            {
                return path;
            }

            closed.Add( path.LastStep );
            
            foreach ( BattleHex hex in path.LastStep.Neighbours )
            {
                if ( hex.IsPassable == false || hex.HasUnit )
                {
                    continue;
                }

                double distance = Distance( path.LastStep, hex );
                var newPath = path.AddStep( hex, distance );
                var estimate = newPath.TotalCost + Estimate( hex, destination, newPath.TotalCost );

                queue.Enqueue( estimate, newPath );
            }
        }

        return null;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods
        
    /// <summary>
    /// Gets the distance between two tiles.
    /// </summary>
    private static double Distance( BattleHex tile1, BattleHex tile2 )
    {
        return Vector3.Distance( tile1.WorldPosition, tile2.WorldPosition );
    }

    /// <summary>
    /// Gets an estimate of the cost to travel between two hex tiles.
    /// </summary>
    /// <param name="tile">The tile to travel from.</param>
    /// <param name="destination">The tile to travel to.</param>
    /// <param name="totalCost">The cost of travelling up to this point so far.</param>
    /// <returns></returns>
    private static double Estimate( BattleHex tile, BattleHex destination, double totalCost )
    {
        float destX = destination.X;
        float destY = destination.Y;
        float tileX = tile.X;
        float tileY = tile.Y;

        float dx = Mathf.Abs( destX - tileX );
        float dy = Mathf.Abs( destY - tileY );
        float z1 = -( tileX + tileY );
        float z2 = -( destX + destY );
        float dz = Mathf.Abs( z2 - z1 );
        
        return Mathf.Max( dx, dy, dz );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
