using Semillero_ProgramacioFinal.Models;
using Semillero_ProgramacioFinal.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace Semillero_ProgramacioFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            datosBasicos();
            datosPlataforma();
            return View();
        }
        public ActionResult QuienesSomos()
        {
            datosBasicos();
            datosPlataforma();
            return View();
        }
        public ActionResult Login()
        {
            datosBasicos();
            datosPlataforma();
            return View();
        }
        public ActionResult Registro()
        {
            datosPlataforma();
            datosBasicos();
            return View();
        }
        public ActionResult Actividades()
        {
            datosBasicos();
            datosPlataforma();
            return View();
        }
        public ActionResult Trayectoria()
        {
            datosBasicos();
            datosPlataforma();
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            datosBasicos();
            try
            {
                string cmp, cmp2;
                using (plataformaEntities bd = new plataformaEntities())
                {
                     cmp = Models.Encriptacion.Encrypt.GetSHA256(password);
                    var oUser = (from d in bd.usuario
                                 where d.correoelectronico == email.Trim() && d.contrasena == cmp
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {

                        ViewBag.Error = "Datos incorrectos";
                        return View();
                       
                    }
                    else {
                        cmp2 = Models.Encriptacion.Encrypt.GetSHA256(password);
                       var  persona = (from d2 in bd.persona
                                     where d2.idpersona == oUser.fkidepersona 
                                     select d2).FirstOrDefault();
                        var Rol = (from d3 in bd.rol
                                       where d3.idrol == oUser.fkrol  
                                       select d3).FirstOrDefault();
                        Session["User"] = oUser;
                        Session["Persona"] = persona.primernombre+" "+persona.primerapellido;
                        Session["Rol"] = Rol.nombre;
                        Session["Fkrol"] = oUser.fkrol;
                        Session["identificacion"] = persona.identificacion;
                        Session["idusuario"] = oUser.idusuario;
                        
                        
                    }

                }
                cmp2 = "";
                cmp = "";
                password = "";
                return RedirectToAction("SesionAdmin", "Sesion");

            }
            catch (Exception ex)
            {
               
                ViewBag.Error = "Datos incorrectos";
                return View();
            }



        }


        [HttpGet]
        public ActionResult Add()
        {

            datosBasicos();
            datosPlataforma();
            return View();

        }

        [HttpPost]
        public ActionResult Add(UsuarioViewModel model)
        {
            datosBasicos();
            datosPlataforma();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new plataformaEntities())
            {

                persona persona = new persona();
                usuario oUser = new usuario();
                ////oUser.idPersona = 111222;
                ////oUser.email = "ppp@gmail.com";
                ////oUser.idRol = 1;
                ////oUser.idEstado = 1;
                ////oUser.passwordd = "12345";
                ////persona.identificacion = 111222;
                ////persona.idAcademia =1;
                ////persona.idTipodocumento =1;
                ////persona.primernombre = "ppp";
                ////persona.segundonombre = "ppp";
                ////persona.primerapellido = "ppp";
                ////persona.segundoapellido = "ppp";
                ////persona.telefono = "312121";
                int tipoDocumento = int.Parse(Request.Form["tipoDocumento"]);
                int academia = int.Parse(Request.Form["academia"]);

                //oUser.fkidpersona = model.Identificacion;
                
                oUser.correoelectronico = model.Email;
                oUser.fkrol = 2;
                oUser.fkestado = 1;
                oUser.contrasena = Models.Encriptacion.Encrypt.GetSHA256(model.Password);
                persona.identificacion = model.Identificacion;
                persona.fkacademia = academia;
                persona.fktipodocumento =tipoDocumento;
                persona.primernombre = model.PrimerNombre;
                persona.segundonombre = model.SegundoNombre;
                persona.primerapellido = model.PrimerApellido;
                persona.segundoapellido = model.SegundoApellido;
                persona.telefono = model.Telefono + "";
                oUser.fkidepersona = persona.idpersona;
                db.usuario.Add(oUser);
                db.persona.Add(persona);
                db.SaveChanges();
            }
            return Redirect(Url.Content("~/Home/Login"));

        }
        //---------------------------------------------Footer
        public ActionResult MostrarPlataforma()
        {

            PlataformaViewModel plataforma = new PlataformaViewModel();

            using (plataformaEntities db = new plataformaEntities())
            {
                var aux = db.plataforma.Find(1);
                plataforma.nombre = aux.nombre;
            }
            @ViewBag.nombrePlataforma = plataforma.nombre;
            return View(plataforma);

        }
        public void datosPlataforma()
        {
            var plataforma = new Models.plataforma();
            using (plataformaEntities bd=new plataformaEntities())
            {
                
                plataforma = bd.plataforma.Find(1);

            }
            ViewBag.nombrePlataforma = plataforma.nombre;
            ViewBag.telefonoPlataforma = plataforma.telefono;
            ViewBag.ubicacionPlataforma = plataforma.ubicacion;
            ViewBag.descripcionPlataforma = plataforma.descripcion;
            ViewBag.correoPlataforma = plataforma.correo;
            Session["plataforma"] = plataforma.nombre;
        }
        public void datosBasicos()
        {
            List<Models.ViewModels.basica> documento = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                documento = (from d in bd.tipodocumento
                             select new basica
                             {
                                 id = d.idtipodocumento,
                                 nombre = d.nombre
                             }).ToList();
            }
            List<Models.ViewModels.basica> academia = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                academia = (from d in bd.academia
                            select new basica
                            {
                                id = d.idacademia,
                                nombre = d.nombre
                            }).ToList();
            }
            ViewBag.documento = documento;
            ViewBag.academia = academia;
        }
    }
}