using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    public interface IDataTable <E, F> where E : IEntityTable where F : class
    {
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
    }
}
