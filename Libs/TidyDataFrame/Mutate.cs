using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame
{
    /// <summary>
    /// Generate new data frame columns from existing ones
    /// </summary>
    public static class Mutate
    {
        public static DataFrame Fun2<A, B, R>(DataFrame df, string newName, (string, string) columns, Func<(A, B), R> fun)
            where A : unmanaged
            where B : unmanaged
            where R : unmanaged
        {
            var rowCount = df.Rows.Count;
            var colNames = df.Columns.Select(c => c.Name).ToList();

            // check valid column names
            if (!colNames.Contains(columns.Item1))
            {
                throw new InvalidDataException($"{columns.Item1} does not exist in data frame ({colNames})");
            }
            if (!colNames.Contains(columns.Item2))
            {
                throw new InvalidDataException($"{columns.Item2} does not exist in data frame ({colNames})");
            }

            // generate new data collection
            if (!Column.TryToEnumerable<A>(df, columns.Item1, out var data1))
            {
                throw new InvalidDataTypeException($"Column {columns.Item1} could not be cast to {typeof(A)}");
            }
            if (!Column.TryToEnumerable<B>(df, columns.Item1, out var data2))
            {
                throw new InvalidDataTypeException($"Column {columns.Item2} could not be cast to {typeof(B)}");
            }
            var newData = data1.Zip(data2).Select(fun);

            // add data frame column
            var newColumn = Column.ToDataFrameColumn(newData, newName);
            return new DataFrame(df.Columns.Append(newColumn));
        }
    }
}
