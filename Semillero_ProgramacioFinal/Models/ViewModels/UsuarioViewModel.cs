using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Semillero_ProgramacioFinal.Models.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "el {0} debe tener almenos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las Contraseñas No Son Iguales")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name ="Identificacion")]
        public int Identificacion { get; set; }

        [Required]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }

     
        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public int Telefono { get; set; }

        [Required]
        [Display(Name = "Academia")]
        public int Academia { get; set; }

        [Required]
        [Display(Name = "Tipo Identidad")]
        public int TipoIdentidad { get; set; }

        public int idusuario { get; set; }
    }
    public class EditUsuarioViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "el {0} debe tener almenos {1} caracteres", MinimumLength = 1)]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "Las Contraseñas No Son Iguales")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Identificacion")]
        public int Identificacion { get; set; }

        [Required]
        [Display(Name = "Primer Nombre")]
        public string PrimerNombre { get; set; }


        [Display(Name = "Segundo Nombre")]
        public string SegundoNombre { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Required]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        public string Telefono { get; set; }

        [Required]
        [Display(Name = "Academia")]
        public int Academia { get; set; }

        [Required]
        [Display(Name = "Tipo Identidad")]
        public int TipoIdentidad { get; set; }


    }
}