//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Repetido.Models.Modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Jugador
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jugador()
        {
            this.Bloqueo = new HashSet<Bloqueo>();
            this.Bloqueo1 = new HashSet<Bloqueo>();
            this.Partida = new HashSet<Partida>();
            this.Partida1 = new HashSet<Partida>();
        }
    
        public int Id_Jugador { get; set; }
        public string Nombres_Jugador { get; set; }
        public string Apellidos_Jugador { get; set; }
        public string Nombre_Usuario { get; set; }
        public int Id_Pais { get; set; }
        public string Correo { get; set; }
        public string Pass { get; set; }
        public string Confirm_Pass { get; set; }
        public Nullable<int> Id_Torneo { get; set; }
        public Nullable<int> Id_Partida { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bloqueo> Bloqueo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bloqueo> Bloqueo1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partida> Partida { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Partida> Partida1 { get; set; }
        public virtual Pais Pais { get; set; }
    }
}
