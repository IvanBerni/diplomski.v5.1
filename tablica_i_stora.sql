use master

create table [dbo].[korisnik] (
[korisnik_id]   INT   IDENTITY (1,1) NOT NULL,
[korisniko_ime] VARCHAR (50)  NOT NULL,
[lozinka]       NVARCHAR (256) NOT NULL,
[ime]           VARCHAR (50) NULL,
[prezime]       VARCHAR (50) NULL,
[email]         VARCHAR (50) NULL,
CONSTRAINT [pk_korisnik] PRIMARY KEY CLUSTERED ([korisnik_id] ASC)
);

GO

 

CREATE PROCEDURE InsertirajXML

 

@xml XML

 

AS

BEGIN

SET NOCOUNT ON;

 

INSERT INTO dbo.korisnik

select

 

--korisnik.value('@Id','INT') AS Id,

korisnik.value('@korisnik_id','INT') as KorisnikId,

korisnik.value('(@korisnicko_ime/text()) [1]','VARCHAR(100)') as KorisnickoIme,

korisnik.value('(@ime/text()) [1]','varchar(100)') as Ime,

korisnik.value('(@prezime/text()) [1]','varchar(100)') as Prezime,

korisnik.value('(@email/text()) [1]','varchar(100)') as Email,

korisnik.value('(@lozinka/text() [1]','varchar(100)') as Lozinka

 

from @xml.nodes('/korisnici/korisnik') as TEMPTABLE (korisnik)

 

END