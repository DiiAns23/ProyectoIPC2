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
        public ActionResult Login(LoginView model)
        {
            if (!string.IsNullOrEmpty(model.usuario) && !string.IsNullOrEmpty(Encrypt.GetSHA256(model.pass)))
            {
                using (BaseDatos1 db = new BaseDatos1())
                {
                    var usuario = db.Jugador.FirstOrDefault(dato => dato.Nombre_Usuario == model.usuario && dato.Pass == model.pass);
                    if (usuario != null)
                    {
                        //se encuentra un usuario con los datos de Usuario y Password
                        FormsAuthentication.SetAuthCookie(usuario.Nombre_Usuario, true);
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