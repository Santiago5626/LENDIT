using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class RolSolicitanteService
    {
        private readonly RolSolicitanteRepository _rolSolicitanteRepository;

        public RolSolicitanteService()
        {
            _rolSolicitanteRepository = new RolSolicitanteRepository();
        }

        public List<Tuple<int, string>> ObtenerRolesSolicitante()
        {
            try
            {
                return _rolSolicitanteRepository.ObtenerRolesSolicitante();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener roles de solicitante: " + ex.Message);
                return new List<Tuple<int, string>>(); // Devuelve lista vacía en caso de error
            }
        }

        public bool AgregarRolSolicitante(string nombreRolSolicitante)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreRolSolicitante))
                {
                    Console.WriteLine("El nombre del rol de solicitante no puede estar vacío.");
                    return false;
                }

                return _rolSolicitanteRepository.AgregarRolSolicitante(nombreRolSolicitante);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar rol de solicitante: " + ex.Message);
                return false; // Indica fallo en caso de excepción
            }
        }
    }
}
