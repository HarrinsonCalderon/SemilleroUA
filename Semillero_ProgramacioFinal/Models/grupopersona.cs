//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Semillero_ProgramacioFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class grupopersona
    {
        public int idgrupopersona { get; set; }
        public Nullable<int> puesto { get; set; }
        public Nullable<int> fkidpersona { get; set; }
        public Nullable<int> fkidgrupo { get; set; }
    
        public virtual grupo grupo { get; set; }
        public virtual persona persona { get; set; }
    }
}
