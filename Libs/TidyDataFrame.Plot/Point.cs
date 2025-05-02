using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame.Plot
{
    public class Point : Geometry
    {
        public override List<AestheticMapping> RequiredAesthetics => [
            AestheticMapping.XCoord,
            AestheticMapping.YCoord];

        public override List<AestheticMapping> OptionalAesthetics => [
            AestheticMapping.Color,
            AestheticMapping.Size,
            AestheticMapping.Shape];
    }
}
