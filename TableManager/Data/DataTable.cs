using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TableManager.Interfaces;

namespace TableManager.Data
{
    /// <summary>
    /// Expone los atributos y métodos de la capa de datos de una tabla SQL
    /// </summary>
    public class DataTable<Entity, ColumnNames> : IDataTable<Entity, ColumnNames> where Entity : IEntityTable where ColumnNames : class
    {
        #region Properties
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public bool LogicDelete { get; set; }
        public string DefaultFilter { get; set; }
        #endregion

        #region Global Variables

        #endregion

        #region Constructor
        public DataTable()
        {
            LogicDelete = true;
            DefaultFilter = "Id > 0";
        }
        #endregion

        #region Methods

        //separar cpnsultas en clase aparte
        #region SQL Operations
        public Entity GetEntity(int Id)
        {
            try
            {
                if (Id <= 0) 
                    throw new Exception("El id no es valido.");

                if (string.IsNullOrEmpty(TableName))
                    throw new Exception("No se recibio el nombre de la tabla.");

                string SelectQuery = string.Format("SELECT * FROM {0} T WHERE T.{1} = {2}",TableName,
                    Globals.DefaultTableColumns.DefaultColumns.Id, Id);

                if (!string.IsNullOrEmpty(DefaultFilter))
                {
                    SelectQuery = string.Format("{1} AND {2}", SelectQuery, DefaultFilter);
                }

                DataTable dt = Helpers.SqlHelper.ExecuteDatatable(SelectQuery, Globals.GLOBALS.ConnectionString);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return ParseToEntity(dt.Rows[0]);
                }

                return default(Entity);

            }
            catch (Exception ex)
            {
                throw new Exception($"{this.GetType().Name}:::{System.Reflection.MethodBase.GetCurrentMethod()}: {ex.Message}");
            }
        }

        public List<Entity> GetAll()
        {
            throw new NotImplementedException();
        }

        public DataTable GetDataTable()
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(Entity EntityObject)
        {
            throw new NotImplementedException();
        }

        public int Update(Entity EntityObject)
        {
            throw new NotImplementedException();
        }

        public int BulkInsert(DataTable dt, int TimeOut = 999, bool UseTransaction = true)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Table Schema
        public bool GetTableSchema()
        {
            throw new NotImplementedException();
        }
        #endregion
        private Entity ParseToEntity(DataRow dr)
        {
            try
            {
                List<string> ErrorMessage = new List<string>();
                if (dr is null) throw new Exception("La fila esta vacia.");

                //Contiene las propiedades
                Entity _entityObject = Activator.CreateInstance<Entity>();
                List<PropertyInfo> _properties = _entityObject.GetType().GetProperties().ToList();

                //Contiene los attributos y nombres de columnas
                ColumnNames _columnsTable = Activator.CreateInstance<ColumnNames>();
                List<FieldInfo> constantsColumns = _columnsTable.GetType().GetFields(BindingFlags.Public 
                    | BindingFlags.Instance 
                    | BindingFlags.Static 
                    | BindingFlags.FlattenHierarchy)
                    .Where(x => !x.IsInitOnly & x.IsLiteral).ToList();

                Globals.DefaultTableColumns.DefaultColumns _columnsDefault = Activator.CreateInstance<Globals.DefaultTableColumns.DefaultColumns>();

                foreach (PropertyInfo property in _properties)
                {
                    foreach (FieldInfo constant in constantsColumns)
                    {
                        if (!property.Name.Equals(constant.Name, StringComparison.OrdinalIgnoreCase))
                            ErrorMessage.Add($"La propiedad {constant.Name} de la entidad {typeof(Entity).Name} no tiene declarada una constante en la clase de {typeof(ColumnNames).Name} .");
                            continue;

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"{this.GetType().Name}:::{System.Reflection.MethodBase.GetCurrentMethod()}: {ex.Message}");
            }
            Entity eobject = Activator.CreateInstance<Entity>();

            return eobject;
        }

        #endregion
    }
}
