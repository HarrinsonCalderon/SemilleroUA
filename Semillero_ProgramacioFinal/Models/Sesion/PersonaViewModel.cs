using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class PersonaViewModel
    {
        public int idpersona { get; set; }

        public int? identificacion { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(3)]
        [Display(Name = "Primer nombre")]
        public string primernombre { get; set; }
        
        [StringLength(50)]
        [MinLength(4)]
        [Display(Name = "Segundo nombre")]
        public string segundonombre { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(4)]
        [Display(Name = "Primer apellido")]
        public string primerapellido { get; set; }
        
        [StringLength(50)]
        [MinLength(4)]
        [Display(Name = "Segundo apellido")]
        public string segundoapellido { get; set; }
        public string correo { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Identificacion")]
        public int telefono { get; set; }

        public int? fktipodocumento { get; set; }

        public int? fkacademia { get; set; }

    }
}