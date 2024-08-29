using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    public interface IBusinessTable <Entity, ColumnNames, DataTable> : ITableSchema 
        where Entity : IEntityTable 
        where ColumnNames : class 
        where DataTable : IDataTable<Entity,ColumnNames>
    {
        //bool GetTableSchema();
        #region Properties
        string ConnectionString { get; set; }
        string TableName { get; set; }
        #endregion

        #region SQL Methods
        /// <summary>
        /// Obtiene todos los elementos de la tabla
        /// </summary>
        /// <returns>Lista de tipo Entity</returns>
        List<Entity> GetAll();

        /// <summary>
        /// Obtiene todos los elementos de la tabla
        /// </summary>
        /// <returns>Datatable de los registros</returns>
        DataTable GetDataTable();
        int GetEntity(int Id);
        int Save(Entity EntityObject);
        int Delete(Entity EntityObject);
        int BulkInsert(DataTable dt);
        #endregion
    }
}
