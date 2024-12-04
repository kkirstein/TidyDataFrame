using Microsoft.Data.Analysis;

namespace TidyDataFrame.Examples
{
    public static class Df
    {
        public static DataFrame ReligIncome
        {
            get
            {
                using var inp = File.OpenRead(""".\Datasets\relig_income.csv""");
                return DataFrame.LoadCsv(inp);
            }
        }

        public static DataFrame MtCars
        {
            get
            {
                using var inp = File.OpenRead(""".\Datasets\mtcars.csv""");
                return DataFrame.LoadCsv(inp,
                    dataTypes:
                        [typeof(string), typeof(float), typeof(int), typeof(float),
                        typeof(int), typeof(float), typeof(float), typeof(float),
                        typeof(int), typeof(int), typeof(int), typeof(int)]);
            }
        }
    }
}
