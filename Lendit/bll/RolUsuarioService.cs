using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class RolUsuarioService
    {
        private readonly RolUsuarioRepository _rolUsuarioRepository;

        public RolUsuarioService()
        {
            _rolUsuarioRepository = new RolUsuarioRepository();
        }

        public List<Tuple<int, string>> ObtenerRolesUsuario()
        {
            try
            {
                return _rolUsuarioRepository.ObtenerRolesUsuario();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener roles de usuario: " + ex.Message);
                return new List<Tuple<int, string>>(); // Devuelve lista vacía en caso de error
            }
        }
    }
}
