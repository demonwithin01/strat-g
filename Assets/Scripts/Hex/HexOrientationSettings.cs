using UnityEngine;

public class HexOrientationSettings
{

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Class Members

    private static float sqrt3 = Mathf.Sqrt( 3 );

    private readonly float _f0;
    private readonly float _f1;
    private readonly float _f2;
    private readonly float _f3;

    private readonly float _b0;
    private readonly float _b1;
    private readonly float _b2;
    private readonly float _b3;
    
    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Constructor & Intialisation

    private HexOrientationSettings( HexOrientation orientation, IMap map, float f0, float f1, float f2, float f3, float b0, float b1, float b2, float b3, float startAngle )
    {
        this.Orientation = orientation;
        this.Map = map;
        this.StartAngle = startAngle;

        this._f0 = f0;
        this._f1 = f1;
        this._f2 = f2;
        this._f3 = f3;

        this._b0 = b0;
        this._b1 = b1;
        this._b2 = b2;
        this._b3 = b3;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Static Methods

    /// <summary>
    /// Creates settings for a specific orientation.
    /// </summary>
    /// <param name="orientation">The Hex Orientation to create the settings for.</param>
    /// <param name="map">The map that these settings should be attached to.</param>
    public static HexOrientationSettings Settings( HexOrientation orientation, IMap map )
    {
        if ( orientation == HexOrientation.VerticalPoint )
        {
            return new HexOrientationSettings( orientation, map, sqrt3, sqrt3 / 2, 0, 3f / 2, sqrt3 / 3, -1f / 3, 0, 2f / 3, 0.5f );
        }

        return new HexOrientationSettings( orientation, map, 3f / 2, 0, sqrt3 / 2, sqrt3, 2f / 3, 0, -1f / 3, sqrt3 / 3, 0f );
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Public Methods
        

    public Vector3 AxialToWorldPosition( AxialPoint axial )
    {
        if ( this.Orientation == HexOrientation.VerticalPoint )
        {
            return new Vector3()
            {
                x = axial.Q * this.Map.Size.x * Constants.Root3 + ( axial.R * Constants.Root3 / 2f ),
                z = axial.R * this.Map.Size.y * 1.5f
            };
        }

        return new Vector3()
        {
            x = axial.Q * this.Map.Size.x * 1.5f,
            z = axial.R * this.Map.Size.y * Constants.Root3 + ( axial.Q * Constants.Root3 / 2f )
        };
    }
    
    public Vector3 MapSize()
    {
        float groundSizeX;
        float groundSizeZ;

        if ( this.Orientation == HexOrientation.VerticalPoint )
        {
            groundSizeX = this.Map.MapRadius * 2f * Constants.Root3 * this.Map.Size.x + Constants.Root3;
            groundSizeZ = this.Map.MapRadius * 2f * Constants.Root3 * this.Map.Size.y;
        }
        else
        {
            groundSizeX = this.Map.MapRadius * 2f * Constants.Root3 * this.Map.Size.x;
            groundSizeZ = this.Map.MapRadius * 2f * Constants.Root3 * this.Map.Size.y + Constants.Root3;
        }

        return new Vector3( groundSizeX, 0.1f, groundSizeZ );
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the orientation for the hex.
    /// </summary>
    public HexOrientation Orientation { get; private set; }

    /// <summary>
    /// Gets the map that these settings are attached to.
    /// </summary>
    public IMap Map { get; private set; }

    /// <summary>
    /// Gets the start angle for the hex.
    /// </summary>
    public float StartAngle { get; private set; }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Events

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

}
