using System;
using System.Collections.Generic;

namespace DEnt.Language
{
    public abstract class LanguageSettingsBase<T> : ILanguageSettings, IDisposable where T : class, IDisposable, new()
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        /// <summary>
        /// The name of the language settings folder.
        /// </summary>
        private const string LANGUAGE_SETTINGS_FOLDER = "Assets/Language/";

        /// <summary>
        /// Holds the name of the folder for the current settings.
        /// </summary>
        private string _folderName;

        /// <summary>
        /// Holds a list of all settings for 
        /// </summary>
        protected List<LanguageSettingsDetails<T>> _allSettings;

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates the required instances for the language settings.
        /// </summary>
        /// <param name="folder">The folder to load the settings from.</param>
        protected LanguageSettingsBase( string folder )
        {
            this._allSettings = new List<LanguageSettingsDetails<T>>();

            this._folderName = "/" + folder + "/";
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        /// <summary>
        /// For disposing of the settings.
        /// </summary>
        public virtual void Dispose()
        {
            foreach ( LanguageSettingsDetails<T> settings in this._allSettings )
            {
                settings.Dispose();
            }

            this._allSettings = null;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Protected Methods

        /// <summary>
        /// Creates new stat settings.
        /// </summary>
        /// <param name="filename">The filename to load the settings for.</param>
        protected LanguageSettingsDetails<T> Create( string filename )
        {
            string fileLocation = LANGUAGE_SETTINGS_FOLDER
                                + GlobalSettings.CurrentLanguage.FolderName
                                + _folderName // Folder location
                                + filename + ".ls"; // .ls is the Language Settings file format.

            LanguageSettingsDetails<T> settings = new LanguageSettingsDetails<T>( fileLocation );

            this._allSettings.Add( settings );

            return settings;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods
            
        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}
