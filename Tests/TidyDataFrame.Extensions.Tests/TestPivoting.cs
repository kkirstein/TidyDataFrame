using TidyDataFrame;
using TidyDataFrame.Extensions;
using TidyDataFrame.Examples;


namespace TidyDataFrame.Extensions.Tests
{
    [TestClass]
    public sealed class TestPivoting
    {
        [TestMethod]
        public void TestToLonger()
        {
            var df1 = Df.MtCars;

            var df2 = df1.PivotLonger(["cyl", "carb", "gear"]);
            var dfExpected = Pivot.ToLonger(df1, ["cyl", "carb", "gear"]);

            Assert.AreEqual(dfExpected.Columns.Count, df2.Columns.Count);
            Assert.AreEqual(dfExpected.Rows.Count, df2.Rows.Count);
            CollectionAssert.AreEqual(dfExpected.Names().ToList(), df2.Names().ToList());
        }
    }
}
