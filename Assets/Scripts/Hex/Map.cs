using System.Collections.Generic;
using UnityEngine;

public class Map<THexType> : MonoBehaviour,
                             IMap
                             where THexType : Hex, new()
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

#pragma warning disable 0649
    [SerializeField]
    private GameObject _hexTemplate;

    [SerializeField]
    private Material _lineMaterial;

    [SerializeField]
    private HexOrientation _hexOrientation;

    [SerializeField]
    private Vector2 _size;

    [SerializeField]
    private int _mapRadius;

    [SerializeField]
    private BoxCollider _groundCollider;
#pragma warning restore 0649

    private List<THexType> _hexesAsList;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    internal Vector3 origin;

    internal THexType[][] hexes;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Construction

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Unity Start event handler.
    /// </summary>
    private void Start ()
	{
        
    }
	
    /// <summary>
    /// Unity Update loop.
    /// </summary>
	private void Update ()
	{
		
	}

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    protected void CreateMap()
    {
        this.origin = Vector3.zero;

        this.OrientationSettings = HexOrientationSettings.Settings( this._hexOrientation, this );

        int arraySize = this._mapRadius * 2 + 1;
        this.hexes = new THexType[ arraySize ][];
        this._hexesAsList = new List<THexType>();

        for ( int i = 0 ; i < arraySize ; i++ )
        {
            this.hexes[ i ] = new THexType[ arraySize ];
        }

        for ( int i = -this._mapRadius ; i <= this._mapRadius ; i++ )
        {
            int r1 = Mathf.Max( -this._mapRadius, -i - this._mapRadius );
            int r2 = Mathf.Min( this._mapRadius, -i + this._mapRadius );

            for ( int r = r1 ; r <= r2 ; r++ )
            {
                Vector3 position = this.OrientationSettings.AxialToWorldPosition( new AxialPoint( i, r ) );

                GameObject hex = (GameObject)Instantiate( this._hexTemplate );
                THexType hexComponent = hex.AddComponent<THexType>();
                hexComponent.Configure( this, position, _lineMaterial );

                hex.transform.SetParent( this.transform );

                int x = i + this._mapRadius;
                int y = r + this._mapRadius;

                hexComponent.X = x;
                hexComponent.Y = y;

                this.hexes[ x ][ y ] = hexComponent;
                
                this._hexesAsList.Add( hexComponent );

            }
        }

        this._groundCollider.size = this.OrientationSettings.MapSize();
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the orientation settings for the each hex.
    /// </summary>
    public HexOrientationSettings OrientationSettings { get; set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets the size of each grid hex.
    /// </summary>
    public Vector2 Size { get { return this._size; } }

    /// <summary>
    /// Gets the map radius in terms of hexes.
    /// </summary>
    public int MapRadius { get { return this._mapRadius; } }

    /// <summary>
    /// Gets all the hex tiles in the grid.
    /// </summary>
    public List<THexType> HexTiles { get { return this._hexesAsList; } }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
