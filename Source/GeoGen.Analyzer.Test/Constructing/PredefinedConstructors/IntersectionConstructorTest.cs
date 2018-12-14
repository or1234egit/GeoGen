﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using GeoGen.AnalyticalGeometry;
//using GeoGen.AnalyticalGeometry.AnalyticalObjects;
//using GeoGen.Analyzer.Test.TestHelpers;
//using GeoGen.Core.Configurations;
//using GeoGen.Core.Constructions.Arguments;
//using GeoGen.Core.Constructions.PredefinedConstructions;
//using GeoGen.Core.Theorems;
//using NUnit.Framework;

//namespace GeoGen.Analyzer.Test.Constructing.PredefinedConstructors
//{
//    [TestFixture]
//    public class IntersectionConstructorTest
//    {
//        private static InteresectionFromPointsConstructor Constructor() => new InteresectionFromPointsConstructor();

//        [Test]
//        public void Test_Type_Is_Correct()
//        {
//            Assert.AreEqual(typeof(IntersectionFromPoints), Constructor().PredefinedConstructionType);
//        }

//        [Test]
//        public void Test_Constructed_Objects_Cant_Be_Null()
//        {
//            Assert.Throws<ArgumentNullException>(() => Constructor().Construct(null));
//        }

//        [Test]
//        public void Test_Constructed_Objects_Must_Have_Count_One()
//        {
//            Assert.Throws<AnalyzerException>(() => Constructor().Construct(new List<ConstructedConfigurationObject>()));
//            Assert.Throws<AnalyzerException>(() => Constructor().Construct(new List<ConstructedConfigurationObject> {null, null}));
//        }

//        [Test]
//        public void Test_With_Incorrectly_Constructed_Objects()
//        {
//            var points = ConfigurationObjects.Objects(4, ConfigurationObjectType.Point);

//            var arguments = new List<ConstructionArgument>
//            {
//                new ObjectConstructionArgument(points[0]),
//                new ObjectConstructionArgument(points[1]),
//                new ObjectConstructionArgument(points[2])
//            };

//            var constructedObject = new ConstructedConfigurationObject(new IntersectionFromPoints(), arguments, 0);

//            var listOfObjects = new List<ConstructedConfigurationObject> {constructedObject};

//            Assert.Throws<AnalyzerException>(() => Constructor().Construct(listOfObjects));
//        }

//        [Test]
//        public void Test_Construction_With_Correct_Input()
//        {
//            var points = ConfigurationObjects.Objects(4, ConfigurationObjectType.Point);

//            var arguments = new List<ConstructionArgument>
//            {
//                new SetConstructionArgument(new HashSet<ConstructionArgument>
//                {
//                    new SetConstructionArgument(new HashSet<ConstructionArgument>
//                    {
//                        new ObjectConstructionArgument(points[0]),
//                        new ObjectConstructionArgument(points[1])
//                    }),
//                    new SetConstructionArgument(new HashSet<ConstructionArgument>
//                    {
//                        new ObjectConstructionArgument(points[2]),
//                        new ObjectConstructionArgument(points[3])
//                    })
//                })
//            };

//            var constructedObject = new ConstructedConfigurationObject(new IntersectionFromPoints(), arguments, 0);

//            var listOfObjects = new List<ConstructedConfigurationObject> {constructedObject};

//            var result = Constructor().Construct(listOfObjects);

//            var theorems = result.Theorems;

//            Assert.AreEqual(2, theorems.Count);

//            Assert.IsTrue(theorems[0].Type == TheoremType.CollinearPoints);
//            Assert.IsTrue(theorems[1].Type == TheoremType.CollinearPoints);

//            Assert.IsTrue(theorems.ContainsTheoremWithObjects(points[0], points[1], constructedObject));
//            Assert.IsTrue(theorems.ContainsTheoremWithObjects(points[2], points[3], constructedObject));

//            var function = result.ConstructorFunction;

//            Assert.NotNull(function);

//            Assert.Throws<ArgumentNullException>(() => function(null));

//            var representations = new List<List<Point>>
//            {
//                new List<Point> {new Point(0, 0), new Point(0, 1), new Point(1, 5), new Point(1, 6)},
//                new List<Point> {new Point(0, 0), new Point(1, 1), new Point(1, 5), new Point(1, 6)},
//                new List<Point> {new Point(0, 0), new Point(1, 1), new Point(1, 5), new Point(2, 7)}
//            };

//            var expected = new List<List<IAnalyticalObject>>
//            {
//                null,
//                null,
//                new List<IAnalyticalObject> {new Point(-3, -3)}
//            };

//            bool Equals(List<IAnalyticalObject> l1, List<IAnalyticalObject> l2) => l1 == l2 || l1.SequenceEqual(l2);

//            for (var i = 0; i < representations.Count; i++)
//            {
//                var container = new ObjectsContainer();

//                for (var j = 0; j < 4; j++)
//                {
//                    container.Add(representations[i][j], points[j]);
//                }

//                var output = function(container);

//                Assert.IsTrue(Equals(expected[i], output));
//            }
//        }
//    }
//}