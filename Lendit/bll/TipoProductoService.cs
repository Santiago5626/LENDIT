using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bll
{
    public class TipoProductoService
    {
        private readonly TipoProductoRepository _tipoProductoRepository;

        public TipoProductoService()
        {
            _tipoProductoRepository = new TipoProductoRepository();
        }

        // Obtener lista de tipos de producto
        public List<Tuple<int, string>> ObtenerTiposProducto()
        {
            try
            {
                return _tipoProductoRepository.ObtenerTiposProducto();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener tipos de producto: " + ex.Message);
                return new List<Tuple<int, string>>(); // Devuelve lista vacía en caso de error
            }
        }

        // Agregar un nuevo tipo de producto
        public bool AgregarTipoProducto(string nombreTipoProducto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreTipoProducto))
                {
                    Console.WriteLine("El nombre del tipo de producto no puede estar vacío.");
                    return false;
                }

                return _tipoProductoRepository.AgregarTipoProducto(nombreTipoProducto);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar tipo de producto: " + ex.Message);
                return false; // Indica fallo en caso de excepción
            }
        }
    }
}
