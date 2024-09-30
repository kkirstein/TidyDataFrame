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
            var rowCount = df.Rows.Count;
            var colNames = df.Columns.Select(c => c.Name).ToList();

            // TODO: check cols are valid
            foreach (var colName in cols)
            {
                if (!colNames.Contains(colName))
                {
                    throw new InvalidDataException($"{colName} does not exist in data frame ({colNames})");
                }
            }

            // TODO: compile remaining columns
            var longDataCount = cols.Count * rowCount;
            var remainingCols = new List<DataFrameColumn>();

            // TODO: check column types are compatible
            var coercedType = typeof(Nullable<double>);

            // TODO: collect pivoted data
            long[] idx = new long[longDataCount];
            foreach (var i in Enumerable.Range(0, (int)rowCount)) // FIXME: use long type
            {
                foreach (var j in Enumerable.Range(0, cols.Count))
                {
                    // TODO: generate index
                }
            }


            var nameColumn = new StringDataFrameColumn(namesTo, longDataCount);

            var longDf = new DataFrame(remainingCols);
            switch (coercedType)
            {
                case Type _ when coercedType == typeof(double) || coercedType == typeof(double?):
                    var col = new DoubleDataFrameColumn(valuesTo, longDataCount);
                    //longDf.Add(col);
                    break;
                case Type _ when coercedType == typeof(int) || coercedType == typeof(int?):
                    var intCol = new Int64DataFrameColumn(valuesTo, longDataCount);
                    break;
                case Type _ when coercedType == typeof(string):
                    var stringCol = new DoubleDataFrameColumn(valuesTo, longDataCount);
                    break;
                default: throw new ApplicationException($"Unsupported type {nameof(coercedType)}");
            };

            throw new NotImplementedException();

        }

        public static DataFrame ToWider(DataFrame df, List<string> cols, string namesFrom)
        {
            throw new NotImplementedException();
        }
    }
}
