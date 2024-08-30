using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableManager.Entity;

namespace TableManager.Interfaces
{
    /// <summary>
    /// Expone las operaciones basicas para manejar el esquema de tablas SQL
    /// </summary>
    public interface ITableSchema
    {
        #region Properties
        List<ColumnTableSchema> Columns { get; set; }
        #endregion

        #region Schema
        bool VerifyTableExists();
        bool VerifyTableSchema();
        bool Execute();
        bool UpdateSchema();
        bool TableSchemaGenerator();
        #endregion
    }
}
