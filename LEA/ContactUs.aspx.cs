using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;

public partial class ContactUs : System.Web.UI.Page
{
    CmsBAL cmsBAL = new CmsBAL();
    DataTable dt = new DataTable();
    //public static string name = "";
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {            
            this.Form.DefaultButton = btn_login.UniqueID;

            cmsBAL.ID = 6;
            cmsBAL.LanguageID = 1;

            //int language = 1;
            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
            {
                if (Request.QueryString["l"].ToString() == "es-ES")
                {
                    cmsBAL.LanguageID = 2;
                }
                else if (Request.QueryString["l"].ToString() == "en-US")
                {
                    cmsBAL.LanguageID = 1;
                }
            }
            //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            //{
            //    name = "Nombre";
            //}
            //else
            //{
            //    name = "Name";
            //}

            dt = cmsBAL.SelectcmsByID();
            lblCont_info.Text = dt.Rows[0]["Description"].ToString();
        }
    }

    protected void btn_submit(object sender, EventArgs e)
    {
        var sb = new StringBuilder();
        sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");
        var secretKey = "6LcnLEEUAAAAABhCY_vE9AdgVeg_LVvoWA6McR28";
        sb.Append(secretKey);
        sb.Append("&");
        sb.Append("response=");
        var reCaptchaResponse = Request["g-recaptcha-response"];
        sb.Append(reCaptchaResponse);
        sb.Append("&");
        sb.Append("remoteip=");
        var clientIpAddress = GetUserIp();
        sb.Append(clientIpAddress);

        using (var client = new WebClient())
        {
            var uri = sb.ToString();
            var json = client.DownloadString(uri);
            var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

            if (result == null)
            {

            }
            else // If Yes
            {
                //api call contains errors
                if (result.ErrorCodes != null)
                {
                    if (result.ErrorCodes.Count > 0)
                    {
                        foreach (var error in result.ErrorCodes)
                        {

                        }
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por favor, compruebe Captcha');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Verify Captcha');", true);
                        }
                    }
                }
                else //api does not contain errors
                {
                    if (!result.Success) //captcha was unsuccessful for some reason
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por favor, vuelva a verificar Captcha hay algún problema');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Reverify Captcha there is some problem');", true);
                        }
                    }
                    else //---- If successfully verified. Do your rest of logic.
                    {
                        #region Save Review Code
                        string Email = txtEmail.Value;
                        string Name = txtName.Value;
                        string Phone = txtPhone.Value;
                        string msg = txtmsgbox.Text;
                        string ContactEmail = "";
                        WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
                        DataTable DT = WSB.GetAllWebseetings();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            ContactEmail = DT.Rows[0]["ContactUs"].ToString();
                        }
                        // string body =
                        //"         <head>"
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
                        //+ "         <td  style=\"background-color:#001c4a;Font-size:20px;\"> "
                        //     + "             <img src=\"http://203.124.107.14:35/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\"/>"
                        //+ "<div style=\"margin-left: 55px; margin-top: -25px;\"><b >LEA eBooks</b></div>"
                        //+ "         </td> "
                        //+ "     </tr> "
                        //+ "     <tr> "
                        //+ "         <td valign=\"top\"> "
                        //+ "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
                        //+ "                 <br /> "
                        //+ "                 <span lang=\"EN-IN\"></span><p class=\"MsoNormal\"> "


                        //+ "                         <b>Email</b>: " + Email + "<br /> "
                        //+ "                         <b>User Name</b>: " + Name + "<br /> "
                        //     // + "                         <b>Password</b>: " + dt.Rows[0]["Password"].ToString() + "</span></p> "
                        // + "                         <b>Phone</b>: " + Phone + "<br /> "

                        //+ "                         <b>Message</b>: " + msg + "</span></p> "
                        //+ "                 <p class=\"MsoNormal\"> "
                        //+ "                     To get more information please contact our representative.</p> "
                        //+ "                 <p class=\"MsoNormal\"><br /><br />"
                        //+ "                     <span lang=\"EN-IN\">Regards,<br /> "
                        //+ "                         LEA eBooks</span></p> "
                        //+ "                 <br /> "
                        //+ "                 <br /> "
                        //+ "             </font> "
                        //+ "         </td> "
                        //+ "     </tr> "
                        //+ " </table> ";

                        string body = string.Empty;
                        string file = Server.MapPath("~\\EmailTemplates\\ContactUs.htm");
                        using (StreamReader reader = new StreamReader(file))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{Email}", Email);
                        body = body.Replace("{Name}", Name);
                        body = body.Replace("{ContactEmail}", ContactEmail);
                        body = body.Replace("{Phone}", Phone);
                        body = body.Replace("{msg}", msg);

                        //return body;




                        int val = 0;
                        System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);

                        try
                        {
                            string EmailTo = "";
                            WebsiteSettingsBAL WS = new WebsiteSettingsBAL();
                            DataTable dt = WS.GetAllWebseetings();
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                EmailTo = dt.Rows[0]["ClientEmail"].ToString();
                            }
                            Global.SendEmail(EmailTo, "LEA eBooks | Contact - US", strBody.ToString());
                            txtName.Value = "";
                            txtPhone.Value = "";
                            txtEmail.Value = "";
                            txtmsgbox.Text = "";
                            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            {
                                //Global.Alert(this, "Tu mensaje ha sido enviado con éxito a su dirección de correo electrónico");
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Tu mensaje ha sido enviado con éxito.');", true);
                            }
                            else
                            {
                                //Global.Alert(this, "Your message has been sent successfully to your email address");
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Your message has been sent successfully.');", true);
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        #endregion
                    }
                }

            }

        }            
    }

    [DataContract]
    public class RecaptchaApiResponse
    {
        [DataMember(Name = "success")]
        public bool Success;

        [DataMember(Name = "error-codes")]
        public List<string> ErrorCodes;
    }

    private string GetUserIp()
    {
        var visitorsIpAddr = string.Empty;

        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        {
            visitorsIpAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        }
        else if (!string.IsNullOrEmpty(Request.UserHostAddress))
        {
            visitorsIpAddr = Request.UserHostAddress;
        }

        return visitorsIpAddr;
    }
}