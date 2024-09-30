
using Microsoft.Data.Analysis;
using TidyDataFrame;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection;

namespace TidyDataFrame.Benchmark
{
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
