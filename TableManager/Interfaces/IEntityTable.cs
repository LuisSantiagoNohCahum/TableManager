using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    /// <summary>
    /// Expone los atributos básicos del esquema de una tabla SQL
    /// </summary>
    public interface IEntityTable
    {
        /// <summary>
        /// Obtiene o establece una clave única para el registro
        /// </summary>
        int CVE_RCRD { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de creacion del registro
        /// </summary>
        DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de actualización del registro
        /// </summary>
        DateTime UPDATE_DATE { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del último usuario que modifico el registro
        /// </summary>
        string LAST_USR_MODIFIED { get; set; }

        /// <summary>
        /// Obtiene o establece el id del registro
        /// </summary>
        int Id { get; set; }

    }
}
