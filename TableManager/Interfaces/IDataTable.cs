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
        List<Entity> GetAll();

        DataTable GetDataTable();

        Entity GetEntity(int id);

        int Insert(Entity EntityObject);

        int Update(Entity EntityObject);

        int Delete(int id);

        int BulkInsert(DataTable dt);
        #endregion
    }
}
