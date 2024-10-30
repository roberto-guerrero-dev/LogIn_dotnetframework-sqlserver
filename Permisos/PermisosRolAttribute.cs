using LogIn_dotnetframework_sqlserver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogIn_dotnetframework_sqlserver.Permisos
{
    //Clase para crear el filtro de autorizacion
    public class PermisosRolAttribute : ActionFilterAttribute
    {
        //Definimos el atributo de la clase
        private Rol idRol;

        //Definimos el contrusctor el cual recibe un parametro de tipo Rol
        //el cual inicializara el atributo idRol
        public PermisosRolAttribute(Rol _idrol)
        {
            idRol = _idrol;
        }

        //Sobreescribimos el metodo heredado para crear nuestro filtro
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Se valida si hay una sesion activa
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                //si existe una sesion convertimos los datos de la sesion en
                //un objeto de tipo Usuarios
                Usuarios usuario = HttpContext.Current.Session["Usuario"] as Usuarios;

                //validamos si el rol no es el esperado para redirigir a la pagina
                //de sin permiso
                if(usuario.IdRol != this.idRol)
                {
                    //se instancia el objeto RedirectResult con la ruta de la pagina
                    //mencionada anteriormente
                    filterContext.Result = new RedirectResult("~/Home/SinPermiso");
                }
            }
            //se invoca el metodo y se le pasa como parametro el filtro que creamos
            base.OnActionExecuting(filterContext);
        }
    }
}