using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;


namespace Repetido.Models.Modelos
{
    public class Cargar
    {
        public string Confirmacion { get; set; }
        public Exception error { get; set; }
        public string Color { get; set; }
        public string Columna { get; set; }
        public int Fila { get; set; }
        public void CargarArchivo(String ruta, HttpPostedFileBase file)
        {
            try
            {
                file.SaveAs(ruta);
                this.Confirmacion = "Archivo Guardado";
            }
            catch (Exception ex)
            {
                this.error = ex;
            }
        }
    }
}