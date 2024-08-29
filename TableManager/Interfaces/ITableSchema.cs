using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    /// <summary>
    /// Expone las operaciones basicas para manejar el esquema de tablas SQL
    /// </summary>
    public interface ITableSchema
    {
        /// <summary>
        /// Obtiene el esquema de la tabla de base de datos
        /// </summary>
        void GetTableSchema();

        #region ExecutorInfo
        bool VerifyTableExists();
        bool VerifyTableSchema();
        bool UpdateSchema();
        bool TableSchemaGenerator();
        #endregion
    }
}
