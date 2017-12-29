﻿using System;
using System.Collections.Generic;
using GeoGen.AnalyticalGeometry;
using GeoGen.AnalyticalGeometry.AnalyticalObjects;
using GeoGen.Core.Configurations;

namespace GeoGen.Analyzer
{
    /// <summary>
    /// A default implementation of <see cref="IObjectsContainer"/>.
    /// </summary>
    internal sealed class ObjectsContainer : IObjectsContainer
    {
        #region Private fields

        /// <summary>
        /// The dictionary mapping analytical objects to their corresponding
        /// configuration objects.
        /// </summary>
        private readonly Dictionary<IAnalyticalObject, ConfigurationObject> _objectsDictionary;

        /// <summary>
        /// The dictionary mapping configuration object's ids 
        /// </summary>
        private readonly Dictionary<int, IAnalyticalObject> _idToObjects;

        /// <summary>
        /// The dictionary mapping accepted types of analytical objects to 
        /// their corresponding configuration object types.
        /// </summary>
        private readonly IReadOnlyDictionary<Type, ConfigurationObjectType> _correctTypes;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ObjectsContainer()
        {
            _objectsDictionary = new Dictionary<IAnalyticalObject, ConfigurationObject>();
            _idToObjects = new Dictionary<int, IAnalyticalObject>();
            _correctTypes = new Dictionary<Type, ConfigurationObjectType>
            {
                {typeof(Point), ConfigurationObjectType.Point},
                {typeof(Circle), ConfigurationObjectType.Circle},
                {typeof(Line), ConfigurationObjectType.Line}
            };
        }

        #endregion

        #region IObjectsContainer implementation

        /// <summary>
        /// Adds a given object to the container. If the analytical version 
        /// of the object is already present in the container, then it will return
        /// the instance the <see cref="ConfigurationObject"/> that represents the 
        /// given object. If the object is new, it will return the original object.
        /// </summary>
        /// <param name="analyticalObject">The analytical object.</param>
        /// <param name="configurationObject">The configuration object.</param>
        /// <returns>The representation of an equal object.</returns>
        public ConfigurationObject Add(IAnalyticalObject analyticalObject, ConfigurationObject configurationObject)
        {
            if (configurationObject == null)
                throw new ArgumentNullException(nameof(configurationObject));

            if (analyticalObject == null)
                throw new ArgumentNullException(nameof(analyticalObject));

            if (_correctTypes[analyticalObject.GetType()] != configurationObject.ObjectType)
                throw new AnalyzerException("Can't add objects of wrong types to the container.");

            if (_objectsDictionary.ContainsKey(analyticalObject))
                return _objectsDictionary[analyticalObject];

            _objectsDictionary.Add(analyticalObject, configurationObject);

            var id = configurationObject.Id ?? throw new AnalyzerException("Id must be set");
            _idToObjects.Add(id, analyticalObject);

            return configurationObject;
        }

        /// <summary>
        /// Removes a given configuration object from the container. 
        /// </summary>
        /// <param name="configurationObject">The configuration object.</param>
        public void Remove(ConfigurationObject configurationObject)
        {
            if (configurationObject == null)
                throw new ArgumentNullException(nameof(configurationObject));

            var id = configurationObject.Id ?? throw new AnalyzerException("Id must be set");

            if (!_idToObjects.ContainsKey(id))
                throw new AnalyzerException("Object to be removed not found in the container.");

            _objectsDictionary.Remove(_idToObjects[id]);
            _idToObjects.Remove(id);
        }

        /// <summary>
        /// Gets the analytical representation of a given configuration object. 
        /// </summary>
        /// <typeparam name="T">The type of analytical object.</typeparam>
        /// <param name="configurationObject">The configuration object.</param>
        /// <returns>The analytical object.</returns>
        public T Get<T>(ConfigurationObject configurationObject) where T : IAnalyticalObject
        {
            if (configurationObject == null)
                throw new ArgumentNullException(nameof(configurationObject));

            var id = configurationObject.Id ?? throw new AnalyzerException("Id must be set.");

            try
            {
                var result = _idToObjects[id];

                if (!(result is T castedResult))
                    throw new AnalyzerException("Incorrect asked type of the analytical object.");

                return castedResult;
            }
            catch (KeyNotFoundException)
            {
                throw new AnalyzerException("Object not found in the container.");
            }
        }

        /// <summary>
        /// Gets the analytical representation of a given configuration object. 
        /// </summary>
        /// <param name="configurationObject">The configuration object.</param>
        /// <returns>The analytical object.</returns>
        public IAnalyticalObject Get(ConfigurationObject configurationObject)
        {
            if (configurationObject == null)
                throw new ArgumentNullException(nameof(configurationObject));

            return Get<IAnalyticalObject>(configurationObject);
        }

        #endregion
    }
}