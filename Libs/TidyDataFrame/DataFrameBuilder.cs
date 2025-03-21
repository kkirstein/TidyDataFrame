﻿using Microsoft.Data.Analysis;
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
        public Dictionary<string, Type> ColumnSpec
        {
            get { return _columns.ToDictionary(x => x.Key, x => x.Value.PropertyType); }
        }

        /// <summary>
        /// Count of the currently added data records
        /// </summary>
        public int Count { get { return _records.Count; } }

        #endregion

        #region private properties

        private List<TDataRecord> _records;
        private Dictionary<string, PropertyInfo> _columns;

        //private bool _frozen;

        #endregion

        #region constructor

        /// <summary>
        /// Instantiates a DataFrameBuilder for the given data record type
        /// </summary>
        public DataFrameBuilder()
        {
            var dataFields = typeof(TDataRecord).GetProperties();

            _columns = new Dictionary<string, PropertyInfo>(dataFields.Length);
            foreach (var field in dataFields)
            {
                var fieldName = field.Name;
                var fieldType = field.PropertyType;
                _columns[fieldName] = field;
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
        /// <exception cref="InvalidDataTypeException">Thrown if unsupperted data types are provided</exception>
        public DataFrame ToDataFrame()
        {
            var numCols = ColumnSpec.Count;
            var cols = new List<DataFrameColumn>(numCols);

            foreach (var column in _columns)
            {
                var name = column.Key;
                var prop = column.Value;
                var type = prop.PropertyType;

                cols.Add(Column.ToDataFrameColumn(_records.Select(r => prop.GetValue(r)), name));
            }


            return new DataFrame(cols);
        }

        #endregion
    }
}
