
using Microsoft.Data.Analysis;

namespace TidyDataFrame
{
    internal record TypeCoercing
    {
        internal record CoerceTo(Type T) : TypeCoercing();

        internal record TypeMatch(Type T) : TypeCoercing();

        internal record TypeIncompatible() : TypeCoercing();

        internal TypeCoercing Check(DataFrameColumn col)
        {
            return this switch
            {
                TypeIncompatible t => t,
                TypeMatch t => CheckType(t.T, col),
                CoerceTo t when CheckType(t.T, col) is TypeMatch _ => t,
                CoerceTo t when CheckType(t.T, col) is CoerceTo t2 => t2,
                CoerceTo t when CheckType(t.T, col) is TypeIncompatible t2 => t2,
                _ => throw new Exception("Unreachable")
            };
        }
        private TypeCoercing() { }

        internal static TypeCoercing CheckType(Type current, DataFrameColumn col)
        {
            return current switch
            {
                Type t when t == typeof(string) => col switch
                {
                    StringDataFrameColumn _ => new TypeMatch(typeof(string)),
                    _ => new CoerceTo(typeof(string)),
                },
                Type t when t == typeof(float) => col switch
                {
                    SingleDataFrameColumn _ => new TypeMatch(typeof(float)),
                    DoubleDataFrameColumn _ => new CoerceTo(typeof(double)),
                    StringDataFrameColumn _ => new CoerceTo(typeof(string)),
                    Object other => throw new ApplicationException($"Column type {other.GetType()} cannot be coerced to float")
                },
                Type t when t == typeof(double) => col switch
                {
                    SingleDataFrameColumn _ => new CoerceTo(typeof(double)),
                    DoubleDataFrameColumn _ => new TypeMatch(typeof(double)),
                    StringDataFrameColumn _ => new CoerceTo(typeof(string)),
                    Object other => throw new ApplicationException($"Column type {other.GetType()} cannot be coerced to double")
                },
                _ => throw new ApplicationException("Type {nameof(t)} not supported")
            };
        }

    }

}
