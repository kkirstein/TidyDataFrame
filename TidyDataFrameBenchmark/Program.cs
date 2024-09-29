
using Microsoft.Data.Analysis;
using TidyDataFrame;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection;

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

        public record WideDataRecord : IDataRecord
        {
            public double Value0 { get; set; }
            public string Name0 { get; set; }
            public int Id0 { get; set; }
            public double Value1 { get; set; }
            public string Name1 { get; set; }
            public int Id1 { get; set; }
            public double Value2 { get; set; }
            public string Name2 { get; set; }
            public int Id2 { get; set; }
            public double Value3 { get; set; }
            public string Name3 { get; set; }
            public int Id3 { get; set; }
            public double Value4 { get; set; }
            public string Name4 { get; set; }
            public int Id4 { get; set; }
            public double Value5 { get; set; }
            public string Name5 { get; set; }
            public int Id5 { get; set; }
            public double Value6 { get; set; }
            public string Name6 { get; set; }
            public int Id6 { get; set; }
            public double Value7 { get; set; }
            public string Name7 { get; set; }
            public int Id7 { get; set; }
            public double Value8 { get; set; }
            public string Name8 { get; set; }
            public int Id8 { get; set; }
            public double Value9 { get; set; }
            public string Name9 { get; set; }
            public int Id9 { get; set; }
        }

        private List<WideDataRecord> GenerateWideDataRecords(int count)
        {
            var records = new List<WideDataRecord>();
            var rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                var rec = new WideDataRecord();
                for (int f = 0; f < 10; f++)
                {
                    var type = rec.GetType();
                    var prop = type.GetProperty($"Value{f:d1}");
                    prop?.SetValue(rec, rnd.NextDouble());
                    prop = type.GetProperty($"Name{f:d1}");
                    prop?.SetValue(rec, $"Name_{rnd.NextInt64(99)}");
                    prop = type.GetProperty($"Id{f:d1}");
                    prop?.SetValue(rec, (int)rnd.NextInt64(int.MaxValue));
                }
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

    internal class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            var dut = new DataFrameBuilderBench();
            Console.WriteLine("done!");
#else
            var summary = BenchmarkRunner.Run<DataFrameBuilderBench>();
#endif
        }
    }
}
