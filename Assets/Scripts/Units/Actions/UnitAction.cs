namespace StrategyGame.Units
{
    /// <summary>
    /// The base for all unit actions.
    /// </summary>
    public abstract class UnitAction
    {

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Class Members

        /// <summary>
        /// Holds a reference to the unit that this action is attached to.
        /// </summary>
        protected BattleUnit unit;

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Constructors/Initialisation

        /// <summary>
        /// Creates a new unit action for the provided unit.
        /// </summary>
        /// <param name="unit">The unit this action is attached to.</param>
        internal UnitAction( BattleUnit unit )
        {
            this.unit = unit;
        }

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Internal Methods

        /// <summary>
        /// Called immediately before the first perform cycle.
        /// </summary>
        /// <returns>Whether or not this action is still required.</returns>
        internal abstract bool PerformStart();

        /// <summary>
        /// The update loop for performing the action.
        /// </summary>
        internal abstract void Perform();

        #endregion

        /* ---------------------------------------------------------------------------------------------------------- */

        #region Protected Methods

        /// <summary>
        /// Finishes the current action.
        /// </summary>
        protected void FinishAction()
        {
            this.unit.FinishAction( this );
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