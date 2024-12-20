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
    public class TestMutate
    {
        [TestMethod]
        [Ignore]
        public void TestFun2()
        {
            var df = Df.MtCars;

            //Func<(float, int), float> f = arg => arg.Item1 / arg.Item2;
            var df2 = Mutate.Fun2<float, int, float>(df, "DispPerCyl", ("disp", "cyl"), x => x.Item1 / x.Item2);

            Assert.Equals(df.Columns.Count + 1, df2.Columns.Count);
            Assert.AreEqual(df.Rows.Count, df2.Rows.Count);
        }
    }
}
