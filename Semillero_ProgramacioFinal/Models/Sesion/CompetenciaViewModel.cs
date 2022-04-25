using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class CompetenciaViewModel
    {
        public int idcompetencia { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }
       // [Required]
       // [Display(Name = "Cupos")]
        public int? cupos { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha competencia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fechacompetencia { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Hora inicio")]
       // [DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.TimeSpan> horainicio { get; set; }
        [Required]
        [DataType(DataType.Time)]
        [Display(Name = "Hora final")]
        //[DisplayFormat(DataFormatString = "{0:hh:mm tt}", ApplyFormatInEditMode = true)]
        public Nullable<System.TimeSpan> horafin { get; set; }
        [Required]
        [Display(Name = "Cantidad ejercicios")]
        public int? cantidadejercicios { get; set; }
        [Display(Name = "Numero participantes")]
        [Required]
        public int? numeroparticipantes { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha limite inscripcion")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public Nullable<System.DateTime> limiteinscripcion { get; set; }
        public int? fkestado { get; set; }

 
      
    }
    
}