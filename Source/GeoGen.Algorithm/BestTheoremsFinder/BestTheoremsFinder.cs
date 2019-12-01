﻿using GeoGen.Core;
using GeoGen.TheoremProver;
using GeoGen.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace GeoGen.Algorithm
{
    /// <summary>
    /// The default implementation of <see cref="IBestTheoremsFinder"/>. This implementation does this: 
    /// 
    /// 1. Look at each group of <see cref="TheoremProverOutput.UnprovenTheoremGroups"/>.
    /// 2. From each group select the theorem with the highest ranking that can't be simplified.
    /// 3. Return these theorems.
    /// 
    /// </summary>
    public class BestTheoremsFinder : IBestTheoremsFinder
    {
        /// <summary>
        /// Processes a given algorithm output in order to find interesting theorems. 
        /// </summary>
        /// <param name="output">The algorithm output to be processed.</param>
        /// <returns>The interesting theorems of the output.</returns>
        public IEnumerable<Theorem> FindInterestingTheorems(AlgorithmOutput output)
        {
            // Take the groups
            return output.ProverOutput.UnprovenTheoremGroups
                // For each group find its non-simplified theorems
                .Select(group => group.Where(theorem => !output.SimplifiedTheorems.ContainsKey(theorem)).ToList())
                // Take only groups where there is an least one theorem
                .Where(group => group.Any())
                // From each group take the theorem with the highest ranking
                .Select(group => group.MaxItem(theorem => output.Rankings[theorem].TotalRanking));
        }
    }
}