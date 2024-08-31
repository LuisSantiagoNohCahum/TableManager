using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TableManager.Entity;

namespace TableManager.Helpers
{
    public class ObjectHelper
    {
        public static List<FieldInfo> GetConstants<T>()
        {
            T _columnsTable = Activator.CreateInstance<T>();
            List<FieldInfo> constantsColumns = _columnsTable.GetType().GetFields(BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.FlattenHierarchy)
                .Where(x => !x.IsInitOnly & x.IsLiteral).ToList();
            return constantsColumns;
        }

        public static List<PropertyInfo> GetProperties<T>()
        {
            T _entityObject = Activator.CreateInstance<T>();
            List<PropertyInfo> _properties = _entityObject.GetType().GetProperties().ToList();
            return _properties;
        }

        public static ColumnAttribute GetCustomAttribute(FieldInfo constant)
        {
            if (Attribute.IsDefined(constant, typeof(Entity.ColumnAttribute)))
            {
                return Attribute.GetCustomAttribute(constant, typeof(Entity.ColumnAttribute)) as ColumnAttribute;
            }

            return null;
        }

        public static void ParseValueObjects()
        { 
        }
    }
}
