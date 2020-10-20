using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Repetido.Models.Modelos
{
    public class PersonalizadaModel
    {
        public int Index { get; set; }
        public string Color { get; set; }

        public PersonalizadaModel(int inde, string colorcito = "")
        {
            Index = inde;
            Color = colorcito;
        }
    }
}