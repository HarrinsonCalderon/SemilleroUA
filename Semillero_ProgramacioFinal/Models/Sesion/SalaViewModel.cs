using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class SalaViewModel
    {
        public int idsala { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(4)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Range(0,1000)]
        [Display(Name = "Cupos")]
        public int? cupos { get; set; }

        public int? fkestado { get; set; }
    }
}