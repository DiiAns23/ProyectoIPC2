using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repetido.Models.Modelos
{
    public class PaisViewModel
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string abrev { get; set; }
    }
}