﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;
using TidyDataFrame;
using TidyDataFrame.Examples;

namespace TidyDataFrameTests
{
    [TestClass]
    public class TestPivoting
    {

        [TestMethod]
        public void TestToLonger()
        {
            var df1 = Df.MtCars;

            var df2 = Pivoting.ToLonger(df1, ["cyl", "carb", "gear"]);

            Assert.IsInstanceOfType(df2, typeof(DataFrame));
            Assert.AreEqual(df1.Columns.Count - 3 + 2, df2.Columns.Count);
            Assert.AreEqual(df1.Rows.Count * 3, df2.Rows.Count);

            var names = df2["Names"];
            Assert.IsInstanceOfType(names, typeof(StringDataFrameColumn));
            var n = Enumerable.Range(0, (int)names.Length).Select(i => names[i]);
            Assert.IsTrue(n.Contains("cyl"));
            Assert.IsTrue(n.Contains("gear"));
            Assert.IsTrue(n.Contains("carb"));

            var values = df2["Values"];
            Assert.IsInstanceOfType(values, typeof(Int32DataFrameColumn));

            CollectionAssert.AreEqual(new List<String> { "cyl", "carb", "gear" }, names[0, 3].ToList());
            CollectionAssert.AreEqual(new List<String> { "cyl", "carb", "gear" }, names[df2.Rows.Count - 3, 3].ToList());
            CollectionAssert.AreEqual(new List<int> { 6, 4, 4 }, values[0, 3].ToList());
            CollectionAssert.AreEqual(new List<int> { 4, 2, 4 }, values[df2.Rows.Count - 3, 3].ToList());
        }

        [TestMethod]
        public void TestToWider()
        {
        }
    }
}
