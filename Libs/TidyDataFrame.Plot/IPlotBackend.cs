using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame.Plot
{
    public enum PlotTarget
    {
    }

    /// <summary>
    /// Interface to the underlying plotting framework to render the plot
    /// </summary>
    public interface IPlotBackend
    {
        /// <summary>
        /// Basic method to render a GGraph into a Plot object
        /// </summary>
        /// <param name="graph">Graph definition object</param>
        /// <param name="target">Target output format</param>
        /// <returns>Rendered Plot object, e.g., a raster graphic</returns>
        public IPlot Render<T>(IGGraph<T> graph, PlotTarget target) where T : IGGraph<T>;
    }
}
