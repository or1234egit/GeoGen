﻿using GeoGen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using static GeoGen.Core.LooseObjectsLayout;

namespace GeoGen.Core
{
    /// <summary>
    /// Represents a wrapper of <see cref="LooseConfigurationObject"/>s together with their <see cref="LooseObjectsLayout"/>.
    /// </summary>
    public class LooseObjectsHolder
    {
        #region Public properties

        /// <summary>
        /// Gets the list of actual loose configurations objects held by this instance.
        /// </summary>
        public IReadOnlyList<LooseConfigurationObject> LooseObjects { get; }

        /// <summary>
        /// Gets the loose objects wrapped in an objects map.
        /// </summary>
        public ConfigurationObjectsMap ObjectsMap { get; }

        /// <summary>
        /// Gets the layout in which these objects are arranged.
        /// </summary>
        public LooseObjectsLayout Layout { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LooseObjectsHolder"/> 
        /// instance wrapping the actual loose objects and possible their layout.
        /// </summary>
        /// <param name="looseObjects">The actual loose configurations objects.</param>
        /// <param name="layout">The layout of these loose objects.</param>
        public LooseObjectsHolder(IEnumerable<LooseConfigurationObject> looseObjects, LooseObjectsLayout layout)
        {
            LooseObjects = looseObjects?.ToList() ?? throw new ArgumentNullException(nameof(looseObjects));
            ObjectsMap = new ConfigurationObjectsMap(looseObjects);
            Layout = layout;

            // Make sure the objects match the layout
            if (!LooseObjects.Select(o => o.ObjectType).SequenceEqual(layout.ObjectTypes()))
                throw new GeoGenException($"The loose objects don't match the specified layout {layout}.");

            // Make sure they are distinct 
            if (LooseObjects.Distinct().Count() != LooseObjects.Count)
                throw new GeoGenException("The loose objects are not distinct.");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Finds all mappings of loose objects to themselves that represent geometrically equivalent
        /// layout. These mappings include the identical mapping. For example, if we have a triangle ABC,
        /// i.e. the loose objects are A, B, C, then there are 6 possible mappings ([A, B, C], [A, C, B],...)
        /// yielding a symmetric layout.
        /// </summary>
        /// <returns>The dictionaries mapping the current loose objects to themselves.</returns>
        public IEnumerable<IReadOnlyDictionary<LooseConfigurationObject, LooseConfigurationObject>> GetSymmetryMappings()
        {
            // Switch based on the layout
            switch (Layout)
            {
                // Cases where any permutation works
                case TwoPoints:
                case ThreePoints:

                    // Take all permutations and zip them with the objects
                    return LooseObjects.Permutations().Select(LooseObjects.ZipToDictionary);

                // Default case
                default:
                    throw new GeoGenException($"Unhandled type of loose objects layout: {Layout}");
            }
        }

        #endregion

        #region HashCode and Equals

        /// <summary>
        /// Gets the hash code of this object.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode() => (Layout, LooseObjects.GetHashCodeOfList()).GetHashCode();

        /// <summary>
        /// Finds out if a passed object is equal to this one.
        /// </summary>
        /// <param name="otherObject">The passed object.</param>
        /// <returns>true, if they are equal; false otherwise.</returns>
        public override bool Equals(object otherObject)
        {
            // Either the references are equals
            return this == otherObject
                // Or the object is not null
                || otherObject != null
                // And is a loose objects holder
                && otherObject is LooseObjectsHolder holder
                // And the layouts are equal
                && holder.Layout.Equals(Layout)
                // And the loose objects are equal
                && holder.LooseObjects.SequenceEqual(LooseObjects);
        }

        #endregion

        #region To String

        /// <summary>
        /// Converts the loose objects holder to a string. 
        /// NOTE: This method is used only for debugging purposes.
        /// </summary>
        /// <returns>A human-readable string representation of the configuration.</returns>
        public override string ToString() => $"{Layout}({LooseObjects.Select(o => o.Id).ToJoinedString()})";

        #endregion
    }
}