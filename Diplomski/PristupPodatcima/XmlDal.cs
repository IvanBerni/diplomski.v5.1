using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Model;



namespace PristupPodatcima
{
    public class XmlDal
    {


        public void InsertirajPodatkeUBazuIzXmla()
        {

            SqlDataAdapter adpter = new SqlDataAdapter();
            DataSet ds = new DataSet();


            int KorisnikId = 0;
            string KorisnickoIme = null;
            string Ime = null;
            string Prezime = null;
            string Email = null;
            string Lozinka = null;


            string connetionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connetionString);

            XmlReader xmlFile = XmlReader.Create("Korisnici.xml", new XmlReaderSettings());
            ds.ReadXml(xmlFile);
            int i = 0;
            connection.Open();
            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {
                KorisnikId = Convert.ToInt32(ds.Tables[0].Rows[i].ItemArray[0]);
                KorisnickoIme = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                Ime = ds.Tables[0].Rows[i].ItemArray[2].ToString();
                Prezime = ds.Tables[0].Rows[i].ItemArray[3].ToString();
                Email = ds.Tables[0].Rows[i].ItemArray[4].ToString();
                Lozinka = ds.Tables[0].Rows[i].ItemArray[5].ToString();
            }

            string sql = "insert into korisnik values(" + KorisnikId + ",'" + KorisnickoIme + "'," + Ime + "'," + Prezime + "'," + Email + "'," + Lozinka + "',";
            SqlCommand command = new SqlCommand(sql, connection);
            adpter.InsertCommand = command;
            adpter.InsertCommand.ExecuteNonQuery();

            connection.Close();
        }

    }

}



