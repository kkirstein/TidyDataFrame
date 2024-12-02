using Microsoft.Data.Analysis;

namespace TidyDataFrame.Examples
{
    public static class Df
    {
        public static DataFrame ReligIncome
        {
            get
            {
                return DataFrame.LoadCsv(""".\Datasets\relig_income.csv""");
            }
        }

        public static DataFrame MtCars
        {
            get
            {
                return DataFrame.LoadCsv(""".\Datasets\mtcars.csv""",
                    dataTypes:
                        [typeof(string), typeof(float), typeof(int), typeof(float),
                        typeof(int), typeof(float), typeof(float), typeof(float),
                        typeof(int), typeof(int), typeof(int), typeof(int)]);
            }
        }
    }
}
