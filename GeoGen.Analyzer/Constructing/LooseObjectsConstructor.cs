﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeoGen.AnalyticalGeometry;
using GeoGen.Core;

namespace GeoGen.Analyzer
{
    /// <summary>
    /// A default implementation of <see cref="ILooseObjectsConstructor"/> that uses 
    /// <see cref="IRandomObjectsProvider"/> to construct mutually distinct objects
    /// with no other conditions.
    /// </summary>
    internal class LooseObjectsConstructor : ILooseObjectsConstructor
    {
        #region Private fields

        /// <summary>
        /// The random objects provider.
        /// </summary>
        private readonly IRandomObjectsProvider _provider;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new loose objects constructor that uses a given
        /// random objects provider.
        /// </summary>
        /// <param name="provider">The random objects provider.</param>
        public LooseObjectsConstructor(IRandomObjectsProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        #endregion

        #region ILooseObjectsConstructor implementation

        /// <summary>
        /// Constructs given loose objects.
        /// </summary>
        /// <param name="looseObjects">The loose objects.</param>
        /// <returns>The list of analytical objects.</returns>
        public List<AnalyticalObject> Construct(IEnumerable<LooseConfigurationObject> looseObjects)
        {
            return looseObjects.Select(looseObject =>
            {
                if (looseObject == null)
                    throw new ArgumentException("Loose objects contains null value.");

                switch (looseObject.ObjectType)
                {
                    case ConfigurationObjectType.Point:
                        return _provider.NextRandomObject<Point>();
                    case ConfigurationObjectType.Line:
                        return _provider.NextRandomObject<Line>();
                    case ConfigurationObjectType.Circle:
                        return _provider.NextRandomObject<Circle>();
                    default:
                        throw new AnalyzerException("Unhandled case.");
                }
            }).ToList();
        }

        #endregion
    }
}