Use master;
Go
Drop Database Proyecto
Go

Create database Proyecto
use Proyecto

CREATE TABLE Pais (
	Id_Pais int identity(1,1) primary key,
	Nombre varchar(80) DEFAULT NULL,
	Abrev varchar(3) NOT NULL,
) 
	
Create Table Jugador
(
	Id_Jugador int identity(1,1) Primary key,
	Nombres_Jugador varchar(20) not null,
	Apellidos_Jugador varchar(20) not null,
	Nombre_Usuario varchar(20) not null,
	Id_Pais int not null,
	Correo varchar(50) not null,
	Pass varchar(100) not null,
	Confirm_Pass varchar(100) not null,
	Id_Torneo int,
	constraint FK_Pais foreign key (Id_Pais) references Pais(Id_Pais),
); 

Create Table Bloqueo
(
	Id_Bloqueo int identity(1,1) Primary key,
	Id_Jugador int not null,
	Id_Bloqueado int not null
	constraint FK_Jugador foreign key (Id_Jugador) references Jugador(Id_Jugador),
	constraint FK_Bloqueado foreign key (Id_Bloqueado) references Jugador(Id_Jugador)
);

Create Table Torneo
(
	Id_Torneo int identity(1,1) Primary key,
	Nombre_Torneo varchar(20) not null,
	Tipo varchar(20) not null
		Check(Tipo in('4', '8', '16')),
	Fecha_Inic date not null,
	Id_Partida int not null
);

Create Table Partida
(
	Id_Partida int identity(1,1) Primary key,
	Tipo varchar(20)
		Check(Tipo in('Solitario', 'Multijugador')),
	Id_Jugador1 int not null,
	Id_Torneo int not null,
	Punteo int,
	Movimiento int,
	Ganador varchar(20),
	constraint FK_Jugador1 foreign key (Id_Jugador1) references Jugador(Id_Jugador),
);

Truncate Table Jugador;
select*from Jugador
select*from Partida

select*from Pais

	