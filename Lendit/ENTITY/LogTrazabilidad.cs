using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class LogTrazabilidad
    {
        public int IdLog { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Accion { get; set; }
        public string NombreTabla { get; set; }
        public string DatosAnteriores { get; set; }
        public string DatosNuevos { get; set; }
    }
}
