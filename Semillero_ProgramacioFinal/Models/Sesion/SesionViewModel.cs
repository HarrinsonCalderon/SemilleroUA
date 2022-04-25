using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class SesionViewModel
    {
        public int idusuario { set; get; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Correo")]
        [EmailAddress]
        public string correo { get; set; }
        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }
        [DataType(DataType.Password)]
        [StringLength(50)]
        [Display(Name = "Contraseña nueva")]
        public string contrasenanueva { get; set; }

        public int? fkidpersona { set; get; }

        public int? fkrol { set; get; }


        public int? fkestado { set; get; }
    }
}