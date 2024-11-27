using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class FichaService
    {
        private FichaRepository _fichaRepository;

        public FichaService()
        {
            _fichaRepository = new FichaRepository();
        }

        // Método para obtener todas las fichas
        public List<Tuple<string, string, DateTime, DateTime>> ObtenerFichas(string codPrograma)
        {
            try
            {
                return _fichaRepository.ObtenerFichas(codPrograma);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio al obtener fichas: " + ex.Message);
                return new List<Tuple<string, string, DateTime, DateTime>>(); // Retorna una lista vacía en caso de error
            }
        }

        public List<Tuple<string, string>> ObtenerFichasBuscar()
        {
            try
            {
                return _fichaRepository.ObtenerFichasBuscar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio al obtener fichas: " + ex.Message);
                return new List<Tuple<string, string>>(); // Retorna una lista vacía en caso de error
            }
        }

        // Método para agregar una ficha
        public string AgregarFicha(string codFicha, string codPrograma, DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                // Validación simple: Asegurarse que las fechas no sean nulas o que la fecha fin no sea antes de la fecha inicio
                if (fechaInicio >= fechaFin)
                {
                    return "La fecha de inicio debe ser anterior a la fecha de fin.";
                }

                // Agregar la ficha
                bool resultado = _fichaRepository.AgregarFicha(codFicha, codPrograma, fechaInicio, fechaFin);
                if (resultado)
                {
                    return "Ficha registrada con éxito.";
                }
                else
                {
                    return "Error al registrar la ficha.";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar la ficha: " + ex.Message);
                return "Error inesperado al registrar la ficha.";
            }
        }
    }
}
