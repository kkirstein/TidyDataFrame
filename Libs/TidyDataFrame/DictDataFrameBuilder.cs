using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TidyDataFrame
{
    public class DictDataFrameBuilder : IDataFrameBuilder<Dictionary<string, object>>
    {
        #region public properties

        /// <summary>
        /// Column names and type specification
        /// </summary>
        public Dictionary<string, Type> ColumnSpec => _columns;

        #endregion

        #region privat properties

        private Dictionary<string, Type> _columns;
        private List<Dictionary<string, object>> _records;
        private bool _allowTypeCoerce;

        #endregion

        #region constructor

        /// <summary>
        /// Instantiates a DataFrameBuilder for dictionary records
        /// </summary>
        public DictDataFrameBuilder(bool allowTypeCoerce = false)
        {
            _columns = new Dictionary<string, Type>();
            _records = new List<Dictionary<string, object>>();
            _allowTypeCoerce = allowTypeCoerce;
        }

        #endregion

        #region public members

        /// <summary>
        /// Add a single data record to the builder
        /// </summary>
        /// <exception cref="InvalidDataTypeException">Thrown if incompatible data types are provided</exception>
        /// <inheritdoc/>
        public void Add(Dictionary<string, object> record)
        {
            if (_allowTypeCoerce)
            {
                throw new NotImplementedException();
            }
            else
            {
                // check type compatibility
                foreach (var col in record)
                {
                    if (_columns.ContainsKey(col.Key))
                    {
                        var currentType = _columns[col.Key];
                        var newType = col.Value.GetType();
                        if (currentType != newType)
                        {
                            var msg = $"Type {newType} is not compatible with already present type {currentType} for column {col.Key}";
                            throw new ApplicationException(msg);
                        }
                    }
                    else
                    {
                        _columns.Add(col.Key, col.Value.GetType());
                    }
                }
            }
            _records.Add(record);
        }

        /// <summary>
        /// Add a collection of data records to the builder
        /// </summary>
        /// <exception cref="InvalidDataTypeException">Thrown if incompatible data types are provided</exception>
        /// <inheritdoc/>
        public void Add(ICollection<Dictionary<string, object>> records)
        {
            foreach (var r in records)
            {
                Add(r);
            }
        }

        /// <summary>
        /// Converts builder to a DataFrame.
        /// </summary>
        /// <inheritdoc/>
        /// <exception cref="InvalidDataTypeException">Thrown if unsupperted data types are provided</exception>
        public DataFrame ToDataFrame()
        {
            var numCols = ColumnSpec.Count;
            var cols = new List<DataFrameColumn>(numCols);

            foreach (var column in _columns)
            {
                var name = column.Key;
                var type = column.Value;

                //cols.Add(Column.ToDataFrameColumn(_records.Select(r => r.TryGetValue(name, out var v) ? v : null), name));
                switch (type)
                {
                    case Type _ when type == typeof(string):
                        var _stringCol = new StringDataFrameColumn(name, _records.Select(r => r.TryGetValue(name, out var v) ? (string?)v : null));
                        cols.Add(_stringCol);
                        break;
                    case Type _ when type == typeof(double):
                        var _doubleCol = new DoubleDataFrameColumn(name, _records.Select(r => r.TryGetValue(name, out var v) ? (double?)v : null));
                        cols.Add(_doubleCol);
                        break;
                    case Type _ when type == typeof(float):
                        var _singleCol = new SingleDataFrameColumn(name, _records.Select(r => r.TryGetValue(name, out var v) ? (float?)v : null));
                        cols.Add(_singleCol);
                        break;
                    case Type _ when type == typeof(System.Int32):
                        var _intCol = new Int32DataFrameColumn(name, _records.Select(r => r.TryGetValue(name, out var v) ? (int?)v : null));
                        cols.Add(_intCol);
                        break;
                    default:
                        throw new InvalidDataTypeException($"Unsupported type {type}");
                }
            }

            return new DataFrame(cols);
        }

        #endregion
    }
}
