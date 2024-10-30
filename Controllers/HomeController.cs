using LogIn_dotnetframework_sqlserver.Permisos;
using LogIn_dotnetframework_sqlserver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LogIn_dotnetframework_sqlserver.Controllers
{
    [Authorize]//esta es la cookie que restringe el acceso a los modulos si no hay una sesion activa
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [PermisosRol(Rol.Administrador)]//se restringe el acceso al modulo Contact para el usuario 'Asistente'
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult SinPermiso()
        {
            ViewBag.Message = "No tiene permisos para acceder";
            return View();
        }

        public ActionResult CerrarSesion()
        {
            //aca se destruye la cookie y la sesion para poder continuar con otro loggeo
            FormsAuthentication.SignOut();

            Session["Usuario"] = null;

            return RedirectToAction("LogIn","Acceso");
        }

    }
}