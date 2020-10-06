using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repetido.Models.Modelos;

namespace Repetido.Controllers
{
    [Authorize]
    public class PantallaPrincipalController : Controller
    {
        static List<PartidaIndividual> listaT = new List<PartidaIndividual>();
        public ActionResult PantallaPrincipal()
        {
            return View();
        }
        public ActionResult Jugar()
        {
            return View();
        }
        public ActionResult Torneo()
        {
            return View();
        }
        public ActionResult Historial()
        {
            return View();
        }
        public ActionResult Individual()
        {
            TempData["title"] = "Individual";
            for (int i = 0; i < 64; i++)
            {
                PartidaIndividual test = new PartidaIndividual(i);
                listaT.Add(test);
            }
            listaT[27] = new PartidaIndividual(27, "black");
            listaT[36] = new PartidaIndividual(36, "black");
            listaT[28] = new PartidaIndividual(28, "white");
            listaT[35] = new PartidaIndividual(35, "white");
            TempData["Color"] = "black";
            return View(listaT);
        }

        public ActionResult Change(int io)
        {
            if (io != -1)
            {
                if (TempData["Color"].Equals("black"))
                {
                    PartidaIndividual fichaNueva = new PartidaIndividual(io, "black");
                    listaT[io] = fichaNueva;
                    TempData["Color"] = "white";
                }
                else
                {
                    PartidaIndividual fichaNueva = new PartidaIndividual(io, "white");
                    listaT[io] = fichaNueva;
                    TempData["Color"] = "black";
                }
                change2(io);
            }
            return View("Individual", listaT);
        }

        public void change2(int io2)
        {
            int aux = io2 - 8;
            int aux2 = io2 + 8;
            int aux3 = io2 + 1;
            int aux4 = io2 - 1;
            int aux5 = io2 - 9;
            int aux6 = io2 - 7;
            int aux7 = io2 + 7;
            int aux8 = io2 + 9;
            bool arr = true, ab = true, der = true, izq = true, supizq = true, supder = true, infeizq = true, infeder = true;
            List<int> arriba = new List<int>();
            List<int> abajo = new List<int>();
            List<int> derecha = new List<int>();
            List<int> izquierda = new List<int>();
            List<int> superiorizquierda = new List<int>();
            List<int> superiorderecha = new List<int>();
            List<int> inferiorizquierda = new List<int>();
            List<int> inferiorderecha = new List<int>();
            //Revisar Arriba
            if (aux < 0)
            {
                arr = false;
            }
            else
            {
                while (aux >= 0)
                {
                    if (aux < 0)
                    {
                        arr = false;
                        aux = -1;
                    }
                    else
                    {
                        if (listaT[io2].Color != listaT[aux].Color)
                        {
                            if (listaT[aux].Color.Equals(""))
                            {
                                arr = false;
                                aux = -1;
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
                                arr = false;
                            }
                            else
                            {
                                for (int x = 0; x < arriba.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(arriba[x], "black");
                                        listaT[arriba[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(arriba[x], "white");
                                        listaT[arriba[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }
                            }
                            aux = -1;
                        }
                    }

                }
            }
            //Revisar Abajo
            if (aux2 >= 64)
            {
                ab = false;
            }
            else
            {
                while (aux2 < 64)
                {
                    if (aux2 >= 64)
                    {
                        ab = false;
                    }
                    else
                    {
                        if (listaT[io2].Color != listaT[aux2].Color)
                        {
                            if (listaT[aux2].Color.Equals(""))
                            {
                                ab = false;
                                aux2 = 65;
                            }
                            else
                            {
                                abajo.Add(aux2);
                                aux2 = aux2 + 8;
                            }
                        }
                        else
                        {
                            if (abajo.Count() == 0)
                            {
                                ab = false;
                            }
                            else
                            {
                                for (int x = 0; x < abajo.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(abajo[x], "black");
                                        listaT[abajo[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(abajo[x], "white");
                                        listaT[abajo[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }
                            }
                            aux2 = 65;
                        }
                    }
                }
            }
            //Revisar Derecha
            int contador1 = 0;
            while (aux3 > 8)
            {
                aux3 = aux3 - 8;
                contador1++;
            }
            while (aux3 <= 8)
            {
                if ((aux3 + contador1 * 8) <= 63)
                {
                    if (listaT[io2].Color != listaT[aux3 + contador1 * 8].Color)
                    {
                        if (listaT[aux3 + contador1 * 8].Color.Equals(""))
                        {
                            der = false;
                            aux3 = 9;
                        }
                        else
                        {
                            derecha.Add(aux3 + contador1 * 8);
                            aux3 = aux3 + 1;
                        }

                    }
                    else
                    {
                        if (derecha.Count() == 0)
                        {
                            der = false;
                        }
                        else
                        {
                            for (int x = 0; x < derecha.Count(); x++)
                            {
                                if (TempData["Color"].Equals("white"))
                                {
                                    PartidaIndividual fichaNueva = new PartidaIndividual(derecha[x], "black");
                                    listaT[derecha[x]] = fichaNueva;
                                    TempData["Color"] = "white";
                                }
                                else if (TempData["Color"].Equals("black"))
                                {
                                    PartidaIndividual fichaNueva = new PartidaIndividual(derecha[x], "white");
                                    listaT[derecha[x]] = fichaNueva;
                                    TempData["Color"] = "black";
                                }
                            }
                        }
                        aux3 = 9;
                    }
                }
                else
                {
                    der = false;
                    aux3 = 9;
                }
            }
            //Revisar Izquierda
            int contador2 = 0;
            while (aux4 > 8)
            {
                aux4 = aux4 - 8;
                contador2++;
            }
            if (aux4 < 0)
            {
                izq = false;
            }
            else
            {
                while (aux4 >= 0)
                {
                    if (aux4 + contador2 * 8 >= 0)
                    {
                        if (listaT[io2].Color != listaT[aux4 + contador2 * 8].Color)
                        {
                            if (listaT[aux4 + contador2 * 8].Color.Equals(""))
                            {
                                izq = false;
                                aux4 = -1;
                            }
                            else
                            {
                                izquierda.Add(aux4 + contador2 * 8);
                                aux4 = aux4 - 1;
                            }

                        }
                        else
                        {
                            if (izquierda.Count() == 0)
                            {
                                izq = false;
                            }
                            else
                            {
                                for (int x = 0; x < izquierda.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(izquierda[x], "black");
                                        listaT[izquierda[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(izquierda[x], "white");
                                        listaT[izquierda[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }
                            }
                            aux4 = -1;
                        }
                    }
                    else
                    {
                        izq = false;
                        aux4 = -1;
                    }
                }
            }
            //Revisar Superior Izquierda
            if (aux5 < 0)
            {
                supizq = false;
            }
            else
            {
                while (aux5 >= 0)
                {
                    if (aux5 < 0)
                    {
                        supizq = false;
                        aux5 = -1;
                    }
                    else
                    {
                        if (listaT[io2].Color != listaT[aux5].Color)
                        {
                            if (listaT[aux5].Color.Equals(""))
                            {
                                supizq = false;
                                aux5 = -1;
                            }
                            else
                            {
                                superiorizquierda.Add(aux5);
                                aux5 = aux5 - 9;
                            }
                        }
                        else
                        {
                            if (superiorizquierda.Count() == 0)
                            {
                                supizq = false;
                            }
                            else
                            {
                                for (int x = 0; x < superiorizquierda.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(superiorizquierda[x], "black");
                                        listaT[superiorizquierda[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(superiorizquierda[x], "white");
                                        listaT[superiorizquierda[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }
                            }
                            aux5 = -1;
                        }
                    }
                }
            }
            //Revisar Superior Derecha
            if (aux6 < 0)
            {
                supder = false;
            }
            else
            {
                while (aux6 >= 0)
                {
                    if (aux6 < 0)
                    {
                        supder = false;
                        aux6 = -1;
                    }
                    else
                    {
                        if (listaT[io2].Color != listaT[aux6].Color)
                        {
                            if (listaT[aux6].Color.Equals(""))
                            {
                                supder = false;
                                aux6 = -1;
                            }
                            else
                            {
                                superiorderecha.Add(aux6);
                                aux6 = aux6 - 7;
                            }
                        }
                        else
                        {
                            if (superiorderecha.Count() == 0)
                            {
                                supder = false;
                            }
                            else
                            {
                                for (int x = 0; x < superiorderecha.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(superiorderecha[x], "black");
                                        listaT[superiorderecha[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(superiorderecha[x], "white");
                                        listaT[superiorderecha[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }
                            }
                            aux6 = -1;
                        }
                    }
                }
            }

            //Revisar Inferior Izquierda
            if (aux7 > 64)
            {
                infeizq = false;
            }
            else
            {
                while (aux7 <= 63)
                {
                    if (aux7 > 63)
                    {
                        infeizq = false;
                        aux7 = 64;
                    }
                    else
                    {
                        if (listaT[io2].Color != listaT[aux7].Color)
                        {
                            if (listaT[aux7].Color.Equals(""))
                            {
                                infeizq = false;
                                aux7 = 64;
                            }
                            else
                            {
                                inferiorizquierda.Add(aux7);
                                aux7 = aux7 + 7;
                            }
                        }
                        else
                        {
                            if (inferiorizquierda.Count() == 0)
                            {
                                infeizq = false;
                            }
                            else
                            {
                                for (int x = 0; x < inferiorizquierda.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(inferiorizquierda[x], "black");
                                        listaT[inferiorizquierda[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(inferiorizquierda[x], "white");
                                        listaT[inferiorizquierda[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }
                            }
                            aux7 = 64;
                        }
                    }
                }
            }

            //Revisar Inferior Derecha
            if (aux8 > 64)
            {
                infeizq = false;
            }
            else
            {
                while (aux8 <= 63)
                {
                    if (aux8 > 63)
                    {
                        infeder = false;
                        aux8 = 64;
                    }
                    else
                    {
                        if (listaT[io2].Color != listaT[aux8].Color)
                        {
                            if (listaT[aux8].Color.Equals(""))
                            {
                                infeder = false;
                                aux8 = 64;
                            }
                            else
                            {
                                inferiorderecha.Add(aux8);
                                aux8 = aux8 + 9;
                            }
                        }
                        else
                        {
                            if (inferiorderecha.Count() == 0)
                            {
                                infeder = false;
                            }
                            else
                            {
                                for (int x = 0; x < inferiorderecha.Count(); x++)
                                {
                                    if (TempData["Color"].Equals("white"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(inferiorderecha[x], "black");
                                        listaT[inferiorderecha[x]] = fichaNueva;
                                        TempData["Color"] = "white";
                                    }
                                    else if (TempData["Color"].Equals("black"))
                                    {
                                        PartidaIndividual fichaNueva = new PartidaIndividual(inferiorderecha[x], "white");
                                        listaT[inferiorderecha[x]] = fichaNueva;
                                        TempData["Color"] = "black";
                                    }
                                }
                            }
                            aux8 = 64;
                        }
                    }
                }
            }

            if (arr == true || ab == true || der == true || izq == true || supizq == true || supder == true || infeizq == true || infeder == true)
            {
                if (TempData["Color"].Equals("black"))
                {
                    TempData["Color"] = "black";
                }
                else
                {
                    TempData["Color"] = "white";
                }

            }
            else
            {
                PartidaIndividual fichaNueva = new PartidaIndividual(io2, "");
                listaT[io2] = fichaNueva;
                if (TempData["Color"].Equals("black"))
                {
                    TempData["Color"] = "white";
                }
                else
                {
                    TempData["Color"] = "black";
                }
            }
        }
    }

}