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
using TableManager.Globals;
using TableManager.Helpers;
using TableManager.Entity;
using System.Collections;

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
                if (dr is null) throw new Exception("La fila esta vacia.");

                Entity _objectEntity = Activator.CreateInstance<Entity>();
                //Contiene las propiedades
                List<PropertyInfo> _properties = ObjectHelper.GetProperties<Entity>();
                //Contiene las constantes por defecto
                List<FieldInfo> constantsColumns = ObjectHelper.GetConstants<DefaultTableColumns.DefaultColumns>();
                //Contiene los attributos y nombres de columnas
                constantsColumns.AddRange(ObjectHelper.GetConstants<ColumnNames>());

                foreach (PropertyInfo property in _properties)
                {
                    //verificar si una propiedad hace match con una constante
                    if (!constantsColumns.Any(x => x.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase)))
                    {
                        throw new Exception($"La propiedad {property.Name} de la entidad {typeof(Entity).Name} no tiene declarada una constante en la clase {typeof(ColumnNames).Name} con el mismo nombre.");
                    }

                    FieldInfo constant = constantsColumns.Find(x => x.Name.Equals(property.Name, StringComparison.OrdinalIgnoreCase));

                    if (constant is default(FieldInfo))
                        throw new Exception($"No se encontro la constante [{property.Name}]");

                    ColumnAttribute customAttribute = Helpers.ObjectHelper.GetCustomAttribute(constant);
                    int indexColumn = dr.Table.Columns.IndexOf(customAttribute.ColumnName);

                    if (indexColumn == -1) 
                        throw new Exception($"El esquema de la tabla no incluye la columna [{customAttribute.ColumnName}] actualmente. Ejecute el Verificador de esquemas.");

                    PropertyInfo propertyToSet = _objectEntity.GetType().GetProperty(property.Name);

                    if (dr[customAttribute.ColumnName] != System.DBNull.Value)
                        propertyToSet.SetValue(_objectEntity, dr[customAttribute.ColumnName], null);
                }

                return _objectEntity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{this.GetType().Name}:::{System.Reflection.MethodBase.GetCurrentMethod()}: {ex.Message}");
            }
        }

        #endregion
    }
}
