using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StrategyGame.Units
{
    /// <summary>
    /// The standard unit rotation action.
    /// </summary>
    public class UnitRotation : UnitAction
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        /// <summary>
        /// The initial rotation value.
        /// </summary>
        private readonly float _startRotation;

        /// <summary>
        /// The end rotation value.
        /// </summary>
        private readonly float _endRotation;

        ///// <summary>
        ///// The direction of the rotation.
        ///// </summary>
        //private readonly float _direction;
        
        /// <summary>
        /// The current lerp value for the rotation.
        /// </summary>
        private float _lerpValue;

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates a new standard unit rotation action for the specified unit.
        /// </summary>
        /// <param name="unit">The unit that this action is attached to.</param>
        /// <param name="startRotation">The initial rotation value.</param>
        /// <param name="endRotation">The end rotation value.</param>
        internal UnitRotation( BattleUnit unit, float startRotation, float endRotation )
            : base( unit )
        {
            this._startRotation = startRotation;
            this._endRotation = endRotation;

            while( this._startRotation < 0f )
            {
                this._startRotation += 360f;
            }

            while ( this._startRotation >= 360f )
            {
                this._startRotation -= 360f;
            }

            while ( this._endRotation < 0f )
            {
                this._endRotation += 360f;
            }

            while ( this._endRotation >= 360f )
            {
                this._endRotation -= 360f;
            }

            //if ( this._startRotation >= this._endRotation && this._endRotation - this._startRotation >= 0f )
            //{
            //    this._direction = 1f;
            //}
            //else
            //{
            //    this._direction = -1f;
            //}

            this._lerpValue = 0f;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Internal Methods

        /// <summary>
        /// Called immediately before the first perform cycle.
        /// </summary>
        /// <returns>Whether or not this action is still required.</returns>
        internal override bool PerformStart()
        {
            return ( base.unit.transform.rotation.eulerAngles.y != this._endRotation );
        }

        /// <summary>
        /// Performs the standard unit rotation.
        /// </summary>
        internal override void Perform()
        {
            Vector3 currentRotation = base.unit.transform.rotation.eulerAngles;
            
            currentRotation.y = Mathf.LerpAngle( this._startRotation, this._endRotation, this._lerpValue );

            base.unit.transform.rotation = Quaternion.Euler( currentRotation );

            if ( this._lerpValue >= 1f )
            {
                base.unit.FacingDirection = HexHelpers.ReverseDirection( HexHelpers.GetRotationDirection( this._endRotation ) );
                base.FinishAction();
            }
            else
            {
                this._lerpValue += ( Time.deltaTime * base.unit._standingRotationSpeed );
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