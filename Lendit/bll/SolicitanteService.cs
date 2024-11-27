using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class SolicitanteService
    {
        private readonly SolicitanteRepository _solicitanteRepository;

        public SolicitanteService()
        {
            _solicitanteRepository = new SolicitanteRepository();
        }

        // Registrar solicitante
        public string RegistrarSolicitante(Solicitante solicitante)
        {
            try
            {
                // Validación de datos del solicitante
                if (solicitante == null)
                {
                    return "El objeto solicitante es nulo.";
                }

                if (string.IsNullOrWhiteSpace(solicitante.Identificacion))
                {
                    return "La identificación del solicitante es requerida.";
                }

                /*if (string.IsNullOrWhiteSpace(solicitante.CodFicha))
                {
                    return "El código de ficha es requerido.";
                }

                if (string.IsNullOrWhiteSpace(solicitante.CodPrograma))
                {
                    return "El código de programa es requerido.";
                }*/


                // Si todas las validaciones son exitosas, se procede al registro
                bool registrado = _solicitanteRepository.RegistrarSolicitante(solicitante);
                return registrado ? "Solicitante registrado con éxito." : "Error al registrar el solicitante en la base de datos.";
            }
            catch (Exception ex)
            {
                return "Error en el servicio de registrar solicitante: " + ex.Message;
            }
        }



        // Obtener todos los solicitantes
        public DataTable ObtenerSolicitantes()
        {
            try
            {
                return _solicitanteRepository.ObtenerSolicitantes();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio de obtener solicitantes: " + ex.Message);
                return null;
            }
        }

        // Buscar solicitante por ID
        public Solicitante BuscarSolicitantePorId(string IDENTIFICACION)
        {
            try
            {
                return _solicitanteRepository.BuscarSolicitantePorId(IDENTIFICACION);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio de buscar solicitante: " + ex.Message);
                return null;
            }
        }

        // Editar solicitante
        public string EditarSolicitante(Solicitante solicitante)
        {
            try
            {
                // Validar los datos del solicitante si es necesario

                bool resultado = _solicitanteRepository.EditarSolicitante(solicitante);
                if (resultado)
                {
                    return "El solicitante ha sido editado exitosamente.";
                }
                else
                {
                    return "No se pudo editar el solicitante.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio de editar solicitante: " + ex.Message);
                return "Ocurrió un error al editar el solicitante: " + ex.Message;
            }
        }


    }
}
