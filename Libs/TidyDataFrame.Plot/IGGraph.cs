using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame.Plot
{
    public interface IGGraph<TSelf> where TSelf : IGGraph<TSelf>
    {
        /// <summary>
        /// Geometry is an element of the plot representing data, e.g., a point at coordinates, color, shape.
        /// </summary>
        /// Each geometry requires a certain set of aesthetics to map a data column to a geometrc property, e.g,
        /// x/y-coordinates, geometry color, or shape.
        public TSelf Geometry(Geometry geom);

        /// <summary>
        /// Checks if all required aesthetics for all geometries have been provided
        /// </summary>
        /// <returns>True, if required aesthetics are set</returns>
        public bool HasValidAesthetics();

        /// <summary>
        /// Aestathetics describe the mapping of data columns to a geometry property
        /// </summary>
        public TSelf Aesthetics(AestheticMapping mapping, string dataColumn);
        public TSelf Aesthetics(Dictionary<AestheticMapping, string> aesthetics);

        public TSelf Scale(Scale scale);
    }
}
