
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
                    UInt16DataFrameColumn _ => new CoerceTo(typeof(string)),
                    UInt32DataFrameColumn _ => new CoerceTo(typeof(string)),
                    UInt64DataFrameColumn _ => new CoerceTo(typeof(string)),
                    Int32DataFrameColumn _ => new CoerceTo(typeof(string)),
                    Int64DataFrameColumn _ => new CoerceTo(typeof(string)),
                    SingleDataFrameColumn _ => new CoerceTo(typeof(string)),
                    DoubleDataFrameColumn _ => new CoerceTo(typeof(string)),
                    StringDataFrameColumn _ => new TypeMatch(typeof(string)),
                    _ => new CoerceTo(typeof(string)),
                },
                Type t when t == typeof(float) => col switch
                {
                    UInt16DataFrameColumn _ => new TypeIncompatible(),
                    UInt32DataFrameColumn _ => new TypeIncompatible(),
                    UInt64DataFrameColumn _ => new TypeIncompatible(),
                    Int32DataFrameColumn _ => new TypeIncompatible(),
                    Int64DataFrameColumn _ => new TypeIncompatible(),
                    SingleDataFrameColumn _ => new TypeMatch(typeof(float)),
                    DoubleDataFrameColumn _ => new CoerceTo(typeof(double)),
                    StringDataFrameColumn _ => new CoerceTo(typeof(string)),
                    Object other => throw new ApplicationException($"Column type {other.GetType()} cannot be coerced to float")
                },
                Type t when t == typeof(double) => col switch
                {
                    UInt16DataFrameColumn _ => new TypeIncompatible(),
                    UInt32DataFrameColumn _ => new TypeIncompatible(),
                    UInt64DataFrameColumn _ => new TypeIncompatible(),
                    Int32DataFrameColumn _ => new TypeIncompatible(),
                    Int64DataFrameColumn _ => new TypeIncompatible(),
                    SingleDataFrameColumn _ => new CoerceTo(typeof(double)),
                    DoubleDataFrameColumn _ => new TypeMatch(typeof(double)),
                    StringDataFrameColumn _ => new CoerceTo(typeof(string)),
                    Object other => throw new ApplicationException($"Column type {other.GetType()} cannot be coerced to double")
                },
                Type t when t == typeof(System.Int32) => col switch
                {
                    UInt16DataFrameColumn _ => new TypeMatch(typeof(System.Int32)),
                    UInt32DataFrameColumn _ => new CoerceTo(typeof(System.UInt32)),
                    UInt64DataFrameColumn _ => new CoerceTo(typeof(System.UInt64)),
                    Int32DataFrameColumn _ => new TypeMatch(typeof(System.Int32)),
                    Int64DataFrameColumn _ => new TypeMatch(typeof(System.Int64)),
                    SingleDataFrameColumn _ => new TypeIncompatible(),
                    DoubleDataFrameColumn _ => new TypeIncompatible(),
                    StringDataFrameColumn _ => new CoerceTo(typeof(string)),
                    Object other => throw new ApplicationException($"Column type {other.GetType()} cannot be coerced to double")
                },
                Type t when t == typeof(System.Int64) => col switch
                {
                    UInt16DataFrameColumn _ => new TypeMatch(typeof(System.Int64)),
                    UInt32DataFrameColumn _ => new TypeMatch(typeof(System.Int64)),
                    UInt64DataFrameColumn _ => new CoerceTo(typeof(System.UInt64)),
                    Int32DataFrameColumn _ => new TypeMatch(typeof(System.Int64)),
                    Int64DataFrameColumn _ => new TypeMatch(typeof(System.Int64)),
                    SingleDataFrameColumn _ => new TypeIncompatible(),
                    DoubleDataFrameColumn _ => new TypeIncompatible(),
                    StringDataFrameColumn _ => new CoerceTo(typeof(string)),
                    Object other => throw new ApplicationException($"Column type {other.GetType()} cannot be coerced to double")
                },
                _ => throw new ApplicationException("Type {nameof(t)} not supported")
            };
        }
    }
}
