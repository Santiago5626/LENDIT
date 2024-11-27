using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TipoProductoRepository
    {
        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        // Obtener lista de tipos de producto
        public List<Tuple<int, string>> ObtenerTiposProducto()
        {
            List<Tuple<int, string>> tiposProducto = new List<Tuple<int, string>>();
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT idtipoproducto, nombre_tipo_producto FROM gs_tipo_producto";
                Command.CommandType = CommandType.Text;

                dr = Command.ExecuteReader();

                while (dr.Read())
                {
                    int idTipoProducto = Convert.ToInt32(dr["idtipoproducto"]);
                    string nombreTipoProducto = dr["nombre_tipo_producto"].ToString();
                    tiposProducto.Add(new Tuple<int, string>(idTipoProducto, nombreTipoProducto));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener tipos de producto: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return tiposProducto;
        }

        // Agregar un nuevo tipo de producto
        public bool AgregarTipoProducto(string nombreTipoProducto)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "INSERT INTO gs_tipo_producto (nombre_tipo_producto) VALUES (:nombreTipoProducto)";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("nombreTipoProducto", nombreTipoProducto));

                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar tipo de producto: " + ex.Message);
                return false;
            }
            finally
            {
                Command.Connection.Close();
            }
        }
    }
}
