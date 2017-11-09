public class HexHelpers
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
    /// Gets the reverse facing direction.
    /// </summary>
    /// <param name="direction">The direction to reverse.</param>
    public static HexDirection ReverseDirection( HexDirection direction )
    {
        switch ( direction )
        {
            case HexDirection.NorthEast:
                return HexDirection.SouthWest;
            case HexDirection.East:
                return HexDirection.West;
            case HexDirection.SouthEast:
                return HexDirection.NorthWest;
            case HexDirection.SouthWest:
                return HexDirection.NorthEast;
            case HexDirection.West:
                return HexDirection.East;
            case HexDirection.NorthWest:
                return HexDirection.SouthEast;
        }

        return HexDirection.SouthWest;
    }

    /// <summary>
    /// Gets the target direction between two tiles based off their position in the grid.
    /// </summary>
    public static HexDirection GetTargetDirection( BattleHex tile1, BattleHex tile2 )
    {
        HexDirection direction;

        if ( tile1.Y == tile2.Y )
        {
            if ( tile1.X > tile2.X )
            {
                direction = HexDirection.East;
            }
            else
            {
                direction = HexDirection.West;
            }
        }
        else if ( tile1.Y > tile2.Y )
        {
            if ( tile1.X > tile2.X || tile1.X == tile2.X + ( 1 - tile1.Y % 2 ) )
            {
                direction = HexDirection.SouthEast;
            }
            else
            {
                direction = HexDirection.SouthWest;
            }
        }
        else if ( tile1.X > tile2.X || tile1.X == tile2.X + ( 1 - tile1.Y % 2 ) )
        {
            direction = HexDirection.NorthEast;
        }
        else
        {
            direction = HexDirection.NorthWest;
        }

        return direction;
    }

    /// <summary>
    /// Gets the target degrees of the direction between two tiles.
    /// </summary>
    public static float GetTargetDegrees( BattleHex tile1, BattleHex tile2 )
    {
        HexDirection direction = GetTargetDirection( tile1, tile2 );

        return GetTargetDegrees( direction );
    }

    /// <summary>
    /// Gets the target degrees of the provided direction.
    /// </summary>
    public static float GetTargetDegrees( HexDirection direction )
    {
        switch ( direction )
        {
            case HexDirection.NorthEast:
                return 210f;
            case HexDirection.East:
                return 270f;
            case HexDirection.SouthEast:
                return 330f;
            case HexDirection.SouthWest:
                return 30f;
            case HexDirection.West:
                return 90f;
            case HexDirection.NorthWest:
                return 150f;
        }

        return 0f;
    }

    /// <summary>
    /// Gets the direction of the provided degrees. Expects the direction to be a 60 degree rotation starting at 30 degrees.
    /// </summary>
    public static HexDirection GetRotationDirection( float degrees )
    {
        if ( degrees == 210f ) return HexDirection.NorthEast;
        if ( degrees == 270f ) return HexDirection.East;
        if ( degrees == 330f ) return HexDirection.SouthEast;
        if ( degrees == 30f ) return HexDirection.SouthWest;
        if ( degrees == 90f ) return HexDirection.West;

        return HexDirection.NorthWest;
    }

    /// <summary>
    /// Gets the direction between two directions.
    /// </summary>
    public static HexDirection DirectionBetween( HexDirection left, HexDirection right )
    {
        if ( left == right ) return left;

        if ( left == HexDirection.East )
        {
            if ( right == HexDirection.SouthEast )
            {
                return HexDirection.SouthWest;
            }
            else if ( right == HexDirection.NorthEast )
            {
                return HexDirection.NorthWest;
            }
        }

        if ( left == HexDirection.SouthEast )
        {
            if ( right == HexDirection.SouthWest )
            {
                return HexDirection.West;
            }
            else if ( right == HexDirection.East )
            {
                return HexDirection.NorthEast;
            }
        }

        if ( left == HexDirection.SouthWest )
        {
            if ( right == HexDirection.West )
            {
                return HexDirection.NorthWest;
            }
            else if ( right == HexDirection.SouthEast )
            {
                return HexDirection.East;
            }
        }

        if ( left == HexDirection.West )
        {
            if ( right == HexDirection.NorthWest )
            {
                return HexDirection.NorthEast;
            }
            else if ( right == HexDirection.SouthWest )
            {
                return HexDirection.SouthEast;
            }
        }

        if ( left == HexDirection.NorthWest )
        {
            if ( right == HexDirection.NorthEast )
            {
                return HexDirection.East;
            }
            else if ( right == HexDirection.West )
            {
                return HexDirection.SouthWest;
            }
        }

        if ( left == HexDirection.NorthEast )
        {
            if ( right == HexDirection.East )
            {
                return HexDirection.SouthEast;
            }
            else if ( right == HexDirection.NorthWest )
            {
                return HexDirection.West;
            }
        }

        return right;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
