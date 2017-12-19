#region Using

using C4rm4x.Tools.Utilities;
using System;
using System.Collections.Generic;

#endregion

namespace C4rm4x.Tools.ADOUtilities
{
    /// <summary>
    /// The data table mapping
    /// </summary>
    public class DataTableMapping : List<DataTableMappingItem>
    {
    }

    /// <summary>
    /// Each column-field mapping
    /// </summary>
    public class DataTableMappingItem
    {
        /// <summary>
        /// Gets the column name
        /// </summary>
        public string ColumnName { get; private set; }

        /// <summary>
        /// Gets the column type
        /// </summary>
        public Type ColumnType { get; private set; }

        /// <summary>
        /// Gets the property name
        /// </summary>
        public string PropertyName { get; private set; }

        private DataTableMappingItem()
        {

        }

        /// <summary>
        /// Creates a new instance of DataTableMappingItem
        /// </summary>
        /// <typeparam name="TProperty">Type of the property to map</typeparam>
        /// <param name="propertyName">Property name</param>
        /// <param name="columnName">Column name</param>
        /// <returns></returns>
        public static DataTableMappingItem New<TProperty>(
            string propertyName,
            string columnName)
        {
            propertyName.NotNullOrEmpty(nameof(propertyName));
            columnName.NotNullOrEmpty(nameof(columnName));

            return new DataTableMappingItem
            {
                ColumnName = columnName,
                ColumnType = typeof(TProperty),
                PropertyName = propertyName,
            };
        }
    }
}
