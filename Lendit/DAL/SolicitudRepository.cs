using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class SolicitudRepository
    {
        private readonly DBoracle Conexion = new DBoracle();
        private readonly OracleCommand Command = new OracleCommand();

        public bool RegistrarSolicitud(string identificacion, string codigoInterno, int estadoProducto, int idSolicitud)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "PKG_SOLICITUD.INSERTAR_SOLICITUD";
                Command.CommandType = CommandType.StoredProcedure;

                // Asignar parámetros al procedimiento
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("p_identificacion", OracleDbType.Varchar2)).Value = identificacion;
                Command.Parameters.Add(new OracleParameter("p_codigo_interno", OracleDbType.Varchar2)).Value = codigoInterno;
                Command.Parameters.Add(new OracleParameter("p_estado_producto", OracleDbType.Int64)).Value = estadoProducto;
                Command.Parameters.Add(new OracleParameter("p_id_solicitud", OracleDbType.Int64)).Value = idSolicitud;


                // Ejecutar el procedimiento almacenado
                Command.ExecuteNonQuery();

                // Cerrar la conexión
                Command.Connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al registrar la solicitud: {ex.Message}");
                if (Command.Connection.State == ConnectionState.Open)
                {
                    Command.Connection.Close();
                }
                return false;
            }
        }


        public DataTable ObtenerSolicitudTable()
        {
            DataTable dtProductos = new DataTable();

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            SELECT 
                so.IDSOLICITUD,
                t.primer_nombre || ' ' || t.segundo_nombre || ' ' || t.primer_apellido || ' ' || t.segundo_apellido AS NOMBRE,
                t.identificacion, 
                t.telefono, 
                t.correo, 
                f.CODFICHA,
                p.CODPROGRAMA,
                LISTAGG(po.codigointerno, ', ') WITHIN GROUP (ORDER BY po.codigointerno) AS CODIGOSINTERNOS
            FROM 
                GS_TERCERO t
            LEFT JOIN 
                GS_SOLICITANTE s ON t.IDENTIFICACION = s.IDENTIFICACION
            LEFT JOIN 
                GS_PROGRAMAS p ON p.CODPROGRAMA = s.CODPROGRAMA
            LEFT JOIN 
                GS_FICHA f ON p.CODPROGRAMA = f.CODPROGRAMA
            JOIN 
                GS_SOLICITUD so ON t.identificacion = so.identificacion
            JOIN 
                GS_PRODUCTO po ON po.codigointerno = so.codigointerno
            JOIN 
                GS_ROL_SOLICITANTE soo ON soo.IDROLSOLICITANTE = t.IDROL
            WHERE po.ESTADO = 'No disponible' and so.estado = 'No devuelto' AND  soo.NOMBRE_ROL_SOLICITANTE != 'Usuario'
            GROUP BY 
               so.IDSOLICITUD, t.primer_nombre, t.segundo_nombre, t.primer_apellido, t.segundo_apellido, 
                t.identificacion, t.telefono, t.correo, f.CODFICHA, p.CODPROGRAMA";
                Command.CommandType = CommandType.Text;

                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtProductos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtProductos;
        }

        public DataTable ObtenerSolicitantePorIDyCI(string identificacion, string codigoInterno)
        {
            DataTable dtProductos = new DataTable();

            try
            {
                Command.Connection = Conexion.Conectar();

                Command.CommandText = @"
            SELECT 
                so.IDSOLICITUD,
                 t.primer_nombre || ' ' || t.segundo_nombre || ' ' || t.primer_apellido || ' ' || t.segundo_apellido AS NOMBRE,
                 t.identificacion, 
                 t.telefono, 
                 t.correo, 
                 f.CODFICHA,
                 p.CODPROGRAMA,
                 LISTAGG(po.codigointerno, ', ') WITHIN GROUP (ORDER BY po.codigointerno) AS CODIGOSINTERNOS
             FROM 
                 GS_TERCERO t
             LEFT JOIN 
                 GS_SOLICITANTE s ON t.IDENTIFICACION = s.IDENTIFICACION
             LEFT JOIN 
                 GS_PROGRAMAS p ON p.CODPROGRAMA = s.CODPROGRAMA
             LEFT JOIN 
                 GS_FICHA f ON p.CODPROGRAMA = f.CODPROGRAMA
             JOIN 
                 GS_SOLICITUD so ON t.identificacion = so.identificacion
             JOIN 
                 GS_PRODUCTO po ON po.codigointerno = so.codigointerno
             JOIN 
                GS_ROL_SOLICITANTE soo ON soo.IDROLSOLICITANTE = t.IDROL
             WHERE 
                 soo.NOMBRE_ROL_SOLICITANTE != 'Usuario' AND
                 t.identificacion = :identificacion
                 or so.codigointerno = :codigoInterno
                     GROUP BY 
                         t.primer_nombre, t.segundo_nombre, t.primer_apellido, t.segundo_apellido, 
                         t.identificacion, t.telefono, t.correo, f.CODFICHA, p.CODPROGRAMA,so.IDSOLICITUD";
                Command.CommandType = CommandType.Text;

                // Parámetros para evitar inyecciones SQL
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter(":identificacion", identificacion));
                Command.Parameters.Add(new OracleParameter(":codigoInterno", codigoInterno));

                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtProductos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener datos: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtProductos;
        }


    }
}
