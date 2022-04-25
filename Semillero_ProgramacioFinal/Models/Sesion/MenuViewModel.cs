using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.Sesion
{
    public class MenuViewModel
    {
        
        public int? id { get; set; }
        public int? fkmenu { get; set; }
        public string nombre { get; set; }

        public string ruta { get; set; }


    }
}