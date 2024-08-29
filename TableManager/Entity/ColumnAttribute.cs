using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Entity
{
    /// <summary>
    /// Expone las propiedades para configurar atributos de columnas de tablas de base de datos
    /// </summary>
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Obtiene o establece el nombre de la columna de la tabla
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Obtiene o establece la descripción de la columna de la tabla
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Obtiene o establece el tipo de columna de la tabla
        /// </summary>
        public ColumnType.eColumnType ColumnType { get; set; }

        /// <summary>
        /// Obtiene o establece si se verifica la estructura de la tabla cada vez que se ejcuta el Executor
        /// </summary>
        public bool UpdateWithExecutor { get; set; }

        /// <summary>
        /// Obtiene o establece la longitud de la columna si es de tipo texto
        /// </summary>
        public int ColumnLength { get; set; }

        /// <summary>
        /// Obtiene o establece si se permiten valores nulos
        /// </summary>
        public bool AllowNullValues { get; set; }

        /// <summary>
        /// Obtiene o establece un valor por defecto a la columna 
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Obtiene o establece si cada fila de la columna es unica y no permite duplicados
        /// </summary>
        public bool UniqueValue { get; set; }

        /// <summary>
        /// Obtiene o establece el numero de decimales de la columna en caso de ser de tipo NUMERO_DECIMAL
        /// </summary>
        public int Decimals { get; set; }

        public ColumnAttribute(
            string columnName, 
            string description, 
            ColumnType.eColumnType columnType, 
            bool updateWithExecutor = true, 
            int columnLength = 50, 
            bool allowNullValues = true, 
            object defaultValue = null, 
            bool uniqueValue = false, 
            int decimals = 0)
        {
            ColumnName = columnName;
            Description = description;
            ColumnType = columnType;
            UpdateWithExecutor = updateWithExecutor;
            ColumnLength = columnLength;
            AllowNullValues = allowNullValues;
            DefaultValue = defaultValue;
            UniqueValue = uniqueValue;
            Decimals = decimals;
        }
    }
}
