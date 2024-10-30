using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogIn_dotnetframework_sqlserver.Models
{
    //clase de entidad para moldear o mapear la estructura de mi tabla Usuarios
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public Rol IdRol { get; set; }
    }
}