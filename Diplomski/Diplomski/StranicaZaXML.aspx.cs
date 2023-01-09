using System;
using System.Collections.Generic;
using PoslovnaLogika;
using Model;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Linq;
using System.Web;
using System.Web.UI;
using PristupPodatcima;



namespace Diplomski
{
    public partial class StranicaZaXml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
        }

        KorisnikServis kos = new KorisnikServis();
        List<Korisnik> kor = new List<Korisnik>();





        protected void UsporediDvaXmla_Click(object sender, EventArgs e)
        {
            XmlCompare(); //Metoda se nalazi u XmlServis.cs. Poštimati putanje odakle se vuku Xml-i, koristiti fileupload ?
        }

        protected void ExportUXml_Click(object sender, EventArgs e)  //  IZ BAZE U XML
        {

            KreiraXmlKorisnika();


        }

        protected void bt_uveziXml_Click(object sender, EventArgs e)  // S DISKA UČITAVA XML I SPREMA GA U MAPU "MapaXmlKorisnici"
        {

            if (FileUpload1.HasFile)
            {
                string apsolutnaPutanja = Path.Combine(Server.MapPath("~/MapaXmlKorisnici"), FileUpload1.FileName);
                FileUpload1.SaveAs(apsolutnaPutanja);
                Label1.Text = "učitani fajl: " + FileUpload1.FileName;

            }

            else
            {
                Label1.Text = "fajl nije učitan";
            }

        }

        protected void BtUsporedi_Click(object sender, EventArgs e)
        {
            string apsolutnaPutanja = Path.Combine(Server.MapPath("~/MapaXmlKorisnici"), FileUpload1.FileName);

            List<Korisnik> korprvi = DajXmlUListu(apsolutnaPutanja);
            List<Korisnik> kordrugi = kos.DajKorisnike();

            UsporediRijesiDviListeKorisnika(korprvi, kordrugi);
        }

        protected void SpremiXMLuBazu_Click(object sender, EventArgs e)  //sprema u bazu podatke iz XML-a, koristi stored proceduru.JAVLJA GREŠKU!! TREBA Deserialize??
        {
            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            string filePath = Server.MapPath("~/XmlDatoteka/") + fileName;
            FileUpload1.SaveAs(filePath);
            string xml = File.ReadAllText(filePath);
            string constr = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("InsertirajXML"))
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@xml", xd);  ////opcija:xml umjesto xd //cmd.Parameters.Add("@xml", SqlDbType.Xml).Value = xd.GetXml();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            InsertirajPodatkeUBazuIzXmla(); //metoda u Xml.Dal



        }


        public static List<Korisnik> DajXmlUListu(string apsolutnaPutanja)
        {
            XmlDocument xmldok = new XmlDocument();
            //xmldok.Load(apsolutnaPutanja);// ne da mi pristupiti
            xmldok.Load(@"C:\Users\s00ivbe\source\repos\ZadatakDva\MPortal\MapaXmlKorisnici\Korisnici.xml");
            XmlElement root = xmldok.DocumentElement;
            XmlNodeList nodes = root.GetElementsByTagName("korisnik");//korisnik

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



        public void UsporediRijesiDviListeKorisnika(List<Korisnik> korprvi, List<Korisnik> kordrugi)
        {

            List<Korisnik> korisniciBaza = kordrugi; // korprvi;
            List<Korisnik> korisniciXML = kordrugi;

            List<Korisnik> izmjenjeni = new List<Korisnik>();
            List<Korisnik> brisani = new List<Korisnik>();
            List<Korisnik> dodani = new List<Korisnik>();


            /////////////////////////////////////////////////////////////////////////////       


            foreach (Korisnik korisnikBaza in korisniciBaza)
            {
                bool jeDodaan = true;

                foreach (Korisnik korisnikXml in korisniciXML)
                {
                    if (korisnikBaza.KorisnikId == korisnikXml.KorisnikId)
                    {
                        jeDodaan = false;
                        izmjenjeni.Add(korisnikXml);
                        continue;
                    }

                }
                if (jeDodaan)
                {
                    brisani.Add(korisnikBaza);
                }

            }

            foreach (Korisnik korisnikXml in korisniciXML)
            {
                bool jeDodan = true;
                foreach (Korisnik korisnikBaza in korisniciBaza)
                {
                    if (korisnikXml.KorisnikId == korisnikBaza.KorisnikId)
                    {
                        jeDodan = false;
                        continue;
                    }

                }
                if (jeDodan)
                {
                    dodani.Add(korisnikXml);
                }
            }

            /////////////////////////////////////////AŽURIRANJE////////////////////////////////////

            if (izmjenjeni.Count > 0)
            {

                foreach (Korisnik korisnik in izmjenjeni)
                {
                    string lozinka = korisnik.Lozinka.ToString();
                    string ime = korisnik.Ime.ToString();
                    string prezime = korisnik.Prezime.ToString();
                    string email = korisnik.Email.ToString();
                    int korID = Convert.ToInt32(korisnik.KorisnikId);
                    string korisnickoIme = korisnik.KorisnickoIme.ToString();

                    kos.AzuriranjeKorisnika(lozinka, ime, prezime, email, korID, korisnickoIme);

                    Console.WriteLine("korisnici azurirani");
                    Console.ReadKey();
                }
            }

            else
            {
                Console.WriteLine("nije bilo korisnika za ažuriranje ");
                Console.ReadKey();
            }


            /////////////////////////////////////////////////INSERTIRANJE /////////////////////////////////////

            if (dodani.Count > 0)
            {

                foreach (Korisnik korisnik in dodani)
                {
                    string lozinka = korisnik.Lozinka.ToString();
                    string ime = korisnik.Ime.ToString();
                    string prezime = korisnik.Prezime.ToString();
                    string email = korisnik.Email.ToString();
                    string korIme = korisnik.KorisnickoIme.ToString();
                    int korID = Convert.ToInt32(korisnik.KorisnikId);

                    kos.InsertiranjeKorisnika(lozinka, ime, prezime, email, korIme, korID);

                    Console.WriteLine("insertirani korisnici");
                    Console.ReadKey();
                }
            }

            else
            {
                Console.WriteLine("nije bilo novih korisnika ");
                Console.ReadKey();

            }

        }



        public void KreiraXmlKorisnika()   // KREIRA XML DOKUMENT, SVE DOHVAĆENE KORISNIKE IZ TABLICE  SPREMA  U XML
        {
            kor = kos.DajKorisnike();    //IZ BAZE ZAPOSLJAVANJE, TABLICE KORISNIK, DOHVAĆA SVE KORISNIKE
            List<Korisnik> korisnici = kor;

            XmlDocument xmldok = new XmlDocument();
            XmlNode rootnode = xmldok.CreateElement("korisnici");
            xmldok.AppendChild(rootnode);

            foreach (Korisnik korisnik in korisnici)
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
            xmldok.Save(@"C:\Users\s00ivbe\Desktop\završni rad\ZadatakDva\XmlDatoteka\Korisnici.xml");


        }












    }


}
