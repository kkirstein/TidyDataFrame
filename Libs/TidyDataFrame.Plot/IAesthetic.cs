using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame.Plot
{
    public enum AestheticMapping
    {
        XCoord,
        YCoord,
        Color,

    }

    /// <summary>
    /// Aestathetics descirbe the mapping of data columns to a geometry property
    /// </summary>
    public interface IAesthetic<T>
    {
        public T Aesthetics(AestheticMapping mapping, string dataColumn);
        public T Aesthetics(Dictionary<AestheticMapping, string> aesthetics);

        //public void SetScale(Scale scale);
        //public Scale GetScale();

    }
}
