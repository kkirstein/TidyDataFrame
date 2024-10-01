using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidyDataFrame;
using TidyDataFrame.Examples;

namespace TidyDataFrameTests
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
    }
}
