using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Semillero_ProgramacioFinal.Models.ViewModels
{
    public class PrivilegioViewModels
    {
        public int idprivilegio { get; set; }
        public int? fkrol { get; set; }
        public int? fkmenu { get; set; }

    }
}