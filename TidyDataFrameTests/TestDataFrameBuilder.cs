
using Microsoft.Data.Analysis;
using TidyDataFrame;

namespace TidyDataFrameTests
{
    [TestClass]
    public class TestDataFrameBuilder
    {

        private record MyDataRecord(
            double Value,
            string Name,
            int Id);

        private record MyNullableDataRecord(
            double? Value,
            string? Name,
            int? Id);

        [TestMethod]
        public void TestConstructor()
        {
            var builder = new DataFrameBuilder<MyDataRecord>();

            Assert.IsNotNull(builder);

            var colSpec = builder.ColumnSpec;
            Assert.IsNotNull(colSpec);
            Assert.AreEqual(typeof(string), colSpec["Name"].PropertyType);
            Assert.AreEqual(typeof(double), colSpec["Value"].PropertyType);
            Assert.AreEqual(typeof(int), colSpec["Id"].PropertyType);
        }

        [TestMethod]
        public void TestSingleAdd()
        {
            var builder = new DataFrameBuilder<MyDataRecord>();

            var record1 = new MyDataRecord(13.4, "A", 13);
            var record2 = new MyDataRecord(17.8, "B", 14);
            var record3 = new MyDataRecord(24.6, "C", 15);

            Assert.AreEqual(0, builder.Count);
            builder.Add(record1);
            builder.Add(record2);
            Assert.AreEqual(2, builder.Count);
            builder.Add(record3);
            Assert.AreEqual(3, builder.Count);
        }

        [TestMethod]
        public void TestMultipleAdd()
        {
            var builder = new DataFrameBuilder<MyDataRecord>();

            var records = new List<MyDataRecord>
            {
                new(13.4, "A", 13),
                new(17.8, "B", 14),
                new(24.6, "C", 15),
            };

            Assert.AreEqual(0, builder.Count);
            builder.Add(records);
            Assert.AreEqual(3, builder.Count);
        }

        [TestMethod]
        public void TestToDataFrame()
        {
            var builder = new DataFrameBuilder<MyDataRecord>();

            var records = new List<MyDataRecord>
            {
                new(13.4, "A", 13),
                new(17.8, "B", 14),
                new(24.6, "C", 15),
                new(42.0, "D", 16),
            };
            builder.Add(records);

            var df = builder.ToDataFrame();

            Assert.IsInstanceOfType(df, typeof(DataFrame));
            Assert.AreEqual(4, df.Rows.Count);
            Assert.AreEqual(3, df.Columns.Count);

            Assert.IsInstanceOfType(df["Name"], typeof(StringDataFrameColumn));
            Assert.IsInstanceOfType(df["Value"], typeof(DoubleDataFrameColumn));
            Assert.IsInstanceOfType(df["Id"], typeof(Int32DataFrameColumn));

            Assert.AreEqual(13.4, df["Value"][0]);
            Assert.AreEqual("C", df["Name"][2]);
            Assert.AreEqual(16, df["Id"][3]);
        }


        [TestMethod]
        public void TestNullData()
        {
            var builder = new DataFrameBuilder<MyNullableDataRecord>();

            var records = new List<MyNullableDataRecord>
            {
                new(13.4, "A", 13),
                new(null, "B", 14),
                new(24.6, null, 15),
                new(42.0, "D", null),
            };
            builder.Add(records);

            var df = builder.ToDataFrame();

            Assert.IsInstanceOfType(df, typeof(DataFrame));
            Assert.AreEqual(4, df.Rows.Count);
            Assert.AreEqual(3, df.Columns.Count);

            Assert.IsInstanceOfType(df["Name"], typeof(StringDataFrameColumn));
            Assert.IsInstanceOfType(df["Value"], typeof(DoubleDataFrameColumn));
            Assert.IsInstanceOfType(df["Id"], typeof(Int32DataFrameColumn));

            Assert.AreEqual(13.4, df["Value"][0]);
            Assert.AreEqual("B", df["Name"][1]);
            Assert.AreEqual(15, df["Id"][2]);
            Assert.IsNull(df["Value"][1]);
            Assert.IsNull(df["Name"][2]);
            Assert.IsNull(df["Id"][3]);
        }

    }
}
