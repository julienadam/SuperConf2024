drop table inscription;
go

create table inscription
(
	InscriptionId int identity not null,
	Email nvarchar(max) not null,
	Nom nvarchar(max) not null,
	Prenom nvarchar(max) not null,
	DateNaissance datetime not null,
	Nbjours int not null,
	ChoixRepas varchar(20) not null,
	DemandeParticuliere nvarchar(180) null,
	Soiree bit not null,
	CONSTRAINT PK_Inscription PRIMARY KEY (InscriptionId),
	CONSTRAINT CK_ChoixRepas CHECK (ChoixRepas in ('Vegetarien', 'Vegan', 'Standard')),
	CONSTRAINT CK_Nbjours CHECK (NbJours >= 2 AND NbJours <=3)
);
go