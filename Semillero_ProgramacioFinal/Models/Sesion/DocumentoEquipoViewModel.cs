using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class DocumentoEquipoViewModel
    {
        public int fkidcompetencia { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Documento primer participante")]

        public int documento1 { get; set; }


        [Required]
  
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Documento segundo participante")]

        public int documento2 { get; set; }
    }
}