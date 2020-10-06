using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Repetido.Models.Modelos
{
    public class RegistrarViewModel
    {
        [Required]
        public string nombre { get; set; }
        public string Apellido { get; set; }
        public string Usuario { get; set; }
        public string contaseña { get; set; }
        public string recontaseña { get; set; }
        public string fecha_nacimiento { get; set; }
        public string Correo_electronico { get; set; }
        public int codigo_pais { get; set; }
    }
}