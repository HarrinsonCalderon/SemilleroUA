using Semillero_ProgramacioFinal.Controllers;
using Semillero_ProgramacioFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Semillero_ProgramacioFinal.Filters
{
    /// <summary>
    /// PARA VERIFICAR SI HAY UNA SESION Y ESO SE MANDA PARA EL FILTERCONFIG
    /// </summary>
    public class VerificaSesion : ActionFilterAttribute
    {
              //esto entra antes del controller
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //se evalua sesion
            var ouser = (usuario)HttpContext.Current.Session["User"];
            if (ouser == null)
            {
                //access controller no necesita tener sesion, si quitamos esto da bucle
                //Si el controlador es distinto a accesso controller, vaya a login porque no tiene sesion
                if (filterContext.Controller is HomeController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Login");
                }
            }
            else
            {
                //Quiero ir a login pero ya tengo sesion, simplemente voy a Home
                if (filterContext.Controller is HomeController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Sesion/SesionAdmin");
                }
            }
            //Esto es para que despues ejecute lo de la clase padre
            base.OnActionExecuting(filterContext);
        }



    }
    }
 