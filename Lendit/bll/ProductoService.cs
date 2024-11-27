using DAL;
using ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL
{
    public class ProductoService
    {
        private readonly ProductoRepository _productoRepository;

        public ProductoService()
        {
            _productoRepository = new ProductoRepository();
        }

        public string RegistrarProducto(Producto producto)
        {
            try
            {
                if (string.IsNullOrEmpty(producto.CodigoInterno))
                {
                    return "Error: El CodigoInterno del producto es obligatorio.";
                }


                if (string.IsNullOrEmpty(producto.NombreProducto))
                {
                    return "Error: El nombre del producto es obligatorio.";
                }

                if (producto.IdTipoProducto <= 0)
                {
                    return "Error: El tipo de producto es obligatorio.";
                }

               
                if (producto.IdTipoProducto == 1 && (string.IsNullOrEmpty(producto.CodigoInterno) || string.IsNullOrEmpty(producto.Serial) || string.IsNullOrEmpty(producto.CodigoSena) || string.IsNullOrEmpty(producto.NombreProducto)))
                {
                    return "Error: El código interno, el serial, el codigo SENA son obligatorios para el accesorio.";
                }

                bool isRegistered = _productoRepository.RegistrarProducto(producto);

                return isRegistered ? "Registro exitoso." : "Error: No se pudo registrar el producto.";
            }
            catch (Exception ex)
            {
                // Manejo de excepciones con mensaje descriptivo
                return "Error en la capa de negocio al registrar el producto: " + ex.Message;
            }

        }

        public List<Tuple<string, string,string,string>> ObtenerProductos(string ESTADO)
        {
            return _productoRepository.ObtenerProductos(ESTADO);
        }


        public Producto BuscarProductoPorCodigoInterno(string codigoInterno)
        {
            try
            {
          
                return _productoRepository.BuscarProductoPorCodigoInterno(codigoInterno);
            }
            catch (Exception ex)
            {
                // Manejo de errores y registro del mensaje
                Console.WriteLine("Error en BLL al buscar producto: " + ex.Message);

                // Lanza una excepción más genérica para ser manejada en capas superiores
                throw new Exception("Ocurrió un error al intentar buscar el producto. Por favor, inténtelo de nuevo más tarde.");
            }
        }


        public DataTable ObtenerProductosTable()
        {
            try
            {
                return _productoRepository.ObtenerProductosTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL al obtener producto: " + ex.Message);
                throw new Exception("Ocurrió un error al intentar obtener los producto. Por favor, inténtelo de nuevo más tarde.");
            }
        }

        // Método para obtener un producto por su serial
        public Producto ObtenerProductoPorSerial(string serial)
        {
            try
            {
                return _productoRepository.BuscarProductoPorSerial(serial);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en ProductoService al obtener producto por serial: " + ex.Message);
                return null;
            }
        }

        public DataTable ObtenerProductoPorSerialYCodigoInterno(string serial = "", string codigointerno = "")
        {
            try
            {
                // Llamar al método de la capa de acceso a datos (DAL) para obtener los productos
                return _productoRepository.ObtenerProductoPorSerialYCodigoInterno(serial, codigointerno);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en el servicio de productos: " + ex.Message);
                throw;
            }
        }

        // Método para editar un producto existente
        public string EditarProducto(Producto producto)
        {
            // Validaciones antes de actualizar en la base de datos
            if (string.IsNullOrWhiteSpace(producto.CodigoInterno))
            {
                return "El código interno es obligatorio.";
            }

            if (string.IsNullOrWhiteSpace(producto.NombreProducto))
            {
                return "El nombre del producto es obligatorio.";
            }

     

            try
            {
                // Llamamos al repositorio para actualizar el producto
                bool editado = _productoRepository.EditarProducto(producto);

                return editado ? "Producto editado con éxito." : "No se pudo editar el producto.";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL al editar producto: " + ex.Message);
                return "Ocurrió un error al intentar editar el producto. Por favor, inténtelo de nuevo más tarde.";
            }
        }


    }
}
