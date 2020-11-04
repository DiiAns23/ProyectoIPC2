using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Repetido.Models.Modelos
{
    public class PerfilUsuario
    {
        public int id_Jugador { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string usuario { get; set; }
        public string correo { get; set; }
        public string pais { get; set; }
        public string partidas { get; set; }
        public string tparticipacion { get; set; }
        public string tganados { get; set; }
        public string ptorneos { get; set; }
    }
}