using TidyDataFrame.Plot;
using TidyDataFrame.Examples;

namespace TidyDataFrame.Plot.Tests
{
    [TestClass]
    public sealed class ApiTest
    {
        [TestMethod]
        public void TestSimpleScatterPlot()
        {
            var data = Df.MtCars;
            var ggplot = new GGraph(data);

            ggplot.Aesthetics(new() {
                { AestheticMapping.XCoord, "displ" },
                { AestheticMapping.YCoord, "hwy" }
            }).Geometry(new Point());

            Assert.IsNotNull(ggplot);
            Assert.IsInstanceOfType(ggplot, typeof(GGraph));
            Assert.IsTrue(ggplot.HasValidAesthetics());
        }

        [TestMethod]
        public void TestSimpleLinePlot()
        {
            var data = Df.MtCars;
            var ggplot = new GGraph(data);

            ggplot.Aesthetics(new() {
                { AestheticMapping.XCoord, "displ" },
                { AestheticMapping.YCoord, "hwy" }
            }).Geometry(new Line());

            Assert.IsNotNull(ggplot);
            Assert.IsInstanceOfType(ggplot, typeof(GGraph));
            Assert.IsTrue(ggplot.HasValidAesthetics());
        }

        [TestMethod]
        public void TestMissingAesthetics()
        {
            var data = Df.MtCars;
            var ggplot = new GGraph(data);

            ggplot.Aesthetics(new() {
                { AestheticMapping.XCoord, "displ" },
            }).Geometry(new Point());

            Assert.IsNotNull(ggplot);
            Assert.IsInstanceOfType(ggplot, typeof(GGraph));
            Assert.IsFalse(ggplot.HasValidAesthetics());
        }
    }
}
