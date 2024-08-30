using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    public interface IDataTable <Entity, ColumnNames> where Entity : IEntityTable where ColumnNames : class
    {
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
        /// Obtiene o establece el borrado lógico de registros en base de datos
        /// </summary>
        bool LogicDelete { get; set; }

        /// <summary>
        /// Obtiene o establece el filtro por defecto de los registros para la tabla (Def. Id > 0)
        /// </summary>
        string DefaultFilter { get; set; }

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
        Entity GetEntity(int id);

        /// <summary>
        /// Inserta la entidad en la tabla de base de datos
        /// </summary>
        /// <param name="EntityObject">Objeto a insertar</param>
        /// <returns>Id del registro insertado</returns>
        int Insert(Entity EntityObject);

        /// <summary>
        /// Actualiza la entidad en la tabla de base de datos
        /// </summary>
        /// <param name="EntityObject">Objeto a actualizar</param>
        /// <returns>Id del registro actualizado</returns>
        int Update(Entity EntityObject);

        /// <summary>
        /// Elimina una entidad de la base de datos
        /// </summary>
        /// <param name="EntityObject">Objeto a eliminar de la tabla</param>
        /// <returns>Numero de registros eliminados</returns>
        int Delete(int id);

        /// <summary>
        /// Realiza una inserción masiva a la tabla
        /// </summary>
        /// <param name="dt">Numero de registros insertados</param>
        /// <param name="TimeOut">Tiempo de espera para ejecutar la consulta</param>
        /// <param name="UseTransaction">Usar una transacción</param>
        /// <returns>Numero de registros insertados</returns>
        int BulkInsert(DataTable dt, int TimeOut = 999, bool UseTransaction = true);
        #endregion

        #region Executor Schema
        /// <summary>
        /// Obtiene el esquema de la tabla de base de datos
        /// </summary>
        bool GetTableSchema();
        #endregion
    }
}
