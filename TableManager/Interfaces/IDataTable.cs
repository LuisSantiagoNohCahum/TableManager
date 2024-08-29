using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableManager.Interfaces
{
    public interface IDataTable <E, F> where E : IEntityTable where F : class
    {
    }
}
