using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Defines details for describing languages and loading language files.
/// </summary>
public class LanguageDescriptionAttribute : Attribute
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates the language description using provided values.
    /// </summary>
    /// <param name="displayName">The display name.</param>
    /// <param name="folderName">The folder name.</param>
    public LanguageDescriptionAttribute( string displayName, string folderName )
    {
        this.DisplayName = displayName;
        this.FolderName = folderName;
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
    /// Gets the display name of the language.
    /// </summary>
    public string DisplayName { get; private set; }

    /// <summary>
    /// Gets the folder name of the language.
    /// </summary>
    public string FolderName { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}
