﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Semillero_ProgramacioFinal.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class plataformaEntities : DbContext
    {
        public plataformaEntities()
            : base("name=plataformaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<academia> academia { get; set; }
        public virtual DbSet<asignacionsalas> asignacionsalas { get; set; }
        public virtual DbSet<competencia> competencia { get; set; }
        public virtual DbSet<correoCompetencia> correoCompetencia { get; set; }
        public virtual DbSet<correomasivo> correomasivo { get; set; }
        public virtual DbSet<estadocompetencia> estadocompetencia { get; set; }
        public virtual DbSet<estadogrupo> estadogrupo { get; set; }
        public virtual DbSet<estadoprivilegio> estadoprivilegio { get; set; }
        public virtual DbSet<estadorol> estadorol { get; set; }
        public virtual DbSet<estadosala> estadosala { get; set; }
        public virtual DbSet<estadosalacompetencia> estadosalacompetencia { get; set; }
        public virtual DbSet<estadousuario> estadousuario { get; set; }
        public virtual DbSet<grupo> grupo { get; set; }
        public virtual DbSet<grupopersona> grupopersona { get; set; }
        public virtual DbSet<menu> menu { get; set; }
        public virtual DbSet<nivel> nivel { get; set; }
        public virtual DbSet<participacion> participacion { get; set; }
        public virtual DbSet<persona> persona { get; set; }
        public virtual DbSet<plataforma> plataforma { get; set; }
        public virtual DbSet<privilegio> privilegio { get; set; }
        public virtual DbSet<rol> rol { get; set; }
        public virtual DbSet<sala> sala { get; set; }
        public virtual DbSet<salacompetencia> salacompetencia { get; set; }
        public virtual DbSet<tipodocumento> tipodocumento { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
    
        public virtual int sp_activarPrivilegio(Nullable<int> idp)
        {
            var idpParameter = idp.HasValue ?
                new ObjectParameter("idp", idp) :
                new ObjectParameter("idp", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_activarPrivilegio", idpParameter);
        }
    
        public virtual int sp_agregar_grupo(string nombre, Nullable<int> documento1, Nullable<int> documento2, Nullable<int> idcompetencia, Nullable<int> idnivel, string usuario, string constrasena)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var documento1Parameter = documento1.HasValue ?
                new ObjectParameter("documento1", documento1) :
                new ObjectParameter("documento1", typeof(int));
    
            var documento2Parameter = documento2.HasValue ?
                new ObjectParameter("documento2", documento2) :
                new ObjectParameter("documento2", typeof(int));
    
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            var idnivelParameter = idnivel.HasValue ?
                new ObjectParameter("idnivel", idnivel) :
                new ObjectParameter("idnivel", typeof(int));
    
            var usuarioParameter = usuario != null ?
                new ObjectParameter("usuario", usuario) :
                new ObjectParameter("usuario", typeof(string));
    
            var constrasenaParameter = constrasena != null ?
                new ObjectParameter("constrasena", constrasena) :
                new ObjectParameter("constrasena", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_agregar_grupo", nombreParameter, documento1Parameter, documento2Parameter, idcompetenciaParameter, idnivelParameter, usuarioParameter, constrasenaParameter);
        }
    
        public virtual ObjectResult<sp_AsentarCompetencia_Result> sp_AsentarCompetencia(Nullable<int> idcompetencia)
        {
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_AsentarCompetencia_Result>("sp_AsentarCompetencia", idcompetenciaParameter);
        }
    
        public virtual int sp_cambiarDeSala(Nullable<int> idgrupo, Nullable<int> idsala)
        {
            var idgrupoParameter = idgrupo.HasValue ?
                new ObjectParameter("idgrupo", idgrupo) :
                new ObjectParameter("idgrupo", typeof(int));
    
            var idsalaParameter = idsala.HasValue ?
                new ObjectParameter("idsala", idsala) :
                new ObjectParameter("idsala", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_cambiarDeSala", idgrupoParameter, idsalaParameter);
        }
    
        public virtual int sp_cambiarRolUsuario(Nullable<int> identificacion, Nullable<int> idrol)
        {
            var identificacionParameter = identificacion.HasValue ?
                new ObjectParameter("identificacion", identificacion) :
                new ObjectParameter("identificacion", typeof(int));
    
            var idrolParameter = idrol.HasValue ?
                new ObjectParameter("idrol", idrol) :
                new ObjectParameter("idrol", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_cambiarRolUsuario", identificacionParameter, idrolParameter);
        }
    
        public virtual ObjectResult<sp_competenciasA_Result> sp_competenciasA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_competenciasA_Result>("sp_competenciasA");
        }
    
        public virtual ObjectResult<sp_competenciasU_Result> sp_competenciasU(Nullable<int> identificacion)
        {
            var identificacionParameter = identificacion.HasValue ?
                new ObjectParameter("identificacion", identificacion) :
                new ObjectParameter("identificacion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_competenciasU_Result>("sp_competenciasU", identificacionParameter);
        }
    
        public virtual int sp_desactivarPrivilegio(Nullable<int> idp)
        {
            var idpParameter = idp.HasValue ?
                new ObjectParameter("idp", idp) :
                new ObjectParameter("idp", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_desactivarPrivilegio", idpParameter);
        }
    
        public virtual ObjectResult<sp_editarUsuariosA_Result> sp_editarUsuariosA()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_editarUsuariosA_Result>("sp_editarUsuariosA");
        }
    
        public virtual int sp_eliminarEquipo(Nullable<int> idequipo)
        {
            var idequipoParameter = idequipo.HasValue ?
                new ObjectParameter("idequipo", idequipo) :
                new ObjectParameter("idequipo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_eliminarEquipo", idequipoParameter);
        }
    
        public virtual ObjectResult<sp_historialCompetenciaA_Result> sp_historialCompetenciaA(Nullable<int> idcompetencia)
        {
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_historialCompetenciaA_Result>("sp_historialCompetenciaA", idcompetenciaParameter);
        }
    
        public virtual ObjectResult<sp_historialCompetenciaU_Result> sp_historialCompetenciaU(Nullable<int> idcompetencia)
        {
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_historialCompetenciaU_Result>("sp_historialCompetenciaU", idcompetenciaParameter);
        }
    
        public virtual ObjectResult<sp_listarCompetenciasActivasCorreo_Result> sp_listarCompetenciasActivasCorreo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarCompetenciasActivasCorreo_Result>("sp_listarCompetenciasActivasCorreo");
        }
    
        public virtual ObjectResult<sp_listarGruposEnSala_Result> sp_listarGruposEnSala(Nullable<int> idcompetencia)
        {
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarGruposEnSala_Result>("sp_listarGruposEnSala", idcompetenciaParameter);
        }
    
        public virtual ObjectResult<sp_listarGruposSubirResultado_Result> sp_listarGruposSubirResultado(Nullable<int> idcompetencia)
        {
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarGruposSubirResultado_Result>("sp_listarGruposSubirResultado", idcompetenciaParameter);
        }
    
        public virtual ObjectResult<sp_listarParaEditar_equipoPropio_Result> sp_listarParaEditar_equipoPropio(Nullable<int> idcompetencia, Nullable<int> identificacion)
        {
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            var identificacionParameter = identificacion.HasValue ?
                new ObjectParameter("identificacion", identificacion) :
                new ObjectParameter("identificacion", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarParaEditar_equipoPropio_Result>("sp_listarParaEditar_equipoPropio", idcompetenciaParameter, identificacionParameter);
        }
    
        public virtual ObjectResult<sp_listarPrivilegios_Result> sp_listarPrivilegios(Nullable<int> idrol)
        {
            var idrolParameter = idrol.HasValue ?
                new ObjectParameter("idrol", idrol) :
                new ObjectParameter("idrol", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarPrivilegios_Result>("sp_listarPrivilegios", idrolParameter);
        }
    
        public virtual ObjectResult<sp_listarSolicitudes_Result> sp_listarSolicitudes(Nullable<int> idcompetencia)
        {
            var idcompetenciaParameter = idcompetencia.HasValue ?
                new ObjectParameter("idcompetencia", idcompetencia) :
                new ObjectParameter("idcompetencia", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarSolicitudes_Result>("sp_listarSolicitudes", idcompetenciaParameter);
        }
    
        public virtual ObjectResult<sp_listarUsuario_Result> sp_listarUsuario()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_listarUsuario_Result>("sp_listarUsuario");
        }
    
        public virtual int sp_negarSolicitud(Nullable<int> ide)
        {
            var ideParameter = ide.HasValue ?
                new ObjectParameter("ide", ide) :
                new ObjectParameter("ide", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_negarSolicitud", ideParameter);
        }
    
        public virtual int sp_responderSolicitudCompetencia(Nullable<int> idgrupo)
        {
            var idgrupoParameter = idgrupo.HasValue ?
                new ObjectParameter("idgrupo", idgrupo) :
                new ObjectParameter("idgrupo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_responderSolicitudCompetencia", idgrupoParameter);
        }
    
        public virtual int sp_subirResultado(Nullable<int> idgrupo, Nullable<int> puesto)
        {
            var idgrupoParameter = idgrupo.HasValue ?
                new ObjectParameter("idgrupo", idgrupo) :
                new ObjectParameter("idgrupo", typeof(int));
    
            var puestoParameter = puesto.HasValue ?
                new ObjectParameter("puesto", puesto) :
                new ObjectParameter("puesto", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_subirResultado", idgrupoParameter, puestoParameter);
        }
    }
}
