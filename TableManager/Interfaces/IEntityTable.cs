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
        DateTime FHA_CREATION { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha de actualización del registro
        /// </summary>
        DateTime FHA_UPDATE { get; set; }

        /// <summary>
        /// Obtiene o establece el nombre del último usuario que modifico el registro
        /// </summary>
        string LAST_USR_MODIFIED { get; set; }

    }
}
