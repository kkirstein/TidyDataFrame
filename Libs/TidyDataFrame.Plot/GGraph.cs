using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace TidyDataFrame.Plot
{
    /// <summary>
    /// Definition of the graph, as described by the Grammar of Graphics
    /// </summary>
    public class GGraph
    {
        #region Properties

        public List<IGeometry> Geometries { get; }

        public List<IAesthetic> Aesthetics { get; }

        #endregion

        #region private fields

        private DataFrame? _data;

        #endregion

        public GGraph() : this(null) { }

        public GGraph(DataFrame? data)
        {
            Geometries = new List<IGeometry>();
            Aesthetics = new List<IAesthetic>();

            _data = data;
        }

        #region Methods

        public GGraph Geometry(IGeometry geometry)
        {
            throw new NotImplementedException();
        }

        public GGraph Aesthestics(IEnumerable<IAesthetic> aesthetics)
        {
            throw new NotImplementedException();
        }

        public GGraph Scale(Scale scale)
        {
            throw new NotImplementedException();
        }

        public Plot Render(IPlotBackend backend)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
