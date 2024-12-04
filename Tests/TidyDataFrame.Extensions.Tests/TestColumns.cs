using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TidyDataFrame.Extensions;
using TidyDataFrame.Examples;

namespace TidyDataFrame.Extensions.Tests
{
    [TestClass]
    public class TestColumns
    {
        [TestMethod]
        public void TestColumnDrop()
        {
            var df = Df.MtCars;

            Assert.AreEqual(12, df.Columns.Count);
            Assert.AreEqual(11, df.DropColumn("cyl").Columns.Count);
            Assert.AreEqual(9, df.DropColumn(["mpg", "gear", "qsec"]).Columns.Count);
        }

        [TestMethod]
        public void TestColumnTake()
        {
            var df = Df.MtCars;

            Assert.AreEqual(12, df.Columns.Count);
            //Assert.AreEqual(1, df.TakeColumn("cyl").Columns.Count);
            Assert.AreEqual(3, df.TakeColumn(["mpg", "gear", "qsec"]).Columns.Count);
        }


        [TestMethod]
        public void TestColumnNames()
        {
            var df = Df.MtCars;
            var expected = df.Columns.Select(c => c.Name).ToList();

            CollectionAssert.AreEqual(expected, df.Names().ToList());
        }
    }
}
