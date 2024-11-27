using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class EstadisticaService
    {
        private readonly EstadisticasRepository _estadisticasRepository;

        public EstadisticaService()
        {
            _estadisticasRepository = new EstadisticasRepository();
        }

        public int ObtenerSolicitantes(string estado, string tipoProducto)
        {
            try
            {
                return _estadisticasRepository.ObtenerSolicitantes(estado, tipoProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener solicitantes: " + ex.Message);
                return 0; // En caso de error, se devuelve 0
            }
        }
    }
}
