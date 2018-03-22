using DEnt;
using UnityEngine;

namespace StrategyGame.Units
{
    /// <summary>
    /// The standard unit movement action.
    /// </summary>
    public class UnitMovement : UnitAction
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        private const float PiOver3 = 1.0471975512f;

        /// <summary>
        /// The initial position to start this action from.
        /// </summary>
        private readonly Vector3 _startPosition;

        /// <summary>
        /// The position to end this action on.
        /// </summary>
        private readonly Vector3 _endPosition;

        /// <summary>
        /// The position to rotate around.
        /// </summary>
        private readonly Vector3 _rotateAround;
        
        /// <summary>
        /// The initial rotation to start this action from.
        /// </summary>
        private readonly float _startRotation;

        /// <summary>
        /// The end rotation for this movement action.
        /// </summary>
        private readonly float _endRotation;

        private float _rotationAngle = 0f;
        
        /// <summary>
        /// The movement speed modifier.
        /// </summary>
        private readonly float _speedModifier;

        /// <summary>
        /// Holds the current lerp value.
        /// </summary>
        private float _lerpValue;

        /// <summary>
        /// The tile that the unit will be on when it completes this movement action.
        /// </summary>
        private BattleHex _endTile;

        /// <summary>
        /// The direction the unit will be facing when it reaches the end of the movement.
        /// </summary>
        private HexDirection _endFacingDirection;

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates a new movement action for the specified unit.
        /// </summary>
        /// <param name="unit">The unit this action is attached to.</param>
        /// <param name="startPosition">The initial position to start this action from.</param>
        /// <param name="distance">The total distance of the movement.</param>
        /// <param name="startRotation">The initial rotation to start this action from.</param>
        /// <param name="endRotation">The end rotation for this movement action.</param>
        /// <param name="speedModifier">The movement speed modifier.</param>
        internal UnitMovement( BattleUnit unit, Vector3 startPosition, Vector3 endPosition, BattleHex currentTile, HexDirection startDirection, HexDirection endDirection, float startRotation, float endRotation, float speedModifier )
            : base( unit )
        {
            this._startPosition = startPosition;
            this._endPosition = endPosition;
            this._endTile = currentTile;
            this._endFacingDirection = endDirection;

            if ( startDirection != endDirection )
            {
                HexDirection between = HexHelpers.DirectionBetween( startDirection, endDirection );

                this._rotateAround = currentTile.GetNeighbourPosition( between );

                this._startRotation = startRotation;
                this._endRotation = endRotation;

                float rotation = endRotation - startRotation;

                while ( rotation <= 0f )
                {
                    rotation += 360f;
                }

                this._rotationAngle = rotation;
            }
            
            this._speedModifier = speedModifier;

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
            base.unit.CurrentHexTile.Unit = null;
            base.unit.CurrentHexTile = this._endTile;
            base.unit.CurrentHexTile.Unit = base.unit;

            return true;
        }

        /// <summary>
        /// Performs the standard movement action.
        /// </summary>
        internal override void Perform()
        {
            if ( this._startRotation == this._endRotation )
            {
                MoveStraight();
            }
            else
            {
                MoveAndRotate();
            }
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Private Methods

        /// <summary>
        /// Performs a straight movement action in the current facing direction.
        /// </summary>
        private void MoveStraight()
        {
            base.unit.transform.position = Vector3.Lerp( this._startPosition, this._endPosition, this._lerpValue );

            if ( this._lerpValue >= 1f )
            {
                base.FinishAction();
            }
            else
            {
                this._lerpValue += ( Time.fixedDeltaTime * this._speedModifier * base.unit._walkSpeed );
            }
        }

        /// <summary>
        /// Performs the move and rotate portion of a movement.
        /// </summary>
        private void MoveAndRotate()
        {
            unit.transform.position = this._startPosition.RotateAroundPivot( this._rotateAround, Mathf.LerpAngle( 0f, this._rotationAngle, this._lerpValue ) );

            Vector3 rotation = unit.transform.eulerAngles;

            rotation.y = Mathf.LerpAngle( this._startRotation, this._endRotation, this._lerpValue );

            unit.transform.eulerAngles = rotation;
            
            if ( this._lerpValue >= 1f )
            {
                base.unit.FacingDirection = this._endFacingDirection;
                base.FinishAction();
            }
            else
            {
                this._lerpValue += ( Time.fixedDeltaTime * this._speedModifier * base.unit._walkSpeed / PiOver3 );
            }
        }

        /// <summary>
        /// R
        /// </summary>
        /// <param name="point"></param>
        /// <param name="pivot"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        private Vector3 RotateAroundPivot( Vector3 point, Vector3 pivot, float angle )
        {
            Vector3 direction = point - pivot;

            direction = Quaternion.Euler( 0f, angle, 0f ) * direction;

            point = direction + pivot;

            return point;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Properties

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

    }
}