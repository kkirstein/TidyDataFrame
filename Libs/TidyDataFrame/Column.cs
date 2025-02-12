
using System.Collections.Generic;
using Microsoft.Data.Analysis;
using System.Diagnostics.CodeAnalysis;

namespace TidyDataFrame
{
    /// <summary>
    /// Manipulate column set of a data frame
    /// </summary>
    public static class Column
    {
        /// <summary>
        /// Remove data column of given name from data frame
        /// </summary>
        /// If the given column name does not exist in the data frame, it is
        /// silently ignored and the orginal columns are returned.
        /// <param name="df">Data frame with column to be removed</param>
        /// <param name="columnName">Name of column to be removed</param>
        /// <returns>Data frame without given column name</returns>
        public static DataFrame Drop(DataFrame df, string columnName)
        {
            var selectedCols = df.Columns.Where(c => c.Name != columnName);
            return new DataFrame(selectedCols);
        }

        /// <summary>
        /// Remove data columns of given names fomr data frame
        /// </summary>
        /// If the any of the column names do not exist in the data frame, it silently
        /// ignores those colujmn names.
        /// <param name="df">Data frame with column to be removed</param>
        /// <param name="columnNames">An IEnumarable with the column names to be removed</param>
        /// <returns>Data frame without given column names</returns>
        public static DataFrame Drop(DataFrame df, IEnumerable<string> columnNames)
        {
            var selectedCols = df.Columns.Where(c => !columnNames.Contains(c.Name));
            return new DataFrame(selectedCols);
        }

        /// <summary>
        /// Select data columns of given data frame
        /// </summary>
        /// This function is named `Take` instead of the tidy verb `Select` in order
        /// to avoid confusion with the LINQ command `Select`.
        /// If a given column name does not exist, it is silently ignored.
        /// <param name="df">Data frame with columns to be selected</param>
        /// <param name="columnNames">Name of columns to be selected</param>
        /// <returns>Data frame with selected columns</returns>
        public static DataFrame Take(DataFrame df, IEnumerable<string> columnNames)
        {
            var selectedCols = df.Columns.Where(c => columnNames.Contains(c.Name));
            return new DataFrame(selectedCols);
        }


        /// <summary>
        /// Convert a data frame column to an enumerable of respective data type
        /// </summary>
        /// <typeparam name="T">Data type of column data</typeparam>
        /// <param name="df">Data frame with selected column</param>
        /// <param name="name">Nam eof slected column</param>
        /// <returns>Enumarable with selected column data</returns>
        /// <exception cref="InvalidDataException">Raised, when data column does not exist</exception>
        public static IEnumerable<T> ToEnumerable<T>(DataFrame df, string name)
        {
            var colNames = df.Columns.Select(c => c.Name);
            if (!colNames.Contains(name))
            {
                throw new InvalidDataException($"{name} does not exist in data frame ({colNames})");
            }

            var length = df[name].Length;
            var type = df[name].DataType;

            var data = Enumerable.Range(0, (int)length).Select(i => (T)df[name][i]);
            return data;
        }

        /// <summary>
        /// Try to convert a data frame column to an enumerable of respective data type
        /// </summary>
        /// <returns>True, if column exists and conversion was successfull</returns>
        /// <inheritdoc cref="ToEnumerable{T}(DataFrame, string)"/>
        public static bool TryToEnumerable<T>(DataFrame df, string name,
            [NotNullWhen(true)] out IEnumerable<T>? data)
        {
            try
            {
                data = ToEnumerable<T>(df, name);
                return true;
            }
            catch (InvalidCastException)
            {
                data = null;
                return false;
            }
        }


        /// <summary>
        /// Convert given enumerable to a data frame column of matching data type
        /// </summary>
        /// <typeparam name="T">Data type of given values</typeparam>
        /// <param name="data">Data to be converted</param>
        /// <param name="name">Name of data frame column</param>
        /// <returns>A data frame column</returns>
        /// <exception cref="InvalidDataTypeException">Raised, when 'data' is of unsupported type</exception>
        public static DataFrameColumn ToDataFrameColumn<T>(IEnumerable<T> data, string name)
            where T : unmanaged
        {
            throw new InvalidDataTypeException($"Unsupported type '{typeof(T)}' for column '{name}'");
        }

        /// <summary>
        /// Convert given enumerable to a data frame column of matching data type
        /// </summary>
        /// <typeparam name="T">Data type of given values</typeparam>
        /// <param name="data">Data to be converted</param>
        /// <param name="name">Name of data frame column</param>
        /// <returns>A data frame column</returns>
        /// <exception cref="InvalidDataException">Raised, when 'data' is empty</exception>"
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<dynamic> data, string name)
        {
            if (data == null || data.Count() == 0)
            {
                throw new InvalidDataException($"Data of column'{name}' is empty");
            }
            var first = data.First();
            return first switch
            {
                string => new StringDataFrameColumn(name,data.Select(x=>(string)x)),
                double => new DoubleDataFrameColumn(name,data.Select(x=>(double)x)),
                float => new SingleDataFrameColumn(name, data.Select(x=>(float)x)),
                int => new Int32DataFrameColumn(name, data.Select(x => (int)x)),
                _ => throw new InvalidDataTypeException($"Unsupported type '{first.GetType()}' for column '{name}'")
            };
        }

        /// <inheritdoc cref="ToDataFrameColumn(IEnumerable{dynamic}, string)"/>
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<string> data, string name)
        {
            return new StringDataFrameColumn(name, data);
        }

        /// <inheritdoc cref="ToDataFrameColumn(IEnumerable{dynamic}, string)"/>
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<double> data, string name)
        {
            return new DoubleDataFrameColumn(name, data);
        }

        /// <inheritdoc cref="ToDataFrameColumn(IEnumerable{dynamic}, string)"/>
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<double?> data, string name)
        {
            return new DoubleDataFrameColumn(name, data);
        }

        /// <inheritdoc cref="ToDataFrameColumn(IEnumerable{dynamic}, string)"/>
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<float> data, string name)
        {
            return new SingleDataFrameColumn(name, data);
        }

        /// <inheritdoc cref="ToDataFrameColumn(IEnumerable{dynamic}, string)"/>
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<float?> data, string name)
        {
            return new SingleDataFrameColumn(name, data);
        }

        /// <inheritdoc cref="ToDataFrameColumn(IEnumerable{dynamic}, string)"/>
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<int> data, string name)
        {
            return new Int32DataFrameColumn(name, data);
        }

        /// <inheritdoc cref="ToDataFrameColumn(IEnumerable{dynamic}, string)"/>
        public static DataFrameColumn ToDataFrameColumn(IEnumerable<int?> data, string name)
        {
            return new Int32DataFrameColumn(name, data);
        }
    }
}

