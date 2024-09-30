using Microsoft.Data.Analysis;
using System.Runtime.CompilerServices;

namespace TidyDataFrame.Extensions
{

    public static class Pivoting
    {

        public static DataFrame PivotLonger(this DataFrame df, List<string> cols, string namesTo = "Names", string valuesTo = "Values")
        {
            throw new NotImplementedException();
        }

        public static DataFrame PivotWider(this DataFrame df, List<string> cols, string namesFrom)
        {
            throw new NotImplementedException();
        }
    }

}
