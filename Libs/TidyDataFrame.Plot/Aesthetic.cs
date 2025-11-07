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
        Size,
        Shape,
        LineType,
    }

    /// <summary>
    /// Aestathetics descirbe the mapping of data columns to a geometry property
    /// </summary>
    public abstract class Aesthetic
    {
        //public void SetScale(Scale scale);
        //public Scale GetScale();

    }
}
