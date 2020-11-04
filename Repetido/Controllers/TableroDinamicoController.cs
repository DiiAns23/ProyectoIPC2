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
    public class TableroDinamicoController : Controller
    {
        static List<TableroDinamicoP> listaT = new List<TableroDinamicoP>();
        static List<TableroDinamicoP> listaC = new List<TableroDinamicoP>();
        static List<int> posibles = new List<int>();
        static string J1 = LoginController.user;
        static string C1 = "";
        static string J2 = "";
        static string C2 = "";
        static bool respuesta, fin = false;
        static int TirosNegros = 0;
        static int TirosBlancos = 0;
        public ActionResult Tablero()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tablero(TableroDinamico datos)
        {
            string nombre = datos.jugador2;
            string color = datos.colorj2;
            int columnas = datos.columnas;
            int filas = datos.filas;
            string modo = datos.modo;
            TempData["J1"] = J1;
            if (nombre != null && color != null)
            {
                J2 = nombre;
                TempData["J2"] = J2;
                if (color == "Blanco")
                {
                    C2 = "white";
                    TempData["C2"] = C2;
                    TempData["C1"] = "black";
                }
                else
                {
                    C2 = "black";
                    TempData["C2"] = C2;
                    TempData["C1"] = "white";
                }
            }
            else
            {
                J2 = "Invitado";
                C2 = "white";
                TempData["C1"] = "black";
                TempData["J2"] = J2;
                TempData["C2"] = C2;
            }
            for (int i = 0; i < filas*columnas; i++)
            {
                TableroDinamicoP test = new TableroDinamicoP(i);
                listaT.Add(test);
                listaC.Add(test);
            }
            TempData["Columnas"] = datos.columnas;
            TempData["Filas"] = datos.filas;
            TempData["Modo"] = datos.modo;
            TempData["Color"] = "black";
            TempData["TirosBlancos"] = 0;
            TempData["TirosNegros"] = 0;
            TempData["Modo"] = modo;
            TempData["Fin"] = false;

            return View(listaT);
        }
    }
}