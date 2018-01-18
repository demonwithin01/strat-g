using System;
using System.Collections.Generic;
using DEnt.Language;

public partial class LanguageSettings : IDisposable
{
    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    /// <summary>
    /// Holds the reference to the current language settings.
    /// </summary>
    private static LanguageSettings _current;

    /// <summary>
    /// Holds a list of all language settings for ease of developer use.
    /// </summary>
    private List<ILanguageSettings> _allSettings;

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates all instances of the language settings.
    /// </summary>
    private LanguageSettings()
    {
        this.CreateSettings();
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Static Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Reloads all language settings that have already been loaded into memory.
    /// </summary>
    public void ReloadLanguageSettings()
    {
        this.Dispose();

        this.CreateSettings();
    }

    /// <summary>
    /// Disposes of all instances.
    /// </summary>
    public void Dispose()
    {
        for ( int i = 0 ; i < this._allSettings.Count ; i++ )
        {
            this._allSettings[ i ].Dispose();

            this._allSettings[ i ] = null;
        }

        this._allSettings = new List<ILanguageSettings>();
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    /// <summary>
    /// Creates all the settings for the current settings.
    /// </summary>
    private void CreateSettings()
    {
        this.Language = GlobalSettings.CurrentLanguage;

        this._allSettings = new List<ILanguageSettings>();

        this.Stats = Create<StatLanguageSettings>();
        this.Units = Create<UnitLanguageSettings>();
    }

    /// <summary>
    /// Creates a new instance of the settings and adds them to the settings list.
    /// </summary>
    /// <typeparam name="T">The type to create the settings for.</typeparam>
    private T Create<T>() where T : ILanguageSettings, new()
    {
        T instance = new T();

        this._allSettings.Add( instance );

        return instance;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the language details that these settings are associated with.
    /// </summary>
    public LanguageDetails Language { get; private set; }

    #region Settings

    /// <summary>
    /// Gets the language settings for stats.
    /// </summary>
    public StatLanguageSettings Stats { get; private set; }

    /// <summary>
    /// Gets the language settings for unitss.
    /// </summary>
    public UnitLanguageSettings Units { get; private set; }

    #endregion

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Static Properties

    /// <summary>
    /// Gets the current language settings.
    /// </summary>
    public static LanguageSettings Current
    {
        get
        {
            return ( _current ?? ( _current = new LanguageSettings() ) );
        }
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}