
using TidyDataFrame;
using TidyDataFrame.Examples;

namespace TidyDataFrame.Tests
{
    [TestClass]
    public class TestTypeCoercing
    {

        [TestMethod]
        public void TestStringCoercing()
        {
            var df = Df.MtCars;

            Assert.AreEqual(typeof(string), df["Column0"][0].GetType());

            var coercedType = TypeCoercing.CheckType(typeof(string), df["Column0"]);
            Assert.AreEqual(new TypeCoercing.TypeMatch(typeof(string)), coercedType);

            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                TypeCoercing.CheckType(typeof(string), df["mpg"]));
            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                coercedType.Check(df["mpg"]));

            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                TypeCoercing.CheckType(typeof(string), df["cyl"]));
            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                coercedType.Check(df["cyl"]));
        }

        [TestMethod]
        public void TestIntCoercing()
        {
            var df = Df.MtCars;

            Assert.AreEqual(typeof(int), df["cyl"][0].GetType());

            var coercedType = TypeCoercing.CheckType(typeof(int), df["cyl"]);
            Assert.AreEqual(new TypeCoercing.TypeMatch(typeof(int)), coercedType);

            Assert.AreEqual(new TypeCoercing.TypeIncompatible(),
                TypeCoercing.CheckType(typeof(int), df["wt"]));
            Assert.AreEqual(new TypeCoercing.TypeIncompatible(),
                coercedType.Check(df["wt"]));

            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                TypeCoercing.CheckType(typeof(int), df["Column0"]));
            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                coercedType.Check(df["Column0"]));
        }

        [TestMethod]
        public void TestFloatCoercing()
        {
            var df = Df.MtCars;

            Assert.AreEqual(typeof(float), df["mpg"][0].GetType());

            var coercedType = TypeCoercing.CheckType(typeof(float), df["mpg"]);
            Assert.AreEqual(new TypeCoercing.TypeMatch(typeof(float)), coercedType);

            Assert.AreEqual(new TypeCoercing.TypeIncompatible(),
                TypeCoercing.CheckType(typeof(float), df["cyl"]));
            Assert.AreEqual(new TypeCoercing.TypeIncompatible(),
                coercedType.Check(df["cyl"]));

            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                TypeCoercing.CheckType(typeof(float), df["Column0"]));
            Assert.AreEqual(new TypeCoercing.CoerceTo(typeof(string)),
                coercedType.Check(df["Column0"]));
        }
    }
}
