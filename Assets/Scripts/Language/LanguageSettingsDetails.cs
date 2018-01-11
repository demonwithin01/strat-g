using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace DEnt.Language
{
    /// <summary>
    /// Maintains information on how to load a language settings file.
    /// </summary>
    /// <typeparam name="T">The type that the settings file loads.</typeparam>
    public class LanguageSettingsDetails<T> : IDisposable where T : class, IDisposable, new()
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        /// <summary>
        /// The name of the file to load the settings from.
        /// </summary>
        private string _filename;

        /// <summary>
        /// Holds the loaded settings for the current language.
        /// </summary>
        private T _loadedSettings;

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// The name of the file settings to load.
        /// </summary>
        /// <param name="filename">The name of the file to be loaded.</param>
        public LanguageSettingsDetails( string filename )
        {
            this._filename = filename;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        /// <summary>
        /// Disposes of the current language settings.
        /// </summary>
        public void Dispose()
        {
            if ( this._loadedSettings != null )
            {
                this._loadedSettings.Dispose();
            }

            this._loadedSettings = null;
        }
        
        /// <summary>
        /// Loads the settings for the current language.
        /// </summary>
        /// <param name="force">Whether or not to force a load if the settings are already loaded.</param>
        public void Load( bool force = false )
        {
            if ( this._loadedSettings == null || force )
            {
                Stream stream = null;

                try
                {
                    XmlSerializer serializer = new XmlSerializer( typeof( T ) );
                    stream = new FileStream( this._filename, FileMode.Open );

                    this._loadedSettings = (T)( serializer.Deserialize( stream ) );
                }
                catch( Exception ex )
                {
                    Debug.Log( "Could not load: " + this._filename + " -- Reason: " + ex.Message );
                    throw new Exception( "Could not load language settings - " + ex.Message, ex );
                }
                finally
                {
                    if ( stream != null )
                    {
                        stream.Close();
                        stream.Dispose();
                    }
                }
            }
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public T Settings
        {
            get
            {
                if ( this._loadedSettings == null )
                {
                    this.Load( true );
                }

                return this._loadedSettings;
            }
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}