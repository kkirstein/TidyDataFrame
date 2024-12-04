
using Microsoft.Data.Analysis;
using System.Diagnostics;

namespace TidyDataFrame.Extensions
{
    public static class Columns
    {
        public static DataFrame DropColumn(this DataFrame df, string columnName)
        {
            return TidyDataFrame.Column.Drop(df, columnName);
        }

        public static DataFrame DropColumn(this DataFrame df, IEnumerable<string> columnNames)
        {
            return TidyDataFrame.Column.Drop(df, columnNames);
        }

        public static DataFrame TakeColumn(this DataFrame df, IEnumerable<string> columnNames)
        {
            return TidyDataFrame.Column.Take(df, columnNames);
        }

        public static IEnumerable<string> Names(this DataFrame df) => df.Columns.Select(c => c.Name);
       
    }
}
