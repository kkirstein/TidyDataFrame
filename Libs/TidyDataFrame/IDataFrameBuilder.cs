
namespace TidyDataFrame
{
    public interface IDataFrameBuilder<TDataRecord>
    {
        /// <summary>
        /// Column names and type specification
        /// </summary>
        public Dictionary<string, Type> ColumnSpec { get; }

        /// <summary>
        /// Add a single data record to the builder
        /// </summary>
        /// <param name="record">Data record to be added</param>
        public void Add(TDataRecord record);

        /// <summary>
        /// Add a collection of data records to the builder
        /// </summary>
        /// <param name="records">Collection of data records</param>
        public void Add(ICollection<TDataRecord> records);

        //public void Freeze();

        /// <summary>
        /// Converts builder to a DataFrame.
        /// </summary>
        /// Data is copied from the supplied data records to the new DataFrame columns.
        /// <returns>A DataFrame of the collected data records</returns>
        public Microsoft.Data.Analysis.DataFrame ToDataFrame();
    }
}
