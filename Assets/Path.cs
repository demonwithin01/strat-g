using System.Collections;
using System.Collections.Generic;

/// <summary>
/// An object responsible for maintaining a path across a group of Hexes.
/// </summary>
public class Path<Hex> : IEnumerable<Hex>
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    /// <summary>
    /// Creates a new path step.
    /// </summary>
    /// <param name="lastStep">The step for the path to this point.</param>
    /// <param name="previousSteps">The previous steps for the path.</param>
    /// <param name="totalCost">The total cost to get to this</param>
    private Path( Hex lastStep, Path<Hex> previousSteps, double totalCost )
    {
        LastStep = lastStep;
        PreviousSteps = previousSteps;
        TotalCost = totalCost;
    }

    /// <summary>
    /// Creates a new path.
    /// </summary>
    /// <param name="start">The initial hex for the path.</param>
    public Path( Hex start )
        : this( start, null, 0 )
    {

    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Public Methods

    /// <summary>
    /// Adds a new hex tile step to the current path.
    /// </summary>
    /// <param name="step">The tile to be travelled to.</param>
    /// <param name="stepCost">The cost of travelling the to this step.</param>
    public Path<Hex> AddStep( Hex step, double stepCost )
    {
        return new Path<Hex>( step, this, TotalCost + stepCost );
    }

    /// <summary>
    /// Gets the enumerator so that the object can be traversed sequentially.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    /// <summary>
    /// Gets the enumerator so that the object can be traversed sequentially.
    /// </summary>
    public IEnumerator<Hex> GetEnumerator()
    {
        for ( Path<Hex> p = this ; p != null ; p = p.PreviousSteps )
        {
            yield return p.LastStep;
        }
    }

    /// <summary>
    /// Gets the path in a list.
    /// </summary>
    public List<Hex> GetPath()
    {
        List<Hex> path = new List<Hex>();

        for ( Path<Hex> p = this ; p != null ; p = p.PreviousSteps )
        {
            path.Add( p.LastStep );
        }

        path.Reverse();

        return path;
    }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Private Methods

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Properties

    /// <summary>
    /// Gets the last step in the current path.
    /// </summary>
    public Hex LastStep { get; private set; }

    /// <summary>
    /// Gets all the previous steps in the path.
    /// </summary>
    public Path<Hex> PreviousSteps { get; private set; }

    /// <summary>
    /// Gets the total cost of the path up to this point.
    /// </summary>
    public double TotalCost { get; private set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}