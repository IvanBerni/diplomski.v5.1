

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