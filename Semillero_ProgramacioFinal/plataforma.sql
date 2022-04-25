use borrar
drop database plataforma
create database plataforma;
use plataforma;

   
create table plataforma(
id int primary key identity(1,1),
nombre varchar(50),
descripcion varchar(500),
telefono varchar(50),
ubicacion varchar(100),
correo varchar(50)

);

create table tipodocumento(
idtipodocumento int primary key identity(1,1),
nombre varchar(50)
);
create table academia(
idacademia int primary key identity(1,1),
nombre varchar(50)
); 
create table persona(
idpersona int primary key identity(1,1),
identificacion int ,
primernombre varchar(50),
segundonombre varchar(50),
primerapellido varchar(50),
segundoapellido varchar(50),
telefono varchar(50),
fktipodocumento int,
fkacademia int
--UNIQUE (identificacion)
);

create table usuario(
idusuario int primary key identity(1,1),
correoelectronico varchar(50),
contrasena varchar(500),
fkidepersona int,
fkrol int,
fkestado int,
UNIQUE (correoelectronico)
);

create table estadousuario(
idestadousuario int primary key IDENTITY(1,1) ,
nombre varchar(50)
);

create table rol(
idrol int primary key IDENTITY(1,1) ,
nombre varchar(50),
fkestadorol int
);
create table estadorol(
idestadorol int primary key IDENTITY(1,1) ,
nombre varchar(50)
);

create table privilegio(
idprivilegio int primary key IDENTITY(1,1) ,
fkestadoprivilegio int,
fkrol int,
fkmenu int,
);
create table estadoprivilegio(
idestadoprivilegio int primary key identity(1,1),
nombre varchar(50)
);

create table menu(
idmenu int primary key identity(1,1),
nombre varchar(50),
ruta varchar(50),
fkmenu int
);



 create table estadocompetencia(
 idestadocompetencia int primary key identity(1,1),
 nombre varchar(50)
 );
 create table competencia(
 idcompetencia int primary key identity(1,1),
 nombre varchar(50),
 cupos int,
 fechacompetencia date,
 hotainicio time(7),
 horafin time(7),
 cantidadejercicios int,
 numeroparticipantes int, /*Para mostrar los reportes, es calculado con consulta*/
 limiteinscripcion date,
 fkestado int
 );
 create table correoCompetencia(
 idcorreoCompetencia int primary key identity(1,1),
 asunto varchar(100),
 mensaje varchar(1000),
 fkidcompetencia int
 );
 create table correomasivo(
 correomasivo int primary key identity(1,1),
 asunto varchar(100),
 mensaje varchar(1000)
 );

/*Inicio salas*/ 
create table sala(
idsala int primary key identity(1,1),
nombre varchar(50),
cupos int,
fkestado int
);
create table estadosala(
idestadosala int primary key identity(1,1),
nombre varchar(50),
);
create table salacompetencia(
idsalacompetencia int primary key identity(1,1),
nombre varchar(50),
cupos int,
fkidcompetencia int,
fkidsala int,
fkestado int
);
create table estadosalacompetencia(
idestadosalacompetencia int primary key identity(1,1),
nombre varchar(50),
);


alter table correoCompetencia add constraint fkecompete foreign key(fkidcompetencia) references competencia(idcompetencia);
alter table sala add constraint fkestadosala foreign key(fkestado) references estadosala(idestadosala);
alter table salacompetencia add constraint fksalasala foreign key(fkidsala) references sala(idsala);
alter table salacompetencia add constraint fkcompsala foreign key(fkidcompetencia) references competencia(idcompetencia);
alter table salacompetencia add constraint fkestadosalacompetencia foreign key(fkestado) references estadosalacompetencia(idestadosalacompetencia);
/*Fin salas*/ 
  

/*Inicio equipos y lo demas*/

create table estadogrupo(
idestadogrupo int primary key identity(1,1),
nombre varchar(50)
);
create table nivel(
idnivel int primary key identity(1,1),
nombre varchar(50),
descripcion varchar(100)
);
create table grupo(
idgrupo int primary key identity(1,1),
nombre varchar(50),
usuario varchar(50),
contrasena varchar(50),
fkidcompetencia int,
fkidnivel int,
fkidestado int,
);
create table grupopersona(
idgrupopersona int primary key identity(1,1),
puesto int,
fkidpersona int,
fkidgrupo int,
);
create table participacion(
idparticipacion int primary key identity(1,1),
fkidgrupo int,
fkidnivel int,
fkidcompetencia int,
);
create table asignacionsalas(
idasignacionsalas int primary key identity(1,1),
fkidparticipacion int,
fkidsalacompetencia int,
);

alter table grupo add constraint fkcomp foreign key (fkidcompetencia) references competencia(idcompetencia);
alter table grupo add constraint fkidnivv foreign key (fkidnivel) references nivel(idnivel);
alter table grupo add constraint fkidesta foreign key (fkidestado) references estadogrupo(idestadogrupo);

alter table grupopersona add constraint fkidp foreign key (fkidpersona) references persona(idpersona);
alter table grupopersona add constraint fkidg foreign key (fkidgrupo) references grupo(idgrupo);

alter table participacion add constraint fkidgg foreign key (fkidgrupo) references grupo(idgrupo);
alter table participacion add constraint fkidnivev foreign key (fkidnivel) references nivel(idnivel);
alter table participacion add constraint fkidcomp foreign key (fkidcompetencia) references competencia(idcompetencia);

alter table asignacionsalas add constraint fkidpar foreign key (fkidparticipacion) references participacion(idparticipacion);
alter table asignacionsalas add constraint fkidsalas foreign key (fkidsalacompetencia) references salacompetencia(idsalacompetencia);
/*Fin equipos y lo demas*/

/*LLaves principal usuario rol*/
 alter table persona add constraint fkacade foreign key (fkacademia) references academia(idacademia);
 alter table persona add constraint fkiden foreign key (fktipodocumento) references tipodocumento(idtipodocumento);
 alter table usuario add constraint fkper foreign key (fkidepersona) references persona(idpersona);
 alter table usuario add constraint fkestau foreign key (fkestado) references estadousuario(idestadousuario);
  alter table usuario add constraint fkrolle foreign key (fkrol) references rol(idrol);
 
 

 /*llaves competencia*/
 alter table competencia add constraint fkesta foreign key (fkestado) references estadocompetencia(idestadocompetencia);

 /*fin llaves complemento*/

 /*roles,menu*/
alter table privilegio add constraint fkroll foreign key(fkrol) references rol(idrol);
alter table privilegio add constraint fkmenu foreign key(fkmenu) references menu(idmenu);
alter table menu add constraint fksm foreign key(fkmenu) references menu(idmenu);
alter table privilegio add constraint fkestadop foreign key(fkestadoprivilegio) references estadoprivilegio(idestadoprivilegio)
alter table rol add constraint fkrolesta  foreign key (fkestadorol) references estadorol(idestadorol);
/*fin roles menu*/


 
 
  
 
---REGISTROS--- 
 /*academia*/
 insert into academia values('UDLA-Ingenieria de sistemas');
 insert into academia values('UDLA-Ingenieria agroecologica');
 insert into academia values('UDLA-Ingenieria alimentos');
 insert into academia values('Institucion-Normal superior');
 
 /*documento*/
 insert into tipodocumento values('Cedula de ciudadania');
 insert into tipodocumento values('Tarjeta de identidad');
 /*estadousuario*/
 insert into estadousuario values('Activo');
 insert into estadousuario values('Inactivo');
 /*estadorol*/
  insert into estadorol values('Activo');
 insert into estadorol values('Inactivo');
  /*rol*/
 insert into rol values('Administrador',1);
 insert into rol values('Estudiante',1);
 insert into rol values('Secretario',1);
 insert into estadoprivilegio values('Activo');
 insert into estadoprivilegio values('Inactivo');

 --INICION MENU
 insert into menu values('COMPETENCIAS','',null);
 insert into menu values('Gestionar Competencia','/Sesion/gestionarCompetencia',1);
 insert into menu values('Inscripcion competencia','/Sesion/registroACompetencia',1);
 insert into menu values('Historial competencias finalizadas','/Sesion/historialCompetenciaA',1);
 insert into menu values('Historial competencia','/Sesion/historialCompetenciaU',1);



 insert into menu values('PLATAFORMA','',null); 
 insert into menu values('Gestionar Roles','/Sesion/rolesPermisos',6);
 insert into menu values('Gestionar plataforma','/Sesion/gestionarPlataforma',6);
 insert into menu values('Gestionar academia','/Sesion/gestionarAcademia',6);
 insert into menu values('Gestionar documento','/Sesion/gestionarDocumento',6);
 insert into menu values('Gestionar salas','/Sesion/gestionarSala',6);
 insert into menu values('Gestionar usuarios','/Sesion/editarUsuarioA',6);
 insert into menu values('Cambiar rol','/Sesion/listarCambioRol',6);

  select * from menu
 insert into menu values('CORREO','',null); 
 insert into menu values('Gestionar correo masivo','/Sesion/gestionarCorreoMasivo',14);
 insert into menu values('Correo competencia','/Sesion/listarCompetenciasCorreo',14);

  --Usuario


 
 
 select * from menu
 --FIN MENU
 
--INICIO PRIVILEGIO
 insert into privilegio values(1,1,1);
 insert into privilegio values(1,1,2);
 insert into privilegio values(2,1,3);
 insert into privilegio values(1,1,4);
 insert into privilegio values(2,1,5);
 insert into privilegio values(1,1,6);
 insert into privilegio values(1,1,7);
 insert into privilegio values(1,1,8);
 insert into privilegio values(1,1,9);
 insert into privilegio values(1,1,10);
 insert into privilegio values(1,1,11);
 insert into privilegio values(1,1,12);
 insert into privilegio values(1,1,13);
 insert into privilegio values(1,1,14);
 insert into privilegio values(1,1,15);
 insert into privilegio values(1,1,16);
 --insert into privilegio values(1,1,15);
 --USUARIO
 select * from menu

 insert into privilegio values(1,2,1);
 insert into privilegio values(2,2,2);
 insert into privilegio values(1,2,3);
 insert into privilegio values(2,2,4);
 insert into privilegio values(1,2,5);
 insert into privilegio values(2,2,6);
 insert into privilegio values(2,2,7);
 insert into privilegio values(2,2,8);
 insert into privilegio values(2,2,9);
 insert into privilegio values(2,2,10);
 insert into privilegio values(2,2,11);
 insert into privilegio values(2,2,12);
 insert into privilegio values(2,2,13);
 insert into privilegio values(2,2,14);
 insert into privilegio values(2,2,15);
 insert into privilegio values(2,2,16);

 insert into privilegio values(1,3,1);
 insert into privilegio values(2,3,2);
 insert into privilegio values(1,3,3);
 insert into privilegio values(1,3,4);
 insert into privilegio values(1,3,5);
 insert into privilegio values(2,3,6);
 insert into privilegio values(2,3,7);
 insert into privilegio values(2,3,8);
 insert into privilegio values(2,3,9);
 insert into privilegio values(2,3,10);
 insert into privilegio values(2,3,11);
 insert into privilegio values(2,3,12);
 insert into privilegio values(2,3,13);
 insert into privilegio values(2,3,14);
 insert into privilegio values(2,3,15);
 insert into privilegio values(2,2,16);
 SELECT * FROM privilegio
 select * from menu
--FIN PRIVILEGIO


 /*insert into privilegio values(1,3,26);
 insert into privilegio values(1,3,28);*/
  insert into plataforma values('SEMILLERO DE PROGRAMACION','Lorem lorem lorem lorem','312345565','FLORENCIA-CAQUETA','example@gmail.com');
 insert into estadocompetencia values('Activo');
 insert into estadocompetencia values('Finalizado');
 insert into  competencia values('competencia 1',100,'2020-10-10','08:00','12:00',4,0,'2020-10-10',1);

 
 insert into persona values(121121,'Pepe',null,'Perez',null,312121,1,1);
 insert into usuario values('administrador@gmail.com','5994471abb01112afcc18159f6cc74b4f511b99806da59b3caf5a9c173cacfc5',1,1,1);

 insert into correomasivo values('Asunto','Mensaje');



insert into estadosala values('Activo');
insert into estadosala values('Inactivo');

insert into estadosalacompetencia values('Activo');
insert into estadosalacompetencia values('Inactivo');

insert into sala values('Sala 1',20,1);
insert into sala values('Sala 2',20,1);
insert into sala values('Sala 3',20,2);

insert into salacompetencia values('Sala 1',20,1,1,1);

insert into estadogrupo values('Aceptado');
insert into estadogrupo values('Pendiente');
insert into estadogrupo values('Eliminado');

insert into nivel values('Nivel 1','Logica I,II');
insert into nivel values('Nivel 2','Logica II en adelante');

/*INICIO TRIGGER*/
go
CREATE OR ALTER TRIGGER TriggerAgregarSalaCompetencia
ON competencia
FOR INSERT
AS
BEGIN
DECLARE @idcompetencia INT
SET @idcompetencia=(SELECT idcompetencia FROM inserted);
INSERT INTO salacompetencia (nombre,cupos,fkidcompetencia,fkidsala,fkestado)
select sala.nombre,sala.cupos,@idcompetencia,sala.idsala,sala.fkestado FROM sala;

insert into correoCompetencia values('Asunto','Mensaje',@idcompetencia);
END;

go
CREATE OR ALTER TRIGGER TriggerAgregarPrivilegios
ON rol
FOR INSERT
AS
BEGIN
declare @idrol as int;
SET @idrol=(SELECT idrol FROM inserted);
INSERT INTO privilegio (fkestadoprivilegio,fkrol,fkmenu)
select 2,@idrol,menu.idmenu FROM menu;
END;
/*FIN TRIGGER*/

/*INICIO PROCEDIMIENTO*/
go
create or alter procedure sp_agregar_grupo /*agregar un nuevo grupo*/
@nombre varchar(50),
@documento1 int,
@documento2 int,
@idcompetencia int,
@idnivel int,
@usuario varchar(50),
@constrasena varchar(50)
as
begin
declare @idgrupo as int;
declare @idp1 as int;
declare @idp2 as int;
insert into grupo values(@nombre,@usuario,@constrasena,@idcompetencia,@idnivel,2);
set @idgrupo=(select max(grupo.idgrupo) from grupo);
set @idp1=(select persona.idpersona from persona where persona.identificacion=@documento1);
set @idp2=(select persona.idpersona from persona where persona.identificacion=@documento2);
insert into grupopersona values(0,@idp1,@idgrupo);
insert into grupopersona values(0,@idp2,@idgrupo);
end;


go
create or alter procedure sp_listarParaEditar_equipoPropio/*listar equipo de un usuario*/
@idcompetencia int,
@identificacion int
as
begin
select p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.idgrupo as idGrupo,grupo.nombre as NombreEquipo, competencia.nombre as NombreCompetencia,estadogrupo.nombre,nivel.nombre as Nivel from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join nivel on grupo.fkidnivel=nivel.idnivel
inner join (
select primernombre,primerapellido,identificacion,grupo.idgrupo from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 
where p2.identificacion=@identificacion and competencia.idcompetencia=@idcompetencia  and p2.idgrupo=grupo.idgrupo and p2.identificacion!=p1.identificacion and (estadogrupo.idestadogrupo=1 or estadogrupo.idestadogrupo=2)
end;


go
create or alter procedure sp_eliminarEquipo
@idequipo int
as
declare @estadoequipo as int;
declare @idsalacompetencia as int;
declare @competencia as int;
begin
set @estadoequipo=(select grupo.fkidestado from grupo where idgrupo=@idequipo);
if @estadoequipo=1
begin
set @competencia=(
select competencia.idcompetencia from competencia
inner join grupo on grupo.fkidcompetencia=competencia.idcompetencia
where grupo.idgrupo=@idequipo
);
set @idsalacompetencia=(
select salacompetencia.idsalacompetencia from salacompetencia
inner join asignacionsalas on salacompetencia.idsalacompetencia=asignacionsalas.fkidsalacompetencia
inner join participacion on participacion.idparticipacion=asignacionsalas.fkidparticipacion
inner join grupo on grupo.idgrupo=participacion.fkidgrupo
where grupo.idgrupo=@idequipo and salacompetencia.fkidcompetencia=@competencia and participacion.fkidcompetencia=@competencia  
);
update salacompetencia set cupos=cupos+1 where salacompetencia.idsalacompetencia=@idsalacompetencia;
end;
update grupo set fkidestado=3 where idgrupo=@idequipo;

end;


go
create or alter procedure sp_listarSolicitudes
@idcompetencia int
as
begin
select p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.idgrupo as idGrupo,   grupo.nombre as NombreEquipo, competencia.nombre as NombreCompetencia,estadogrupo.nombre from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join (
select primernombre,primerapellido,identificacion,grupo.idgrupo from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 
where competencia.idcompetencia=@idcompetencia  and p2.idgrupo=grupo.idgrupo  and p2.identificacion!=p1.identificacion and (estadogrupo.idestadogrupo=2) 
end;


go
create or alter procedure sp_responderSolicitudCompetencia
@idgrupo int
as
begin
declare @idcompetencia as int;
declare @cupos as int;
declare @nivel as int;
declare @idsalacompetencia as int;
declare @idparticipacion as int;
set @idcompetencia=(
select competencia.idcompetencia from competencia
inner join grupo on competencia.idcompetencia=grupo.fkidcompetencia
where grupo.idgrupo=@idgrupo
);
set @cupos=(
select competencia.cupos from competencia
where competencia.idcompetencia=@idcompetencia
);
if @cupos>=1
begin
 
set @nivel=(select grupo.fkidnivel from grupo where grupo.idgrupo=@idgrupo);
insert into participacion values(@idgrupo,@nivel,@idcompetencia);

set @idsalacompetencia=(
select top 1 salacompetencia.idsalacompetencia from salacompetencia
inner join competencia on competencia.idcompetencia=salacompetencia.fkidcompetencia
where competencia.idcompetencia=@idcompetencia and salacompetencia.cupos>=1
); 
set @idparticipacion=(select max(participacion.idparticipacion) from participacion);
print @idsalacompetencia
insert into asignacionsalas values(@idparticipacion,@idsalacompetencia);

update salacompetencia set cupos=cupos-1 where idsalacompetencia=@idsalacompetencia and cupos>=1;
update grupo set fkidestado=1 where grupo.idgrupo=@idgrupo;
update competencia set cupos=cupos-1 where idcompetencia=@idcompetencia and cupos>=1
end

end;



go
create or alter procedure sp_negarSolicitud
@ide int
as
begin
update grupo set fkidestado=3 where idgrupo=@ide
end;





go
create or alter procedure sp_listarGruposEnSala
@idcompetencia int
as
begin
select  distinct grupo.idgrupo as   idGrupo,p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.nombre as NombreEquipo,estadogrupo.nombre as EstadoEquipo,p3.nombre as NombreSala, competencia.idcompetencia as idCompetencia, p3.idsalacompetencia as idsalacompetencia,nivel.nombre as Nivel from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join nivel on grupo.fkidnivel=nivel.idnivel
inner join (
select distinct grupo.idgrupo, primernombre,primerapellido,identificacion from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 
inner join(
select distinct participacion.fkidgrupo, salacompetencia.idsalacompetencia,salacompetencia.nombre,grupo.idgrupo,salacompetencia.fkidcompetencia as salafkidcompetencia,participacion.fkidcompetencia as participacionfkidcompetencia  from salacompetencia
inner join asignacionsalas on salacompetencia.idsalacompetencia=asignacionsalas.fkidsalacompetencia
inner join participacion on asignacionsalas.fkidparticipacion=participacion.idparticipacion	
inner join grupo on grupo.idgrupo=participacion.fkidgrupo
) as p3 on p2.idgrupo=p3.idgrupo 
where competencia.idcompetencia=@idcompetencia  and p2.idgrupo=grupo.idgrupo  and p2.identificacion!=p1.identificacion and (estadogrupo.idestadogrupo=1)  and p3.salafkidcompetencia=@idcompetencia and p3.participacionfkidcompetencia=@idcompetencia and (p2.idgrupo=p3.fkidgrupo)
order by   p3.idsalacompetencia	
end;


go
create or alter procedure sp_cambiarDeSala
@idgrupo int,
@idsala int
as
begin
declare @idsalacompetenciagrupo as int;
declare @cupos as int;
declare @idasignacion as int;
set  @idsalacompetenciagrupo=(select top 1 salacompetencia.idsalacompetencia from salacompetencia
	inner join asignacionsalas on salacompetencia.idsalacompetencia=asignacionsalas.fkidsalacompetencia
	inner join participacion on asignacionsalas.fkidparticipacion=participacion.idparticipacion
	where participacion.fkidgrupo=@idgrupo
 );
if  @idsalacompetenciagrupo!=@idsala
begin
set @cupos =(select salacompetencia.cupos from salacompetencia where salacompetencia.idsalacompetencia=@idsala);
if @cupos>=1
begin
set @idasignacion=(select top 1 asignacionsalas.idasignacionsalas from asignacionsalas
	inner join participacion on participacion.idparticipacion=asignacionsalas.fkidparticipacion
	where participacion.fkidgrupo=@idgrupo
);
 
 update asignacionsalas set fkidsalacompetencia=@idsala where asignacionsalas.idasignacionsalas=@idasignacion;
 update salacompetencia set cupos=cupos+1 where salacompetencia.idsalacompetencia=@idsalacompetenciagrupo 
 update salacompetencia set cupos=cupos-1 where salacompetencia.idsalacompetencia=@idsala;
 
end;
end;
end;


go
create or alter procedure sp_listarGruposSubirResultado
@idcompetencia int
as
begin
select    grupo.idgrupo as idGrupo,p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.nombre as NombreEquipo,estadogrupo.nombre as EstadoEquipo,p3.nombre as NombreSala, competencia.idcompetencia as idCompetencia, p3.idsalacompetencia as idsalacompetencia,nivel.nombre as Nivel,grupopersona.puesto from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join nivel on grupo.fkidnivel=nivel.idnivel
inner join (
select   grupo.idgrupo, primernombre,primerapellido,identificacion from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 
inner join(
select   participacion.fkidgrupo, salacompetencia.idsalacompetencia,salacompetencia.nombre,grupo.idgrupo,salacompetencia.fkidcompetencia as salafkidcompetencia,participacion.fkidcompetencia as participacionfkidcompetencia  from salacompetencia
inner join asignacionsalas on salacompetencia.idsalacompetencia=asignacionsalas.fkidsalacompetencia
inner join participacion on asignacionsalas.fkidparticipacion=participacion.idparticipacion	
inner join grupo on grupo.idgrupo=participacion.fkidgrupo
) as p3 on p2.idgrupo=p3.idgrupo 
where competencia.idcompetencia=@idcompetencia  and (p2.idgrupo=grupo.idgrupo  and p2.identificacion!=p1.identificacion) and (estadogrupo.idestadogrupo=1)  and p3.salafkidcompetencia=@idcompetencia and p3.participacionfkidcompetencia=@idcompetencia and (p2.idgrupo=p3.fkidgrupo)
order by   nivel.idnivel	
end;


go
create or alter procedure sp_subirResultado
@idgrupo int,
@puesto int
as
begin
update grupopersona set puesto=@puesto where grupopersona.fkidgrupo=@idgrupo
end;



go
create or alter procedure sp_historialCompetenciaU

@idcompetencia int
as
begin
select  distinct  grupo.idgrupo as idGrupo,p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.nombre as NombreEquipo,estadogrupo.nombre as EstadoEquipo, competencia.idcompetencia as idCompetencia, nivel.nombre as Nivel,grupopersona.puesto from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join nivel on grupo.fkidnivel=nivel.idnivel
inner join (
select  distinct grupo.idgrupo, primernombre,primerapellido,identificacion from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 

where   p2.idgrupo=grupo.idgrupo  and p2.identificacion!=p1.identificacion and estadogrupo.idestadogrupo=1   and competencia.fkestado=2 and competencia.idcompetencia=@idcompetencia
order by   grupopersona.puesto
end;



go
create or alter procedure sp_competenciasU
@identificacion int
as
begin
select competencia.idcompetencia,competencia.nombre as Competencia,competencia.fechacompetencia,estadocompetencia.nombre as EstadoCompetencia from competencia
inner join estadocompetencia on competencia.fkestado=estadocompetencia.idestadocompetencia
inner join grupo on competencia.idcompetencia=grupo.fkidcompetencia
inner join grupopersona on grupo.idgrupo=grupopersona.fkidgrupo
inner join persona on grupopersona.fkidpersona=persona.idpersona
where persona.identificacion=@identificacion and estadocompetencia.idestadocompetencia=2;
end;


go
create or alter procedure sp_competenciasA
as
begin
select competencia.idcompetencia,competencia.nombre as Competencia,competencia.fechacompetencia,estadocompetencia.nombre as EstadoCompetencia from competencia
inner join estadocompetencia on competencia.fkestado=estadocompetencia.idestadocompetencia
where   estadocompetencia.idestadocompetencia=2;
end;


go
create or alter procedure sp_historialCompetenciaA

@idcompetencia int
as
begin
select  distinct  grupo.idgrupo as idGrupo,p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.nombre as NombreEquipo,estadogrupo.nombre as EstadoEquipo, competencia.idcompetencia as idCompetencia, nivel.nombre as Nivel,grupopersona.puesto from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join nivel on grupo.fkidnivel=nivel.idnivel
inner join (
select  distinct grupo.idgrupo, primernombre,primerapellido,identificacion from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 

where   p2.idgrupo=grupo.idgrupo  and p2.identificacion!=p1.identificacion and estadogrupo.idestadogrupo=1   and competencia.fkestado=2 and competencia.idcompetencia=@idcompetencia
order by   grupopersona.puesto
end;

go
create or alter procedure sp_listarCompetenciasActivasCorreo
as
begin
select competencia.idcompetencia,competencia.nombre,competencia.fechacompetencia,correoCompetencia.idcorreoCompetencia from competencia
inner join correoCompetencia on competencia.idcompetencia=correoCompetencia.fkidcompetencia 
where competencia.fkestado=1
end;

go
create or alter procedure sp_editarUsuariosA
as
begin
select persona.idpersona,persona.identificacion,persona.primernombre,persona.primerapellido,usuario.correoelectronico from persona 
inner join usuario on persona.idpersona=usuario.fkidepersona

end;

go
create or alter procedure sp_AsentarCompetencia
@idcompetencia int
as
begin
select    grupo.idgrupo as idGrupo,p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.nombre as NombreEquipo,estadogrupo.nombre as EstadoEquipo,p3.nombre as NombreSala, competencia.idcompetencia as idCompetencia, p3.idsalacompetencia as idsalacompetencia,nivel.nombre as Nivel,grupopersona.puesto from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join nivel on grupo.fkidnivel=nivel.idnivel
inner join (
select   grupo.idgrupo, primernombre,primerapellido,identificacion from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 
inner join(
select   participacion.fkidgrupo, salacompetencia.idsalacompetencia,salacompetencia.nombre,grupo.idgrupo,salacompetencia.fkidcompetencia as salafkidcompetencia,participacion.fkidcompetencia as participacionfkidcompetencia  from salacompetencia
inner join asignacionsalas on salacompetencia.idsalacompetencia=asignacionsalas.fkidsalacompetencia
inner join participacion on asignacionsalas.fkidparticipacion=participacion.idparticipacion	
inner join grupo on grupo.idgrupo=participacion.fkidgrupo
) as p3 on p2.idgrupo=p3.idgrupo 
where competencia.idcompetencia=@idcompetencia  and (p2.idgrupo=grupo.idgrupo  and p2.identificacion!=p1.identificacion) and (estadogrupo.idestadogrupo=1)  and p3.salafkidcompetencia=@idcompetencia and p3.participacionfkidcompetencia=@idcompetencia and (p2.idgrupo=p3.fkidgrupo)
order by   nivel.idnivel	
end;

go
create or alter procedure sp_AsentarCompetencia
@idcompetencia int
as
begin
select    grupo.idgrupo as idGrupo,p1.primernombre as NombreParticipante1,p1.primerapellido as ApellidoParticipante1,p1.identificacion as identificacion1,p2.primernombre as NombreParticipante2,p2.primerapellido as ApellidoParticipante2,p2.identificacion as identificacion2,grupo.nombre as NombreEquipo,estadogrupo.nombre as EstadoEquipo,p3.nombre as NombreSala, competencia.idcompetencia as idCompetencia, p3.idsalacompetencia as idsalacompetencia,nivel.nombre as Nivel,grupopersona.puesto from persona as p1 
inner join grupopersona on p1.idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
inner join estadogrupo on grupo.fkidestado=estadogrupo.idestadogrupo
inner join nivel on grupo.fkidnivel=nivel.idnivel
inner join (
select   grupo.idgrupo, primernombre,primerapellido,identificacion from persona  
 inner join grupopersona on idpersona = grupopersona.fkidpersona
inner join grupo on grupopersona.fkidgrupo=grupo.idgrupo
inner join competencia on grupo.fkidcompetencia=competencia.idcompetencia
) as p2 on p2.idgrupo=grupo.idgrupo 
inner join(
select   participacion.fkidgrupo, salacompetencia.idsalacompetencia,salacompetencia.nombre,grupo.idgrupo,salacompetencia.fkidcompetencia as salafkidcompetencia,participacion.fkidcompetencia as participacionfkidcompetencia  from salacompetencia
inner join asignacionsalas on salacompetencia.idsalacompetencia=asignacionsalas.fkidsalacompetencia
inner join participacion on asignacionsalas.fkidparticipacion=participacion.idparticipacion	
inner join grupo on grupo.idgrupo=participacion.fkidgrupo
) as p3 on p2.idgrupo=p3.idgrupo 
where competencia.idcompetencia=@idcompetencia  and (p2.idgrupo=grupo.idgrupo  and p2.identificacion!=p1.identificacion) and (estadogrupo.idestadogrupo=1)  and p3.salafkidcompetencia=@idcompetencia and p3.participacionfkidcompetencia=@idcompetencia and (p2.idgrupo=p3.fkidgrupo)
order by   idsalacompetencia;
end;


go
create or alter procedure sp_listarPrivilegios
@idrol int
as
begin
select menu.nombre,privilegio.idprivilegio,privilegio.fkestadoprivilegio from menu 
inner join privilegio on menu.idmenu=privilegio.fkmenu
inner join rol on privilegio.fkrol=rol.idrol
where rol.idrol=@idrol
order by privilegio.fkestadoprivilegio 
end;

go 
create or alter procedure sp_activarPrivilegio
@idp int
as
begin
update privilegio set fkestadoprivilegio=1 where idprivilegio=@idp;
end;
 

go 
create or alter procedure sp_desactivarPrivilegio
@idp int
as
begin
update privilegio set fkestadoprivilegio=2 where idprivilegio=@idp;
end;


go
create or alter procedure sp_listarUsuario
as
begin
select persona.identificacion,persona.primernombre,persona.primerapellido,usuario.correoelectronico,rol.nombre,rol.idrol from persona
inner join usuario on persona.idpersona=usuario.fkidepersona
inner join rol on rol.idrol=usuario.fkrol
end;

go
create or alter procedure sp_cambiarRolUsuario
@identificacion int,
@idrol int
as
begin
declare  @idusuario as int
set @idusuario=(
select usuario.idusuario from usuario inner join
persona on usuario.fkidepersona=persona.idpersona
where persona.identificacion=@identificacion
)
update usuario set fkrol=@idrol where usuario.idusuario=@idusuario
end;

/*FIN PROCEDIMIENTO*/
 
--insert into  competencia values('competencia 1',100,'2020-10-10','08:00','12:00',4,0,'2020-10-10',1);