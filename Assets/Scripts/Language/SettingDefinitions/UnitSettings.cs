using System;
using System.Xml.Serialization;

namespace DEnt.Language
{
    [XmlRoot( "UnitSettings" )]
    public class UnitSettings : IDisposable
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Public Methods

        /// <summary>
        /// Disposes of the unit settings.
        /// </summary>
        public void Dispose()
        {

        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        /// <summary>
        /// The name in the current language.
        /// </summary>
        [XmlElement( "Name" )]
        public string Name { get; set; }

        /// <summary>
        /// The description in the current language.
        /// </summary>
        [XmlElement( "Description" )]
        public string Description { get; set; }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}
