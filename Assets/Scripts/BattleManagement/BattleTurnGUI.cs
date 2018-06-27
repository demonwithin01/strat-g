using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the Battle Turn Manager GUI.
/// </summary>
public class BattleTurnGUI
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds a reference to the turn manager.
    /// </summary>
    private BattleTurnManager _turnManager;

    /// <summary>
    /// Gets the panel responsible for rendering the turn UI elements.
    /// </summary>
    internal readonly GameObject turnPanel;

    /// <summary>
    /// Gets the gutter between buttons.
    /// </summary>
    internal readonly float gutter = 10f;

    /// <summary>
    /// Gets the width of the buttons.
    /// </summary>
    internal readonly float width = 100f;

    /// <summary>
    /// Gets the calculated height of the panel.
    /// </summary>
    internal readonly float height;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates a new Battle Turn GUI manager.
    /// </summary>
    /// <param name="turnPanel">The UI panel for rendering the UI elements to.</param>
    /// <param name="turnManager">The turn manager.</param>
    public BattleTurnGUI( GameObject turnPanel, BattleTurnManager turnManager )
    {
        this.turnPanel = turnPanel;

        this._turnManager = turnManager;

        List<BattleUnit> livingUnits = Object.FindObjectsOfType<BattleUnit>().ToList();

        this.height = ( this.turnPanel.GetComponent<RectTransform>().sizeDelta.y - GutterX2 );

        for ( int i = 0 ; i < livingUnits.Count ; i++ )
        {
            new UnitTurnGUI( this._turnManager, this, livingUnits[ i ], i );
        }

        this._turnManager.TurnOrderChanged += UpdateUIPositions;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Performs update actions for the GUI.
    /// </summary>
    public void Update()
    {

    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods
        
    /// <summary>
    /// Updates the positions of the current turn orders.
    /// </summary>
    /// <param name="turnOrder">The current turn order.</param>
    /// <param name="waitTurnOrder">The wait turn order.</param>
    private void UpdateUIPositions( Queue<Turn> turnOrder, Queue<Turn> waitTurnOrder )
    {
        int index = 0;
        bool isInteractible = true;
        foreach ( Turn turn in turnOrder )
        {
            turn.Unit.UIButton.SetPosition( new Vector3( width / 2f + gutter + ( ( width + gutter ) * index++ ), height / 2f + gutter ), isInteractible );
            isInteractible = false;
        }

        foreach ( Turn turn in waitTurnOrder )
        {
            turn.Unit.UIButton.SetPosition( new Vector3( width / 2f + gutter + ( ( width + gutter ) * index++ ), height / 2f + gutter ), isInteractible );
            isInteractible = false;
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the gutter amount x2.
    /// </summary>
    internal float GutterX2 { get { return gutter * 2f; } }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
