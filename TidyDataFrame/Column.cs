
using Microsoft.Data.Analysis;

namespace TidyDataFrame
{
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
    }
}
