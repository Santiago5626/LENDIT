using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class CodigoProgramaService
    {
        private CodigoProgramaRepository codigoProgramaRepository;

        public CodigoProgramaService()
        {
            codigoProgramaRepository = new CodigoProgramaRepository();
        }

        public List<Tuple<string, string>> ObtenerProgramas()
        {
            return codigoProgramaRepository.ObtenerProgramas();
        }

        public List<Tuple<string, string>> ObtenerProgramasPorCodigo(string codProgramaFiltro)
        {
            try
            {
                // Llama al repositorio para obtener los programas filtrados por código
                return codigoProgramaRepository.ObtenerProgramasPorCodigo(codProgramaFiltro);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener los programas por código: {ex.Message}");
                return new List<Tuple<string, string>>(); // Retorna una lista vacía en caso de error
            }
        }
    }
}
