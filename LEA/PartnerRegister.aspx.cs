using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Configuration;
using System.IO;

public partial class PartnerRegister : System.Web.UI.Page
{
    Country Obj_Country = new Country();
    BAL_Account Obj_Reg = new BAL_Account();
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            txt_firstname.Focus();
            this.Form.DefaultButton = btn_submit.UniqueID;
            BindCountry();
        }
    }

    public void BindCountry()
    {
        DataTable dt = new DataTable();
        dt = Obj_Country.SelectAllActiveCountry();
        if (dt.Rows.Count > 0)
        {
            dd_country.DataSource = dt;
            dd_country.DataTextField = "countryname";
            dd_country.DataValueField = "countryid";
            dd_country.DataBind();
            dd_country.DataBind();
            dd_country.Items.Insert(0, new ListItem("Select Country", "0"));
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {

        // check email duplication 

        BAL_Account Obj_account = new BAL_Account();
        Obj_account.Email = txt_email.Text.Trim();
        Obj_account.RegistrationID = 0;
        Obj_account.UserName = "";//txt_username.Text.Trim();
        DataTable dt = Obj_account.Check_Email_Duplication();
        int emailcount = Convert.ToInt32(dt.Rows[0]["emailcount"].ToString());
        dt.Clear();
        dt = Obj_account.Check_User_Duplication();
        int usercount = 0; //Convert.ToInt32(dt.Rows[0]["usercount"].ToString());

        if (emailcount == 0 && usercount == 0)
        {
            Obj_Reg.FirstName = txt_firstname.Text.ToString().Trim();
            Obj_Reg.LastName = "";//txt_lastname.Text.ToString().Trim();
            Obj_Reg.Email = txt_email.Text.Trim();
            Obj_Reg.Password = txt_password.Text.Trim();
            Obj_Reg.CreatedDate = System.DateTime.Now;
            Obj_Reg.IsActive = false;
            Obj_Reg.IsNewsLetter = false;
            Obj_Reg.UserType = 4;
            Obj_Reg.LoginDate = System.DateTime.Now;
            Obj_Reg.LastLoginDate = System.DateTime.Now;
            Obj_Reg.NewIssues = false;
            Obj_Reg.Renewals = false;
            Obj_Reg.AppUpdates = false;
            Obj_Reg.FacebookEmail = "";
            Obj_Reg.ActivationID = System.Guid.NewGuid().ToString();
            Obj_Reg.UserName = "";//txt_username.Text.ToString().Trim();
            Obj_Reg.GenderID = 0;
            //if (rdo_male.Checked == true)
            //{
            //    Obj_Reg.GenderID = 1;
            //}
            //else
            //{
            //    Obj_Reg.GenderID = 2;
            //}
            Obj_Reg.BirthdayDate = Convert.ToDateTime("01/01/1990");
            //Obj_Reg.BirthdayDate = Convert.ToDateTime(txt_birthdate.Text.ToString());
            Obj_Reg.Countryid = Convert.ToInt32(dd_country.SelectedValue);
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name != null)
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToString() == "en-US")
                {
                    Obj_Reg.LanguageID = 1;
                }
                else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToString() == "es-ES")
                {
                    Obj_Reg.LanguageID = 2;
                }
                else
                {
                    Obj_Reg.LanguageID = 1;
                }
            }
            else
            {
                Obj_Reg.LanguageID = 1;
            }

            int ID = Obj_Reg.InsertRegistration();
            sendmail(Obj_Reg.ActivationID);
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Sus datos se ha enviado a su dirección de correo electrónico. Por favor, haga clic en el enlace de activación para activar su cuenta.'); window.location ='Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name +"';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Your details has been sent to your email address. Please click on activation link to active your account.'); window.location = 'Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';", true);
            }
        }
        else
        {
            if (emailcount > 0)
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('El correo electrónico es ya existen.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('EmailId " + txt_email.Text.ToString() + " is already exist.');", true);
                }
            }           
        }
    }

    public void clear()
    {
        //txt_birthdate.Text = "";
        txt_ConfirmPassword.Text = "";
        txt_email.Text = "";
        txt_firstname.Text = "";        
        txt_password.Text = "";        
        dd_country.SelectedIndex = 0;
        //rdo_Female.Checked = false;
        //rdo_male.Checked = false;
    }


    public void sendmail( string ID)
    {
        string ContactEmail = "";
        WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
        DataTable  DT = WSB.GetAllWebseetings();
        if (DT != null && DT.Rows.Count > 0)
        {
            ContactEmail = DT.Rows[0]["ContactUs"].ToString();
        }
        if (ID.ToString() != "")
        {

            string Email = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&loginid=" + ID;


            string body = string.Empty;
            string file = Server.MapPath("~\\EmailTemplates\\ActivationPartner.htm");
            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Email}", Email);
            body = body.Replace("{txt_firstname}", txt_firstname.Text);
            body = body.Replace("{ContactEmail}", ContactEmail);
            body = body.Replace("{SiteUrl}", Config.WebSiteMain + @"Partner/Default.aspx");
            System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);
            Global.SendEmail(txt_email.Text, "LEA eBooks | Activation", strBody.ToString());
            clear();

        }
    }

    
    

}