using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitTurnGUI
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    private GameObject _unitTurnGui;

    private Text _label;

    private Button _button;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    public UnitTurnGUI( BattleTurnGUI battleTurnGui )
    {
        _unitTurnGui = new GameObject();
        _unitTurnGui.transform.SetParent( battleTurnGui.turnPanel.transform );
        RectTransform parentTransform = battleTurnGui.turnPanel.GetComponent<RectTransform>();

        _label = _unitTurnGui.AddComponent<Text>();
        _label.font = GameResources.GameFont;
        _label.text = "Hello World";
        _label.rectTransform.anchorMin = new Vector2( 0f, 0f );
        _label.rectTransform.anchorMax = new Vector2( 0f, 0f );
        _label.rectTransform.pivot = new Vector2( 0.5f, 0.5f );
        _label.rectTransform.sizeDelta = new Vector2( 100f, parentTransform.sizeDelta.y - battleTurnGui.GutterX2 );
        _label.rectTransform.position = new Vector3( 50f + battleTurnGui.gutter, parentTransform.sizeDelta.y / 2f, 0f );
        _label.alignment = TextAnchor.LowerCenter;


        _button = _unitTurnGui.AddComponent<Button>();
        _button.targetGraphic = _label;
        _button.onClick.AddListener( ButtonClicked );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    private void ButtonClicked()
    {
        Debug.Log( "Button Clicked" );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
