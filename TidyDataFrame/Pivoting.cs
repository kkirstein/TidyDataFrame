using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace TidyDataFrame
{
    public static class Pivoting
    {
        public static DataFrame ToLonger(DataFrame df, List<string> cols, string namesTo = "Names", string valuesTo = "Values")
        {
            throw new NotImplementedException();
        }

        public static DataFrame ToWider(DataFrame df, List<string> cols, string namesFrom)
        {
            throw new NotImplementedException();
        }
    }
}
