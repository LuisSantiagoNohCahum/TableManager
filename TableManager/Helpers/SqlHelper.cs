using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableManager.Helpers
{
    /// <summary>
    /// Expone los métodos para hacer consultas a base de datos
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// Ejecuta una sentencia SQL de tipo SELECT para consulta de datos
        /// </summary>
        /// <param name="SelectQuery">Sentencia SQL SELECT</param>
        /// <param name="ConnectionString">Cadena de Conexión a base de datos</param>
        /// <returns>DataTable con los registros de la tabla</returns>
        /// <exception cref="Exception"></exception>
        public static DataTable ExecuteDatatable(string SelectQuery, string ConnectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(SelectQuery))
                    throw new Exception("No se recibio la consulta SQL.");

                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("No se recibio la cadena de conexión");

                DataSet ds = new DataSet();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(SelectQuery, connection))
                    {
                        dataAdapter.Fill(ds);
                        if (ds.Tables.Count > 0)
                        {
                            return ds.Tables[0];
                        }
                    }
                    connection.Close();
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"ExecuteDatatable: {ex.Message}");
            } 
        }

        /// <summary>
        /// Ejecuta una sentencia SQL (Funciones de agregación) y devuelve la primera fila 
        /// </summary>
        /// <param name="Query">Sentencia SQL</param>
        /// <param name="ConnectionString">Cadena de conexion a base de datos</param>
        /// <param name="timeout">Tiempo de espera para la ejecución de la consulta</param>
        /// <returns>Un objeto que representa la primera fila del resultado, si la conulta no devuelve nunguna fila retorna null</returns>
        /// <exception cref="Exception"></exception>
        public static object ExecuteScalar(string Query, string ConnectionString, int timeout = 999)
        {
            try
            {
                if (string.IsNullOrEmpty(Query))
                    throw new Exception("No se recibio la consulta SQL.");

                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("No se recibio la cadena de conexión");

                if (timeout < 0)
                    throw new Exception("El tiempo de espera debe ser mayor a 0");

                object result = null;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand exec = new SqlCommand(Query, connection))
                    {
                        exec.CommandTimeout = timeout;
                        result = exec.ExecuteScalar();
                    }
                    connection.Close();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"ExecuteScalar: {ex.Message}");
            }
            
        }

        /// <summary>
        /// Ejecuta una sentencia SQL que no retorna filas (INSERT, UPDATE, DELETE)
        /// </summary>
        /// <param name="Query">Sentencia SQL</param>
        /// <param name="ConnectionString">Cadena de conexion a base de datos</param>
        /// <param name="timeout">Tiempo de espera para la ejecución de la consulta</param>
        /// <returns>Un entero que representa el numero de filas que afecto la consulta</returns>
        /// <exception cref="Exception"></exception>
        public static int ExecuteNonQuery(string Query, string ConnectionString, int timeout = 999)
        {
            try
            {
                if (string.IsNullOrEmpty(Query))
                    throw new Exception("No se recibio la consulta SQL.");

                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("No se recibio la cadena de conexión");

                if (timeout < 0)
                    throw new Exception("El tiempo de espera debe ser mayor a 0");

                int result = 0;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand exec = new SqlCommand(Query, connection))
                        {
                            exec.CommandTimeout = timeout;
                            result = exec.ExecuteNonQuery();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"ExecuteNonQuery: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene un DataTable de ejecucion de un procedimeinto almacennado sin parametros
        /// </summary>
        /// <param name="sp_name">Nombre del procedimiento almacenado</param>
        /// <param name="ConnectionString">Cadena de conexión</param>
        /// <param name="timeout">Timepo de espera</param>
        /// <returns>DataTable resultante de la consulta</returns>
        public static DataTable GetDatatableFromSP(string sp_name, string ConnectionString, int timeout = 9999)
        {
            return GetDatatableFromSP(sp_name, null, ConnectionString, timeout);
        }

        /// <summary>
        /// Obtiene un DataTable de ejecucion de un procedimeinto almacennado con parametros
        /// </summary>
        /// <param name="sp_name">Nombre del procedimiento almacenado</param>
        /// <param name="Parameters">Parametros del procedimiento almacenado</param>
        /// <param name="ConnectionString">Cadena de conexión</param>
        /// <param name="timeout">Tiempo de espera</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DataTable GetDatatableFromSP(string sp_name, Hashtable Parameters, string ConnectionString, int timeout = 9999)
        {
            try
            {
                if (string.IsNullOrEmpty(sp_name))
                    throw new Exception("No se recibio el nombre del Stored Procedure a ejecutar.");

                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("No se recibio la cadena de conexión");

                if (timeout < 0)
                    throw new Exception("El tiempo de espera debe ser mayor a 0");

                DataSet ds = new DataSet();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        using (SqlCommand exec = new SqlCommand(sp_name, connection))
                        {
                            exec.CommandTimeout = timeout;
                            exec.CommandType = CommandType.StoredProcedure;

                            foreach (var key in Parameters.Keys)
                            {
                                exec.Parameters.AddWithValue(key.ToString(), Parameters[key]);
                            }
                            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(exec))
                            {
                                //change to dataset
                                dataAdapter.Fill(ds);
                                dataAdapter.Dispose();
                            }      
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"GetDatatableFromSP: {ex.Message}");
            }

        }

        /// <summary>
        /// Inserta masivamente un DataTable sin verificar la tabla de origen con la fuente de datos
        /// </summary>
        /// <param name="dt">Fuente de datos a insertar</param>
        /// <param name="tablename">Tabla de destino</param>
        /// <param name="ConnectionString">Cadena de conexión</param>
        /// <param name="timeout">Tiempo de espera de la operación</param>
        public static void BulkInsert(DataTable dt, string tablename, string ConnectionString, int timeout = 9999)
        {
            List<SqlBulkCopyColumnMapping> columns = null;
            BulkInsert(dt, columns, tablename, ConnectionString, timeout);
        }

        /// <summary>
        /// Inserta masivamente un DataTable y verifica que una lista de columnas este en el Datatable antes de insertar
        /// </summary>
        /// <param name="dt">Fuente de datos a insertar</param>
        /// <param name="columnsMapping">Columnnas a verificar</param>
        /// <param name="tablename">Tabla de destino</param>
        /// <param name="ConnectionString">Cadena de conexión</param>
        /// <param name="timeout">Tiempo de espera para la operación</param>
        /// <exception cref="SqlColumnException">Columnas no encontradas</exception>
        /// <exception cref="Exception"></exception>
        public static void BulkInsert(DataTable dt, List<string> columnsMapping, string tablename, string ConnectionString, int timeout = 9999)
        {
            try
            {
                if (columnsMapping.Count <= 0)
                {
                    throw new Exception("No se recibieron las columnas a mapear");
                }

                //no se verifica si llega vacio o mayor a cero por que ya se verifica en el metodo principal
                List<SqlBulkCopyColumnMapping> columns = VerifyColumns(dt, columnsMapping);

                BulkInsert(dt, columns, tablename, ConnectionString, timeout);
            }
            catch (SqlColumnException cex)
            {
                throw new SqlColumnException(cex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"BulkInsert: {ex.Message}");
            }
        }

        /// <summary>
        /// Inserta un DataTable masivamente en una tabla de base de datos
        /// </summary>
        /// <param name="dt">Fuente de datos a insertar</param>
        /// <param name="columnsMapping">Columnas a mapear</param>
        /// <param name="tablename">Tabla de destino</param>
        /// <param name="ConnectionString">Cadena de conexión</param>
        /// <param name="timeout">Tiempo de espera para la operación.</param>
        /// <exception cref="Exception"></exception>
        public static void BulkInsert(DataTable dt, List<SqlBulkCopyColumnMapping> columnsMapping, string tablename, string ConnectionString,  int timeout = 9999)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(ConnectionString)) 
                        throw new Exception("No se recibio la cadena de conexión");
                    if (string.IsNullOrEmpty(tablename)) 
                        throw new Exception("No se recibio el nombre de la tabla");
                    if(timeout <= 0) 
                        throw new Exception("El tiempo de espera debe ser mayor a 0");

                    //Conexion string con timeout
                    SqlConnection connection = new SqlConnection(ConnectionString);
                    SqlTransaction sqlTransaction = null;
                    try
                    {
                        //Abrimos la conexion
                        connection.Open();

                        //obtenemos una transaccion de la conexion
                        sqlTransaction = connection.BeginTransaction();

                        //abrir bulk con la transaccion y la conexion actual
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, sqlTransaction))
                        {
                            bulkCopy.NotifyAfter = 1; //genera nootificacion en evento SqlRowsCopied
                            bulkCopy.BulkCopyTimeout = timeout; //establecer tiempo de espera para la operacion bulk
                            bulkCopy.DestinationTableName = tablename;

                            if (columnsMapping != null && columnsMapping.Count > 0) 
                            {
                                //mapear columas
                                foreach (var column in columnsMapping)
                                {
                                    bulkCopy.ColumnMappings.Add(column.SourceColumn, column.DestinationColumn);
                                }
                            }
                            
                            //Verificar estructura de las columnas del dt a insertar con la tabla de bd -> en dal a cada datatable hay que añadirle un rango con las columnas basicas

                            //bulkCopy.ColumnMappings;

                            bulkCopy.WriteToServer(dt);
                        }

                        //tomar cambios si todo se ejecuto bien
                        sqlTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (sqlTransaction != null)
                        {
                            //descartar cambios
                            sqlTransaction.Rollback();
                        }

                        throw new Exception($"BulkCopy: {ex.Message}");
                    }
                    finally
                    {
                        if (connection != null)
                        {
                            //cierra conexion al finalizar el proceso sin importar el resultado
                            connection.Close();
                            //liberar recursos
                            connection.Dispose();
                        }

                        if (sqlTransaction != null)
                        {
                            sqlTransaction.Dispose();
                        }
                        
                    }
                }
                else
                {
                    throw new Exception("No se recibieron los datos a insertar en la tabla.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"BulkInsert: {ex.Message}");
            }
        }

        private static List<SqlBulkCopyColumnMapping> VerifyColumns(DataTable dt, List<string> columns)
        {
            try
            {
                List<string> ColumnErrors = new List<string>();

                List <SqlBulkCopyColumnMapping> columnsMapping = new List<SqlBulkCopyColumnMapping >();  
                
                foreach (DataColumn dc in dt.Columns)
                {
                    //verificar si la lista de columnas de la tabla tiene la columna del datatable tal cual
                    if (!columns.Contains(dc.ColumnName))
                    {
                        ColumnErrors.Add(dc.ColumnName);
                        continue;
                    }

                    //crear columnas a mappear
                    SqlBulkCopyColumnMapping columnMapping = new SqlBulkCopyColumnMapping();
                    columnMapping.SourceColumn = dc.ColumnName;
                    columnMapping.DestinationColumn = dc.ColumnName;
                    columnsMapping.Add(columnMapping);
                }

                if (ColumnErrors.Count > 0)
                {
                    throw new SqlColumnException($"El esquema de la tabla no contiene la siguientes columnas: {string.Join(", ", ColumnErrors)}");
                }

                return columnsMapping;
            }
            catch (SqlColumnException exc)
            {
                throw new SqlColumnException(exc.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"VerifyColumns: {ex.Message}");
            }
        }

        public class SqlColumnException : Exception 
        {
            public SqlColumnException(string message) : base (message)
            {
                
            }
        }
    }
}
