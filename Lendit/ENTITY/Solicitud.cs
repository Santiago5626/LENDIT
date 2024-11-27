using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Solicitud
    {
        public int IdSolicitud { get; set; }
        public int IdTercero { get; set; }
        public string SerialProducto { get; set; }
        public int Estado { get; set; }
    }
}
