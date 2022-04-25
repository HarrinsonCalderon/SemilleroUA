using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class CorreoMasivoViewModel
    {
        public int? id { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(4)]

        [Display(Name = "Asunto")]
        public string asunto { get; set; }
        [Required]
        [StringLength(8000)]
        [MinLength(4)]
        [Display(Name = "Mensaje")]
        public string mensaje { get; set; }
        
    }
}