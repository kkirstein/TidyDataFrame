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

            // check cols are valid
            foreach (var colName in cols)
            {
                if (!colNames.Contains(colName))
                {
                    throw new InvalidDataException($"{colName} does not exist in data frame ({colNames})");
                }
            }

            // TODO: check type compatibility
            // ..

            var pivotNames = colNames.Where(x => !cols.Contains(x)).ToList();
            var builder = new DictDataFrameBuilder(false);

            foreach (var row in df.Rows)
            {
                var currentPivot = pivotNames.ToDictionary(n => n, n => row[n]);

                foreach (var col in cols)
                {
                    var newRow = new Dictionary<string, object> { [namesTo] = col, [valuesTo] = row[col] }.Concat(currentPivot).ToDictionary();
                    builder.Add(newRow);
                }
            }

            return builder.ToDataFrame();


                //var selectedCols = Column.Take(df, cols);


                //// TODO: compile remaining columns
                //var remainingCols = Column.Drop(df, cols);
                //var longDataCount = cols.Count * rowCount;
                ////var remainingColsLong = new List<DataFrameColumn>();

                //// TODO: check column types are compatible
                //var coercedType = typeof(Nullable<double>);

                //// TODO: collect pivoted data
                //long[] idx = new long[longDataCount];
                //foreach (var i in Enumerable.Range(0, (int)rowCount)) // FIXME: use long type
                //{
                //    foreach (var j in Enumerable.Range(0, cols.Count))
                //    {
                //        // TODO: generate index
                //    }
                //}


                //var nameColumn = new StringDataFrameColumn(namesTo, longDataCount);

                //var longDf = new DataFrame(remainingCols.Columns);
                //switch (coercedType)
                //{
                //    case Type _ when coercedType == typeof(double) || coercedType == typeof(double?):
                //        var col = new DoubleDataFrameColumn(valuesTo, longDataCount);
                //        //longDf.Add(col);
                //        break;
                //    case Type _ when coercedType == typeof(int) || coercedType == typeof(int?):
                //        var intCol = new Int64DataFrameColumn(valuesTo, longDataCount);
                //        break;
                //    case Type _ when coercedType == typeof(string):
                //        var stringCol = new DoubleDataFrameColumn(valuesTo, longDataCount);
                //        break;
                //    default: throw new ApplicationException($"Unsupported type {nameof(coercedType)}");
                //};

                //throw new NotImplementedException();

            }

        public static DataFrame ToWider(DataFrame df, List<string> cols, string namesFrom)
        {
            throw new NotImplementedException();
        }
    }
}
