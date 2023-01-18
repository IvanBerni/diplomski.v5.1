using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Sql;
using PristupPodatcima;

namespace PoslovnaLogika
{
    public class XmlServis
    {
       

        public class XmlComparerId : IEqualityComparer<Korisnik>
        {
            bool IEqualityComparer<Korisnik>.Equals(Korisnik x, Korisnik y)
            {
                return (Equals(x.KorisnikId, y.KorisnikId));
                throw new NotImplementedException();
            }

            int IEqualityComparer<Korisnik>.GetHashCode(Korisnik korisnik)
            {
                return korisnik.KorisnikId.GetHashCode();
                throw new NotImplementedException();
            }

        }
        public class XmlComparer : IEqualityComparer<Korisnik>
        {
            bool IEqualityComparer<Korisnik>.Equals(Korisnik x, Korisnik y)
            {
                return (Equals(x.KorisnikId, y.KorisnikId) && Equals(x.KorisnickoIme, y.KorisnickoIme)
                    && Equals(x.Lozinka, y.Lozinka) && Equals(x.Ime, y.Ime) && Equals(x.Prezime, y.Prezime) && Equals(x.Email, y.Email));
                throw new NotImplementedException();
            }

            int IEqualityComparer<Korisnik>.GetHashCode(Korisnik korisnik)
            {
                return korisnik.KorisnikId.GetHashCode();
                throw new NotImplementedException();
            }


        }

        public static XmlDocument KreiraXmlListuRazlike(List<Korisnik> kor)
        {
            XmlDocument xmldok = new XmlDocument();
            XmlNode rootnode = xmldok.CreateElement("korisnici");
            xmldok.AppendChild(rootnode);

            foreach (Korisnik korisnik in kor)
            {

                XmlNode kornode = xmldok.CreateElement("korisnik");
                rootnode.AppendChild(kornode);

                var kor_id = xmldok.CreateElement("korisnik_id", korisnik.KorisnikId.ToString());
                kornode.AppendChild(kor_id);
                var kor_ime = xmldok.CreateElement("korisnicko_ime", korisnik.KorisnickoIme);
                kornode.AppendChild(kor_ime);
                var loz = xmldok.CreateElement("lozinka", korisnik.Lozinka);
                kornode.AppendChild(loz);
                var ime = xmldok.CreateElement("ime", korisnik.Ime);
                kornode.AppendChild(ime);
                var prez = xmldok.CreateElement("prezime", korisnik.Prezime);
                kornode.AppendChild(prez);
                var email = xmldok.CreateElement("email", korisnik.Email);
                kornode.AppendChild(email);

            }

            return xmldok;


        }

        public static void XmlCompare(string putanja1,string putanja2)
        {
            XmlDocument xmldok1 = new XmlDocument();
            XmlDocument xmldok2 = new XmlDocument();

            List<Korisnik> korisnici1 = new List<Korisnik>();
            List<Korisnik> korisnici2 = new List<Korisnik>();


            xmldok1.Load(putanja1);
            XmlElement root = xmldok1.DocumentElement;
            XmlNodeList nodes = root.GetElementsByTagName("Korisnik");
            XmlRootAttribute xra = new XmlRootAttribute();
            xra.ElementName = "korisnik";
            XmlSerializer xs = new XmlSerializer(typeof(Korisnik), xra);


            foreach (XmlNode xmlNode in nodes)
            {
                Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("<korisnik>" + xmlNode.InnerXml.ToString() + "</korisnik>"));
                korisnici1.Add((Korisnik)xs.Deserialize(stream));
            }

            xmldok2.Load(putanja2);
            XmlElement root2 = xmldok2.DocumentElement;
            XmlNodeList nodes2 = root2.GetElementsByTagName("Korisnik");
            XmlRootAttribute xra2 = new XmlRootAttribute();
            xra2.ElementName = "korisnik";
            XmlSerializer xs2 = new XmlSerializer(typeof(Korisnik), xra2);

            foreach (XmlNode xmlNode in nodes2)
            {
                Stream stream = new MemoryStream(Encoding.UTF8.GetBytes("<korisnik>" + xmlNode.InnerXml.ToString() + "</korisnik>"));
                korisnici2.Add((Korisnik)xs.Deserialize(stream));
            }


            ////////sve iz liste A u listi B ///////////

            XmlComparer comparer = new XmlComparer();
            List<Korisnik> imaAnemaB = korisnici1.Except(korisnici2, comparer).ToList();// ima A nema B
            List<Korisnik> imaBnemaA = korisnici2.Except(korisnici1, comparer).ToList(); // ima B nema A
            List<Korisnik> sviRazlicitiKorisnici = imaAnemaB.Union(imaBnemaA).Distinct<Korisnik>(new XmlComparerId()).ToList();
            List<Korisnik> sviRazlicitiKorisniciPoElementima = imaAnemaB.Union(imaBnemaA).Distinct<Korisnik>(new XmlComparer()).ToList();


            int kolko = sviRazlicitiKorisnici.Count();
            if (kolko != 0)
            {
                XmlDocument razdok = KreiraXmlListuRazlike(sviRazlicitiKorisnici);
                razdok.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\Različiti_iz_AB_po_IDu.xml");
                Console.WriteLine("Kreiran XML sa svim razlikama");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("nema razlike između A i B po svim kriterijima");
                Console.ReadKey();
            }



            /////////////////sviRazlicitiKorisniciPoElementima////////////////

            int kolkoima = sviRazlicitiKorisniciPoElementima.Count();
            if (kolkoima != 0)
            {
                XmlDocument razdok = KreiraXmlListuRazlike(sviRazlicitiKorisniciPoElementima);
                razdok.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\Različiti_iz_AB_po_elementu.xml");
                Console.WriteLine("Kreira XML sa svim različitim iz A i B po elementima");
                //Console.ReadKey();
            }

            else
            {
                Console.WriteLine("nema razlike između A i B po svim kriterijima");
                //  Console.ReadKey();
            }





            ///////////////////////////////////////////////////////////////////////////////////////


            int br = imaAnemaB.Count();
            if (br != 0)
            {
                XmlDocument razdok = KreiraXmlListuRazlike(imaAnemaB);
                razdok.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\RazlikaAB.xml");
                Console.WriteLine("Kreira XML sa svim A kojih nema u B");
                //  Console.ReadKey();
            }

            else
            {
                Console.WriteLine("nema razlike između A i B po svim kriterijima");
                // Console.ReadKey();
            }


            int bro = imaBnemaA.Count();
            if (bro != 0)
            {
                XmlDocument razdok = KreiraXmlListuRazlike(imaBnemaA);
                razdok.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\RazlikaBA.xml");
                Console.WriteLine("Kreira XML sa svim B kojih nema u A");
                //Console.ReadKey();
            }

            else
            {
                Console.WriteLine("nema razlike između B i A po svim kriterijima");
                // Console.ReadKey();
            }



            //////////////IZ LISTE A TRAŽI U LISTI B PO ID-u ///////////////////


            XmlComparerId comparerId = new XmlComparerId();
            List<Korisnik> obrisaniKorisnici = korisnici1.Except(korisnici2, comparerId).ToList();

            int broj = obrisaniKorisnici.Count();
            if (broj != 0)
            {
                XmlDocument razdokA = KreiraXmlListuRazlike(obrisaniKorisnici);
                razdokA.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\ImaANemaB.xml");
                Console.WriteLine("Kreiran XML Ima A nema B");
                Console.ReadKey();
            }

            else
            {
                Console.WriteLine("nema razlike između A i B");
                Console.ReadKey();
            }
            //////////////IZ LISTE B TRAŽI U LISTI A PO ID-u ///////////////////

            List<Korisnik> dodaniKorisnici = korisnici2.Except(korisnici1, comparerId).ToList();
            int a = dodaniKorisnici.Count();
            if (a != 0)
            {
                XmlDocument razdokB = KreiraXmlListuRazlike(dodaniKorisnici);
                razdokB.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\ImaBNemaA.xml");
                Console.WriteLine("Kreiran XML Ima B nema A");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("nema razlike između B i A");
                Console.ReadKey();
            }

        }







        public void Xml_Azuriranje() //treba kreirati text box za unos podataka za ažuriranje ako ću imati opciju ažuriranja preko unosa
        {
            XmlDocument xmlDok = new XmlDocument();
            xmlDok.Load(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\Korisnici.xml");

            XmlNode usernode = xmlDok.SelectSingleNode("//korisnici//korisnik/korisnik_id");
            //usernode.InnerText = txt_box.Text;
            usernode.InnerText = "2";

            xmlDok.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\AzuriranKorisnik_id.xml");

            Console.WriteLine("xml ažuriran");
            Console.ReadKey();
        }

        public void KreiraXMLsaXmlDocument()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode rootNode = xmldoc.CreateElement("korisnici");
            xmldoc.AppendChild(rootNode);

            XmlNode korisnikNode = xmldoc.CreateElement("korisnik");
            rootNode.AppendChild(korisnikNode);
            var kor_id = xmldoc.CreateElement("korisnik_id");
            //kor_id.InnerText = "1";

            var kor_ime = xmldoc.CreateElement("korisnicko_ime");
            var lozinka = xmldoc.CreateElement("lozinka");
            var ime = xmldoc.CreateElement("ime");
            var prezime = xmldoc.CreateElement("prezime");
            var email = xmldoc.CreateElement("email");


            korisnikNode.AppendChild(kor_ime);
            korisnikNode.AppendChild(lozinka);
            korisnikNode.AppendChild(ime);
            korisnikNode.AppendChild(prezime);
            korisnikNode.AppendChild(email);
            korisnikNode.PrependChild(kor_id);


            rootNode.AppendChild(korisnikNode);

            xmldoc.Save(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\Korisnici.xml");



        }


    }
}
