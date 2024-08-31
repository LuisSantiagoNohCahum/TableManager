using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableManager.Entity;

namespace TableManager.Globals
{
    public class DefaultTableColumns
    {
        public class DefaultColumns
        {
            [ColumnAttribute(CVE_RCRD, "Identificador único del registro", ColumnType.eColumnType.IDENTITY)]
            public const string CVE_RCRD = "CVE_RCRD";

            [ColumnAttribute(CREATE_DATE, "Fecha de creación del registro", ColumnType.eColumnType.FECHA)]
            public const string CREATE_DATE = "CREATE_DATE";

            [ColumnAttribute(UPDATE_DATE, "Fecha de actualización del registro", ColumnType.eColumnType.FECHA)]
            public const string UPDATE_DATE = "UPDATE_DATE";

            [ColumnAttribute(LAST_USR_MODIFIED, "Ultimo usuario que modifico el registro", ColumnType.eColumnType.TEXTO)]
            public const string LAST_USR_MODIFIED = "LAST_USR_MODIFIED";

            [ColumnAttribute(Id, "Id de control del registro", ColumnType.eColumnType.NUMERO_ENTERO)]
            public const string Id = "Id";
        }
    }
}
