using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repetido.Models.Modelos;
using Repetido.Controllers;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml;

namespace Repetido.Controllers
{
    public class TableroDinamicoController : Controller
    {
        static List<TableroDinamicoP> listaT = new List<TableroDinamicoP>();
        static List<TableroDinamicoP> listaC = new List<TableroDinamicoP>();
        static List<string> listaColores1 = new List<string>();
        static List<string> listaColores2 = new List<string>();
        static List<int> posibles = new List<int>();
        static string J1 = LoginController.user;
        static string C1 = "";
        static string J2 = "";
        static string C2 = "", modo = "";
        static int TirosNegros = 0;
        static int TirosBlancos = 0;
        static int columnas = 0, filas = 0;
        public static int color1 = 0, color2 = 0;
        static string[] letras = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T" };
        public ActionResult Tablero()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tablero(TableroDinamico datos)
        {
            string nombre = datos.jugador2;
            string color = datos.colorj2;
            columnas = datos.columnas;
            filas = datos.filas;
            modo = datos.modo;
            string posicion;
            TempData["J1"] = J1;
            if (nombre != null && color != null)
            {
                J2 = nombre;
                TempData["J2"] = J2;
                if (color == "Blanco")
                {
                    C2 = "blanco";
                    TempData["C2"] = C2;
                    TempData["C1"] = "negro";
                    listaColores1.Add("negro");
                    listaColores2.Add("blanco");
                    TempData["Turno"] = listaColores1[0];
                }
                else
                {
                    C2 = "negro";
                    TempData["C2"] = C2;
                    TempData["C1"] = "blanco";
                    listaColores1.Add("blanco");
                    listaColores2.Add("negro");
                    TempData["Turno"] = listaColores2[0];
                }
            }
            else
            {
                J2 = "Invitado";
                C2 = "blanco";
                TempData["C1"] = "negro";
                TempData["J2"] = J2;
                TempData["C2"] = C2;
            }

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    posicion = letras[j];
                    int a = i + 1;
                    string b = a.ToString();
                    posicion = posicion + b;
                    TableroDinamicoP test = new TableroDinamicoP(posicion);
                    listaT.Add(test);
                    listaC.Add(test);
                }

            }

            TempData["Turno"] = "J1";
            TempData["Color"] = listaColores1[0];

            TempData["Columnas"] = datos.columnas;
            TempData["Filas"] = datos.filas;
            TempData["Modo"] = datos.modo;
            TempData["TirosBlancos"] = 0;
            TempData["TirosNegros"] = 0;
            TempData["Modo"] = modo;
            TempData["Fin"] = false;
            ViewBag.Colores1 = listaColores1;
            ViewBag.Colores2 = listaColores2;
            return View(listaT);
        }

        [HttpPost]
        public ActionResult Tiro(string ficha)
        {
            if (TempData["Turno"].Equals("J1"))
            {
                TableroDinamicoP nficha = new TableroDinamicoP(ficha,listaColores1[color1]);
                for(int x = 0; x < listaT.Count(); x++)
                {
                    if (listaT[x].index == ficha)
                    {
                        listaT[x] = nficha;
                        TempData["Color"] = listaColores2[color2];
                        TempData["Turno"] = "J2";
                        j1();
                    }
                }
            }
            else
            {
                TableroDinamicoP nficha = new TableroDinamicoP(ficha, listaColores2[color2]);
                for (int x = 0; x < listaT.Count(); x++)
                {
                    if (listaT[x].index == ficha)
                    {
                        listaT[x] = nficha;
                        TempData["Color"] = listaColores1[color1];
                        TempData["Turno"] = "J1";
                        j2();
                    }
                }
            }
            TempData["J1"] = J1;
            TempData["C1"] = C1;
            TempData["J2"] = J2;
            TempData["C2"] = C2;
            TempData["Columnas"] = columnas;
            TempData["Filas"] = filas;
            ViewBag.Colores1 = listaColores1;
            ViewBag.Colores2 = listaColores2;
            return View("Tablero", listaT);
        }

        public ActionResult Upload(HttpPostedFileBase path, string nombre, string color2)
        {
            columnas = 0;
            filas = 0;
            listaColores1.Clear();
            listaColores2.Clear();
            J1 = LoginController.user;
            if (nombre != null && color2 != null)
            {
                J2 = nombre;
                TempData["J2"] = J2;
                if (color2 == "Blanco")
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
            listaC.Clear();
            listaT.Clear();
            string sigTiro = "";
            string ruta;
            string posicion;
            Cargar cargar = new Cargar();
            if (path != null)
            {
                ruta = Server.MapPath("~/Temporal/");  // C:\Users\diego\Downloads\[IPC2]Proyecto1_Entregable2_201903865\Repetido\Temporal\
                ruta += path.FileName; // C:\Users\diego\Downloads\[IPC2]Proyecto1_Entregable2_201903865\Repetido\Temporal\DiiAns23.xml
                cargar.CargarArchivo(ruta, path);
                XmlDocument partida = new XmlDocument();
                partida.Load(ruta); //  C:\Users\diego\Downloads\[IPC2]Proyecto1_Entregable2_201903865\Repetido\Temporal\DiiAns23.xml
                foreach (XmlNode xmlNode in partida.DocumentElement.ChildNodes)
                {

                    if (xmlNode.Name.Equals("filas"))
                    {
                        filas = Convert.ToInt32(xmlNode.InnerText);
                    }
                    else if (xmlNode.Name.Equals("columnas"))
                    {
                        columnas = Convert.ToInt32(xmlNode.InnerText);
                        if (filas != 0 && columnas != 0)
                        {
                            for (int i = 0; i < filas; i++)
                            {
                                for (int j = 0; j < columnas; j++)
                                {
                                    posicion = letras[j];
                                    int a = i + 1;
                                    string b = a.ToString();
                                    posicion = posicion + b;
                                    TableroDinamicoP test = new TableroDinamicoP(posicion);
                                    listaT.Add(test);
                                    listaC.Add(test);
                                }
                            }
                        }
                    }
                    else if (xmlNode.Name.Equals("Jugador1"))
                    {
                        foreach(XmlNode xmlNodeItem in xmlNode.ChildNodes)
                        {
                            listaColores1.Add(xmlNodeItem.InnerText);
                        }
                    }
                    else if (xmlNode.Name.Equals("Jugador2"))
                    {
                        foreach (XmlNode xmlNodeItem in xmlNode.ChildNodes)
                        {
                            listaColores2.Add(xmlNodeItem.InnerText);
                        }
                    }
                    else if (xmlNode.Name.Equals("Modalidad"))
                    {
                        modo = xmlNode.InnerText;
                    }
                    else if (xmlNode.Name.Equals("tablero"))
                    {
                        foreach (XmlNode hijos1 in xmlNode.ChildNodes)
                        {
                            if (hijos1.Name.Equals("ficha"))
                            {
                                string color="", columna="", fila="";
                                foreach (XmlNode hijos2 in hijos1.ChildNodes)
                                {
                                    if (hijos2.Name.Equals("color"))
                                    {
                                        color = hijos2.InnerText;
                                    }
                                    if (hijos2.Name.Equals("columna"))
                                    {
                                        columna = hijos2.InnerText;
                                    }
                                    if (hijos2.Name.Equals("fila"))
                                    {
                                        fila = hijos2.InnerText;
                                    }
                                    if(color!="" && columna!="" && fila != "")
                                    {
                                        TableroDinamicoP nficha = new TableroDinamicoP(columna+fila, color);
                                        for (int x = 0; x < listaT.Count(); x++)
                                        {
                                            if (listaT[x].index == columna + fila)
                                            {
                                                listaT[x] = nficha;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (hijos1.Name.Equals("siguienteTiro"))
                            {
                                foreach (XmlNode tiro in hijos1.ChildNodes)
                                {
                                    if (tiro.Name.Equals("color"))
                                    {
                                        sigTiro = tiro.InnerText;
                                        for(int x = 0; x < listaColores1.Count(); x++)
                                        {
                                            if (listaColores1[x].Equals(sigTiro))
                                            {
                                                TempData["Color"] = sigTiro;
                                            }
                                        }
                                        for (int x = 0; x < listaColores2.Count(); x++)
                                        {
                                            if (listaColores2[x].Equals(sigTiro))
                                            {
                                                TempData["Color"] = sigTiro;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
            TirosNegros = 0;
            TirosBlancos = 0;
            TempData["TirosBlancos"] = TirosBlancos;
            TempData["TirosNegros"] = TirosNegros;
            TempData["J1"] = J1;
            TempData["C1"] = C1;
            TempData["J2"] = J2;
            TempData["C2"] = C2;
            TempData["Columnas"] = columnas;
            TempData["Filas"] = filas;
            ViewBag.Colores1 = listaColores1;
            ViewBag.Colores2 = listaColores2;
            return View("Tablero", listaT);
        }
        public ActionResult Personalizada(string Negro, string Blanco, string Rojo, string Gris, string Anaranjado,
            string Violeta, string Verde, string Celeste, string Amarillo, string Azul, int fil, int col)
        {
            if (Negro == null && Blanco == null && Rojo == null && Gris == null && Anaranjado == null && Violeta == null
                && Verde == null && Celeste == null && Amarillo == null && Azul == null)
            {
                return RedirectToAction("Jugar", "Pantallaprincipal");
            }
            else
            {
                if (Negro != null)
                {
                    listaColores1.Add("negro");
                }
                if (Blanco != null)
                {
                    listaColores1.Add("blanco");
                }
                if (Rojo != null)
                {
                    listaColores1.Add("rojo");
                }
                if (Gris != null)
                {
                    listaColores1.Add("gris");
                }
                if (Anaranjado != null)
                {
                    listaColores1.Add("orange");
                }


                if (Violeta != null)
                {
                    listaColores2.Add("violeta");
                }
                if (Verde != null)
                {
                    listaColores2.Add("verde");
                }
                if (Celeste != null)
                {
                    listaColores2.Add("celeste");
                }
                if (Amarillo != null)
                {
                    listaColores2.Add("amarillo");
                }
                if (Azul != null)
                {
                    listaColores2.Add("azul");
                }
            }
            TempData["Columnas"] = col;
            TempData["Filas"] = fil;
            TempData["TirosBlancos"] = 0;
            TempData["TirosNegros"] = 0;
            TempData["Modo"] = modo;
            TempData["Fin"] = false;
            TempData["Turno"] = "J1";
            TempData["Color"] = listaColores1[0];
            TempData["C1"] = "";
            TempData["C2"] = "";
            string posicion;
            filas = fil;
            columnas = col;
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    posicion = letras[j];
                    int a = i + 1;
                    string b = a.ToString();
                    posicion = posicion + b;
                    TableroDinamicoP test = new TableroDinamicoP(posicion);
                    listaT.Add(test);
                    listaC.Add(test);
                }

            }
            ViewBag.Colores1 = listaColores1;
            ViewBag.Colores2 = listaColores2;
            return View("Tablero", listaT);
        }
        public int j1()
        {
            if (color1 < listaColores1.Count - 1)
            {
                color1++;
            }
            else
            {
                color1 = 0;
            }
            return color1;
        }
        public int j2()
        {
            if (color2 < listaColores2.Count - 1)
            {
                color2++;
            }
            else
            {
                color2 = 0;
            }
            return color2;
        }

    }
}