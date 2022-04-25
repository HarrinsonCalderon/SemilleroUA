using Semillero_ProgramacioFinal.Filters;
using Semillero_ProgramacioFinal.Models;
using Semillero_ProgramacioFinal.Models.ViewModels;
using Semillero_ProgramacioFinal.Models.Sesion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Net.Mail;
using OfficeOpenXml;




namespace Semillero_ProgramacioFinal.Controllers
{
 
    public class SesionController : Controller
    {
         
        // GET: Sesion
        public ActionResult SesionAdmin()
        {
            menu();//En todos los Action que tengan vista
            obtenerDatosUsuario();
            return View( );
        }
        public ActionResult Salir()
        {
            menu();
            Session["User"] = null;
            Session["Persona"] = null;
            Session["User"] = null;
            Session["Persona"] =null;
            Session["Rol"] = null;
            Session["Fkrol"] =null;
            Session["identificacion"] = null;
            Session["idusuario"] = null;
            Session["idc"] = null;
            Session["msm"] = null;
            Session["fkc"] = null;
            Session["competencia"] = null;
            Session["d1"] = null;
            Session["d2"] = null;
            Session["com"] = null;
            Session["eq"] = null;
            return RedirectToAction("Login", "Home");
        }
        public void obtenerDatosUsuario()
        {
            ViewBag.fkrol = Session["Fkrol"];
            int? fkrol = ViewBag.fkrol;
            List<Models.ViewModels.PrivilegioViewModels> privilegio = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                privilegio = (from d in bd.privilegio
                              where d.fkrol == fkrol && d.fkestadoprivilegio == 1
                              select new PrivilegioViewModels
                              {
                                  idprivilegio = d.idprivilegio,
                                  fkrol = d.fkrol,
                                  fkmenu = d.fkmenu

                              }).ToList();
            }

            ViewBag.privilegio = privilegio;
            ViewBag.nombreApellido = Session["Persona"];
            ViewBag.rol = Session["Rol"];
            ViewBag.identificacion = Session["identificacion"];
            ViewBag.idu = Session["idusuario"];
            ViewBag.plataforma = Session["plataforma"];

        }
        //[AuthorizeUser(idOperacion: 3)]
        public void menu()
        {

            try
            {
                int idr = (int)Session["Fkrol"];
                List<Models.Sesion.MenuViewModel> l = null;
                using (plataformaEntities bd = new plataformaEntities())
                {
                    l = (from m in bd.menu
                         join p in bd.privilegio on m.idmenu equals p.fkmenu
                         join r in bd.rol on p.fkrol equals r.idrol
                         where r.idrol==idr && p.fkestadoprivilegio==1
                         select new Models.Sesion.MenuViewModel
                         {
                             fkmenu=m.fkmenu,
                             id=m.idmenu,
                             nombre=m.nombre,
                             ruta=m.ruta   
                         }

                        ).ToList();
                   

                }

                ViewBag.menu = l;
            }
            catch (Exception ex)
            {

                ViewBag.error = ex;
            }

            
        }
        public ActionResult error404()
        {
            obtenerDatosUsuario();
            menu();
            return View();
        }
        ///////////////////////////// INICIO ROLES Y PERMISOS ////////////////////////////
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult rolesPermisos()
        {
            menu();
            obtenerDatosUsuario();
            List<Models.Sesion.RolViewModels> l = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.rol
                     select new Models.Sesion.RolViewModels
                     {
                         nombre=a.nombre,
                         id=a.idrol,
                         fkestado=a.fkestadorol
                     }

                    ).ToList();
            }
            ViewBag.rol = l;
            var msm = (string)Session["msm"];
            if (msm!="" && msm!=null)
            {
                ViewBag.error = msm;
                Session["msm"] = null;
            }
                return View();
        }
        [AuthorizeUser(idOperacion: 7)]
        [HttpGet]
        public ActionResult eliminarRol(int id)
        {
            menu();
            obtenerDatosUsuario();
            using (plataformaEntities bd=new plataformaEntities())
            {
               
             var  contador = (from u in bd.usuario
                               where u.fkrol==id
                               select u
                            ).FirstOrDefault();
                if (contador==null)
                {
                     
                var rol = bd.rol.Find(id);
                bd.rol.Remove(rol);
                bd.SaveChanges();
                }
                else
                {
                    Session["msm"] = "No se puede eliminar porque esta en uso";         
                }

             
            }


                return Redirect("~/Sesion/rolesPermisos");
        }


        //public ActionResult editarRol(int id)
        //{
        //    menu();
        //    obtenerDatosUsuario();
        //    RolViewModels model = new RolViewModels();
        //    List<Models.Sesion.MenuViewModel> l = null;
        //    int idr = (int)Session["Fkrol"];
        //    using (plataformaEntities bd = new plataformaEntities())
        //    {
        //        var rol = bd.rol.Find(id);
        //        model.nombre = rol.nombre;

        //        l = (from m in bd.menu
        //             join p in bd.privilegio on m.idmenu equals p.fkmenu
        //             join r in bd.rol on p.fkrol equals r.idrol
        //             where r.idrol == idr && p.fkestadoprivilegio == 1
        //             select new Models.Sesion.MenuViewModel
        //             {
        //                 fkmenu = m.fkmenu,
        //                 id = m.idmenu,
        //                 nombre = m.nombre,
        //                 ruta = m.ruta
        //             }

        //           ).ToList();

        //    }
        //    ViewBag.menu = l;
        //    return View(model);
        //}
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult editarRol(int id)
        {
            menu();
            obtenerDatosUsuario();
            RolViewModels model = new RolViewModels();
            using (plataformaEntities bd =new plataformaEntities())
            {
                var t = bd.rol.Find(id);
                model.fkestado = t.fkestadorol;
                model.nombre = t.nombre;
                model.id = t.idrol;
            }
                return View(model);
        }
        [AuthorizeUser(idOperacion: 7)]
        [HttpPost]
        public ActionResult editarRol(RolViewModels model)
        {
            menu();
            obtenerDatosUsuario();
            if (ModelState.IsValid)
            {
                using (plataformaEntities bd = new plataformaEntities())
                {
                    var tabla = bd.rol.Find(model.id);
                    tabla.nombre = model.nombre;
                    bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                     
                }

            }
            else
            {

                return View(model);
            }
            return Redirect("~/Sesion/rolesPermisos");
        }
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult agregarRol()
        {
            menu();
            obtenerDatosUsuario();
            return View();
        }
        [AuthorizeUser(idOperacion: 7)]
        [HttpPost]
        public ActionResult agregarRol(RolViewModels model)
        {
            menu();
            obtenerDatosUsuario();
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd=new plataformaEntities())
                    {
                        var tabla = new rol();
                        tabla.nombre = model.nombre;
                        tabla.fkestadorol = 1;
                        //tabla.fkestado = int.Parse(Request.Form["estadocompetencia"]);
                        bd.rol.Add(tabla);
                        bd.SaveChanges();
                    }

                    return Redirect("~/Sesion/rolesPermisos");
                }
                else
                {
                     return View(model);
                }
              
            }
            catch (Exception e)
            {
                ViewBag.error = e;
                return Redirect("~/Sesion/error404");
            }
           
        }
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult gestionarPrivilegios()
        {

            return View();
        }

        ///////////////////////////// FIN ROLES Y PERMISOS ////////////////////////////

        ///////////////////////////// INICION PLATAFORMA ////////////////////////////
        [AuthorizeUser(idOperacion: 8)]
        public ActionResult gestionarPlataforma()
        {
            obtenerDatosUsuario();
            menu();
            PlataformaViewModel model = new PlataformaViewModel();
            try
            {
               
                using (plataformaEntities bd = new plataformaEntities())
                {
                    var tabla = bd.plataforma.Find(1);
                    model.correo = tabla.correo;
                    model.descripcion = tabla.descripcion;
                    model.id = tabla.id;
                    model.nombre = tabla.nombre;
                    model.telefono = tabla.telefono;
                    model.ubicacion = tabla.ubicacion;

                }
            }
            catch (Exception e)
            {
                ViewBag.error = e.Message;
                return Redirect("~/Sesion/error404");
            }
            
          return View(model);
        }
        [AuthorizeUser(idOperacion: 8)]
        [HttpPost]
        public ActionResult gestionarPlataforma(PlataformaViewModel model)
        {

            menu();
            obtenerDatosUsuario();
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = bd.plataforma.Find(model.id);
                        tabla.correo = model.correo;
                        tabla.descripcion = model.descripcion;
                        tabla.nombre = model.nombre;
                        tabla.telefono = model.telefono;
                        tabla.ubicacion = model.ubicacion;
                        tabla.id = model.id;
                        bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                    }

                    return Redirect("~/Sesion/gestionarPlataforma");
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception e)
            {
                ViewBag.error = e;
                
                return Redirect("~/Sesion/error404");
            }
        }


        ///////////////////////////// FIN PLATAFORMA ////////////////////////////
        ///
        ///////////////////////////// INICION COMPETENCIA ADMINISTRADOR ////////////////////////////
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult gestionarCompetencia()
        {
            obtenerDatosUsuario();
            menu();
            List<CompetenciaViewModel> l = null;
            List<basica> estado = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.competencia
                     select new CompetenciaViewModel
                     {
                         nombre=a.nombre,
                         cupos=a.cupos,
                         numeroparticipantes=a.numeroparticipantes,
                         fkestado=a.fkestado,
                         idcompetencia=a.idcompetencia
                     }
                    ).ToList();
                estado = (from a in bd.estadocompetencia
                     select new basica
                     {
                         nombre = a.nombre,
                         id=a.idestadocompetencia 

                     }
                  ).ToList();
            }
            ViewBag.competencia = l;
            ViewBag.estado = estado;
            ViewBag.error = Session["msm"];
            Session["msm"] = null;
            Session["fkc"] = null;
            return View();
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult operacionesGestionCompetencia(int id)
        {
            obtenerDatosUsuario();;
            menu();
            ViewBag.id = id;
            return View();
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult editarCompetencia(int id)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> estado = null;
            var model = new CompetenciaViewModel();
            using (plataformaEntities bd=new plataformaEntities())
            {
                var tabla = bd.competencia.Find(id);
                model.idcompetencia = tabla.idcompetencia;
                model.nombre = tabla.nombre;
                model.cupos = tabla.cupos;
                model.fechacompetencia = tabla.fechacompetencia;
                model.horainicio = tabla.hotainicio;
                model.horafin = tabla.horafin;
                model.cantidadejercicios = tabla.cantidadejercicios;
                model.numeroparticipantes = tabla.numeroparticipantes;
                model.limiteinscripcion = tabla.limiteinscripcion;
                model.fkestado = tabla.fkestado;
                estado = (from a in bd.estadocompetencia
                          select new basica
                          {
                              nombre = a.nombre,
                              id = a.idestadocompetencia

                          }
                  ).ToList();
            }
            ViewBag.estadoCompetencia = estado;
            return View(model);
        }
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult editarCompetencia(CompetenciaViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> estado = null;
            using (plataformaEntities bd=new plataformaEntities()){
                estado = (from a in bd.estadocompetencia
                          select new basica
                          {
                              nombre = a.nombre,
                              id = a.idestadocompetencia

                          }
                 ).ToList();
            }
            ViewBag.estadoCompetencia = estado;
            if (ModelState.IsValid)
            {
                using (plataformaEntities bd = new plataformaEntities())
                {
                    var tabla = bd.competencia.Find(model.idcompetencia);
                    tabla.idcompetencia = model.idcompetencia;
                    tabla.nombre = model.nombre;
                    tabla.cupos = tabla.cupos;
                    tabla.fechacompetencia = model.fechacompetencia;
                    tabla.hotainicio = model.horainicio;
                    tabla.horafin = model.horafin;
                    tabla.cantidadejercicios = model.cantidadejercicios;
                    tabla.numeroparticipantes = model.numeroparticipantes;
                    tabla.limiteinscripcion = model.limiteinscripcion;
                    int fkestado = int.Parse(Request.Form["estadocompetencia"]);
                    model.fkestado = tabla.fkestado;
                    if (fkestado>=1)
                    {
                    tabla.fkestado = fkestado;
                    bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    }
                    else
                    {
                        
                        return View(model);
                    }
                   
                }

            }
            else
            {
                 
                return View(model);
            }

           
            return Redirect("~/Sesion/gestionarCompetencia");
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult agregarCompetencia()
        {
            obtenerDatosUsuario();
            menu();
            List<basica> l = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.estadocompetencia
                     select new basica
                     {
                         id=a.idestadocompetencia,
                         nombre=a.nombre
                     }
                    
                    ).ToList();
            }
            ViewBag.estadoCompetencia = l;
                return View();
        }
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult agregarCompetencia(CompetenciaViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            try
            {
                List<basica> l = null;
                using (plataformaEntities bd = new plataformaEntities())
                {
                    l = (from a in bd.estadocompetencia
                         select new basica
                         {
                             id = a.idestadocompetencia,
                             nombre = a.nombre
                         }

                        ).ToList();
                }
                ViewBag.estadoCompetencia = l;
              
                if (ModelState.IsValid    )
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {

                       
                        var tabla = new competencia();
                        tabla.nombre = model.nombre;
                        tabla.cupos = model.numeroparticipantes;
                        tabla.fechacompetencia = model.fechacompetencia;
                        tabla.hotainicio = model.horainicio;
                        tabla.horafin = model.horafin;
                        tabla.cantidadejercicios = model.cantidadejercicios;
                        tabla.numeroparticipantes = model.numeroparticipantes;
                        tabla.limiteinscripcion = model.limiteinscripcion;
                        int fkestado = int.Parse(Request.Form["estadocompetencia"]);
                        if (fkestado>=1)
                        {
                        tabla.fkestado = (int)(fkestado);
                        bd.competencia.Add(tabla);
                        bd.SaveChanges();
                        }
                        else
                        {
                            return View(model);
                        }
                       

                    }

                    return Redirect("~/Sesion/gestionarCompetencia");
                }
                else
                {
                 
                    return View(model);
                }

            }
            catch (Exception e)
            {
                ViewBag.error = e;

                return Redirect("~/Sesion/error404");
            }
           
           
        }

        ///////////////////////////// FIN COMPETENCIA ADMINISTRADOR ////////////////////////////
        /////////////////////// INICIO GESTIONAR SALA COMPETENCIA/////////////////////////
        [AuthorizeUser(idOperacion: 11)]
        public ActionResult gestionarSalasRegistros(int id)
        {
            obtenerDatosUsuario();
            menu();
            
            List<SalaCompetenciaViewModel> l = null;
            List<basica> estado = null;
            Session["fkc"]=id;
            using (plataformaEntities bd = new plataformaEntities())
            {
                l = (from a in bd.competencia
                     join b in bd.salacompetencia on a.idcompetencia equals b.fkidcompetencia
                     where a.idcompetencia == id  
                     select new SalaCompetenciaViewModel
                     {
                         idsalacompetencia = b.idsalacompetencia,
                         nombre = b.nombre,
                         cupos = b.cupos,
                         fkestado = b.fkestado,
                         fkidcompetencia = b.fkidcompetencia,
                         fkidsala = b.fkidsala

                     }
                    ).ToList();
                estado = (from a in bd.estadosalacompetencia
                          select new basica
                          {
                              id = a.idestadosalacompetencia,
                              nombre = a.nombre
                          }
                  ).ToList();
            }
            ViewBag.sala = l;
            ViewBag.estado = estado;
            ViewBag.idc = id;
            return View();
        }
        [AuthorizeUser(idOperacion: 11)]
        public ActionResult agregarSalaCompetencia()
        {
            obtenerDatosUsuario();
            menu();
            List<basica> estado = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                estado = (from a in bd.estadosalacompetencia
                          select new basica
                          {
                              nombre = a.nombre,
                              id = a.idestadosalacompetencia

                          }
                  ).ToList();
            }
            ViewBag.estado = estado;
            ViewBag.idc = estado;
            return View();
        }
        [AuthorizeUser(idOperacion: 11)]
        [HttpPost]
        public ActionResult agregarSalaCompetencia(SalaCompetenciaViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> es = null;
            
            using (plataformaEntities bd = new plataformaEntities())
            {
                es = (from a in bd.estadosala
                      select new basica
                      {
                          nombre = a.nombre,
                          id = a.idestadosala

                      }
                  ).ToList();
            }
            ViewBag.estado = es;
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = new salacompetencia();
                        tabla.nombre = model.nombre;
                        tabla.cupos = model.cupos;
                        tabla.fkidcompetencia = (int)Session["fkc"];
                        int fkestado = int.Parse(Request.Form["estado"]);
                        if (fkestado >= 1)
                        {
                            tabla.fkestado = (int)(fkestado);
                            bd.salacompetencia.Add(tabla);
                            bd.SaveChanges();
                            return Redirect("~/Sesion/gestionarCompetencia");
                        }
                        else
                        {

                            return View(model);
                        }
                    }
                }
                else
                {

                    return View(model);
                }
            }
            catch (Exception e)
            {
                return Redirect("~/Sesion/error404");
            }

        }
        [AuthorizeUser(idOperacion: 11)]
        public ActionResult editarSalaCompetencia(int id)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> es = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                es = (from a in bd.estadosalacompetencia
                      select new basica
                      {
                          nombre = a.nombre,
                          id = a.idestadosalacompetencia

                      }
                  ).ToList();
            }
            ViewBag.estado = es;
            SalaCompetenciaViewModel model = new SalaCompetenciaViewModel();
            using (plataformaEntities bd = new plataformaEntities())
            {
                var tabla = bd.salacompetencia.Find(id);
                model.idsalacompetencia = tabla.idsalacompetencia;
                model.nombre = tabla.nombre;
                model.cupos = tabla.cupos;
                model.fkestado = tabla.fkestado;
            }
            return View(model);
        }
        [AuthorizeUser(idOperacion: 11)]
        [HttpPost]
        public ActionResult editarSalaCompetencia(SalaCompetenciaViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> es = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                es = (from a in bd.estadosalacompetencia
                      select new basica
                      {
                          nombre = a.nombre,
                          id = a.idestadosalacompetencia

                      }
                  ).ToList();
            }
            ViewBag.estado = es;
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {


                        int fkestado = int.Parse(Request.Form["estado"]);
                        if (fkestado >= 1)
                        {
                            var tabla = bd.salacompetencia.Find(model.idsalacompetencia);
                            tabla.idsalacompetencia = model.idsalacompetencia;
                            tabla.nombre = model.nombre;
                            tabla.cupos = model.cupos;
                            tabla.fkestado = (int)(fkestado);
                            bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                            bd.SaveChanges();
                            return Redirect("~/Sesion/gestionarCompetencia");
                        }
                        else
                        {

                            return View(model);
                        }
                    }
                }
                else
                {

                    return View(model);
                }
            }
            catch (Exception e)
            {
                return Redirect("~/Sesion/error404");
            }

        }
        /////////////////////// FIN GESTIONAR SALA COMPETENCIA/////////////////////////
        ///////////////////////////// INICION ACADEMIA /////////////////////////////////////////
        [AuthorizeUser(idOperacion: 9)]
        public ActionResult gestionarAcademia()
        {
            obtenerDatosUsuario();
            menu();
            List<basica> l = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from i in bd.academia
                     select new basica
                     {
                         id = i.idacademia,
                         nombre = i.nombre

                     }
                     ).ToList();
            }
            ViewBag.academia = l;
            ViewBag.error = Session["msm"];
            Session["msm"] = null;
                return View();
        }
        [AuthorizeUser(idOperacion: 9)]
        public ActionResult agregarAcademia()
        {
            obtenerDatosUsuario();
            menu();
            return View();
        }
        [AuthorizeUser(idOperacion: 9)]
        [HttpPost]
        public ActionResult agregarAcademia(AcademiaViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = new academia();
                        tabla.nombre = model.nombre;
                        bd.academia.Add(tabla);
                        bd.SaveChanges();

                    }

                    return Redirect("~/Sesion/gestionarAcademia");
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception e)
            {
                ViewBag.error = e;

                return Redirect("~/Sesion/error404");
            }
            
        }
        [AuthorizeUser(idOperacion: 9)]
        [HttpGet]
        public ActionResult eliminarAcademia(int id)
        {

            using (plataformaEntities bd=new plataformaEntities())
            {
                var u = new basica();
                u = (from a in bd.persona
                     join b in bd.academia on a.fkacademia equals b.idacademia
                     where b.idacademia==id
                     select new basica
                     {
                        nombre=a.primernombre
                     }
                     ).FirstOrDefault();
                if (u==null)
                {
                var tabla = new academia();
                tabla = bd.academia.Find(id);
                bd.academia.Remove(tabla);
                bd.SaveChanges();
                }
                else
                {
                    Session["msm"] = "No se puede eliminar, academia en uso";
                }
             
            }
            return Redirect("~/Sesion/gestionarAcademia");
        }
        [AuthorizeUser(idOperacion: 9)]
        public ActionResult editarAcademia(int id)
        {
            obtenerDatosUsuario();
            menu();
            var model = new AcademiaViewModel();
            using (plataformaEntities bd=new plataformaEntities())
            {
                var tabla = bd.academia.Find(id); 
                model.id = tabla.idacademia;
                model.nombre = tabla.nombre;

            }

                return View(model);
        }
        [AuthorizeUser(idOperacion: 9)]
        [HttpPost]
        public ActionResult editarAcademia(AcademiaViewModel model)
        {

            obtenerDatosUsuario();
            menu();
            if (ModelState.IsValid)
                {
                using (plataformaEntities bd = new plataformaEntities())
                {
                    var tabla = bd.academia.Find(model.id);
                    tabla.idacademia = model.id;
                    tabla.nombre = model.nombre;
                    bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                }

            }
            else
            {
                return View(model);
            }


            return Redirect("~/Sesion/gestionarAcademia");
        }
        ///////////////////////////// FIN    ACADEMIA /////////////////////////////////////////
        ///
        ///////////////////////////// INICIO DOCUMENTO /////////////////////////////////////////
        [AuthorizeUser(idOperacion:10)]
        public ActionResult gestionarDocumento()
        {
            obtenerDatosUsuario();
            menu();
            List<basica> l = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                l = (from i in bd.tipodocumento
                     select new basica
                     {
                         id = i.idtipodocumento,
                         nombre = i.nombre

                     }
                     ).ToList();
            }
            ViewBag.documento = l;
            ViewBag.error = Session["msm"];
            Session["msm"] = null;
            return View();
        }
        [AuthorizeUser(idOperacion: 10)]
        public ActionResult agregarDocumento()
        {
            obtenerDatosUsuario();
            menu();
            return View();
        }
        [AuthorizeUser(idOperacion: 10)]
        [HttpPost]
        public ActionResult agregarDocumento(DocumentoViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = new tipodocumento();
                        tabla.nombre = model.nombre;
                        bd.tipodocumento.Add(tabla);
                        bd.SaveChanges();

                    }

                    return Redirect("~/Sesion/gestionarDocumento");
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception e)
            {
                ViewBag.error = e;

                return Redirect("~/Sesion/error404");
            }

        }
        [AuthorizeUser(idOperacion: 10)]
        [HttpGet]
        public ActionResult eliminarDocumento(int id)
        {

            using (plataformaEntities bd = new plataformaEntities())
            {
                var u = new basica();
                u = (from a in bd.persona
                     join b in bd.tipodocumento on a.fktipodocumento equals b.idtipodocumento
                     where b.idtipodocumento == id
                     select new basica
                     {
                         nombre = a.primernombre
                     }
                     ).FirstOrDefault();
                if (u == null)
                {
                    var tabla = new tipodocumento();
                    tabla = bd.tipodocumento.Find(id);
                    bd.tipodocumento.Remove(tabla);
                    bd.SaveChanges();
                }
                else
                {
                    Session["msm"] = "No se puede eliminar, academia en uso";
                }

            }
            return Redirect("~/Sesion/gestionarDocumento");
        }
        [AuthorizeUser(idOperacion: 10)]
        public ActionResult editarDocumento(int id)
        {
            obtenerDatosUsuario();
            menu();
            var model = new DocumentoViewModel();
            using (plataformaEntities bd = new plataformaEntities())
            {
                var tabla = bd.tipodocumento.Find(id);
                model.id = tabla.idtipodocumento;
                model.nombre = tabla.nombre;

            }

            return View(model);
        }
        [AuthorizeUser(idOperacion: 10)]
        [HttpPost]
        public ActionResult editarDocumento(DocumentoViewModel model)
        {

            obtenerDatosUsuario();
            menu();
            if (ModelState.IsValid)
            {
                using (plataformaEntities bd = new plataformaEntities())
                {
                    var tabla = bd.tipodocumento.Find(model.id);
                    tabla.idtipodocumento = model.id;
                    tabla.nombre = model.nombre;
                    bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                }

            }
            else
            {
                return View(model);
            }


            return Redirect("~/Sesion/gestionarDocumento");
        }
        ///////////////////////////// FIN    DOCUMENTO /////////////////////////////////////////
        //////////////////////////////// INICIO SALA /////////////////////////////////////////
        [AuthorizeUser(idOperacion: 11)]
        public ActionResult gestionarSala()
        {
            obtenerDatosUsuario();
            menu();
            List<SalaViewModel> l = null;
            List<basica> estado = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.sala
                     select new SalaViewModel
                     {
                         nombre=a.nombre,
                         cupos=a.cupos,
                         fkestado=a.fkestado,
                         idsala=a.idsala
                         
                     }
                     ).ToList();
                      estado = (from a in bd.estadosala
                     select new basica
                     {
                          nombre=a.nombre,
                          id=a.idestadosala

                     }
                  ).ToList();
            }
            ViewBag.sala = l;
            ViewBag.estado = estado;
            ViewBag.error = Session["msm"];
            Session["msm"] = null;
            return View();
        }
        [AuthorizeUser(idOperacion: 11)]
        public ActionResult agregarSala()
        {
            obtenerDatosUsuario();
            menu();
            List<basica> estado = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                estado = (from a in bd.estadosala
                          select new basica
                          {
                              nombre = a.nombre,
                              id = a.idestadosala

                          }
                  ).ToList();
            }
            ViewBag.estado = estado;
            return View();
        }
        [AuthorizeUser(idOperacion: 11)]
        [HttpPost]
        public ActionResult agregarSala(SalaViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> es = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                es = (from a in bd.estadosala
                      select new basica
                      {
                          nombre = a.nombre,
                          id = a.idestadosala

                      }
                  ).ToList();
            }
            ViewBag.estado = es;
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = new sala();
                        tabla.nombre = model.nombre;
                        tabla.cupos = model.cupos;
                        int fkestado = int.Parse(Request.Form["estado"]);
                        if (fkestado >= 1)
                        {
                            tabla.fkestado = (int)(fkestado);
                            bd.sala.Add(tabla);
                            bd.SaveChanges();
                            return Redirect("~/Sesion/gestionarSala");
                        }
                        else
                        {

                            return View(model);
                        }
                    }
                }
                else
                {

                    return View(model);
                }
            }
            catch (Exception e)
            {
                return Redirect("~/Sesion/error404");
            }

        }
        [AuthorizeUser(idOperacion: 11)]
        public ActionResult editarSala(int id)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> es = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                es = (from a in bd.estadosala
                      select new basica
                      {
                          nombre = a.nombre,
                          id = a.idestadosala

                      }
                  ).ToList();
            }
            ViewBag.estado = es;
            SalaViewModel model = new SalaViewModel();
            using (plataformaEntities bd=new plataformaEntities())
            {
                var tabla = bd.sala.Find(id);
                model.idsala = tabla.idsala;
                model.nombre = tabla.nombre;
                model.cupos = tabla.cupos;
                model.fkestado = tabla.fkestado;
            }
                return View(model);
        }
        [AuthorizeUser(idOperacion: 11)]
        [HttpPost]
        public ActionResult editarSala(SalaViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> es = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                es = (from a in bd.estadosala
                      select new basica
                      {
                          nombre = a.nombre,
                          id = a.idestadosala

                      }
                  ).ToList();
            }
            ViewBag.estado = es;
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                       
                       
                        int fkestado = int.Parse(Request.Form["estado"]);
                        if (fkestado >= 1)
                        {
                            var tabla = bd.sala.Find(model.idsala);
                            tabla.idsala = model.idsala;
                            tabla.nombre = model.nombre;
                            tabla.cupos = model.cupos;              
                            tabla.fkestado = (int)(fkestado);
                            bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                            bd.SaveChanges();
                            return Redirect("~/Sesion/gestionarSala");
                        }
                        else
                        {

                            return View(model);
                        }
                    }
                }
                else
                {

                    return View(model);
                }
            }
            catch (Exception e)
            {
                return Redirect("~/Sesion/error404");
            }

        }
        [AuthorizeUser(idOperacion: 11)]
        [HttpGet]
        public ActionResult eliminarSala(int id)
        {

            using (plataformaEntities bd = new plataformaEntities())
            {
                var u = new basica();
                u = (from a in bd.sala
                     join b in bd.salacompetencia on a.idsala equals b.fkidsala
                     where a.idsala==id
                     select new basica
                     {
                         nombre = a.nombre
                     }
                     ).FirstOrDefault();
                if (u == null)
                {
                    var tabla = new sala();
                    tabla = bd.sala.Find(id);
                    bd.sala.Remove(tabla);
                    bd.SaveChanges();
                }
                else
                {
                    Session["msm"] = "No se puede eliminar, sala en uso";
                }

            }
            return Redirect("~/Sesion/gestionarSala");
        }
        //////////////////////////////// FIN SALA /////////////////////////////////////////
        //////////////////////////////// INICIO REGISTRO A COMPETENCIA /////////////////////////////////////////
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult registroACompetencia()
        {
            obtenerDatosUsuario();
            menu();
            List<CompetenciaViewModel> l = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.competencia
                     join b in bd.estadocompetencia on a.fkestado equals b.idestadocompetencia
                     where b.idestadocompetencia==1
                     select new CompetenciaViewModel
                     {
                         idcompetencia=a.idcompetencia,
                         nombre=a.nombre,
                         fechacompetencia=a.fechacompetencia,
                         horafin=a.horafin,
                         horainicio=a.hotainicio
                     }
                    
                    ).ToList();
            }
            ViewBag.l = l;
            ViewBag.error = Session["msm"];
            Session["msm"] = null;
                return View(l);
        }
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult regristroDocumento(int id)
        {
            obtenerDatosUsuario();
            menu();
             
            Session["competencia"] = id;
            int identi = (int)Session["identificacion"];
            int idc = (int)id;
            basica v = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                v = (from a in bd.persona
                     join b in bd.grupopersona on a.idpersona equals b.fkidpersona
                     join c in bd.grupo on b.fkidgrupo equals c.idgrupo
                     where a.identificacion == identi && c.fkidcompetencia == idc && c.fkidestado != 3
                     select new basica
                     {
                         nombre = c.nombre
                     }
                    ).FirstOrDefault();
                if (v == null)
                {
                     return View();
                }
                else
                {

                    Session["msm"] = "Existe un equipo registrado en la competencia";
                    return Redirect("~/Sesion/registroACompetencia");
                }

            }
            
        }
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public ActionResult regristroDocumento(DocumentoEquipoViewModel model)
        {
            obtenerDatosUsuario();
            menu();

            try
            {
                if (ModelState.IsValid)
                {
                    basica v = null;
                    List<basica> v2 = null;
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        int fkcompetencia = (int)Session["competencia"];
                        v = (from a in bd.competencia
                             join b in bd.grupo on a.idcompetencia equals b.fkidcompetencia
                             join c in bd.grupopersona on b.idgrupo equals c.fkidgrupo
                             join d in bd.persona on c.fkidpersona equals d.idpersona
                             join e in bd.estadogrupo on b.fkidestado equals e.idestadogrupo
                             where a.idcompetencia == fkcompetencia && (d.identificacion == model.documento1 || d.identificacion == model.documento2) && e.idestadogrupo!=3
                             select new basica
                             {
                                 nombre = a.nombre
                             }

                            ).FirstOrDefault();
                        v2 = (from p in bd.persona
                              where p.identificacion == model.documento1|| p.identificacion == model.documento2
                              select new basica
                              {
                                  nombre = p.primerapellido
                              }

                              ).ToList();
                    }
                    if (v != null)
                    {
                        
                        ViewBag.error = "Documentos en uso en la misma competencia";
                        return View(model);
                    }
                    else if (v2.Count < 2)
                    {
                        
                        ViewBag.error = "Documentos no encontrados o erroneos";
                        return View(model);
                    }
                }

                else
                {

                    return View(model);
                }
            }
            catch (Exception e)
            {
                return Redirect("~/Sesion/error404");
            }
            Session["d1"] = model.documento1;
            Session["d2"] = model.documento2;
            ViewBag.error = "";
            return Redirect("~/Sesion/registroEquipo");
        }
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult registroEquipo()
        {
            obtenerDatosUsuario();
            menu();
            List<basica> l = null;
            basica p1 = null;
            basica p2 = null;
            basica comp = null;
            int id1 = (int)Session["d1"];
            int id2 = (int)Session["d2"];
            int com = (int)Session["competencia"];
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.nivel
                     select new basica
                     {
                         id=a.idnivel,
                         nombre=a.nombre+", "+a.descripcion
                     }
                    ).ToList();
                p1= (from a in bd.persona
                     where a.identificacion==id1
                     select new basica
                     {
                         id = a.idpersona,
                         nombre = a.primernombre+" "+a.primerapellido,
                         cc=""+a.identificacion
                     }
                    ).FirstOrDefault();
                p2 = (from a in bd.persona
                      where a.identificacion == id2
                      select new basica
                      {
                          id = a.idpersona,
                          nombre = a.primernombre + " " + a.primerapellido,
                          cc = "" + a.identificacion
                      }
           ).FirstOrDefault();
                comp = (from a in bd.competencia
                      where a.idcompetencia == com
                      select new basica
                      {
                        
                          nombre = a.nombre
                          
                      }
           ).FirstOrDefault();
            }
            ViewBag.p1 = p1;
            ViewBag.p2 = p2;
            ViewBag.com = comp;
            ViewBag.nivel = l;
            return View();
        }
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public ActionResult registroEquipo(EquipoViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            List<basica> l = null;
            basica p1 = null;
            basica p2 = null;
            basica comp = null;
            int id1 = (int)Session["d1"];
            int id2 = (int)Session["d2"];
            int com = (int)Session["competencia"];
            using (plataformaEntities bd = new plataformaEntities())
            {
                l = (from a in bd.nivel
                     select new basica
                     {
                         id = a.idnivel,
                         nombre = a.nombre + ", " + a.descripcion
                     }
                    ).ToList();
                p1 = (from a in bd.persona
                      where a.identificacion == id1
                      select new basica
                      {
                          id = a.idpersona,
                          nombre = a.primernombre + " " + a.primerapellido,
                          cc = "" + a.identificacion
                      }
                    ).FirstOrDefault();
                p2 = (from a in bd.persona
                      where a.identificacion == id2
                      select new basica
                      {
                          id = a.idpersona,
                          nombre = a.primernombre + " " + a.primerapellido,
                          cc = "" + a.identificacion
                      }
           ).FirstOrDefault();
                comp = (from a in bd.competencia
                        where a.idcompetencia == com
                        select new basica
                        {

                            nombre = a.nombre

                        }
           ).FirstOrDefault();
            }
            ViewBag.p1 = p1;
            ViewBag.p2 = p2;
            ViewBag.com = comp;
            ViewBag.nivel = l;



            try
            {
                if (ModelState.IsValid)
                {

                    string usuario, contra;
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        int fknivel = int.Parse(Request.Form["nivel"]);
                        if (fknivel>=1)
                        {
                            var end = (from aux in bd.grupo
                                       orderby aux.idgrupo descending
                                       select aux
                            ).FirstOrDefault();
                            if (end != null)
                            {  
                              usuario = "Equipo " +(end.idgrupo + 1);
                              contra = contrasena();

                            }
                            else
                            {
                                  usuario = "Equipo " + (1);
                                  contra = contrasena();
                            }
                         
                            bd.sp_agregar_grupo(model.nombre,id1,id2,com,fknivel,usuario,contra);

                            bd.SaveChanges();
                            return Redirect("~/Sesion/registroACompetencia");
                            //procedimiento
                        }
                        else
                        {
                            return View(model);
                        }
                    }
                }

                else
                {

                    return View(model);
                }
            }
            catch (Exception e)
            {
                return View();
            }

        }
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult editarEquipoPropio(int id)
        {
            obtenerDatosUsuario();
            menu();
            int identi = (int)Session["identificacion"];
            int idc = (int)id;
            basica v = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                v = (from a in bd.persona
                     join b in bd.grupopersona on a.idpersona equals b.fkidpersona
                     join c in bd.grupo on b.fkidgrupo equals c.idgrupo
                     where a.identificacion==identi && c.fkidcompetencia==idc && c.fkidestado!=3
                     select new basica
                     {
                         nombre=c.nombre
                     }
                    ).FirstOrDefault();
                if (v!=null)
                {
                 //return View(bd.sp_listarParaEditar_equipoPropio(idc, identi).ToList());
                return View(bd.sp_listarParaEditar_equipoPropio(idc, identi).FirstOrDefault());
                }
                else
                {
                    
                    Session["msm"] = "No existe un equipo para gestionar";
                    return Redirect("~/Sesion/registroACompetencia");
                }
               
            }

            
        }
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult editarEquipoU(int id)
        {
            obtenerDatosUsuario();
            menu();
            EqViewModel model = new EqViewModel();
            using (plataformaEntities bd=new plataformaEntities())
            {
               
                var tabla = bd.grupo.Find(id);
                model.id = tabla.idgrupo;
                model.nombre = tabla.nombre;
                model.usuario = tabla.usuario;
                model.contrasena = tabla.contrasena;
                model.fkidcompetencia = tabla.fkidcompetencia;
                model.fkidnivel = tabla.fkidnivel;
                model.fkidestado = tabla.fkidestado;
            }
           
                return View(model);
        }
        [AuthorizeUser(idOperacion: 3)]
        [HttpPost]
        public ActionResult editarEquipoU(EqViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd=new plataformaEntities())
                    {
                        var tabla = bd.grupo.Find(model.id);
                        tabla.idgrupo = model.id;
                        tabla.nombre = model.nombre;
                        //tabla.usuario = model.usuario;
                        //tabla.contrasena = model.contrasena;
                        //tabla.fkidcompetencia = model.fkidcompetencia;
                        //tabla.fkidnivel = model.fkidnivel;
                        //tabla.fkidestado = model.fkidestado;
                        bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                        return Redirect("~/Sesion/registroACompetencia");
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {

            }

            return View();
        }
        [AuthorizeUser(idOperacion: 3)]
        public ActionResult eliminarEquipoU(int id)
        {

            using (plataformaEntities bd=new plataformaEntities())
            {
                bd.sp_eliminarEquipo(id);
                Session["msm"] = "Equipo eliminado";
            }
                return Redirect("~/Sesion/registroACompetencia");
        }
        public string contrasena()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 5;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }
        //////////////////////////////// FIN REGISTRO A COMPETENCIA /////////////////////////////////////////
        ////////////////////////////////// INICIO SOLICITUDES Y ASIGNACION DE SALAS ////////////////////////////
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult reponderSolicitudCompetencia(int id)
        {
            obtenerDatosUsuario();
            menu();
            basica v = null;
            Session["com"] = id;
            using (plataformaEntities bd=new plataformaEntities())
            {
                v = (from a in bd.grupo
                     join b in bd.competencia on a.fkidcompetencia equals b.idcompetencia
                     where a.fkidestado==2
                     select new basica
                     {
                         nombre=a.nombre
                     }
                    
                    ).FirstOrDefault();
                if (v==null)
                {
                    Session["msm"] = "No hay solicitudes";
                    return Redirect("~/Sesion/gestionarCompetencia");

                }
                else
                {
                    
                    return View(bd.sp_listarSolicitudes(id).ToList());
                }
                    
            }
            
                
        }

        [AuthorizeUser(idOperacion: 2)]
        public ActionResult aceptarSolicitud(int id)
        {
          
            using (plataformaEntities bd=new plataformaEntities()){
                bd.sp_responderSolicitudCompetencia(id);
                var auxequipo = bd.grupo.Find(id);

                PersonaViewModel p1 = (from a in bd.persona
                                       join b in bd.grupopersona on a.idpersona equals b.fkidpersona
                                       where b.fkidgrupo==id
                                       select new PersonaViewModel
                          {
                             idpersona=a.idpersona,
                             identificacion=a.identificacion,
                             primernombre=a.primernombre,
                             segundonombre=a.segundonombre,
                             primerapellido=a.primerapellido,
                             segundoapellido=a.segundoapellido  
                             
                          }
                    ).FirstOrDefault();
                PersonaViewModel p2 = (from a in bd.persona
                                       join b in bd.grupopersona on a.idpersona equals b.fkidpersona
                                       where b.fkidgrupo == id && a.idpersona!=p1.idpersona
                                       select new PersonaViewModel
                                       {
                                           idpersona = a.idpersona,
                                           identificacion = a.identificacion,
                                           primernombre = a.primernombre,
                                           segundonombre = a.segundonombre,
                                           primerapellido = a.primerapellido,
                                           segundoapellido = a.segundoapellido
                                       }
                   ).FirstOrDefault();
                basica c1 = (from a in bd.usuario
                             join b in bd.persona on a.fkidepersona equals b.idpersona
                             where b.idpersona == p1.idpersona
                             select new basica
                             {
                                 nombre = a.correoelectronico
                             }
                          ).FirstOrDefault() ;
                basica c2 = (from a in bd.usuario
                             join b in bd.persona on a.fkidepersona equals b.idpersona
                             where b.idpersona == p2.idpersona
                             select new basica
                             {
                                 nombre = a.correoelectronico
                             }
                ).FirstOrDefault();
                basica c4 = (from a in bd.competencia
                             join b in bd.grupo on   a.idcompetencia equals b.fkidcompetencia
                             select new basica
                             {
                                 nombre = a.nombre
                                 }
                         ).FirstOrDefault();

                p1.correo = c1.nombre;
                p2.correo = c2.nombre;
                if (auxequipo.fkidestado==1)//Enviar correo aceptacion equipo
                {

                    try
                    {
                        MailMessage correo = new MailMessage();//importamos using System.Net.Mail;
                        correo.From = new MailAddress("semillerou678@gmail.com");//Correo para enviar los mensajes
                        correo.To.Add(p1.correo);
                        correo.To.Add(p2.correo);
                        correo.Subject = "Semillero aceptacion de equipo";
                        correo.Body = "El equipo "+auxequipo.nombre+" ha sido aceptado a la competencia "+c4.nombre+"\n"+
                            "Primer participante "+p1.primernombre+" "+p1.primerapellido+", correo "+ p1.correo+"\n"+
                            "Segundo participante " + p2.primernombre + " " + p2.primerapellido + ", correo " + p2.correo + "\n"
                           +"Nombre equipo: " + auxequipo.nombre;
                        correo.IsBodyHtml = true;
                        correo.Priority = MailPriority.Normal;


                        //Configurar del servidor smtp para cada tipo hay diferencia gmail,yahoo etc
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 25;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = true;
                        //correo contrasena
                        string sCuentaCorreo = "semillerou678@gmail.com";//correo para enviar correos
                        string sContrasenaCorreo = "12345qwertasd";//contrasena
                                                                   //capturar credenciales,luego iniciar conexion
                        smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sContrasenaCorreo);

                        //Enviar correo
                        smtp.Send(correo);

                    }
                    catch (Exception e)
                    {
                        ViewBag.error = e;

                    }

                }
            }
            return Redirect(Url.Content("~/Sesion/reponderSolicitudCompetencia/" + Session["com"]));
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult reachazarSolicitud(int id)
        {

            using (plataformaEntities bd = new plataformaEntities())
            {
                bd.sp_negarSolicitud(id);
                var auxequipo = bd.grupo.Find(id);

                PersonaViewModel p1 = (from a in bd.persona
                                       join b in bd.grupopersona on a.idpersona equals b.fkidpersona
                                       where b.fkidgrupo == id
                                       select new PersonaViewModel
                                       {
                                           idpersona = a.idpersona,
                                           identificacion = a.identificacion,
                                           primernombre = a.primernombre,
                                           segundonombre = a.segundonombre,
                                           primerapellido = a.primerapellido,
                                           segundoapellido = a.segundoapellido

                                       }
                    ).FirstOrDefault();
                PersonaViewModel p2 = (from a in bd.persona
                                       join b in bd.grupopersona on a.idpersona equals b.fkidpersona
                                       where b.fkidgrupo == id && a.idpersona != p1.idpersona
                                       select new PersonaViewModel
                                       {
                                           idpersona = a.idpersona,
                                           identificacion = a.identificacion,
                                           primernombre = a.primernombre,
                                           segundonombre = a.segundonombre,
                                           primerapellido = a.primerapellido,
                                           segundoapellido = a.segundoapellido
                                       }
                   ).FirstOrDefault();
                basica c1 = (from a in bd.usuario
                             join b in bd.persona on a.fkidepersona equals b.idpersona
                             where b.idpersona == p1.idpersona
                             select new basica
                             {
                                 nombre = a.correoelectronico
                             }
                          ).FirstOrDefault();
                basica c2 = (from a in bd.usuario
                             join b in bd.persona on a.fkidepersona equals b.idpersona
                             where b.idpersona == p2.idpersona
                             select new basica
                             {
                                 nombre = a.correoelectronico
                             }
                ).FirstOrDefault();
                basica c4 = (from a in bd.competencia
                             join b in bd.grupo on a.idcompetencia equals b.fkidcompetencia
                             select new basica
                             {
                                 nombre = a.nombre
                             }
                         ).FirstOrDefault();

                p1.correo = c1.nombre;
                p2.correo = c2.nombre;
                if (auxequipo.fkidestado == 1)//Enviar correo aceptacion equipo
                {

                    try
                    {
                        MailMessage correo = new MailMessage();//importamos using System.Net.Mail;
                        correo.From = new MailAddress("semillerou678@gmail.com");//Correo para enviar los mensajes
                        correo.To.Add(p1.correo);
                        correo.To.Add(p2.correo);
                        correo.Subject = "Semillero equipo no aceptado a la competencia";
                        correo.Body = "El equipo " + auxequipo.nombre + " ha sido aceptado a la competencia " + c4.nombre + "\n" +
                            "Primer participante " + p1.primernombre + " " + p1.primerapellido + ", correo " + p1.correo + "\n" +
                            "Segundo participante " + p2.primernombre + " " + p2.primerapellido + ", correo " + p2.correo + "\n"
                           + "Nombre equipo: " + auxequipo.nombre;
                        correo.IsBodyHtml = true;
                        correo.Priority = MailPriority.Normal;


                        //Configurar del servidor smtp para cada tipo hay diferencia gmail,yahoo etc
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 25;
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = true;
                        //correo contrasena
                        string sCuentaCorreo = "semillerou678@gmail.com";//correo para enviar correos
                        string sContrasenaCorreo = "12345qwertasd";//contrasena
                                                                   //capturar credenciales,luego iniciar conexion
                        smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sContrasenaCorreo);

                        //Enviar correo
                        smtp.Send(correo);

                    }
                    catch (Exception e)
                    {
                        ViewBag.error = e;

                    }

                }
            }
            return Redirect(Url.Content("~/Sesion/reponderSolicitudCompetencia/" + Session["com"]));
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult asentarCompetencia(int id)
        {
            obtenerDatosUsuario();
            menu();
            Session["com"] = id;
            List<basica> l = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.competencia
                     join b in bd.salacompetencia on a.idcompetencia equals b.fkidcompetencia
                     where a.idcompetencia==id && b.fkestado==1
                     select new basica
                     {
                         id=b.idsalacompetencia,
                         nombre=b.nombre
                     }
                    
                    ).ToList();
                ViewBag.salas = l;
                ViewBag.com = id;
                return View(bd.sp_listarGruposEnSala(id).ToList());
            }
        }
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult cambiarSalaCompetenciaEquipo()
        {
            int idsala = int.Parse(Request.Form["sala"]);
            int idgrupo = int.Parse(Request.Form["idgrupo"]);
            using (plataformaEntities bd=new plataformaEntities())
            {
                bd.sp_cambiarDeSala(idgrupo, idsala);
            }
                 
            return Redirect(Url.Content("~/Sesion/asentarCompetencia/" + Session["com"]));
        }

        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult editarEquipo()
        {
            obtenerDatosUsuario();
            menu();
            
            int identi = int.Parse(Request.Form["identificacion"]);
            int idc = int.Parse(Request.Form["comp"]);
            basica v = null;
            using (plataformaEntities bd = new plataformaEntities())
            {
                v = (from a in bd.persona
                     join b in bd.grupopersona on a.idpersona equals b.fkidpersona
                     join c in bd.grupo on b.fkidgrupo equals c.idgrupo
                     where a.identificacion == identi && c.fkidcompetencia == idc && c.fkidestado != 3
                     select new basica
                     {
                         nombre = c.nombre
                     }
                    ).FirstOrDefault();
                if (v != null)
                {
                    //return View(bd.sp_listarParaEditar_equipoPropio(idc, identi).ToList());
                    return View(bd.sp_listarParaEditar_equipoPropio(idc, identi).FirstOrDefault());
                }
                else
                {

                    Session["msm"] = "No existe un equipo para gestionar";
                    return Redirect("~/Sesion/registroACompetencia");
                }

            }


        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult editarEquipoA(int id)
        {
            obtenerDatosUsuario();
            menu();
            EqViewModel model = new EqViewModel();
            using (plataformaEntities bd = new plataformaEntities())
            {

                var tabla = bd.grupo.Find(id);
                model.id = tabla.idgrupo;
                model.nombre = tabla.nombre;
                model.usuario = tabla.usuario;
                model.contrasena = tabla.contrasena;
                model.fkidcompetencia = tabla.fkidcompetencia;
                model.fkidnivel = tabla.fkidnivel;
                model.fkidestado = tabla.fkidestado;
            }

            return View(model);
        }
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult editarEquipoA(EqViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = bd.grupo.Find(model.id);
                        tabla.idgrupo = model.id;
                        tabla.nombre = model.nombre;
                        //tabla.usuario = model.usuario;
                        //tabla.contrasena = model.contrasena;
                        //tabla.fkidcompetencia = model.fkidcompetencia;
                        //tabla.fkidnivel = model.fkidnivel;
                        //tabla.fkidestado = model.fkidestado;
                        bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                        return Redirect(Url.Content("~/Sesion/asentarCompetencia/" + Session["com"]));
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {

            }

            return View();
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult eliminarEquipoA(int id)
        {

            using (plataformaEntities bd = new plataformaEntities())
            {
                bd.sp_eliminarEquipo(id);
                Session["msm"] = "Equipo eliminado";
            }
            return Redirect(Url.Content("~/Sesion/asentarCompetencia/" + Session["com"]));
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult subirResultado(int id)
        {
            obtenerDatosUsuario();
            menu();
            basica l = null;
            Session["eq"] = id;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.competencia
                     where a.idcompetencia==id
                     select new basica
                     {
                         id=a.fkestado
                     }
                     ).FirstOrDefault();
                if (l.id == 2)
                {
                    return View(bd.sp_listarGruposSubirResultado(id).ToList());
                }
                else
                {
                    
                    return Redirect("~/Sesion/gestionarCompetencia");
                }
            }
           
             
        }
        [AuthorizeUser(idOperacion: 2)]
        [HttpPost]
        public ActionResult subirResultado()
        {
            
            using (plataformaEntities bd = new plataformaEntities())
            {
                int idgrupo = int.Parse(Request.Form["idgrupo"]);
                int puesto = int.Parse(Request.Form["puesto"]);
                bd.sp_subirResultado(idgrupo,puesto);
                return Redirect(Url.Content("~/Sesion/subirResultado/" + Session["eq"]));

            }


        }
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult historialCompetenciaU()
        {
            obtenerDatosUsuario();
            menu();
            int id = (int)Session["identificacion"];
            using (plataformaEntities bd=new plataformaEntities())
            {
                return View(bd.sp_competenciasU(id).ToList());
            }
                
        }
        [AuthorizeUser(idOperacion: 5)]
        public ActionResult resultadosCompetenciaU(int id)
        {
            obtenerDatosUsuario();
            menu();
             
            using (plataformaEntities bd = new plataformaEntities())
            {
                return View(bd.sp_historialCompetenciaU(id).ToList());
            }

        }
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult historialCompetenciaA()
        {
            obtenerDatosUsuario();
            menu();
             
            using (plataformaEntities bd = new plataformaEntities())
            {
                return View(bd.sp_competenciasA().ToList());
            }

        }
        [AuthorizeUser(idOperacion: 4)]
        public ActionResult resultadosCompetenciaA(int id)
        {
            obtenerDatosUsuario();
            menu();

            using (plataformaEntities bd = new plataformaEntities())
            {
                return View(bd.sp_historialCompetenciaA(id).ToList());
            }

        }
        ////////////////////////////////// FIN SOLICITUDES Y ASIGNACION DE SALAS ////////////////////////////
        ///

        ////////////////////////////////// INICIO DATOS PERSONALES ////////////////////////////


        public ActionResult datosPersonales()
        {
            obtenerDatosUsuario();
            menu();
            int id = (int)Session["identificacion"];
            PersonaViewModel model = new PersonaViewModel();
            basica b = null;
            using (plataformaEntities bd=new plataformaEntities())
            {

                b = (from a in bd.persona
                     where a.identificacion==id
                     select new basica
                     {
                         id=a.idpersona
                     }
                     ).FirstOrDefault();
                var tabla = bd.persona.Find(b.id);

                model.idpersona = tabla.idpersona;
                model.identificacion = tabla.identificacion;
                model.primernombre = tabla.primernombre;
                model.segundonombre = tabla.segundonombre;
                model.primerapellido = tabla.primerapellido;
                model.segundoapellido = tabla.segundoapellido;
                model.telefono = int.Parse(tabla.telefono);
                model.fktipodocumento = tabla.fktipodocumento;
                model.fkacademia = tabla.fkacademia;
                         

            }
                return View(model);
        }

        public ActionResult editarDatosPersonales(int id)
        {
            obtenerDatosUsuario();
            menu();
             
            PersonaViewModel model = new PersonaViewModel();
           
            using (plataformaEntities bd = new plataformaEntities())
            {
                var tabla = bd.persona.Find(id);
                model.idpersona = tabla.idpersona;
                model.identificacion = tabla.identificacion;
                model.primernombre = tabla.primernombre;
                model.segundonombre = tabla.segundonombre;
                model.primerapellido = tabla.primerapellido;
                model.segundoapellido = tabla.segundoapellido;
                model.telefono = int.Parse(tabla.telefono);
                model.fktipodocumento = tabla.fktipodocumento;
                model.fkacademia = tabla.fkacademia;


            }
            return View(model);
        }
        [HttpPost]
        public ActionResult editarDatosPersonales(PersonaViewModel model)
        {
             
            obtenerDatosUsuario();
            menu();
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = bd.persona.Find(model.idpersona);
                        tabla.idpersona = model.idpersona;
                        tabla.identificacion = model.identificacion;
                        tabla.primernombre = model.primernombre;
                        tabla.segundonombre = model.segundonombre;
                        tabla.primerapellido = model.primerapellido;
                        tabla.segundoapellido = model.segundoapellido;
                        tabla.telefono = ""+(model.telefono);
                        tabla.fktipodocumento = tabla.fktipodocumento;
                        tabla.fkacademia = tabla.fkacademia;

                        bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                        return Redirect(("~/Sesion/datosPersonales"));
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {

            }

            return View();

        }

        public ActionResult datosSesion()
        {
            obtenerDatosUsuario();
            menu();
            int id = (int)Session["identificacion"];
            SesionViewModel model = new SesionViewModel();
            basica ba = null;
            using (plataformaEntities bd = new plataformaEntities())
            {

                ba = (from a in bd.persona
                     join b in bd.usuario on a.idpersona equals b.fkidepersona
                     where a.identificacion == id
                     select new basica
                     {
                         id = b.idusuario
                     }
                     ).FirstOrDefault();
                var tabla = bd.usuario.Find(ba.id);

                model.idusuario = tabla.idusuario;
                model.correo = tabla.correoelectronico;
                model.contrasena = tabla.contrasena;
                model.fkidpersona = tabla.fkidepersona;
                model.fkrol = tabla.fkrol;
                model.fkestado = tabla.fkestado;
                

            }
            return View(model);
            
        }
        public ActionResult editardatosSesion(int id)
        {
            obtenerDatosUsuario();
            menu();
             
            SesionViewModel model = new SesionViewModel();
            basica ba = null;
            using (plataformaEntities bd = new plataformaEntities())
            {

                ba = (from a in bd.persona
                      join b in bd.usuario on a.idpersona equals b.fkidepersona
                      where b.idusuario==id
                      select new basica
                      {
                          id = b.idusuario
                      }
                     ).FirstOrDefault();
                var tabla = bd.usuario.Find(ba.id);

                model.idusuario = tabla.idusuario;
                model.correo = tabla.correoelectronico;
                model.contrasena ="*******";
                model.fkidpersona = tabla.fkidepersona;
                model.fkrol = tabla.fkrol;
                model.fkestado = tabla.fkestado;


            }
            return View(model);

        }
        [HttpPost]
        public ActionResult editardatosSesion(SesionViewModel model)
        {
            obtenerDatosUsuario();
            menu();

           
            try
            {
                if (ModelState.IsValid)
                {
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = bd.usuario.Find(model.idusuario);

                        if (Models.Encriptacion.Encrypt.GetSHA256(tabla.contrasena)== Models.Encriptacion.Encrypt.GetSHA256(tabla.contrasena) && model.contrasenanueva!="")
                        {
                            tabla.idusuario = model.idusuario;
                            tabla.correoelectronico = model.correo;
                            tabla.contrasena = Models.Encriptacion.Encrypt.GetSHA256(model.contrasenanueva);
                            tabla.fkidepersona = tabla.fkidepersona;
                            tabla.fkrol = tabla.fkrol;
                            tabla.fkestado = tabla.fkestado;
                            bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                            bd.SaveChanges();
                        }
                        
                         
                        

                      
                        return Redirect(("~/Sesion/datosPersonales"));
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {

            }

            return View();
        }
        ////////////////////////////////// FIN DATOS PERSONALES ////////////////////////////
        ///////////////////////////////////// INICIO CORREOS ///////////////////////
        [AuthorizeUser(idOperacion: 16)]
        public ActionResult listarCompetenciasCorreo()
        {
            obtenerDatosUsuario();
            menu();
            ViewBag.error = Session["msm"];
            Session["msm"] = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                return View(bd.sp_listarCompetenciasActivasCorreo().ToList());
            }
                
        }
        [AuthorizeUser(idOperacion: 16)]
        public ActionResult editarCorreoConfirmacion(int id)
        {
            obtenerDatosUsuario();
            menu();
            CorreoConfirmacionViewModels correo = new CorreoConfirmacionViewModels();
            using (plataformaEntities bd = new plataformaEntities())
            {
                correo = (from a in bd.correoCompetencia
                          where a.idcorreoCompetencia == id
                          select new CorreoConfirmacionViewModels
                          {
                              id = a.idcorreoCompetencia,
                              asunto = a.asunto,
                              mensaje = a.mensaje,
                              fkidcompetencia = a.fkidcompetencia

                          }

                    ).FirstOrDefault();

            }
            return View(correo);
        }
        [AuthorizeUser(idOperacion: 16)]
        [HttpPost]
        public ActionResult editarCorreoConfirmacion(CorreoConfirmacionViewModels model)
        {
            obtenerDatosUsuario();
            menu();


            try
            {
                if (ModelState.IsValid)
                {
                    
                    using (plataformaEntities bd = new plataformaEntities())
                    {
                        var tabla = bd.correoCompetencia.Find(model.id);
                        tabla.idcorreoCompetencia = tabla.idcorreoCompetencia;
                        tabla.asunto = model.asunto;
                        tabla.mensaje = model.mensaje;
                        tabla.fkidcompetencia = tabla.fkidcompetencia;
                        bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();

                        return Redirect(("~/Sesion/listarCompetenciasCorreo"));
                    }
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception e)
            {

            }

            return View();
        }
        [AuthorizeUser(idOperacion: 15)]
        public ActionResult gestionarCorreoMasivo()
        {
            obtenerDatosUsuario();
            menu();
            CorreoMasivoViewModel correo = new CorreoMasivoViewModel();
            using (plataformaEntities bd=new plataformaEntities()) {
                correo = (from a in bd.correomasivo
                          where a.correomasivo1==1
                          select new CorreoMasivoViewModel
                          {
                               id=a.correomasivo1,
                               asunto=a.asunto,
                               mensaje=a.mensaje
                          }
                    ).FirstOrDefault();
            }
            ViewBag.error = Session["msm"];
            Session["msm"] = null;
                return View(correo);
          
        }
        [AuthorizeUser(idOperacion: 15)]
        [HttpPost]
        public ActionResult gestionarCorreoMasivo(CorreoMasivoViewModel model)
        {
            obtenerDatosUsuario();
            menu();
            if (ModelState.IsValid)
            {
                using (plataformaEntities bd=new plataformaEntities())
                {
                    var tabla = bd.correomasivo.Find(1);
                    tabla.correomasivo1 = tabla.correomasivo1;
                    tabla.asunto = model.asunto;
                    tabla.mensaje = model.mensaje;
                    bd.Entry(tabla).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return Redirect(("~/Sesion/gestionarCorreoMasivo"));

                }
            }
            else
            {
                return View(model);
            }
             
        }
        [AuthorizeUser(idOperacion: 15)]
        public ActionResult enviarCorreoMasivo()
        {
            List<PersonaViewModel> l = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.usuario
                     select new PersonaViewModel
                     {
                         correo=a.correoelectronico
                     }
                     ).ToList();
                 var correom = bd.correomasivo.Find(1);
                 
                try
                {
                    MailMessage correo = new MailMessage();//importamos using System.Net.Mail;
                    correo.From = new MailAddress("semillerou678@gmail.com");//Correo para enviar los mensajes
                    foreach (var it in l)
                    {
                        correo.To.Add(it.correo);

                    }
              
                    correo.Subject = correom.asunto;
                    correo.Body = correom.mensaje;
                    correo.IsBodyHtml = true;
                    correo.Priority = MailPriority.Normal;


                    //Configurar del servidor smtp para cada tipo hay diferencia gmail,yahoo etc
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = true;
                    //correo contrasena
                    string sCuentaCorreo = "semillerou678@gmail.com";//correo para enviar correos
                    string sContrasenaCorreo = "12345qwertasd";//contrasena
                                                               //capturar credenciales,luego iniciar conexion
                    smtp.Credentials = new System.Net.NetworkCredential(sCuentaCorreo, sContrasenaCorreo);

                    //Enviar correo
                    smtp.Send(correo);
                    Session["msm"] = "Correo masivo enviado";

                }
                catch (Exception e)
                {
                    ViewBag.error = e;

                }
            }
            



                return Redirect(("~/Sesion/gestionarCorreoMasivo"));
        }
        //////////////////////////// FIN CORREOS /////////////////////////////
        ///////////////////////////INICIO EDITAR USUARIOR POR ADMIN///////////
        ///
        [AuthorizeUser(idOperacion: 12)]
        public ActionResult editarUsuarioA()
        {

            obtenerDatosUsuario();
            menu();

            using (plataformaEntities bd = new plataformaEntities())
            {
                return View(bd.sp_editarUsuariosA().ToList());
            }

        }

        public ActionResult print()
        {
             return new Rotativa.ViewAsPdf("editarUsuarioA");
             
        }
        [AuthorizeUser(idOperacion: 12)]
        public ActionResult datosUsuario(int id)
        {
            obtenerDatosUsuario();
            menu();
            int id2 = id;
            PersonaViewModel model = new PersonaViewModel();
            basica b = null;
            using (plataformaEntities bd = new plataformaEntities())
            {

                b = (from a in bd.persona
                     where a.identificacion == id2
                     select new basica
                     {
                         id = a.idpersona
                     }
                     ).FirstOrDefault();
                var tabla = bd.persona.Find(b.id);

                model.idpersona = tabla.idpersona;
                model.identificacion = tabla.identificacion;
                model.primernombre = tabla.primernombre;
                model.segundonombre = tabla.segundonombre;
                model.primerapellido = tabla.primerapellido;
                model.segundoapellido = tabla.segundoapellido;
                model.telefono = int.Parse(tabla.telefono);
                model.fktipodocumento = tabla.fktipodocumento;
                model.fkacademia = tabla.fkacademia;


            }
            return View(model);
        }
        //datosSesion
        [AuthorizeUser(idOperacion: 12)]
        public ActionResult datosSesionU(int id)
        {
            obtenerDatosUsuario();
            menu();
            int id2 = id;
            SesionViewModel model = new SesionViewModel();
            basica ba = null;
            using (plataformaEntities bd = new plataformaEntities())
            {

                ba = (from a in bd.persona
                      join b in bd.usuario on a.idpersona equals b.fkidepersona
                      where a.identificacion == id2
                      select new basica
                      {
                          id = b.idusuario
                      }
                     ).FirstOrDefault();
                var tabla = bd.usuario.Find(ba.id);

                model.idusuario = tabla.idusuario;
                model.correo = tabla.correoelectronico;
                model.contrasena = tabla.contrasena;
                model.fkidpersona = tabla.fkidepersona;
                model.fkrol = tabla.fkrol;
                model.fkestado = tabla.fkestado;


            }
            return View(model);
        }
        ///////////////////////////FIN   EDITAR USUARIOR POR ADMIN///////////
        ///Asentar Competnecia
        ///
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult reporteAsentarCompetencia(int id)
        {
            obtenerDatosUsuario();
            menu();

            using (plataformaEntities bd = new plataformaEntities())
            {
                 
                ViewBag.id = id;
                var tabla = bd.competencia.Find(id);
                ViewBag.nom = tabla.nombre;
                return View(bd.sp_AsentarCompetencia(id).ToList());
            }
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult GenerarReporteAsentarCompetencia(int id)
        {

            //return Redirect(Url.Content("~/Sesion/reporteAsentarCompetencia/" + id));
            return new Rotativa.ActionAsPdf(Url.Content("reporteAsentarCompetencia/" + id));
            //return new Rotativa.ViewAsPdf(Url.Content("reporteAsentarCompetencia/" + id));
        }
        // return Redirect(Url.Content("~/Sesion/subirResultado/" + Session["eq"]));
        ///////////////Fin asentar competencia
        ///


        [AuthorizeUser(idOperacion: 2)]
        public ActionResult reporteFichas(int id)
        {
            obtenerDatosUsuario();
            menu();
            int? n;
            using (plataformaEntities bd=new plataformaEntities())
            {
                var tabla = bd.competencia.Find(id);
                n = tabla.numeroparticipantes;
            }
            ViewBag.n = n;
                return View();
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult GenerarReportFichas(int id)
        {

            //return Redirect(Url.Content("~/Sesion/reporteAsentarCompetencia/" + id));
            return new Rotativa.ActionAsPdf(Url.Content("reporteFichas/" + id));
            //return new Rotativa.ViewAsPdf(Url.Content("reporteAsentarCompetencia/" + id));
        }
        [AuthorizeUser(idOperacion: 2)]
        public ActionResult reporteSalas(int id)
        {
            obtenerDatosUsuario();
            menu();

            using (plataformaEntities bd = new plataformaEntities())
            {

                 ViewBag.equipo= bd.sp_AsentarCompetencia(id).ToList();
                return View(bd.sp_AsentarCompetencia(id).ToList());
            }
            //foreach (var it in ViewBag.equipo)
            //{

            //}
        }
        [AuthorizeUser(idOperacion: 2)]
        public void generarReporteSalas(int id)
        {
            obtenerDatosUsuario();
            menu();
            int? numero;
            int n=1;
            using (plataformaEntities bd = new plataformaEntities())
            {
                var c = bd.competencia.Find(id);
                numero = c.cantidadejercicios;
                ViewBag.equipo = bd.sp_AsentarCompetencia(id).ToList();
               
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reporte"); //nombre hoja

            ws.Cells["A1"].Value = "ID";
            ws.Cells["B1"].Value = "Equipo";
            ws.Cells["C1"].Value = "Participante1";
            ws.Cells["D1"].Value = "Participante2";
            ws.Cells["E1"].Value = "Sala";
            ws.Cells["F1"].Value = "Nivel";

            List<string> l=new List<string>();
            l.Add("H1");
            l.Add("I1");
            l.Add("J1");
            l.Add("K1");
            l.Add("L1");
            l.Add("M1");
            l.Add("N1");
            l.Add("O1");
            l.Add("P1");
            l.Add("Q1");
            List<string> v = new List<string>();
            v.Add("a");
            v.Add("b");
            v.Add("c");
            v.Add("d");
            v.Add("e");
            v.Add("f");
            v.Add("g");
            v.Add("h");
            v.Add("i");
            v.Add("j");
            for (int i=0;i<numero;i++)
            {
                ws.Cells[l[i]].Value = v[i];
            }


            n = 0;
            int rowStart = 2;
            foreach (var item in ViewBag.equipo)
            {
                if (n%2==0)
                {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.idGrupo;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.NombreEquipo;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.NombreParticipante1+" "+ item.ApellidoParticipante1;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.NombreParticipante2 + " " + item.ApellidoParticipante2;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.NombreSala;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Nivel;
                rowStart++;
                 
                }
                n++;
            }
            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }
        /////////////////////////////INICIO GESTION ROL//////////////////////////////

        [AuthorizeUser(idOperacion: 7)]
        public ActionResult gestionRol()
        {
            obtenerDatosUsuario();
            menu();
            List<RolViewModel> l = null;
            using (plataformaEntities bd=new plataformaEntities())
            {
                l = (from a in bd.rol
                     select new RolViewModel
                     {
                         nombre=a.nombre,
                         idrol=a.idrol,
                         fkestado=a.fkestadorol
                     }                    
                    ).ToList();
            }
            ViewBag.rol = l;
            return View();
        }

        [AuthorizeUser(idOperacion: 7)]
        public ActionResult listarPermisos(int id)
        {
            obtenerDatosUsuario();
            menu();
            Session["p"] = id;
            using (plataformaEntities bd=new plataformaEntities())
            {
                return View(bd.sp_listarPrivilegios(id).ToList());
               
            }
        }
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult desactivarPrivilegio(int id)
        {
            using (plataformaEntities bd=new plataformaEntities())
            {
                bd.sp_desactivarPrivilegio(id);
            }
            return Redirect(Url.Content("~/Sesion/listarPermisos/" + Session["p"]));
        }
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult activarPrivilegio(int id)
        {
            using (plataformaEntities bd = new plataformaEntities())
            {
                bd.sp_activarPrivilegio(id);
            }
            return Redirect(Url.Content("~/Sesion/listarPermisos/" + Session["p"]));
        }
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult listarCambioRol()
        {
            obtenerDatosUsuario();
            menu();
            using (plataformaEntities bd =new plataformaEntities())
            {
                return View(bd.sp_listarUsuario().ToList());
            }

        }
        [AuthorizeUser(idOperacion: 7)]
        public ActionResult cambiarRol(int id)
        {
            obtenerDatosUsuario();
            menu();
            Session["idcambio"] = id;
            List<RolViewModels> l = null;
            using (plataformaEntities bd =new plataformaEntities())
            {
                l = (from a in bd.rol
                     select new RolViewModels
                     {
                         nombre=a.nombre,
                         fkestado=a.fkestadorol,
                         id=a.idrol
                     }
                    ).ToList();
            }
            ViewBag.rol = l;
            return View();
        }
        [AuthorizeUser(idOperacion: 7)]
        [HttpPost]
        public ActionResult cambiarRolUsuario()
        {

            int usu = (int)Session["idcambio"];
            int idr = int.Parse(Request.Form["rol"]);
            using (plataformaEntities bd=new plataformaEntities())
            {
                bd.sp_cambiarRolUsuario(usu,idr);
            }
                return Redirect(Url.Content("~/Sesion/listarCambioRol"));
        }

        /////////////////////////////FIN GESTION ROL//////////////////////////////
    }

}


