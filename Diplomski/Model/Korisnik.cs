using System;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace Model
{
    [Serializable]
    public class Korisnik
    {
        [XmlElement(ElementName = "korisnik_id")]
        public int KorisnikId { get; set; }

        [XmlElement(ElementName = "korisnicko_ime")]
        public string KorisnickoIme { get; set; }

        [XmlElement(ElementName = "ime")]
        public string Ime { get; set; }

        [XmlElement(ElementName = "prezime")]
        public string Prezime { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlElement(ElementName = "lozinka")]
        public string Lozinka { get; set; }


        public Korisnik() { }
    }
}
























