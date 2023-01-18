using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Model;
using PoslovnaLogika;
using System.Collections;
using PristupPodatcima;

namespace Diplomski
{
    public partial class Prijava : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        KorisnikServis kos = new KorisnikServis();
        Korisnik kor = new Korisnik();
        

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                Label1.Text = "unesi korisničko ime";
            }
            else if (txtPwd.Text == "")
            {
                Label2.Text = "unesi lozinku";

            }

            else
            {
                kor = kos.DajKorisnikaPoKorimeLozinka(txtUserName.Text, txtPwd.Text);

                //if(count.kor != 0) ?!
                if (kor != null)

                {
                    Session["username"] = txtUserName.Text.Trim();

                    Session["userId"] = kor.KorisnikId;
                    Session["user"] = kor;

                    Response.Redirect("StranicaZaXML.aspx");

                }

                else
                {
                    Label3.Text = "unešeni podatci nisu ispravni";

                    Label1.Text = "";
                    Label2.Text = "";

                }

            }


        }

        protected void txtPwd_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
