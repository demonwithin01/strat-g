using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Path<Hex> : IEnumerable<Hex>
{

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Class Members

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

    #region Constructors/Initialisation

    private Path( Hex lastStep, Path<Hex> previousSteps, double totalCost )
    {
        LastStep = lastStep;
        PreviousSteps = previousSteps;
        TotalCost = totalCost;
    }

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

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<Hex> GetEnumerator()
    {
        for ( var p = this ; p != null ; p = p.PreviousSteps )
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

        for ( var p = this ; p != null ; p = p.PreviousSteps )
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

    public Hex LastStep { get; private set; }
    public Path<Hex> PreviousSteps { get; private set; }
    public double TotalCost { get; set; }

    #endregion

    /* ---------------------------------------------------------------------------------------------------------- */

}