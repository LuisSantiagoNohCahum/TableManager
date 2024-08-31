using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableManager.Interfaces;

namespace TableManager.Entity
{
    public abstract class EntityTable : IEntityTable
    {
        public int CVE_RCRD { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string LAST_USR_MODIFIED { get; set; }

        //Id de lectura.Toma una copia de CVE_RCRD insertar registro y luego hacer update a este campo con el id insertado
        public abstract int Id { get; set; }
    }
}
