using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Runtime.Remoting.Contexts;
using ENTITY;

namespace DAL
{
    public class ProductoRepository
    {
        public ProductoRepository() { }

        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        public bool RegistrarProducto(Producto producto)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                INSERT INTO GS_PRODUCTO (
                    SERIAL,
                    NOMBREPRODUCTO,
                    MARCA,
                    IDTIPOPRODUCTO,
                    DESCRIPCION,
                    ESTADO,
                    CODIGOSENA,
                    CODIGOINTERNO
                ) VALUES (
                    :serial,
                    :NOMBREPRODUCTO,
                    :marca,
                    :IDTIPOPRODUCTO,
                    :descripcion,
                    :estado,
                    :CODIGOSENA,
                    :CODIGOINTERNO
                )";
                Command.CommandType = CommandType.Text;

                // Asignar parámetros
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("serial", producto.Serial));
                Command.Parameters.Add(new OracleParameter("NOMBREPRODUCTO", producto.NombreProducto));
                Command.Parameters.Add(new OracleParameter("marca", producto.Marca));
                Command.Parameters.Add(new OracleParameter("IDTIPOPRODUCTO", producto.IdTipoProducto));
                Command.Parameters.Add(new OracleParameter("descripcion", producto.Descripcion));
                Command.Parameters.Add(new OracleParameter("estado", producto.Estado));
                Command.Parameters.Add(new OracleParameter("CODIGOSENA", producto.CodigoSena));
                Command.Parameters.Add(new OracleParameter("CODIGOINTERNO", producto.CodigoInterno));

                // Ejecutar comando
                int rowsAffected = Command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar producto: " + ex.Message);
                return false;
            }
            finally
            {
                Command.Connection.Close();
            }
        }

        public List<Tuple<string, string, string, string>> ObtenerProductos(string estado)
        {
            List<Tuple<string, string, string, string>> productos = new List<Tuple<string, string, string, string>>();

            try
            {
                // Establecer la conexión a la base de datos
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            SELECT 
                CODIGOINTERNO, 
                NOMBREPRODUCTO, 
                MARCA, 
                ESTADO
            FROM 
                GS_PRODUCTO
            WHERE 
                ESTADO = :ESTADO";
                Command.CommandType = CommandType.Text;

                // Agregar el parámetro para evitar inyecciones SQL
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter(":ESTADO", estado));

                // Ejecutar el lector de datos
                using (OracleDataReader dr = Command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string codigoInterno = dr["CODIGOINTERNO"].ToString();
                        string nombreProducto = dr["NOMBREPRODUCTO"].ToString();
                        string marca = dr["MARCA"].ToString();
                        string estadoProducto = dr["ESTADO"].ToString();

                        productos.Add(new Tuple<string, string, string, string>(codigoInterno, nombreProducto, marca, estadoProducto));
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener productos: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión a la base de datos
                Conexion.CerrarConexion();
            }

            return productos;
        }

        public DataTable ObtenerProductosTable()
        {
            DataTable dtProductos = new DataTable();

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    SELECT SERIAL, NOMBREPRODUCTO, MARCA, IDTIPOPRODUCTO, DESCRIPCION, ESTADO, CODIGOSENA, CODIGOINTERNO 
                    FROM GS_PRODUCTO";
                Command.CommandType = CommandType.Text;

                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtProductos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener productos: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtProductos;
        }

        public DataTable ObtenerProductoPorSerialYCodigoInterno(string serial = "", string codigointerno = "")
        {
            DataTable dtProductos = new DataTable();

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    SELECT 
                        SERIAL, 
                        NOMBREPRODUCTO, 
                        MARCA, 
                        IDTIPOPRODUCTO, 
                        DESCRIPCION, 
                        ESTADO, 
                        CODIGOSENA, 
                        CODIGOINTERNO 
                    FROM 
                        GS_PRODUCTO
                    WHERE 
                        SERIAL = :serial OR CODIGOINTERNO = :codigointerno";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("serial", serial));
                Command.Parameters.Add(new OracleParameter("codigointerno", codigointerno));

                using (OracleDataAdapter adapter = new OracleDataAdapter(Command))
                {
                    adapter.Fill(dtProductos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener productos: " + ex.Message);
            }
            finally
            {
                Conexion.CerrarConexion();
            }

            return dtProductos;
        }

        public Producto BuscarProductoPorSerial(string serial)
        {
            Producto producto = null;

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    SELECT 
                        SERIAL,
                        NOMBREPRODUCTO,
                        MARCA,
                        IDTIPOPRODUCTO,
                        DESCRIPCION,
                        ESTADO,
                        CODIGOSENA,
                        CODIGOINTERNO
                    FROM 
                        GS_PRODUCTO
                    WHERE 
                        CODIGOINTERNO = :serial";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("CODIGOINTERNO", serial));

                dr = Command.ExecuteReader();

                if (dr.Read())
                {
                    producto = new Producto
                    {
                        Serial = dr["SERIAL"].ToString(),
                        NombreProducto = dr["NOMBREPRODUCTO"].ToString(),
                        Marca = dr["MARCA"].ToString(),
                        IdTipoProducto = Convert.ToInt32(dr["IDTIPOPRODUCTO"]),
                        Descripcion = dr["DESCRIPCION"].ToString(),
                        Estado = dr["ESTADO"].ToString(),
                        CodigoSena = dr["CODIGOSENA"].ToString(),
                        CodigoInterno = dr["CODIGOINTERNO"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar producto: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return producto;
        }


        public Producto BuscarProductoPorCodigoInterno(string CODIGOINTERNO)
        {
            Producto producto = null;

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    SELECT 
                        SERIAL,
                        NOMBREPRODUCTO,
                        MARCA,
                        IDTIPOPRODUCTO,
                        DESCRIPCION,
                        ESTADO,
                        CODIGOSENA,
                        CODIGOINTERNO
                    FROM 
                        GS_PRODUCTO
                    WHERE 
                        CODIGOINTERNO = :CODIGOINTERNO";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("CODIGOINTERNO", CODIGOINTERNO));

                dr = Command.ExecuteReader();

                if (dr.Read())
                {
                    producto = new Producto
                    {
                        Serial = dr["SERIAL"].ToString(),
                        NombreProducto = dr["NOMBREPRODUCTO"].ToString(),
                        Marca = dr["MARCA"].ToString(),
                        IdTipoProducto = Convert.ToInt32(dr["IDTIPOPRODUCTO"]),
                        Descripcion = dr["DESCRIPCION"].ToString(),
                        Estado = dr["ESTADO"].ToString(),
                        CodigoSena = dr["CODIGOSENA"].ToString(),
                        CodigoInterno = dr["CODIGOINTERNO"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar producto: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                Command.Connection.Close();
            }

            return producto;
        }

        public bool EditarProducto(Producto producto)
        {
            try
            {
                using (var connection = Conexion.Conectar())
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                    UPDATE GS_PRODUCTO SET
                        NOMBREPRODUCTO = :NOMBREPRODUCTO,
                        MARCA = :MARCA,
                        IDTIPOPRODUCTO = :IDTIPOPRODUCTO,
                        DESCRIPCION = :DESCRIPCION,
                        ESTADO = :ESTADO,
                        CODIGOSENA = :CODIGOSENA,
                        SERIAL = :SERIAL
                    WHERE CODIGOINTERNO = :CODIGOINTERNO";
                    command.CommandType = CommandType.Text;

                    // Asignar parámetros
                    command.Parameters.Add(new OracleParameter("NOMBREPRODUCTO", producto.NombreProducto));
                    command.Parameters.Add(new OracleParameter("MARCA", producto.Marca));
                    command.Parameters.Add(new OracleParameter("IDTIPOPRODUCTO", producto.IdTipoProducto));
                    command.Parameters.Add(new OracleParameter("DESCRIPCION", producto.Descripcion ));
                    command.Parameters.Add(new OracleParameter("ESTADO", producto.Estado ));
                    command.Parameters.Add(new OracleParameter("CODIGOSENA", producto.CodigoSena));
                    command.Parameters.Add(new OracleParameter("SERIAL", producto.Serial));
                    command.Parameters.Add(new OracleParameter("CODIGOINTERNO", producto.CodigoInterno));

                    // Ejecutar el comando
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Error de Oracle al editar producto: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al editar producto: {ex.Message}");
                return false;
            }

        }
    }
}
