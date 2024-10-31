create database autenticacion
go
use autenticacion
go
create table Rol
(
IdRol int primary key identity(1,1),
Descripcion varchar(50)
)
go
insert into Rol(Descripcion) values ('Administrador')
insert into Rol(Descripcion) values ('Asistente')
go
create table Usuarios
(
IdUsuario int primary key identity(1,1),
Nombre varchar(100),
Correo varchar(100),
Clave varbinary(500),
IdRol int references Rol(IdRol)
)
go

create proc SP_AgregarUsuario
(
@Nombre varchar(100),
@Correo varchar(100),
@Clave varchar(20),
@IdRol int,
@Patron varchar(20)
)
as
begin
if not exists (select * from Usuarios where Correo=@Correo)
	begin
		insert into Usuarios(Nombre,Correo,Clave,IdRol) 
		values(@Nombre,@Correo, ENCRYPTBYPASSPHRASE(@Patron,@Clave),@IdRol)
	end
end

go
SP_AgregarUsuario 'Roberto Guerrero','roberto@gmail.com','1234',1,'aut0riz4c10n'
go
SP_AgregarUsuario 'Eduardo Perez','eduardo@gmail.com','1234',1,'aut0riz4c10n'

go
create proc SP_ValidarUsuario
(
@Correo varchar(100),
@Clave varchar(20),
@Patron varchar(20)
)
as
begin
	select * from Usuarios where Correo=@Correo and 
	convert( varchar(20), DECRYPTBYPASSPHRASE(@Patron,Clave))=@Clave
end
go
SP_ValidarUsuario 'roberto@gmail.com','1234','aut0riz4c10n'
