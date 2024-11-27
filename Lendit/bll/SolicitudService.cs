using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace bll
{
    public class SolicitudService
    {
        private readonly SolicitudRepository _solicitudRepository;

        public SolicitudService()
        {
            _solicitudRepository = new SolicitudRepository();
        }
        public string RegistrarSolicitud(string identificacion = "", string codigoInterno = "", int estadoProducto = 0, int idSolicitud = 0)
        {
            try
            {
                // Llamar al repositorio para registrar la solicitud
                bool registroExitoso = _solicitudRepository.RegistrarSolicitud(
                    identificacion,
                    codigoInterno,
                    estadoProducto,
                    idSolicitud);

                // Retornar mensaje de éxito o error
                if (registroExitoso)
                    return "La solicitud fue registrada exitosamente.";
                else
                    return "No se pudo registrar la solicitud. Intente nuevamente.";
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error: {ex.Message}");
                return "Ocurrió un error al registrar la solicitud. Por favor, contacte al administrador del sistema.";
            }
        }
        public DataTable ObtenerSolicitudes()
        {
            try
            {
                // Llamada a la capa de acceso a datos
                return _solicitudRepository.ObtenerSolicitudTable();
            }
            catch (Exception ex)
            {
                // Manejo del error, por ejemplo, registrándolo en un log o mostrando un mensaje personalizado
                Console.WriteLine("Error en la capa de lógica de negocios: " + ex.Message);
                throw new ApplicationException("Se produjo un error al obtener las solicitudes. Por favor, intente más tarde.", ex);
            }
        }

        public DataTable ObtenerSolicitantePorIDyCI(string identificacion, string codigoInterno)
        {
            try
            {
                

                return _solicitudRepository.ObtenerSolicitantePorIDyCI(identificacion, codigoInterno);
            }
            catch (Exception ex)
            {
                // Log o manejo de excepciones personalizado (puedes reemplazar por una solución de logs)
                throw new Exception($"Error al obtener el solicitante: {ex.Message}", ex);
            }
        }



        public void GenerarPDFReporte(string rutaArchivo)
        {
            DataTable data = ObtenerSolicitudes();

            if (data.Rows.Count == 0)
            {
                throw new Exception("No hay datos disponibles para generar el reporte.");
            }

            Document documento = new Document(PageSize.A4);
            documento.AddTitle("Reporte de Solicitudes");
            documento.AddAuthor("Sistema de Gestión");
            documento.AddCreationDate();

            try
            {
                PdfWriter writer = PdfWriter.GetInstance(documento, new FileStream(rutaArchivo, FileMode.Create));
                documento.Open();

                // Agregar encabezado
                Paragraph titulo = new Paragraph("Reporte de Solicitudes\n\n", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                titulo.Alignment = Element.ALIGN_CENTER;
                documento.Add(titulo);

                // Agregar descripción y fecha
                Paragraph descripcion = new Paragraph($"Este reporte muestra las solicitudes con productos no disponibles.\n" +
                                                       $"Fecha de creación: {DateTime.Now:dd/MM/yyyy}\n\n",
                                                       FontFactory.GetFont(FontFactory.HELVETICA, 12));
                descripcion.Alignment = Element.ALIGN_LEFT;
                documento.Add(descripcion);

                // Agregar tabla
                PdfPTable tabla = new PdfPTable(data.Columns.Count) { WidthPercentage = 100 };
                tabla.HorizontalAlignment = Element.ALIGN_CENTER;

                // Encabezados
                foreach (DataColumn columna in data.Columns)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(columna.ColumnName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10)));
                    celda.BackgroundColor = BaseColor.LIGHT_GRAY;
                    celda.HorizontalAlignment = Element.ALIGN_CENTER;
                    tabla.AddCell(celda);
                }

                // Filas
                foreach (DataRow fila in data.Rows)
                {
                    foreach (var item in fila.ItemArray)
                    {
                        PdfPCell celda = new PdfPCell(new Phrase(item.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)));
                        celda.HorizontalAlignment = Element.ALIGN_LEFT;
                        tabla.AddCell(celda);
                    }
                }

                documento.Add(tabla);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el PDF: {ex.Message}");
            }
            finally
            {
                documento.Close();
            }
        }

    }
}
