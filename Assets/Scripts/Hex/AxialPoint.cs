using UnityEngine;

public struct AxialPoint
{

    /* --------------------------------------------------------------------- */

    #region Editable Fields

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Construction

    public AxialPoint( float q, float r )
    {
        this.Q = q;
        this.R = r;
        this.S = -q - r;
    }

    #endregion

    /* --------------------------------------------------------------------- */

    #region Unity Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Public Methods

    public override string ToString()
    {
        return "{ Q: " + this.Q + ", R: " + this.R + ", S: " + this.S + " }";
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Protected Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Internal Methods

    #endregion

    /* --------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    public float Q { get; private set; }

    public float R { get; private set; }

    public float S { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
