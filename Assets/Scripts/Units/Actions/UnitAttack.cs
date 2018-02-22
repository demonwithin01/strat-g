using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyGame.Units
{
    /// <summary>
    /// The standard unit attack action.
    /// </summary>
    public class UnitAttack : UnitAction
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        /// <summary>
        /// The unit to attack.
        /// </summary>
        private BattleUnit _toAttack;

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates a new standard unit attack action for the specified unit.
        /// </summary>
        /// <param name="unit">The unit that this attack action is to be attached to.</param>
        /// <param name="toAttack">The unit to attack.</param>
        internal UnitAttack( BattleUnit unit, BattleUnit toAttack )
            : base( unit )
        {
            _toAttack = toAttack;
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
            return true;
        }

        /// <summary>
        /// Performs the attack action.
        /// </summary>
        internal override void Perform()
        {
            AttackDamage[] attacks = new AttackDamage[ 1 ];
            attacks[ 0 ] = new AttackDamage( base.unit.Unit.MeleeAttack, base.unit.Unit.MeleePrecision );

            this._toAttack.ReceiveAttacks( attacks );

            base.FinishAction();
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