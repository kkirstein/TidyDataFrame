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

        public Dictionary<string, PropertyInfo> ColumnSpec { get; }

        public int Count { get { return _records.Count; } }

        #endregion

        #region private properties

        private List<TDataRecord> _records;

        private bool _frozen;

        #endregion

        #region constructor

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
            _frozen = false;
        }


        #endregion

        #region public members

        public void Add(TDataRecord record)
        {
            _records.Add(record);
        }


        public void Add(ICollection<TDataRecord> records)
        {
            _records = _records.Concat(records).ToList();
        }

        public void Freeze()
        {
            _frozen = true;
        }

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
