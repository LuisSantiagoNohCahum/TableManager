using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableManager.Interfaces;

namespace TableManager.Entity
{
    public class TableSchema : ITableSchema
    {
        public List<ColumnTableSchema> Columns { get; set; }

        public bool TableSchemaGenerator()
        {
            throw new NotImplementedException();
        }

        public bool UpdateSchema()
        {
            throw new NotImplementedException();
        }

        public bool VerifyTableExists()
        {
            throw new NotImplementedException();
        }

        public bool VerifyTableSchema()
        {
            throw new NotImplementedException();
        }

        public string ColumnScriptGenerator(ColumnTableSchema column)
        {
            return string.Empty;
        }
    }
}
