using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame.Plot
{
    /// <summary>
    /// Geometry is an element of the plot representing data, e.g., a point at coordinates, color, shape.
    /// </summary>
    /// Each geometry requires a certain set of aesthetics to map a data column to a geometrc property, e.g,
    /// x/y-coordinates, geometry color, or shape.
    public interface IGeometry<T>
    {
        public T Geometry(Geometry geom);

        public bool HasValidAesthetics();
    }
}
