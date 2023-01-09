using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using PristupPodatcima;
using System.Xml;
using System.IO;
using System.Xml.Serialization;


namespace PoslovnaLogika
{
    public class KorisnikServis
    {
        KorisnikDal korisnikDal = new KorisnikDal();

        public List<Korisnik> DajKorisnike()
        {

            return korisnikDal.DajKorisnike();
        }


        public Korisnik DajKorisnika(string korIme)
        {
            return korisnikDal.DajKorisnika(korIme);
        }

        public void AzuriranjeKorisnika(string lozinka, string ime, string prezime, string email, int korID, string korisnickoIme)
        {


            korisnikDal.AzuriranjeKorisnika(lozinka, ime, prezime, email, korID, korisnickoIme);
        }

        public List<Korisnik> DajXmlUListu()
        {
            XmlDocument xmldok = new XmlDocument();
            xmldok.Load(@"C:\Users\s00ivbe\source\repos\zadatak\zadatak\XmlDatoteka\Korisnici.xml");
            XmlElement root = xmldok.DocumentElement;
            XmlNodeList nodes = root.GetElementsByTagName("Korisnik");

            XmlRootAttribute xra = new XmlRootAttribute();
            xra.ElementName = "korisnik";
            XmlSerializer xs = new XmlSerializer(typeof(Korisnik), xra);
            List<Korisnik> korisnici = new List<Korisnik>();
            foreach (XmlNode xmlNode in nodes)
            {
                Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("<korisnik>" + xmlNode.InnerXml.ToString() + "</korisnik>"));
                korisnici.Add((Korisnik)xs.Deserialize(stream));
            }

            return korisnici;




        }


        public void InsertiranjeKorisnika(string lozinka, string ime, string prezime, string email, string korIme, int korId)
        {
            korisnikDal.InsertiranjeKorisnika(lozinka, ime, prezime, email, korIme, korId);
        }

        public Korisnik DajKorisnikaPoKorimeLozinka(string ime, string psw)
        {
            return korisnikDal.DajKorisnikaPoKorimeLozinka(ime, psw);
        }

    }
}

