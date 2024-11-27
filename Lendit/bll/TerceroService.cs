using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL
{
    public class TerceroService
    {
        private TerceroRepository _terceroRepository;

        public TerceroService()
        {
            _terceroRepository = new TerceroRepository();
        }

        public string RegistrarTercero(Tercero tercero)
        {
            try
            {

                if (string.IsNullOrEmpty(tercero.Identificacion) || string.IsNullOrEmpty(tercero.PrimerNombre) || string.IsNullOrEmpty(tercero.PrimerApellido))
                {
                    return "Error: La identificación, el primer nombre y el primer apellido son obligatorios.";
                }
                if (!IsValidEmail(tercero.Correo))
                {
                    return "El correo electrónico no es válido.";
                }

                bool isRegistered = _terceroRepository.RegistrarTercero(tercero);

                return isRegistered ? "Registro exitoso." : "Error: No se pudo registrar el tercero.";
            }
            catch (Exception ex)
            {

                return "Error en la capa de negocio al registrar el tercero: " + ex.Message;
            }

        }
        public DataTable ObtenerTerceros()
        {
            try
            {
                return _terceroRepository.ObtenerTerceros();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL al obtener terceros: " + ex.Message);
                throw new Exception("Ocurrió un error al intentar obtener los terceros. Por favor, inténtelo de nuevo más tarde.");
            }
        }
        public List<Tuple<string, string>> ObtenerTercerosCombo()
        {
            return _terceroRepository.ObtenerTercerosCombo();
        }

        public Tercero BuscarTerceroPorIdentificacion(string identificacion)
        {
            try
            {
                return _terceroRepository.BuscarTerceroPorIdentificacion(identificacion);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error en BLL al buscar tercero: " + ex.Message);

                throw new Exception("Ocurrió un error al intentar buscar el tercero. Por favor, inténtelo de nuevo más tarde.");
            }
        }


        public DataTable ObtenerTerceroPorIdCd(string identificacion = "", string codficha = "")
        {
             
            return _terceroRepository.ObtenerTerceroPorIdCd(identificacion, codficha);
        }


        public DataTable ObtenerTodosLosTerceros()
        {
            return _terceroRepository.ObtenerTodosLosTerceros();
        }

        public string EditarTercero(Tercero tercero)
        {
            // Validaciones antes de actualizar en la base de datos
            if (string.IsNullOrWhiteSpace(tercero.Identificacion))
            {
                return "La identificación es obligatoria.";
            }

            if (string.IsNullOrWhiteSpace(tercero.PrimerNombre))
            {
                return "El primer nombre es obligatorio.";
            }

            if (string.IsNullOrWhiteSpace(tercero.PrimerApellido))
            {
                return "El primer apellido es obligatorio.";
            }

            if (!IsValidEmail(tercero.Correo))
            {
                return "El correo electrónico no es válido.";
            }

            if (string.IsNullOrWhiteSpace(tercero.Genero) || (tercero.Genero != "M" && tercero.Genero != "F"))
            {
                return "El género debe ser 'M' (Masculino) o 'F' (Femenino).";
            }

            

            try
            {
                bool editado = _terceroRepository.EditarTercero(tercero);
                return editado ? "Tercero editado con éxito." : "No se pudo editar el tercero.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL al editar tercero: " + ex.Message);
                return "Ocurrió un error al intentar editar el tercero. Por favor, inténtelo de nuevo más tarde.";
            }
        }

        // Método auxiliar para validar el correo electrónico
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}
