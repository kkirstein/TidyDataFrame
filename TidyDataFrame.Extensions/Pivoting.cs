using Microsoft.Data.Analysis;
using System.Runtime.CompilerServices;

namespace TidyDataFrame.Extensions
{

    public static class Pivoting
    {

        public static DataFrame PivotLonger(this DataFrame df, List<string> cols, string names_to = "Names", string values_to = "Values")
        {
            throw new NotImplementedException();
        }

        public static DataFrame PivotWider(this DataFrame df, List<string> cols, string names_from)
        {
            throw new NotImplementedException();
        }
    }

}
