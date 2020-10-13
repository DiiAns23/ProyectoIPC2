﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repetido.Models.Modelos
{
    public class PartidaIndividual
    {
        public int Index { get; set; }
        public string Color { get; set; }
        public string Jugador2 { get; set; }
        public string ColorJ2 { get; set; }
        public PartidaIndividual(int inde, string colorcito = "")
        {
            Index = inde;
            Color = colorcito;
        }
    }
}