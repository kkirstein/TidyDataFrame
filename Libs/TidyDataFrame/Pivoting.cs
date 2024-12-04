using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Analysis;

namespace TidyDataFrame
{
    /// <summary>
    /// Handling and transforming data frames with pivotal data columns
    /// </summary>
    public static class Pivoting
    {
        /// <summary>
        /// Transforms a data frame into long format
        /// </summary>
        /// <param name="df">Data frame to be transformed</param>
        /// <param name="cols">List column names to be pivoted to long format</param>
        /// <param name="namesTo">Name of new column for data identifier</param>
        /// <param name="valuesTo">Name of new column for data values</param>
        /// <returns>A data frame in long format</returns>
        /// <exception cref="InvalidDataException">Thrown, if selected columns do not exist in 'df'</exception>
        public static DataFrame ToLonger(DataFrame df, List<string> cols, string namesTo = "Names", string valuesTo = "Values")
        {
            var rowCount = df.Rows.Count;
            var colNames = df.Columns.Select(c => c.Name).ToList();

            // check cols are valid
            foreach (var c in cols)
            {
                if (!colNames.Contains(c))
                {
                    throw new InvalidDataException($"{c} does not exist in data frame ({colNames})");
                }
            }

            // check type compatibility
            var initialType = new TypeCoercing.TypeMatch(df[cols.First()].DataType);
            var typeCheck = cols.Aggregate((TypeCoercing)initialType, (cur, next) => cur.Check(df[next]));
            if (typeCheck is TypeCoercing.TypeIncompatible)
            {
                throw new InvalidDataTypeException("Column types can not be coerced to common data type");
            }

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
        }

        /// <summary>
        /// Transforms a data frame into wide format
        /// </summary>
        /// <param name="df">Data frame to be transformed</param>
        /// <param name="namesFrom">Selects column with data identifiers, data type of this column must be 'string'</param>
        /// <param name="valuesFrom">Selects column with data values</param>
        /// <param name="allowMultipleEntries">Optionally allow multiple entries for a data identifier</param>
        /// <returns>A data frame in wide format</returns>
        /// <exception cref="InvalidDataException">Thrown, if selected columns do not exist in 'df'</exception>
        /// <exception cref="InvalidDataTypeException">Thrown, if values if 'namesFrom' column has not data type 'string'</exception>
        public static DataFrame ToWider(DataFrame df, string namesFrom, string valuesFrom, bool allowMultipleEntries = false)
        {
            var colNames = df.Columns.Select(c => c.Name).ToList();
            var cols = new List<string> { namesFrom, valuesFrom };

            // check cols are valid
            if (!colNames.Contains(namesFrom))
            {
                throw new InvalidDataException($"{namesFrom} does not exist in data frame ({colNames})");
            }
            if (df[namesFrom].DataType != typeof(string))
            {
                throw new InvalidDataTypeException($"Data column {namesFrom} must be 'string', but is {df[namesFrom].DataType}");
            }
            if (!colNames.Contains(valuesFrom))
            {
                throw new InvalidDataException($"{valuesFrom} does not exist in data frame ({colNames})");
            }

            var pivotNames = colNames.Where(x => !cols.Contains(x)).ToList();

            var lut = new Dictionary<int, Dictionary<string, object>>();

            foreach (var row in df.Rows)
            {
                // get a hash of pivot values
                var hashString = string.Join("-", pivotNames.Select(n => row[n].ToString()));
                var hash = hashString.GetHashCode();
                if (lut.ContainsKey(hash))
                {
                    if (!lut[hash].TryAdd((string)row[namesFrom], row[valuesFrom]))
                    {
                        if (!allowMultipleEntries)
                        {
                            throw new InvalidDataException($"Ambigious data, entry for {row[namesFrom]} already present");
                        }
                        // adjust hashString & add to lut
                        hashString += "_1";
                        hash = hashString.GetHashCode();
                        var entry = new Dictionary<string, object>();
                        foreach (var p in pivotNames)
                        {
                            entry.Add(p, row[p]);
                        }
                        entry.Add((string)row[namesFrom], row[valuesFrom]);
                        lut.Add(hash, entry);
                    }
                }
                else
                {
                    // add new entry to lut
                    var entry = new Dictionary<string, object>();
                    foreach (var p in pivotNames)
                    {
                        entry.Add(p, row[p]);
                    }
                    entry.Add((string)row[namesFrom], row[valuesFrom]);
                    lut.Add(hash, entry);
                }
            }

            var builder = new DictDataFrameBuilder(false);
            builder.Add(lut.Values);

            return builder.ToDataFrame();
        }
    }
}
