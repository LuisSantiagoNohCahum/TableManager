using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    public interface IBusinessTable <Entity, ColumnNames, DataTable>  
        where Entity : IEntityTable 
        where ColumnNames : class 
        where DataTable : IDataTable<Entity,ColumnNames>
    {
        //bool GetTableSchema();
        #region Properties
        /// <summary>
        /// Obtiene o establece la cadena de conexion a base de datos
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre de la tabla de base de datos
        /// </summary>
        string TableName { get; set; }

        /// <summary>
        /// Obtiene o establece el objeto de la capa de datos
        /// </summary>
        DataTable DataTableManager { get; set; }
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

        /// <summary>
        /// Obtiene una entidad de la base de datos
        /// </summary>
        /// <param name="Id">Id de la entidad a encontrar</param>
        /// <returns>Objeto entidad</returns>
        int GetEntity(int Id);

        /// <summary>
        /// Guarda la entidad en la tabla de base de datos
        /// </summary>
        /// <param name="EntityObject">Objeto a insertar o actualizar</param>
        /// <returns>Id del registro guardado</returns>
        int Save(Entity EntityObject);

        /// <summary>
        /// Elimina una entidad de la base de datos
        /// </summary>
        /// <param name="EntityObject">Objeto a eliminar de la tabla</param>
        /// <returns>Numero de registros eliminados</returns>
        int Delete(Entity EntityObject);

        /// <summary>
        /// Realiza una inserción masiva a la tabla
        /// </summary>
        /// <param name="dt">Numero de registros insertados</param>
        /// <param name="TimeOut">Tiempo de espera para ejecutar la consulta</param>
        /// <param name="UseTransaction">Usar una transacción</param>
        /// <returns>Numero de registros insertados</returns>
        int BulkInsert(DataTable dt, int TimeOut, bool UseTransaction);
        #endregion

        #region Executor Schema
        /// <summary>
        /// Obtiene el esquema de la tabla de base de datos
        /// </summary>
        bool GetTableSchema();
        #endregion
    }
}
