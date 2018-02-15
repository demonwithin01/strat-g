using DEnt;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitPreview : MonoBehaviour
{


    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* --------------------------------------------------------------------- */

    #region Class Members

    private Unit _unit = null;

    private RectTransform _statsPanel;

    private Text _unitName;

    private Text _unitDescription;
        
    #endregion

    /* --------------------------------------------------------------------- */

    #region Construction
        
    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    private void Start()
    {
        this._unitName = transform.FindDescendant( "UnitName" ).GetComponent<Text>();
        this._unitDescription = transform.FindDescendant( "UnitDescription" ).GetComponent<Text>();
        this._statsPanel = (RectTransform)( transform.FindDescendant( "Stats" ) );


        Spearman spearman = GameObject.Find( "UnitPreviewModel" ).GetComponent<Spearman>();

        this.Unit = spearman;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    private void RebuildPanelForUnit()
    {
        this._statsPanel.transform.DetachAndDestroyChildren();

        if ( this._unit != null )
        {
            List<Stat> stats = this._unit.ValidStats();

            this._unitName.text = this._unit.Name;
            this._unitDescription.text = this._unit.Description;

            for ( int i = 0 ; i < stats.Count ; i++ )
            {
                CreateStatText( stats[ i ], i );
            }

            RectTransform contentTransform = this._statsPanel.transform.parent.GetComponent<RectTransform>();

            contentTransform.offsetMin = new Vector2( contentTransform.offsetMin.x, - ( ( stats.Count - 1 ) * 30 ) );
        }
        else
        {
            this._unitName.text = "<Unit Name>";
            this._unitDescription.text = "<Unit Description>";
        }
    }

    private void CreateStatText( Stat stat, int index )
    {
        GameObject obj = new GameObject( "Stat - " + stat.Name );

        obj.transform.SetParent( this._statsPanel.transform );

        float height = 30f;

        RectTransform trans = obj.AddComponent<RectTransform>();
        trans.pivot = Vector2.zero;
        trans.anchorMin = new Vector2( 0f, 1f );
        trans.anchorMax = new Vector2( 1f, 1f );
        trans.anchoredPosition = new Vector2( 30f, - height * ( 2 + index ) - 2f );
        trans.sizeDelta = new Vector2( trans.sizeDelta.x, height );
        
        //trans.he

        Text text = obj.AddComponent<Text>();

        text.font = this._unitName.font;
        text.text = stat.Name + ": " + stat.Value;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Properties

    #endregion

    /* --------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets/Sets the unit that is using this preview.
    /// </summary>
    public Unit Unit
    {
        get
        {
            return _unit;
        }
        set
        {
            this._unit = value;

            RebuildPanelForUnit();
        }
    }

    #endregion

    /* --------------------------------------------------------------------- */

}
