
using TidyDataFrame;
using TidyDataFrame.Examples;

namespace TidyDataFrameTests
{
    [TestClass]
    public class TestTypeCoercing
    {

        [TestMethod]
        public void TestStringCoercing()
        {
            var df = Df.MtCars;

            var coercedType = TypeCoercing.CheckType(typeof(string), df["Column0"]);
            Assert.AreEqual(new TypeCoercing.TypeMatch(typeof(string)), coercedType);

            coercedType = coercedType.Check(df["mpg"]);
            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)), coercedType);
        }

    }
}
