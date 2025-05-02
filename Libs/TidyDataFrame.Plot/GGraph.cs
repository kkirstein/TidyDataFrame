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
    public class GGraph : IAesthetic<GGraph>, IGeometry<GGraph>
    {
        #region Properties

        //public List<IGeometry> Geometries { get { return _geometries; } }

        //public Dictionary<AestheticMapping, string> Aesthetics { get { return _aesthetics; } }

        #endregion

        #region private fields

        private DataFrame? _data;
        private List<Geometry> _geometries;
        private Dictionary<AestheticMapping, string> _aesthetics;

        #endregion

        public GGraph() : this(null) { }

        public GGraph(DataFrame? data)
        {
            _geometries = new List<Geometry>();
            _aesthetics = new Dictionary<AestheticMapping, string>();

            _data = data;
        }

        #region IGeometry methods

        public GGraph Geometry(Geometry geometry)
        {
            _geometries.Add(geometry);

            return this;
        }

        public bool HasValidAesthetics()
        {
            return _geometries.SelectMany(g => g.RequiredAesthetics).Select(aes => _aesthetics.ContainsKey(aes)).All(x => x);
        }

        #endregion

        #region IAesthetics methods
        public GGraph Aesthetics(AestheticMapping mapping, string dataColumn)
        {
            _aesthetics.Add(mapping, dataColumn);

            return this;
        }

        public GGraph Aesthetics(Dictionary<AestheticMapping, string> aesthetics)
        {
            aesthetics.ToList().ForEach(x => Aesthetics(x.Key, x.Value));

            return this;
        }

        #endregion

        #region IScale methods
        // TODO
        public GGraph Scale(Scale scale)
        {
            throw new NotImplementedException();
        }

        #endregion

        public Plot Render(IPlotBackend backend)
        {
            throw new NotImplementedException();
        }

    }
}
