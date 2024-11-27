using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Identificacion { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
        public int EstadoUsuario { get; set; }
    }
}
