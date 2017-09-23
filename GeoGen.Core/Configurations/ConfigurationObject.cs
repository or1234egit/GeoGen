﻿namespace GeoGen.Core.Configurations
{
    /// <summary>
    /// Represents an object that can be contained in a <see cref="Configuration"/>.
    /// </summary>
    public abstract class ConfigurationObject
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the id of this configuration object. The id should be
        /// unit solely during the generation process. It will be reseted every time
        /// the process starts over.
        /// </summary>
        public int? Id { get; set; }

        #endregion

        #region Public abstract properties

        /// <summary>
        /// Gets the actual geometrical type of this object (such as Point, Line...)
        /// </summary>
        public abstract ConfigurationObjectType ObjectType { get; }

        #endregion
    }
}