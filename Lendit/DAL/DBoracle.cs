using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace DAL
{
    public class DBoracle
    {
        static public string Conexionstring = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
        public OracleConnection Conexion = new OracleConnection(Conexionstring);

        public OracleConnection Conectar()
        {
            try
            {
                if (Conexion.State == ConnectionState.Closed)
                    Conexion.Open();
                //MessageBox.Show("CONEXION EXITOSA");
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error al abrir la conexión: " + ex.Message);
            }
            return Conexion;
        }

        public void CerrarConexion()
        {
            try
            {
                if (Conexion.State == ConnectionState.Open)
                    Conexion.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error al cerrar la conexión: " + ex.Message);
            }
        }
    }
}
