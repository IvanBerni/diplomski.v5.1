using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PoslovnaLogika;
using Model;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void DajKorisnike()
        {
            try
            {
                KorisnikServis korisnikServis = new KorisnikServis();
                List<Korisnik> korisnici = korisnikServis.DajKorisnike();

                bool imaKorisnika = korisnici.Count > 0;
                Assert.AreEqual(imaKorisnika, true);
            }
            catch (Exception)
            {

                Assert.AreEqual(1, 2);
            }

        }


        [TestMethod]


        public void AzurirajKor()
        {
            string lozinka = "test";
            string ime = "test";
            string prezime = "test";
            string email = "test";
            int kor_id = 1;
            try
            {
                KorisnikServis korisnikServis = new KorisnikServis();

                //korsničko ime treba?

                korisnikServis.AzuriranjeKorisnika(lozinka, ime, prezime, email, kor_id,"");


                Assert.AreEqual(1, 1);
            }

            catch (Exception ex)
            {
                Assert.AreEqual(1, 2);
            }
        }

    }
}

