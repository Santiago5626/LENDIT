using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class UsuarioService
    {
        private UsuarioRepository _usuarioRepository;

        public UsuarioService()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public string Login(string usuario, string password)
        {
            // Validar los parámetros de entrada
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(password))
            {
                return "Usuario o contraseña no pueden estar vacíos.";
            }

            try
            {
                // Llamada al método Login de la capa de datos (DAL)
                string resultado = _usuarioRepository.Login(usuario, password);

                // Verificar el resultado y aplicar lógica adicional si es necesario
                if (resultado == "Incorrecto")
                {
                    return "Usuario o contraseña incorrectos.";
                }
                else if (resultado == "Inactivo")
                {
                    return "La cuenta de usuario está inactiva.";
                }

                // Retornar el rol si el usuario es válido
                return resultado; // Retorna directamente el rol sin un mensaje adicional
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return $"Error en el inicio de sesión: {ex.Message}";
            }
        }

        public string RegistrarUsuario(Usuario usuario)
        {
            try
            {
                // Validación de datos requeridos
                if (string.IsNullOrEmpty(usuario.Identificacion) || string.IsNullOrEmpty(usuario.Password) || usuario.IdRol == 0)
                {
                    return "Error: La identificación, la contraseña y el rol son obligatorios.";
                }

  
                bool isRegistered = _usuarioRepository.RegistrarUsuario(usuario);

                return isRegistered ? "Registro exitoso." : "Error: No se pudo registrar el usuario.";
            }
            catch (Exception ex)
            {
                return "Error en la capa de negocio al registrar el usuario: " + ex.Message;
            }
        }

        public string BuscarNombreUsuario(string identificacion)
        {
            try
            {
                // Llamada a la capa de acceso a datos (DAL)
                return _usuarioRepository.BuscarNombreUsuario(identificacion);
            }
            catch (Exception ex)
            {
                // Manejo de errores, si ocurre alguna excepción
                return "Error: " + ex.Message;
            }
        }
        public Usuario BuscarUsuarioPorIdentificacion(string identificacion)
        {
            // Validación de entrada
            if (string.IsNullOrWhiteSpace(identificacion))
            {
                throw new ArgumentException("La identificación no puede estar vacía.");
            }

            try
            {
                // Intentar buscar el usuario
                Usuario usuario = _usuarioRepository.BuscarUsuarioPorIdentificacion(identificacion);

                // Si el usuario no se encuentra, se devuelve null sin lanzar excepción
                return usuario;
            }
            catch (ArgumentException argEx)
            {
                // Manejo específico para errores de argumento inválido
                Console.WriteLine("Error de validación en BLL: " + argEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                // Manejo genérico de excepciones en la capa de negocio
                Console.WriteLine("Error en BLL al buscar usuario: " + ex.Message);
                throw new Exception("Ocurrió un error al intentar buscar el usuario. Por favor, inténtelo de nuevo más tarde.");
            }
        }
        public bool ActualizarEstadoUsuario(string idUsuario, int nuevoEstado)
        {
            try
            {
                // Intentar actualizar el estado en la capa DAL
                return _usuarioRepository.ActualizarEstadoUsuario(idUsuario, nuevoEstado);
            }
            catch (ArgumentException argEx)
            {
                // Manejo específico para errores de argumento inválido
                Console.WriteLine("Error de validación en BLL: " + argEx.Message);
                throw new ArgumentException("El valor proporcionado no es válido.", argEx);
            }
            catch (Exception ex)
            {
                // Manejo genérico de excepciones en la capa de negocio
                Console.WriteLine("Error en BLL al actualizar estado del usuario: " + ex.Message);
                throw new Exception("Ocurrió un error al intentar actualizar el estado del usuario. Por favor, inténtelo de nuevo más tarde.", ex);
            }
        }
    }
}

