using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;
using TidyDataFrame;

namespace TidyDataFrame.Tests
{
    [TestClass]
    public class TestDictDataframeBuilder
    {



        [TestMethod]
        public void TestConstructor()
        {
            var builder = new DictDataFrameBuilder();

            Assert.IsNotNull(builder);
            Assert.AreEqual(0, builder.ColumnSpec.Count);
        }

        [TestMethod]
        public void TestSingleAdd()
        {
            var builder = new DictDataFrameBuilder();

            var record1 = new Dictionary<string, object>
            {
                ["Value"] = 13.3,
                ["Name"] = "A"
            };
            var record2 = new Dictionary<string, object>
            {
                ["Value"] = 14.4,
                ["Id"] = 2
            };
            var record3 = new Dictionary<string, object>
            {
                ["Value"] = 15.5,
                ["Name"] = "C",
                ["Id"] = 3
            };

            builder.Add(record1);
            Assert.AreEqual(2, builder.ColumnSpec.Count);
            builder.Add(record2);
            Assert.AreEqual(3, builder.ColumnSpec.Count);
            builder.Add(record3);
            Assert.AreEqual(3, builder.ColumnSpec.Count);
        }

        [TestMethod]
        public void TestMultipleAdd()
        {
            var builder = new DictDataFrameBuilder();

            var record1 = new Dictionary<string, object>
            {
                ["Value"] = 13.3,
                ["Name"] = "A"
            };
            var record2 = new Dictionary<string, object>
            {
                ["Value"] = 14.4,
                ["Id"] = 2
            };
            var record3 = new Dictionary<string, object>
            {
                ["Value"] = 15.5,
                ["Name"] = "C",
                ["Id"] = 3
            };

            builder.Add([record1, record2, record3]);
            Assert.AreEqual(3, builder.ColumnSpec.Count);
        }

        [TestMethod]
        public void TestToDataframe()
        {
            var builder = new DictDataFrameBuilder();

            var record1 = new Dictionary<string, object>
            {
                ["Value"] = 13.3,
                ["Name"] = "A"
            };
            var record2 = new Dictionary<string, object>
            {
                ["Value"] = 14.4,
                ["Id"] = 2
            };
            var record3 = new Dictionary<string, object>
            {
                ["Value"] = 15.5,
                ["Name"] = "C",
                ["Id"] = 3
            };
            var record4 = new Dictionary<string, object>
            {
                ["Value"] = 16.6,
                ["Name"] = "D",
                ["Id"] = 4
            };

            builder.Add([record1, record2, record3, record4]);

            var df = builder.ToDataFrame();

            Assert.IsInstanceOfType(df, typeof(DataFrame));
            Assert.AreEqual(4, df.Rows.Count);
            Assert.AreEqual(3, df.Columns.Count);

            Assert.IsInstanceOfType(df["Name"], typeof(StringDataFrameColumn));
            Assert.IsInstanceOfType(df["Value"], typeof(DoubleDataFrameColumn));
            Assert.IsInstanceOfType(df["Id"], typeof(Int32DataFrameColumn));

            Assert.AreEqual(13.3, df["Value"][0]);
            Assert.AreEqual("C", df["Name"][2]);
            Assert.AreEqual(4, df["Id"][3]);
        }
    }
}
