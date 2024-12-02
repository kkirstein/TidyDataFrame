using Microsoft.Data.Analysis;
using System.Runtime.CompilerServices;

namespace TidyDataFrame.Extensions
{
    public static class Pivoting
    {
        /// <inheritdoc cref="TidyDataFrame.Pivoting.ToLonger(DataFrame, List{string}, string, string)"/>
        public static DataFrame PivotLonger(this DataFrame df, List<string> cols, string namesTo = "Names", string valuesTo = "Values")
        {
            return TidyDataFrame.Pivoting.ToLonger(df, cols, namesTo, valuesTo);
        }

        /// <inheritdoc cref="TidyDataFrame.Pivoting.ToWider(DataFrame, string, string, bool)"/>
        public static DataFrame PivotWider(this DataFrame df, string namesFrom, string valuesFrom, bool allowMultipleEntries)
        {
            return TidyDataFrame.Pivoting.ToWider(df, namesFrom, valuesFrom, allowMultipleEntries);
        }
    }

}
