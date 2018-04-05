using DEnt;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitTurnGUI
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    private TurnManager _turnManager;

    private GameObject _unitTurnGui;

    private Text _label;

    private Button _button;

    private Image _background;

    private RectTransform _guiTransform;

    private BattleUnit _unit;

    private Turn _turn;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public UnitTurnGUI( TurnManager turnManager, BattleTurnGUI battleTurnGui, BattleUnit unit, int index )
    {
        this._turnManager = turnManager;
        this._unit = unit;
        this._unit.UIButton = this;

        this._unitTurnGui = new GameObject( "unitTurnUI" );
        this._unitTurnGui.transform.SetParent( battleTurnGui.turnPanel.transform );
        this._guiTransform = _unitTurnGui.AddComponent<RectTransform>();

        EventTrigger eventTrigger = _unitTurnGui.AddComponent<EventTrigger>();

        float width = battleTurnGui.width;
        float height = battleTurnGui.height;

        this._guiTransform.anchorMin = new Vector2( 0f, 0.5f );
        this._guiTransform.anchorMax = new Vector2( 0f, 0.5f );
        this._guiTransform.pivot = new Vector2( 0.5f, 0.5f );
        this._guiTransform.sizeDelta = new Vector2( width, height );
        this._guiTransform.position = new Vector3( width / 2f + battleTurnGui.gutter + ( ( width + battleTurnGui.gutter ) * index ), height / 2f + battleTurnGui.gutter, 0f );

        _background = _unitTurnGui.AddComponent<Image>();
        _background.color = this._unit.IsAIControlled ? Colour.RGB( 255, 0, 0 ) : Colour.RGB( 0, 255, 0 );

        GameObject textContainer = new GameObject( "textContainer" );
        textContainer.transform.SetParent( _unitTurnGui.transform );
        RectTransform labelRect = textContainer.AddComponent<RectTransform>();

        _label = textContainer.AddComponent<Text>();
        _label.font = GameResources.GameFont;
        _label.text = this._unit.Unit.Name;
        _label.alignment = TextAnchor.LowerCenter;

        labelRect.anchorMin = new Vector2( 0.5f, 0f );
        labelRect.anchorMax = new Vector2( 0.5f, 0f );
        labelRect.pivot = new Vector2( 0.5f, 0.5f );
        labelRect.position = new Vector3( 0f, height / 2f, 0f );
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;
        labelRect.anchoredPosition = new Vector3( 0f, height / 2f, 0f );
        labelRect.sizeDelta = new Vector2( width, height );

        _button = _unitTurnGui.AddComponent<Button>();
        _button.targetGraphic = _background;
        _button.onClick.AddListener( ButtonClicked );

        EventTrigger.Entry mouseOverTrigger = new EventTrigger.Entry();
        mouseOverTrigger.eventID = EventTriggerType.PointerEnter;
        mouseOverTrigger.callback.AddListener( MouseOver );

        EventTrigger.Entry mouseOutTrigger = new EventTrigger.Entry();
        mouseOutTrigger.eventID = EventTriggerType.PointerExit;
        mouseOutTrigger.callback.AddListener( MouseOut );

        eventTrigger.triggers.Add( mouseOverTrigger );
        eventTrigger.triggers.Add( mouseOutTrigger );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Sets the position of the element.
    /// </summary>
    /// <param name="position">The new position of the UI element.</param>
    public void SetPosition( Vector3 position )
    {
        this._guiTransform.position = position;
    }

    /// <summary>
    /// Sets the active state of the UI element.
    /// </summary>
    /// <param name="active"></param>
    public void SetActiveState( bool active )
    {
        this._guiTransform.gameObject.SetActive( active );
    }

    /// <summary>
    /// Sets the turn that the UI element is currently attached to.
    /// </summary>
    /// <param name="turn">The turn that the UI element is currently attached to.</param>
    public void SetTurn( Turn turn )
    {
        this._turn = turn;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// The event handler for when the mouse enters the button.
    /// </summary>
    /// <param name="eventData">The mouse event data.</param>
    private void MouseOver( BaseEventData eventData )
    {
        this._unit.OnUIMouseOver();
    }

    /// <summary>
    /// The event handler for when the mouse exits the button.
    /// </summary>
    /// <param name="eventData">The mouse event data.</param>
    private void MouseOut( BaseEventData eventData )
    {
        this._unit.OnUIMouseOut();
    }

    /// <summary>
    /// The event for when the button is clicked.
    /// </summary>
    private void ButtonClicked()
    {
        if ( this._turn.IsCurrentTurn )
        {
            this._turnManager.Wait();
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
