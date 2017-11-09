using System.Collections.Generic;
using UnityEngine;
using DEnt;

public class BattleHex : MonoBehaviour
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

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Construction

    /// <summary>
    /// Configures this hex tile position, colour, parent, etc.
    /// </summary>
    /// <param name="x">The x index of this tile</param>
    /// <param name="y">The y index of this tile</param>
    /// <param name="gridSize">The size of the grid</param>
    /// <param name="length">The length of the tile</param>
    /// <param name="parent">The parent to attach this tile to</param>
    public void Configure( int x, int y, Point2D gridSize, float length, GameObject parent )
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
    
    }

    /// <summary>
    /// Unity Update loop.
    /// </summary>
    void Update ()
    {
        
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public void MouseEnter() // TEMP
    {
        _color.a = 1f;
        _renderer.material.color = _color;
    }

    public void MouseLeave() // TEMP
    {
        _color.a = 0.2f;
        _renderer.material.color = _color;
    }

    public void SetColour( Color color ) // TEMP
    {
        _color.r = color.r;
        _color.g = color.g;
        _color.b = color.b;

        _renderer.material.color = _color;
    }

    public void ResetColour() // TEMP
    {
        _color.r = 1f;
        _color.g = 1f;
        _color.b = 1f;

        _renderer.material.color = _color;
    }

    /// <summary>
    /// Finds all the tiles that are considered to be neighbours of the current tile.
    /// </summary>
    /// <param name="hexes">A dictionary of all the hex tiles</param>
    public void FindNeighbours( Dictionary<Point2D, BattleHex> hexes )
    {
        List<Point2D> neighbourShift = new List<Point2D>();
        neighbourShift.Add( new Point2D( 0, -1 ) );
        neighbourShift.Add( new Point2D( 1, -1 ) );
        neighbourShift.Add( new Point2D( 0, 1 ) );
        neighbourShift.Add( new Point2D( 1, 1 ) );

        if ( Y % 2 == 0 )
        {
            neighbourShift.Add( new Point2D( -1, 0 ) );
            neighbourShift.Add( new Point2D( 1, 0 ) );
        }
        else
        {
            neighbourShift.Add( new Point2D( 1, 0 ) );
            neighbourShift.Add( new Point2D( -1, 0 ) );
        }

        Neighbours = new List<BattleHex>();
        
        foreach ( Point2D shift in neighbourShift )
        {
            int x = X + shift.X;
            int y = Y + shift.Y;
            int xOffset = ( Y % 2 == 0 && y % 2 == 1 ? 1 : 0 );
            x -= xOffset;

            Point2D point = new Point2D( x, y );
            if ( hexes.ContainsKey( point ) )
            {
                Neighbours.Add( hexes[ point ] );
            }
        }
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
        return _neighbourPositions[ direction ];
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
    /// Gets the world position of this hex tile.
    /// </summary>
    public Vector3 WorldPosition
    {
        get
        {
            return transform.position;
        }
    }

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
    /// The X location in the hex grid.
    /// </summary>
    public int X { get; private set; }

    /// <summary>
    /// The Y location in the hex grid.
    /// </summary>
    public int Y { get; private set; }

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
