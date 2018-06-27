using System;
using DEnt.Language;

/// <summary>
/// Maintains global settings in memory allow a single access point for various settings of the game.
/// </summary>
public static class GlobalSettings
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds the current difficulty for the game.
    /// </summary>
    private static Difficulty _gameDifficulty;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Initialises with default settings.
    /// </summary>
    static GlobalSettings()
    {
        CurrentLanguage = new LanguageDetails( SupportedLanguage.English_US );

        //TODO: Set up loading from files.
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Changes the current language.
    /// </summary>
    /// <param name="language"></param>
    public static void ChangeLanguage( SupportedLanguage language )
    {
        if ( CurrentLanguage == null || CurrentLanguage.Language != language )
        {
            CurrentLanguage = new LanguageDetails( language );

            LanguageSettings.Current.ReloadLanguageSettings();
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the current language.
    /// </summary>
    public static LanguageDetails CurrentLanguage { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Derived Properties

    /// <summary>
    /// Gets/Sets the difficulty level for the game.
    /// </summary>
    public static Difficulty GameDifficulty
    {
        get
        {
            return _gameDifficulty;
        }
        set
        {
            if ( value != _gameDifficulty )
            {
                _gameDifficulty = value;

                if ( DifficultyChanged != null )
                {
                    DifficultyChanged.Invoke( _gameDifficulty );
                }
            }
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Events

    /// <summary>
    /// The event which is raised when the difficulty is changed.
    /// </summary>
    public static event Action<Difficulty> DifficultyChanged;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}