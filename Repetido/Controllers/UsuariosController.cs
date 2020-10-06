using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repetido.Models;
using Repetido.Models.Modelos;

namespace Repetido.Controllers
{
    public class UsuariosController : Controller
    {
        public ActionResult Registrar()
        {
            List<PaisViewModel> ListaPais;
            using (BaseDatos1 db = new BaseDatos1())
            {
                ListaPais = (from dato in db.Pais
                             select new PaisViewModel
                             {
                                 codigo = dato.Id_Pais,
                                 nombre = dato.Nombre,
                                 abrev = dato.Abrev
                             }).ToList();

            }
            ViewBag.Paises = ListaPais;
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(RegistrarViewModel model)
        {
            List <PaisViewModel> ListaPais;
            using (BaseDatos1 db = new BaseDatos1())
            {
                ListaPais = (from dato in db.Pais
                             select new PaisViewModel
                             {
                                 codigo = dato.Id_Pais,
                                 nombre = dato.Nombre,
                                 abrev = dato.Abrev
                             }).ToList();

            }
            ViewBag.Paises = ListaPais;
            if (ModelState.IsValid)
            {
                using (BaseDatos1 db = new BaseDatos1())
                {
                    var user = db.Jugador.FirstOrDefault(dato => dato.Nombre_Usuario == model.Usuario);
                    var correo = db.Jugador.FirstOrDefault(dato => dato.Correo == model.Correo_electronico);
                    if (user == null)
                    {
                        if (model.contaseña == model.recontaseña)
                        {
                            if (correo == null)
                            {
                                var Usuario = new Jugador();
                                Usuario.Nombres_Jugador = model.nombre;
                                Usuario.Apellidos_Jugador = model.Apellido;
                                Usuario.Nombre_Usuario = model.Usuario;
                                Usuario.Correo = model.Correo_electronico;
                                Usuario.Pass = Encrypt.GetSHA256(model.contaseña);
                                Usuario.Confirm_Pass = Encrypt.GetSHA256(model.recontaseña);
                                Usuario.Id_Pais = model.codigo_pais;
                                db.Jugador.Add(Usuario);
                                db.SaveChanges();
                                return RedirectToAction("Login", "Login");
                            }
                        }
                    }
                }
            }
            return View();
        }

    }
}
