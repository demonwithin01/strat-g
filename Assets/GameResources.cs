using UnityEngine;

/// <summary>
/// Provides shortcuts to global game resource items.
/// </summary>
public static class GameResources
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Loads all global game resources.
    /// </summary>
    static GameResources()
    {
        LoadFonts();
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Loads all the common game fonts.
    /// </summary>
    private static void LoadFonts()
    {
        GameFont = Resources.GetBuiltinResource<Font>( "Arial.ttf" );
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the main font to use throughout the game.
    /// </summary>
    public static Font GameFont { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
