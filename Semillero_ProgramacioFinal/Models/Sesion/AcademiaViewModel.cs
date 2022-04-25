using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class AcademiaViewModel
    {
        public int id { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(4)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
    }
}