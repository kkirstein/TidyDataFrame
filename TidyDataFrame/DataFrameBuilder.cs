using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame
{
    public class DataFrameBuilder<TDataRecord> : IDataFrameBuilder<TDataRecord>
    {
        #region public properties

        /// <summary>
        /// Column name and type specification, extracted from TDataRecord
        /// </summary>
        public Dictionary<string, PropertyInfo> ColumnSpec { get; }

        /// <summary>
        /// Count of the currently added data records
        /// </summary>
        public int Count { get { return _records.Count; } }

        #endregion

        #region private properties

        private List<TDataRecord> _records;

        //private bool _frozen;

        #endregion

        #region constructor

        /// <summary>
        /// Instantiates a DataFrameBuilder for the given data record type
        /// </summary>
        public DataFrameBuilder()
        {
            var dataFields = typeof(TDataRecord).GetProperties();

            ColumnSpec = new Dictionary<string, PropertyInfo>(dataFields.Length);
            foreach (var field in dataFields)
            {
                var fieldName = field.Name;
                var fieldType = field.PropertyType;
                ColumnSpec[fieldName] = field;
            }

            _records = new List<TDataRecord>();
            //_frozen = false;
        }


        #endregion

        #region public members

        /// <summary>
        /// Add a single data record to the builder
        /// </summary>
        /// <inheritdoc/>
        public void Add(TDataRecord record)
        {
            _records.Add(record);
        }


        /// <summary>
        /// Add a collection of data records to the builder
        /// </summary>
        /// <inheritdoc/>
        public void Add(ICollection<TDataRecord> records)
        {
            _records = _records.Concat(records).ToList();
        }

        //public void Freeze()
        //{
        //    _frozen = true;
        //}

        /// <summary>
        /// Converts builder to a DataFrame.
        /// </summary>
        /// <inheritdoc/>
        /// <exception cref="ApplicationException">Thrown if unsupperted data types are provided</exception>
        public DataFrame ToDataFrame()
        {
            var numCols = ColumnSpec.Count;
            var cols = new List<DataFrameColumn>(numCols);

            foreach (var column in ColumnSpec)
            {
                var name = column.Key;
                var prop = column.Value;
                var type = prop.PropertyType;

                switch (type)
                {
                    case Type _ when type == typeof(string):
                        var _stringCol = new StringDataFrameColumn(name, _records.Select(r => (string?)prop.GetValue(r)));
                        cols.Add(_stringCol);
                        break;
                    case Type _ when type == typeof(double) || type == typeof(Nullable<double>):
                        var _doubleCol = new DoubleDataFrameColumn(name, _records.Select(r => (double?)prop.GetValue(r)));
                        cols.Add(_doubleCol);
                        break;
                    case Type _ when type == typeof(System.Int32) || type == typeof(Nullable<System.Int32>):
                        var _intCol = new Int32DataFrameColumn(name, _records.Select(r => (int?)prop.GetValue(r)));
                        cols.Add(_intCol);
                        break;
                    default:
                        throw new ApplicationException($"Unsupported type {nameof(type)}");
                }
            }


            return new DataFrame(cols);
        }

        #endregion
    }
}
