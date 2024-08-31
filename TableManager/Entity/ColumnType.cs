using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Entity
{
    public class ColumnType
    {
        public enum eColumnType
        {
            /// <summary>
            /// Columna de tipo IDENTITY
            /// </summary>
            IDENTITY,

            /// <summary>
            /// Columna de tipo NVARCHAR
            /// </summary>
            TEXTO,

            /// <summary>
            /// Columna de tipo DATETIME
            /// </summary>
            FECHA,

            /// <summary>
            /// Columna de tipo BIT
            /// </summary>
            SINO,

            /// <summary>
            /// Columna de tipo BYTE
            /// </summary>
            ARCHIVO,

            /// <summary>
            /// Columna de tipo XML
            /// </summary>
            XML,

            /// <summary>
            /// Columna de tipo INT
            /// </summary>
            NUMERO_ENTERO,

            /// <summary>
            /// Columna de tipo DECIMAL
            /// </summary>
            NUMERO_DECIMAL
        }
    }
}
