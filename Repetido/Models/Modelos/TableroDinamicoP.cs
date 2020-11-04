using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Repetido.Models.Modelos
{
    public class TableroDinamicoP
    {
        public int index { get; set; }
        public string Color { get; set; }
        public TableroDinamicoP(int indice, string colorf = "")
        {
            index = indice;
            Color = colorf;
        }
    }
}