using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DEnt
{
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

        public static GameObject FindObject( this GameObject parent, string name )
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
