using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hex : MonoBehaviour
{

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Class Members

    private HexOrientation _orientation;

    private List<Vector3> _corners;

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Constructor & Intialisation

    public void Configure( IMap map, Vector3 position, Material lineMaterial )
    {
        this.Map = map;

        this._orientation = map.OrientationSettings.Orientation;
        
        position.y = 0;
        this.transform.position = position;

        this._corners = new List<Vector3>();
        List<Vector3> lineCorners = new List<Vector3>();

        Dictionary<float, int> horizontal = new Dictionary<float, int>();
        Dictionary<float, int> vertical = new Dictionary<float, int>();
        
        for ( int i = 0 ; i < 6 ; i++ )
        {
            Vector2 offset = CornerOffset( i );
            Vector3 cornerWorldPosition = new Vector3( position.x + offset.x, position.y, position.z + offset.y );

            float offsetX = (float)Math.Round( offset.x, 4 ); // To counter floating point calculation errors...
            float offsetY = (float)Math.Round( offset.y, 4 ); // To counter floating point calculation errors...
            // Shouldn't be getting floating point calculation errors in C#... What are Unity doing???

            if ( horizontal.ContainsKey( offsetX ) )
            {
                horizontal[ offsetX ] += 1;
            }
            else
            {
                horizontal.Add( offsetX, 1 );
            }

            if ( vertical.ContainsKey( offsetY ) )
            {
                vertical[ offsetY ] += 1;
            }
            else
            {
                vertical.Add( offsetY, 1 );
            }

            this._corners.Add( cornerWorldPosition );
        }

        float? rectLeft = null;
        float? pointLeft = null;
        float? rectRight = null;
        float? pointRight = null;

        float? rectTop = null;
        float? pointTop = null;
        float? rectBottom = null;
        float? pointBottom = null;

        foreach ( KeyValuePair<float, int> kvp in horizontal )
        {
            if ( kvp.Key < 0 )
            {
                if ( kvp.Value > 1 )
                {
                    if ( rectLeft == null || rectLeft > kvp.Key + position.x )
                    {
                        rectLeft = kvp.Key + position.x;
                    }
                }
                else if ( pointLeft == null || pointLeft > kvp.Key + position.x )
                {
                    pointLeft = kvp.Key + position.x;
                }
            }
            else
            {
                if ( kvp.Value > 1 )
                {
                    if ( rectRight == null || rectRight < kvp.Key + position.x )
                    {
                        rectRight = kvp.Key + position.x;
                    }
                }
                else if ( pointRight == null || pointRight < kvp.Key + position.x )
                {
                    pointRight = kvp.Key + position.x;
                }
            }
        }

        foreach ( KeyValuePair<float, int> kvp in vertical )
        {
            if ( kvp.Key < 0 )
            {
                if ( kvp.Value > 1 )
                {
                    if ( rectBottom == null || rectBottom > kvp.Key + position.z )
                    {
                        rectBottom = kvp.Key + position.z;
                    }
                }
                else if ( pointBottom == null || pointBottom > kvp.Key + position.z )
                {
                    pointBottom = kvp.Key + position.z;
                }
            }
            else
            {
                if ( kvp.Value > 1 )
                {
                    if ( rectTop == null || rectTop < kvp.Key + position.z )
                    {
                        rectTop = kvp.Key + position.z;
                    }
                }
                else if ( pointTop == null || pointTop < kvp.Key + position.z )
                {
                    pointTop = kvp.Key + position.z;
                }
            }
        }

        this.RectLeft = rectLeft ?? pointLeft ?? 0f;
        this.PointLeft = pointLeft ?? rectLeft ?? 0f;
        this.RectRight = rectRight ?? pointRight ?? 0f;
        this.PointRight = pointRight ?? rectRight ?? 0f;
                         
        this.RectTop = rectTop ?? pointTop ?? 0f;
        this.PointTop = pointTop ?? rectTop ?? 0f;
        this.RectBottom = rectBottom ?? pointBottom ?? 0f;
        this.PointBottom = pointBottom ?? rectBottom ?? 0f;
        
        float cornerInset = 0.03f;

        foreach ( Vector3 corner in _corners )
        {
            Vector3 lineCorner = corner;

            if ( lineCorner.x < WorldPosition.x )
            {
                lineCorner.x += cornerInset;
            }
            else if ( lineCorner.x > WorldPosition.x )
            {
                lineCorner.x -= cornerInset;
            }

            if ( lineCorner.z < WorldPosition.z )
            {
                lineCorner.z += cornerInset;
            }
            else if ( lineCorner.z > WorldPosition.z )
            {
                lineCorner.z -= cornerInset;
            }

            lineCorner.x = (float)Math.Round( lineCorner.x, 2 );
            lineCorner.z = (float)Math.Round( lineCorner.z, 2 );

            lineCorners.Add( lineCorner );
        }

        lineCorners.Add( new Vector3( lineCorners[ 0 ].x, lineCorners[ 0 ].y, lineCorners[ 0 ].z ) );

        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = lineCorners.Count;
        lineRenderer.SetPositions( lineCorners.ToArray() );

        lineRenderer.material = lineMaterial;
        lineRenderer.loop = false;
        lineRenderer.allowOcclusionWhenDynamic = false;
        lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        lineRenderer.receiveShadows = false;
        lineRenderer.startWidth = 0.02f;
        lineRenderer.endWidth = 0.02f;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Gets whether the point is within the hex.
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool IsPointWithinHex( Vector3 point )
    {
        Vector3 position = this.WorldPosition;
        float top = this.RectTop;
        float bottom = this.RectBottom;
        float left = this.RectLeft;
        float right = this.RectRight;

        if ( this._orientation == HexOrientation.VerticalPoint )
        {
            if ( left > point.x || right < point.x )
            {
                return false;
            }

            if ( bottom <= point.z && top >= point.z )
            {
                return true;
            }

            float pointTop = this.PointTop;
            float pointBottom = this.PointBottom;
            
            float xNorm = point.x - position.x;
            
            if ( point.z <= pointTop && point.z > top )
            {
                float zTop = -pointTop + point.z;
                float my = zTop * Constants.Root3;

                if ( xNorm < 0f && xNorm > my )
                {
                    return true;
                }
                else if ( xNorm >= 0f && xNorm < -my )
                {
                    return true;
                }
            }
            else if ( point.z < bottom && point.z > pointBottom )
            {
                float zTop = point.z - pointBottom;
                float my = zTop * Constants.Root3;

                if ( xNorm >= 0f && xNorm < my )
                {
                    return true;
                }
                else if ( xNorm < 0f && xNorm > -my )
                {
                    return true;
                }
            }
        }
        else
        {
            if ( top < point.z || bottom > point.z )
            {
                return false;
            }

            if ( left <= point.x && right >= point.x )
            {
                return true;
            }

            float pointLeft = this.PointLeft;
            float pointRight = this.PointRight;

            float zNorm = point.z - position.z;

            if ( point.x >= pointLeft && point.x < left )
            {
                float xLeft = -pointLeft + point.x;
                float my = xLeft * Constants.Root3;

                if ( zNorm < 0f && zNorm > -my ) // bottom
                {
                    return true;
                }
                else if ( zNorm >= 0f && zNorm < my ) // top
                {
                    return true;
                }
            }
            else if ( point.x > right && point.x < pointRight )
            {
                float xLeft = point.x - pointRight;
                float my = xLeft * Constants.Root3;

                if ( zNorm >= 0f && zNorm < -my )
                {
                    return true;
                }
                if ( zNorm < 0f && zNorm > my )
                {
                    return true;
                }
            }
        }

        return false;
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Static Methods

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Calculates the corner offset.
    /// </summary>
    /// <param name="corner">The corner number.</param>
    private Vector2 CornerOffset( int corner )
    {
        Vector2 size = this.Map.Size;
        float angle = 2 * Mathf.PI * ( this.Map.OrientationSettings.StartAngle + corner ) / 6f;

        return new Vector2( size.x * Mathf.Cos( angle ), size.y * Mathf.Sin( angle ) );
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Properties

    public IMap Map { get; private set; }

    public float PointLeft { get; private set; }
    public float PointRight { get; private set; }
    public float PointTop { get; private set; }
    public float PointBottom { get; private set; }

    public float RectLeft { get; private set; }
    public float RectRight { get; private set; }
    public float RectTop { get; private set; }
    public float RectBottom { get; private set; }

    public int X { get; internal set; }
    public int Y { get; internal set; }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    public Vector3 WorldPosition
    {
        get
        {
            return this.transform.position;
        }
    }

    public List<Vector3> Corners
    {
        get
        {
            return this._corners;
        }
    }

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

    #region Events

    #endregion

    /* ----------------------------------------------------------------------------------------------------------------------------------------- */

}
