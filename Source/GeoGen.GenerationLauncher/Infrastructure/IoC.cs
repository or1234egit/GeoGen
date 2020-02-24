﻿using GeoGen.Algorithm;
using GeoGen.ConsoleLauncher;
using GeoGen.Constructor;
using GeoGen.Generator;
using GeoGen.Infrastructure;
using GeoGen.TheoremFinder;
using Ninject;
using System.Threading.Tasks;

namespace GeoGen.GenerationLauncher
{
    /// <summary>
    /// Represents a static initializer of the dependency injection system.
    /// </summary>
    public static class IoC
    {
        #region Kernel

        /// <summary>
        /// Gets the dependency injection container.
        /// </summary>
        public static IKernel Kernel { get; private set; }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the <see cref="Kernel"/> and all the dependencies.
        /// </summary>
        /// <param name="settings">The settings of the application.</param>
        public static Task InitializeAsync(Settings settings)
        {
            // Initialize the container
            Kernel = Infrastructure.IoC.CreateKernel();

            // Add the logging system
            Kernel.AddLogging(settings.LoggingSettings);

            #region Local dependencies

            // Add local dependencies
            Kernel.Bind<IBatchRunner>().To<BatchRunner>();
            Kernel.Bind<IAlgorithmRunner>().To<GenerationAlgorithmRunner>().WithConstructorArgument(settings.GenerationAlgorithmRunnerSettings);
            Kernel.Bind<IAlgorithmInputProvider>().To<AlgorithmInputProvider>().WithConstructorArgument(settings.AlgorithmInputProviderSettings);

            #endregion           

            #region Algorithm

            // Add generator
            Kernel.AddGenerator(settings.GenerationSettings)
                // Add constructor
                .AddConstructor()
                // Add algorithm
                .AddAlgorithm(settings.AlgorithmSettings);

            // Add an empty theorem finder
            Kernel.Bind<ITheoremFinder>().To<EmptyTheoremFinder>();

            #endregion

            #region Tracers

            #region ConstructorFailureTracer

            // Rebind Constructor Failure Tracer only if we're supposed be tracing
            if (settings.TracingSettings.ConstructorFailureTracerSettings != null)
                Kernel.Rebind<IConstructorFailureTracer>().To<ConstructorFailureTracer>().WithConstructorArgument(settings.TracingSettings.ConstructorFailureTracerSettings);

            #endregion

            #region GeometryFailureTracer

            // Rebind Geometry Failure Tracer only if we're supposed be tracing
            if (settings.TracingSettings.GeometryFailureTracerSettings != null)
                Kernel.Rebind<IGeometryFailureTracer>().To<GeometryFailureTracer>().WithConstructorArgument(settings.TracingSettings.GeometryFailureTracerSettings);

            #endregion

            #endregion

            // Return a finished task
            return Task.CompletedTask;
        }

        #endregion
    }
}