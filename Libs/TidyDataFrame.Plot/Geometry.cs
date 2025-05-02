using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace TidyDataFrame.Plot
{
    /// <summary>
    /// Abstract base class for geometries
    /// </summary>
    public abstract class Geometry
    {
        protected DataFrame? _overloadedData;

        public abstract List<AestheticMapping> RequiredAesthetics { get; }
        public abstract List<AestheticMapping> OptionalAesthetics { get; }

        public Geometry(DataFrame? overloadedData)
        {
            _overloadedData = overloadedData;
        }

        public Geometry() : this(null) { }

        #region abstract methods

        #endregion
    }
}
