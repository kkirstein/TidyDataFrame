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
                return DataFrame.LoadCsv(""".\Datasets\mtcars.csv""");
            }
        }
    }
}
