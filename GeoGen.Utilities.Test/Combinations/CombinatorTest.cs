﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeoGen.Utilities.Combinations;
using NUnit.Framework;

namespace GeoGen.Utilities.Test.Combinations
{
    [TestFixture]
    public class CombinatorTest
    {
        private static Combinator Combinator()
        {
            return new Combinator();
        }

        [Test]
        public void Test_With_Four_Lists_To_Be_Combined()
        {
            var testSample = new Dictionary<int, IEnumerable<int>>
            {
                {1, new List<int> {1, 2, 3, 42}},
                {2, new List<int> {1, 2, 3, 25}},
                {3, new List<int> {1, 2, 42, 25}},
                {4, new List<int> {1, 2, 3, 7, 9}}
            };

            var result = Combinator().Combine(testSample).ToList();

            var contains = result.Any(dic => dic[1] == 2 && dic[2] == 3 && dic[3] == 1 && dic[4] == 7);
            var count = result.Count;

            Assert.IsTrue(contains);
            Assert.AreEqual(320, count);
        }

        [Test]
        public void Test_With_One_List()
        {
            var testSample = new Dictionary<int, IEnumerable<int>>
            {
                {1, new List<int> {1, 2, 3, 42}}
            };

            var result = Combinator().Combine(testSample).ToList();

            var contains = result.Any(dic => dic[1] == 42);
            var count = result.Count;

            Assert.IsTrue(contains);
            Assert.AreEqual(4, count);
        }

        [Test]
        public void Test_With_More_One_Element_Lists()
        {
            var testSample = new Dictionary<int, IEnumerable<int>>
            {
                {1, new List<int> {1}},
                {2, new List<int> {1}},
                {3, new List<int> {2}}
            };

            var result = Combinator().Combine(testSample).ToList();

            var contains = result.Any(dic => dic[1] == 1 && dic[2] == 1 && dic[3] == 2);
            var count = result.Count;

            Assert.IsTrue(contains);
            Assert.AreEqual(1, count);
        }

        [Test]
        public void Test_With_One_Element_List_And_Other_List()
        {
            var testSample = new Dictionary<int, IEnumerable<int>>
            {
                {1, new List<int> {1}},
                {2, new List<int> {1, 2}}
            };

            var result = Combinator().Combine(testSample).ToList();

            var contains = result.Any(dic => dic[1] == 1 && dic[2] == 2);
            var count = result.Count;

            Assert.IsTrue(contains);
            Assert.AreEqual(2, count);
        }

        [Test]
        public void Test_With_An_Empty_List_Inside_Possibilities()
        {
            var testSample = new Dictionary<int, IEnumerable<int>>
            {
                {1, new List<int> {1, 2}},
                {2, new List<int>()}
            };

            var result = Combinator().Combine(testSample).ToList();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void Test_With_Two_Empty_Lists_Inside_Possibilities()
        {
            var testSample = new Dictionary<int, IEnumerable<int>>
            {
                {1, new List<int> {1, 2}},
                {2, new List<int>()},
                {3, new List<int> {1, 2, 4}},
                {5, new List<int>()}
            };

            var result = Combinator().Combine(testSample).ToList();

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void Test_Possibilites_Cant_Be_Empty()
        {
            Assert.Throws<ArgumentException>(() => Combinator().Combine(new Dictionary<int, IEnumerable<int>>()));
        }

        [Test]
        public void Test_Possibilities_Cant_Be_Null()
        {
            Assert.Throws<ArgumentNullException>(() => Combinator().Combine<int, int>(null));
        }
    }
}