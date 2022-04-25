using Semillero_ProgramacioFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Semillero_ProgramacioFinal.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeUser : AuthorizeAttribute
    {
        private usuario oUsuario;
        private plataformaEntities db = new plataformaEntities();
        private int idOperacion;




        public AuthorizeUser(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            try
            {
                oUsuario = (usuario)HttpContext.Current.Session["User"];

                var lstMisOperaciones = from p in db.privilegio
                                        where p.fkrol == oUsuario.fkrol
                                            && p.fkmenu == idOperacion
                                                && p.fkestadoprivilegio==1
                                        select p;


                if (lstMisOperaciones.ToList().Count() == 0)
                {
                    filterContext.Result = new RedirectResult("~/Sesion/SesionAdmin");
                }
            }
            catch (Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Sesion/SesionAdmin");
            }
        }

    }
}