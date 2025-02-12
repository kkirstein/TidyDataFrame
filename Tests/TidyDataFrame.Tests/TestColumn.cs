using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidyDataFrame;
using TidyDataFrame.Examples;

namespace TidyDataFrame.Tests
{
    [TestClass]
    public class TestColumn
    {
        [TestMethod]
        public void TestColumnDrop()
        {
            var df = Df.MtCars;

            // single column drop
            var df1 = Column.Drop(df, "cyl");

            Assert.AreEqual(df.Columns.Count - 1, df1.Columns.Count);

            var expectedColumnNames = new List<string> {
                "Column0","mpg","disp","hp","drat","wt","qsec","vs","am","gear","carb"
            };
            var actualColumnNames = df1.Columns.Select(x => x.Name).ToList();
            CollectionAssert.AreEqual(expectedColumnNames, actualColumnNames);

            // multiple column drop
            var df2 = Column.Drop(df, ["mpg", "hp", "vs"]);

            Assert.AreEqual(df.Columns.Count - 3, df2.Columns.Count);

            expectedColumnNames = new List<string> {
                "Column0","cyl","disp","drat","wt","qsec","am","gear","carb"
            };
            actualColumnNames = df2.Columns.Select(x => x.Name).ToList();
            CollectionAssert.AreEqual(expectedColumnNames, actualColumnNames);
        }

        [TestMethod]
        public void TestColumnTake()
        {
            var df = Df.MtCars;

            var colNames = new List<string> { "cyl", "disp", "wt", "qsec", "am" };
            var df1 = Column.Take(df, colNames);

            Assert.AreEqual(colNames.Count, df1.Columns.Count);

            var actualColumnNames = df1.Columns.Select(x => x.Name).ToList();
            CollectionAssert.AreEqual(colNames, actualColumnNames);
        }

        [TestMethod]
        public void TestDataColumnString()
        {
            List<string> data = ["eins", "zwei", "drei", "vier"];
            var col = Column.ToDataFrameColumn(data, "test");
            Assert.AreEqual("test", col.Name);
            Assert.AreEqual(4, col.Length);
            Assert.AreEqual("eins", col[0]);
            Assert.AreEqual("zwei", col[1]);
            Assert.AreEqual("drei", col[2]);
            Assert.AreEqual("vier", col[3]);
        }

        [TestMethod]
        public void TestDataColumnInt()
        {
            List<int> data = [1, 2, 3, 4];
            var col = Column.ToDataFrameColumn(data, "test");
            Assert.AreEqual("test", col.Name);
            Assert.AreEqual(4, col.Length);
            Assert.AreEqual(1, col[0]);
            Assert.AreEqual(2, col[1]);
            Assert.AreEqual(3, col[2]);
            Assert.AreEqual(4, col[3]);
        }

        [TestMethod]
        public void TestDataColumnDouble()
        {
            List<double> data = [1.0, 2.0, 3.0, 4.0];
            var col = Column.ToDataFrameColumn(data, "test");
            Assert.AreEqual("test", col.Name);
            Assert.AreEqual(4, col.Length);
            Assert.AreEqual(1.0, col[0]);
            Assert.AreEqual(2.0, col[1]);
            Assert.AreEqual(3.0, col[2]);
            Assert.AreEqual(4.0, col[3]);
        }

        [TestMethod]
        public void TestDatacolumnUnsupported()
        {
            List<object> data = ["eins", 2, 3.0, null];
            Assert.ThrowsException<InvalidDataTypeException>(() => Column.ToDataFrameColumn(data, "test"));
        }
    }
}
