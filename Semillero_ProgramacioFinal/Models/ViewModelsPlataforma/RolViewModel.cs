using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.SqlTypes;


namespace Semillero_ProgramacioFinal.Models.ViewModelsPlataforma
{
    public class RolViewModel
    {
        int idrol { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "el {0} debe tener almenos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Nombre")]
        string nombre { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "el {0} debe tener almenos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Nombre")]
        string nombre2 { get; set; }
        [Display(Name = "Estado")]
        int fkestadorol { get; set; }
        
    }
}