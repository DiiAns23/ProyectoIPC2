using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repetido.Models.Modelos;
using System.Xml.Linq;
using System.Xml;
using Repetido.Controllers;
using System.Diagnostics;
namespace Repetido.Controllers
{
    public class AperturaController : Controller
    {
        public static Stopwatch osW = new Stopwatch();
        public static List<PersonalizadaModel> ListaColores = new List<PersonalizadaModel>();
        public static List<string> colores = new List<string>();
        public static List<string> colores2 = new List<string>();
        public static int color1 = 0, color2 = 0;

        public ActionResult Xtreme()
        {
            return View();
        }
        public ActionResult Personalizada()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Personalizada(string Negro, string Blanco, string Rojo, string Cafe, string Anaranjado,
            string Magenta, string Verde, string Celeste, string Amarillo, string Azul)
        {
            if (Negro == null && Blanco == null && Rojo == null && Cafe == null && Anaranjado == null && Magenta == null
                && Verde == null && Celeste == null && Amarillo == null && Azul == null)
            {
                return RedirectToAction("Jugar", "Pantallaprincipal");
            }
            else
            {
                if (Negro != null)
                {
                    colores.Add("black");
                }
                if (Blanco != null)
                {
                    colores.Add("white");
                }
                if (Rojo != null)
                {
                    colores.Add("red");
                }
                if (Cafe != null)
                {
                    colores.Add("coffe");
                }
                if (Anaranjado != null)
                {
                    colores.Add("orange");
                }


                if (Magenta != null)
                {
                    colores2.Add("magenta");
                }
                if (Verde != null)
                {
                    colores2.Add("greenF");
                }
                if (Celeste != null)
                {
                    colores2.Add("skyblue");
                }
                if (Amarillo != null)
                {
                    colores2.Add("yellow");
                }
                if (Azul != null)
                {
                    colores2.Add("blue");
                }
            }

            for (int i = 0; i < 64; i++)
            {
                PersonalizadaModel test = new PersonalizadaModel(i);
                ListaColores.Add(test);
            }
            ListaColores[27] = new PersonalizadaModel(27, "gray");
            ListaColores[28] = new PersonalizadaModel(28, "gray");
            ListaColores[35] = new PersonalizadaModel(35, "gray");
            ListaColores[36] = new PersonalizadaModel(36, "gray");
            TempData["Turno"] = "J1";
            TempData["Color"] = colores[0];
            return View(ListaColores);
        }
        public ActionResult Tiro(int io)
        {
            if (io != -1)
            {
                if (TempData["Turno"].Equals("J1"))
                {
                    PersonalizadaModel fichaNueva = new PersonalizadaModel(io, colores[color1]);
                    ListaColores[io] = fichaNueva;
                    TempData["Color"] = colores2[color2];
                    TempData["Turno"] = "J2";
                    J1();
                }
                else
                {
                    PersonalizadaModel fichaNueva = new PersonalizadaModel(io, colores2[color2]);
                    ListaColores[io] = fichaNueva;
                    TempData["Color"] = colores[color1];
                    TempData["Turno"] = "J1";
                    J2();
                }
            }
            return View("Personalizada", ListaColores);
        }
        public int J1()
        {
            if (color1 < colores.Count-1)
            {
                color1++;
            }
            else
            {
                color1 = 0;
            }
            return color1;
        }
        public int J2()
        {
            if (color2 < colores2.Count-1)
            {
                color2++;
            }
            else
            {
                color2 = 0;
            }
            return color2;
        }
        public void VerificarTiro(int io)
        {

        }
        public Boolean VerificarArriba(int io2)
        {
            int aux = io2 - 8;
            List<int> arriba = new List<int>();
            //Revisar Arriba
            if (aux < 0)
            {
                return false;
            }
            else
            {
                while (aux >= 0)
                {
                    if (aux < 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (colores2.Contains(ListaColores[io2].Color))
                        {
                            if (ListaColores[aux].Color.Equals("") || ListaColores[aux].Color.Equals("gray"))
                            {
                                return false;
                            }
                            else
                            {
                                arriba.Add(aux);
                                aux = aux - 8;
                            }

                        }
                        else
                        {
                            if (arriba.Count() == 0)
                            {
                                return false;
                            }
                            else
                            {
                                for (int x = 0; x < arriba.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PersonalizadaModel fichaNueva = new PersonalizadaModel(arriba[x], "black");
                                        ListaColores[arriba[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PersonalizadaModel fichaNueva = new PersonalizadaModel(arriba[x], "white");
                                        ListaColores[arriba[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }

                            }
                            aux = -1;
                        }
                    }
                }
                return true;
            }
        }
    }
}