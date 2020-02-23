﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoGen.ConsoleLauncher
{
    /// <summary>
    /// Represents a service that gets <see cref="LoadedInferenceRule"/>s.
    /// </summary>
    public interface IInferenceRuleProvider
    {
        /// <summary>
        /// Gets inference rules.
        /// </summary>
        /// <returns>The list of loaded inference rules.</returns>
        Task<IReadOnlyList<LoadedInferenceRule>> GetInferenceRulesAsync();
    }
}