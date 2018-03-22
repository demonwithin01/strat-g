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

        RaycastHit[] allHits = Physics.RaycastAll( ray, 200f );

        if ( allHits.Length > 0 )
        {
            for ( int i = 0 ; i < allHits.Length && hit == null ; i++ )
            {
                BattleHex newHit = allHits[ i ].transform.gameObject.GetComponentInParent<BattleHex>();

                if ( newHit != null )
                {
                    hit = allHits[ i ];

                    if ( newHit.HasUnit && newHit.Unit is AIControlledUnit )
                    {
                        foundUnit = true;

                        if ( _attackState == false )
                        {
                            _attackState = true;

                            Cursor.SetCursor( _attackCursor, Vector2.zero, CursorMode.Auto );
                        }

                        BattleHex closestHex = null;
                        float closestDistance = float.MaxValue;
                        foreach ( var hex in newHit.Neighbours )
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
                            toAttack = newHit.Unit;

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
                    else if ( _currentHit != newHit )
                    {
                        if ( _currentHit == null )
                        {
                            _currentHit = newHit;
                            _currentHit.MouseEnter();
                        }
                        else
                        {
                            _currentHit.MouseLeave();
                            _currentHit = newHit;
                            _currentHit.MouseEnter();
                        }
                    }
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

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

}
