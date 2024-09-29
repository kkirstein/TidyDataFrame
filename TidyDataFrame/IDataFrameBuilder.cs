
namespace TidyDataFrame
{
    public interface IDataFrameBuilder<TDataRecord>
    {
        public void Add(TDataRecord record);

        public void Add(ICollection<TDataRecord> records);

        public void Freeze();

        public Microsoft.Data.Analysis.DataFrame ToDataFrame();
    }
}
