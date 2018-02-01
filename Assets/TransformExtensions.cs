using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DEnt
{
    public static class TransformExtensions
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
        /// Finds a descendant with the provided name.
        /// </summary>
        /// <param name="parent">The parent transform object to find the descendant on.</param>
        /// <param name="name">The name of the descendant to find.</param>
        /// <returns>A valid transform object if the descendant was found, otherwise null.</returns>
        public static Transform FindDescendant( this Transform parent, string name )
        {
            if ( parent == null ) return null;

            if ( parent.name == name ) return parent;

            if ( parent.childCount == 0 ) return null;

            foreach( Transform child in parent )
            {
                Transform result = FindDescendant( child, name );

                if ( result != null ) return result;
            }

            return null;
        }

        /// <summary>
        /// Detaches and destroys all child objects.
        /// </summary>
        /// <param name="parent">The parent transform object to detach and destroy all children for.</param>
        public static void DetachAndDestroyChildren( this Transform parent )
        {
            List<Transform> children = new List<Transform>();
            foreach ( Transform child in parent )
            {
                children.Add( child );
            }

            parent.DetachChildren();

            foreach( Transform child in children )
            {
                GameObject.Destroy( child );
            }
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
