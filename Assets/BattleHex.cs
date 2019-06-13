using System.Collections.Generic;
using UnityEngine;
using DEnt;
using System;

/// <summary>
/// The object responsible for maintaining the Hex information in a battle scenario.
/// </summary>
public class BattleHex : Hex
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds a reference to the unit that is currently on this tile.
    /// </summary>
    private BattleUnit _unit;
    
    /// <summary>
    /// Holds a reference to the renderer.
    /// </summary>
    private Renderer _renderer;

    /// <summary>
    /// Holds the current color of the tile.
    /// </summary>
    private Color _color;
    
    /// <summary>
    /// Holds all the real world edge center points.
    /// </summary>
    private Dictionary<HexDirection, Vector3> _edgeCenters;

    /// <summary>
    /// Holds all the real world edge center points.
    /// </summary>
    private Dictionary<HexDirection, Vector3> _neighbourPositions;

    private List<Vector3> _lines;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Construction

    public BattleHex()
    {
        this.IsPassable = true;
    }

    internal void ConfigureEdges()
    {
        Vector3 worldPosition = transform.position;
        float halfLength = 1 / 2f;

        Vector3 eastOffset = Quaternion.Euler( 0f, 0f, 0f ) * Vector3.right * halfLength;
        Vector3 northEastOffset = Quaternion.Euler( 0f, 300f, 0f ) * Vector3.right * halfLength;
        Vector3 southEastOffset = Quaternion.Euler( 0f, 60f, 0f ) * Vector3.right * halfLength;
        Vector3 westOffset = Quaternion.Euler( 0f, 180f, 0f ) * Vector3.right * halfLength;
        Vector3 northWestOffset = Quaternion.Euler( 0f, 240f, 0f ) * Vector3.right * halfLength;
        Vector3 southWestOffset = Quaternion.Euler( 0f, 120f, 0f ) * Vector3.right * halfLength;

        _edgeCenters = new Dictionary<HexDirection, Vector3>();

        _edgeCenters.Add( HexDirection.NorthEast, worldPosition + northEastOffset );
        _edgeCenters.Add( HexDirection.East, worldPosition + eastOffset );
        _edgeCenters.Add( HexDirection.SouthEast, worldPosition + southEastOffset );
        _edgeCenters.Add( HexDirection.SouthWest, worldPosition + southWestOffset );
        _edgeCenters.Add( HexDirection.West, worldPosition + westOffset );
        _edgeCenters.Add( HexDirection.NorthWest, worldPosition + northWestOffset );
    }

    /// <summary>
    /// Configures this hex tile position, colour, parent, etc.
    /// </summary>
    /// <param name="x">The x index of this tile</param>
    /// <param name="y">The y index of this tile</param>
    /// <param name="gridSize">The size of the grid</param>
    /// <param name="length">The length of the tile</param>
    /// <param name="parent">The parent to attach this tile to</param>
    public void Configure( int x, int y, Point2D gridSize, float length, GameObject parent, Material lineMaterial )
    {
        X = x;
        Y = y;
        this.TileLength = length;

        _color = Colour.RGBA( 255, 255, 255, 0.2f ); ;
        _renderer = GetComponentInChildren<Renderer>();
        
        Vector3 initPos = new Vector3( -length * gridSize.X / 2f + length / 2, 0, gridSize.Y / 2f * length - length / 2 );

        float offset = ( y % 2 != 0 ? length / 2 : 0 );

        Vector3 worldPosition = new Vector3( initPos.x + offset + x * length, 0, initPos.z - y * length * 0.865f );

        transform.position = worldPosition;


        name = "hex_" + x + "_" + y;

        transform.SetParent( parent.transform );
        
        _renderer.material.color = _color;

        float halfLength = length / 2f;

        Vector3 eastOffset = Quaternion.Euler( 0f, 0f, 0f ) * Vector3.right * halfLength;
        Vector3 northEastOffset = Quaternion.Euler( 0f, 300f, 0f ) * Vector3.right * halfLength;
        Vector3 southEastOffset = Quaternion.Euler( 0f, 60f, 0f ) * Vector3.right * halfLength;
        Vector3 westOffset = Quaternion.Euler( 0f, 180f, 0f ) * Vector3.right * halfLength;
        Vector3 northWestOffset = Quaternion.Euler( 0f, 240f, 0f ) * Vector3.right * halfLength;
        Vector3 southWestOffset = Quaternion.Euler( 0f, 120f, 0f ) * Vector3.right * halfLength;

        _edgeCenters = new Dictionary<HexDirection, Vector3>();

        _edgeCenters.Add( HexDirection.NorthEast, worldPosition + northEastOffset );
        _edgeCenters.Add( HexDirection.East, worldPosition + eastOffset );
        _edgeCenters.Add( HexDirection.SouthEast, worldPosition + southEastOffset );
        _edgeCenters.Add( HexDirection.SouthWest, worldPosition + southWestOffset );
        _edgeCenters.Add( HexDirection.West, worldPosition + westOffset );
        _edgeCenters.Add( HexDirection.NorthWest, worldPosition + northWestOffset );

        _neighbourPositions = new Dictionary<HexDirection, Vector3>();

        _neighbourPositions.Add( HexDirection.NorthEast, worldPosition + 2 * northEastOffset );
        _neighbourPositions.Add( HexDirection.East, worldPosition + 2 * eastOffset );
        _neighbourPositions.Add( HexDirection.SouthEast, worldPosition + 2 * southEastOffset );
        _neighbourPositions.Add( HexDirection.SouthWest, worldPosition + 2 * southWestOffset );
        _neighbourPositions.Add( HexDirection.West, worldPosition + 2 * westOffset );
        _neighbourPositions.Add( HexDirection.NorthWest, worldPosition + 2 * northWestOffset );

        _lines = new List<Vector3>();
        _lines.Add( worldPosition + Quaternion.Euler( 0f, 0f, 0f ) * Vector3.forward * length * 0.577f );
        _lines.Add( worldPosition + Quaternion.Euler( 0f, 300f, 0f ) * Vector3.forward * length * 0.577f );
        _lines.Add( worldPosition + Quaternion.Euler( 0f, 240f, 0f ) * Vector3.forward * length * 0.577f );
        _lines.Add( worldPosition + Quaternion.Euler( 0f, 180f, 0f ) * Vector3.forward * length * 0.577f );
        _lines.Add( worldPosition + Quaternion.Euler( 0f, 120f, 0f ) * Vector3.forward * length * 0.577f );
        _lines.Add( worldPosition + Quaternion.Euler( 0f, 60f, 0f ) * Vector3.forward * length * 0.577f );

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = _lines.Count;
        lineRenderer.SetPositions( _lines.ToArray() );

        lineRenderer.material = lineMaterial;
        lineRenderer.loop = true;
        lineRenderer.allowOcclusionWhenDynamic = false;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;

        IsPassable = true;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Unity Awake event handler.
    /// </summary>
    void Start ()
    {
        this.IsPassable = true;
    }

    /// <summary>
    /// Unity Update loop.
    /// </summary>
    void Update ()
    {
        this.IsPassable = true;
        //for ( int i = 1 ; i < _lines.Count ; i++ )
        //{
        //    if ( name == "hex_0_0" )
        //    Debug.Log( _lines[ i - 1 ] );
        //    Debug.DrawLine( _lines[ i - 1 ], _lines[ i ] );
        //}

        //Debug.DrawLine( _lines[ 0 ], _lines[ _lines.Count - 1 ] );
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public void MouseEnter() // TEMP
    {
        //_color.a = 1f;
        //_renderer.material.color = _color;
    }

    public void MouseLeave() // TEMP
    {
        //_color.a = 0.2f;
        //_renderer.material.color = _color;

        LineRenderer line = this.GetComponent<LineRenderer>();

        for ( int i = 0 ; i < line.positionCount ; i++ )
        {
            Vector3 position = line.GetPosition( i );

            position.y = 0f;

            line.SetPosition( i, position );
        }

        line.widthMultiplier = 1f;
        line.sortingLayerID = SortingLayer.NameToID( "Default" );
    }

    public void SetColour( Color color ) // TEMP
    {
        _color.r = color.r;
        _color.g = color.g;
        _color.b = color.b;

        LineRenderer line = this.GetComponent<LineRenderer>();

        line.material.color = color;
        line.startColor = color;
        line.endColor = color;

        //_renderer.material.color = _color;
    }

    public void ResetColour() // TEMP
    {
        _color.r = 1f;
        _color.g = 1f;
        _color.b = 1f;

        LineRenderer line = this.GetComponent<LineRenderer>();

        line.material.color = Color.white;
        line.startColor = Color.white;
        line.endColor = Color.white;

        //_renderer.material.color = _color;
    }

    /// <summary>
    /// Finds all the tiles that are considered to be neighbours of the current tile.
    /// </summary>
    /// <param name="hexes">A dictionary of all the hex tiles</param>
    public void FindNeighbours( BattleHex[][] hexes )
    {
        List<Point2D> neighbourShift = new List<Point2D>();
        List<HexDirection> shiftDirections = new List<HexDirection>();

        neighbourShift.Add( new Point2D( 0, -1 ) );
        shiftDirections.Add( HexDirection.West );

        neighbourShift.Add( new Point2D( 1, -1 ) );
        shiftDirections.Add( HexDirection.NorthWest );

        neighbourShift.Add( new Point2D( 0, 1 ) );
        shiftDirections.Add( HexDirection.East );

        neighbourShift.Add( new Point2D( 1, 1 ) );
        shiftDirections.Add( HexDirection.NorthEast );

        if ( Y % 2 == 0 )
        {
            neighbourShift.Add( new Point2D( -1, 0 ) );
            shiftDirections.Add( HexDirection.SouthEast );

            neighbourShift.Add( new Point2D( 1, 0 ) );
            shiftDirections.Add( HexDirection.SouthWest );
        }
        else
        {
            neighbourShift.Add( new Point2D( 1, 0 ) );
            shiftDirections.Add( HexDirection.SouthEast );

            neighbourShift.Add( new Point2D( -1, 0 ) );
            shiftDirections.Add( HexDirection.SouthWest );
        }

        Neighbours = new List<BattleHex>();

        this._edgeCenters = new Dictionary<HexDirection, Vector3>();
        this._neighbourPositions = new Dictionary<HexDirection, Vector3>();

        if ( this.WorldPosition.z == 0f && this.WorldPosition.x == -6.928203f )
        {

        }

        foreach ( Point2D shift in neighbourShift )
        {
            int x = X + shift.X;
            int y = Y + shift.Y;
            int xOffset = ( Y % 2 == 0 && y % 2 == 1 ? 1 : 0 );
            //x -= xOffset;
            
            Point2D point = new Point2D( x, y );
            if ( x >= 0 && x < hexes.Length && hexes[ x ] != null && y >= 0 && y < hexes[ x ].Length && hexes[ x ][ y ] != null )
            {
                BattleHex neighbour = hexes[ x ][ y ];

                Neighbours.Add( neighbour );
                HexDirection direction = shiftDirections[ neighbourShift.IndexOf( shift ) ];
                _neighbourPositions.Add( direction, neighbour.WorldPosition );
                Vector3[] sharedCorners = new Vector3[ 2 ];
                int index = 0;

                for ( int i = 0 ; i < neighbour.Corners.Count && index < 2 ; i++ )
                {
                    for ( int j = 0 ; j < this.Corners.Count && index < 2 ; j++ )
                    {
                        if ( this.Corners[ j ] == neighbour.Corners[ i ] )
                        {
                            sharedCorners[ index ] = neighbour.Corners[ i ];

                            ++index;
                        }
                    }
                }

                this._edgeCenters.Add( direction, Vector3.Lerp( sharedCorners[ 0 ], sharedCorners[ 1 ], 0.5f ) );
            }
        }
    }

    public HexDirection DirectionOfNeighbour( BattleHex hex )
    {
        Vector3 currentPosition = this.WorldPosition;
        Vector3 neighbourPosition = hex.WorldPosition;

        if ( currentPosition.z == neighbourPosition.z )
        {
            if ( currentPosition.x < neighbourPosition.x )
            {
                return HexDirection.East;
            }
            else
            {
                return HexDirection.West;
            }
        }
        else if ( currentPosition.z < neighbourPosition.z ) //
        {
            if ( currentPosition.x < neighbourPosition.x )
            {
                return HexDirection.SouthEast;
            }
            else if ( currentPosition.x > neighbourPosition.x )
            {
                return HexDirection.SouthWest;
            }
            else
            {
                return HexDirection.East;
            }
        }
        else // North
        {
            if ( currentPosition.x < neighbourPosition.x )
            {
                return HexDirection.NorthEast;
            }
            else if ( currentPosition.x > neighbourPosition.x )
            {
                return HexDirection.NorthWest;
            }
            else
            {
                return HexDirection.West;
            }
        }
    }

    /// <summary>
    /// Finds all the tiles that are considered to be neighbours of the current tile.
    /// </summary>
    /// <param name="hexes">A dictionary of all the hex tiles</param>
    public void FindNeighbours( List<BattleHex> hexes )
    {
        this.Neighbours = new List<BattleHex>();

        this._edgeCenters = new Dictionary<HexDirection, Vector3>();
        this._neighbourPositions = new Dictionary<HexDirection, Vector3>();

        float root3 = MathHelpers.RoundTo( Constants.Root3, 4 );

        if ( hexes.IndexOf( this ) == 35 )
        {

        }
        
        foreach ( BattleHex hex in hexes )
        {
            float distance = MathHelpers.RoundTo( Vector3.Distance( this.WorldPosition, hex.WorldPosition ), 4 );
            
            if ( this != hex && distance == root3 )
            {
                HexDirection direction = DirectionOfNeighbour( hex );

                this.Neighbours.Add( hex );
                if ( _neighbourPositions.ContainsKey( direction ) )
                {
                    Debug.Log( "Continue" );
                    continue;
                }

                _neighbourPositions.Add( direction, hex.WorldPosition );
                Vector3[] sharedCorners = new Vector3[ 2 ];
                int index = 0;

                for ( int i = 0 ; i < hex.Corners.Count && index < 2 ; i++ )
                {
                    for ( int j = 0 ; j < this.Corners.Count && index < 2 ; j++ )
                    {
                        if ( this.Corners[ j ] == hex.Corners[ i ] )
                        {
                            sharedCorners[ index ] = hex.Corners[ i ];

                            ++index;
                        }
                    }
                }

                this._edgeCenters.Add( direction, Vector3.Lerp( sharedCorners[ 0 ], sharedCorners[ 1 ], 0.5f ) );
            }
        }

    //    foreach ( Point2D shift in neighbourShift )
    //    {
    //        int x = X + shift.X;
    //        int y = Y + shift.Y;
    //        int xOffset = ( Y % 2 == 0 && y % 2 == 1 ? 1 : 0 );
    //        //x -= xOffset;

    //        Point2D point = new Point2D( x, y );
    //        if ( x >= 0 && x < hexes.Length && hexes[ x ] != null && y >= 0 && y < hexes[ x ].Length && hexes[ x ][ y ] != null )
    //        {
    //            BattleHex neighbour = hexes[ x ][ y ];

    //            Neighbours.Add( neighbour );
    //            HexDirection direction = shiftDirections[ neighbourShift.IndexOf( shift ) ];
    //            _neighbourPositions.Add( direction, neighbour.WorldPosition );
    //            Vector3[] sharedCorners = new Vector3[ 2 ];
    //            int index = 0;

    //            for ( int i = 0 ; i < neighbour.Corners.Count && index < 2 ; i++ )
    //            {
    //                for ( int j = 0 ; j < this.Corners.Count && index < 2 ; j++ )
    //                {
    //                    if ( this.Corners[ j ] == neighbour.Corners[ i ] )
    //                    {
    //                        sharedCorners[ index ] = neighbour.Corners[ i ];

    //                        ++index;
    //                    }
    //                }
    //            }

    //            this._edgeCenters.Add( direction, Vector3.Lerp( sharedCorners[ 0 ], sharedCorners[ 1 ], 0.5f ) );
    //        }
    //    }
    }

    /// <summary>
    /// Gets the center point of the edge of the tile for the specified direction.
    /// </summary>
    /// <param name="direction">The direction to get the edge of the tile for.</param>
    /// <returns>The real world co-ordinates of the end of the tile.</returns>
    public Vector3 GetEdge( HexDirection direction )
    {
        return _edgeCenters[ direction ];
    }

    /// <summary>
    /// Gets the position of one of the neighbour hex
    /// </summary>
    /// <param name="direction">The direction to get the neighbour position.</param>
    /// <returns>The real world co-ordinates of the center of the neighbouring tile.</returns>
    public Vector3 GetNeighbourPosition( HexDirection direction )
    {
        try
        {
            return _neighbourPositions[ direction ];
        }
        catch( Exception ex )
        {
            return Vector3.zero;
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    /// <summary>
    /// Gets whether or not one of the neighbouring tiles has a unit on it.
    /// </summary>
    internal bool CheckIfNeighbourHasUnit()
    {
        foreach ( BattleHex neighbour in Neighbours )
        {
            if ( neighbour.HasUnit )
            {
                return true;
            }
        }

        return false;
    }
    
    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties
        
    /// <summary>
    /// Gets the length of the tile.
    /// </summary>
    public float TileLength { get; private set; }

    /// <summary>
    /// Gets whether or not this tile can be travelled to.
    /// </summary>
    public bool IsPassable { get; private set; }

    /// <summary>
    /// Gets whether or not there is currently a unit standing on this tile.
    /// </summary>
    public bool HasUnit { get { return ( Unit != null ); } }
    
    /// <summary>
    /// Gets whether or not this tile has a neighbour with a unit on it.
    /// </summary>
    public bool DoesNeighbourHaveUnit { get; internal set; }

    /// <summary>
    /// Gets a list of hex tiles which are considered to be neighbours of this tile.
    /// </summary>
    public List<BattleHex> Neighbours { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets the unit that is currently attached to this tile.
    /// </summary>
    public BattleUnit Unit
    {
        get
        {
            return _unit;
        }
        internal set
        {
            _unit = value;

            if ( value == null )
            {
                foreach ( BattleHex neighbour in Neighbours )
                {
                    neighbour.DoesNeighbourHaveUnit = neighbour.CheckIfNeighbourHasUnit();
                }
            }
            else
            {
                foreach ( BattleHex neighbour in Neighbours )
                {
                    neighbour.DoesNeighbourHaveUnit = true;
                }
            }
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
