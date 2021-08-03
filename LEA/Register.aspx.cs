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
using System.Net.Mail;
using System.Globalization; 

public partial class Register : System.Web.UI.Page
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
            Obj_Reg.IsActive = true;
            Obj_Reg.IsNewsLetter = false;
            Obj_Reg.UserType = 3;
            Obj_Reg.LoginDate = System.DateTime.Now;
            Obj_Reg.LastLoginDate = System.DateTime.Now;
            Obj_Reg.NewIssues = false;
            Obj_Reg.Renewals = false;
            Obj_Reg.AppUpdates = false;
            Obj_Reg.FacebookEmail = "";
            Obj_Reg.ActivationID = System.Guid.NewGuid().ToString();
            Obj_Reg.UserName = "";//txt_username.Text.ToString().Trim();
            //if (rdo_male.Checked == true)
            //{
            //    Obj_Reg.GenderID = 1;
            //}
            //else
            //{
            //    Obj_Reg.GenderID = 2;
            //}
            Obj_Reg.GenderID = 0;
            //Obj_Reg.BirthdayDate = Convert.ToDateTime(txt_birthdate.Text.ToString());
            //if (!string.IsNullOrEmpty(txt_birthdate.Text))
            //{
            //    string[] format = { "MMM/dd/yyyy", "MM/dd/yyyy", "MM-dd-yyyy", "MMM-dd-yyyy", };
            //    Obj_Reg.BirthdayDate = DateTime.ParseExact(txt_birthdate.Text, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            //}
            //else
            //{
            //    Obj_Reg.BirthdayDate = Convert.ToDateTime("01/01/1990");
            //}
            Obj_Reg.BirthdayDate = Convert.ToDateTime("01/01/1990");
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
                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Has creado con éxito la cuenta de Lea..'); window.location ='Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name +"';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('You have successfully create Lea Account.'); window.location = 'Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';", true);
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
        string facebooklink = "";
        string googlepluslink = "";
        string twiterlink = "";
        string storephone = "";
        WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
        DataTable  DT = WSB.GetAllWebseetings();
        if (DT != null && DT.Rows.Count > 0)
        {
            ContactEmail = DT.Rows[0]["ContactUs"].ToString();
            facebooklink = DT.Rows[0]["FaceBookLink"].ToString();
            googlepluslink = DT.Rows[0]["GoogleLink"].ToString();
            twiterlink = DT.Rows[0]["TwiterLink"].ToString();
            storephone = DT.Rows[0]["BookStorePhone"].ToString();
        }
        if (ID.ToString() != "")
        {
            //Session["UserName"] = txt_firstname.Text.ToString().Trim();
            //Session["UserID"] = ID;

            string Email = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&loginid=" + ID;
            // string body =
            //  "         <head>"
            //+ "     <style type=\"text/css\">"
            //+ "         p.MsoNormal "
            //+ "         { "
            //+ "             margin-top: 0in;"
            //+ "             margin-right: 0in; "
            //+ "             margin-bottom: 10.0pt; "
            //+ "             margin-left: 0in;  "
            //+ "             line-height: 115%; "
            //+ "             font-size: 11.0pt; "
            //+ "             font-family: \"Calibri\" , \"sans-serif\"; "
            //+ "         } "
            //+ "     </style> "
            //+ " </head>"
            //+ " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"2\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
            //+ "     <tr> "
            //+ "         <td  style=\"background-color:#001c4a;Font-size:20px; color:White;\"> "
            //+ "             <img src=\"http://203.124.107.14:35/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" />"
            //+ "<div style=\"margin-left: 55px; margin-top: -25px;\"><b >LEA eBooks</b></div>"
            //+ "         </td> "
            //+ "     </tr> "
            //+ "     <tr> "
            //+ "         <td valign=\"top\">"
            //+ "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
            //+ "                 <br/>"
            //+ "                       <p class=\"MsoNormal\"> "
            //+ "                           Dear, " + txt_firstname.Text + ""
            //+"                          </p>"

            //+"                       <br/>"
            //+ "                          <p class=\"MsoNormal\"> "
            //+"                               To complete your registration and successfully activate your account on LEA eBooks, please copy the link below and paste it in your browser"
            //+"                           </p>"
            //+"                       <br/>"

            //+ "                 <span lang=\"EN-IN\"></span><p class=\"MsoNormal\"> "
            //+ "                         <b>Action your acount </b>:<html><body><a href = '"+Email+"'> " + Email + "</a><br /></body></html>"
            //+ "                 <p class=\"MsoNormal\"> "
            //+ "                     Once again, thank you for LEA eBooks!</p> "
            //+ "                 <p class=\"MsoNormal\"><br /><br />"
            //+ "                     <span lang=\"EN-IN\">Best Wishes,<br /> "
            //+ "                         LEA eBooks Team.</span></p> "
            //+ "                 <br /> "
            //+ "                 <br /> "
            //+ "                 <p class=\"MsoNormal\"> "
            //+ "                     <span>NOTE :</span> <span>If you have any questions or further query then please contact us:<br />"
            //+ "                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ContactEmail.ToString() + ""
            //+ "                                  </span></p> "
            //+ "                 <br /> "
            //+ "             </font> "
            //+ "         </td> "
            //+ "     </tr> "
            //+ " </table> ";

            string body = string.Empty;
            string file = Server.MapPath("~\\EmailTemplates\\Activation.htm");
            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Email}", Email);
            body = body.Replace("{txt_firstname}", txt_firstname.Text);
            body = body.Replace("{ContactEmail}", ContactEmail);
            body = body.Replace("{BookStorePhone}", storephone);
            body = body.Replace("{FacebookLink}", facebooklink);
            body = body.Replace("{GooglePlusLink}", googlepluslink);
            body = body.Replace("{TwiterLink}", twiterlink);
            System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);
            SendEmail(txt_email.Text, "LEA eBooks | Activation", strBody.ToString());
            clear();

            //Response.Redirect("Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);

        }
    }

    public static void SendEmail(string To, string Subject, string Body)
    {
        try
        {
            if (Global.EmailAddressCheck(To))
            {
                string PORT = ConfigurationManager.AppSettings["PORT"].ToString().Trim();
                string SMTPFrom = ConfigurationManager.AppSettings["FROMEMAIL"].ToString().Trim();
                string SMTPpass = ConfigurationManager.AppSettings["FROMPWD"].ToString().Trim();
                string SMTP = ConfigurationManager.AppSettings["SMTP"].ToString().Trim();

                SmtpClient client = new SmtpClient(SMTP, Convert.ToInt32(PORT));
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(SMTPFrom, SMTPpass);
                MailAddress fromAddress = new MailAddress("sales@leaebook.com", "LEA eBook");
                MailMessage message = new MailMessage();
                client.EnableSsl = false;
                message.From = fromAddress;
                message.To.Add(To.ToString().Trim());
                message.Body = Body.ToString().Trim();
                message.Subject = Subject.ToString().Trim();
                message.IsBodyHtml = true;
                client.Send(message);
                message.Dispose();
            }
        }
        catch (Exception exc)
        {
             throw exc;
        }
    }
    
    

}