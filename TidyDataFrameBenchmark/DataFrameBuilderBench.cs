
using Microsoft.Data.Analysis;
using TidyDataFrame;
using BenchmarkDotNet.Attributes;

namespace TidyDataFrame.Benchmark
{
    public class DataFrameBuilderBench
    {
        public interface IDataRecord { }

        public record SmallDataRecord(
            double Value,
            string Name,
            int Id) : IDataRecord;

        private List<SmallDataRecord> GenerateDataRecords(int count)
        {
            var records = new List<SmallDataRecord>();
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                records.Add(new(
                    rnd.NextDouble(),
                    $"Name_{rnd.NextInt64(99)}",
                    (int)rnd.NextInt64(int.MaxValue)));
            }
            return records;
        }

        public record WideDataRecord(
            double Value0,
            string Name0,
            int Id0,
            double Value1,
            string Name1,
            int Id1,
            double Value2,
            string Name2,
            int Id2,
            double Value3,
            string Name3,
            int Id3,
            double Value4,
            string Name4,
            int Id4,
            double Value5,
            string Name5,
            int Id5,
            double Value6,
            string Name6,
            int Id6,
            double Value7,
            string Name7,
            int Id7,
            double Value8,
            string Name8,
            int Id8,
            double Value9,
            string Name9,
            int Id9
            ) : IDataRecord;

        private List<WideDataRecord> GenerateWideDataRecords(int count)
        {
            var records = new List<WideDataRecord>();
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                var rec = new WideDataRecord(
                    Value0: rnd.NextDouble(),
                    Name0: $"Name_{rnd.NextInt64(99)}",
                    Id0: (int)rnd.NextInt64(int.MaxValue),
                    Value1: rnd.NextDouble(),
                    Name1: $"Name_{rnd.NextInt64(99)}",
                    Id1: (int)rnd.NextInt64(int.MaxValue),
                    Value2: rnd.NextDouble(),
                    Name2: $"Name_{rnd.NextInt64(99)}",
                    Id2: (int)rnd.NextInt64(int.MaxValue),
                    Value3: rnd.NextDouble(),
                    Name3: $"Name_{rnd.NextInt64(99)}",
                    Id3: (int)rnd.NextInt64(int.MaxValue),
                    Value4: rnd.NextDouble(),
                    Name4: $"Name_{rnd.NextInt64(99)}",
                    Id4: (int)rnd.NextInt64(int.MaxValue),
                    Value5: rnd.NextDouble(),
                    Name5: $"Name_{rnd.NextInt64(99)}",
                    Id5: (int)rnd.NextInt64(int.MaxValue),
                    Value6: rnd.NextDouble(),
                    Name6: $"Name_{rnd.NextInt64(99)}",
                    Id6: (int)rnd.NextInt64(int.MaxValue),
                    Value7: rnd.NextDouble(),
                    Name7: $"Name_{rnd.NextInt64(99)}",
                    Id7: (int)rnd.NextInt64(int.MaxValue),
                    Value8: rnd.NextDouble(),
                    Name8: $"Name_{rnd.NextInt64(99)}",
                    Id8: (int)rnd.NextInt64(int.MaxValue),
                    Value9: rnd.NextDouble(),
                    Name9: $"Name_{rnd.NextInt64(99)}",
                    Id9: (int)rnd.NextInt64(int.MaxValue)
                    );
                records.Add(rec);
            }
            return records;
        }

        private List<SmallDataRecord> _records1, _records2;
        private List<WideDataRecord> _wideRecords1, _wideRecords2;


        public DataFrameBuilderBench()
        {
            _records1 = GenerateDataRecords(1000);
            _records2 = GenerateDataRecords(10000);
            _wideRecords1 = GenerateWideDataRecords(1000);
            _wideRecords2 = GenerateWideDataRecords(10000);
        }

        //[ParamsSource(nameof(Records))]
        //public List<IDataRecord> A { get; set; }

        //public IEnumerable<List<IDataRecord>> Records => new[] { _records1, _records2, _wideRecords1, _wideRecords2 };


        [Benchmark]
        public DataFrame BySingleAddSmall()
        {
            var builder = new DataFrameBuilder<SmallDataRecord>();
            foreach (var r in _records2)
            {
                builder.Add(r);
            }
            return builder.ToDataFrame();
        }

        [Benchmark]
        public DataFrame BySingleAddWide()
        {
            var builder = new DataFrameBuilder<WideDataRecord>();
            foreach (var r in _wideRecords2)
            {
                builder.Add(r);
            }
            return builder.ToDataFrame();
        }

        [Benchmark]
        public DataFrame ByCollectionAddSmall()
        {
            var builder = new DataFrameBuilder<SmallDataRecord>();
            builder.Add(_records2);
            return builder.ToDataFrame();
        }

        [Benchmark]
        public DataFrame ByCollectionAddWide()
        {
            var builder = new DataFrameBuilder<WideDataRecord>();
            builder.Add(_wideRecords2);
            return builder.ToDataFrame();
        }
    }

}

