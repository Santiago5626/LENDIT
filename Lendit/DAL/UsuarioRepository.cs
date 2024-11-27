using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace DAL
{
    public class UsuarioRepository
    {
        public UsuarioRepository() { }

        public DBoracle Conexion = new DBoracle();
        public OracleCommand Command = new OracleCommand();
        public OracleDataReader dr;

        public string Login(string usuario, string password)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = "SELECT u.identificacion, t.PRIMER_NOMBRE ||' '|| t.SEGUNDO_NOMBRE ||' '|| t.PRIMER_APELLIDO ||' '|| t.SEGUNDO_APELLIDO, r.nombre_rol, u.estadousuario FROM gs_usuario u JOIN gs_tercero t ON u.identificacion = t.identificacion JOIN GS_ROLUSUARIO r ON u.idrol = r.idrol WHERE u.identificacion = :Usuario AND u.PASSWORDS = :Password AND u.ESTADOUSUARIO = 1";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Add(new OracleParameter("Usuario", usuario));
                Command.Parameters.Add(new OracleParameter("Password", password));

                OracleDataAdapter sda = new OracleDataAdapter(Command);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    return dt.Rows[0]["nombre_rol"].ToString();
                }
                else
                {
                    return "Incorrecto";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (Command.Connection != null && Command.Connection.State == ConnectionState.Open)
                {
                    Command.Connection.Close();
                }
                Command.Parameters.Clear();
            }
        }

        public bool RegistrarUsuario(Usuario usuario)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                INSERT INTO GS_USUARIO (
                    identificacion,
                    passwords,
                    idrol,
                    estadousuario
                ) VALUES (
                    :identificacion,
                    :passwords,
                    :idrol,
                    :estadousuario
                )";
                        Command.CommandType = CommandType.Text;

                        // Asignar parámetros
                        Command.Parameters.Clear();
                        Command.Parameters.Add(new OracleParameter("identificacion", usuario.Identificacion));
                        Command.Parameters.Add(new OracleParameter("passwords", usuario.Password));
                        Command.Parameters.Add(new OracleParameter("idrol", usuario.IdRol));
                        Command.Parameters.Add(new OracleParameter("estadousuario", usuario.EstadoUsuario));  // Puede ser 0 o 1 dependiendo del estado

                        // Ejecutar comando
                        int rowsAffected = Command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al registrar usuario: " + ex.Message);
                        return false;
                    }
                    finally
                    {
                        Command.Connection.Close();
                    }
        }

        public string BuscarNombreUsuario(string identificacion)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"SELECT t.PRIMER_NOMBRE || ' ' || t.PRIMER_APELLIDO AS nombre_usuario FROM gs_usuario u JOIN gs_tercero t ON u.identificacion = t.identificacion WHERE u.identificacion = :Identificacion";
                Command.CommandType = CommandType.Text;
                 
                Command.Parameters.Add(new OracleParameter("Identificacion", identificacion));

                OracleDataAdapter sda = new OracleDataAdapter(Command);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    // Retornar solo el nombre del usuario
                    return dt.Rows[0]["nombre_usuario"].ToString();
                }
                else
                {
                    return "Incorrecto";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (Command.Connection != null && Command.Connection.State == ConnectionState.Open)
                {
                    Command.Connection.Close();
                }
                Command.Parameters.Clear();
            }
        }


        public Usuario BuscarUsuarioPorIdentificacion(string identificacion)
        {
            Usuario usuario = null;

            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
                    SELECT u.idusuario,
                           u.identificacion, 
                           u.passwords,
                           u.idrol, 
                           u.estadousuario
                    FROM gs_usuario u 
                    WHERE u.identificacion = :identificacion";
                Command.CommandType = CommandType.Text;

                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("identificacion", identificacion));

                dr = Command.ExecuteReader();

                if (dr.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(dr["idusuario"]),
                        Identificacion = dr["identificacion"].ToString(),
                        Password = dr["passwords"].ToString(),   
                        IdRol = Convert.ToInt32(dr["idrol"]),
                        EstadoUsuario = Convert.ToInt32(dr["estadousuario"])
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al buscar usuario: " + ex.Message);
            }
            finally
            {
                dr?.Close();
                if (Command.Connection != null && Command.Connection.State == ConnectionState.Open)
                {
                    Command.Connection.Close();
                }
                Command.Parameters.Clear();
            }

            return usuario;
        }

        public bool ActualizarEstadoUsuario(string idUsuario, int nuevoEstado)
        {
            try
            {
                Command.Connection = Conexion.Conectar();
                Command.CommandText = @"
            UPDATE gs_usuario 
            SET ESTADOUSUARIO = :nuevoEstado 
            WHERE IDENTIFICACION = :idUsuario";
                Command.CommandType = CommandType.Text;

                // Asignar parámetros
                Command.Parameters.Clear();
                Command.Parameters.Add(new OracleParameter("nuevoEstado", nuevoEstado));
                Command.Parameters.Add(new OracleParameter("idUsuario", idUsuario));

                // Ejecutar la consulta y verificar si se realizó una actualización
                int rowsAffected = Command.ExecuteNonQuery();
                Console.WriteLine("Rows affected: " + rowsAffected);  // Debug: Ver cuántas filas se actualizaron
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar estado del usuario: " + ex.Message);
                return false;
            }
            finally
            {
                if (Command.Connection != null && Command.Connection.State == ConnectionState.Open)
                {
                    Command.Connection.Close();
                }
                Command.Parameters.Clear();
            }
        }
    }
}
