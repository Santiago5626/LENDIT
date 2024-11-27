using ENTITY;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SolicitanteRepository
    {

        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        // Registrar un solicitante
        public bool RegistrarSolicitante(Solicitante solicitante)
        {
            Command.Connection = Conexion.Conectar();
            Command.CommandText = @"
        INSERT INTO GS_SOLICITANTE (
            IDENTIFICACION,
            CODFICHA,
            CODPROGRAMA
        ) VALUES (
            :identificacion,
           
            :codficha,
            :codprograma
        )";
            Command.CommandType = CommandType.Text;

            // Asignar parámetros
            Command.Parameters.Clear();
            Command.Parameters.Add(new OracleParameter("identificacion", solicitante.Identificacion));
            Command.Parameters.Add(new OracleParameter("codficha", solicitante.CodFicha));
            Command.Parameters.Add(new OracleParameter("codprograma", solicitante.CodPrograma));

            // Ejecutar comando
            int rowsAffected = Command.ExecuteNonQuery();
            Command.Connection.Close();

            return rowsAffected > 0;
        }


        // Obtener todos los solicitantes
        public DataTable ObtenerSolicitantes()
        {
            DataTable dtSolicitantes = new DataTable();

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    SELECT 
                        s.IDSOLICITANTE,
                        t.primer_nombre || ' ' || t.segundo_nombre || ' ' || t.primer_apellido || ' ' || t.segundo_apellido AS NOMBRE,
                        s.CODFICHA,
                        s.CODPROGRAMA
                    FROM 
                        GS_SOLICITANTE s
                    JOIN 
                        GS_TERCERO t ON s.IDENTIFICACION = t.IDENTIFICACION";
                Command.CommandType = CommandType.Text;

                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtSolicitantes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener solicitantes: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtSolicitantes;
        }

        // Buscar un solicitante por su ID
        public Solicitante BuscarSolicitantePorId(string IDENTIFICACION)
        {
            Solicitante solicitante = null;

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    SELECT 
                        s.IDSOLICITANTE,
                        s.IDENTIFICACION,
                        s.CODFICHA,
                        s.CODPROGRAMA
                    FROM 
                        GS_SOLICITANTE s
                    WHERE 
                        s.IDENTIFICACION = :IDENTIFICACION";
                Command.CommandType = CommandType.Text;

                // Asignar parámetro
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("IDENTIFICACION", IDENTIFICACION));

                // Ejecutar consulta
                dr = Command.ExecuteReader();

                if (dr.Read())
                {
                    solicitante = new Solicitante
                    {
                        IdSolicitante = Convert.ToInt32(dr["IDSOLICITANTE"]),
                        Identificacion = dr["IDENTIFICACION"].ToString(),
                        CodFicha = dr["CODFICHA"].ToString(),
                        CodPrograma = dr["CODPROGRAMA"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar solicitante: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return solicitante;
        }

        // Editar un solicitante
        public bool EditarSolicitante(Solicitante solicitante)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    UPDATE GS_SOLICITANTE SET
                        CODFICHA = :codficha,
                        CODPROGRAMA = :codprograma
                    WHERE IDENTIFICACION = :identificacion";
                Command.CommandType = CommandType.Text;

                // Asignar parámetros
                Command.Parameters.Clear();
            
                Command.Parameters.Add(new OracleParameter("codficha", solicitante.CodFicha));
                Command.Parameters.Add(new OracleParameter("codprograma", solicitante.CodPrograma));

                Command.Parameters.Add(new OracleParameter("identificacion", solicitante.Identificacion));
                // Ejecutar comando
                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar solicitante: " + ex.Message);
                return false;
            }
            finally
            {
                Command.Connection.Close();
            }
        }

    }
}
