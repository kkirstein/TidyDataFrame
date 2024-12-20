
using TidyDataFrame.Examples;

namespace TidyDataFrame.Tests
{
    [TestClass]
    public class TestExamples
    {
        [TestMethod]
        public void TestReligIncomeDataset()
        {
            var df = Df.ReligIncome;

            Assert.IsNotNull(df);
            Assert.AreEqual(12, df.Columns.Count);
            Assert.AreEqual(18, df.Rows.Count);

            var colNames = new List<string> {
                "religion",
                "<$10k",
                "$10-20k",
                "$20-30k",
                "$30-40k",
                "$40-50k",
                "$50-75k",
                "$75-100k",
                "$100-150k",
                ">150k",
                "Don't know/refused"
            };

            foreach (var col in colNames) {
                Assert.IsNotNull(df[col]);
            }
        }


        [TestMethod]
        public void TestMtCarsDataset()
        {
            var df = Df.MtCars;

            Assert.IsNotNull(df);
            Assert.AreEqual(12, df.Columns.Count);
            Assert.AreEqual(32, df.Rows.Count);

            var colNames = new List<string> {
                "mpg",
                "cyl",
                "disp",
                "hp",
                "drat",
                "wt",
                "qsec",
                "vs",
                "am",
                "gear",
                "carb"
            };

            foreach (var col in colNames) {
                Assert.IsNotNull(df[col]);
            }
        }
    }
}