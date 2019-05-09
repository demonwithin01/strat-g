using System.Collections.Generic;
using DEnt;
using UnityEngine;

public class BattleMouse : MonoBehaviour
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

#pragma warning disable 0649
    [SerializeField]
    private Texture2D _regularCursor;

    [SerializeField]
    private Texture2D _attackCursor;
#pragma warning restore 0649

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds a reference to the battle grid.
    /// </summary>
    private BattleGrid _battleGrid = null;

    private BattleHex _currentHit = null;

    private bool _attackState = false;

    /// <summary>
    /// Holds whether or not the mouse input can be accepted.
    /// </summary>
    private bool _isLocked = false;

    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction

    public BattleMouse()
    {

    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    /// <summary>
    /// Unity Start event handler.
    /// </summary>
    void Start ()
    {
        _battleGrid = transform.GetComponent<BattleGrid>();

        Cursor.SetCursor( _regularCursor, Vector2.zero, CursorMode.Auto );
    }

    /// <summary>
    /// Unity Update loop.
    /// </summary>
    void Update ()
    {
        RaycastHit? hit = null;
        bool foundUnit = false;
        BattleUnit toAttack = null;

        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        //PixelMe( Camera.main.ScreenToWorldPoint( Input.mousePosition ) );

        RaycastHit[] allHits = Physics.RaycastAll( ray, 200f );

        if ( allHits.Length > 0 )
        {
            for ( int i = 0 ; i < allHits.Length && hit == null ; i++ )
            {
                Vector3 testPoint = allHits[ 0 ].point;

                BattleHex hexHit = FindHexForPoint( testPoint );

                //if ( hexHit != null && hexHit != _currentHit )
                //{
                //    //decolour = false;

                //    DeColourHex();

                //    SetLineColour( hexHit, Color.red, false );
                //    _currentHit = hexHit;
                //}

                //PixelMe( allHits[ i ].point );
                //BattleHex newHit = allHits[ i ].transform.gameObject.GetComponentInParent<BattleHex>();

                if ( hexHit != null )
                {
                    hit = allHits[ i ];

                    if ( hexHit != _currentHit )
                    {
                        DeColourHex();

                        SetLineColour( hexHit, Color.red, false );
                    }

                    if ( hexHit.HasUnit && hexHit.Unit is AIControlledUnit )
                    {
                        foundUnit = true;

                        if ( _attackState == false )
                        {
                            _attackState = true;

                            Cursor.SetCursor( _attackCursor, Vector2.zero, CursorMode.Auto );
                        }

                        BattleHex closestHex = null;
                        float closestDistance = float.MaxValue;
                        foreach ( var hex in hexHit.Neighbours )
                        {
                            float distance = Vector3.Distance( hex.transform.position, hit.Value.point );

                            if ( distance < closestDistance )
                            {
                                closestDistance = distance;
                                closestHex = hex;
                            }
                        }

                        if ( closestHex != null )
                        {
                            toAttack = hexHit.Unit;

                            if ( _currentHit != closestHex )
                            {
                                if ( _currentHit == null )
                                {
                                    _currentHit = closestHex;
                                    _currentHit.MouseEnter();
                                }
                                else
                                {
                                    _currentHit.MouseLeave();
                                    _currentHit = closestHex;
                                    _currentHit.MouseEnter();
                                }
                            }
                        }
                        else if ( _currentHit != null )
                        {
                            _currentHit.MouseLeave();
                            _currentHit = null;
                        }
                    }
                    else if ( _currentHit != hexHit )
                    {
                        if ( _currentHit == null )
                        {
                            _currentHit = hexHit;
                            _currentHit.MouseEnter();
                        }
                        else
                        {
                            _currentHit.MouseLeave();
                            _currentHit = hexHit;
                            _currentHit.MouseEnter();
                        }
                    }
                }
                else
                {
                    DeColourHex();
                }
            }
        }
        
        if ( hit == null && _currentHit != null )
        {
            _currentHit.MouseLeave();
            _currentHit = null;
        }

        if ( !_isLocked && Input.GetMouseButtonDown( MouseButton.Right ) && _currentHit != null )
        {
            Path<BattleHex> path = null;

            if ( _battleGrid.CurrentUnit.CanTravelTo( _currentHit ) )
            {
                path = PathFinder.FindPath( _battleGrid.CurrentUnit.CurrentHexTile, _currentHit );
            }
            else if ( toAttack != null && _battleGrid.CurrentUnit.IsOnTile( _currentHit ) )
            {
                path = new Path<BattleHex>( _currentHit );
            }

            if ( path != null )
            {
                if ( toAttack != null )
                {
                    _battleGrid.CurrentUnit.TravelPathAndAttack( path.GetPath(), toAttack );
                }
                else
                {
                    _battleGrid.CurrentUnit.TravelPath( path.GetPath() );
                }

                Lock();
            }
        }
        
        if ( foundUnit == false && _attackState )
        {
            _attackState = false;
            Cursor.SetCursor( _regularCursor, Vector2.zero, CursorMode.Auto );
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Locks the mouse and prevents it from accepting input.
    /// </summary>
    public void Lock()
    {
        this._isLocked = true;
    }

    /// <summary>
    /// Unlocks the mouse and allows it to start accepting input.
    /// </summary>
    public void Unlock()
    {
        this._isLocked = false;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    private void PixelMe( Vector3 position )
    {
        Vector3 hex0 = new Vector3( -11, 0, 8 );
        Debug.Log( Vector3.Distance( position, hex0 ) );

        float x = ( Mathf.Sqrt( 3 ) / 3 * position.x - 1f / 3 * position.z );
        float z = ( 2f / 3 * position.z );
        float y = -x - z;

        int rx = Mathf.RoundToInt( x );
        int rz = Mathf.RoundToInt( z );
        int ry = Mathf.RoundToInt( y ); ;

        float xDiff = Mathf.Abs( rx - x );
        float zDiff = Mathf.Abs( rz - z );
        float yDiff = Mathf.Abs( ry - y );

        if ( xDiff > zDiff && xDiff > yDiff )
        {
            rx = -rz-ry;
        }
        else if ( yDiff > zDiff )
        {
            ry = -rx-rz;
        }
        else
        {
            rz = -rx-ry;
        }

        Debug.Log( rx + ", " + rz );

        Debug.Log( position.x + ", " + position.z + " => " + x + ", " + z );
    }

    private BattleHex FindHexForPoint( Vector3 point )
    {
        HexOrientationSettings orientationSettings = this._battleGrid.OrientationSettings;

        float minLength = 1.5f;
        float maxLength = minLength / 2f * Constants.Root3;

        List<BattleHex> testFurther = new List<BattleHex>();

        for ( int x = 0 ; x < this._battleGrid.hexes.Length ; x++ )
        {
            for ( int y = 0 ; y < this._battleGrid.hexes[ x ].Length ; y++ )
            {
                BattleHex testHex = this._battleGrid.hexes[ x ][ y ];

                if ( testHex == null )
                {
                    continue;
                }

                float distance = Vector3.Distance( testHex.WorldPosition, point );

                if ( distance <= maxLength )
                {
                    testFurther.Add( testHex );
                }
            }
        }

        foreach ( BattleHex hex in testFurther )
        {
            if ( hex.IsPointWithinHex( point ) )
            {
                return hex;
            }
        }

        return null;
    }

    private void DeColourHex()
    {
        SetLineColour( _currentHit, Color.white, true );

        _currentHit = null;
    }

    private void SetLineColour( Hex hex, Color color, bool returningToDefault )
    {
        if ( hex != null )
        {
            LineRenderer line = hex.GetComponent<LineRenderer>();

            line.material.color = color;

            for ( int i = 0 ; i < line.positionCount ; i++ )
            {
                Vector3 position = line.GetPosition( i );

                position.y = returningToDefault ? 0f : 0.01f;

                line.SetPosition( i, position );
            }

            if ( returningToDefault )
            {
                line.widthMultiplier = 1f;

                line.sortingLayerID = SortingLayer.NameToID( "Default" );
            }
            else
            {
                line.widthMultiplier = 4f;

                line.sortingLayerID = SortingLayer.NameToID( "Overlay" );
            }
            
            
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

}
