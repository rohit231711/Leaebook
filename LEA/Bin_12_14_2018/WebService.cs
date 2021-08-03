using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using BAL;
using System.Xml;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using General;
using System.Configuration;
using System.Threading;
using System.Globalization;
using System.Data.SqlClient;
using DAL;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class WebService : System.Web.Services.WebService
{
    string price = "";
    string retJSON = "";
    int i = 0;

    RegistrationBAL objUser = new RegistrationBAL();
    CategoryBAL objCategory = new CategoryBAL();

    BookBAL objBook = new BookBAL();
    CmsBAL objCms = new CmsBAL();
    Country objCountry = new Country();
    BookPurchaseBAL objPurchase = new BookPurchaseBAL();
    LanguageBAL objLanguage = new LanguageBAL();
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookDeliveryAddressBAL objBookDelivery = new BookDeliveryAddressBAL();

    //[WebMethod(Description = "This webservice for insert user")]
    //public void InsertUser(string FacebookEmail, string FirstName, string LastName, string EmailAddress, string Password, string IsNewsLetter)
    //{
    //    objUser.FacebookEmail = FacebookEmail;
    //    objUser.FirstName = FirstName;
    //    objUser.LastName = LastName;
    //    objUser.EmailAddress = EmailAddress;
    //    objUser.IsActive = false;
    //    objUser.IsNewsLetter = Global.ConvertToBool(IsNewsLetter);
    //    objUser.Password = Password;
    //    objUser.UserType = 3;
    //    objUser.Renewals = false;
    //    objUser.NewIssues = false;
    //    objUser.AppUpdates = false;

    //    objUser.ActivationID = System.Guid.NewGuid().ToString();

    //    i = objUser.InsertRegistration();
    //    if (i > 0)
    //    {
    //        retJSON = Success1("Registration done successfully.", i);
    //    }
    //    else
    //    {
    //        retJSON = Fail1("Email already exists", 0);
    //    }
    //    objUser = new RegistrationBAL();
    //    objUser.RegistrationID = i;
    //    DataTable dt = objUser.SelectRegistraionByID();

    //    if (dt.Rows.Count != 0 && i > 0)
    //    {
    //        EmailSender objemail = new EmailSender();
    //        System.Text.StringBuilder strBody = rEGISTEReMAIL();// new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "register.htm")));
    //        objemail.RegistrationMail(dt.Rows[0]["FirstName"].ToString() + "  " + dt.Rows[0]["LastName"].ToString(), "", dt.Rows[0]["Password"].ToString(), dt.Rows[0]["EmailAddress"].ToString(), strBody, Config.WebSiteMain + "Client/Activation.aspx?Id=" + dt.Rows[0]["ActivationID"].ToString(), dt.Rows[0]["ActivationID"].ToString());
    //    }
    //    Context.Response.Output.Write(retJSON);
    //}

    private string DecodeFromUtf8(string utf8String)
    {
        // copy the string as UTF-8 bytes.
        byte[] utf8Bytes = new byte[utf8String.Length];
        for (int i = 0; i < utf8String.Length; ++i)
        {
            //Debug.Assert( 0 <= utf8String[i] && utf8String[i] <= 255, "the char must be in byte's range");
            utf8Bytes[i] = (byte)utf8String[i];
        }

        return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
    }

    private string getUnicodeString(string strInput)
    {
        string strOutput = "";

        foreach (char ch in strInput)
        {
            if (ch == ' ')
            {
                strOutput += " ";
            }
            else
            {
                //strOutput += "\\u00" + String.Format("{0:X}", Convert.ToInt32(ch));  //ch.ToString("X4");
                //strOutput += "" + String.Format("\\u{0:X4}", Convert.ToInt16(ch));
                strOutput += "" + String.Format("\\u{0:X4}", Convert.ToInt64(ch));
            }
        }

        return strOutput;
    }

    [WebMethod(Description = "This webservice for insert user")]
    public void Register(string Username, string Email, Int32 GenderID, string Password, string Birthdaydate, Int32 Countryid, Int32 Languageid)
    {
        try
        {
            string aspJson = "";
            objUser.UserName = Username;
            objUser.EmailAddress = Email;
            objUser.GenderID = GenderID;
            objUser.FirstName = Username;
            objUser.Password = Password;
            objUser.IsNewsLetter = false;
            objUser.BirthdayDate = Birthdaydate.ToString();
            //try
            //{
            //    objUser.BirthdayDate = DateTime.ParseExact(Birthdaydate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("MMM dd yyyy");
            //}
            //catch
            //{
            //    objUser.BirthdayDate = DateTime.Now.ToString("MMM dd yyyy");
            //}
            objUser.UserType = 3;
            objUser.Countryid = Countryid;
            objUser.LanguageID = Languageid;
            objUser.ActivationID = System.Guid.NewGuid().ToString();
            try
            {
                DataTable UserDT = objUser.GetOneByEmail();
                if (UserDT != null && UserDT.Rows.Count > 0)
                {
                    Fail("-1", "Your Email id already exists");
                }
                else
                {
                    i = objUser.InsertRegistration();
                    if (i > 0)
                    {
                        aspJson += "[{\"Userid\": \"" + i + "\"}";

                        aspJson += "]";
                        objUser = new RegistrationBAL();
                        objUser.RegistrationID = i;
                        DataTable dt = objUser.SelectRegistraionByID();

                        if (dt.Rows.Count != 0 && i > 0)
                        {
                            EmailSender objemail = new EmailSender();
                            //System.Text.StringBuilder strBody = rEGISTEReMAIL();// new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "register.htm")));

                            string ContactEmail = ""; string facebooklink = ""; string googlepluslink = ""; string twiterlink = ""; string storephone = "";
                            WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
                            DataTable DT = WSB.GetAllWebseetings();
                            if (DT != null && DT.Rows.Count > 0)
                            {
                                ContactEmail = DT.Rows[0]["ContactUs"].ToString();
                                facebooklink = DT.Rows[0]["FaceBookLink"].ToString();
                                googlepluslink = DT.Rows[0]["GoogleLink"].ToString();
                                twiterlink = DT.Rows[0]["TwiterLink"].ToString();
                                storephone = DT.Rows[0]["BookStorePhone"].ToString();
                            }
                            string body = string.Empty;
                            string file = Server.MapPath("~\\EmailTemplates\\Activation.htm");
                            using (StreamReader reader = new StreamReader(file))
                            {
                                body = reader.ReadToEnd();
                            }
                            string EmailAddress = "";
                            if (Languageid == 1)
                            {
                                EmailAddress = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Index.aspx?l=en-US&loginid=" + dt.Rows[0]["ActivationID"].ToString();
                            }
                            else
                            {
                                EmailAddress = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Index.aspx?l=es-ES&loginid=" + dt.Rows[0]["ActivationID"].ToString();
                            }
                            body = body.Replace("{Email}", EmailAddress);
                            body = body.Replace("{txt_firstname}", dt.Rows[0]["UserName"].ToString());
                            body = body.Replace("{ContactEmail}", ContactEmail);
                            body = body.Replace("{BookStorePhone}", storephone);
                            body = body.Replace("{FacebookLink}", facebooklink);
                            body = body.Replace("{GooglePlusLink}", googlepluslink);
                            body = body.Replace("{TwiterLink}", twiterlink);
                            System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);
                            objemail.RegistrationMail("", dt.Rows[0]["UserName"].ToString(), dt.Rows[0]["Password"].ToString(), dt.Rows[0]["EmailAddress"].ToString(), strBody, EmailAddress, dt.Rows[0]["ActivationID"].ToString());

                        }
                        Success2("0", "Registration Success Your account has been created and an activation link has been sent to the email address you entered. You must activate the account by clicking on the activation link when you get the email before you can login.", aspJson);

                    }
                }
            }
            catch (Exception ex)
            {
                //string Error = cExceptionHandling.HandleException(ex, HttpContext.Current.Request.Url.ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name);
                //return Error + "@error";
                Fail("-3", "Cannot connect to server.");
            }
            //if (i > 0)
            //{
            //    aspJson += "[{\"Userid\": \"" + i + "\"}";

            //    aspJson += "]";
            //    objUser = new RegistrationBAL();
            //    objUser.RegistrationID = i;
            //    DataTable dt = objUser.SelectRegistraionByID();

            //    if (dt.Rows.Count != 0 && i > 0)
            //    {
            //        EmailSender objemail = new EmailSender();
            //        System.Text.StringBuilder strBody = rEGISTEReMAIL();// new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "register.htm")));
            //        objemail.RegistrationMail("", dt.Rows[0]["UserName"].ToString(), dt.Rows[0]["Password"].ToString(), dt.Rows[0]["EmailAddress"].ToString(), strBody, Config.WebSiteMain + "Client/Activation.aspx?Id=" + dt.Rows[0]["ActivationID"].ToString(), dt.Rows[0]["ActivationID"].ToString());
            //    }
            //    Success2("0", "Registration Success Your account has been created and an activation link has been sent to the email address you entered. You must activate the account by clicking on the activation link when you get the email before you can login.", aspJson);

            //}
            //else
            //{
            //    //retJSON = Fail1("Email already exists", 0);
            //    //Fail("-1", "Your Email id already exists");
            //}

            // Context.Response.Output.Write(retJSON);
        }
        catch (Exception ex)
        {
            Fail("-3", "Cannot connect to server.");
        }


    }

    //public System.Text.StringBuilder rEGISTEReMAIL()
    //{
    //    string Register =
    //       "         <head>"
    //       + "     <style type=\"text/css\">"
    //       + "         p.MsoNormal "
    //       + "         { "
    //       + "             margin-top: 0in;"
    //       + "             margin-right: 0in; "
    //       + "             margin-bottom: 10.0pt; "
    //       + "             margin-left: 0in;  "
    //       + "             line-height: 115%; "
    //       + "             font-size: 11.0pt; "
    //       + "             font-family: \"Calibri\" , \"sans-serif\"; "
    //       + "         } "
    //       + "     </style> "
    //       + " </head>"
    //       + " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"2\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
    //       + "     <tr> "
    //       + "         <td  style=\"background-color:#4E93B4;Font-size:20px;\"> "
    //        //+ "             <img src=\"http://themagz.net/client/images/logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" /> style=\"background-color: #008000\"  rgb(10, 109, 10);"
    //       + "<b>LEA eBooks</b>"
    //       + "         </td> "
    //       + "     </tr> "
    //       + "     <tr> "
    //       + "         <td valign=\"top\"> "
    //       + "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
    //       + "                 <br /> "
    //       + "                 <span lang=\"EN-IN\">Dear Customer,</span><p class=\"MsoNormal\"> "
    //       + "                     <span lang=\"EN-IN\">Welcome to LEA eBooks, To Verify your account Please Copy Following "
    //       + "                         Code,<br /> "
    //       + "                         <br /> "
    //       + "                         <br /> "
    //       + "                         {%[Code]%}<br /> "
    //       + "                         <br /> "
    //       + "                         and Go on <br /> <a style=\"\" href=\"{%[SiteUrl]%}\"> {%[SiteUrl]%} </a> <br /> Kindly make a note of credentials for your LEA eBooks "
    //       + "                         account.<br /> "
    //       + "                         <br /> "
    //       + "                         <b>Email</b>: {%[EmailAddr]%}<br /> "
    //       + "                         <b>Password</b>: {%[Password]%}</span></p> "
    //       + "                 <p> "
    //       + "                     To get more information please contact our representative.</p> "
    //       + "                 <p class=\"MsoNormal\"> "
    //       + "                     <span lang=\"EN-IN\">Regards,<br /> "
    //       + "                         LEA eBooks</span></p> "
    //       + "                 <br /> "
    //       + "                 <br /> "
    //       + "             </font> "
    //       + "         </td> "
    //       + "     </tr> "
    //       + "     <tr> "
    //       + "         <td align=\"center\"> "
    //       + "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#9b98d2\">{%[CopyRight]%}</font> "
    //       + "         </td> "
    //       + "     </tr> "
    //       + " </table> ";


    //    int val = 0;
    //    System.Text.StringBuilder strBody = new System.Text.StringBuilder(Register);
    //    return strBody;
    //}

    public System.Text.StringBuilder rEGISTEReMAIL()
    {
        string ContactEmail = "";
        WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
        DataTable DT = WSB.GetAllWebseetings();
        if (DT != null && DT.Rows.Count > 0)
        {
            ContactEmail = DT.Rows[0]["ContactUs"].ToString();
        }
        //string Register =
        //    "         <head>"
        //   + "     <style type=\"text/css\">"
        //   + "         p.MsoNormal "
        //   + "         { "
        //   + "             margin-top: 0in;"
        //   + "             margin-right: 0in; "
        //   + "             margin-bottom: 10.0pt; "
        //   + "             margin-left: 0in;  "
        //   + "             line-height: 115%; "
        //   + "             font-size: 11.0pt; "
        //   + "             font-family: \"Calibri\" , \"sans-serif\"; "
        //   + "         } "
        //   + "     </style> "
        //   + " </head>"
        //   + " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"2\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
        //   + "     <tr> "
        //   + "         <td  style=\"background-color:#001c4a;Font-size:20px; color:White;\"> "
        //   + "             <img src=\"http://203.124.107.14:35/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" />"
        //   + "<div style=\"margin-left: 55px; margin-top: -25px;\"><b >LEA eBooks</b></div>"
        //   + "   