using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Runtime.Remoting.Contexts;

namespace DAL
{
    public class TerceroRepository
    {
        public TerceroRepository() { }

        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        public bool RegistrarTercero(Tercero tercero)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
        INSERT INTO GS_TERCERO (
            identificacion,
            primer_nombre,
            segundo_nombre,
            primer_apellido,
            segundo_apellido,
            correo,
            telefono,
            genero,
            idrol
        ) VALUES (
            :identificacion,
            :primer_nombre,
            :segundo_nombre,
            :primer_apellido,
            :segundo_apellido,
            :correo,
            :telefono,
            :genero,
            :idrol
        )";
                Command.CommandType = CommandType.Text;

                // Asignar parámetros
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("identificacion", tercero.Identificacion));
                Command.Parameters.Add(new OracleParameter("primer_nombre", tercero.PrimerNombre));
                Command.Parameters.Add(new OracleParameter("segundo_nombre", tercero.SegundoNombre));
                Command.Parameters.Add(new OracleParameter("primer_apellido", tercero.PrimerApellido));
                Command.Parameters.Add(new OracleParameter("segundo_apellido", tercero.SegundoApellido));
                Command.Parameters.Add(new OracleParameter("correo", tercero.Correo));
                Command.Parameters.Add(new OracleParameter("telefono", tercero.Telefono));
                Command.Parameters.Add(new OracleParameter("genero", tercero.Genero));
                Command.Parameters.Add(new OracleParameter("idrol", tercero.IdRol));  // Se agrega el IdRol

                // Ejecutar comando
                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar tercero: " + ex.Message);
                return false;
            }
            finally
            {
                Command.Connection.Close();
            }
        }

        public DataTable ObtenerTerceros()
        {
            DataTable dtTerceros = new DataTable();

            try { 
            
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT t.primer_nombre || ' ' || t.segundo_nombre || ' ' || t.primer_apellido || ' ' || t.segundo_apellido AS NOMBRE, t.identificacion, t.telefono, t.correo, f.CODFICHA ,p.CODPROGRAMA FROM GS_TERCERO t join GS_SOLICITANTE s ON t.IDENTIFICACION = s.IDENTIFICACION join GS_PROGRAMAS p on p.CODPROGRAMA = s.CODPROGRAMA left join GS_FICHA f on  p.CODPROGRAMA = f.CODPROGRAMA ";
                Command.CommandType = CommandType.Text;

                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtTerceros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener terceros: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtTerceros;
        }

        public List<Tuple<string, string>> ObtenerTercerosCombo()
        {
            List<Tuple<string, string>> terceros = new List<Tuple<string, string>>();
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            SELECT 
                t.identificacion, 
                t.primer_nombre || ' ' || t.segundo_nombre || ' ' || t.primer_apellido || ' ' || t.segundo_apellido AS NOMBRE
            FROM GS_TERCERO t 
            LEFT JOIN GS_SOLICITANTE s ON t.IDENTIFICACION = s.IDENTIFICACION
            JOIN GS_ROL_SOLICITANTE so ON so.IDROLSOLICITANTE = t.IDROL
            WHERE so.NOMBRE_ROL_SOLICITANTE != 'Usuario'";
                Command.CommandType = CommandType.Text;

                // Ejecutar el lector de datos
                dr = Command.ExecuteReader();

                // Leer cada fila y agregarla a la lista
                while (dr.Read())
                {
                    string identificacion = dr["identificacion"].ToString();

                    string nombre = dr["NOMBRE"].ToString();

                    terceros.Add(new Tuple<string, string>(
                        identificacion, nombre));
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine("Error al obtener terceros: " + ex.Message);
            }
            finally
            {
                // Cerrar el lector y la conexión
                dr?.Close();
                Command.Connection.Close();
            }

            return terceros;
        }


        public DataTable ObtenerTodosLosTerceros()
        {
            DataTable dtTerceros = new DataTable();

            try
            {
                Command.Connection = Conexion.Conectar();

                // Consulta sin filtros para obtener todos los registros
                Command.CommandText = @"SELECT 
                    t.primer_nombre || ' ' || t.segundo_nombre || ' ' || t.primer_apellido || ' ' || t.segundo_apellido AS NOMBRE, 
                    t.identificacion, 
                    t.telefono, 
                    t.correo, 
                    f.codficha, 
                    p.CODPROGRAMA
                FROM 
                    GS_TERCERO t
                LEFT JOIN 
                    GS_SOLICITANTE s ON t.IDENTIFICACION = s.IDENTIFICACION
                LEFT JOIN 
                    GS_PROGRAMAS p ON p.CODPROGRAMA = s.CODPROGRAMA
                LEFT JOIN 
                    GS_FICHA f ON p.CODPROGRAMA = f.CODPROGRAMA
                JOIN 
                    GS_ROL_SOLICITANTE so ON so.IDROLSOLICITANTE = t.IDROL
                WHERE so.NOMBRE_ROL_SOLICITANTE != 'Usuario'";

                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear(); // No se necesitan parámetros para esta consulta

                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtTerceros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener todos los terceros: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtTerceros;
        }

        public DataTable ObtenerTerceroPorIdCd(string identificacion = "", string codficha = "")
        {
            DataTable dtTerceros = new DataTable();

            try
            {
                Command.Connection = Conexion.Conectar();

                Command.CommandText = @"
                 SELECT 
                     t.primer_nombre || ' ' || t.segundo_nombre || ' ' || t.primer_apellido || ' ' || t.segundo_apellido AS NOMBRE, 
                     t.identificacion, 
                     t.telefono, 
                     t.correo, 
                     f.codficha, 
                     p.CODPROGRAMA
                 FROM 
                     GS_TERCERO t
                 LEFT JOIN 
                     GS_SOLICITANTE s ON t.IDENTIFICACION = s.IDENTIFICACION
                 LEFT JOIN 
                     GS_PROGRAMAS p ON p.CODPROGRAMA = s.CODPROGRAMA
                 LEFT JOIN 
                     GS_FICHA f ON p.CODPROGRAMA = f.CODPROGRAMA
                 JOIN 
                    GS_ROL_SOLICITANTE so ON so.IDROLSOLICITANTE = t.IDROL
                 WHERE 
                     (t.identificacion = :identificacion OR f.codficha = :codficha)
                     AND so.NOMBRE_ROL_SOLICITANTE != 'Usuario'";

                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();

                Command.Parameters.Add(new OracleParameter("identificacion", identificacion));
                Command.Parameters.Add(new OracleParameter("codficha", codficha));


                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtTerceros);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener terceros: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtTerceros;
        }




        public Tercero BuscarTerceroPorIdentificacion(string identificacion)
        {
            Tercero tercero = null;

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            SELECT 
     
                t.identificacion, 
                t.primer_nombre, 
                t.segundo_nombre, 
                t.primer_apellido, 
                t.segundo_apellido, 
                t.correo, 
                t.telefono, 
                t.genero,
                p.codprograma,
                t.activo,
                t.idrol
            FROM 
                GS_TERCERO t
            LEFT JOIN 
                GS_SOLICITANTE s ON t.IDENTIFICACION = s.IDENTIFICACION 
            LEFT JOIN 
                GS_PROGRAMAS p ON p.CODPROGRAMA = s.CODPROGRAMA
            JOIN 
                GS_ROL_SOLICITANTE so ON so.IDROLSOLICITANTE = t.IDROL
            WHERE 
                t.identificacion = :identificacion
                AND so.NOMBRE_ROL_SOLICITANTE != 'Usuario'";
                Command.CommandType = CommandType.Text;

                // Asignar parámetro
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("identificacion", identificacion));

                // Ejecutar consulta
                dr = Command.ExecuteReader();

                if (dr.Read())
                {
                    tercero = new Tercero
                    {
                  
                        Identificacion = dr["identificacion"].ToString(),
                        PrimerNombre = dr["primer_nombre"].ToString(),
                        SegundoNombre = dr["segundo_nombre"].ToString(),
                        PrimerApellido = dr["primer_apellido"].ToString(),
                        SegundoApellido = dr["segundo_apellido"].ToString(),
                        Correo = dr["correo"].ToString(),
                        Telefono = dr["telefono"].ToString(),
                        Genero = dr["genero"].ToString(),
                        CodPrograma = dr["codprograma"].ToString(),
                        Activo = dr["activo"].ToString(),
                        IdRol = Convert.ToInt32(dr["idrol"])


                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar tercero: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return tercero;
        }
        public bool EditarTercero(Tercero tercero)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            UPDATE GS_TERCERO SET
                primer_nombre = :primer_nombre,
                segundo_nombre = :segundo_nombre,
                primer_apellido = :primer_apellido,
                segundo_apellido = :segundo_apellido,
                correo = :correo,
                telefono = :telefono,
                genero = :genero,
                activo = :activo,
                idrol = :idrol  -- Actualizar el idrol
            WHERE identificacion = :identificacion";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("primer_nombre", tercero.PrimerNombre));
                Command.Parameters.Add(new OracleParameter("segundo_nombre", tercero.SegundoNombre));
                Command.Parameters.Add(new OracleParameter("primer_apellido", tercero.PrimerApellido));
                Command.Parameters.Add(new OracleParameter("segundo_apellido", tercero.SegundoApellido));
                Command.Parameters.Add(new OracleParameter("correo", tercero.Correo));
                Command.Parameters.Add(new OracleParameter("telefono", tercero.Telefono));
                Command.Parameters.Add(new OracleParameter("genero", tercero.Genero));
                Command.Parameters.Add(new OracleParameter("activo", tercero.Activo));
                Command.Parameters.Add(new OracleParameter("idrol", tercero.IdRol));  // Se actualiza el idrol

                Command.Parameters.Add(new OracleParameter("identificacion", tercero.Identificacion));

                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al editar tercero: " + ex.Message);
                return false;
            }
            finally
            {
                Command.Connection.Close();
            }
        }

    }
}
//prueba todo lo de tercero y  todoo el aplicativo por fa, voy