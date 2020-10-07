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
        static List<PartidaIndividual> Punteo = new List<PartidaIndividual>();
        static List<int> posibles = new List<int>();
        static bool respuesta = true;

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
            listaT[20] = new PartidaIndividual(20, "gray");
            listaT[29] = new PartidaIndividual(29, "gray");
            listaT[34] = new PartidaIndividual(34, "gray");
            listaT[43] = new PartidaIndividual(43, "gray");
            posibles.Add(20);
            posibles.Add(29);
            posibles.Add(34);
            posibles.Add(43);
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
                if (respuesta == true)
                {
                    movimientosPosibles(io);
                }
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
                            if (listaT[aux].Color.Equals("") || listaT[aux].Color.Equals("gray"))
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
                            if (listaT[aux2].Color.Equals("") || listaT[aux2].Color.Equals("gray"))
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
            if (io2 == 7 || io2 == 15 || io2 == 23 || io2 == 31 || io2 == 39 || io2 == 47 || io2 == 55 || io2 == 63)
            {
                der = false;
            }
            else
            {
                int contador1 = 0;
                while (aux3 > 7)
                {
                    aux3 = aux3 - 8;
                    contador1++;
                }
                while (aux3 <= 7)
                {
                    if ((aux3 + contador1 * 8) <= 63)
                    {
                        if (listaT[io2].Color != listaT[aux3 + contador1 * 8].Color)
                        {
                            if (listaT[aux3 + contador1 * 8].Color.Equals("") || listaT[aux3 + contador1 * 8].Color.Equals("gray"))
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
            }
            //Revisar Izquierda
            if (io2 == 0 || io2 == 8 || io2 == 16 || io2 == 24 || io2 == 32 || io2 == 40 || io2 == 48 || io2 == 56)
            {
                izq = false;
            }
            else
            {
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
                                if (listaT[aux4 + contador2 * 8].Color.Equals("") || listaT[aux4 + contador2 * 8].Color.Equals("gray"))
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
            }
            //Revisar Superior Izquierda
            if (io2 - 9 < 0)
            {
                supizq = false;
            }
            else
            {
                if(io2 == 8 || io2 == 16 || io2 == 24 || io2 == 32 || io2 == 40 || io2 == 48 || io2 == 56)
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
                                if (listaT[aux5].Color.Equals("") || listaT[aux5].Color.Equals("gray"))
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
            }

            //Revisar Superior Derecha
            if (io2-7<=0)
            {
                supder = false;
            }
            else
            {
                if (io2 == 15 || io2 == 23 || io2 == 31 || io2 == 39 || io2 == 47 || io2 == 55 || io2 == 63)
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
                                if (listaT[aux6].Color.Equals("") || listaT[aux6].Color.Equals("gray"))
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
            }

            //Revisar Inferior Izquierda
            if (io2 >= 56)
            {
                infeizq = false;
            }
            else
            {
                if (io2 == 0 || io2 == 8 || io2 == 16 || io2 == 24 || io2 == 32 || io2 == 40 || io2 == 48)
                {
                    infeder = false;
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
                                if (listaT[aux7].Color.Equals("") || listaT[aux7].Color.Equals("gray"))
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
            }

            //Revisar Inferior Derecha
            if (io2 >= 56)
            {
                infeder = false;
            }
            else
            {
                if (io2 == 7 || io2 == 15 || io2 == 23 || io2 == 31 || io2 == 39 || io2 == 47 || io2 == 55 || io2 == 63)
                {
                    infeder = false;
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
                                if (listaT[aux8].Color.Equals("") || listaT[aux8].Color.Equals("gray"))
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
                respuesta = true;

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
                respuesta = false;
            }
        }

        public void movimientosPosibles(int io2)
        {
            for (int y = 0; y < posibles.Count(); y++)
            {
                if (posibles[y] != io2)
                {
                    PartidaIndividual vaciar = new PartidaIndividual(posibles[y], "");
                    listaT[posibles[y]] = vaciar;
                }
            }
            posibles.Clear();
            
            if (TempData["Color"].Equals("black"))
            {
                for (int f = 0; f < listaT.Count(); f++)
                {
                    bool arr = true, ab = true, der = true, izq = true, supizq = true, supder = true, infeizq = true, infeder = true;
                    int aux1 = 8;
                    int aux2 = 8;
                    int aux3 = 1;
                    int aux4 = 1;
                    int aux5 = 9;
                    int aux6 = 7;
                    int aux7 = 7;
                    int aux8 = 9;
                    if (listaT[f].Color.Equals("white"))
                    {
                        //Revision Superior
                        if ((f - 8) < 0) //Revisa que no sea la primera fila
                        {
                            arr = false;
                        }
                        else
                        {
                            while (aux1 < 64)
                            {
                                if (listaT[f - 8].Color.Equals("white") || listaT[f - 8].Color.Equals("black"))
                                {
                                    aux1 = 64;
                                    arr = false;
                                }
                                else
                                {
                                    if (f + aux1 > 63)
                                    {
                                        aux1 = 64;
                                        arr = false;
                                    }
                                    else
                                    {
                                        if (listaT[f + aux1].Color.Equals("white"))
                                        {
                                            aux1 = aux1 + 8;
                                        }
                                        else if (listaT[f + aux1].Color.Equals("black"))
                                        {
                                            PartidaIndividual fichaPosible = new PartidaIndividual(f - 8, "gray");
                                            listaT[f - 8] = fichaPosible;
                                            posibles.Add(f - 8);
                                            aux1 = 64;
                                        }
                                        else
                                        {
                                            aux1 = 64;
                                            arr = false;
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Inferior
                        if ((f + 8) > 63) //Revisa que no sea la ultima fila
                        {
                            ab = false;
                        }
                        else
                        {
                            while (aux2 < 63)
                            {
                                if (listaT[f + 8].Color.Equals("black") || listaT[f + 8].Color.Equals("white"))
                                {
                                    aux2 = 64;
                                    ab = false;
                                }
                                else
                                {
                                    if (f - aux2 < 0)
                                    {
                                        aux2 = 64;
                                        ab = false;
                                    }
                                    else
                                    {
                                        if (listaT[f - aux2].Color.Equals("white"))
                                        {
                                            aux2 = aux2 + 8;
                                        }
                                        else if (listaT[f - aux2].Color.Equals("black"))
                                        {
                                            PartidaIndividual fichaPosible = new PartidaIndividual(f + 8, "gray");
                                            listaT[f + 8] = fichaPosible;
                                            posibles.Add(f + 8);
                                            aux2 = 64;
                                        }
                                        else
                                        {
                                            aux2 = 64;
                                            ab = false;
                                        }
                                    }
                                }
                            }

                        }

                        //Revision Izquierda
                        if (f == 7 || f == 15 || f == 23 || f == 31 || f == 39 || f == 47 || f == 55 || f == 63)
                        {
                            izq = false;
                        }
                        else
                        {
                            int ayudante1 = f, contador1 = 0;
                            while (ayudante1 > 7)
                            {
                                ayudante1 = ayudante1 - 8;
                                contador1++;
                            }
                            if ((ayudante1 - 1) < 0)
                            {
                                izq = false;
                            }
                            else
                            {
                                while (ayudante1 <= 7)
                                {
                                    if (listaT[f - 1].Color.Equals("black") || listaT[f - 1].Color.Equals("white"))
                                    {
                                        ayudante1 = 8;
                                        izq = false;
                                    }
                                    else
                                    {
                                        if (f + aux3 > 63)
                                        {
                                            ayudante1 = 8;
                                            izq = false;
                                        }
                                        else
                                        {
                                            if (listaT[f + aux3].Color.Equals("white"))
                                            {
                                                aux3 = aux3 + 1;
                                            }
                                            else if (listaT[f + aux3].Color.Equals("black"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f - 1, "gray");
                                                listaT[f - 1] = fichaPosible;
                                                posibles.Add(f - 1);
                                                ayudante1 = 8;
                                            }
                                            else
                                            {
                                                ayudante1 = 8;
                                                ab = false;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        //Revision Derecha
                        if (f == 0 || f == 8 || f == 16 || f == 24 || f == 32 || f == 40 || f == 48 || f == 56)
                        {
                            der = false;
                        }
                        else
                        {
                            int ayudante2 = f, contador2 = 0;
                            while (ayudante2 > 7)
                            {
                                ayudante2 = ayudante2 - 8;
                                contador2++;
                            }
                            if ((ayudante2 + 1) > 7)
                            {
                                der = false;
                            }
                            else
                            {
                                while (ayudante2 >= 0)
                                {
                                    if (listaT[f + 1].Color.Equals("black") || listaT[f + 1].Color.Equals("white"))
                                    {
                                        ayudante2 = -1;
                                        der = false;
                                    }
                                    else
                                    {
                                        if (f - aux4 < 0)
                                        {
                                            ayudante2 = -1;
                                            der = false;
                                        }
                                        else
                                        {
                                            if (listaT[f - aux4].Color.Equals("white"))
                                            {
                                                aux4 = aux4 + 1;
                                            }
                                            else if (listaT[f - aux4].Color.Equals("black"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f + 1, "gray");
                                                listaT[f + 1] = fichaPosible;
                                                posibles.Add(f + 1);
                                                ayudante2 = -1;
                                            }
                                            else
                                            {
                                                ayudante2 = -1;
                                                der = false;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        //Revision Superior Izquierda
                        if ((f-9) < 0)
                        {
                            supizq = false;
                        }
                        else
                        {
                            if ((f) == 8 || (f) == 16 || (f) == 24 || (f) == 32 || (f) == 40 || (f) == 48 || (f) == 56)
                            {
                                supizq = false;
                            }
                            else
                            {
                                while (aux5 >= 0)
                                {
                                    if (listaT[f - 9].Color.Equals("black") || listaT[f - 9].Color.Equals("white"))
                                    {
                                        supizq = false;
                                        aux5 = -1;
                                    }
                                    else
                                    {
                                        if (f + aux5 > 63)
                                        {
                                            supizq = false;
                                            aux5 = -1;
                                        }
                                        else
                                        {
                                            if (listaT[f + aux5].Color.Equals("white"))
                                            {
                                                aux5 = aux5 + 9;
                                            }
                                            else if (listaT[f + aux5].Color.Equals("black"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f - 9, "gray");
                                                listaT[f - 9] = fichaPosible;
                                                posibles.Add(f - 9);
                                                aux5 = -1;
                                            }
                                            else
                                            {
                                                aux5 = -1;
                                                supizq = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Superior Derecha
                        if ((f - 7) <= 0)
                        {
                            supder = false;
                        }
                        else
                        {
                            if ((f) == 15 || (f) == 23 || (f) == 31 || (f) == 39 || (f) == 47 || (f) == 55|| f == 63)
                            {
                                supder = false;
                            }
                            else
                            {
                                while (aux6 >= 0)
                                {
                                    if (listaT[f - 7].Color.Equals("black") || listaT[f - 7].Color.Equals("white"))
                                    {
                                        supder = false;
                                        aux6 = -1;
                                    }
                                    else
                                    {
                                        if (f + aux6 > 63)
                                        {
                                            supder = false;
                                            aux6 = -1;
                                        }
                                        else
                                        {
                                            if (listaT[f + aux6].Color.Equals("white"))
                                            {
                                                aux6 = aux6 + 7;
                                            }
                                            else if (listaT[f + aux6].Color.Equals("black"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f - 7, "gray");
                                                listaT[f - 7] = fichaPosible;
                                                posibles.Add(f - 7);
                                                aux6 = -1;
                                            }
                                            else
                                            {
                                                aux6 = -1;
                                                supder = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Inferior Izquierda
                        if (f >= 56)
                        {
                            supder = false;
                        }
                        else
                        {
                            if ((f + 7) == 7 || (f + 7) == 15 || (f + 7) == 23 || (f + 7) == 31 || (f + 7) == 39 || (f + 7) == 47 || (f + 7) == 55)
                            {
                                infeizq = false;
                            }
                            else
                            {
                                while (aux7 < 64)
                                {
                                    if (listaT[f + 7].Color.Equals("black") || listaT[f + 7].Color.Equals("white"))
                                    {
                                        infeizq = false;
                                        aux7 = 64;
                                    }
                                    else
                                    {
                                        if (f - aux7 < 0)
                                        {
                                            infeizq = false;
                                            aux7 = 64;
                                        }
                                        else
                                        {
                                            if (listaT[f - aux7].Color.Equals("white"))
                                            {
                                                aux7 = aux7 + 7;
                                            }
                                            else if (listaT[f - aux7].Color.Equals("black"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f + 7, "gray");
                                                listaT[f + 7] = fichaPosible;
                                                posibles.Add(f + 7);
                                                aux7 = 64;
                                            }
                                            else
                                            {
                                                aux7 = 64;
                                                infeizq = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Inferior Derecha
                        if (f >= 56)
                        {
                            supder = false;
                        }
                        else
                        {
                            if ((f) == 7 || (f) == 15 || (f) == 23 || (f) == 31 || (f) == 39 || (f) == 47 || (f) == 55)
                            {
                                infeder = false;
                            }
                            else
                            {
                                while (aux8 < 64)
                                {
                                    if (listaT[f + 9].Color.Equals("black") || listaT[f + 9].Color.Equals("white"))
                                    {
                                        infeder = false;
                                        aux8 = 64;
                                    }
                                    else
                                    {
                                        if (f - aux8 < 0)
                                        {
                                            infeder = false;
                                            aux8 = 64;
                                        }
                                        else
                                        {
                                            if (listaT[f - aux8].Color.Equals("white"))
                                            {
                                                aux8 = aux8 + 9;
                                            }
                                            else if (listaT[f - aux8].Color.Equals("black"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f + 9, "gray");
                                                listaT[f + 9] = fichaPosible;
                                                posibles.Add(f + 9);
                                                aux8 = 64;
                                            }
                                            else
                                            {
                                                aux8 = 64;
                                                infeder = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                TempData["Color"] = "black";
            }
            else
            {
                for (int f = 0; f < listaT.Count(); f++)
                {
                    bool arr = true, ab = true, der = true, izq = true, supizq = true, supder = true, infeizq = true, infeder = true;
                    int aux1 = 8;
                    int aux2 = 8;
                    int aux3 = 1;
                    int aux4 = 1;
                    int aux5 = 9;
                    int aux6 = 7;
                    int aux7 = 7;
                    int aux8 = 9;
                    if (listaT[f].Color.Equals("black"))
                    {
                        //Revision Superior
                        if ((f - 8) < 0) //Revisa que no sea la primera fila
                        {
                            arr = false;
                        }
                        else
                        {
                            while (aux1 < 64)
                            {
                                if (listaT[f - 8].Color.Equals("black") || listaT[f - 8].Color.Equals("white"))
                                {
                                    aux1 = 64;
                                    arr = false;
                                }
                                else
                                {
                                    if (f + aux1 > 63)
                                    {
                                        arr = false;
                                        aux1 = 64;
                                    }
                                    else
                                    {
                                        if (listaT[f + aux1].Color.Equals("black"))
                                        {
                                            aux1 = aux1 + 8;
                                        }
                                        else if (listaT[f + aux1].Color.Equals("white"))
                                        {
                                            PartidaIndividual fichaPosible = new PartidaIndividual(f - 8, "gray");
                                            listaT[f - 8] = fichaPosible;
                                            posibles.Add(f - 8);
                                            aux1 = 64;
                                        }
                                        else
                                        {
                                            aux1 = 64;
                                            arr = false;
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Inferior
                        if ((f + 8) > 63) //Revisa que no sea la ultima fila
                        {
                            ab = false;
                        }
                        else
                        {
                            while (aux2 <= 63)
                            {
                                if (listaT[f + 8].Color.Equals("black") || listaT[f + 8].Color.Equals("white"))
                                {
                                    aux2 = 64;
                                    ab = false;
                                }
                                else
                                {
                                    if (f - aux2 < 0)
                                    {
                                        aux2 = 64;
                                        ab = false;
                                    }
                                    else
                                    {
                                        if (listaT[f - aux2].Color.Equals("black"))
                                        {
                                            aux2 = aux2 + 8;
                                        }
                                        else if (listaT[f - aux2].Color.Equals("white"))
                                        {
                                            PartidaIndividual fichaPosible = new PartidaIndividual(f + 8, "gray");
                                            listaT[f + 8] = fichaPosible;
                                            posibles.Add(f + 8);
                                            aux2 = 64;
                                        }
                                        else
                                        {
                                            aux2 = 64;
                                            ab = false;
                                        }
                                    }
                                }
                            }

                        }
                        //Revision Izquierda
                        int ayudante1 = f, contador1 = 0;
                        while (ayudante1 > 7)
                        {
                            ayudante1 = ayudante1 - 8;
                            contador1++;
                        }
                        if ((ayudante1 - 1) < 0)
                        {
                            izq = false;
                        }
                        else
                        {
                            while (ayudante1 <= 7)
                            {
                                if (listaT[f - 1].Color.Equals("black") || listaT[f - 1].Color.Equals("white"))
                                {
                                    ayudante1 = 8;
                                    izq = false;
                                }
                                else
                                {
                                    if (f + aux3 > 63)
                                    {
                                        ayudante1 = 8;
                                        izq = false;
                                    }
                                    else
                                    {
                                        if (listaT[f + aux3].Color.Equals("black"))
                                        {
                                            aux3 = aux3 + 1;
                                        }
                                        else if (listaT[f + aux3].Color.Equals("white"))
                                        {
                                            PartidaIndividual fichaPosible = new PartidaIndividual(f - 1, "gray");
                                            listaT[f - 1] = fichaPosible;
                                            posibles.Add(f - 1);
                                            ayudante1 = 8;
                                        }
                                        else
                                        {
                                            ayudante1 = 8;
                                            ab = false;
                                        }
                                    }
                                }
                            }

                        }
                        //Revision Derecha
                        int ayudante2 = f, contador2 = 0;
                        while (ayudante2 > 7)
                        {
                            ayudante2 = ayudante2 - 8;
                            contador2++;
                        }
                        if ((ayudante2 + 1) > 7)
                        {
                            der = false;
                        }
                        else
                        {
                            while (ayudante2 >= 0)
                            {
                                if (listaT[f + 1].Color.Equals("black") || listaT[f + 1].Color.Equals("white"))
                                {
                                    ayudante2 = -1;
                                    der = false;
                                }
                                else
                                {
                                    if (f - aux4 < 0)
                                    {
                                        ayudante2 = -1;
                                        der = false;
                                    }
                                    else
                                    {
                                        if (listaT[f - aux4].Color.Equals("black"))
                                        {
                                            aux4 = aux4 + 1;
                                        }
                                        else if (listaT[f - aux4].Color.Equals("white"))
                                        {
                                            PartidaIndividual fichaPosible = new PartidaIndividual(f + 1, "gray");
                                            listaT[f + 1] = fichaPosible;
                                            posibles.Add(f + 1);
                                            ayudante2 = -1;
                                        }
                                        else
                                        {
                                            ayudante2 = -1;
                                            der = false;
                                        }
                                    }
                                }
                            }

                        }
                        //Revision Superior Izquierda
                        if ((f - 9) < 0)
                        {
                            supizq = false;
                        }
                        else
                        {
                            if ((f) == 8 || (f) == 16 || (f) == 24 || (f) == 32 || (f) == 40 || (f) == 48 || (f) == 56)
                            {
                                supizq = false;
                            }
                            else
                            {
                                while (aux5 >= 0)
                                {
                                    if (listaT[f - 9].Color.Equals("black") || listaT[f - 9].Color.Equals("white"))
                                    {
                                        supizq = false;
                                        aux5 = -1;
                                    }
                                    else
                                    {
                                        if (f + aux5 > 63)
                                        {
                                            supizq = false;
                                            aux5 = -1;
                                        }
                                        else
                                        {
                                            if (listaT[f + aux5].Color.Equals("black"))
                                            {
                                                aux5 = aux5 + 9;
                                            }
                                            else if (listaT[f + aux5].Color.Equals("white"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f - 9, "gray");
                                                listaT[f - 9] = fichaPosible;
                                                posibles.Add(f - 9);
                                                aux5 = -1;
                                            }
                                            else
                                            {
                                                aux5 = -1;
                                                supizq = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Superior Derecha
                        if ((f - 7) <= 0)
                        {
                            supder = false;
                        }
                        else
                        {
                            if ((f) == 15 || (f) == 23 || (f) == 31 || (f - 7) == 39 || (f - 7) == 47 || (f - 7) == 55 || f+aux6>63)
                            {
                                supder = false;
                            }
                            else
                            {
                                while (aux6 >= 0)
                                {
                                    if (listaT[f - 7].Color.Equals("black") || listaT[f - 7].Color.Equals("white"))
                                    {
                                        supder = false;
                                        aux6 = -1;
                                    }
                                    else
                                    {
                                        if (f + aux6 > 63)
                                        {
                                            supder = false;
                                            aux6 = -1;

                                        }
                                        else
                                        {
                                            if (listaT[f + aux6].Color.Equals("black"))
                                            {
                                                aux6 = aux6 + 7;
                                            }
                                            else if (listaT[f + aux6].Color.Equals("white"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f - 7, "gray");
                                                listaT[f - 7] = fichaPosible;
                                                posibles.Add(f - 7);
                                                aux6 = -1;
                                            }
                                            else
                                            {
                                                aux6 = -1;
                                                supder = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Inferior Izquierda
                        if (f >= 56)
                        {
                            supder = false;
                        }
                        else
                        {
                            if ((f + 7) == 7 || (f + 7) == 15 || (f + 7) == 23 || (f + 7) == 31 || (f + 7) == 39 || (f + 7) == 47 || (f + 7) == 55)
                            {
                                infeizq = false;
                            }
                            else
                            {
                                while (aux7 < 64)
                                {
                                    if (listaT[f + 7].Color.Equals("black") || listaT[f + 7].Color.Equals("white"))
                                    {
                                        infeizq = false;
                                        aux7 = 64;
                                    }
                                    else
                                    {
                                        if (f - aux7 < 0)
                                        {
                                            infeizq = false;
                                            aux7 = 64;
                                        }
                                        else
                                        {
                                            if (listaT[f - aux7].Color.Equals("black"))
                                            {
                                                aux7 = aux7 + 7;
                                            }
                                            else if (listaT[f - aux7].Color.Equals("white"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f + 7, "gray");
                                                listaT[f + 7] = fichaPosible;
                                                posibles.Add(f + 7);
                                                aux7 = 64;
                                            }
                                            else
                                            {
                                                aux7 = 64;
                                                infeizq = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //Revision Inferior Derecha
                        if (f >= 56)
                        {
                            supder = false;
                        }
                        else
                        {
                            if ((f) == 7 || (f) == 15 || (f) == 23 || (f) == 31 || (f) == 39 || (f) == 47 || (f) == 55)
                            {
                                infeder = false;
                            }
                            else
                            {
                                while (aux8 < 64)
                                {
                                    if (listaT[f + 9].Color.Equals("black") || listaT[f + 9].Color.Equals("white"))
                                    {
                                        infeder = false;
                                        aux8 = 64;
                                    }
                                    else
                                    {
                                        if (f - aux8 < 0)
                                        {
                                            infeder = false;
                                            aux8 = 64;
                                        }
                                        else
                                        {
                                            if (listaT[f - aux8].Color.Equals("black"))
                                            {
                                                aux8 = aux8 + 9;
                                            }
                                            else if (listaT[f - aux8].Color.Equals("white"))
                                            {
                                                PartidaIndividual fichaPosible = new PartidaIndividual(f + 9, "gray");
                                                listaT[f + 9] = fichaPosible;
                                                posibles.Add(f + 9);
                                                aux8 = 64;
                                            }
                                            else
                                            {
                                                aux8 = 64;
                                                infeder = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (arr == true || ab == true || der == true || izq == true || supizq == true || supder == true || infeizq == true || infeder == true)
                        {
                            TempData["Color"] = "white";
                        }
                    }
                    

                }
                TempData["Color"] = "white";
            }
        }

        public void contaPunteo()
        {

        }
    }
}
