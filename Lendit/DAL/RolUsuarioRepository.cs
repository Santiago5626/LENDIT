using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RolUsuarioRepository
    {
        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;
        public List<Tuple<int, string>> ObtenerRolesUsuario()
        {
            List<Tuple<int, string>> rolesUsuario = new List<Tuple<int, string>>();
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT idrol, nombre_rol FROM gs_rolusuario"; // Consulta para obtener roles de usuario
                Command.CommandType = CommandType.Text;

                dr = Command.ExecuteReader();

                while (dr.Read())
                {
                    int idRolUsuario = Convert.ToInt32(dr["idrol"]);
                    string nombreRolUsuario = dr["nombre_rol"].ToString();
                    rolesUsuario.Add(new Tuple<int, string>(idRolUsuario, nombreRolUsuario));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener roles de usuario: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return rolesUsuario;
        }

    }
}
