using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class RolViewModels
    {
        public int? id { get; set; }
        public int? fkestado { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre rol")]
        public string nombre { get; set; }

    }
}