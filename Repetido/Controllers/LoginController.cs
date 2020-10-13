using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Repetido.Models;
using Repetido.Models.Modelos;

namespace Repetido.Controllers
{
    public class LoginController : Controller
    {
        public static string user = "";
        public static int idUser = 0;
        public ActionResult Login(LoginView model)
        {
            if (!string.IsNullOrEmpty(model.usuario) && !string.IsNullOrEmpty((model.pass)))
            {
                using (BaseDatos2 db = new BaseDatos2())
                {
                    var usuario = db.Jugador.FirstOrDefault(dato => dato.Nombre_Usuario == model.usuario && dato.Pass == model.pass);
                    if (usuario != null)
                    {
                        //se encuentra un usuario con los datos de Usuario y Password
                        FormsAuthentication.SetAuthCookie(usuario.Nombre_Usuario, true);
                        user = usuario.Nombre_Usuario;
                        idUser = usuario.Id_Jugador;
                        return RedirectToAction("PantallaPrincipal", "PantallaPrincipal");
                    }
                }
            }
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        
    }
}