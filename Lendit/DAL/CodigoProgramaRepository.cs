using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;


namespace DAL
{
    public class CodigoProgramaRepository
    {
        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        public List<Tuple<string, string>> ObtenerProgramas()
        {
            List<Tuple<string, string>> programas = new List<Tuple<string, string>>();
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT codprograma, nombre_programa FROM gs_programas";
                Command.CommandType = CommandType.Text;

                dr = Command.ExecuteReader();

                while (dr.Read())
                {
                    string codPrograma = dr["codprograma"].ToString();
                    string nombrePrograma = dr["nombre_programa"].ToString();
                    programas.Add(new Tuple<string, string>(codPrograma, nombrePrograma));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener programas: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return programas;
        }

        public List<Tuple<string, string>> ObtenerProgramasPorCodigo(string codProgramaFiltro)
        {
            List<Tuple<string, string>> programas = new List<Tuple<string, string>>();

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            SELECT 
                codprograma, 
                nombre_programa 
            FROM 
                gs_programas 
            WHERE 
                codprograma = :codPrograma";
                Command.CommandType = CommandType.Text;

                // Agregar parámetro para evitar inyecciones SQL
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter(":codPrograma", codProgramaFiltro));

                using (OracleDataReader dr = Command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string codPrograma = dr["codprograma"].ToString();
                        string nombrePrograma = dr["nombre_programa"].ToString();
                        programas.Add(new Tuple<string, string>(codPrograma, nombrePrograma));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener programas: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return programas;
        }

    }
}
