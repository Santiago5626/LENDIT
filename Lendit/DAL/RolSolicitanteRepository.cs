using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RolSolicitanteRepository
    {
        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        public List<Tuple<int, string>> ObtenerRolesSolicitante()
        {
            List<Tuple<int, string>> rolesSolicitante = new List<Tuple<int, string>>();
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT idrolsolicitante, nombre_rol_solicitante FROM gs_rol_solicitante";
                Command.CommandType = CommandType.Text;

                dr = Command.ExecuteReader();

                while (dr.Read())
                {
                    int idRolSolicitante = Convert.ToInt32(dr["idrolsolicitante"]);
                    string nombreRolSolicitante = dr["nombre_rol_solicitante"].ToString();
                    rolesSolicitante.Add(new Tuple<int, string>(idRolSolicitante, nombreRolSolicitante));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener roles de solicitante: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return rolesSolicitante;
        }

        public bool AgregarRolSolicitante(string nombreRolSolicitante)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "INSERT INTO gs_rol_solicitante (nombre_rol_solicitante) VALUES (:nombreRolSolicitante)";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("nombreRolSolicitante", nombreRolSolicitante));

                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar rol de solicitante: " + ex.Message);
                return false;
            }
            finally
            {
                Command.Connection.Close();
            }
        }
    }
}
