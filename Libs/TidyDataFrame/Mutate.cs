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
            if (!Column.TryToEnumerable<B>(df, columns.Item2, out var data2))
            {
                throw new InvalidDataTypeException($"Column {columns.Item2} could not be cast to {typeof(B)}");
            }

            // static dispatch does not work here, so we have to use dynamic
            var newData = data1.Zip(data2).Select(fun).Select(x => (dynamic)x);

            // add data frame column
            var newColumn = Column.ToDataFrameColumn(newData, newName);
            return new DataFrame(df.Columns.Append(newColumn));
        }

        public static DataFrame Fun<A, B, R>(DataFrame df, string newName, (string, string) columns, Func<A, B, R> fun)
            //where A : unmanaged
            //where B : unmanaged
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
            if (!Column.TryToEnumerable<B>(df, columns.Item2, out var data2))
            {
                throw new InvalidDataTypeException($"Column {columns.Item2} could not be cast to {typeof(B)}");
            }

            dynamic[] newData = new dynamic[rowCount];
            var aryData1 = data1.ToArray();
            var aryData2 = data2.ToArray();
            for (var idx = 0; idx < rowCount; ++idx)
            {
                newData[idx] = fun(aryData1[idx], aryData2[idx]);
            }

            // add data frame column
            var newColumn = Column.ToDataFrameColumn(newData, newName);
            return new DataFrame(df.Columns.Append(newColumn));
        }

        public static DataFrame Fun<A, B, C, R>(DataFrame df, string newName, (string, string, string) columns, Func<A, B, C, R> fun)
            //where A : unmanaged
            //where B : unmanaged
            //where C : unmanaged
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
            if (!colNames.Contains(columns.Item3))
            {
                throw new InvalidDataException($"{columns.Item3} does not exist in data frame ({colNames})");
            }

            // generate new data collection
            if (!Column.TryToEnumerable<A>(df, columns.Item1, out var data1))
            {
                throw new InvalidDataTypeException($"Column {columns.Item1} could not be cast to {typeof(A)}");
            }
            if (!Column.TryToEnumerable<B>(df, columns.Item2, out var data2))
            {
                throw new InvalidDataTypeException($"Column {columns.Item2} could not be cast to {typeof(B)}");
            }
            if (!Column.TryToEnumerable<C>(df, columns.Item3, out var data3))
            {
                throw new InvalidDataTypeException($"Column {columns.Item3} could not be cast to {typeof(C)}");
            }

            dynamic[] newData = new dynamic[rowCount];
            var aryData1 = data1.ToArray();
            var aryData2 = data2.ToArray();
            var aryData3 = data3.ToArray();
            for (var idx = 0; idx < rowCount; ++idx)
            {
                newData[idx] = fun(aryData1[idx], aryData2[idx], aryData3[idx]);
            }

            // add data frame column
            var newColumn = Column.ToDataFrameColumn(newData, newName);
            return new DataFrame(df.Columns.Append(newColumn));
        }
    }
}
