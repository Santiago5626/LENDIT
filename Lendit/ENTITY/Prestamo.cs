using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public int IdSolicitud { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
