using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    public interface ITableSchemaExecutor
    {
        //Reemplazar string con un schema object
        List<string> Tables { get; }
        bool Execute();
        bool GetTablesSchema();
    }
}
