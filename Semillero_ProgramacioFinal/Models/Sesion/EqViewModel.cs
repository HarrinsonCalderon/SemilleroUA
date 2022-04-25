using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class EqViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(4)]
        [Display(Name = "Nombre equipo")]
        public string nombre { get; set; }
       
      
 
        public string usuario { get; set; }
        public string contrasena { get; set; }

        public int? fkidcompetencia { get; set; }
        public int? fkidnivel { get; set; }
        public int? fkidestado { get; set; }
    }
}