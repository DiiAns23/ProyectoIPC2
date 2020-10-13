using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repetido.Models.Modelos;
using System.Xml.Linq;
using System.Xml;
using Repetido.Controllers;

namespace Repetido.Controllers
{
    [Authorize]
    public class LocalController : Controller
    {
        static List<PartidaIndividual> listaT = new List<PartidaIndividual>();
        static List<PartidaIndividual> listaC = new List<PartidaIndividual>();
        static List<int> posibles = new List<int>();
        static string J1 = "";
        static string C1 = "";
        static string J2 = "";
        static string C2 = "";
        static bool respuesta, fin = false;
        static int TirosNegros = 0;
        static int TirosBlancos = 0;
        
        public ActionResult Individual()
        {
            J1 = LoginController.user;
            J2 = "Zero Machine";
            TempData["J1"] = J1;
            C1 = "black";
            C2 = "white";
            TempData["C1"] = C1;
            TempData["C2"] = C2;
            TempData["title"] = "Individual";
            for (int i = 0; i < 64; i++)
            {
                PartidaIndividual test = new PartidaIndividual(i);
                listaT.Add(test);
                listaC.Add(test);
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
            TempData["TirosBlancos"] = 0;
            TempData["TirosNegros"] = 0;
            return View(listaT);
        }

        public ActionResult Change(int io)
        {
            int machine = 0;
            if (io != -1)
            {
                if (TempData["Color"].Equals("black"))
                {
                    PartidaIndividual fichaNueva = new PartidaIndividual(io, "black");
                    listaT[io] = fichaNueva;
                    TempData["Color"] = "white";
                }
                change2(io);
                movimientosPosibles(io);
                for(int x = 0; x<listaT.Count(); x++)
                {
                    if (listaT[x].Color.Equals("gray"))
                    {
                        machine = x;
                    }
                }
                
            }
            PartidaIndividual fichaW = new PartidaIndividual(machine, "white");
            listaT[machine] = fichaW;
            TempData["Color"] = "black"; 
            change2(machine);
            movimientosPosibles(machine);
            TempData["J1"] = J1;
            TempData["C1"] = C1;
            TempData["J2"] = J2;
            TempData["C2"] = C2;
            return View("Individual", listaT);
        }

        public ActionResult TerminarPartida()
        {
            int punteo = 0, movimientos = 0;
            if (TempData["C1"].Equals("black"))
            {
                punteo = (int)TempData["PunteoNegras"];
                movimientos = TirosNegros;
            }
            else
            {
                punteo = (int)TempData["PunteoBlancas"];
                movimientos = TirosBlancos;
            }
            int userId = LoginController.idUser;
            using (BaseDatos2 db = new BaseDatos2())
            {
                var partida = new Partida();
                partida.Tipo = "Solitario";
                partida.Id_Jugador1 = userId;
                partida.Punteo = punteo;
                partida.Movimiento = movimientos;
                if (TempData["Ganador"].Equals(J1))
                {
                    partida.Ganador = "Ganada";
                }
                else if (TempData["Ganador"].Equals(J2))
                {
                    partida.Ganador = "Perdida";
                }
                else if (TempData["Ganador"].Equals("Empate"))
                {
                    partida.Ganador = "Empatada";
                }
                db.Partida.Add(partida);
                db.SaveChanges();
            }
            listaT.Clear();
            return RedirectToAction("Jugar", "PantallaPrincipal");
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
                        if (listaT[io2].Color != listaT[aux].Color)
                        {
                            if (listaT[aux].Color.Equals("") || listaT[aux].Color.Equals("gray"))
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
                return true;
            }
        }

        public Boolean VerificarAbajo(int io2)
        {
            int aux2 = io2 + 8;
            List<int> abajo = new List<int>();
            //Revisar Abajo
            if (aux2 >= 64)
            {
                return false;
            }
            else
            {
                while (aux2 < 64)
                {
                    if (aux2 >= 64)
                    {
                        return false;
                    }
                    else
                    {
                        if (listaT[io2].Color != listaT[aux2].Color)
                        {
                            if (listaT[aux2].Color.Equals("") || listaT[aux2].Color.Equals("gray"))
                            {
                                return false;
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
                                return false;
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
                return true;
            }

        }

        public Boolean VerificarDerecha(int io2)
        {
            int aux3 = 1;
            List<int> derecha = new List<int>();
            while (aux3 < 7)
            {
                if (io2 + aux3 == 8 || io2 + aux3 == 16 || io2 + aux3 == 24 || io2 + aux3 == 32 || io2 + aux3 == 40 || io2 + aux3 == 48 || io2 + aux3 == 56 || io2 + aux3 == 64)
                {
                    if (derecha.Count() == 0)
                    {
                        return false;
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
                        return true;
                    }
                }
                else
                {
                    if (listaT[io2].Color != listaT[io2 + aux3].Color)
                    {
                        if (listaT[io2 + aux3].Color.Equals("") || listaT[io2 + aux3].Color.Equals("gray"))
                        {
                            return false;
                        }
                        else
                        {
                            if (io2 + aux3 != 8 && io2 + aux3 != 16 && io2 + aux3 != 24 && io2 + aux3 != 32 && io2 + aux3 != 40 && io2 + aux3 != 48 && io2 + aux3 != 56 && io2 + aux3 != 64)
                            {
                                derecha.Add(io2 + aux3);
                                aux3 = aux3 + 1;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (derecha.Count() == 0)
                        {
                            return false;
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
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        public Boolean VerificarIzquierda(int io2)
        {
            int aux3 = 1;
            List<int> izquierda = new List<int>();
            while (aux3 < 7)
            {
                if (io2 - aux3 < 0 || io2 - aux3 == 7 || io2 - aux3 == 15 || io2 - aux3 == 23 || io2 - aux3 == 31 || io2 - aux3 == 39 || io2 - aux3 == 47 || io2 - aux3 == 55)
                {
                    if (izquierda.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        for (int x = 0; x < izquierda.Count(); x++)
                        {
                            if (izquierda[x] != 0 && izquierda[x] != 7 && izquierda[x] != 56 && izquierda[x] != 63)
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
                        return true;
                    }
                }
                else
                {
                    if (listaT[io2].Color != listaT[io2 - aux3].Color)
                    {
                        if (listaT[io2 - aux3].Color.Equals("") || listaT[io2 - aux3].Color.Equals("gray"))
                        {
                            return false;
                        }
                        else
                        {
                            if (io2 - aux3 != 0 && io2 - aux3 != 8 && io2 - aux3 != 16 && io2 - aux3 != 24 && io2 - aux3 != 32 && io2 - aux3 != 40 && io2 - aux3 != 48 && io2 - aux3 != 56)
                            {
                                izquierda.Add(io2 - aux3);
                                aux3 = aux3 + 1;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (izquierda.Count() == 0)
                        {
                            return false;
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
                            return true;
                        }
                    }
                }
            }
            return true;

        }

        public Boolean VerificarIzquierdaSup(int io2)
        {
            List<int> superiorizquierda = new List<int>();
            int aux5 = 9;
            while (aux5 <= 56)
            {
                if (io2 - aux5 < 0 || io2 - aux5 == 7 || io2 - aux5 == 15 || io2 - aux5 == 23 || io2 - aux5 == 31 || io2 - aux5 == 39 || io2 - aux5 == 47 || io2 - aux5 == 55)
                {
                    if (superiorizquierda.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (listaT[io2 - aux5 + 9].Color == listaT[io2].Color)
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (listaT[io2].Color != listaT[io2 - aux5].Color)
                    {
                        if (listaT[io2 - aux5].Color.Equals("") || listaT[io2 - aux5].Color.Equals("gray"))
                        {
                            return false;
                        }
                        else
                        {
                            superiorizquierda.Add(io2 - aux5);
                            aux5 = aux5 + 9;
                        }
                    }
                    else
                    {
                        if (superiorizquierda.Count() == 0)
                        {
                            return false;
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
                            return true;
                        }

                    }

                }
            }
            return true;



        }

        public Boolean VerificarDerechaSup(int io2)
        {
            List<int> superiorderecha = new List<int>();
            int aux5 = 7;
            while (aux5 <= 56)
            {
                if (io2 - aux5 <= 0 || io2 - aux5 == 1 || io2 - aux5 == 8 || io2 - aux5 == 16 || io2 - aux5 == 24 || io2 - aux5 == 32 || io2 - aux5 == 40 || io2 - aux5 == 56)
                {
                    if (superiorderecha.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (listaT[io2 - aux5 + 7].Color == listaT[io2].Color)
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (listaT[io2].Color != listaT[io2 - aux5].Color)
                    {
                        if (listaT[io2 - aux5].Color.Equals("") || listaT[io2 - aux5].Color.Equals("gray"))
                        {
                            return false;
                        }
                        else
                        {
                            superiorderecha.Add(io2 - aux5);
                            aux5 = aux5 + 7;
                        }
                    }
                    else
                    {
                        if (superiorderecha.Count() == 0)
                        {
                            return false;
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
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        public Boolean VerificarIzquierdaInf(int io2)
        {
            List<int> inferiorizquierda = new List<int>();
            int aux5 = 7;
            while (aux5 <= 56)
            {
                if (io2 + aux5 == 7 || io2 + aux5 == 15 || io2 + aux5 == 23 || io2 + aux5 == 31 || io2 + aux5 == 39 || io2 + aux5 == 47 || io2 + aux5 == 55 || io2 + aux5 >= 56)
                {
                    if (inferiorizquierda.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (listaT[io2 + aux5].Color == listaT[io2].Color)
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
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (listaT[io2].Color != listaT[io2 + aux5].Color)
                    {
                        if (listaT[io2 + aux5].Color.Equals("") || listaT[io2 + aux5].Color.Equals("gray"))
                        {
                            return false;
                        }
                        else
                        {
                            inferiorizquierda.Add(io2 + aux5);
                            aux5 = aux5 + 7;
                        }
                    }
                    else
                    {
                        if (inferiorizquierda.Count() == 0)
                        {
                            return false;
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
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        public Boolean VerificarDerechaInf(int io2)
        {
            List<int> derechainferior = new List<int>();
            int aux5 = 9;
            while (aux5 <= 56)
            {
                if (io2 + aux5 == 16 || io2 + aux5 == 24 || io2 + aux5 == 32 || io2 + aux5 == 40 || io2 + aux5 == 48 || io2 + aux5 >= 56)
                {
                    if (derechainferior.Count() == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (listaT[io2 + aux5 - 9].Color == listaT[io2].Color)
                        {
                            for (int x = 0; x < derechainferior.Count(); x++)
                            {
                                if (TempData["Color"].Equals("white"))
                                {
                                    PartidaIndividual fichaNueva = new PartidaIndividual(derechainferior[x], "black");
                                    listaT[derechainferior[x]] = fichaNueva;
                                    TempData["Color"] = "white";
                                }
                                else if (TempData["Color"].Equals("black"))
                                {
                                    PartidaIndividual fichaNueva = new PartidaIndividual(derechainferior[x], "white");
                                    listaT[derechainferior[x]] = fichaNueva;
                                    TempData["Color"] = "black";
                                }
                            }
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (listaT[io2].Color != listaT[io2 + aux5].Color)
                    {
                        if (listaT[io2 + aux5].Color.Equals("") || listaT[io2 + aux5].Color.Equals("gray"))
                        {
                            return false;
                        }
                        else
                        {
                            derechainferior.Add(io2 + aux5);
                            aux5 = aux5 + 9;
                        }
                    }
                    else
                    {
                        if (derechainferior.Count() == 0)
                        {
                            return false;
                        }
                        else
                        {
                            for (int x = 0; x < derechainferior.Count(); x++)
                            {
                                if (TempData["Color"].Equals("white"))
                                {
                                    PartidaIndividual fichaNueva = new PartidaIndividual(derechainferior[x], "black");
                                    listaT[derechainferior[x]] = fichaNueva;
                                    TempData["Color"] = "white";
                                }
                                else if (TempData["Color"].Equals("black"))
                                {
                                    PartidaIndividual fichaNueva = new PartidaIndividual(derechainferior[x], "white");
                                    listaT[derechainferior[x]] = fichaNueva;
                                    TempData["Color"] = "black";
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            return true;
        }

        public void change2(int io2)
        {
            int numero = io2;
            bool ab = VerificarAbajo(numero);
            bool arr = VerificarArriba(numero);
            bool der = VerificarDerecha(numero);
            bool izq = VerificarIzquierda(numero);
            bool supizq = VerificarIzquierdaSup(numero);
            bool supder = VerificarDerechaSup(numero);
            bool infeizq = VerificarIzquierdaInf(numero);
            bool infeder = VerificarDerechaInf(numero);

            if (arr == true || ab == true || der == true || izq == true || supizq == true || supder == true || infeizq == true || infeder == true)
            {
                if (TempData["Color"].Equals("black"))
                {
                    TempData["Color"] = "black";
                    TirosBlancos++;
                    TempData["TirosBlancos"] = TirosBlancos;
                    TempData["TirosNegros"] = TirosNegros;

                }
                else
                {
                    TempData["Color"] = "white";
                    TirosNegros++;
                    TempData["TirosNegros"] = TirosNegros;
                    TempData["TirosBlancos"] = TirosBlancos;
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
                    TempData["TirosNegros"] = TirosNegros;
                }
                else
                {
                    TempData["Color"] = "black";
                    TempData["TirosBlancos"] = TirosBlancos;
                }
                respuesta = false;
            }
        }

        public Boolean PosibleArriba(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 8;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] <= 7 || listafichas[x] >= 56)
                {
                    continue;
                }
                else
                {
                    if (listaT[listafichas[x] - 8].Color != ("black") && listaT[listafichas[x] - 8].Color != ("white"))
                    {
                        while (listafichas[x] + aux1 < 56 && listaT[listafichas[x] + aux1].Color == listaT[listafichas[x]].Color)
                        {
                            aux1 = aux1 + 8;
                        }
                        if (listafichas[x] + aux1 < 63)
                        {
                            if (listaT[listafichas[x] + aux1].Color != listaT[listafichas[x]].Color)
                            {
                                if (listaT[listafichas[x] + aux1].Color != "" && listaT[listafichas[x] + aux1].Color != "gray")
                                {
                                    PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] - 8, "gray");
                                    listaT[listafichas[x] - 8] = fichaPosible;
                                    posibles.Add(listafichas[x] - 8);
                                    temporal = true;
                                }
                                aux1 = 8;
                            }
                        }
                    }
                    aux1 = 8;
                }
            }
            return temporal;
        }

        public Boolean PosibleAbajo(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 8;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] <= 7 || listafichas[x] >= 56)
                {
                    continue;
                }
                else
                {
                    if (listaT[listafichas[x] + 8].Color != ("black") && listaT[listafichas[x] + 8].Color != ("white"))
                    {
                        while (listafichas[x] - aux1 > 0 && listaT[listafichas[x] - aux1].Color == listaT[listafichas[x]].Color)
                        {
                            aux1 = aux1 + 8;
                        }
                        if (listafichas[x] - aux1 > 0)
                        {
                            if (listaT[listafichas[x] - aux1].Color != listaT[listafichas[x]].Color)
                            {
                                if (listaT[listafichas[x] - aux1].Color != "" && listaT[listafichas[x] - aux1].Color != "gray")
                                {
                                    PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] + 8, "gray");
                                    listaT[listafichas[x] + 8] = fichaPosible;
                                    posibles.Add(listafichas[x] + 8);
                                    temporal = true;
                                }
                                aux1 = 8;
                            }
                        }
                    }
                    aux1 = 8;
                }
            }
            return temporal;
        }

        public Boolean PosibleIzquierda(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 1;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] == 0 || listafichas[x] == 8 || listafichas[x] == 16 || listafichas[x] == 24 || listafichas[x] == 32 || listafichas[x] == 40 || listafichas[x] == 48 || listafichas[x] == 56
                  || listafichas[x] == 7 || listafichas[x] == 15 || listafichas[x] == 23 || listafichas[x] == 31 || listafichas[x] == 39 || listafichas[x] == 47 || listafichas[x] == 55 || listafichas[x] == 63)
                {
                    continue;
                }
                else
                {
                    if (listaT[listafichas[x] - 1].Color != ("black") && listaT[listafichas[x] - 1].Color != ("white"))
                    {
                        while (listaT[listafichas[x] + aux1].Color == listaT[listafichas[x]].Color && aux1 < 7)
                        {
                            aux1 = aux1 + 1;
                        }
                        if (listaT[listafichas[x] + aux1].Color != listaT[listafichas[x]].Color)
                        {
                            if ((listaT[listafichas[x] + aux1].Color != "") && listaT[listafichas[x] + aux1].Color != "gray")
                            {
                                PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] - 1, "gray");
                                listaT[listafichas[x] - 1] = fichaPosible;
                                posibles.Add(listafichas[x] - 1);
                                temporal = true;
                            }
                            aux1 = 1;
                        }
                    }
                }
            }
            return temporal;
        }

        public Boolean PosibleDerecha(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 1;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] <= 0 || listafichas[x] == 8 || listafichas[x] == 16 || listafichas[x] == 24 || listafichas[x] == 32 || listafichas[x] == 40 || listafichas[x] == 48 || listafichas[x] == 56
                  || listafichas[x] == 7 || listafichas[x] == 15 || listafichas[x] == 23 || listafichas[x] == 31 || listafichas[x] == 39 || listafichas[x] == 47 || listafichas[x] == 55 || listafichas[x] == 63)
                {
                    continue;
                }
                else
                {
                    if (listaT[listafichas[x] + 1].Color != ("black") && listaT[listafichas[x] + 1].Color != ("white"))
                    {
                        if (listafichas[x] - aux1 > 0)
                        {
                            while (listafichas[x] - aux1 > 0 && listaT[listafichas[x] - aux1].Color == listaT[listafichas[x]].Color)
                            {
                                aux1 = aux1 + 1;
                            }
                            if (listafichas[x] - aux1 >= 0)
                            {
                                if (listaT[listafichas[x] - aux1].Color != listaT[listafichas[x]].Color)
                                {
                                    if (listaT[listafichas[x] - aux1].Color != "" && listaT[listafichas[x] - aux1].Color != "gray")
                                    {
                                        PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] + 1, "gray");
                                        listaT[listafichas[x] + 1] = fichaPosible;
                                        posibles.Add(listafichas[x] + 1);
                                        temporal = true;
                                    }
                                    aux1 = 1;
                                }
                            }
                        }
                    }
                    aux1 = 1;
                }
            }
            return temporal;
        }

        public Boolean PosibleSuperiorIzquierda(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 9;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] <= 8 || listafichas[x] == 16 || listafichas[x] == 24 || listafichas[x] == 32 || listafichas[x] == 40 || listafichas[x] == 48 || listafichas[x] == 56 || listafichas[x] == 63
                    || listafichas[x] == 7 || listafichas[x] == 15 || listafichas[x] == 23 || listafichas[x] == 31 || listafichas[x] == 39 || listafichas[x] == 47 || listafichas[x] == 55)
                {
                    continue;
                }
                else
                {
                    if (listaT[listafichas[x] - 9].Color != ("black") && listaT[listafichas[x] - 9].Color != ("white"))
                    {
                        bool cerrarciclo = false;
                        while (listafichas[x] + aux1 < 63 && listaT[listafichas[x] + aux1].Color == listaT[listafichas[x]].Color && cerrarciclo == false)
                        {
                            if (listafichas[x] + aux1 == 7 || listafichas[x] + aux1 == 15 || listafichas[x] + aux1 == 23 || listafichas[x] + aux1 == 31 || listafichas[x] + aux1 == 39
                                || listafichas[x] + aux1 == 47 || listafichas[x] + aux1 == 55)
                            {
                                cerrarciclo = true;
                            }
                            else
                            {
                                aux1 = aux1 + 9;
                            }
                        }
                        if (listafichas[x] + aux1 < 63)
                        {
                            if (listaT[listafichas[x] + aux1].Color != listaT[listafichas[x]].Color)
                            {
                                if (listaT[listafichas[x] + aux1].Color != "" && listaT[listafichas[x] + aux1].Color != "gray")
                                {
                                    PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] - 9, "gray");
                                    listaT[listafichas[x] - 9] = fichaPosible;
                                    posibles.Add(listafichas[x] - 9);
                                    temporal = true;
                                }
                                aux1 = 9;
                            }
                        }
                    }
                }
            }
            return temporal;
        }

        public Boolean PosibleSuperiorDerecha(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 7;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] <= 8 || listafichas[x] == 16 || listafichas[x] == 24 || listafichas[x] == 32 || listafichas[x] == 40 || listafichas[x] == 48 || listafichas[x] == 56 || listafichas[x] == 63
                     || listafichas[x] == 7 || listafichas[x] == 15 || listafichas[x] == 23 || listafichas[x] == 31 || listafichas[x] == 39 || listafichas[x] == 47 || listafichas[x] == 55)
                {
                    continue;
                }
                else
                {
                    if (listaT[listafichas[x] - 7].Color != ("black") && listaT[listafichas[x] - 7].Color != ("white"))
                    {
                        bool cerrarciclo = false;
                        while (listafichas[x] + aux1 < 63 && listaT[listafichas[x] + aux1].Color == listaT[listafichas[x]].Color && cerrarciclo == false)
                        {
                            if (listafichas[x] + aux1 == 8 || listafichas[x] + aux1 == 16 || listafichas[x] + aux1 == 24 || listafichas[x] + aux1 == 32 || listafichas[x] + aux1 == 40
                                || listafichas[x] + aux1 == 48 || listafichas[x] + aux1 == 56 || listafichas[x] + aux1 == 63)
                            {
                                cerrarciclo = true;
                            }
                            else
                            {
                                aux1 = aux1 + 7;
                            }
                        }
                        if (listafichas[x] + aux1 < 63)
                        {
                            if (listaT[listafichas[x] + aux1].Color != listaT[listafichas[x]].Color)
                            {
                                if (listaT[listafichas[x] + aux1].Color != "" && listaT[listafichas[x] + aux1].Color != "gray")
                                {
                                    PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] - 7, "gray");
                                    listaT[listafichas[x] - 7] = fichaPosible;
                                    posibles.Add(listafichas[x] - 7);
                                    temporal = true;
                                }
                                aux1 = 7;
                            }
                        }
                    }
                }
            }
            return temporal;
        }

        public Boolean PosibleInferiorIzquierda(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 7;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] <= 8 || listafichas[x] == 16 || listafichas[x] == 24 || listafichas[x] == 32 || listafichas[x] == 40 || listafichas[x] == 48 || listafichas[x] >= 56 || listafichas[x] == 63
                    || listafichas[x] == 7 || listafichas[x] == 15 || listafichas[x] == 23 || listafichas[x] == 31 || listafichas[x] == 39 || listafichas[x] == 47 || listafichas[x] == 55)
                {
                    continue;
                }
                else
                {
                    if (listafichas[x] + 7 < 64)
                    {
                        if (listaT[listafichas[x] + 7].Color != ("black") && listaT[listafichas[x] + 7].Color != ("white"))
                        {
                            bool cerrarciclo = false;
                            while (listafichas[x] - aux1 > 0 && listaT[listafichas[x] - aux1].Color == listaT[listafichas[x]].Color && cerrarciclo == false)
                            {
                                if (listafichas[x] - aux1 == 7 || listafichas[x] - aux1 == 15 || listafichas[x] - aux1 == 23 || listafichas[x] - aux1 == 31 || listafichas[x] - aux1 == 39
                                || listafichas[x] - aux1 == 47 || listafichas[x] - aux1 == 55 || listafichas[x] - aux1 == 63)
                                {
                                    cerrarciclo = true;
                                }
                                else
                                {
                                    aux1 = aux1 + 7;
                                }
                            }
                            if (listafichas[x] - aux1 > 0)
                            {
                                if (listaT[listafichas[x] - aux1].Color != listaT[listafichas[x]].Color)
                                {
                                    if (listaT[listafichas[x] - aux1].Color != "" && listaT[listafichas[x] - aux1].Color != "gray")
                                    {
                                        PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] + 7, "gray");
                                        listaT[listafichas[x] + 7] = fichaPosible;
                                        posibles.Add(listafichas[x] + 7);
                                        temporal = true;
                                    }
                                    aux1 = 7;
                                }
                            }
                        }
                    }
                }
            }
            return temporal;
        }

        public Boolean PosibleInferiorDerecha(List<int> listafichas)
        {
            bool temporal = false;
            int aux1 = 9;
            for (int x = 0; x < listafichas.Count; x++)
            {
                if (listafichas[x] <= 8 || listafichas[x] == 16 || listafichas[x] == 24 || listafichas[x] == 32 || listafichas[x] == 40 || listafichas[x] == 48 || listafichas[x] >= 56 || listafichas[x] == 63
                    || listafichas[x] == 7 || listafichas[x] == 15 || listafichas[x] == 23 || listafichas[x] == 31 || listafichas[x] == 39 || listafichas[x] == 47 || listafichas[x] == 55)
                {
                    continue;
                }
                else
                {
                    if (listafichas[x] + 9 <= 63)
                    {
                        if (listaT[listafichas[x] + 9].Color != ("black") && listaT[listafichas[x] + 9].Color != ("white"))
                        {
                            bool cerrarciclo = false;
                            while (listafichas[x] - aux1 > 0 && listaT[listafichas[x] - aux1].Color == listaT[listafichas[x]].Color && cerrarciclo == false)
                            {
                                if (listafichas[x] - aux1 == 8 || listafichas[x] - aux1 == 16 || listafichas[x] - aux1 == 24 || listafichas[x] - aux1 == 32 || listafichas[x] - aux1 == 40
                                || listafichas[x] - aux1 == 48 || listafichas[x] - aux1 == 56)
                                {
                                    cerrarciclo = true;
                                }
                                else
                                {
                                    aux1 = aux1 + 9;
                                }
                            }
                            if (listafichas[x] - aux1 > 0)
                            {
                                if (listaT[listafichas[x] - aux1].Color != listaT[listafichas[x]].Color)
                                {
                                    if (listaT[listafichas[x] - aux1].Color != "" && listaT[listafichas[x] - aux1].Color != "gray")
                                    {
                                        PartidaIndividual fichaPosible = new PartidaIndividual(listafichas[x] + 9, "gray");
                                        listaT[listafichas[x] + 9] = fichaPosible;
                                        posibles.Add(listafichas[x] + 9);
                                        temporal = true;
                                    }
                                    aux1 = 9;
                                }
                            }
                        }
                    }
                }
            }
            return temporal;
        }

        public void movimientosPosibles(int io2)
        {
            bool movblancas = false, movnegras = false, salir = false;
            List<int> PosiblesNegras = new List<int>();
            List<int> PosiblesBlancas = new List<int>();
            for (int y = 0; y < posibles.Count(); y++)
            {
                if (posibles[y] != io2)
                {
                    PartidaIndividual vaciar = new PartidaIndividual(posibles[y], "");
                    listaT[posibles[y]] = vaciar;
                }
            }
            posibles.Clear();
            for (int q = 0; q < listaT.Count(); q++)
            {
                if (listaT[q].Color.Equals("black"))
                {
                    PosiblesNegras.Add(q);
                }
                else if (listaT[q].Color.Equals("white"))
                {
                    PosiblesBlancas.Add(q);
                }
            }
            while (salir == false)
            {
                if (TempData["Color"].Equals("black"))
                {
                    movblancas = MovimintosPosiblesBlancas(PosiblesBlancas);
                    if (movblancas == false)
                    {
                        TempData["Color"] = "white";
                    }
                    else
                    {
                        salir = true;
                        TempData["Fin"] = false;
                    }
                }
                if (TempData["Color"].Equals("white"))
                {
                    movnegras = MovimientosPosiblesNegras(PosiblesNegras);
                    if (movnegras == false)
                    {
                        TempData["Color"] = "black";
                    }
                    else
                    {
                        salir = true;
                        TempData["Fin"] = false;
                    }
                }
                if (movblancas == false && movblancas == false)
                {
                    salir = true;
                    fin = true;
                    TempData["Fin"] = true;
                }
            }
        }

        public Boolean MovimintosPosiblesBlancas(List<int> PosiblesBlancas)
        {
            bool negras = false;
            bool arr = PosibleArriba(PosiblesBlancas);
            bool ab = PosibleAbajo(PosiblesBlancas);
            bool izq = PosibleIzquierda(PosiblesBlancas);
            bool der = PosibleDerecha(PosiblesBlancas);
            bool supizq = PosibleSuperiorIzquierda(PosiblesBlancas);
            bool supder = PosibleSuperiorDerecha(PosiblesBlancas);
            bool infeizq = PosibleInferiorIzquierda(PosiblesBlancas);
            bool infeder = PosibleInferiorDerecha(PosiblesBlancas);
            if (arr == true || ab == true || der == true || izq == true || supizq == true || supder == true || infeizq == true || infeder == true)
            {
                negras = true;
                return negras;
            }
            else
            {
                return negras;
            }
        }

        public Boolean MovimientosPosiblesNegras(List<int> PosiblesNegras)
        {
            bool blancas = false;
            bool arr = PosibleArriba(PosiblesNegras);
            bool ab = PosibleAbajo(PosiblesNegras);
            bool izq = PosibleIzquierda(PosiblesNegras);
            bool der = PosibleDerecha(PosiblesNegras);
            bool supizq = PosibleSuperiorIzquierda(PosiblesNegras);
            bool supder = PosibleSuperiorDerecha(PosiblesNegras);
            bool infeizq = PosibleInferiorIzquierda(PosiblesNegras);
            bool infeder = PosibleInferiorDerecha(PosiblesNegras);
            if (arr == true || ab == true || der == true || izq == true || supizq == true || supder == true || infeizq == true || infeder == true)
            {
                blancas = true;
                return blancas;
            }
            else
            {
                return blancas;
            }
        }

        [HttpPost]
        public ActionResult Load()
        {
            XDocument load = new XDocument();
            XElement raiz = new XElement("tablero");
            string path = Server.MapPath("~/Partidas Descargadas/");
            load.Add(raiz);
            for (int x = 0; x < listaT.Count; x++)
            {
                int aux = x;
                string columna = "";
                int fila = 1;
                while (aux > 7)
                {
                    aux = aux - 8;
                    fila++;
                }
                if (aux == 0)
                {
                    columna = "A";
                }
                if (aux == 1)
                {
                    columna = "B";
                }
                if (aux == 2)
                {
                    columna = "C";
                }
                if (aux == 3)
                {
                    columna = "D";
                }
                if (aux == 4)
                {
                    columna = "E";
                }
                if (aux == 5)
                {
                    columna = "F";
                }
                if (aux == 6)
                {
                    columna = "G";
                }
                if (aux == 7)
                {
                    columna = "H";
                }
                if (listaT[x].Color != ("") && listaT[x].Color != ("gray"))
                {
                    XElement ficha = new XElement("ficha");
                    if (listaT[x].Color.Equals("black"))
                    {
                        ficha.Add(new XElement("color", "negro"));
                    }
                    else
                    {
                        ficha.Add(new XElement("color", "blanco"));
                    }
                    ficha.Add(new XElement("columna", columna));
                    ficha.Add(new XElement("fila", fila));
                    raiz.Add(ficha);
                }

            }
            XElement turno = new XElement("siguienteTiro");
            if (TempData["Color"].Equals("white"))
            {
                turno.Add(new XElement("color", "blanco"));
            }
            else
            {
                turno.Add(new XElement("color", "negro"));
            }
            raiz.Add(turno);
            load.Save(path + J1 + ".xml");
            listaT.Clear();
            TempData["Color"] = "black";
            return RedirectToAction("Jugar", "PantallaPrincipal");
        }
    }
}







