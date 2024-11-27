using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FichaRepository
    {
        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        // Obtener todas las fichas
        public List<Tuple<string, string, DateTime, DateTime>> ObtenerFichas(string codPrograma)
        {
            List<Tuple<string, string, DateTime, DateTime>> fichas = new List<Tuple<string, string, DateTime, DateTime>>();
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT F.CODFICHA, F.CODPROGRAMA, F.FECHA_INICIO, F.FECHA_FIN FROM GS_FICHA F JOIN GS_PROGRAMAS P ON F.CODPROGRAMA = P.CODPROGRAMA WHERE P.CODPROGRAMA = :CODPROGRAMA";
                Command.CommandType = CommandType.Text;

                // Parámetro para el código de programa
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("CODPROGRAMA", codPrograma));

                dr = Command.ExecuteReader();

                while (dr.Read())
                {
                    string codFicha = dr["CODFICHA"].ToString();
                    string CodPrograma = dr["CODPROGRAMA"].ToString();
                    DateTime fechaInicio = Convert.ToDateTime(dr["FECHA_INICIO"]);
                    DateTime fechaFin = Convert.ToDateTime(dr["FECHA_FIN"]);

                    fichas.Add(new Tuple<string, string, DateTime, DateTime>(codFicha, codPrograma, fechaInicio, fechaFin));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener fichas: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return fichas;
        }


        public List<Tuple<string, string>> ObtenerFichasBuscar()
        {
            List<Tuple<string, string>> fichas = new List<Tuple<string, string>>();
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT CODFICHA, CODPROGRAMA FROM GS_FICHA";  
                Command.CommandType = CommandType.Text;

                dr = Command.ExecuteReader();

                while (dr.Read())
                {
                    string codFicha = dr["CODFICHA"].ToString();
                    string nombreFicha = dr["CODPROGRAMA"].ToString(); 
                    fichas.Add(new Tuple<string, string>(codFicha, nombreFicha));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener fichas: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return fichas;
        }


        // Agregar una nueva ficha
        public bool AgregarFicha(string codFicha, string codPrograma, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    INSERT INTO GS_FICHA (CODFICHA, CODPROGRAMA, FECHA_INICIO, FECHA_FIN)
                    VALUES (:codFicha, :codPrograma, :fechaInicio, :fechaFin)";
                Command.CommandType = CommandType.Text;

                // Limpiar parámetros antes de agregar nuevos
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("codFicha", codFicha));
                Command.Parameters.Add(new OracleParameter("codPrograma", codPrograma));
                Command.Parameters.Add(new OracleParameter("fechaInicio", fechaInicio));
                Command.Parameters.Add(new OracleParameter("fechaFin", fechaFin));

                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected > 0; // Retorna true si se afectaron filas, es decir, si se insertó la ficha
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar ficha: " + ex.Message);
                return false;
            }
            finally
            {
                Command.Connection.Close();
            }
        }
    }
}
