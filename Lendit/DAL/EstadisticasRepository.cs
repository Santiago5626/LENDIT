using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EstadisticasRepository
    {
        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        public int ObtenerSolicitantes(string estado, string tipoProducto)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            SELECT 
                COUNT(DISTINCT CODIGOINTERNO)
            FROM 
                GS_PRODUCTO p
            JOIN 
                GS_TIPO_PRODUCTO tp ON tp.IDTIPOPRODUCTO = p.IDTIPOPRODUCTO
            WHERE   
                p.ESTADO = :estado and tp.NOMBRE_TIPO_PRODUCTO = :tipoProducto";
                Command.CommandType = CommandType.Text;

                // Asignar el parámetro del estado
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("estado", estado));
                Command.Parameters.Add(new OracleParameter("tipoProducto", tipoProducto));

                // Ejecutar la consulta y devolver el resultado
                object result = Command.ExecuteScalar();

                // Verificar si el resultado es null o no, y devolver el valor correcto
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener el dato: " + ex.Message);
                return 0; // En caso de error, devolvemos 0
            }
            finally
            {
                Conexion.CerrarConexion();
            }
        }

    }
}
