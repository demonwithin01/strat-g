using System;

namespace DEnt.Language
{
    public class LanguageDetails
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates a new instance of the language details.
        /// </summary>
        /// <param name="language">The language to base the details off.</param>
        public LanguageDetails( SupportedLanguage language )
        {
            this.Language = language;

            bool isAttributeFound = false;

            Type type = language.GetType();

            object[] properties = type.GetMember( language.ToString() )[ 0 ].GetCustomAttributes( false );

            foreach ( object property in properties )
            {
                if ( property is LanguageDescriptionAttribute )
                {
                    LanguageDescriptionAttribute attribute = ( property as LanguageDescriptionAttribute );

                    this.DisplayName = attribute.DisplayName;
                    this.FolderName = attribute.FolderName;
                }
            }

            if ( isAttributeFound == false )
            {
                this.DisplayName = this.Language.ToString();
                this.FolderName = this.Language.ToString();
            }
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods
            
        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        /// <summary>
        /// Gets the language type.
        /// </summary>
        public SupportedLanguage Language { get; private set; }

        /// <summary>
        /// Gets the display name for the language.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Gets the folder name for the language.
        /// </summary>
        public string FolderName { get; private set; }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}
