using System.Collections.Generic;
using System;
using System.Data;using System.Data.SqlClient;
using System.Configuration;
using Model;




namespace PristupPodatcima
{
    public class KorisnikDal
    {

        public List<Korisnik> DajKorisnike()
        {
            List<Korisnik> korisnici = new List<Korisnik>();

            string connString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;



            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select korisnik_id as KorisnikId,korisnicko_ime as KorisnickoIme, lozinka as Lozinka, email as Email, ime as Ime, prezime as Prezime from dbo.korisnik;";
                cmd.CommandType = CommandType.Text;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Korisnik k = new Korisnik();

                    k.KorisnikId = (int)reader["KorisnikId"];
                    k.KorisnickoIme = reader["KorisnickoIme"].ToString();
                    k.Lozinka = reader["Lozinka"].ToString();
                    k.Email = reader["Email"].ToString();
                    k.Ime = reader["Ime"].ToString();
                    k.Prezime = reader["Prezime"].ToString();

                    korisnici.Add(k);
                }
            }

            return korisnici;
        }


        public Korisnik DajKorisnika(string korIme)
        {

            Korisnik korisnik = new Korisnik();

            string connString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;



            using (SqlConnection conn = new SqlConnection(connString))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"select korisnik_id as KorisnikId, lozinka as Lozinka, email as Email, ime as Ime, prezime as Prezime from dbo.korisnik
                                     where korisnicko_ime = @korisnicko_ime ;";
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("@korisnicko_ime", SqlDbType.VarChar).Value = korIme;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {


                    korisnik.KorisnikId = (int)reader["KorisnikId"];
                    korisnik.KorisnickoIme = reader["KorisnickoIme"].ToString();
                    korisnik.Lozinka = reader["Lozinka"].ToString();
                    korisnik.Email = reader["Email"].ToString();
                    korisnik.Ime = reader["Ime"].ToString();
                    korisnik.Prezime = reader["Prezime"].ToString();


                }
            }

            return korisnik;
        }


        public void AzuriranjeKorisnika(string lozinka, string ime, string prezime, string email, int korID, string korisnickoIme)
        {

            string connString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (@" update korisnik 
                                      set  lozinka = @lozinka, ime = @ime, prezime = @prezime, email = @email, korisnickoIme = @korisnickoIme
                                      where korisnik_id = @korisnik_id ");


                cmd.Parameters.Add("@lozinka", SqlDbType.NVarChar).Value = lozinka;
                cmd.Parameters.Add("@ime", SqlDbType.VarChar).Value = ime;
                cmd.Parameters.Add("@prezime", SqlDbType.VarChar).Value = prezime;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@korisnik_id", SqlDbType.VarChar).Value = korID;
                cmd.Parameters.Add("@korisnickoIme", SqlDbType.VarChar).Value = korisnickoIme;

                cmd.ExecuteNonQuery();

            }



        }


        public void ExportirajKorisnikeUXml() // KORISTI DATASET i WRITEXML za kopiranje korisnika iz baze u xml dokument
        {

            string connString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connString))
            {
                string comm = (@"Select * from korisnik;");

                SqlDataAdapter sda = new SqlDataAdapter(comm, connString);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                ds.WriteXml(@"C:\Users\User\Source\Repos\diplomski.v5.1\Diplomski\Diplomski\Xmlmapa\XMLFile4.xml");



            }

        }



        public Korisnik DajKorisnikaPoKorimeLozinka(string ime, string psw)
        {
            

            Korisnik korisnik = new Korisnik();
            string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string strSelect = @"select korisnik_id as KorisnikId, korisnicko_ime as KorisnickoIme, lozinka as Lozinka, email as Email, ime as Ime, prezime as Prezime from dbo.korisnik
                                     where korisnicko_ime = @korisnicko_ime and lozinka = @lozinka;";

            SqlConnection con = new SqlConnection(strCon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSelect;

            cmd.Parameters.Add("@lozinka", SqlDbType.VarChar, 50).Value = psw;

            cmd.Parameters.Add("@korisnicko_ime", SqlDbType.VarChar, 50).Value = ime;
               
         /*   SqlParameter lozinka = new SqlParameter("@lozinka", SqlDbType.VarChar, 50);
            lozinka.Value = psw;
            cmd.Parameters.Add(lozinka);  */
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                korisnik.KorisnikId = (int)reader["KorisnikId"];
                
                korisnik.Lozinka = reader["Lozinka"].ToString();
                korisnik.Email = reader["Email"].ToString();
                korisnik.Ime = reader["Ime"].ToString();
                korisnik.Prezime = reader["Prezime"].ToString();
                korisnik.KorisnickoIme = reader["KorisnickoIme"].ToString();
            }

            con.Close();

            return korisnik;
        }



        public void InsertiranjeKorisnika(string lozinka, string ime, string prezime, string email, string korIme, int korisnik_id)
        {

            string connString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = (@"insert into korisnik (lozinka, ime, prezime, email, korisnicko_ime, korisnik_id )
                                        values(@lozinka, @ime, @prezime, @email, @KorisnickoIme, @korisnikId ) ");


                cmd.Parameters.Add("@lozinka", SqlDbType.NVarChar).Value = lozinka;
                cmd.Parameters.Add("@ime", SqlDbType.VarChar).Value = ime;
                cmd.Parameters.Add("@prezime", SqlDbType.VarChar).Value = prezime;
                cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                cmd.Parameters.Add("@KorisnickoIme", SqlDbType.VarChar).Value = korIme;
                cmd.Parameters.Add("@korisnikId", SqlDbType.Int).Value = korisnik_id;

                cmd.ExecuteNonQuery();

            }



        }








    }

}



