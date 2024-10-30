using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LogIn_dotnetframework_sqlserver.Models;
using LogIn_dotnetframework_sqlserver.Data;
using System.Web.Security;

namespace LogIn_dotnetframework_sqlserver.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult LogIn()
        {
            ViewBag.Message = "Sign In";

            return View();
        }

        [HttpPost]
        //este metodo de tipo POST hace la validacion del usuario
        public ActionResult LogIn(string correo, string clave)
        {
            //definimos un objeto Usuarios y le asignamos el objeto que retorna
            //el metodo ValidarUsuario() de la clase D_Usuario que es otro objeto 
            //de tipo Usuario
            Usuarios obj = new D_Usuario().ValidarUsuario(correo, clave);

            if(obj.Nombre != null)//validamos si hubo una coincidencia
            {
                //si existe el usuario se crea una cookie de autorizacion la
                //cual permitira restringir el acceso al sistema si el usuario 
                //no se loggea
                FormsAuthentication.SetAuthCookie(obj.Correo,false);

                //creamos nuestras variables de sesion

                Session["Usuario"] = obj;//esta variable la creamos para obtener todos los datos del usuario
                                         //pero mas importante el IdRol del usuario para crear la restriccion
                                         //a las diferentes opciones del menu segun el rol
                
                Session["Rol"] = obj.IdRol;//esta variable obtiene solo el IdRol ya que necesito solo ese valor
                                           //para hacer dinamica la vista en el layout y no mostrar tampoco las
                                           //opciones a las cual no tiene permiso el usuario

                return RedirectToAction("Index","Home");//si el login tiene exito se redirige al modulo principal
            }

            return View();//si no hay coincidencia de usuario simplemente se recarga la pagina
        }
    }
}