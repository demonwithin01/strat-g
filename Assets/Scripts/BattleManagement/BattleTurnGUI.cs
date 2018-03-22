using UnityEngine;

public class BattleTurnGUI
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    internal GameObject turnPanel;

    internal float gutter = 5f;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public BattleTurnGUI( GameObject turnPanel )
    {
        this.turnPanel = turnPanel;

        new UnitTurnGUI( this );
        //Text text 

        //text.text = "Hello World";
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    public void Update()
    {

    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    internal float GutterX2 { get { return gutter * 2f; } }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
