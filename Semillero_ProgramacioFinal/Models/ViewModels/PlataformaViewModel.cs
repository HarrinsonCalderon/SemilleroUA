using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.ViewModels
{
    public class PlataformaViewModel
    {
        public   int id{get;set;}
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Teledono")]
        public string telefono { get; set; }
        [StringLength(50)]
        [MinLength(5)]
        [Display(Name = "Ubicacion")]
        public string ubicacion { get; set; }
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Descripcion")]
        public string descripcion { get; set; }
        [StringLength(100)]
        [MinLength(5)]
        [Display(Name = "Correo")]
        public string correo { get; set; }
    }
}
 