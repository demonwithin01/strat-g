using UnityEngine;

namespace DEnt
{
    /// <summary>
    /// Maintains a list of extensions for the UnityEngine GameObject class.
    /// </summary>
    public static class GameObjectExtensions
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
        /// Finds a descendent game object based off the name, regardless of whether or not the object is inactive.
        /// </summary>
        /// <param name="parent">The game object to find the descendent on.</param>
        /// <param name="name">The name of the descendent to find.</param>
        /// <returns>Null if there is no descendent with that name.</returns>
        public static GameObject FindDescendant( this GameObject parent, string name )
        {
            Transform[] trs = parent.GetComponentsInChildren<Transform>( true );
            foreach ( Transform t in trs )
            {
                if ( t.name == name )
                {
                    return t.gameObject;
                }
            }
            return null;
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
