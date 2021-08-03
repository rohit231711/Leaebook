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
using System.Web.Script.Serialization;
//   using System.Web.Script.Serialization;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class WebService : System.Web.Services.WebService
{
    string price = "";
    string retJSON = "";
    int i = 0;

    RegistrationBAL objUser = new RegistrationBAL();
    CategoryBAL objCategory = new CategoryBAL();
    dbconnection db = new dbconnection();
    BookBAL objBook = new BookBAL();
    CmsBAL objCms = new CmsBAL();
    Country objCountry = new Country();
    BookPurchaseBAL objPurchase = new BookPurchaseBAL();
    LanguageBAL objLanguage = new LanguageBAL();
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookDeliveryAddressBAL objBookDelivery = new BookDeliveryAddressBAL();
    BookPurchaseBAL ObjPurchase = new BookPurchaseBAL();

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
            objUser.BirthdayDate = "01/01/2010";
            objUser.UserType = 3;
            objUser.Countryid = Countryid;
            objUser.LanguageID = Languageid;
            objUser.IsActive = true;
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
                            objUser.RegistrationID = i;
                            DataTable dt1 = objUser.SelectRegistraionByID();

                            if (dt1 != null && dt1.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    objBookDelivery.UserID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                    DataTable dtAdd = objBookDelivery.GetBookAddressByUser();

                                    // CartList count
                                    ObjBookOrders.CustomerID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                    ObjBookOrders.LanguageID = Convert.ToInt32(dt1.Rows[j]["LanguageID"].ToString());
                                    DataTable dtCart = ObjBookOrders.GetCartList();

                                    // MyOrders Count
                                    DataTable dtPurchaseBook = new DataTable();
                                    objPurchase.UserID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                    dtPurchaseBook = objPurchase.GetAllOrderbyUserID();

                                    //WishList count
                                    ObjBookOrders.CustomerID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                    ObjBookOrders.LanguageID = Convert.ToInt32(dt1.Rows[j]["LanguageID"].ToString());
                                    DataTable dt3 = ObjBookOrders.GetWishList();

                                    var genID = 0;
                                    if (Convert.ToInt32(dt1.Rows[j]["GenderID"].ToString()) > 0)
                                    {
                                        genID = Convert.ToInt32(dt1.Rows[j]["GenderID"].ToString()) - 1;
                                    }
                                    aspJson += "[{\"Userid\": \"" + getUnicodeString(dt1.Rows[j]["RegistrationID"].ToString()) + "\"," +
                                               "\"Username\": \"" + getUnicodeString(dt1.Rows[j]["UserName"].ToString()) + "\"," +
                                               "\"Email\": \"" + getUnicodeString(dt1.Rows[j]["EmailAddress"].ToString()) + "\"," +
                                               "\"GenderID\": \"" + getUnicodeString(dt1.Rows[j]["GenderID"].ToString()) + "\"," +
                                               "\"Gender\": \"" + getUnicodeString(genID.ToString()) + "\"," +
                                               "\"Birthdaydate\": \"" + getUnicodeString(dt1.Rows[j]["BirthdayDate"].ToString()) + "\"," +
                                                "\"LanguageID\": \"" + getUnicodeString(dt1.Rows[j]["LanguageID"].ToString()) + "\"," +
                                               "\"Language\": \"" + getUnicodeString(dt1.Rows[j]["Language"].ToString()) + "\"," +
                                               "\"CountryID\": \"" + getUnicodeString(dt1.Rows[j]["Countryid"].ToString()) + "\"," +
                                              "\"Countryname\": \"" + getUnicodeString(dt1.Rows[j]["countryname"].ToString()) + "\"," +
                                                "\"TotalCartCount\": \"" + getUnicodeString(dtCart.Rows.Count.ToString()) + "\"," +
                                               "\"TotalWishListCount\": \"" + getUnicodeString(dt3.Rows.Count.ToString()) + "\"," +
                                               "\"TotalOrderListCount\": \"" + getUnicodeString(dtPurchaseBook.Rows.Count.ToString()) + "\"," +
                                               "\"TotalAddressCount\": \"" + getUnicodeString(dtAdd.Rows.Count.ToString()) + "\"}";
                                }
                            }
                            //Success2("0", "Registration Success Your account has been created", aspJson);
                            aspJson += "]";
                            //Success2("0", "Success Login. ", aspJson);

                        //aspJson += "[{\"Userid\": \"" + i + "\"}";

                       // aspJson += "]";
                        objUser = new RegistrationBAL();
                        objUser.RegistrationID = i;
                        DataTable dt = objUser.SelectRegistraionByID();

                        if (dt.Rows.Count != 0 && i > 0)
                        {
                            EmailSender objemail = new EmailSender();
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
                        Success2("0", "You have SuccessFully Created Lea Account.", aspJson);

                    }
                }
            }
            catch (Exception ex)
            {
                Fail("-3", "Cannot connect to server.");
            }
            
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
        //   + "         </td> "
        //   + "     </tr> "
        //   + "     <tr> "
        //   + "         <td valign=\"top\">"
        //   + "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
        //   + "                 <br/>"
        //   + "                       <p class=\"MsoNormal\"> "
        //   + "                           Dear, {%[UserName]%} "
        //   + "                          </p>"

        //   + "                       <br/>"
        //   + "                          <p class=\"MsoNormal\"> "
        //   + "                               To complete your registration and successfully activate your account on LEA eBooks, please copy the link below and paste it in your browser"
        //   + "                           </p>"
        //   + "                       <br/>"

        //   + "                 <span lang=\"EN-IN\"></span><p class=\"MsoNormal\"> "
        //   + "                         <b>Action your acount </b>:<html><body><a href = '  {%[SiteUrl]%} '> {%[SiteUrl]%}  </a><br /></body></html>"
        //   + "                 <p class=\"MsoNormal\"> "
        //   + "                     Once again, thank you for LEA eBooks!</p> "
        //   + "                 <p class=\"MsoNormal\"><br /><br />"
        //   + "                     <span lang=\"EN-IN\">Best Wishes,<br /> "
        //   + "                         LEA eBooks Team.</span></p> "
        //   + "                 <br /> "
        //   + "                 <br /> "
        //   + "                 <p class=\"MsoNormal\"> "
        //   + "                     <span>NOTE :</span> <span>If you have any questions or further query then please contact us:<br />"
        //   + "                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ContactEmail.ToString() + ""
        //   + "                                  </span></p> "
        //   + "                 <br /> "
        //   + "             </font> "
        //   + "         </td> "
        //   + "     </tr> "
        //   + " </table> ";


        string body = string.Empty;
        string file = Server.MapPath("~\\EmailTemplates\\Activation.htm");
        using (StreamReader reader = new StreamReader(file))
        {
            body = reader.ReadToEnd();
        }

        //body = body.Replace("{Email}", SiteUrl);
        //body = body.Replace("{txt_firstname}", UserName);
        body = body.Replace("{ContactEmail}", ContactEmail);
        System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);

        int val = 0;
        //strBody = new System.Text.StringBuilder(body);
        return strBody;
    }

    [WebMethod(Description = "This webservice for Facebook Register User")]
    public void FacebookRegisteration(string Username, string Email, Int32 GenderID, string Birthdaydate, Int32 Languageid)
    {
        try
        {
            string aspJson = "";
            objUser.UserName = Username;
            objUser.EmailAddress = Email;
            objUser.GenderID = GenderID;
            objUser.FirstName = Username;
            objUser.Password = "";
            objUser.IsNewsLetter = false;
            objUser.BirthdayDate = Birthdaydate;
            objUser.UserType = 3;
            objUser.Countryid = 0;
            objUser.LanguageID = Languageid;
            objUser.ActivationID = System.Guid.NewGuid().ToString();
            objUser.IsActive = true;
            try
            {
                objUser.EmailAddress = Email;
                DataTable dt = objUser.GetOneByEmail();
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        objBookDelivery.UserID = Convert.ToInt32(dt.Rows[j]["RegistrationID"].ToString());
                        DataTable dtAdd = objBookDelivery.GetBookAddressByUser();

                        // Count List( Commneted by : jalpa Limbani - 28th nov. 2018

                        // CartList count
                        ObjBookOrders.CustomerID = Convert.ToInt32(dt.Rows[j]["RegistrationID"].ToString());
                        ObjBookOrders.LanguageID = Convert.ToInt32(dt.Rows[j]["LanguageID"].ToString());
                        DataTable dtCart = ObjBookOrders.GetCartList();

                        // MyOrders Count
                        DataTable dtPurchaseBook = new DataTable();
                        objPurchase.UserID = Convert.ToInt32(dt.Rows[j]["RegistrationID"].ToString());
                        dtPurchaseBook = objPurchase.GetAllOrderbyUserID();

                        //WishList count
                        ObjBookOrders.CustomerID = Convert.ToInt32(dt.Rows[j]["RegistrationID"].ToString());
                        ObjBookOrders.LanguageID = Convert.ToInt32(dt.Rows[j]["LanguageID"].ToString());
                        DataTable dt3 = ObjBookOrders.GetWishList();

                        var genID = 0;
                        if (Convert.ToInt32(dt.Rows[j]["GenderID"].ToString()) > 0)
                        {
                            genID = Convert.ToInt32(dt.Rows[j]["GenderID"].ToString()) - 1;
                        }
                        aspJson += "[{\"Userid\": \"" + getUnicodeString(dt.Rows[j]["RegistrationID"].ToString()) + "\"," +
                                   "\"Username\": \"" + getUnicodeString(dt.Rows[j]["UserName"].ToString()) + "\"," +
                                   "\"Email\": \"" + getUnicodeString(dt.Rows[j]["EmailAddress"].ToString()) + "\"," +
                                   "\"GenderID\": \"" + getUnicodeString(genID.ToString()) + "\"," +
                                   "\"Gender\": \"" + getUnicodeString(dt.Rows[j]["Gender"].ToString()) + "\"," +
                                   "\"Birthdaydate\": \"" + getUnicodeString(dt.Rows[j]["BirthdayDate"].ToString()) + "\"," +
                                   "\"LanguageID\": \"" + getUnicodeString(dt.Rows[j]["LanguageID"].ToString()) + "\"," +
                                   "\"Language\": \"" + getUnicodeString(dt.Rows[j]["Language"].ToString()) + "\"," +
                                   "\"CountryID\": \"" + getUnicodeString(dt.Rows[j]["Countryid"].ToString()) + "\"," +
                                   "\"Countryname\": \"" + getUnicodeString(dt.Rows[j]["countryname"].ToString()) + "\"," +
                                    "\"TotalCartCount\": \"" + getUnicodeString(dtCart.Rows.Count.ToString()) + "\"," +
                                   "\"TotalWishListCount\": \"" + getUnicodeString(dt3.Rows.Count.ToString()) + "\"," +
                                   "\"TotalOrderListCount\": \"" + getUnicodeString(dtPurchaseBook.Rows.Count.ToString()) + "\"," +
                                   "\"TotalAddressCount\": \"" + getUnicodeString(dtAdd.Rows.Count.ToString()) + "\"}";
                    }

                    aspJson += "]";
                    Success2("0", "Get Info", aspJson);
                }
                else
                {
                    i = objUser.InsertRegistration();
                    if (i > 0)
                    {
                        objUser.RegistrationID = i;
                        DataTable dt1 = objUser.SelectRegistraionByID();

                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                objBookDelivery.UserID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                DataTable dtAdd = objBookDelivery.GetBookAddressByUser();

                                // CartList count
                                ObjBookOrders.CustomerID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                ObjBookOrders.LanguageID = Convert.ToInt32(dt1.Rows[j]["LanguageID"].ToString());
                                DataTable dtCart = ObjBookOrders.GetCartList();

                                // MyOrders Count
                                DataTable dtPurchaseBook = new DataTable();
                                objPurchase.UserID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                dtPurchaseBook = objPurchase.GetAllOrderbyUserID();

                                //WishList count
                                ObjBookOrders.CustomerID = Convert.ToInt32(dt1.Rows[j]["RegistrationID"].ToString());
                                ObjBookOrders.LanguageID = Convert.ToInt32(dt1.Rows[j]["LanguageID"].ToString());
                                DataTable dt3 = ObjBookOrders.GetWishList();

                                var genID = 0;
                                if (Convert.ToInt32(dt1.Rows[j]["GenderID"].ToString()) > 0)
                                {
                                    genID = Convert.ToInt32(dt1.Rows[j]["GenderID"].ToString()) - 1;
                                }
                                aspJson += "[{\"Userid\": \"" + getUnicodeString(dt1.Rows[j]["RegistrationID"].ToString()) + "\"," +
                                           "\"Username\": \"" + getUnicodeString(dt1.Rows[j]["UserName"].ToString()) + "\"," +
                                           "\"Email\": \"" + getUnicodeString(dt1.Rows[j]["EmailAddress"].ToString()) + "\"," +
                                           "\"GenderID\": \"" + getUnicodeString(dt1.Rows[j]["GenderID"].ToString()) + "\"," +
                                           "\"Birthdaydate\": \"" + getUnicodeString(dt1.Rows[j]["BirthdayDate"].ToString()) + "\"," +
                                            "\"LanguageID\": \"" + getUnicodeString(dt1.Rows[j]["LanguageID"].ToString()) + "\"," +
                                           "\"Language\": \"" + getUnicodeString(dt1.Rows[j]["Language"].ToString()) + "\"," +
                                           "\"CountryID\": \"" + getUnicodeString(dt1.Rows[j]["Countryid"].ToString()) + "\"," +
                                          "\"Countryname\": \"" + getUnicodeString(dt1.Rows[j]["countryname"].ToString()) + "\"," +
                                            "\"TotalCartCount\": \"" + getUnicodeString(dtCart.Rows.Count.ToString()) + "\"," +
                                           "\"TotalWishListCount\": \"" + getUnicodeString(dt3.Rows.Count.ToString()) + "\"," +
                                           "\"TotalOrderListCount\": \"" + getUnicodeString(dtPurchaseBook.Rows.Count.ToString()) + "\"," +
                                           "\"TotalAddressCount\": \"" + getUnicodeString(dtAdd.Rows.Count.ToString()) + "\"}";
                            }
                        }
                        //Success2("0", "Registration Success Your account has been created", aspJson);
                        aspJson += "]";
                        Success2("0", "Success Login. ", aspJson);
                    }

                }
            }
            catch (Exception ex)
            {
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

    [WebMethod(Description = "Add New Address of User")]
    public void AddAddress(string AddressID, string UserID, string Name, string StreetAddress, string Landmark, string City, string State, string Country, string Pincode, string PhoneNumber)
    {
        try
        {
            DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(UserID) + "'");
            if (DtEXist.Rows.Count > 0)
            {
                if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
                {
                    Fail("-1", "This User is Deleted....");
                }
                else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
                {
                    Fail("-5", "This User is In-Active....");
                }
                else if (!string.IsNullOrEmpty(AddressID) && !string.IsNullOrEmpty(UserID))
                {
                    string aspJson = "";
                    if (Convert.ToInt32(AddressID) > 0)
                    {
                        objBookDelivery.BookDeliveryAddID = Convert.ToInt32(AddressID);
                        objBookDelivery.UserID = Convert.ToInt32(UserID);
                        objBookDelivery.IsDefault = false;
                        objBookDelivery.Name = Name;
                        objBookDelivery.StreetAddress = StreetAddress;
                        objBookDelivery.Landmark = Landmark;
                        objBookDelivery.City = City;
                        objBookDelivery.State = State;
                        objBookDelivery.Country = Country;
                        objBookDelivery.Pincode = Pincode;
                        objBookDelivery.PhoneNumber = PhoneNumber;

                        var id = objBookDelivery.InsertBookAddress();

                        objBookDelivery.UserID = Convert.ToInt32(UserID);
                        DataTable dt1 = objBookDelivery.GetBookAddressByUser();

                        if (id > 0)
                        {
                            aspJson += "[{\"TotalAddressCount\": \"" + getUnicodeString(dt1.Rows.Count.ToString()) + "\"," +
                                       "\"AddressID\": \"" + getUnicodeString(id.ToString()) + "\"}";
                            aspJson += "]";

                            Success2("0", "Success", aspJson);
                        }
                        else
                        {
                            Fail("-1", "Address Can't Be Update");
                        }
                    }
                    else if (Convert.ToInt32(AddressID) == 0)
                    {
                        objBookDelivery.BookDeliveryAddID = -1;
                        objBookDelivery.UserID = Convert.ToInt32(UserID);
                        objBookDelivery.IsDefault = false;
                        objBookDelivery.Name = Name;
                        objBookDelivery.StreetAddress = StreetAddress;
                        objBookDelivery.Landmark = Landmark;
                        objBookDelivery.City = City;
                        objBookDelivery.State = State;
                        objBookDelivery.Country = Country;
                        objBookDelivery.Pincode = Pincode;
                        objBookDelivery.PhoneNumber = PhoneNumber;

                        var id = objBookDelivery.InsertBookAddress();

                        objBookDelivery.UserID = Convert.ToInt32(UserID);
                        DataTable dt1 = objBookDelivery.GetBookAddressByUser();

                        if (id > 0)
                        {
                            aspJson += "[{\"TotalAddressCount\": \"" + getUnicodeString(dt1.Rows.Count.ToString()) + "\"," +
                                       "\"AddressID\": \"" + getUnicodeString(id.ToString()) + "\"}";
                            aspJson += "]";

                            Success2("0", "Success", aspJson);
                        }
                        else
                        {
                            Fail("-1", "Address Can't Be Insert");
                        }
                    }
                    else
                    {
                        Fail("-1", "Address Can't Be Insert or Update");
                    }
                }
                else
                {
                    Fail("-2", "Address and User ID must be enter.");
                }
            }
            else
            {
                Fail("-4", "This User is Does not Exist....");
            }
        }
        catch (Exception ex)
        {
            Fail("-3", "Cannot connect to server.");
        }
    }

    [WebMethod(Description = "Delete Address of User")]
    public void DeleteAddress(string AddressID, string UserID)
    {
        try
        {
            if (!string.IsNullOrEmpty(AddressID) && !string.IsNullOrEmpty(UserID))
            {
                objBookDelivery.BookDeliveryAddID = Convert.ToInt32(AddressID);
                objBookDelivery.UserID = Convert.ToInt32(UserID);
                DataTable dt = objBookDelivery.GetBookAddressByUser();
                int addCount = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (AddressID == dt.Rows[i]["BookDeliveryAddID"].ToString())
                        {
                            addCount++;
                            break;
                        }
                    }
                    if (addCount > 0)
                    {
                        objBookDelivery.DeleteUserAddressDetail();
                        dt = objBookDelivery.GetBookAddressByUser();
                        string aspJson = "[{\"TotalAddressCount\": \"" + getUnicodeString(dt.Rows.Count.ToString()) + "\"}";
                        aspJson += "]";

                        Success2("0", "Success", aspJson);
                    }
                    else
                    {
                        Fail("-1", "Address not found for user.");
                    }
                }
                else
                {
                    Fail("-1", "No Address found.");
                }
            }
            else
            {
                Fail("-2", "Address and User ID must be enter.");
            }
        }
        catch (Exception ex)
        {
            Fail("-3", "Cannot connect to server.");
        }
    }

    [WebMethod(Description = "Delete Address of User")]
    public void AddressListing(string UserID)
    {
        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(UserID) + "'");
        try
        {
            if (DtEXist.Rows.Count > 0)
            {
                if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
                {
                    Fail("-1", "This User is Deleted....");
                }
                else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
                {
                    Fail("-5", "This User is In-Active....");
                }
                else
                {
                    if (!string.IsNullOrEmpty(UserID))
                    {
                        string aspJson = "";
                        objBookDelivery.UserID = Convert.ToInt32(UserID);
                        DataTable dt1 = objBookDelivery.GetBookAddressByUser();
                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            aspJson += "[";
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            //for (int j = 0; j < dt1.Tables[0].Rows.Count; j++)
                            {

                                aspJson += "{\"AddressID\": \"" + getUnicodeString(dt1.Rows[j]["BookDeliveryAddID"].ToString()) + "\"," +
                                           "\"UserID\": \"" + getUnicodeString(dt1.Rows[j]["UserID"].ToString()) + "\"," +
                                           "\"IsDefault\": \"" + getUnicodeString(dt1.Rows[j]["IsDefault"].ToString()) + "\"," +
                                           "\"Name\": \"" + getUnicodeString(dt1.Rows[j]["Name"].ToString()) + "\"," +
                                           "\"StreetAddress\": \"" + getUnicodeString(dt1.Rows[j]["StreetAddress"].ToString()) + "\"," +
                                           "\"Landmark\": \"" + getUnicodeString(dt1.Rows[j]["Landmark"].ToString()) + "\"," +
                                           "\"City\": \"" + getUnicodeString(dt1.Rows[j]["City"].ToString()) + "\"," +
                                           "\"State\": \"" + getUnicodeString(dt1.Rows[j]["State"].ToString()) + "\"," +
                                           "\"Country\": \"" + getUnicodeString(dt1.Rows[j]["Country"].ToString()) + "\"," +
                                           "\"Pincode\": \"" + getUnicodeString(dt1.Rows[j]["Pincode"].ToString()) + "\"," +
                                           "\"PhoneNumber\": \"" + getUnicodeString(dt1.Rows[j]["PhoneNumber"].ToString()) + "\"},";
                            }

                            aspJson = aspJson.TrimEnd(',');
                            aspJson += "]";
                            var Count = Convert.ToInt32(dt1.Rows.Count);
                            Success15("0", "Success", Count, aspJson);
                        }
                        else
                        {
                            Fail("-1", "No record found.");
                        }
                    }

                    else
                    {
                        Fail("-2", "User ID must be enter.");

                    }
                }
            }
            else
            {
                Fail("-4", "This User is Does not Exist....");
            }

        }
        catch (Exception ex)
        {
            Fail("-3", "Cannot connect to server.");
        }
    }

    [WebMethod(Description = "This Webservice for view all orders")]
    public void MyOrders(string UserID)
    {
        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(UserID) + "'");

        if (DtEXist.Rows.Count > 0)
        {
            if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
            {
                Fail("-1", "This User is Deleted....");
            }
            else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
            {
                Fail("-5", "This User is In-Active....");
            }
            else
            {
                if (!string.IsNullOrEmpty(UserID))
                {
                    string aspJsonStr = "";
                    DataTable dtPurchaseBook = new DataTable();
                    objPurchase.UserID = Convert.ToInt32(UserID);
                    dtPurchaseBook = objPurchase.GetAllOrderbyUserID();

                    if (dtPurchaseBook != null && dtPurchaseBook.Rows.Count > 0)
                    {
                        aspJsonStr += "[";
                        for (int i = 0; i < dtPurchaseBook.Rows.Count; i++)
                        {
                            var BookTypeID = 0;
                            if (Convert.ToBoolean(dtPurchaseBook.Rows[i]["IseBook"]))
                                BookTypeID = 1;
                            else if (Convert.ToBoolean(dtPurchaseBook.Rows[i]["IsPaperBook"]))
                                BookTypeID = 2;

                            aspJsonStr += "{";
                            aspJsonStr += "\"OrderID\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["OrderID"].ToString()) + "\"," +
                                       "\"Autohername\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["Autoher"].ToString()) + "\"," +
                                       "\"BookID\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["BookID"].ToString()) + "\"," +
                                       "\"Bookname\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["Title"].ToString()) + "\"," +
                                       "\"Status\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["DeliveryStatus"].ToString()) + "\"," +
                                        "\"Language\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["Language"].ToString()) + "\"," +
                                       "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dtPurchaseBook.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dtPurchaseBook.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                                       "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dtPurchaseBook.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dtPurchaseBook.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                                       //"\"DeliveryAddress\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["FullAddress"].ToString()) + "\"," +
                                       "\"Pricecurrency\": \"$\" ," +
                                       "\"TotalAmount\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["Amount"].ToString()) + "\"," +
                                       "\"ShippingCharge\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["ShippingCharge"].ToString()) + "\"," +
                                       "\"ShippingType \": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["ShippingType"].ToString()) + "\"," +
                                       "\"BookTypeID\": \"" + getUnicodeString(BookTypeID.ToString()) + "\"," +
                                       "\"DeliveryDate\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["DeliveryDate"].ToString()) + "\"," +
                                       "\"Quantity\": \"" + getUnicodeString(Convert.ToInt32(dtPurchaseBook.Rows[i]["Qauntity"]) == 0 ? "1" : dtPurchaseBook.Rows[i]["Qauntity"].ToString()) + "\"," +
                                       "\"OrderStatus\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["OrderStatus"].ToString()) + "\"," +
                                       "\"TrackingNumber\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AppCode"].ToString()) + "\"," +
                                       "\"OrderDate\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["PurchaseDate"].ToString()) + "\",";
                            aspJsonStr += "\"AddName\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddName"].ToString()) + "\"," +
                                          "\"AddStreetAddress\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddStreetAddress"].ToString()) + "\"," +
                                          "\"AddLandmark\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddLandmark"].ToString()) + "\"," +
                                          "\"AddCity\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddCity"].ToString()) + "\"," +
                                          "\"AddState\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddState"].ToString()) + "\"," +
                                          "\"AddCountry\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddCountry"].ToString()) + "\"," +
                                          "\"AddPincode\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddPincode"].ToString()) + "\"," +
                                          "\"AddPhone\": \"" + getUnicodeString(dtPurchaseBook.Rows[i]["AddPhone"].ToString()) + "\"";
                            aspJsonStr += "},";
                        }
                        aspJsonStr = aspJsonStr.TrimEnd(',');
                        aspJsonStr += "]";
                        var Count = Convert.ToInt32(dtPurchaseBook.Rows.Count);
                        //Success2("0", "Get list", aspJsonStr);
                        Success15("0", "Get List", Count, aspJsonStr);
                    }
                    else
                    {
                        Fail("-1", "No Data Available");
                    }
                }
                else
                {
                    Fail("-1", "Please Pass UserID");
                }
            }
        }
        else
        {
            Fail("-4", "This User is Does not Exist....");
        }
    }

    [WebMethod(Description = "This webservice for insert user")]
    public void Updateprofile(string UserID, string Username, string Email, Int32 GenderID, string OldPassword, string Password, string Birthdaydate, Int32 Countryid, Int32 Languageid)
    {
        try
        {
            string aspJson = "";
            objUser.RegistrationID = Convert.ToInt32(UserID);
            objUser.UserName = Username;
            objUser.EmailAddress = Email;
            objUser.GenderID = GenderID;
            objUser.FirstName = Username;
            objUser.LastName = "";
            objUser.Password = Password;
            objUser.IsNewsLetter = false;
            objUser.IsActive = true;
            objUser.BirthdayDate = Birthdaydate.ToString();
            objUser.UserType = 3;
            objUser.Countryid = Countryid;
            objUser.LanguageID = Languageid;
            objUser.ActivationID = System.Guid.NewGuid().ToString();
            DataTable dt1 = objUser.SelectRegistraionByID();

            if (!string.IsNullOrEmpty(OldPassword))
            {

                if (Convert.ToString(dt1.Rows[0]["Password"]) == OldPassword)
                {
                    i = objUser.UpdateRegistration();
                    if (i > 0)
                    {

                        objUser.RegistrationID = Convert.ToInt32(UserID);
                        DataTable dt = objUser.SelectRegistraionByID();

                        // Count List( Commneted by : jalpa Limbani - 28th nov. 2018

                        //Address count
                        objBookDelivery.UserID = Convert.ToInt32(UserID);
                        DataTable dtAdd = objBookDelivery.GetBookAddressByUser();

                        // CartList count
                        ObjBookOrders.CustomerID = Convert.ToInt32(UserID);
                        ObjBookOrders.LanguageID = Languageid;
                        DataTable dtCart = ObjBookOrders.GetCartList();

                        // MyOrders Count
                        DataTable dtPurchaseBook = new DataTable();
                        objPurchase.UserID = Convert.ToInt32(UserID);
                        dtPurchaseBook = objPurchase.GetAllOrderbyUserID();

                        //WishList count
                        ObjBookOrders.CustomerID = Convert.ToInt32(UserID);
                        ObjBookOrders.LanguageID = Convert.ToInt32(dt1.Rows[0]["LanguageID"].ToString());
                        DataTable dt3 = ObjBookOrders.GetWishList();


                        if (dt.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                var genID = GenderID;
                                var gen = dt.Rows[j]["Gender"].ToString();
                                switch (GenderID)
                                {
                                    case 0:
                                        gen = "Male";
                                        break;
                                    case 1:
                                        gen = "Female";
                                        break;
                                }

                                aspJson += "[{\"Userid\": \"" + getUnicodeString(dt.Rows[j]["RegistrationID"].ToString()) + "\"," +
                                           "\"Username\": \"" + getUnicodeString(dt.Rows[j]["UserName"].ToString()) + "\"," +
                                           "\"Email\": \"" + getUnicodeString(dt.Rows[j]["EmailAddress"].ToString()) + "\"," +
                                           "\"GenderID\": \"" + getUnicodeString(genID.ToString()) + "\"," +
                                           "\"Gender\": \"" + getUnicodeString(gen) + "\"," +
                                           "\"Birthdaydate\": \"" + getUnicodeString(dt.Rows[j]["BirthdayDate"].ToString()) + "\"," +
                                            "\"LanguageID\": \"" + getUnicodeString(dt.Rows[j]["LanguageID"].ToString()) + "\"," +
                                           "\"Language\": \"" + getUnicodeString(dt.Rows[j]["Language"].ToString()) + "\"," +
                                           "\"CountryID\": \"" + getUnicodeString(dt.Rows[j]["Countryid"].ToString()) + "\"," +
                                           "\"Countryname\": \"" + getUnicodeString(dt.Rows[j]["countryname"].ToString()) + "\"," +
                                            "\"TotalCartCount\": \"" + getUnicodeString(dtCart.Rows.Count.ToString()) + "\"," +
                                           "\"TotalWishListCount\": \"" + getUnicodeString(dt3.Rows.Count.ToString()) + "\"," +
                                           "\"TotalOrderListCount\": \"" + getUnicodeString(dtPurchaseBook.Rows.Count.ToString()) + "\"," +
                                           "\"TotalAddressCount\": \"" + getUnicodeString(dtAdd.Rows.Count.ToString()) + "\"}";


                            }
                        }
                        aspJson += "]";

                        Success2("0", "Updated Sucessfully", aspJson);

                    }
                    else
                    {
                        //retJSON = Fail1("Email already exists", 0);
                        Fail("-1", "Please Check your email - Id");
                    }
                }
                else
                {
                    Fail("-1", "Please insert Old password correct.");
                }

            }
            else
            {
                dbconnection d = new dbconnection();
                //DataTable dt = d.filltable("update Registration set FirstName='"+ Username + "' ,EmailAddress='"+ Email + "' ,Countryid='"+Countryid +"' where RegistrationID='"+ Convert.ToInt32(UserID) + "' ");
                //int data="update Registration set FirstName='" + Username + "' ,EmailAddress='" + Email + "' ,Countryid='" + Countryid + "' where RegistrationID='" + Convert.ToInt32(UserID) + "' ";
                // if (data.Count >0 )
                // {
                //     data = objUser.UpdateRegistration();
                // }
                // return result;
                objUser.UserName = Username;
                objUser.EmailAddress = Email;
                objUser.GenderID = GenderID;
                objUser.FirstName = Username;
                objUser.Countryid = Countryid;
                objUser.LanguageID = Languageid;
                objUser.ActivationID = System.Guid.NewGuid().ToString();
                DataTable dt2 = objUser.SelectRegistraionByID();
                i = objUser.UpdateRegistration();
                if (i > 0)
                {

                    objUser.RegistrationID = Convert.ToInt32(UserID);
                    DataTable dt = objUser.SelectRegistraionByID();


                    // Count List( Commneted by : jalpa Limbani - 28th nov. 2018

                    //Address count
                    objBookDelivery.UserID = Convert.ToInt32(UserID);
                    DataTable dtAdd = objBookDelivery.GetBookAddressByUser();

                    // CartList count
                    ObjBookOrders.CustomerID = Convert.ToInt32(UserID);
                    ObjBookOrders.LanguageID = Languageid;
                    DataTable dtCart = ObjBookOrders.GetCartList();

                    // MyOrders Count
                    DataTable dtPurchaseBook = new DataTable();
                    objPurchase.UserID = Convert.ToInt32(UserID);
                    dtPurchaseBook = objPurchase.GetAllOrderbyUserID();

                    //WishList count
                    ObjBookOrders.CustomerID = Convert.ToInt32(UserID);
                    ObjBookOrders.LanguageID = Convert.ToInt32(dt2.Rows[0]["LanguageID"].ToString());
                    DataTable dt3 = ObjBookOrders.GetWishList();


                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            var genID = GenderID;
                            var gen = dt.Rows[j]["Gender"].ToString();
                            switch (GenderID)
                            {
                                case 0:
                                    gen = "Male";
                                    break;
                                case 1:
                                    gen = "Female";
                                    break;
                            }

                            aspJson += "[{\"Userid\": \"" + getUnicodeString(dt.Rows[j]["RegistrationID"].ToString()) + "\"," +
                                       "\"Username\": \"" + getUnicodeString(dt.Rows[j]["UserName"].ToString()) + "\"," +
                                       "\"Email\": \"" + getUnicodeString(dt.Rows[j]["EmailAddress"].ToString()) + "\"," +
                                       "\"GenderID\": \"" + getUnicodeString(genID.ToString()) + "\"," +
                                       "\"Gender\": \"" + getUnicodeString(gen) + "\"," +
                                       "\"Birthdaydate\": \"" + getUnicodeString(dt.Rows[j]["BirthdayDate"].ToString()) + "\"," +
                                        "\"LanguageID\": \"" + getUnicodeString(dt.Rows[j]["LanguageID"].ToString()) + "\"," +
                                       "\"Language\": \"" + getUnicodeString(dt.Rows[j]["Language"].ToString()) + "\"," +
                                       "\"CountryID\": \"" + getUnicodeString(dt.Rows[j]["Countryid"].ToString()) + "\"," +
                                       "\"Countryname\": \"" + getUnicodeString(dt.Rows[j]["countryname"].ToString()) + "\"," +
                                        "\"TotalCartCount\": \"" + getUnicodeString(dtCart.Rows.Count.ToString()) + "\"," +
                                       "\"TotalWishListCount\": \"" + getUnicodeString(dt3.Rows.Count.ToString()) + "\"," +
                                       "\"TotalOrderListCount\": \"" + getUnicodeString(dtPurchaseBook.Rows.Count.ToString()) + "\"," +
                                       "\"TotalAddressCount\": \"" + getUnicodeString(dtAdd.Rows.Count.ToString()) + "\"}";
                        }
                    }
                    aspJson += "]";

                    Success2("0", "Updated Sucessfully", aspJson);
                }
                else
                {
                    Fail("-1", "Please Check your email - Id");
                }
            }


            // Context.Response.Output.Write(retJSON);
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    //[WebMethod(Description = "This webservice for log in")]
    //public void Login(string FacebookEmail, string EmailAddress, string Password, string DeviceID, string Department)
    //{
    //    objUser.FacebookEmail = FacebookEmail;
    //    objUser.EmailAddress = EmailAddress;
    //    objUser.Password = Password;
    //    DataTable dt = objUser.Login();
    //    if (dt.Rows.Count > 0 && dt.Rows[0]["IsActive"].ToString() == "1")
    //    {
    //        if (Department == "")
    //        {
    //            Department = "0";
    //        }
    //        InsertIphonePushNotification(DeviceID, Convert.ToInt32(dt.Rows[0]["RegistrationID"]), Convert.ToInt32(Department));
    //        dt.TableName = "result";
    //        Context.Response.Output.Write(JSON_DataTable(dt));
    //    }
    //    else
    //    {
    //        if (dt.Rows.Count > 0)
    //        {
    //            if (dt.Rows[0]["IsActive"].ToString() == "0")
    //            {
    //                retJSON = Fail2("Email not activate", 0);

    //                Context.Response.Output.Write(retJSON);
    //            }
    //        }
    //        else
    //        {

    //            Context.Response.Output.Write(Message("Please enter valid username or password."));
    //        }
    //    }

    //}

    //[WebMethod(Description = "This webservice for log in")]
    //public void Login(string Username, string Password, Int16 Languageid)
    //{
    //    try
    //    {
    //        objUser.UserName = Username;
    //        objUser.Password = Password;
    //        objUser.FacebookEmail = "";
    //        DataTable dt = objUser.Login();
    //        string aspJson = "";
    //        if (dt.Rows.Count > 0 && dt.Rows[0]["IsActive"].ToString() == "1")
    //        {
    //            //if (Department == "")
    //            //{
    //            //    Department = "0";
    //            //}
    //            //InsertIphonePushNotification(DeviceID, Convert.ToInt32(dt.Rows[0]["RegistrationID"]), Convert.ToInt32(Department));
    //            //dt.TableName = "result";
    //            //Context.Response.Output.Write(JSON_DataTable(dt));
    //            {

    //                aspJson += "[{\"Userid\": \"" + dt.Rows[i]["RegistrationID"].ToString() + "\"," +
    //                           "\"Username \": \"" + dt.Rows[i]["UserName"].ToString().Replace("/", "\\/") + "\"," +
    //                           "\"Email \": \"" + dt.Rows[i]["EmailAddress"].ToString().Replace("/", "\\/") + "\"," +
    //                           "\"Gender \": \"" + dt.Rows[i]["Gender"].ToString().Replace("/", "\\/") + "\"," +
    //                           "\"Birthdaydate \": \"" + dt.Rows[i]["BirthdayDate"].ToString() + "\"," +
    //                            "\"Language \": \"" + dt.Rows[i]["Language"].ToString() + "\"," +
    //                           "\"Countryname \": \"" + dt.Rows[i]["countryname"].ToString() + "\"}";
    //            }
    //            aspJson += "]";

    //            Success2("0", " Success Login.", aspJson);
    //        }
    //        else
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                if (dt.Rows[0]["IsActive"].ToString() == "0")
    //                {
    //                    Fail("-2", " Your account is not activated yet so first activate your account by clicking on the activation link which has been sent to your email.");
    //                }
    //            }
    //            else
    //            {

    //                Fail("-1", "Please check the username and password.");
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Fail("-3", "Cannot connect to server.");
    //    }

    //}

    [WebMethod(Description = "This webservice for log in")]
    public void Login(string Email, string Password, Int16 Languageid, string Deviceid, string DeviceType)
    {
        try
        {
            objUser.EmailAddress = Email;
            objUser.Password = Password;
            objUser.Deviceid = Deviceid;
            objUser.DeviceType = DeviceType;
            objUser.FacebookEmail = "";
            DataTable dt = objUser.Login();
            string aspJson = "";
            if (dt.Rows.Count > 0 && dt.Rows[0]["IsActive"].ToString() == "1")
            {
                {
                    objBookDelivery.UserID = Convert.ToInt32(dt.Rows[i]["RegistrationID"].ToString());
                    DataTable dtAdd = objBookDelivery.GetBookAddressByUser();

                    ObjBookOrders.CustomerID = Convert.ToInt32(dt.Rows[i]["RegistrationID"].ToString());
                    ObjBookOrders.LanguageID = Languageid;
                    DataTable dtCart = ObjBookOrders.GetCartList();

                    DataTable dtPurchaseBook = new DataTable();
                    objPurchase.UserID = Convert.ToInt32(dt.Rows[i]["RegistrationID"].ToString());
                    dtPurchaseBook = objPurchase.GetAllOrderbyUserID();

                    ObjBookOrders.CustomerID = Convert.ToInt32(dt.Rows[i]["RegistrationID"].ToString());
                    ObjBookOrders.LanguageID = Convert.ToInt32(dt.Rows[i]["LanguageID"].ToString());
                    DataTable dt3 = ObjBookOrders.GetWishList();

                    aspJson += "[{\"Userid\": \"" + getUnicodeString(dt.Rows[i]["RegistrationID"].ToString()) + "\"," +
                               "\"Username\": \"" + getUnicodeString(dt.Rows[i]["UserName"].ToString()) + "\"," +
                               "\"Email\": \"" + getUnicodeString(Email) + "\"," +
                               //"\"GenderID\": \"" + getUnicodeString(dt.Rows[i]["GenderID"].ToString()) + "\"," +
                               "\"GenderID\": \"" + 0 + "\"," +
                               //"\"Gender\": \"" + getUnicodeString(dt.Rows[i]["Gender"].ToString()) + "\"," +
                               "\"Gender\": \"" + "" + "\"," +
                               //"\"Birthdaydate\": \"" + getUnicodeString(Convert.ToDateTime(dt.Rows[i]["BirthdayDate"].ToString()).ToString("MM/dd/yyyy")) + "\"," +
                               "\"Birthdaydate\": \"" + "" + "\"," +
                                "\"LanguageID\": \"" + getUnicodeString(dt.Rows[i]["LanguageID"].ToString()) + "\"," +
                               "\"Language\": \"" + getUnicodeString(dt.Rows[i]["Language"].ToString()) + "\"," +
                               "\"CountryID\": \"" + getUnicodeString(dt.Rows[i]["Countryid"].ToString()) + "\"," +
                               "\"Countryname\": \"" + getUnicodeString(dt.Rows[i]["countryname"].ToString()) + "\"," +
                               "\"TotalCartCount\": \"" + getUnicodeString(dtCart.Rows.Count.ToString()) + "\"," +
                               "\"TotalWishListCount\": \"" + getUnicodeString(dt3.Rows.Count.ToString()) + "\"," +
                               "\"TotalOrderListCount\": \"" + getUnicodeString(dtPurchaseBook.Rows.Count.ToString()) + "\"," +
                               "\"TotalAddressCount\": \"" + getUnicodeString(dtAdd.Rows.Count.ToString()) + "\"}";
                }
                aspJson += "]";

                Success2("0", " Success Login.", aspJson);
            }
            else
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["IsActive"].ToString() == "0")
                    {
                        Fail("-2", " Your account is not activated yet so first activate your account by clicking on the activation link which has been sent to your email.");
                    }
                }
                else
                {
                    Fail("-1", "Please check the Email and password.");
                }
            }
        }
        catch (Exception ex)
        {
            Fail("-3", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for getting Country")]
    public void GetAllCountry()
    {
        string aspJson = "";
        DataTable dt = objCountry.SelectAllCountry();
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                aspJson += "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    aspJson += "{";
                    aspJson += "\"Countryid\": \"" + getUnicodeString(dt.Rows[i]["countryid"].ToString()) + "\"," +
                                "\"RegionID\": \"" + getUnicodeString(dt.Rows[i]["Rid"].ToString()) + "\"," +
                                "\"RegionName\": \"" + getUnicodeString(dt.Rows[i]["Region"].ToString()) + "\"," +
                                   "\"Countryname\": \"" + getUnicodeString(dt.Rows[i]["countryname"].ToString()) + "\"," +
                                   "\"Countryflag\": \"" + getUnicodeString(dt.Rows[i]["countryimage"].ToString()) + "\"";
                    aspJson += "},";

                }
                aspJson = aspJson.TrimEnd(',');
                aspJson += "]";
                Success2("0", "countrylist", aspJson);
            }
            else
            {
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for getting Language")]
    public void GetAllLanguage()
    {
        string aspJson = "";
        DataTable dt = objCountry.SelectAllLanguage();
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                aspJson += "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    aspJson += "{";
                    aspJson += "\"Languageid\": \"" + getUnicodeString(dt.Rows[i]["ID"].ToString()) + "\"," +

                                   "\"Languagename\": \"" + getUnicodeString(dt.Rows[i]["Language"].ToString()) + "\"";
                    aspJson += "},";

                }
                aspJson = aspJson.TrimEnd(',');
                aspJson += "]";
                Success2("0", "List of languages.", aspJson);
            }
            else
            {
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }
    }

    //[WebMethod(Description = "This webservice for forgot password")]
    //public void ForgotPassword(string EmailAddress)
    //{
    //    objUser.EmailAddress = EmailAddress;

    //    if (Global.EmailAddressCheck(EmailAddress))
    //    {
    //        objUser.EmailAddress = EmailAddress;
    //        DataTable dt = objUser.ForgotPassword();
    //        if (dt.Rows.Count > 0 && dt != null)
    //        {
    //            string body = "";

    //            body = body +
    //         "                <head> "
    //        + "    <style type=\"text/css\"> "
    //        + "        p.MsoNormal "
    //        + "        { "
    //        + "            margin-top: 0in; "
    //        + "            margin-right: 0in; "
    //        + "            margin-bottom: 10.0pt; "
    //        + "            margin-left: 0in; "
    //        + "            line-height: 115%; "
    //        + "            font-size: 11.0pt; "
    //        + "            font-family: \"Calibri\" , \"sans-serif\"; "
    //        + "        } "
    //        + "    </style> "
    //        + "</head> "
    //        + "<table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"2\" > "
    //        + "    <tr> "
    //        + "        <td style=\"background-color: rgb(10, 109, 10);\"> "
    //        + "            <img src=\"http://themagz.net/client/images/logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" /> "
    //        + "        </td> "
    //        + "    </tr> "
    //        + "    <tr> "
    //        + "        <td valign=\"top\"> "
    //        + "            <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
    //        + "                <br /> ";






    //            body = body + "<table>";
    //            body = body + "<tr>";
    //            body = body + "<td>Dear User, ";
    //            body = body + "</td>";
    //            body = body + "</tr>";
    //            body = body + "<tr>";
    //            body = body + "<td> Your Credentials for theMagz.net are as below. ";
    //            body = body + "</td>";
    //            body = body + "</tr>";
    //            //body = body + "<tr>";
    //            //body = body + "<td> URL: http://theMagz.net/Admin/Default.aspx";
    //            //body = body + "</td>";
    //            body = body + "</tr>";
    //            body = body + "<tr>";
    //            body = body + "<td> User Name : " + dt.Rows[0]["EmailAddress"].ToString() + " ";
    //            body = body + "</td>";
    //            body = body + "</tr>";
    //            body = body + "<tr>";
    //            body = body + "<td> User Password : " + dt.Rows[0]["Password"].ToString() + " ";
    //            body = body + "</td>";
    //            body = body + "</tr>";
    //            body = body + "<tr>";
    //            body = body + "<td>Thanks & Regards,";
    //            body = body + "</td>";
    //            body = body + "</tr>";
    //            body = body + "<tr>";
    //            body = body + "<td>theMagz.net";
    //            body = body + "</td>";
    //            body = body + "</tr>";
    //            body = body + "</table>";


    //            body = body + " </font>"
    //                            + "        </td> "
    //                            + "    </tr> "
    //                            //+ "    <tr> "
    //                            //+ "        <td align=\"center\"> "
    //                            //+ "            <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#9b98d2\">{%[CopyRight]%}</font> "
    //                            //+ "        </td> "
    //                            //+ "    </tr> "
    //                            + "</table> ";





    //            try
    //            {
    //                Global.SendEmail(EmailAddress, "Forgot Password", body);
    //                retJSON = Success("Password has been sent successfully to your email address");
    //            }
    //            catch (Exception)
    //            {

    //                throw;
    //            }

    //        }
    //        else
    //        {
    //            retJSON = Message("Email Address not found.");
    //        }
    //    }
    //    else
    //    {
    //        retJSON = Message("Email Address not in valid format.");
    //    }

    //    Context.Response.Output.Write(retJSON);
    //}

    //[WebMethod(Description = "This webservice to change password")]
    //public void ChangePassword(Int64 UserID, string NewPassword, string oldpassword)
    //{
    //    objUser.RegistrationID = UserID;
    //    DataTable dt = objUser.SelectRegistraionByID();

    //    if (dt.Rows[0]["Password"].ToString() == oldpassword)
    //    {
    //        objUser.Password = NewPassword;
    //        objUser.ChangePassword();
    //        retJSON = Success("Password changed successfully.");
    //    }


    //    else
    //    {
    //        retJSON = Message("Password does not match");
    //    }

    //    Context.Response.Output.Write(retJSON);
    //}

    //[WebMethod(Description = "This webservice for getting category")]
    //public void GetAllCategory()
    //{

    //    DataTable dt = objCategory.wsSelectAllCartegory();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        dt.TableName = "result";
    //        retJSON = JSON_DataTable(dt);
    //    }
    //    else
    //    {
    //        retJSON = Message("No category found.");
    //    }

    //    Context.Response.Output.Write(retJSON);
    //}


    //[WebMethod(Description = "This webservice for search Book by category")]
    //public void BookList(string Book, int BookID, int CategoryID, int CurrentPage, int NoofDisplayRows, int IsFree, int IsFeature, int IsNewArrival, int IsTop10, String Country_ISOcode)
    //{
    //    objBook.Title = Book;
    //    objBook.CurrentPage = CurrentPage;
    //    objBook.Rows = NoofDisplayRows;
    //    objBook.WsIsFree = IsFree;
    //    objBook.BookID = BookID;
    //    objBook.CategoryID = CategoryID;
    //    objBook.CategoryID = CategoryID;
    //    objBook.IsFeatured = IsFeature;
    //    objBook.IsNewArrival = IsNewArrival;
    //    objBook.IsTop10 = IsTop10;
    //    objBook.ISOCode = Country_ISOcode;
    //    DataTable DT = objBook.BookList();
    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

    //    if (DT != null && DT.Rows.Count > 0)
    //    {
    //        string aspJson = "";
    //        aspJson += "[{ \"BookApp\": {\"Book\": [";
    //        for (int i = 0; i <= DT.Rows.Count - 1; i++)
    //        {
    //            aspJson += "{\"CategoryName\": \"" + DT.Rows[i]["CategoryName"].ToString() + "\"," +
    //                       "\"Date\": \"" + DT.Rows[i]["Date"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"Image\": \"" + DT.Rows[i]["Image"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"Image1\": \"" + DT.Rows[i]["Image1"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"BookID\": \"" + DT.Rows[i]["BookID"].ToString() + "\"," +
    //                       "\"Description\": \"" + DT.Rows[i]["Description"].ToString().Replace("\t", "").Replace("\n", "").Replace("  ", "").Trim() + "\"," +
    //                       "\"Title\": \"" + DT.Rows[i]["Title"].ToString() + "\"},";
    //        }
    //        aspJson += "]}}]";
    //        Context.Response.Output.Write(RemoveHTML(aspJson.Remove(aspJson.Length - 5, 1)));
    //    }
    //    else
    //    {
    //        retJSON = Message("No Book found.");
    //    }

    //    Context.Response.Output.Write(retJSON);
    //}

    //[WebMethod(Description = "This webservice for search Book issue by Book")]
    //public void BookIssueList(int BookID, int CurrentPage, int NoofDisplayRows)
    //{
    //    objBook.BookID = BookID;
    //    DataTable DT = objBook.SelectAllBookIssuePaging(CurrentPage, NoofDisplayRows);
    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

    //    if (DT != null && DT.Rows.Count > 0)
    //    {
    //        string aspJson = "";
    //        aspJson += "[{ \"BookApp\": {\"Book\": [";
    //        for (int i = 0; i <= DT.Rows.Count - 1; i++)
    //        {
    //            aspJson += "{\"CategoryName\": \"" + DT.Rows[i]["CategoryName"].ToString() + "\"," +
    //                       "\"Date\": \"" + DT.Rows[i]["CreatedOn"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"Image\": \"" + DT.Rows[i]["Image"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"BookID\": \"" + DT.Rows[i]["ID"].ToString() + "\"," +
    //                       "\"Title\": \"" + DT.Rows[i]["Title"].ToString() + "\"},";
    //        }
    //        aspJson += "]}}]";
    //        Context.Response.Output.Write(aspJson.Remove(aspJson.Length - 5, 1));
    //    }
    //    else
    //    {
    //        retJSON = Message("No Book found.");
    //    }

    //    Context.Response.Output.Write(retJSON);
    //}

    //[WebMethod(Description = "This webservice for search Book issue by Book")]
    //public void UserBookList(int UserID, int column, int order)
    //{

    //    DataTable DT = objBook.UserBookList(UserID, column, order);
    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

    //    if (DT != null && DT.Rows.Count > 0)
    //    {
    //        string aspJson = "";

    //        aspJson += "[{ \"BookApp\": {\"Book\": [";
    //        for (int i = 0; i <= DT.Rows.Count - 1; i++)
    //        {
    //            aspJson += "{\"CategoryName\": \"" + DT.Rows[i]["CategoryName"].ToString() + "\"," +
    //                       "\"Date\": \"" + Convert.ToDateTime(DT.Rows[i]["Date"].ToString()).ToString("MM/dd/yyyy").Replace("/", "\\/") + "\"," +
    //                       "\"Time\": \"" + Convert.ToDateTime(DT.Rows[i]["Date"].ToString()).ToString("hh:mm tt").Replace("/", "\\/") + "\"," +
    //                       "\"Image\": \"" + DT.Rows[i]["Image"].ToString().Replace("/", "\\/") + "\"," +
    //                        "\"PdfPath\": \"" + DT.Rows[i]["PdfPath"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"BookID\": \"" + DT.Rows[i]["BookID"].ToString() + "\"," +
    //                        "\"BookID\": \"" + DT.Rows[i]["BookID"].ToString() + "\"," +
    //                       "\"Title\": \"" + DT.Rows[i]["Title"].ToString() + "\"},";
    //        }
    //        aspJson += "]}}]";
    //        Context.Response.Output.Write(aspJson.Remove(aspJson.Length - 5, 1));
    //    }
    //    else
    //    {
    //        retJSON = Message("No Book found.");
    //    }
    //    Context.Response.Output.Write(retJSON);
    //}

    [WebMethod(Description = "This webservice for search Book by ID")]
    public System.Xml.XmlElement BookDetials(string BookID, string UserID, string LanguageID)
    {
        Security S = new Security();
        string l = LanguageID.ToLower();
        if (l != null && l == "en-us")
        {
            l = "1";
        }
        else if (l != null && l == "es-es")
        {
            l = "2";
        }
        string PurchaseStatus = "0";
        string SubscribStatus = "0";
        if (UserID != "" && UserID != "0")
        {
            ObjBookOrders.CustomerID = Convert.ToInt32(UserID);
            ObjBookOrders.BookID = Convert.ToInt32(BookID);
            if (ObjBookOrders.CheckIteminPurchased().Rows[0][0].ToString() == "1")
            {
                PurchaseStatus = "1";
                SubscribStatus = "1";
            }
        }

        objBook.BookID = Convert.ToInt32(BookID);
        objBook.LanguageID = Convert.ToInt32(l);

        //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        DataTable dt = objBook.getBookDetails();
        System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView();
            dv = dt.AsDataView();
            DateTime date = Convert.ToDateTime(dt.Rows[0]["CreatedOn"].ToString());
            string str = "<?xml version='1.0' encoding='UTF-8' ?>";
            str += "<BookApp>";

            str += "<Book>" +
          "<BookID>" + dt.Rows[0]["BookID"].ToString() + "</BookID>" +
             "<Title>" + dt.Rows[0]["Title1"].ToString().Replace("&", "And") + "</Title>" +
             "<Image>" + Config.WebSiteMain + "Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["ImagePath"].ToString() + "</Image>" +
               //"<BookID>" + dt.Rows[0]["BookID"].ToString() + "</BookID>" +
               "<shareURL>" + ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book-Detail.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dt.Rows[i]["BookID"]))) + "&amp;title=" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[i]["Title1"])) + "</shareURL>" +

                 "<Date>" + date.ToString("MM/dd/yyyy") + "</Date>" +
                  "<Time>" + date.ToString("hh:mm tt") + "</Time>" +
                  "<PurchaseStatus>" + PurchaseStatus + "</PurchaseStatus>" +
                  "<SubscribStatus>" + SubscribStatus + "</SubscribStatus>" +
                 "<CategoryName>" + dt.Rows[0]["CategoryName"].ToString().Replace("&", "And") + "</CategoryName>" +
                     //"<CategoryName>" + dt.Rows[0]["CategoryName"].ToString() + "</CategoryName>" +
                     //"<CountryName>" + dt.Rows[0]["countryname"].ToString() + "</CountryName>" +
                     //"<CountryImage>" + dt.Rows[0]["countryimage"].ToString() + "</CountryImage>" +
                     "<SubscriptionPrice>" + dt.Rows[0]["FinalPrice"].ToString() + "</SubscriptionPrice>" +
                     "<SubscriptionIssue>" + dt.Rows[0]["SubscriptionIssue"].ToString() + "</SubscriptionIssue>" +
                  "<Price>" + dt.Rows[0]["FinalPrice"].ToString() + "</Price>" +
                    "<Description>" + dt.Rows[0]["Description1"].ToString().Replace("&", "And") + "</Description>" +
                     "<Language>" + dt.Rows[0]["Language1"].ToString() + "</Language>" +
                        "</Book>";

            str += "</BookApp>";
            //xdd.LoadXml(str);
            //Context.Response.Output.Write(XmlToJSON(xdd));
            xdd.LoadXml(str);


        }
        else
        {

            //retJSON = "{\"code\":\"0\"}";
            string str = "<BookApp></BookApp>";
            xdd.LoadXml(str);
        }

        // Context.Response.Output.Write(retJSON);
        return xdd.DocumentElement;
    }

    [WebMethod(Description = "This webservice for search Book by ID")]
    public System.Xml.XmlElement specialOfferBooks(string LanguageID)
    {
        Security S = new Security();
        string l = LanguageID.ToLower();
        if (l != null && l == "en-us")
        {
            l = "1";
        }
        else if (l != null && l == "es-es")
        {
            l = "2";
        }
        string PurchaseStatus = "0";
        string SubscribStatus = "0";
        //if (UserID != "" && UserID != "0")
        //{
        //    ObjBookOrders.CustomerID = Convert.ToInt32(UserID);
        //    ObjBookOrders.BookID = Convert.ToInt32(BookID);
        //    if (ObjBookOrders.CheckIteminPurchased().Rows[0][0].ToString() == "1")
        //    {
        //        PurchaseStatus = "1";
        //        SubscribStatus = "1";
        //    }
        //}

        //objBook.BookID = Convert.ToInt32(BookID);
        objBook.LanguageID = Convert.ToInt32(l);
        objBook.EndIndex = -1;
        objBook.StartIndex = 1;

        DataTable dt = objBook.get_all_specialoffer_book();

        System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView();
            dv = dt.AsDataView();
            DateTime date = Convert.ToDateTime(dt.Rows[0]["CreatedOn"].ToString());
            string str = "<?xml version='1.0' encoding='UTF-8' ?>";
            str += "<BookApp>";

            str += "<Book>" +
          "<BookID>" + dt.Rows[0]["BookID"].ToString() + "</BookID>" +
             "<Title>" + dt.Rows[0]["Title1"].ToString().Replace("&", "And") + "</Title>" +
             "<Image>" + Config.WebSiteMain + "Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["ImagePath"].ToString() + "</Image>" +
               "<shareURL>" + ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book-Detail.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dt.Rows[i]["BookID"]))) + "&amp;title=" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[i]["Title1"])) + "</shareURL>" +

                 "<Date>" + date.ToString("MM/dd/yyyy") + "</Date>" +
                  "<Time>" + date.ToString("hh:mm tt") + "</Time>" +
                  "<PurchaseStatus>" + PurchaseStatus + "</PurchaseStatus>" +
                  "<SubscribStatus>" + SubscribStatus + "</SubscribStatus>" +
                 "<CategoryName>" + dt.Rows[0]["CategoryName"].ToString().Replace("&", "And") + "</CategoryName>" +
                     "<SubscriptionPrice>" + dt.Rows[0]["FinalPrice"].ToString() + "</SubscriptionPrice>" +
                     "<SubscriptionIssue>" + dt.Rows[0]["SubscriptionIssue"].ToString() + "</SubscriptionIssue>" +
                  "<Price>" + dt.Rows[0]["FinalPrice"].ToString() + "</Price>" +
                    "<Description>" + dt.Rows[0]["Description1"].ToString().Replace("&", "And") + "</Description>" +
                     "<Language>" + dt.Rows[0]["Language1"].ToString() + "</Language>" +
                        "</Book>";

            str += "</BookApp>";
            xdd.LoadXml(str);


        }
        else
        {

            //retJSON = "{\"code\":\"0\"}";
            string str = "<BookApp></BookApp>";
            xdd.LoadXml(str);
        }

        // Context.Response.Output.Write(retJSON);
        return xdd.DocumentElement;
    }

    //[WebMethod(Description = "This webservice for getting Book image by Book")]
    //public void BookImageByBook(int BookID)
    //{
    //    objBook = new BookBAL();
    //    objBook.BookID = BookID;
    //    DataTable dt = objBook.wsGetAllBookImageByBook();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        dt.TableName = "result";
    //        retJSON = JSON_DataTable(dt);
    //    }
    //    else
    //    {
    //        retJSON = Message("No category found.");
    //    }
    //    Context.Response.Output.Write(retJSON);
    //}

    //[WebMethod(Description = "This webservice for getting Book Top 5 Page Of PDf URL by Book")]
    //public void BookPdfTOP5ByBook(int BookID)
    //{
    //    objBook = new BookBAL();
    //    objBook.BookID = BookID;
    //    DataTable dt = objBook.wsGetAllBookPdfURLByBookID();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        dt.TableName = "result";
    //        retJSON = JSON_DataTable(dt);
    //    }
    //    else
    //    {
    //        retJSON = Message("No Book found.");
    //    }
    //    Context.Response.Output.Write(retJSON);
    //}



    //[WebMethod(Description = "This webservice for Notification Setting")]
    //public void NotificationSetting(string RegistrationID, string NewIssues, string Renewals, string AppUpdates)
    //{

    //    objUser.RegistrationID = Convert.ToInt64(RegistrationID);
    //    objUser.Renewals = Global.ConvertToBool(Renewals);
    //    objUser.NewIssues = Global.ConvertToBool(NewIssues);
    //    objUser.AppUpdates = Global.ConvertToBool(AppUpdates);
    //    i = objUser.UpdateNotification();
    //    if (i > 0)
    //    {
    //        retJSON = Success("Done successfully.");
    //    }
    //    else
    //    {
    //        retJSON = Message("Email address allready exists!");
    //    }
    //    Context.Response.Output.Write(retJSON);
    //}

    //[WebMethod(Description = "This webservice for Notification Setting")]
    //public void GetNotificationSetting(int RegistrationID)
    //{
    //    objUser.RegistrationID = RegistrationID;
    //    DataTable dt = objUser.GetNotificationSetting();

    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();
    //    if (dt != null && dt.Rows.Count > 0)
    //    {
    //        string aspJson = "";
    //        aspJson += "[{ \"BookApp\": {\"Notification\": [";

    //        aspJson += "{\"RegistrationID\": \"" + dt.Rows[i]["RegistrationID"].ToString() + "\"," +
    //                       "\"NewIssues\": \"" + dt.Rows[i]["NewIssues"].ToString() + "\"," +
    //                       "\"Renewals\": \"" + dt.Rows[i]["Renewals"].ToString() + "\"," +
    //                       "\"AppUpdates\": \"" + RemoveHTML(dt.Rows[i]["AppUpdates"].ToString()) + "\"}";

    //        aspJson += "]}}]";
    //        Context.Response.Output.Write(aspJson);
    //    }
    //    else
    //    {
    //        retJSON = Message("No record(s) found.");
    //    }

    //}

    //[WebMethod(Description = "This webservice for facebook login")]
    //public void FacebookLogin(string FacebookEmail)
    //{
    //    string aspJson = "";
    //    objUser.FacebookEmail = FacebookEmail;

    //    DataTable dt = objUser.FacebookLogin();
    //    if (dt.Rows.Count > 0)
    //    {
    //        if (!dt.Columns.Contains("Message"))
    //        {


    //            aspJson += "[{\"RegistrationID\": \"" + dt.Rows[i]["RegistrationID"].ToString() + "\"," +
    //                       "\"FirstName\": \"" + dt.Rows[i]["FirstName"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"LastName\": \"" + dt.Rows[i]["LastName"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"EmailAddress\": \"" + dt.Rows[i]["EmailAddress"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"FacebookEmail\": \"" + dt.Rows[i]["FacebookEmail"].ToString() + "\"," +
    //                        "\"Password\": \"" + dt.Rows[i]["Password"].ToString() + "\"," +
    //                        "\"CreatedDate\": \"" + dt.Rows[i]["CreatedDate"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"IsActive\": \"" + dt.Rows[i]["IsActive"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"IsNewsLetter\": \"" + dt.Rows[i]["IsNewsLetter"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"UserType\": \"" + dt.Rows[i]["UserType"].ToString() + "\"," +
    //                        "\"LoginDate\": \"" + dt.Rows[i]["LoginDate"].ToString() + "\"," +
    //                       "\"LastLoginDate\": \"" + dt.Rows[i]["LastLoginDate"].ToString() + "\"}";


    //            aspJson += "]";
    //            Success2("0", " Success Login.", aspJson);
    //            //dt.TableName = "result";
    //            //Context.Response.Output.Write(JSON_DataTable(dt));
    //        }
    //        else
    //        {
    //            // Context.Response.Output.Write(Message("Please sign up theMagz.net."));
    //            Fail("-1", "Please sign up LEA Books.");
    //        }
    //    }
    //    else
    //    {
    //        Context.Response.Output.Write(Message("Please sign up theMagz.net."));
    //    }

    //}

    //[WebMethod(Description = "This webservice for allEbooks")]
    //public void allEbooks(string Categoryid, string LanguageID, string PageNo)
    //{
    //    string aspJson = "";
    //    if (LanguageID == null || LanguageID == "")
    //    {
    //        objBook.LanguageID = 1;
    //    }
    //    else
    //    {
    //        objBook.LanguageID = Convert.ToInt32(LanguageID);
    //    }


    //    if (Categoryid != null && Categoryid != "")
    //    {
    //        objBook.CategoryID = Convert.ToInt32(Categoryid);
    //    }
    //    else
    //    {
    //        objBook.CategoryID = 0;
    //    }
    //    int cnt = 0;
    //    //int PageNo = 1; //Put PageNo as parameter for paging and uncomment SP
    //    if (PageNo == "" || PageNo == null)
    //    {
    //        PageNo = "1";
    //        cnt++;
    //    }

    //    int skip = (Convert.ToInt32(PageNo) - 1) * 10;
    //    DataSet dt = objBook.GetallEbooks(skip);

    //    try
    //    {
    //        if (dt != null && dt.Tables[0].Rows.Count > 0)
    //        {
    //            // dt.TableName = "result";
    //            // retJSON = JSON_DataTable(dt);
    //            if (PageNo != null && PageNo != "" && cnt == 0)
    //            {
    //                aspJson += "[";
    //                for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
    //                {
    //                    aspJson += "{";
    //                    aspJson += "\"totalNoOfRecords\": \"" + getUnicodeString(dt.Tables[1].Rows[0]["ROW_NUM"].ToString()) + "\"," +
    //                                "\"CategoryID\": \"" + getUnicodeString(dt.Tables[0].Rows[i]["CategoryID"].ToString()) + "\"," +
    //                                   "\"BookID\": \"" + getUnicodeString(dt.Tables[0].Rows[i]["BookID"].ToString()) + "\"," +
    //                                   "\"Bookname\": \"" + getUnicodeString(dt.Tables[0].Rows[i]["Title"].ToString()) + "\"," +
    //                                   "\"Autohername\": \"" + getUnicodeString(dt.Tables[0].Rows[i]["Autoher"].ToString()) + "\"," +
    //                                   "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[0].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[0].Rows[i]["ImagePath"].ToString()) + "\"," +
    //                                   "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[0].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[0].Rows[i]["ImagePath"].ToString()) + "\"";
    //                    aspJson += "},";



    //                }
    //            }
    //            else
    //            {
    //                aspJson += "[";
    //                for (int i = 0; i < dt.Tables[2].Rows.Count; i++)
    //                {
    //                    aspJson += "{";
    //                    aspJson +=
    //                                "\"CategoryID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "\"," +
    //                                   "\"BookID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["BookID"].ToString()) + "\"," +
    //                                   "\"Bookname\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Title"].ToString()) + "\"," +
    //                                   "\"Autohername\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Autoher"].ToString()) + "\"," +
    //                                   "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString()) + "\"," +
    //                                   "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString()) + "\"";
    //                    aspJson += "},";
    //                }
    //            }
    //            aspJson = aspJson.TrimEnd(',');
    //            aspJson += "]";
    //            Success2("0", "Get list", aspJson);
    //        }
    //        else
    //        {
    //            // retJSON = Message("No category found.");
    //            Fail("-1", "No data found");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Fail("-2", "Cannot connect to server.");
    //    }

    //}

    [WebMethod(Description = "This webservice for allEbooks")]
    public void allEbooks(string Categoryid, string LanguageID)
    {
        string aspJson = "";
        if (string.IsNullOrEmpty(LanguageID))
        {
            objBook.LanguageID = 1;
        }
        else
        {
            objBook.LanguageID = Convert.ToInt32(LanguageID);
        }


        if (!string.IsNullOrEmpty(Categoryid))
        {
            objBook.CategoryID = Convert.ToInt32(Categoryid);
        }
        else
        {
            objBook.CategoryID = 0;
        }
        int cnt = 0;
        int PageNo = 1; //Put PageNo as parameter for paging and uncomment SP


        int skip = (Convert.ToInt32(PageNo) - 1) * 10;
        DataSet dt = objBook.GetallEbooks(skip);

        try
        {
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                // dt.TableName = "result";
                // retJSON = JSON_DataTable(dt);

                aspJson += "[";
                for (int i = 0; i < dt.Tables[2].Rows.Count; i++)
                {
                    string type = "";
                    if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IsPaperBook"].ToString()) && Convert.ToBoolean(dt.Tables[2].Rows[i]["IseBook"].ToString()))
                    {
                        type = "0";
                    }
                    else if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IseBook"].ToString()))
                    {
                        type = "1";
                    }
                    else if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IsPaperBook"].ToString()))
                    {
                        type = "2";
                    }

                    var mPaperBookFinalPrice = dt.Tables[2].Rows[i]["PaperBookFinalPrice"];

                    aspJson += "{";
                    aspJson +=
                                "\"CategoryID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "\"," +
                                   "\"BookID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["BookID"].ToString()) + "\"," +
                                   "\"Bookname\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Title"].ToString()) + "\"," +
                                   "\"Autohername\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Autoher"].ToString()) + "\"," +
                                   "\"PaperBookPrice\": \"" + Convert.ToString(mPaperBookFinalPrice).Replace(",", ".") + "\"," +
                                    "\"Price\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["finalprice"].ToString()) + "\"," +
                                   "\"IsFree\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["IsFree"].ToString()) + "\"," +
                                   "\"BookType\": \"" + type + "\"," +
                                    "\"Pricecurrency\": \"$\" ," +
                                   "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"," +
                                   "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"";
                    aspJson += "},";
                }

                aspJson = aspJson.TrimEnd(',');
                aspJson += "]";
                Success2("0", "Get list", aspJson);
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    private string ShippingCharge(string customerID, string address)
    {
        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
        BookDeliveryAddressBAL objBookAddDetail = new BookDeliveryAddressBAL();
        ObjBookOrders.CustomerID = Convert.ToInt32(customerID);
        DataTable WishDT = ObjBookOrders.GetCartList();
        //int result = 0;
        int cnt = 1;

        var piceses = "";
        for (int i = 0; i < WishDT.Rows.Count; i++)
        {
            objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);

            if (Convert.ToBoolean(WishDT.Rows[i]["IsPaperBook"]))
            {
                var quantity = Convert.ToInt32(WishDT.Rows[i]["Qauntity"]);
                for (int j = 0; j < quantity; j++)
                {
                    try
                    {
                        piceses += "<Piece><PieceID>" + cnt + "</PieceID>"
                                + "<Height>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Height"].ToString()) ? WishDT.Rows[i]["Height"].ToString() : "1") + "</Height>"
                                + "<Depth>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Depth"].ToString()) ? WishDT.Rows[i]["Depth"].ToString() : "1") + "</Depth>"
                                + "<Width>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Width"].ToString()) ? WishDT.Rows[i]["Width"].ToString() : "1") + "</Width>"
                                + "<Weight>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Weight"].ToString()) ? WishDT.Rows[i]["Weight"].ToString() : "1") + "</Weight>"
                                + "</Piece>";
                    }
                    catch (Exception)
                    {
                        piceses += "<Piece><PieceID>" + cnt + "</PieceID>"
                                + "<Height>1</Height>"
                                + "<Depth>1</Depth>"
                                + "<Width>1</Width>"
                                + "<Weight>5.0</Weight></Piece>";
                    }
                    cnt++;
                }
            }
        }

        DataTable dt = new DataTable();
        objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(address);
        dt = objBookAddDetail.GetDataByPK();

        var ShippingCharge = "0";
        var TotalAmount = "0";
        try
        {
            GetQuote gq = new GetQuote();
            StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/GetQuote.xml"));
            string text = streamReader.ReadToEnd();

            string xmlRequest = gq.replaceXml(text, dt.Rows[0]["ISOCode"].ToString(), piceses, dt.Rows[0]["Pincode"].ToString(), dt.Rows[0]["City"].ToString());
            string response = gq.sendRequest(xmlRequest);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);
            try
            {
                var condition = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/Note/Condition/ConditionData").InnerText;
                return condition.Replace("origin postcode 10504 or", "");
            }
            catch (Exception)
            {

            }
            ShippingCharge = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/ShippingCharge").InnerText;
            TotalAmount = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/QtdSInAdCur/TotalAmount").InnerText;
            return "$ " + TotalAmount;
        }
        catch (Exception)
        {
        }
        return "$ " + "0";
    }

    private string NationalShippingCharge(string customerID, string address)
    {
        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
        BookDeliveryAddressBAL objBookAddDetail = new BookDeliveryAddressBAL();
        ObjBookOrders.CustomerID = Convert.ToInt32(customerID);
        DataTable WishDT = ObjBookOrders.GetCartList();

        decimal bweight = Convert.ToDecimal(0);
        for (int i = 0; i < WishDT.Rows.Count; i++)
        {
            if (WishDT.Rows[i]["IsPaperBook"].ToString() == "True")
            {
                bweight = bweight + Convert.ToDecimal(WishDT.Rows[i]["bweight"].ToString());
            }
        }

        DataTable dt = new DataTable();
        objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(address);
        dt = objBookAddDetail.GetDataByPK();

        decimal amt = Convert.ToDecimal(0);
        var NationalTotalAmount = "0";
        dbconnection d = new dbconnection();
        if (dt.Rows.Count > 0)
        {
            string country_c = dt.Rows[0]["Country"].ToString();
            DataTable dtrc = d.filltable("select rc.RId, r.* from[dbo].[Region_Country] rc inner join [dbo].[Region] r on r.Id = rc.RId where [Country_Id] = (select [CountryId] from [dbo].[tblcountry] where [countryname] = '" + country_c + "' and isactive = 1)");
            if (dtrc.Rows.Count > 0)
            {
                if (dtrc.Rows[0]["Region"].ToString().ToLower() == "central america")//region
                {
                    #region if region
                    if (country_c.ToLower() == "el salvador")//country
                    {
                        #region if country
                        if (Convert.ToDecimal(bweight) >= Convert.ToDecimal(2))
                        { }
                        else
                        {
                            DataTable dt_nsc = d.filltable("select * from  National_Shipping_Cost where isactive = 1");
                            if (dt_nsc.Rows.Count > 0)
                            {
                                if ((dt.Rows[0]["City"].ToString()).ToLower() == dt_nsc.Rows[0]["city"].ToString().ToLower())//city "san salvador"
                                {
                                    #region if city

                                    if (Convert.ToDecimal(bweight) == Convert.ToDecimal(0.5))
                                    {
                                        var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + val;//3.72
                                    }
                                    else if (Convert.ToDecimal(bweight) > Convert.ToDecimal(0.5))
                                    {
                                        var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + (val * (bweight / Convert.ToDecimal(0.5)));
                                    }

                                    #endregion
                                }
                                else
                                {
                                    #region no city
                                    if (Convert.ToDecimal(bweight) == Convert.ToDecimal(0.5))
                                    {
                                        var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + val;
                                        //amt = amt + Convert.ToDecimal(dt_nsc.Rows[1]["shipping_cost"].ToString().Replace(",", "."));//4.52
                                    }
                                    else if (Convert.ToDecimal(bweight) > Convert.ToDecimal(0.5))
                                    {
                                        var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + (val * (bweight / Convert.ToDecimal(0.5)));
                                    }
                                    #endregion
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                        if (dta.Rows.Count > 0)
                        {
                            var x = dta.Rows[0]["Price"].ToString();
                            var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                            amt = amt + val;
                            // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                        }
                    }
                    #endregion
                }
                else if (dtrc.Rows[0]["Region"].ToString().ToLower() == "united states & canada")//region
                {
                    #region if region
                    if (country_c.ToLower() == "florida")//country
                    {
                        if ((dt.Rows[0]["City"].ToString()).ToLower() == "miami")//city "miami"
                        {
                            #region if city
                            DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '0' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                            if (dta.Rows.Count > 0)
                            {
                                var x = dta.Rows[0]["Price"].ToString();
                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                amt = amt + val;
                                // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                            }
                            #endregion
                        }
                        else
                        {
                            #region no city
                            DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                            if (dta.Rows.Count > 0)
                            {
                                var x = dta.Rows[0]["Price"].ToString();
                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                amt = amt + val;
                                // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                            }
                            #endregion
                        }

                    }
                    else
                    {
                        DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                        if (dta.Rows.Count > 0)
                        {
                            var x = dta.Rows[0]["Price"].ToString();
                            var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                            amt = amt + val;
                            // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                        }
                    }
                    #endregion
                }
                else
                {
                    DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                    if (dta.Rows.Count > 0)
                    {
                        var x = dta.Rows[0]["Price"].ToString();
                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                        amt = amt + val;
                        //  amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                    }
                }
            }
        }
        NationalTotalAmount = Math.Round(amt, 2).ToString().Replace(",", ".");
        return NationalTotalAmount;
    }

    [WebMethod(Description = "This webservice for set default address")]
    public void setDefaultaddress(string customerID, string address)
    {
        BookDeliveryAddressBAL objBookAddress = new BookDeliveryAddressBAL();
        objBookAddress.SetDefaultAddress(customerID, address);
        objBookAddress.UserID = Convert.ToInt32(customerID);
        DataTable dt1 = objBookAddress.GetBookAddressByUser();

        if (address != "0")
        {
            string aspJson = "[{\"TotalAddressCount\": \"" + getUnicodeString(dt1.Rows.Count.ToString()) + "\"," +
                       "\"AddressID\": \"" + getUnicodeString(address) + "\"}";
            aspJson += "]";

            Success2("0", "Success", aspJson);
        }
        else
        {
            Fail("-1", "Address Can't Be Update");
        }
    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void Labels(string LanguageID)
    {
        if (LanguageID == "2")
        {

            // Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
        }
        else
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");


        string aspJson = "";
        objLanguage.ID = Convert.ToInt32(LanguageID);

        DataTable dt = objLanguage.GetallLabels();
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                // dt.TableName = "result";
                // retJSON = JSON_DataTable(dt);
                aspJson += "[";
                aspJson += "{";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    aspJson += "\"" + dt.Rows[i]["Name"].ToString().Trim().Replace(" ", "") + "\" : \"" + getUnicodeString(dt.Rows[i]["Value"].ToString()) + "\",";
                }
                aspJson = aspJson.TrimEnd(',');
                aspJson += "}";

                aspJson += "]";

                var baseurl = ConfigurationManager.AppSettings["BaseURL"].ToString();
                Success2("0", "Labels\", \"BaseURL\":\"" + baseurl + "", aspJson);
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void explore(string LanguageID)
    {
        string aspJson = "";
        string aspJson1 = "";
        string aspJson2 = "";
        string aspJson4 = "";
        // objBook.CategoryID = Convert.ToInt32(Categoryid);
        if (LanguageID == "2")
        {

            // Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
        }
        else
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");

        objBook.LangaugeID = Convert.ToInt32(LanguageID);
        objBook.LanguageID = Convert.ToInt32(LanguageID);
        //objBook.UserID = Convert.ToInt32(UserID);
        DataSet dt = objBook.GetTOP_Coverpages_sellers();
        string CartCount = "";
        if (dt != null)
        {
            if (dt.Tables[3].Rows.Count > 0)
            {
                CartCount = dt.Tables[3].Rows[i]["CartCount"].ToString();
            }
        }
        try
        {
            if (dt != null && dt.Tables.Count > 0)
            {
                if (dt != null)
                {
                    // dt.TableName = "result";
                    // retJSON = JSON_DataTable(dt);
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        aspJson += "[";
                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {
                            aspJson += "{";
                            aspJson += "\"IpadImage\": \"" + Config.WebSiteMain + "Banner/" + getUnicodeString(dt.Tables[0].Rows[i]["ImagePath"].ToString()) + "\"," +
                                       "\"iPhoneImages\": \"" + Config.WebSiteMain + "Banner/" + getUnicodeString(dt.Tables[0].Rows[i]["ImagePath"].ToString()) + "\"";
                            aspJson += "},";

                        }
                        aspJson = aspJson.TrimEnd(',');
                        aspJson += "]";
                    }
                    else
                    {
                        aspJson = "[]";
                    }
                    //Success2("0", "Get list", aspJson);
                }
                if (dt != null)
                {
                    if (dt.Tables[1].Rows.Count > 0)
                    {
                        aspJson1 += "[";
                        for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                        {
                            aspJson1 += "{";
                            aspJson1 += "\"CategoryID\": \"" + getUnicodeString(dt.Tables[1].Rows[i]["CategoryID"].ToString()) + "\"," +
                                           "\"CategoryName\": \"" + getUnicodeString(dt.Tables[1].Rows[i]["CategoryName"].ToString()) + "\"," +
                                           "\"CategoryiphoneImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString("new_" + dt.Tables[1].Rows[i]["CImagePath"].ToString().Replace("%20", " ")) + "\"," +
                                           "\"CategoryipadImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString("new_" + dt.Tables[1].Rows[i]["CImagePath"].ToString().Replace("%20", " ")) + "\"";
                            aspJson1 += "},";

                        }
                        aspJson1 = aspJson1.TrimEnd(',');
                        aspJson1 += "]";
                    }
                    else
                    {
                        aspJson1 = "[]";
                    }
                    //Success3(aspJson1);
                }
                if (dt != null)
                {
                    if (dt.Tables[2].Rows.Count > 0)
                    {
                        aspJson2 += "[";
                        for (int i = 0; i < dt.Tables[2].Rows.Count; i++)
                        {
                            string type = "";
                            if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IsPaperBook"].ToString()) && Convert.ToBoolean(dt.Tables[2].Rows[i]["IseBook"].ToString()))
                            {
                                type = "0";
                            }
                            else if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IseBook"].ToString()))
                            {
                                type = "1";
                            }
                            else if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IsPaperBook"].ToString()))
                            {
                                type = "2";
                            }
                            aspJson2 += "{";
                            aspJson2 += "\"CategoryID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "\"," +
                                           "\"BookID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["BookID"].ToString()) + "\"," +
                                           "\"BookName\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Title"].ToString()) + "\"," +
                                           "\"Autohername\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Autoher"].ToString()) + "\"," +
                                           "\"BookType\": \"" + type + "\"," +
                                           "\"Pricecurrency\": \"$\" ," +
                                           "\"BookPrice\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["FinalPrice"].ToString().Replace(",", ".")) + "\"," +
                                           "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"," +
                                           "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"";
                            aspJson2 += "},";

                        }
                        aspJson2 = aspJson2.TrimEnd(',');
                        aspJson2 += "]";
                    }
                    else
                    {
                        aspJson2 += "[]";
                    }
                    //Success4(aspJson2);
                }
                // for Cart Count added on 15th oct.2018
                //if (dt != null)
                //{
                //    if (dt.Tables[3].Rows.Count > 0)
                //    {
                //        aspJson4 += "[";
                //            aspJson4 += "{";
                //            aspJson4 += "\"CartCount\": \"" + getUnicodeString(dt.Tables[3].Rows[i]["CartCount"].ToString()) + "\"";
                //            //  "\"CartCount\": \"" + getUnicodeString(dt.Tables[3].Rows[i]["CartCount"].ToString()) + "\"";
                //            aspJson4 += "},";
                //        aspJson4 = aspJson4.TrimEnd(',');
                //        aspJson4 += "]";
                //    }
                //    else
                //    {
                //        aspJson4 = "[]";
                //    }
                //    //Success3(aspJson1);
                //}

                objBook.StartIndex = 1;
                objBook.EndIndex = 100;
                DataTable dtOffer = objBook.get_all_specialoffer_book();
                string aspJson3 = "";
                if (dtOffer != null && dtOffer.Rows.Count > 0)
                {
                    aspJson3 += "[";
                    for (int i = 0; i < dtOffer.Rows.Count; i++)
                    {
                        string type = "";
                        if (Convert.ToBoolean(dtOffer.Rows[i]["IsPaperBook"].ToString()) && Convert.ToBoolean(dtOffer.Rows[i]["IseBook"].ToString()))
                        {
                            type = "0";
                        }
                        else if (Convert.ToBoolean(dtOffer.Rows[i]["IseBook"].ToString()))
                        {
                            type = "1";
                        }
                        else if (Convert.ToBoolean(dtOffer.Rows[i]["IsPaperBook"].ToString()))
                        {
                            type = "2";
                        }
                        aspJson3 += "{";
                        aspJson3 += "\"CategoryID\": \"" + getUnicodeString(dtOffer.Rows[i]["CategoryID"].ToString()) + "\"," +
                                       "\"BookID\": \"" + getUnicodeString(dtOffer.Rows[i]["BookID"].ToString()) + "\"," +
                                       "\"BookName\": \"" + getUnicodeString(dtOffer.Rows[i]["Title"].ToString()) + "\"," +
                                       "\"Autohername\": \"" + getUnicodeString(dtOffer.Rows[i]["Autoher"].ToString()) + "\"," +
                                       "\"BookType\": \"" + type + "\"," +
                                       "\"Pricecurrency\": \"$\" ," +
                                       "\"BookPrice\": \"" + getUnicodeString(dtOffer.Rows[i]["FinalPrice"].ToString().Replace(",", ".")) + "\"," +
                                       "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dtOffer.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dtOffer.Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"," +
                                       "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dtOffer.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dtOffer.Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"";
                        aspJson3 += "},";
                    }
                    aspJson3 = aspJson3.TrimEnd(',');
                    aspJson3 += "]";
                }
                else
                {
                    aspJson3 += "[]";
                }

                Success3("0", "Get list ", aspJson, aspJson1, aspJson2, aspJson3);
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void explore_New(string LanguageID, string UserID)
    {
        string aspJson = "";
        string aspJson1 = "";
        string aspJson2 = "";
        string aspJson4 = "";
        // objBook.CategoryID = Convert.ToInt32(Categoryid);
        if (LanguageID == "2")
        {
            // Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
        }
        else
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");

        objBook.LangaugeID = Convert.ToInt32(LanguageID);
        objBook.LanguageID = Convert.ToInt32(LanguageID);
        if (!string.IsNullOrEmpty(UserID))
        {
            objBook.UserID = Convert.ToInt32(UserID);
        }

        DataSet dt = objBook.GetTOP_Coverpages_sellers();
        string CartCount = "";
        if (dt != null)
        {
            if (dt.Tables[3].Rows.Count > 0)
            {
                CartCount = dt.Tables[3].Rows[i]["CartCount"].ToString();
            }
        }
        try
        {
            if (dt != null && dt.Tables.Count > 0)
            {
                if (dt != null)
                {
                    // dt.TableName = "result";
                    // retJSON = JSON_DataTable(dt);
                    if (dt.Tables[0].Rows.Count > 0)
                    {
                        aspJson += "[";
                        for (int i = 0; i < dt.Tables[0].Rows.Count; i++)
                        {
                            aspJson += "{";
                            aspJson += "\"IpadImage\": \"" + Config.WebSiteMain + "Banner/" + getUnicodeString(dt.Tables[0].Rows[i]["ImagePath"].ToString()) + "\"," +
                                       "\"iPhoneImages\": \"" + Config.WebSiteMain + "Banner/" + getUnicodeString(dt.Tables[0].Rows[i]["ImagePath"].ToString()) + "\"";
                            aspJson += "},";

                        }
                        aspJson = aspJson.TrimEnd(',');
                        aspJson += "]";
                    }
                    else
                    {
                        aspJson = "[]";
                    }
                    //Success2("0", "Get list", aspJson);
                }
                if (dt != null)
                {
                    if (dt.Tables[1].Rows.Count > 0)
                    {
                        aspJson1 += "[";
                        for (int i = 0; i < dt.Tables[1].Rows.Count; i++)
                        {
                            aspJson1 += "{";
                            aspJson1 += "\"CategoryID\": \"" + getUnicodeString(dt.Tables[1].Rows[i]["CategoryID"].ToString()) + "\"," +
                                           "\"CategoryName\": \"" + getUnicodeString(dt.Tables[1].Rows[i]["CategoryName"].ToString()) + "\"," +
                                           "\"CategoryiphoneImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString("new_" + dt.Tables[1].Rows[i]["CImagePath"].ToString().Replace("%20", " ")) + "\"," +
                                           "\"CategoryipadImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString("new_" + dt.Tables[1].Rows[i]["CImagePath"].ToString().Replace("%20", " ")) + "\"";
                            aspJson1 += "},";

                        }
                        aspJson1 = aspJson1.TrimEnd(',');
                        aspJson1 += "]";
                    }
                    else
                    {
                        aspJson1 = "[]";
                    }
                    //Success3(aspJson1);
                }
                if (dt != null)
                {
                    if (dt.Tables[2].Rows.Count > 0)
                    {
                        aspJson2 += "[";
                        for (int i = 0; i < dt.Tables[2].Rows.Count; i++)
                        {
                            string type = "";
                            if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IsPaperBook"].ToString()) && Convert.ToBoolean(dt.Tables[2].Rows[i]["IseBook"].ToString()))
                            {
                                type = "0";
                            }
                            else if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IseBook"].ToString()))
                            {
                                type = "1";
                            }
                            else if (Convert.ToBoolean(dt.Tables[2].Rows[i]["IsPaperBook"].ToString()))
                            {
                                type = "2";
                            }
                            aspJson2 += "{";
                            aspJson2 += "\"CategoryID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "\"," +
                                           "\"BookID\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["BookID"].ToString()) + "\"," +
                                           "\"BookName\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Title"].ToString()) + "\"," +
                                           "\"Autohername\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["Autoher"].ToString()) + "\"," +
                                           "\"BookType\": \"" + type + "\"," +
                                           "\"Pricecurrency\": \"$\" ," +
                                           "\"BookPrice\": \"" + getUnicodeString(dt.Tables[2].Rows[i]["FinalPrice"].ToString().Replace(",", ".")) + "\"," +
                                           "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"," +
                                           "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Tables[2].Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Tables[2].Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"";
                            aspJson2 += "},";

                        }
                        aspJson2 = aspJson2.TrimEnd(',');
                        aspJson2 += "]";
                    }
                    else
                    {
                        aspJson2 += "[]";
                    }
                    //Success4(aspJson2);
                }
                // for Cart Count added on 15th oct.2018
                //if (dt != null)
                //{
                //    if (dt.Tables[3].Rows.Count > 0)
                //    {
                //        aspJson4 += "[";
                //            aspJson4 += "{";
                //            aspJson4 += "\"CartCount\": \"" + getUnicodeString(dt.Tables[3].Rows[i]["CartCount"].ToString()) + "\"";
                //            //  "\"CartCount\": \"" + getUnicodeString(dt.Tables[3].Rows[i]["CartCount"].ToString()) + "\"";
                //            aspJson4 += "},";
                //        aspJson4 = aspJson4.TrimEnd(',');
                //        aspJson4 += "]";
                //    }
                //    else
                //    {
                //        aspJson4 = "[]";
                //    }
                //    //Success3(aspJson1);
                //}

                objBook.StartIndex = 1;
                objBook.EndIndex = 100;
                DataTable dtOffer = objBook.get_all_specialoffer_book();
                string aspJson3 = "";
                if (dtOffer != null && dtOffer.Rows.Count > 0)
                {
                    aspJson3 += "[";
                    for (int i = 0; i < dtOffer.Rows.Count; i++)
                    {
                        string type = "";
                        if (Convert.ToBoolean(dtOffer.Rows[i]["IsPaperBook"].ToString()) && Convert.ToBoolean(dtOffer.Rows[i]["IseBook"].ToString()))
                        {
                            type = "0";
                        }
                        else if (Convert.ToBoolean(dtOffer.Rows[i]["IseBook"].ToString()))
                        {
                            type = "1";
                        }
                        else if (Convert.ToBoolean(dtOffer.Rows[i]["IsPaperBook"].ToString()))
                        {
                            type = "2";
                        }
                        aspJson3 += "{";
                        aspJson3 += "\"CategoryID\": \"" + getUnicodeString(dtOffer.Rows[i]["CategoryID"].ToString()) + "\"," +
                                       "\"BookID\": \"" + getUnicodeString(dtOffer.Rows[i]["BookID"].ToString()) + "\"," +
                                       "\"BookName\": \"" + getUnicodeString(dtOffer.Rows[i]["Title"].ToString()) + "\"," +
                                       "\"Autohername\": \"" + getUnicodeString(dtOffer.Rows[i]["Autoher"].ToString()) + "\"," +
                                       "\"BookType\": \"" + type + "\"," +
                                       "\"Pricecurrency\": \"$\" ," +
                                       "\"BookPrice\": \"" + getUnicodeString(dtOffer.Rows[i]["FinalPrice"].ToString().Replace(",", ".")) + "\"," +
                                       "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dtOffer.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dtOffer.Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"," +
                                       "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dtOffer.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dtOffer.Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"";
                        aspJson3 += "},";
                    }
                    aspJson3 = aspJson3.TrimEnd(',');
                    aspJson3 += "]";
                }
                else
                {
                    aspJson3 += "[]";
                }

                Success22("0", "Get list ", CartCount, aspJson, aspJson1, aspJson2, aspJson3);
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void CategoryList(string LanguageID)
    {
        string aspJson = "";

        objCategory.LanguageID = Convert.ToInt32(LanguageID);

        DataTable dt = objCategory.SelectAllCartegory();
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                // dt.TableName = "result";
                // retJSON = JSON_DataTable(dt);
                aspJson += "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    aspJson += "{";

                    aspJson += "\"CategoryID\": \"" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "\"," +
                                   "\"CategoryName\": \"" + getUnicodeString(dt.Rows[i]["CategoryName"].ToString()) + "\"," +
                                   //"\"CategoryiphoneImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString(dt.Rows[i]["CImagePath"].ToString()) + "\"," +
                                   //    "\"CategoryipadImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString(dt.Rows[i]["CImagePath"].ToString()) + "\"";
                                   //New Code Nirav
                                   "\"CategoryiphoneImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString("new_" + dt.Rows[i]["CImagePath"].ToString().Replace("%20", " ")) + "\"," +
                                   "\"CategoryipadImage\": \"" + Config.WebSiteMain + "Category/" + getUnicodeString("new_" + dt.Rows[i]["CImagePath"].ToString().Replace("%20", " ")) + "\"";
                    //End New Code
                    aspJson += "},";

                }
                aspJson = aspJson.TrimEnd(',');
                aspJson += "]";
                Success4("0", "Get list", aspJson);
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void SearchBook(string SearchBook)
    {
        string aspJson = "";
        objBook.Title = SearchBook.ToString();

        DataTable dt = objBook.SearchBook();
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                // dt.TableName = "result";
                // retJSON = JSON_DataTable(dt);
                aspJson += "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    aspJson += "{";
                    aspJson += "\"CategoryID\": \"" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "\"," +
                                   "\"BookID\": \"" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + "\"," +
                                   "\"Bookname\": \"" + getUnicodeString(dt.Rows[i]["Title"].ToString()) + "\"," +
                                   "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString()) + "\"," +
                                       "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString()) + "\"";
                    aspJson += "},";

                }
                aspJson = aspJson.TrimEnd(',');
                aspJson += "]";
                Success2("0", "Get search list", aspJson);
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void getBookDetails(string BookID, string LanguageID)
    {
        string aspJson = "";
        objBook.BookID = Convert.ToInt32(BookID);
        objBook.LanguageID = Convert.ToInt32(LanguageID);

        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        //DataSet ds = objBook.getBookDetails();
        //DataTable dt = ds.Tables[0];
        //DataTable dt1 = ds.Tables[1];
        DataTable dt = objBook.getBookDetails();
        DataTable dtid = objUser.Login();
        // objBookDelivery.UserID = Convert.ToInt32(dt.Rows[0]["RegistrationID"].ToString());
        DataTable dtAdd = objBookDelivery.GetBookAddressByUser();
        DataTable dtCart = ObjBookOrders.GetCartList();

        try
        {

            if (dt != null && dt.Rows.Count > 0)
            {
                // dt.TableName = "result";
                // retJSON = JSON_DataTable(dt);
                aspJson += "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // put Price = Price 
                    price = dt.Rows[i]["FinalPrice"].ToString();
                    if (dt.Rows[i]["IsFree"].ToString() == "False")
                    {
                        price = "";
                    }

                    var bookFilepdf = Server.MapPath("/Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["BookID"].ToString() + ".pdf");
                    var bookFileepub = Server.MapPath("/Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["BookID"].ToString() + ".epub");
                    var bookFile = "";
                    var bookFileType = "1";
                    if (File.Exists(bookFilepdf))
                    {
                        bookFile = ConfigurationManager.AppSettings["SiteUrl"] + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".pdf";
                        bookFileType = "1";
                    }
                    else if (File.Exists(bookFileepub))
                    {
                        bookFile = ConfigurationManager.AppSettings["SiteUrl"] + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".epub";
                        bookFileType = "2";
                    }
                    else
                    {
                        bookFile = ConfigurationManager.AppSettings["SiteUrl"] + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".pdf";
                        bookFileType = "1";
                    }

                    aspJson += "{";
                    aspJson += "\"CategoryID\": \"" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "\"," +
                               "\"BookID\": \"" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + "\"," +
                               "\"Bookname\": \"" + getUnicodeString(dt.Rows[i]["Title1"].ToString()) + "\"," +
                               "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                               "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                               "\"Autohername\": \"" + getUnicodeString(dt.Rows[i]["Autoher"].ToString()) + "\"," +
                               "\"Pusblisherdate\": \"" + getUnicodeString(dt.Rows[i]["PublishDate1"].ToString()) + "\"," +
                               "\"Language\": \"" + getUnicodeString(dt.Rows[i]["Language1"].ToString()) + "\"," +
                               "\"IsFree\": \"" + getUnicodeString(dt.Rows[i]["IsFree"].ToString()) + "\"," +
                               "\"Price\": \"" + getUnicodeString(dt.Rows[i]["FinalPrice"].ToString()) + "\"," +
                               "\"Pricecurrency\": \"$\" ," +
                               //"\"bookPDFURL\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".pdf" + "\"," +
                               "\"bookFileType\": \"" + getUnicodeString(bookFileType) + "\"," +
                               "\"bookPDFURL\": \"" + bookFile + "\"," +
                               "\"BookDescription\": \"" + getUnicodeString(dt.Rows[i]["Description1"].ToString()) + "\",";

                    aspJson += " \"BookTypeList\":[{";
                    string eBook = "";
                    string paperBook = "";
                    if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                    {
                        eBook += "\"BookTypeID\":\"1\",\"BookType\":\"EBook\",\"BookPrice\":\"" + getUnicodeString(dt.Rows[i]["FinalPrice"].ToString()) + "\"";
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                    {
                        paperBook += "\"BookTypeID\":\"2\",\"BookType\":\"PaperBook\",\"BookPrice\":\"" + getUnicodeString(dt.Rows[i]["PaperBookFinalPrice"].ToString()) + "\"";
                    }
                    if (!string.IsNullOrEmpty(eBook) && !string.IsNullOrEmpty(paperBook))
                    {
                        aspJson += eBook + "},{" + paperBook + "}]";
                    }
                    else if (!string.IsNullOrEmpty(eBook))
                    {
                        aspJson += eBook + "}]";
                    }
                    else if (!string.IsNullOrEmpty(paperBook))
                    {
                        aspJson += paperBook + "}]";
                    }
                    else
                    {
                        aspJson += "}]";
                    }
                    aspJson += "},";

                }
                aspJson = aspJson.TrimEnd(',');
                aspJson += "]";
                //ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                //ObjBookOrders.LanguageID = 1;
                //DataTable dtCart = ObjBookOrders.GetCartList();
                Success2("0", "Get search list", aspJson);
                //Success10("0", "Get search list", dtCart.Rows.Count, aspJson);
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void getUserLibrary(string UserID, string LanguageID)
    {
        string aspJson = "";
        objPurchase.UserID = Convert.ToInt32(UserID);
        objPurchase.LanguageID = Convert.ToInt32(LanguageID);

        DataTable dt = objPurchase.getUserLibrary();
        string[] Books = new string[dt.Rows.Count];
        bool Found = false;

        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(UserID) + "'");
        if (DtEXist.Rows.Count > 0)
        {
            if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
            {
                Fail("-1", "This User is Deleted....");
            }
            else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
            {
                Fail("-5", "This User is In-Active....");
            }
            else
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    Hashtable hashtable = new Hashtable();
                    aspJson += "[";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //foreach (string bookid in Books)
                        //{
                        //    if (bookid != null && bookid != "")
                        //    {
                        //        if (bookid == dt.Rows[i]["BookID"].ToString())
                        //        {
                        //            Found = true;
                        //        }
                        //    }
                        //}
                        //if (!Found)
                        //{
                        if (!hashtable.ContainsKey(dt.Rows[i]["BookID"] + "" + dt.Rows[i]["IseBook"] + "" + dt.Rows[i]["IsPaperBook"]))
                        {
                            hashtable.Add(dt.Rows[i]["BookID"] + "" + dt.Rows[i]["IseBook"] + "" + dt.Rows[i]["IsPaperBook"], "Book");
                            Books[i] = dt.Rows[i]["BookID"].ToString();

                            var bookFilepdf = Server.MapPath("/Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["BookID"].ToString() + ".pdf");
                            var bookFileepub = Server.MapPath("/Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["BookID"].ToString() + ".epub");
                            var bookFile = "";
                            var bookFileType = "1";
                            if (File.Exists(bookFilepdf))
                            {
                                bookFile = ConfigurationManager.AppSettings["SiteUrl"] + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".pdf";
                                bookFileType = "1";
                            }
                            else if (File.Exists(bookFileepub))
                            {
                                bookFile = ConfigurationManager.AppSettings["SiteUrl"] + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".epub";
                                bookFileType = "2";
                            }
                            else
                            {
                                bookFile = ConfigurationManager.AppSettings["SiteUrl"] + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".pdf";
                                bookFileType = "1";
                            }
                            string type = "";
                            if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"].ToString()) && Convert.ToBoolean(dt.Rows[i]["IseBook"].ToString()))
                            {
                                type = "0";
                            }
                            else if (Convert.ToBoolean(dt.Rows[i]["IseBook"].ToString()))
                            {
                                type = "1";
                            }
                            else if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"].ToString()))
                            {
                                type = "2";
                                continue;
                            }

                            aspJson += "{";
                            aspJson += "\"CategoryID\": \"" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "\"," +
                                           "\"BookID\": \"" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + "\"," +
                                           "\"Bookname\": \"" + getUnicodeString(dt.Rows[i]["BookTitle1"].ToString()) + "\"," +
                                           //"\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString()) + "\"," +
                                           //"\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString()) + "\"," +
                                           //New Code Nirav
                                           "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"," +
                                           "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace(".jpg", "_1.jpg")) + "\"," +
                                           //End New Code
                                           "\"Autohername\": \"" + getUnicodeString(dt.Rows[i]["Autoher"].ToString()) + "\"," +
                                           "\"Pusblisherdate\": \"" + getUnicodeString(dt.Rows[i]["PublishDate1"].ToString()) + "\"," +
                                           "\"Language\": \"" + getUnicodeString(dt.Rows[i]["Language1"].ToString()) + "\"," +
                                           "\"Price\": \"" + getUnicodeString(dt.Rows[i]["FinalPrice"].ToString()) + "\"," +
                                           "\"Pricecurrency\": \"$\" ," +
                                           "\"BookType\": \"" + type + "\"," +
                                           //"\"bookPDFURL\": \"" + ConfigurationManager.AppSettings["SiteUrl"] + "/Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + ".pdf" + "\"," +
                                           "\"bookPDFURL\": \"" + bookFile + "\"," +
                                           "\"bookFileType\": \"" + getUnicodeString(bookFileType) + "\"," +
                                           "\"OrederID\": \"" + getUnicodeString(dt.Rows[i]["OrderID"].ToString()) + "\"," +
                                           "\"BookDescription\": \"" + getUnicodeString(dt.Rows[i]["Description"].ToString()) + "\"";
                            aspJson += "},";
                        }
                        //}
                    }
                    aspJson = aspJson.TrimEnd(',');
                    aspJson += "]";
                    Success2("0", "Get search list", aspJson);
                }
                else
                {
                    Fail("-1", "No data found");
                }
            }
        }
        else
        {
            Fail("-4", "This User is Does not Exist....");
        }

    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void cartlist(string CustomerID, string LanguageID)
    {
        string aspJson = "";
        string aspJson1 = "";
        string aspJson2 = "";
        ObjBookOrders.CustomerID = Convert.ToInt32(CustomerID);
        ObjBookOrders.LanguageID = Convert.ToInt32(LanguageID);

        dbconnection db = new dbconnection();
        DataTable dtAdd = new DataTable();
        var objBookAddDetail = new BookDeliveryAddressBAL();
        objBookAddDetail.UserID = Convert.ToInt32(CustomerID);
        //dtAdd = objBookAddDetail.GetBookAddressByUser();
        dtAdd = db.filltable("Select * from BookDeliveryAddressDetail where IsDefault=1 and UserID='" + Convert.ToInt32(CustomerID) + "' ");
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");


        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(CustomerID) + "'");


        DataTable dt = ObjBookOrders.GetCartList();
        try
        {
            if (DtEXist.Rows.Count > 0)
            {
                if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
                {
                    Fail("-1", "This User is Deleted....");
                }
                else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
                {
                    Fail("-5", "This User is In-Active....");
                }
                else
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        aspJson1 += "[";
                        int paperbook = 0;
                        Double Amount = 0;
                        aspJson += "[";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var quantity = 0;
                            var bookTypeID = 0;
                            var bookPrice = "0";
                            var bookType = "";
                            //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"].ToString());
                            aspJson += "{";
                            aspJson += "\"CartID\": \"" + getUnicodeString(dt.Rows[i]["OrderID"].ToString()) + "\"," +
                                        "\"OrderNo\": \"" + getUnicodeString(dt.Rows[i]["OrderNo"].ToString()) + "\"," +
                                        "\"CategoryID\": \"" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "\"," +
                                        "\"Category\": \"" + getUnicodeString(dt.Rows[i]["CategoryName"].ToString()) + "\"," +
                                        "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                                        "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                                        "\"BookID\": \"" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + "\"," +
                                        "\"Bookname\": \"" + getUnicodeString(dt.Rows[i]["Title"].ToString()) + "\"," +
                                        "\"Autohername\": \"" + getUnicodeString(dt.Rows[i]["Autoher"].ToString()) + "\"," +
                                        "\"Pusblisherdate\": \"" + getUnicodeString(dt.Rows[i]["PublishDate1"].ToString()) + "\"," +
                                        "\"Language\": \"" + getUnicodeString(dt.Rows[i]["Language1"].ToString()) + "\"," +
                                        "\"Pricecurrency\": \"$\" ," +
                                        "\"BookDescription\": \"" + getUnicodeString(dt.Rows[i]["Description"].ToString()) + "\",";
                            if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                            {
                                quantity++;
                                bookTypeID = 1;
                                bookType = "EBook";
                                bookPrice = dt.Rows[i]["FinalPrice"].ToString();
                            }
                            if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                            {
                                quantity += Convert.ToInt32(dt.Rows[i]["Qauntity"]);
                                bookTypeID = 2;
                                bookType = "PaperBook";
                                bookPrice = (dt.Rows[i]["PaperBookFinalPrice"].ToString() == "" ? "0" : dt.Rows[i]["PaperBookFinalPrice"].ToString());//updated by : 07-09-2018
                                paperbook = 1;
                            }
                            aspJson += "\"Quantity\": \"" + getUnicodeString(quantity.ToString()) + "\"," +
                                       "\"BookTypeID\": \"" + getUnicodeString(bookTypeID.ToString()) + "\"," +
                                       "\"BookType\": \"" + getUnicodeString(bookType) + "\"," +
                                       "\"Price\": \"" + getUnicodeString(bookPrice) + "\"," +
                                       "\"TotalPrice\": \"" + getUnicodeString((quantity * Convert.ToDecimal(bookPrice)).ToString()) + "\"" + "";
                            aspJson += "},";
                            Amount += quantity * Convert.ToDouble(bookPrice);
                        }

                        aspJson = aspJson.TrimEnd(',');
                        aspJson += "]";

                        // Address Details 
                        DataTable DtAddress = new DataTable();
                        var objBookAddDetails = new BookDeliveryAddressBAL();
                        objBookAddDetails.UserID = Convert.ToInt32(CustomerID);
                        DtAddress = objBookAddDetails.GetBookAddressByUser_Default();

                        if (DtAddress != null)
                        {
                            if (DtAddress.Rows.Count > 0)
                            {
                                aspJson2 += "[";
                                for (int i = 0; i < DtAddress.Rows.Count; i++)
                                {
                                    aspJson2 += "{";
                                    aspJson2 += "\"BookDeliveryAddID\": \"" + getUnicodeString(DtAddress.Rows[0]["BookDeliveryAddID"].ToString()) + "\"," +
                                                   "\"UserID\": \"" + getUnicodeString(DtAddress.Rows[0]["UserID"].ToString()) + "\"," +
                                                   "\"IsDefault\": \"" + getUnicodeString(DtAddress.Rows[0]["IsDefault"].ToString()) + "\"," +
                                                   "\"Name\": \"" + getUnicodeString(DtAddress.Rows[0]["Name"].ToString()) + "\"," +
                                                   "\"StreetAddress\": \"" + getUnicodeString(DtAddress.Rows[0]["StreetAddress"].ToString()) + "\"," +
                                                   "\"LandMark\": \"" + getUnicodeString(DtAddress.Rows[0]["LandMark"].ToString()) + "\"," +
                                                   "\"City\": \"" + getUnicodeString(DtAddress.Rows[0]["City"].ToString()) + "\"," +
                                                   "\"State\": \"" + getUnicodeString(DtAddress.Rows[0]["State"].ToString()) + "\"," +
                                                   "\"Country\": \"" + getUnicodeString(DtAddress.Rows[0]["Country"].ToString()) + "\"," +
                                                   "\"Pincode\": \"" + getUnicodeString(DtAddress.Rows[0]["Pincode"].ToString()) + "\"," +
                                                   "\"PhoneNumber\": \"" + getUnicodeString(DtAddress.Rows[0]["PhoneNumber"].ToString()) + "\"";
                                    aspJson2 += "},";
                                }
                                aspJson2 = aspJson2.TrimEnd(',');
                                aspJson2 += "]";
                            }
                            else
                            {
                                aspJson2 = "[]";
                            }
                            //Success3(aspJson1);
                        }

                        var ShippingDetail = "No data found";
                        var NationalShippingDetails = "No data found";
                        var defaultAddressId = "0";
                        if (dtAdd.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtAdd.Rows.Count; i++)
                            {
                                if (Convert.ToBoolean(dtAdd.Rows[i]["IsDefault"].ToString()))
                                {
                                    ShippingDetail = ShippingCharge(CustomerID, dtAdd.Rows[i]["BookDeliveryAddID"].ToString());
                                    NationalShippingDetails = NationalShippingCharge(CustomerID, dtAdd.Rows[i]["BookDeliveryAddID"].ToString());
                                    defaultAddressId = dtAdd.Rows[0]["BookDeliveryAddID"].ToString();
                                    break;
                                }
                            }
                        }

                        SuccessCart("0", "List of product", Amount.ToString() + "\",\"isPaperBookAvailable\":\"" + paperbook, dtAdd.Rows.Count.ToString(), ShippingDetail, NationalShippingDetails, defaultAddressId, aspJson, dt.Rows.Count.ToString(), aspJson2);
                    }
                    else
                    {
                        Fail("-1", "No data found");
                    }
                }
            }
            else
            {
                Fail("-4", "This User is Does not Exist....");
            }

        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    //https ://www.leaebook.com/WebService.asmx/PaymentSuccess?OrderNo=22114452&address=225&TransactionId=1245782587412&ShippingCharge=$%20123
    [WebMethod]
    public void PaymentSuccess(string OrderNo, string address, string TransactionId, string ShippingCharge, string email, string ShippingType, string PaymentFrom)
    {
        try
        {
            if (ShippingType == "1") // For DHL shipping
            {
                ShippingType = "DHL";
            }
            else if (ShippingType == "2") // For National
            {
                ShippingType = "National";
            }
            else
            {
                ShippingType = "";
            }
            ObjBookOrders = new BookOrderBAL();
            ObjBookOrders.OrderNo = OrderNo;
            ObjBookOrders.LanguageID = 1;
            DataTable WishDT = ObjBookOrders.GetCartListByOrderNo();
            Shipment s = new Shipment();

            int result = 0;
            if (WishDT != null && WishDT.Rows.Count > 0)
            {
                StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/Shipment1.xml"));
                string text = streamReader.ReadToEnd();
                var piceses = "";
                decimal weight = 0;
                var cnt = 0;
                for (int i = 0; i < WishDT.Rows.Count; i++)
                {
                    objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);
                    objPurchase.PurchaseDate = Convert.ToDateTime(System.DateTime.Now);
                    objPurchase.TransID = TransactionId;
                    if (PaymentFrom == "1")
                    {
                        PaymentFrom = "App";
                    }
                    else
                    {
                        PaymentFrom = "";
                    }
                    result = objPurchase.MoveToBookPurchase(address, ShippingCharge, ShippingType, PaymentFrom);
                    if (Convert.ToBoolean(WishDT.Rows[i]["IsPaperBook"]))
                    {
                        var quantity = Convert.ToInt32(WishDT.Rows[i]["Qauntity"]);
                        for (int j = 0; j < quantity; j++)
                        {
                            cnt++;
                            try
                            {
                                weight += Convert.ToDecimal(WishDT.Rows[i]["Weight"]);
                                piceses += "<Piece><PieceID>" + (i + 1) + "</PieceID><PackageType>EE</PackageType><Weight>"
                                           + WishDT.Rows[i]["Weight"] + "</Weight><DimWeight>" + WishDT.Rows[i]["DimWeight"]
                                           + "</DimWeight><Width>" + WishDT.Rows[i]["Width"] + "</Width><Height>"
                                           + WishDT.Rows[i]["Height"] + "</Height><Depth>"
                                           + WishDT.Rows[i]["Depth"] + "</Depth></Piece>";
                            }
                            catch (Exception)
                            {
                                weight += 1;
                                piceses += "<Piece><PieceID>" + (i + 1) + "</PieceID><PackageType>EE</PackageType><Weight>"
                                           + "1.0" + "</Weight><DimWeight>" + "1200.0"
                                           + "</DimWeight><Width>" + "100" + "</Width><Height>"
                                           + "200" + "</Height><Depth>"
                                           + "300" + "</Depth></Piece>";
                            }
                        }
                    }
                }
                if (cnt > 0)
                {
                    string xmlRequest = s.replaceXml(text, cnt.ToString(), piceses, weight.ToString(), ShippingCharge);
                    string response = s.sendRequest(xmlRequest);

                    try
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(response);

                        var ProfilePicture = xmlDoc.DocumentElement.SelectSingleNode("LabelImage/OutputImage").InnerText;
                        var billnumber = xmlDoc.DocumentElement.SelectSingleNode("AirwayBillNumber").InnerText;
                        string name = billnumber;
                        if (!string.IsNullOrEmpty(ProfilePicture))
                        {
                            Base64ToImage(ProfilePicture, Server.MapPath("~/ShippingFiles/") + name + ".pdf");
                        }
                        objPurchase.LanguageID = ObjBookOrders.LanguageID;
                        objPurchase.OrderID = Convert.ToInt64(OrderNo);
                        WishDT = objPurchase.GetCartListByOrderID();

                        SendCustomerOrderEmail(email, WishDT, TransactionId, objPurchase.OrderID.ToString(), Server.MapPath("~/ShippingFiles/") + name + ".pdf", billnumber);
                        Success6("0", "Order details send to your mail id successfully. " + TransactionId);
                        return;
                    }
                    catch (Exception)
                    {
                        objPurchase.LanguageID = ObjBookOrders.LanguageID;
                        objPurchase.OrderID = Convert.ToInt64(OrderNo);
                        WishDT = objPurchase.GetCartListByOrderID();

                        SendCustomerOrderEmail(email, WishDT, TransactionId, objPurchase.OrderID.ToString());
                        Success6("0", "Order details send to your mail id successfully. " + TransactionId);
                        return;
                    }
                }
                else
                {
                    objPurchase.LanguageID = ObjBookOrders.LanguageID;
                    objPurchase.OrderID = Convert.ToInt64(OrderNo);
                    WishDT = objPurchase.GetCartListByOrderID();

                    SendCustomerOrderEmail(email, WishDT, TransactionId, objPurchase.OrderID.ToString());
                    Success6("0", "Order details send to your mail id successfully. " + TransactionId);
                    return;
                }
            }
            Success6("-1", "No books found for this order.");

        }
        catch (Exception ex)
        {
            Success6("-2", ex.Message);
        }
    }

    public void Base64ToImage(string base64String, string filename)
    {
        // Convert Base64 String to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);
        byte[] bytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        // Convert byte[] to PDF
        System.IO.FileStream stream = new FileStream(filename, FileMode.CreateNew);
        System.IO.BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(bytes, 0, bytes.Length);
        writer.Close();
        // Convert byte[] to Image
    }

    DataTable dt = new DataTable();
    public void SendCustomerOrderEmail(string email, DataTable ebooksDT, string transactionID, string OrderID, string path, string billNumber)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();
            }
        }
        catch { }

        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b></h3><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No: </b> " + OrderID + "</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Tracking Number: </b> " + billNumber + "</span><br>");
        sb.Append("<table width='550' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        int qty = 0;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            qty += Convert.ToInt32(dr["Qauntity"].ToString());
            price += Convert.ToDouble(dr["Amount"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='57' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quantity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + (ebooksDT.Rows[i]["Qauntity"].ToString() == "0" ? "-" : ebooksDT.Rows[i]["Qauntity"].ToString()) + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["Amount"].ToString() + "</td>");
            sb.Append("</tr>");
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE tbl_BookPurchase set AppCode = " + billNumber + " where PurchaseID = " + ebooksDT.Rows[i]["PurchaseID"] + "");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Book(s):</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>" + qty + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (contactmail != null)
        {
            try
            {
                Global.SendEmail(email, "LEA eBooks | Your Order Confirmation", sb.ToString(), path);
            }
            catch (Exception)
            {

            }
        }
        else
        {
            try
            {
                Global.SendEmail(email, "LEA eBooks | Your Order information", sb.ToString(), path);
            }
            catch (Exception)
            {

            }
        }
        try
        {
            DataTable dtLIst = DAL.SqlHelper.ExecuteDataTable(CommandType.Text, "SELECT * FROM Registration WHERE IsActive = 1 AND UserType = 1 AND IsDeleted = 0");
            if (dtLIst.Rows.Count > 0)
            {
                if (dtLIst.Rows[0]["AlternetEmailAddress"].ToString() != "")
                {
                    Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString() + ", " + dtLIst.Rows[0]["AlternetEmailAddress"].ToString(), "LEA eBooks | User Order information", sb.ToString(), path);
                }
                else
                {
                    Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString(), "LEA eBooks | User Order information", sb.ToString(), path);
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public void SendCustomerOrderEmail(string email, DataTable ebooksDT, string transactionID, string OrderID)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();
            }
        }
        catch { }

        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#f9f9f9\" > "
         + "     <tr> "
         + "         <td  style=\";Font-size:20px;\"> "
         + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px 15px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"width: 70px;\" /></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-size: 18px; font-weight: normal;padding: 20px 0 0 20px;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b></h3><br>");
        sb.Append("<span style='font-size: 15px; font-weight: bold;color: rgb(51, 51, 51);display: block; padding: 0 25px 10px 25px;'>");
        sb.Append("<span style=''>Hello </span>" + UserName + ",</span>");
        sb.Append("<span style='font-size: 15px; font-weight: normal;color: rgb(51, 51, 51);display: block; padding: 0 25px 5px 25px;'>");
        sb.Append("Thank you for your order !</span>");
        sb.Append("<span style='font-size: 15px; font-weight: normal;color: rgb(51, 51, 51);display: block; padding: 0 25px 5px 25px;'>");
        sb.Append("Please find below, the summary of your order:</span>");
        sb.Append("<span style='font-size: 15px; font-weight: normal;color: rgb(51, 51, 51); display: block; padding: 23px 0 0 24px;'>");
        sb.Append("<b>Order No: </b> " + OrderID + "</span><br>");
        sb.Append("<table width='100%' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        int qty = 0;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            qty += Convert.ToInt32(dr["Qauntity"].ToString());
            price += Convert.ToDouble(dr["Amount"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left' style='font-size: 15px; font-weight: bold; color: rgb(62, 62, 62); background: #ebeaea; padding: 8px 12px;'>Image</td>");
        sb.Append("<td width='283' valign='middle' style='font-size: 15px; font-weight: bold; color: rgb(62, 62, 62); background: #ebeaea; padding: 8px 12px;'>eBookName</td>");
        sb.Append("<td width='283' valign='middle' style='font-size: 15px; font-weight: bold; color: rgb(62, 62, 62); background: #ebeaea; padding: 8px 12px;'>Author</td>");
        sb.Append("<td width='57' valign='middle' style='font-size: 15px; font-weight: bold; color: rgb(62, 62, 62); background: #ebeaea; padding: 8px 12px; text-align: center;'>Quantity</td>");
        sb.Append("<td width='78' valign='middle' style='font-size: 15px; font-weight: bold; color: rgb(62, 62, 62); background: #ebeaea; padding: 8px 12px; text-align: center;'>Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr>");
            sb.Append("<td width='70' style='vertical-align: top; padding: 8px 12px;'>");
            sb.Append("<img border='0' title='' alt=''src=" + Config.WebSiteMain + "Book / " + ebooksDT.Rows[i]["CategoryID"].ToString() + " style=\"width: 70px;\" /></td>");
            sb.Append("<td width='270' style = 'vertical-align: top; font-size: 15px; color: rgb(62, 62, 62); padding: 8px 12px;'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "</td>");
            sb.Append("<td width='270' style='vertical-align: top; font-size: 15px; color: rgb(62, 62, 62); padding: 8px 12px;'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "</td>");
            sb.Append("<td width='270' style='vertical-align: top; font-size: 15px; color: rgb(62, 62, 62); padding: 8px 12px;text-align: center;'>" + ebooksDT.Rows[i]["Qauntity"].ToString() + "</td>");
            sb.Append("<td width='78' style='font-size: 15px; color: rgb(62, 62, 62); vertical-align: top; padding: 8px 12px; text-align: center;'>$" + ebooksDT.Rows[i]["Amount"].ToString() + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");

        sb.Append("<table width='250' height='auto' cellspacing='5' cellpadding='5' border='0' style='margin-left: auto;'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td style='font-size: 15px; color: rgb(0, 80, 161); font-weight: bold;'>Total Book(s):</td>");
        sb.Append(" <td width='75' style='font-size: 15px; color: rgb(0, 80, 161); font-weight: bold;'>" + qty + "</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='font-size: 15px; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' style='font-size: 15px; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");
        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td style='font-size: 14px; padding: 0 0 10px 0;'>");
        sb.Append("<img border='0' title='' alt=''src=\"https://www.leatodo.com/Banner/104.png\" style=\"max-width: 100%; width: 100%;\" />");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='font-size: 14px; padding: 0 0 10px 0; text-align:center;'><a target='_blank' href='https://play.google.com/store/apps/details?id=com.vrin.leaebooks&hl=en'><img border='0' title='' alt=''src=\"https://www.leatodo.com/images/googleplaystore.png\" style=\"max-width: 100%;\" /></a> <a target='_blank' href='https://itunes.apple.com/us/app/lea-ebooks/id960249388?ls=1&mt=8'><img border='0' title='' alt=''src=\"https://www.leatodo.com/images/appstoreimg.png\" style=\"max-width: 100%;\" /></a>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("<tr>");
        sb.Append("<td style='font-size: 14px; padding: 10px 24px 20px 24px'>");
        sb.Append("<span style='font-weight: bold; display: inline-block; vertical-align: top; '>NOTE :</span> <span style='width: 89%; display: inline-block; vertical-align: top; '>If you have any questions or further query then please contact us:" + "sales@leaebook.com" + "</span>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (contactmail != null)
        {
            Global.SendEmail(email, "Lea  | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(email, "Lea  | Your Order information", sb.ToString());
        }
        try
        {
            DataTable dtLIst = DAL.SqlHelper.ExecuteDataTable(CommandType.Text, "SELECT * FROM Registration WHERE IsActive = 1 AND UserType = 1 AND IsDeleted = 0");
            if (dtLIst.Rows.Count > 0)
            {
                if (dtLIst.Rows[0]["AlternetEmailAddress"].ToString() != "")
                {
                    Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString() + ", " + dtLIst.Rows[0]["AlternetEmailAddress"].ToString(), "Lea | User Order information", sb.ToString());
                }
                else
                {
                    Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString(), "Lea  | User Order information", sb.ToString());
                }
            }
        }
        catch (Exception)
        {

        }
    }

    [WebMethod(Description = "This webservice for allEbooks")]
    public void Wishlist(string CustomerID, string LanguageID)
    {
        string aspJson = "";
        //string aspJson1 = "";
        ObjBookOrders.CustomerID = Convert.ToInt32(CustomerID);
        ObjBookOrders.LanguageID = Convert.ToInt32(LanguageID);
        double Amount = 0;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        dbconnection db = new dbconnection();
        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(CustomerID) + "'");
        DataTable dt = ObjBookOrders.GetWishList();
        double Total = 0.0;
        var Tot = "";
        try
        {
            if (DtEXist.Rows.Count > 0)
            {
                if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
                {
                    Fail("-1", "This User is Deleted....");
                }
                else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
                {
                    Fail("-5", "This User is In-Active....");
                }
                else
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        aspJson += "[";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var quantity = 0;
                            var bookTypeID = 0;
                            var bookPrice = "0";
                            var bookType = "";
                            //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"].ToString());
                            Amount += Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                            //Total += Convert.ToDouble(dt.Rows[i]["Amount"].ToString());
                            aspJson += "{";
                            aspJson += "\"WishID\": \"" + getUnicodeString(dt.Rows[i]["WishID"].ToString()) + "\"," +
                                        "\"CategoryID\": \"" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "\"," +
                                        "\"Category\": \"" + getUnicodeString(dt.Rows[i]["CategoryName"].ToString()) + "\"," +
                                         "\"BookiPhoneImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                                         "\"BookiPadImage\": \"" + Config.WebSiteMain + "Book/" + getUnicodeString(dt.Rows[i]["CategoryID"].ToString()) + "/" + getUnicodeString(dt.Rows[i]["ImagePath"].ToString().Replace("/", "\\/").Replace(".jpg", "_1.jpg")) + "\"," +
                                          "\"BookID\": \"" + getUnicodeString(dt.Rows[i]["BookID"].ToString()) + "\"," +
                                           "\"Bookname\": \"" + getUnicodeString(dt.Rows[i]["Title"].ToString()) + "\"," +
                                           "\"Autohername\": \"" + getUnicodeString(dt.Rows[i]["Autoher"].ToString()) + "\"," +
                                           "\"Pusblisherdate\": \"" + getUnicodeString(dt.Rows[i]["PublishDate1"].ToString()) + "\"," +
                                           "\"Language\": \"" + getUnicodeString(dt.Rows[i]["Language1"].ToString()) + "\"," +
                                           "\"Price\": \"" + getUnicodeString(dt.Rows[i]["FinalPrice1"].ToString()) + "\"," +
                                           "\"Pricecurrency\": \"$\" ," +
                                            "\"BookDescription\": \"" + getUnicodeString(dt.Rows[i]["Description"].ToString()) + "\",";
                            if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                            {
                                quantity++;
                                bookTypeID = 1;
                                bookType = "EBook";
                                bookPrice = dt.Rows[i]["FinalPrice"].ToString();

                            }
                            if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                            {
                                quantity += Convert.ToInt32(dt.Rows[i]["Qauntity"]);
                                bookTypeID = 2;
                                bookType = "PaperBook";
                                bookPrice = dt.Rows[i]["PaperBookFinalPrice"].ToString();
                            }
                            //Total = Convert.ToString(dt.Rows[i]["Amount"]);
                            aspJson += "\"Quantity\": \"" + getUnicodeString(quantity.ToString()) + "\"," +
                                       "\"BookTypeID\": \"" + getUnicodeString(bookTypeID.ToString()) + "\"," +
                                       "\"BookType\": \"" + getUnicodeString(bookType) + "\"," +
                                       "\"Price\": \"" + getUnicodeString(bookPrice) + "\"," +
                                       "\"TotalPrice\": \"" + getUnicodeString(bookPrice) + "\"" + "";
                            aspJson += "},";

                        }
                        aspJson = aspJson.TrimEnd(',');
                        aspJson += "]";
                        var Count = Convert.ToInt32(dt.Rows.Count);

                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                                {
                                    Total += Convert.ToDouble(dt.Rows[i]["PaperBookFinalPrice"]);
                                }
                                if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                                {
                                    Total += Convert.ToDouble(dt.Rows[i]["FinalPrice"]);
                                }
                            }
                        }
                        //Decimal dPrice = 0;
                        //for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        //{
                        //    dPrice += dt.Rows[i].<Decimal>(2);
                        //}

                        // Success5("0", "List of product", Amount.ToString(), aspJson);
                        Success30("0", "List of Product", Count.ToString(), Total.ToString(), Amount.ToString(), aspJson);
                    }
                    else
                    {
                        Fail("-1", "No data found");
                    }
                }
            }
            else
            {
                Fail("-4", "This User is Does not Exist....");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for addCartList")]
    public void CheckCartList(string CustID, string BookId)
    {
        BookOrderBAL BOB = new BookOrderBAL();
        BOB.CustomerID = Convert.ToInt64(CustID);
        BOB.BookID = Convert.ToInt64(BookId);
        DataTable dt = new DataTable();
        dt = BOB.CheckCartlist();
        if (dt != null && dt.Rows.Count > 0)
        {
            Success6("0", "Already exits");
        }
        else
        {
            Fail("1", "Not in cart");
        }
    }

    [WebMethod(Description = "This webservice for updateCartList")]
    public void updateCartList(string OrderID, string Amount, Int32 Quantity)
    {
        try
        {

            if (!string.IsNullOrEmpty(OrderID) && !string.IsNullOrEmpty(Amount) && Quantity >= 1)
            {
                string query = "UPDATE tbl_CustomerCart SET Qauntity=" + Quantity.ToString().Trim() + ",Amount=" + Amount.ToString().Trim() + " WHERE OrderID = " + OrderID.Trim();
                DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, query);
                //Success6("0", "Success");

                #region
                BookOrderBAL ObjBookOrders = new BookOrderBAL();
                ObjBookOrders.OrderID = Convert.ToInt32(OrderID);
                var dt = GetCartListByOrderID(OrderID);
                double TotalCurrency = 0.0;
                var ShippingDetail = "No data found";
                var NationalShippingDetails = "No data found";
                ObjBookOrders.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"].ToString());
                ObjBookOrders.LanguageID = 1;

                DataTable dtAdd = new DataTable();
                var objBookAddDetail = new BookDeliveryAddressBAL();
                objBookAddDetail.UserID = ObjBookOrders.CustomerID;
                dtAdd = objBookAddDetail.GetBookAddressByUser();

                dt = ObjBookOrders.GetCartList();

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var quantity = 0;
                        var bookPrice = "0";
                        if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                        {
                            quantity++;
                            bookPrice = dt.Rows[i]["FinalPrice"].ToString();
                        }
                        if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                        {
                            quantity += Convert.ToInt32(dt.Rows[i]["Qauntity"]);
                            bookPrice = dt.Rows[i]["PaperBookFinalPrice"].ToString();
                        }
                        TotalCurrency += quantity * Convert.ToDouble(bookPrice);
                    }

                    if (dtAdd.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtAdd.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dtAdd.Rows[i]["IsDefault"].ToString()))
                            {
                                ShippingDetail = ShippingCharge(dt.Rows[0]["CustomerID"].ToString(), dtAdd.Rows[i]["BookDeliveryAddID"].ToString());
                                NationalShippingDetails = NationalShippingCharge(dt.Rows[0]["CustomerID"].ToString(), dtAdd.Rows[i]["BookDeliveryAddID"].ToString());
                                break;
                            }
                        }
                    }
                }
                #endregion

                Success6("0", "Removed sucessfully" + "\",\"ShippingDetail\":\"" + ShippingDetail + "\",\"NationalShippingDetails\":\"" + NationalShippingDetails + "\",\"TotalCurrency\":\"" + Decimal.Round(Convert.ToDecimal(TotalCurrency), 2).ToString().Replace(',', '.') + "");
            }
            else
            {
                Fail("-1", "Please Pass All data");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }
    }

    /*[WebMethod(Description = "This webservice for addCartList")]
    public void addCartListNew(string CustID, string BookId, string BookTypeID, Int32 Quantity)
    {
        //BookTypeID = 1 = eBook
        //BookTypeID = 2 = PaperBook
        string[] strType = BookTypeID.Split(',');
        foreach (string bt_id in strType)
        {
            bool IseBook = false;
            bool IsPaperBook = false;
            if (bt_id == "1" && Quantity > 0)
            {
                if (Quantity > 1)
                {
                    Fail("-1", "Quantity is high for eBook to add Book in Cart");
                }
                else
                    IseBook = true;
            }
            else if (bt_id == "2" && Quantity > 0)
            {
                if (Quantity < 1)
                {
                    Fail("-1", "Quantity is low for Paper Book to add Book in Cart");
                }
                else
                    IsPaperBook = true;
            }
            else
            {
                Fail("-1", "Book Type ID or Quantity is not proper to add book in cart");
            }
            int cnt1 = 0;

            if (CustID != null && CustID.ToString() != "" && BookId != null && BookId.ToString() != "" && (IseBook || IsPaperBook))
            {
                string[] str = BookId.Split(',');
                foreach (string BookID in str)
                {
                    BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                    objPurchase.UserID = Convert.ToInt32(CustID);
                    objPurchase.PurchaseDate = System.DateTime.Now;
                    objPurchase.OrderID = 0;
                    if (BookId != null && BookId.ToString() != "")
                    {
                        objPurchase.BookID = Convert.ToInt16(BookID.ToString());
                    }
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        objPurchase.LanguageID = 2;
                    }
                    else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                    {
                        objPurchase.LanguageID = 1;
                    }
                    DataTable Dt = objPurchase.getUserLibrary();
                    if (Dt != null && Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                            {
                                if (IseBook)
                                {
                                    if (Convert.ToBoolean(Dt.Rows[i]["IseBook"]))
                                    {
                                        cnt1++;
                                    }
                                }
                            }
                        }
                        if (cnt1 > 0)
                        {
                            Fail("-4", "You have already purchased this ebook. do you want to move in my library?");
                        }
                        else
                        {
                            int result = 0;
                            int count = 0;
                            ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                            ObjBookOrders.BookID = Convert.ToInt32(BookID);
                            ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);

                            DataTable DT = ObjBookOrders.GetCartList();
                            if (DT != null && DT.Rows.Count > 0)
                            {
                                ObjBookOrders.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                                for (int i = 0; i < DT.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                    {
                                        if (IseBook)
                                        {
                                            if (Convert.ToBoolean(DT.Rows[i]["IseBook"]))
                                            {
                                                count++;
                                            }
                                        }
                                        if (IsPaperBook)
                                        {
                                            if (Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                            {
                                                count++;
                                            }
                                        }
                                    }
                                }
                                if (count > 0)
                                {
                                    Fail("-1", "You already have this book in your cart.");
                                    break;
                                }
                                else
                                {
                                    if (IseBook)
                                    {
                                        ObjBookOrders.IseBook = true;
                                        ObjBookOrders.IspaperBook = false;
                                        ObjBookOrders.Quantity = 0;
                                        result = ObjBookOrders.InsertCustomerCart1();
                                    }
                                    if (IsPaperBook)
                                    {
                                        ObjBookOrders.IseBook = false;
                                        ObjBookOrders.IspaperBook = true;
                                        ObjBookOrders.Quantity = Quantity;
                                        result = ObjBookOrders.InsertCustomerCart1();
                                    }
                                    //result = ObjBookOrders.InsertCustomerCart1();
                                }
                            }
                            else
                            {
                                string str1 = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                                if (IseBook)
                                {
                                    ObjBookOrders.IseBook = true;
                                    ObjBookOrders.IspaperBook = false;
                                    ObjBookOrders.Quantity = 0;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                if (IsPaperBook)
                                {
                                    ObjBookOrders.IseBook = false;
                                    ObjBookOrders.IspaperBook = true;
                                    ObjBookOrders.Quantity = Quantity;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                //result = ObjBookOrders.InsertCustomerCart1();
                            }
                            try
                            {
                                if (result != 0)
                                {
                                    Success6("0", "Added sucessfully");
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Fail("-2", "Cannot connect to server.");
                            }
                        }
                    }//end
                    else
                    {
                        int result = 0;
                        int count = 0;
                        ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                        ObjBookOrders.BookID = Convert.ToInt32(BookID);
                        ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);

                        DataTable DT = ObjBookOrders.GetCartList();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            ObjBookOrders.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                {
                                    if (IseBook)
                                    {
                                        if (Convert.ToBoolean(DT.Rows[i]["IseBook"]))
                                        {
                                            count++;
                                        }
                                    }
                                    if (IsPaperBook)
                                    {
                                        if (Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                        {
                                            count++;
                                        }
                                    }
                                }
                            }
                            if (count > 0)
                            {
                                Fail("-1", "You already have this book in your cart");
                                break;
                            }
                            else
                            {
                                if (IseBook)
                                {
                                    ObjBookOrders.IseBook = true;
                                    ObjBookOrders.IspaperBook = false;
                                    ObjBookOrders.Quantity = 0;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                if (IsPaperBook)
                                {
                                    ObjBookOrders.IseBook = false;
                                    ObjBookOrders.IspaperBook = true;
                                    ObjBookOrders.Quantity = Quantity;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                //result = ObjBookOrders.InsertCustomerCart1();
                            }
                        }
                        else
                        {
                            string str1 = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                            ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                            if (IseBook)
                            {
                                ObjBookOrders.IseBook = true;
                                ObjBookOrders.IspaperBook = false;
                                ObjBookOrders.Quantity = 0;
                                result = ObjBookOrders.InsertCustomerCart1();
                            }
                            if (IsPaperBook)
                            {
                                ObjBookOrders.IseBook = false;
                                ObjBookOrders.IspaperBook = true;
                                ObjBookOrders.Quantity = Quantity;
                                result = ObjBookOrders.InsertCustomerCart1();
                            }
                            //result = ObjBookOrders.InsertCustomerCart1();
                        }
                        try
                        {
                            if (result != 0)
                            {

                                ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                                ObjBookOrders.LanguageID = 1;
                                DataTable dtCart = ObjBookOrders.GetCartList();

                                Success7("0", dtCart.Rows.Count, "Added sucessfully");
                                break;
                            }
                        }
                        catch (Exception ex)
                        {
                            Fail("-2", "Cannot connect to server. " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                Fail("-1", "Please provide all required fields");
            }
        }
    }
    */

    [WebMethod(Description = "This webservice for addCartList")]
    public void addCartList(string CustID, string BookId, string BookTypeID, string Quantity)
    {
        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(CustID) + "'");
        if (DtEXist.Rows.Count > 0)
        {
            if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
            {
                Fail("-1", "This User is Deleted....");
            }
            else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
            {
                Fail("-5", "This User is In-Active....");
            }
            else
            {
                string[] strType = BookTypeID.Split(',');
                string[] str = BookId.Split(',');
                string[] Qua = Quantity.Split(',');
                if (CustID != null && CustID.ToString() != "" && BookId != null && BookId.ToString() != "")
                {
                    #region Book
                    for (int z = 0; z < strType.Length; z++)
                    {
                        var bt_id = strType[z];
                        var BookID = str[z];
                        var Quantitys = Qua[z];
                        bool IseBook = false;
                        bool IsPaperBook = false;
                        if (bt_id == "1" && Convert.ToInt32(Quantitys) > 0)
                        {
                            if (Convert.ToInt32(Quantitys) > 1)
                            {
                                Fail("-1", "Quantity is high for eBook to add Book in Cart");
                            }
                            else
                                IseBook = true;
                        }
                        else if (bt_id == "2" && Convert.ToInt32(Quantitys) > 0)
                        {
                            if (Convert.ToInt32(Quantitys) < 1)
                            {
                                Fail("-1", "Quantity is low for Paper Book to add Book in Cart");
                            }
                            else
                                IsPaperBook = true;
                        }
                        else
                        {
                            Fail("-1", "Book Type ID or Quantity is not proper to add book in cart");


                        }
                        int cnt1 = 0;

                        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                        objPurchase.UserID = Convert.ToInt32(CustID);
                        objPurchase.PurchaseDate = System.DateTime.Now;
                        objPurchase.OrderID = 0;
                        if (BookId != null && BookId.ToString() != "")
                        {
                            objPurchase.BookID = Convert.ToInt16(BookID.ToString());
                        }
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            objPurchase.LanguageID = 2;
                        }
                        else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                        {
                            objPurchase.LanguageID = 1;
                        }
                        DataTable Dt = objPurchase.getUserLibrary();
                        if (Dt != null && Dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < Dt.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                                {
                                    if (IseBook)
                                    {
                                        if (Convert.ToBoolean(Dt.Rows[i]["IseBook"]))
                                        {
                                            cnt1++;
                                        }
                                    }
                                }
                            }
                            if (cnt1 > 0)
                            {
                                Fail("-4", "You have already purchased this ebook. do you want to move in my library?");
                                return;
                            }
                            else
                            {
                                int result = 0;
                                int count = 0;
                                ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                                ObjBookOrders.BookID = Convert.ToInt32(BookID);
                                ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);

                                DataTable DT = ObjBookOrders.GetCartList();
                                if (DT != null && DT.Rows.Count > 0)
                                {
                                    ObjBookOrders.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                                    for (int i = 0; i < DT.Rows.Count; i++)
                                    {
                                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                        {
                                            if (IseBook)
                                            {
                                                if (Convert.ToBoolean(DT.Rows[i]["IseBook"]))
                                                {
                                                    count++;
                                                }
                                            }
                                            if (IsPaperBook)
                                            {
                                                if (Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                                {
                                                    count++;
                                                }
                                            }
                                        }
                                    }
                                    if (count > 0)
                                    {
                                        Fail("-1", "You already have this book in your cart.");

                                        break;
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(ObjBookOrders.OrderNo))
                                        {
                                            string str1 = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                            ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                                        }
                                        if (IseBook)
                                        {
                                            ObjBookOrders.IseBook = true;
                                            ObjBookOrders.IspaperBook = false;
                                            ObjBookOrders.Quantity = 0;
                                            result = ObjBookOrders.InsertCustomerCart1();
                                        }
                                        if (IsPaperBook)
                                        {
                                            ObjBookOrders.IseBook = false;
                                            ObjBookOrders.IspaperBook = true;
                                            ObjBookOrders.Quantity = Convert.ToInt32(Quantitys);
                                            result = ObjBookOrders.InsertCustomerCart1();
                                        }
                                        try
                                        {
                                            ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                                            ObjBookOrders.LanguageID = 1;
                                            DataTable dtCart = ObjBookOrders.GetCartList();
                                            Success7("0", dtCart.Rows.Count, "Added sucessfully");
                                        }
                                        catch (Exception ex)
                                        {
                                            Fail("-2", "Cannot connect to server. " + ex.Message);
                                        }
                                    }
                                }
                                else
                                {
                                    string str1 = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                    ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                                    if (IseBook)
                                    {
                                        ObjBookOrders.IseBook = true;
                                        ObjBookOrders.IspaperBook = false;
                                        ObjBookOrders.Quantity = 0;
                                        result = ObjBookOrders.InsertCustomerCart1();
                                    }
                                    if (IsPaperBook)
                                    {
                                        ObjBookOrders.IseBook = false;
                                        ObjBookOrders.IspaperBook = true;
                                        ObjBookOrders.Quantity = Convert.ToInt32(Quantitys);
                                        result = ObjBookOrders.InsertCustomerCart1();
                                    }
                                    try
                                    {
                                        ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                                        ObjBookOrders.LanguageID = 1;
                                        DataTable dtCart = ObjBookOrders.GetCartList();
                                        Success7("0", dtCart.Rows.Count, "Added sucessfully");
                                    }
                                    catch (Exception ex)
                                    {
                                        Fail("-2", "Cannot connect to server. " + ex.Message);
                                    }
                                }
                            }
                        }//end
                        else
                        {
                            int result = 0;
                            int count = 0;
                            ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                            ObjBookOrders.BookID = Convert.ToInt32(BookID);
                            ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);

                            DataTable DT = ObjBookOrders.GetCartList();
                            if (DT != null && DT.Rows.Count > 0)
                            {
                                ObjBookOrders.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                                for (int i = 0; i < DT.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                    {
                                        if (IseBook)
                                        {
                                            if (Convert.ToBoolean(DT.Rows[i]["IseBook"]))
                                            {
                                                count++;
                                            }
                                        }
                                        if (IsPaperBook)
                                        {
                                            if (Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                            {
                                                count++;
                                            }
                                        }
                                    }
                                }
                                if (count > 0)
                                {
                                    //Fail("-1", "You already have this book in your cart");
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(ObjBookOrders.OrderNo))
                                    {
                                        string str1 = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                        ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                                    }
                                    if (IseBook)
                                    {
                                        ObjBookOrders.IseBook = true;
                                        ObjBookOrders.IspaperBook = false;
                                        ObjBookOrders.Quantity = 0;
                                        result = ObjBookOrders.InsertCustomerCart1();
                                    }
                                    if (IsPaperBook)
                                    {
                                        ObjBookOrders.IseBook = false;
                                        ObjBookOrders.IspaperBook = true;
                                        ObjBookOrders.Quantity = Convert.ToInt32(Quantitys);
                                        result = ObjBookOrders.InsertCustomerCart1();
                                    }
                                    try
                                    {
                                        ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                                        ObjBookOrders.LanguageID = 1;
                                        DataTable dtCart = ObjBookOrders.GetCartList();
                                        Success7("0", dtCart.Rows.Count, "Added sucessfully");
                                    }
                                    catch (Exception ex)
                                    {
                                        Fail("-2", "Cannot connect to server. " + ex.Message);
                                    }
                                    //result = ObjBookOrders.InsertCustomerCart1();
                                }
                            }
                            else
                            {
                                string str1 = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                                if (IseBook)
                                {
                                    ObjBookOrders.IseBook = true;
                                    ObjBookOrders.IspaperBook = false;
                                    ObjBookOrders.Quantity = 0;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                if (IsPaperBook)
                                {
                                    ObjBookOrders.IseBook = false;
                                    ObjBookOrders.IspaperBook = true;
                                    ObjBookOrders.Quantity = Convert.ToInt32(Quantitys);
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                //result = ObjBookOrders.InsertCustomerCart1();
                                try
                                {
                                    ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                                    ObjBookOrders.LanguageID = 1;
                                    DataTable dtCart = ObjBookOrders.GetCartList();
                                    Success7("0", dtCart.Rows.Count, "Added sucessfully");
                                }
                                catch (Exception ex)
                                {
                                    Fail("-2", "Cannot connect to server. " + ex.Message);
                                }
                            }

                        }

                    }

                    #endregion

                }
                else
                {
                    Fail("-1", "Please provide all required fields");
                    return;
                }
            }
        }
        else
        {
            Fail("-4", "This User is Does not Exist....");
        }

    }

    [WebMethod(Description = "This webservice for addCartList")]
    public void addToMyLibrary(string CustID, string BookID)
    {
        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(CustID) + "'");
        if (DtEXist.Rows.Count > 0)
        {
            if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
            {
                Fail("-1", "This User is Deleted....");
            }
            else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
            {
                Fail("-5", "This User is In-Active....");
            }
            else
            {
                //string aspJson = "";
                int result = 0;
                int count = 0;
                objPurchase.UserID = Convert.ToInt32(CustID);
                objPurchase.BookID = Convert.ToInt32(BookID);
                objPurchase.PurchaseDate = Convert.ToDateTime(System.DateTime.Now);

                objBook.BookID = Convert.ToInt32(BookID);
                objBook.LangaugeID = 1;
                DataTable dt = objBook.getBookDetails();
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dt.Rows[0]["IsFree"]) == true)
                    {
                        DataTable DT = objPurchase.getUserLibrary();
                        if (DT != null && DT.Rows.Count > 0)
                        {

                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(DT.Rows[i]["BookID"]) == objPurchase.BookID)
                                {
                                    count++;
                                }

                            }
                            if (count > 0)
                            {
                                //Message1("You already have this book in your cart");
                                Fail("-1", "You already have this book in your Library");
                            }
                            else
                            {
                                string str = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                objPurchase.OrderID = Convert.ToInt64(Regex.Replace(str, "[^0-9A-Za-z]+", ""));
                                result = objPurchase.AddToBookPurchase();
                            }
                        }
                        else
                        {
                            string str = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                            objPurchase.OrderID = Convert.ToInt64(Regex.Replace(str, "[^0-9A-Za-z]+", ""));
                            result = objPurchase.AddToBookPurchase();
                        }
                    }

                }
                try
                {
                    if (result != 0)
                    {


                        Success6("0", "Added sucessfully");
                    }
                    //else
                    //{
                    //    // retJSON = Message("No category found.");
                    //    Fail("-1", "No data found");
                    //}
                }
                catch (Exception ex)
                {
                    Fail("-2", "Cannot connect to server.");
                }
            }
        }
        else
        {
            Fail("-4", "This User is Does not Exist....");
        }
    }

    [WebMethod(Description = "This webservice for addCustomerWishList")]
    public void addCustomerWishList(string CustID, string BookID)
    {
        //string aspJson = "";
        int cnt1 = 0;
        int result1 = 0;
        int result = 0;
        ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
        DataTable DT = ObjBookOrders.GetWishList();
        if (CustID != null && CustID.ToString() != "")
        {
            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
            objPurchase.UserID = Convert.ToInt32(CustID);
            objPurchase.PurchaseDate = System.DateTime.Now;
            objPurchase.OrderID = 0;
            if (BookID != null && BookID.ToString() != "")
            {
                objPurchase.BookID = Convert.ToInt16(BookID.ToString());
            }
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                objPurchase.LanguageID = 2;
            }
            else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
            {
                objPurchase.LanguageID = 1;
            }
            DataTable Dt = objPurchase.getUserLibrary();
            BookPurchaseBAL ObjPurchase = new BookPurchaseBAL();

            if (Dt != null && Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                    {
                        if (Convert.ToBoolean(Dt.Rows[i]["Isebook"]))
                        {
                            cnt1++;
                        }
                    }
                }
                if (cnt1 > 0)
                {
                    Fail("-4", "You have already purchased this ebook.");
                }
                else
                {

                    int count = 0;
                    ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                    ObjPurchase.UserID = Convert.ToInt32(CustID);
                    ObjBookOrders.BookID = Convert.ToInt32(BookID);
                    ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                    //DataTable DT = ObjBookOrders.GetWishList();
                    DataTable dt1 = ObjPurchase.GetAllOrderbyUserID();

                    ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                    string str1 = (CustID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                    ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                    ObjBookOrders.LanguageID = 1;
                    DataTable DTcart = ObjBookOrders.GetCartList();

                    if (DT != null && DT.Rows.Count > 0)
                    {
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                            {

                                if (Convert.ToBoolean(Dt.Rows[i]["Ispaperbook"]))
                                {
                                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                    {
                                        count++;
                                    }
                                    else
                                    {
                                        result = ObjBookOrders.InsertCustomerWishList1();
                                    }
                                }
                            }
                        }
                        if (count > 0)
                        {
                            //Message1("You already have this book in your WishList");
                            Fail("-1", "You already have this book in your WishList");
                        }
                        else
                        {
                            result = ObjBookOrders.InsertCustomerWishList1();
                        }
                    }
                    else
                    {
                        result = ObjBookOrders.InsertCustomerWishList1();
                    }
                    try
                    {
                        if (result != 0)
                        {
                            //Success6("0", "Added sucessfully");
                            Success25("0", "Added Successfully", DT.Rows.Count);
                        }
                    }
                    catch (Exception ex)
                    {
                        Fail("-2", "Cannot connect to server.");
                    }
                }
            }//end
            else
            {
                //int result = 0;
                int count = 0;
                ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                ObjBookOrders.BookID = Convert.ToInt32(BookID);
                ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                //DataTable DT = ObjBookOrders.GetWishList();
                dt = ObjPurchase.GetAllOrderbyUserID();
                if (DT != null && DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                        {
                            count++;
                        }

                    }
                    if (count > 0)
                    {
                        //Message1("You already have this book in your WishList");
                        Fail("-1", "You already have this book in your WishList");
                    }
                    else
                    {
                        result = ObjBookOrders.InsertCustomerWishList1();
                    }
                }
                else
                {
                    result = ObjBookOrders.InsertCustomerWishList1();
                }
                try
                {
                    if (result != 0)
                    {
                        Success25("0", "Added Successfully", DT.Rows.Count);
                        //Success6("0", "Added sucessfully");
                    }
                    //else
                    //{
                    //    // retJSON = Message("No category found.");
                    //    Fail("-1", "No data found");
                    //}
                }
                catch (Exception ex)
                {
                    Fail("-2", "Cannot connect to server.");
                }
            }
        }
        else
        {
            //int result = 0;
            int count = 0;
            ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
            ObjBookOrders.BookID = Convert.ToInt32(BookID);
            ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
            //DataTable DT = ObjBookOrders.GetWishList();
            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                    {
                        count++;
                    }

                }
                if (count > 0)
                {
                    //Message1("You already have this book in your WishList");
                    Fail("-1", "You already have this book in your WishList");
                }
                else
                {
                    result = ObjBookOrders.InsertCustomerWishList1();
                }
            }
            else
            {
                result = ObjBookOrders.InsertCustomerWishList1();
            }

            try
            {
                if (result != 0)
                {
                    //Success6("0", "Added sucessfully");
                    Success25("0", "Added Successfully", DT.Rows.Count);
                }
                //else
                //{
                //    // retJSON = Message("No category found.");
                //    Fail("-1", "No data found");
                //}
            }
            catch (Exception ex)
            {
                Fail("-2", "Cannot connect to server.");
            }
        }
    }

    [WebMethod(Description = "This webservice for addCustomerWishList")]
    public void addCustomerWishListAndroid(string CustID, string BookID, Int32 BookTypeID)
    {
        DataTable DtEXist = db.filltable("Select * from Registration Where RegistrationID='" + Convert.ToInt32(CustID) + "'");
        if (DtEXist.Rows.Count > 0)
        {
            if (Convert.ToBoolean(DtEXist.Rows[0]["IsDeleted"]) == true)
            {
                Fail("-1", "This User is Deleted....");
            }
            else if (Convert.ToBoolean(DtEXist.Rows[0]["IsActive"]) == false)
            {
                Fail("-5", "This User is In-Active....");
            }
            else
            {
                int cnt1 = 0;
                int result1 = 0;
                if (CustID != null && CustID.ToString() != "")
                {
                    BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                    objPurchase.UserID = Convert.ToInt32(CustID);
                    objPurchase.PurchaseDate = System.DateTime.Now;
                    objPurchase.OrderID = 0;
                    if (BookID != null && BookID.ToString() != "")
                    {
                        objPurchase.BookID = Convert.ToInt16(BookID.ToString());
                    }
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        objPurchase.LanguageID = 2;
                    }
                    else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                    {
                        objPurchase.LanguageID = 1;
                    }
                    DataTable Dt = objPurchase.getUserLibrary();
                    if (Dt != null && Dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                            {
                                if (Convert.ToBoolean(Dt.Rows[i]["Isebook"]))
                                {
                                    cnt1++;
                                }
                            }

                        }
                        if (cnt1 > 0)
                        {
                            Fail("-4", "You have already purchased this ebook.");
                        }
                        else
                        {
                            int result = 0;
                            int count = 0;
                            ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                            ObjBookOrders.BookID = Convert.ToInt32(BookID);
                            ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                            DataTable DT = ObjBookOrders.GetWishList();
                            if (DT != null && DT.Rows.Count > 0)
                            {
                                for (int i = 0; i < DT.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                    {
                                        if (Convert.ToBoolean(Dt.Rows[i]["Ispaperbook"]))
                                        {
                                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                            {
                                                count++;
                                            }
                                            else
                                            {
                                                result = ObjBookOrders.InsertCustomerWishList1();
                                            }
                                        }
                                    }

                                }
                                if (count > 0)
                                {
                                    Fail("-1", "You already have this book in your WishList");
                                }
                                else
                                {
                                    if (BookTypeID == 1)
                                    {
                                        ObjBookOrders.IspaperBook = false;
                                        ObjBookOrders.IseBook = true;
                                    }
                                    else if (BookTypeID == 2)
                                    {
                                        ObjBookOrders.IspaperBook = true;
                                        ObjBookOrders.IseBook = false;
                                    }
                                    result = ObjBookOrders.InsertCustomerWishList_BookType();
                                }
                            }
                            else
                            {
                                if (BookTypeID == 1)
                                {
                                    ObjBookOrders.IspaperBook = false;
                                    ObjBookOrders.IseBook = true;
                                }
                                else if (BookTypeID == 2)
                                {
                                    ObjBookOrders.IspaperBook = true;
                                    ObjBookOrders.IseBook = false;
                                }
                                result = ObjBookOrders.InsertCustomerWishList_BookType();
                            }
                            try
                            {
                                if (result != 0)
                                {
                                    Success6("0", "Added sucessfully");
                                }
                            }
                            catch (Exception ex)
                            {
                                Fail("-2", "Cannot connect to server.");
                            }
                        }
                    }
                    else
                    {
                        int result = 0;
                        int count = 0;
                        ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                        ObjBookOrders.BookID = Convert.ToInt32(BookID);
                        ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                        DataTable DT = ObjBookOrders.GetWishList();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                {
                                    count++;
                                }

                            }
                            if (count > 0)
                            {
                                Fail("-1", "You already have this book in your WishList");
                            }
                            else
                            {
                                if (BookTypeID == 1)
                                {
                                    ObjBookOrders.IspaperBook = false;
                                    ObjBookOrders.IseBook = true;
                                }
                                else if (BookTypeID == 2)
                                {
                                    ObjBookOrders.IspaperBook = true;
                                    ObjBookOrders.IseBook = false;
                                }
                                result = ObjBookOrders.InsertCustomerWishList_BookType();
                            }
                        }
                        else
                        {
                            if (BookTypeID == 1)
                            {
                                ObjBookOrders.IspaperBook = false;
                                ObjBookOrders.IseBook = true;
                            }
                            else if (BookTypeID == 2)
                            {
                                ObjBookOrders.IspaperBook = true;
                                ObjBookOrders.IseBook = false;
                            }
                            result = ObjBookOrders.InsertCustomerWishList_BookType();
                        }
                        try
                        {
                            if (result != 0)
                            {
                                Success6("0", "Added sucessfully");
                            }
                        }
                        catch (Exception ex)
                        {
                            Fail("-2", "Cannot connect to server.");
                        }
                    }
                }
                else
                {
                    int result = 0;
                    int count = 0;
                    ObjBookOrders.CustomerID = Convert.ToInt32(CustID);
                    ObjBookOrders.BookID = Convert.ToInt32(BookID);
                    ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                    DataTable DT = ObjBookOrders.GetWishList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                            {
                                count++;
                            }

                        }
                        if (count > 0)
                        {
                            Fail("-1", "You already have this book in your WishList");
                        }
                        else
                        {
                            if (BookTypeID == 1)
                            {
                                ObjBookOrders.IspaperBook = false;
                                ObjBookOrders.IseBook = true;
                            }
                            else if (BookTypeID == 2)
                            {
                                ObjBookOrders.IspaperBook = true;
                                ObjBookOrders.IseBook = false;
                            }
                            result = ObjBookOrders.InsertCustomerWishList_BookType();
                        }
                    }
                    else
                    {
                        if (BookTypeID == 1)
                        {
                            ObjBookOrders.IspaperBook = false;
                            ObjBookOrders.IseBook = true;
                        }
                        else if (BookTypeID == 2)
                        {
                            ObjBookOrders.IspaperBook = true;
                            ObjBookOrders.IseBook = false;
                        }
                        result = ObjBookOrders.InsertCustomerWishList_BookType();
                    }
                    try
                    {
                        if (result != 0)
                        {
                            Success6("0", "Added sucessfully");
                        }

                    }
                    catch (Exception ex)
                    {
                        Fail("-2", "Cannot connect to server.");
                    }
                }
            }
        }
    }

    [WebMethod(Description = "This webservice for Removecartlist")]
    public void Removecartlist(string OrderID)
    {
        //string aspJson = "";
        ObjBookOrders.OrderID = Convert.ToInt32(OrderID);
        var dt = GetCartListByOrderID(OrderID);
        int result = ObjBookOrders.DeleteItemfromUserCart();
        var ShippingDetail = "No data found";
        var NationalShippingDetails = "No data found";
        decimal Amount = 0;

        try
        {
            ObjBookOrders.CustomerID = Convert.ToInt32(dt.Rows[0]["CustomerID"].ToString());
            ObjBookOrders.LanguageID = 1;

            DataTable dtAdd = new DataTable();
            var objBookAddDetail = new BookDeliveryAddressBAL();
            objBookAddDetail.UserID = ObjBookOrders.CustomerID;
            dtAdd = objBookAddDetail.GetBookAddressByUser();

            dt = ObjBookOrders.GetCartList();
            try
            {

                if (dt != null && dt.Rows.Count > 0)
                {
                    var defaultAddressId = "0";
                    if (dtAdd.Rows.Count > 0)
                    {
                        ShippingDetail = ShippingCharge(ObjBookOrders.CustomerID.ToString(), dtAdd.Rows[0]["BookDeliveryAddID"].ToString());
                        NationalShippingDetails = NationalShippingCharge(ObjBookOrders.CustomerID.ToString(), dtAdd.Rows[0]["BookDeliveryAddID"].ToString());
                        defaultAddressId = dtAdd.Rows[0]["BookDeliveryAddID"].ToString();
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var quantity = 0;
                        var bookPrice = "0";
                        if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                        {
                            quantity++;
                            bookPrice = dt.Rows[i]["FinalPrice"].ToString();
                        }
                        if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                        {
                            quantity += Convert.ToInt32(dt.Rows[i]["Qauntity"]);
                            bookPrice = dt.Rows[i]["PaperBookFinalPrice"].ToString();
                        }

                        Amount += quantity * Convert.ToDecimal(bookPrice);
                    }
                }
                try
                {
                    if (result != null)
                    {
                        Success6("0", "Removed sucessfully" + "\",\"ShippingDetail\":\"" + ShippingDetail + "\",\"NationalShippingDetails\":\"" + NationalShippingDetails + "\",\"TotalCurrency\":\"" + Decimal.Round(Convert.ToDecimal(Amount), 2).ToString().Replace(',', '.') + "");
                    }
                    else
                    {
                        // retJSON = Message("No category found.");
                        Fail("-1", "No data found");
                    }
                }
                catch (Exception ex)
                {
                    Fail("-2", "Cannot connect to server.");
                }
            }
            catch (Exception)
            {
                Fail("-2", "Cannot connect to server.");
            }
        }
        catch (Exception)
        {
            Success6("0", "Removed sucessfully" + "\",\"ShippingDetail\":\"" + ShippingDetail + "\",\"TotalCurrency\":\"" + Amount + "");
        }
    }

    public DataTable GetCartListByOrderID(string OrderID)
    {
        System.Data.SqlClient.SqlParameter[] Params = new SqlParameter[1];
        Int16 index = 0;

        Params[index] = new SqlParameter("@OrderID", OrderID);
        index++;

        DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetCartListByOrderID", Params);

        return dt;

    }

    [WebMethod(Description = "This webservice for Removecartlist")]
    public void Removewishlist(string WishID)
    {
        //string aspJson = "";

        ObjBookOrders.WishID = Convert.ToInt32(WishID);


        int result = ObjBookOrders.DeleteItemfromUserWishList();
        try
        {
            if (result != null)
            {


                Success6("0", "Removed sucessfully");
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for MoveToCartList")]
    public void MoveToCartList(string CustomerID)
    {
        try
        {
            int count = 0;
            //string aspJson = "";
            ObjBookOrders.CustomerID = Convert.ToInt32(CustomerID);
            DataTable WishDT = ObjBookOrders.GetWishList();
            int result = 0;
            int cnt = 0;

            if (WishDT != null && WishDT.Rows.Count > 0)
            {
                for (int i = 0; i < WishDT.Rows.Count; i++)
                {
                    ObjBookOrders.WishID = Convert.ToInt32(WishDT.Rows[i]["WishID"]);
                    ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                    string str1 = (CustomerID.ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                    ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                    ObjBookOrders.LanguageID = 1;
                    DataTable DT = ObjBookOrders.GetCartList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        int cnt1 = 0;
                        for (int j = 0; j < DT.Rows.Count; j++)
                        {
                            if (Convert.ToInt32(DT.Rows[j]["BookID"]) == Convert.ToInt32(WishDT.Rows[i]["BookID"]))
                            {
                                count++;
                                cnt1++;
                                ObjBookOrders.DeleteItemfromUserWishList();
                            }
                        }
                        if (cnt1 == 0)
                        {
                            //result = ObjBookOrders.MoveToCustomerCart();
                            result = ObjBookOrders.MoveToCustomerCart_OrderNo();
                            cnt++;
                        }
                    }
                    else
                    {
                        //result = ObjBookOrders.MoveToCustomerCart();
                        result = ObjBookOrders.MoveToCustomerCart_OrderNo();
                        cnt++;
                    }
                }
                if (count > 0 && cnt == 0)
                {
                    //Message1("You already have this book in your cart");
                    Fail("-1", "You already have this book in your cart");
                    count = 0;
                    //ObjBookOrders.DeleteItemfromUserWishList();
                }
                else if (cnt != 0)
                {

                    ObjBookOrders.CustomerID = Convert.ToInt32(CustomerID);
                    ObjBookOrders.LanguageID = 1;
                    DataTable dtCart = ObjBookOrders.GetCartList();

                    Success7("0", dtCart.Rows.Count, "Added sucessfully");
                }
            }
            else
            {
                Fail("-1", "No data found");
            }

        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }


    }

    [WebMethod(Description = "This webservice for Checkout")]
    public void Checkout(string CustomerID, string address, string ShippingType, string PaymentFrom)
    {
        //string aspJson = "";
        if (ShippingType == "1") // For DHL shipping
        {
            ShippingType = "DHL";
        }
        else if (ShippingType == "2") // For National
        {
            ShippingType = "National";
        }
        else
        {
            ShippingType = "";
        }
        ObjBookOrders.CustomerID = Convert.ToInt32(CustomerID);
        DataTable WishDT = ObjBookOrders.GetCartList();
        int result = 0;
        if (WishDT != null && WishDT.Rows.Count > 0)
        {
            for (int i = 0; i < WishDT.Rows.Count; i++)
            {
                objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);
                objPurchase.PurchaseDate = Convert.ToDateTime(System.DateTime.Now);
                if (PaymentFrom == "1")
                {
                    PaymentFrom = "App";
                }
                else
                {
                    PaymentFrom = "";
                }
                result = objPurchase.MoveToBookPurchase(address, "0.00", ShippingType, PaymentFrom);
            }
        }
        try
        {
            if (result != 0)
            {


                Success6("0", "Added sucessfully");
            }
            else
            {
                // retJSON = Message("No category found.");
                Fail("-1", "No data found");
            }
        }
        catch (Exception ex)
        {
            Fail("-2", "Cannot connect to server.");
        }

    }

    [WebMethod(Description = "This webservice for forgot password")]
    public void cms(string LanguageID, string cmsID)
    {
        CmsBAL CB = new CmsBAL();
        CB.ID = Convert.ToInt32(cmsID);
        CB.LanguageID = Convert.ToInt32(LanguageID);
        DataTable dt = CB.SelectcmsByID();
        string aspJson = "";
        if (dt.Rows.Count > 0 && dt != null)
        {
            aspJson = "[{\"ID\": \"" + getUnicodeString(dt.Rows[0]["ID"].ToString()) + "\"," +
                             "\"Title\": \"" + getUnicodeString(dt.Rows[0]["Title"].ToString()) + "\"," +
                             "\"Description\": \"" + getUnicodeString(dt.Rows[0]["Description"].ToString()) + "\"" + "}]";

            Success2("0", " CMS lisitng done.", aspJson);
        }
        // retJSON = Success4("0", "CMS listing is done", dt.Rows[0]["Description"].ToString());
        else
        {
            Fail("-1 ", "= No data found");
        }
    }

    [WebMethod(Description = "This webservice for forgot password")]
    public void ForgotPassword(string EmailAddress)
    {
        objUser.EmailAddress = EmailAddress;
        string ContactEmail = "";
        WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
        DataTable DT = WSB.GetAllWebseetings();
        if (DT != null && DT.Rows.Count > 0)
        {
            ContactEmail = DT.Rows[0]["ContactUs"].ToString();
        }

        if (Global.EmailAddressCheck(EmailAddress))
        {
            objUser.EmailAddress = EmailAddress;
            DataTable dt = objUser.ForgotPassword();
            if (dt.Rows.Count > 0 && dt != null)
            {
                //   string body = "";

                //    body = body +
                // "                <head> "
                //+ "    <style type=\"text/css\"> "
                //+ "        p.MsoNormal "
                //+ "        { "
                //+ "            margin-top: 0in; "
                //+ "            margin-right: 0in; "
                //+ "            margin-bottom: 10.0pt; "
                //+ "            margin-left: 0in; "
                //+ "            line-height: 115%; "
                //+ "            font-size: 11.0pt; "
                //+ "            font-family: \"Calibri\" , \"sans-serif\"; "
                //+ "        } "
                //+ "    </style> "
                //+ "</head> "
                //+ "<table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"2\" > "
                //+ "    <tr> "
                //+ "        <td style=\"background-color: rgb(10, 109, 10);\"> "
                //+ "            <img src=\"http://themagz.net/client/images/logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" /> "
                //+ "        </td> "
                //+ "    </tr> "
                //+ "    <tr> "
                //+ "        <td valign=\"top\"> "
                //+ "            <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
                //+ "                <br /> ";






                //    body = body + "<table>";
                //    body = body + "<tr>";
                //    body = body + "<td>Dear User, ";
                //    body = body + "</td>";
                //    body = body + "</tr>";
                //    body = body + "<tr>";
                //    body = body + "<td> Your Credentials for theMagz.net are as below. ";
                //    body = body + "</td>";
                //    body = body + "</tr>";
                //    //body = body + "<tr>";
                //    //body = body + "<td> URL: http://theMagz.net/Admin/Default.aspx";
                //    //body = body + "</td>";
                //    body = body + "</tr>";
                //    body = body + "<tr>";
                //    body = body + "<td> User Name : " + dt.Rows[0]["EmailAddress"].ToString() + " ";
                //    body = body + "</td>";
                //    body = body + "</tr>";
                //    body = body + "<tr>";
                //    body = body + "<td> User Password : " + dt.Rows[0]["Password"].ToString() + " ";
                //    body = body + "</td>";
                //    body = body + "</tr>";
                //    body = body + "<tr>";
                //    body = body + "<td>Thanks & Regards,";
                //    body = body + "</td>";
                //    body = body + "</tr>";
                //    body = body + "<tr>";
                //    body = body + "<td>theMagz.net";
                //    body = body + "</td>";
                //    body = body + "</tr>";
                //    body = body + "</table>";


                //    body = body + " </font>"
                //                    + "        </td> "
                //                    + "    </tr> "
                //        //+ "    <tr> "
                //        //+ "        <td align=\"center\"> "
                //        //+ "            <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#9b98d2\">{%[CopyRight]%}</font> "
                //        //+ "        </td> "
                //        //+ "    </tr> "
                //                    + "</table> ";
                //     string body =
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
                //+ "         <td  style=\"background-color:#4E93B4;Font-size:20px;\"> "
                //         //+ "             <img src=\"http://themagz.net/client/images/logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" /> style=\"background-color: #008000\"  rgb(10, 109, 10);"
                //+ "<b>LEA eBooks</b>"
                //+ "         </td> "
                //+ "     </tr> "
                //+ "     <tr> "
                //+ "         <td valign=\"top\"> "
                //+ "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
                //+ "                 <br /> "
                //+ "                 <span lang=\"EN-IN\"></span><p class=\"MsoNormal\"> "
                //+ "                     <span lang=\"EN-IN\">Your Credentials for LEA eBooks are as below."
                //+ "                         <br /> "
                //+ "                         <br /> "
                //+ "                         <b>Email</b>: " + dt.Rows[0]["EmailAddress"].ToString() + "<br /> "
                //+ "                         <b>User Name</b>: " + dt.Rows[0]["UserName"].ToString() + "<br /> "
                //+ "                         <b>Password</b>: " + dt.Rows[0]["Password"].ToString() + "</span></p> "
                //+ "                 <p> "
                //+ "                     To get more information please contact our representative.</p> "
                //+ "                 <p class=\"MsoNormal\"> "
                //+ "                     <span lang=\"EN-IN\">Regards,<br /> "
                //+ "                         LEA eBooks</span></p> "
                //+ "                 <br /> "
                //+ "                 <br /> "
                //+ "             </font> "
                //+ "         </td> "
                //+ "     </tr> "
                //+ " </table> ";

                string body = string.Empty;
                string file = Server.MapPath("~\\EmailTemplates\\ForgotPassword.htm");
                using (StreamReader reader = new StreamReader(file))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{EmailAddress}", dt.Rows[0]["EmailAddress"].ToString());
                body = body.Replace("{Password}", dt.Rows[0]["Password"].ToString());
                body = body.Replace("{ContactEmail}", ContactEmail);

                int val = 0;
                System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);





                try
                {
                    Global.SendEmail(EmailAddress, "Forgot Password", strBody.ToString());
                    Success6("0", "Password has been sent successfully to your email address"); //Success("Password has been sent successfully to your email address");
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                Success6("-1", "Email Address not found.");
            }
        }
        else
        {
            Success6("-1", "Email Address not in valid format.");
        }

        // Context.Response.Output.Write(retJSON);
    }

    [WebMethod(Description = "This webservice for forgot password")]
    public void contactus(string Username, string Email, string subject, string Description)
    {
        string ContactEmail = "";
        WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
        DataTable DT = WSB.GetAllWebseetings();
        if (DT != null && DT.Rows.Count > 0)
        {
            ContactEmail = DT.Rows[0]["ContactUs"].ToString();
        }
        //objUser.EmailAddress = Email;
        //DataTable dt = objUser.GetOneByEmail();
        //if (dt.Rows.Count > 0 && dt != null)
        //{
        //     string body =
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
        //+ "         <td  style=\"background-color:#4E93B4;Font-size:20px;\"> "
        //         //+ "             <img src=\"http://themagz.net/client/images/logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" /> style=\"background-color: #008000\"  rgb(10, 109, 10);"
        //+ "<b>LEA eBooks</b>"
        //+ "         </td> "
        //+ "     </tr> "
        //+ "     <tr> "
        //+ "         <td valign=\"top\"> "
        //+ "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
        //+ "                 <br /> "
        //+ "                 <span lang=\"EN-IN\"></span><p class=\"MsoNormal\"> "


        //+ "                         <b>Email</b>: " + Email + "<br /> "
        //+ "                         <b>User Name</b>: " + Username + "<br /> "
        //         // + "                         <b>Password</b>: " + dt.Rows[0]["Password"].ToString() + "</span></p> "
        // + "                         <b>Subject</b>: " + Subject + "</span></p> "
        //+ "                         <b>Description</b>: " + Description + "</span></p> "
        //+ "                 <p> "
        //+ "                     To get more information please contact our representative.</p> "
        //+ "                 <p class=\"MsoNormal\"> "
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
        body = body.Replace("{Name}", Username);
        body = body.Replace("{ContactEmail}", ContactEmail);
        body = body.Replace("{Phone}", "NA");
        body = body.Replace("{msg}", Description);

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
            Global.SendEmail(EmailTo, "Contact - US", strBody.ToString());
            Success6("0", "Your message has been sent successfully to your email address"); //Success("Your message has been sent successfully to your email address");
        }
        catch (Exception)
        {

            throw;
        }

        //}
        //else
        //{
        //     Message1("Email Address not found.");
        //}

        // Context.Response.Output.Write(retJSON);
    }

    [WebMethod(Description = "")]
    public System.Xml.XmlElement ReaderLogin(string EmailAddress, string Password)
    {
        Security S = new Security();
        string str;
        objUser.FacebookEmail = "";
        objUser.EmailAddress = EmailAddress;
        objUser.Password = Password;
        DataTable dt = objUser.Login();
        System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();
        if (dt.Rows.Count > 0)
        {
            str = "<?xml version='1.0' encoding='UTF-8' ?>";
            str += "<BookApp>";
            str += "<User>" +
                        "<FirstName>" + getUnicodeString(dt.Rows[0]["FirstName"].ToString()) + "</FirstName>" +
                        "<LastName>" + getUnicodeString(dt.Rows[0]["LastName"].ToString()) + "</LastName>" +
                        "<Email>" + getUnicodeString(EmailAddress) + "</Email>" +
                        "<RegistrationID>" + dt.Rows[0]["RegistrationID"].ToString() + "</RegistrationID>" +
                        "<Status>1</Status>" +
                        "</User>";
            str += "</BookApp>";


        }
        else
        {
            str = "<?xml version='1.0' encoding='UTF-8' ?>";
            str += "<BookApp>";
            str += "<User>" +
                   "<Message>Please enter valid username or password.</Message>" +
                   "<Status>0</Status>" +
                   "</User>";

            str += "</BookApp>";

        }
        xdd.LoadXml(str);
        return xdd.DocumentElement;
    }

    [WebMethod(Description = "Read Book Issue")]
    public System.Xml.XmlElement ReadIssue(string UserID, string Title, string LanguageID)
    {
        Security S = new Security();
        //UserID = S.Decrypt(UserID.ToString());
        string l = LanguageID.ToLower();
        if (l != null && l == "en-us")
        {
            l = "1";
        }
        else if (l != null && l == "es-es")
        {
            l = "2";
        }
        string str;
        System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();
        if (Convert.ToInt32(UserID) != 0)
        {
            objPurchase.UserID = Convert.ToInt32(UserID);
            objPurchase.Title = Title;
            objPurchase.LanguageID = Convert.ToInt32(l);
            DataTable dt = objPurchase.GetUserBookList();
            if (dt.Rows.Count > 0)
            {
                str = "<?xml version='1.0' encoding='UTF-8' ?>";
                str += "<BookApp>";
                str += "<librarys>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var bookFileepub = Server.MapPath("/Book/" + dt.Rows[i]["CategoryID"] + "/" + dt.Rows[i]["BookID"] + ".epub");
                    if (File.Exists(bookFileepub))
                    {
                        continue;
                    }
                    if (Directory.Exists(Server.MapPath("Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["BookID"].ToString())))
                    {
                        string[] swfFiles = Directory.GetFiles(Server.MapPath("Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["BookID"].ToString()), "*.swf");
                        string path = Config.WebSite + "Book/" + dt.Rows[i]["CategoryID"].ToString() + "/" + dt.Rows[i]["BookID"].ToString() + "/";

                        str += "<library id=\"" + Convert.ToInt32(dt.Rows[i]["libraryid"].ToString()) + "\" shareurl=\"" + Config.WebSiteMain + "Book-Detail.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dt.Rows[i]["BookID"]))) + "&amp;title=" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[i]["Title"])) + "\" title_eng=\"" + dt.Rows[i]["Title"].ToString().Replace("'", "").Replace("&", "").Replace("_", " ") + "\"  title_mly=\"" + General.General.Translate(dt.Rows[i]["Title"].ToString().Replace("'", "").Replace("&", "").Replace("_", " "), "English", "Malay") + "\"  publicationDate_eng=\"" + dt.Rows[i]["publicationDate_eng"].ToString()
                            + "\"  publicationDate_mly=\"" + General.General.Translate(dt.Rows[i]["publicationDate_mly"].ToString(), "English", "Malay") + "\" thumb=\"" + dt.Rows[i]["thumb"].ToString() + "\" images_width=\"" + dt.Rows[i]["images_width"].ToString() + "\" images_height=\"" + dt.Rows[i]["images_height"].ToString() + "\"  >";

                        DataTable dttable = objPurchase.ReadIssue(Convert.ToInt32(dt.Rows[i]["BookID"].ToString()));
                        try
                        {
                            for (int j = 0; j < swfFiles.Count(); j++)
                            {
                                //str += "<page id=\"" + (j + 1) + "\" sequential_page=\"" + j + "\" name_eng='" + (j + 1) + "'  name_mly='" + (j + 1) + "' url=\"" + path + (j + 1).ToString() + ".swf" + "\" thumb=\"" + dttable.Rows[j]["thumb"].ToString() + "\" />";
                                //New Code Nirav
                                str += "<page id=\"" + (j + 1) + "\" sequential_page=\"" + j + "\" name_eng='" + (j + 1) + "'  name_mly='" + (j + 1) + "' url=\"" + path + (j + 1).ToString() + ".swf" + "\" />";
                                //New Code End
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        str += "</library>";
                    }
                }
                str += "</librarys>" +
               "</BookApp>";
            }
            else
            {
                str = "<?xml version='1.0' encoding='UTF-8' ?>";
                str += "<BookApp>";
                str += "<User>" +
                       "<Message>No record(s) found.</Message>" +
                       "<Status>0</Status>" +
                       "</User>";
                str += "</BookApp>";
            }
            xdd.LoadXml(str);
            return xdd.DocumentElement;
        }
        else
        {
            str = "<?xml version='1.0' encoding='UTF-8' ?>";
            str += "<BookApp>";
            str += "<User>" +
                   "<Message>Please enter valid userID.</Message>" +
                   "<Status>0</Status>" +
                   "</User>";
            str += "</BookApp>";
            xdd.LoadXml(str);
            return xdd.DocumentElement;
        }
    }

    [WebMethod(Description = "Help XML Format")]
    public System.Xml.XmlElement GetHelp(string LanguageID)
    {
        string str;
        System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();
        string l = LanguageID.ToLower();
        if (l != null && l == "en-us")
        {
            l = "1";
        }
        else if (l != null && l == "es-es")
        {
            l = "2";
        }
        CmsBAL cms = new CmsBAL();
        cms.ID = 26;
        cms.LanguageID = Convert.ToInt32(l);
        //cms.LanguageID = 1;
        DataTable dt = cms.SelectcmsByID();

        str = "<?xml version='1.0' encoding='UTF-8' ?>";
        str += "";
        str += "<Help>"
                + dt.Rows[0]["Description"] +
               "</Help>";
        str += "";
        xdd.LoadXml(str);
        return xdd.DocumentElement;
    }

    [WebMethod(Description = "This webservice for Move cast to my library")]
    public void MoveCartListToMyLibrary(string EmailAddress, int CustomerID, int languageID, string ShippingType, string PaymentFrom)
    {
        if (ShippingType == "1") // For DHL shipping
        {
            ShippingType = "DHL";
        }
        else if (ShippingType == "2") // For National
        {
            ShippingType = "National";
        }
        else
        {
            ShippingType = "";
        }
        BookOrderBAL ObjBookOrders = new BookOrderBAL();
        ObjBookOrders.CustomerID = Convert.ToInt32(CustomerID);
        ObjBookOrders.LanguageID = Convert.ToInt32(languageID);

        DataTable dt = ObjBookOrders.GetCartList();

        string OrderNo = "0";
        if (dt != null && dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["OrderNo"] != null && dt.Rows[0]["OrderNo"].ToString() != "")
            {
                try
                {
                    OrderNo = dt.Rows[0]["OrderNo"].ToString();
                }
                catch
                {
                    OrderNo = "0";
                }
            }
        }

        ObjBookOrders.LanguageID = Convert.ToInt32(languageID);
        ObjBookOrders.OrderNo = OrderNo;
        DataTable WishDT = ObjBookOrders.GetCartListByOrderNo();
        if (PaymentFrom == "1")
        {
            PaymentFrom = "App";
        }
        else
        {
            PaymentFrom = "";
        }
        int result = 0;
        if (WishDT != null && WishDT.Rows.Count > 0)
        {
            for (int i = 0; i < WishDT.Rows.Count; i++)
            {
                objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);
                objPurchase.PurchaseDate = Convert.ToDateTime(System.DateTime.Now);
                objPurchase.TransID = "0";
                result = objPurchase.MoveToBookPurchase("0", "0.00", ShippingType, PaymentFrom);
            }
            SendCustomerOrderEmail(WishDT, "0", OrderNo, EmailAddress);
        }

        Success6("0", "Book Moved from cart to library");
    }

    public void SendCustomerOrderEmail(DataTable ebooksDT, string transactionID, string OrderID, string EmailAddress)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();
            }
        }
        catch { }

        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"http://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b><br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No.</b> " + OrderID + "</h3><br>");
        sb.Append("<table width='493' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            price += Convert.ToDouble(dr["FinalPrice1"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["FinalPrice1"].ToString() + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='493' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");

        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");

        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (EmailAddress != null && EmailAddress != "")
        {
            Global.SendEmail(EmailAddress.ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Email, "Lea | Your Order information", sb.ToString());
        }
    }

    //[WebMethod(Description = "Insert/Delete Item in Customer Cart")]
    //public void ManageUserCart(int UserID, string SessionID, string OrderDate, long OrderID, int BookID, string Amount, int IsToDelete)
    //{
    //    string str;
    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

    //    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();

    //    ObjBookOrderBal.SessionID = SessionID;
    //    ObjBookOrderBal.OrderDate = OrderDate == "" ? DateTime.Now : Convert.ToDateTime(OrderDate);
    //    ObjBookOrderBal.CustomerID = UserID;
    //    ObjBookOrderBal.SubscribedBookID = BookID;
    //    ObjBookOrderBal.BookID = BookID;
    //    ObjBookOrderBal.Amount = Amount;
    //    ObjBookOrderBal.OrderID = OrderID;

    //    if (ObjBookOrderBal.CheckDuplicateItemInCart().Rows[0][0].ToString() == "1")
    //    {


    //        retJSON = Message1("Item already exists.");
    //        Context.Response.Output.Write(retJSON);
    //    }
    //    else
    //    {

    //        int orderid = ObjBookOrderBal.ManageCustomerCart(IsToDelete);

    //        retJSON = Message1(orderid.ToString());
    //        Context.Response.Output.Write(retJSON);
    //    }


    //}

    //[WebMethod(Description = "Get Customer Cart")]
    //public void GetCustomerCart(int UserID, string SessionID)
    //{
    //    string str;
    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

    //    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();


    //    ObjBookOrderBal.CustomerID = UserID;
    //    ObjBookOrderBal.SessionID = SessionID;
    //    DataTable DT = ObjBookOrderBal.ws_SelectCustomerCart();


    //    if (DT != null && DT.Rows.Count > 0)
    //    {
    //        string aspJson = "";
    //        aspJson += "[{ \"BookApp\": {\"Cart\": [";
    //        for (int i = 0; i <= DT.Rows.Count - 1; i++)
    //        {
    //            aspJson += "{\"BookID\": \"" + DT.Rows[i]["ID"].ToString() + "\"," +
    //                       "\"Title\": \"" + DT.Rows[i]["Title"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"OrderID\": \"" + DT.Rows[i]["orderid"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"Issues\": \"" + DT.Rows[i]["Issues"].ToString().Replace("/", "\\/") + "\"," +
    //                       "\"TitleImage\": \"" + DT.Rows[i]["TitleImage"].ToString() + "\"," +

    //                       "\"Price\": \"" + Math.Round(Convert.ToDecimal(DT.Rows[i]["Price"].ToString())) + "\"},";
    //        }
    //        aspJson += "]}}]";
    //        Context.Response.Output.Write(aspJson.Remove(aspJson.Length - 5, 1));
    //    }
    //    else
    //    {
    //        retJSON = Message("No Item found.");
    //    }

    //    Context.Response.Output.Write(retJSON);



    //}

    //[WebMethod(Description = "Get Last Order")]
    //public void GetLastPayment(string UserID)
    //{
    //    string str;
    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

    //    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    //    ObjBookOrderBal.CustomerID = Convert.ToInt64(UserID);
    //    DataSet ds = ObjBookOrderBal.GetLastOrder();

    //    DataTable DT = ds.Tables[0];
    //    if (DT != null && DT.Rows.Count > 0)
    //    {
    //        string aspJson = "";
    //        aspJson += "[{ \"BookApp\": {\"Order\": [";

    //        aspJson += "{\"TransactionID\": \"" + DT.Rows[0]["TransactionID"].ToString() + "\"," +
    //                   "\"AppCode\": \"" + DT.Rows[0]["AppCode"].ToString().Replace("/", "\\/") + "\"," +
    //                   "\"OrderID\": \"" + DT.Rows[0]["orderno"].ToString().Replace("/", "\\/") + "\"," +
    //                   "\"Status\": \"" + DT.Rows[0]["Status"].ToString().Replace("/", "\\/") + "\"," +


    //                   "\"PurchaseDate\": \"" + DT.Rows[0]["PurchaseDate"].ToString() + "\"},";

    //        aspJson += "]}}]";
    //        Context.Response.Output.Write(aspJson.Remove(aspJson.Length - 5, 1));
    //    }
    //    else
    //    {
    //        retJSON = Message("No Item found.");
    //    }

    //    Context.Response.Output.Write(retJSON);
    //}

    //[WebMethod(Description = "Customer Checkout")]
    //public void CustomerCheckOut(int UserID, string TotalPrice, string BookID)
    //{
    //    string str;
    //    DataTable dtereg = new DataTable();
    //    System.Xml.XmlDataDocument xdd = new System.Xml.XmlDataDocument();

    //    BookPurchaseBAL ObjBookPurchase = new BookPurchaseBAL();
    //    RegistrationBAL ObjRegistrationBal = new RegistrationBAL();

    //    ObjRegistrationBal.RegistrationID = Convert.ToInt64(UserID);
    //    dtereg = ObjRegistrationBal.SelectRegistraionByID();
    //    string Email = "";
    //    if (dtereg.Rows.Count > 0)
    //    {
    //        Email = dtereg.Rows[0]["EmailAddress"].ToString();
    //    }
    //    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();


    //    ObjBookOrderBal.OrderDate = DateTime.Now;
    //    ObjBookOrderBal.CustomerID = UserID;
    //    ObjBookOrderBal.SubscribedBookID = Convert.ToInt64(BookID);
    //    ObjBookOrderBal.BookID = Convert.ToInt64(BookID);
    //    ObjBookOrderBal.Amount = TotalPrice;


    //    if (ObjBookOrderBal.CheckDuplicateItemInPurchased().Rows[0][0].ToString() == "1")
    //    {


    //        str = "[{\"Url\":\"1\"}]";

    //        Context.Response.Output.Write(str);
    //    }
    //    else
    //    {
    //        //int orderid = ObjBookOrderBal.ManageCustomerCart(0);

    //        //long orderno = Global.RandomNumber(111111111, 999999999);
    //        //ObjBookPurchase.OrderID = orderno;
    //        //ObjBookPurchase.PurchaseDate = DateTime.Now;
    //        //ObjBookPurchase.UserID = UserID;
    //        //ObjBookPurchase.Domain = "test5584";
    //        //ObjBookPurchase.Vcode = "";
    //        //int pid = ObjBookPurchase.InsertBookPurchase();
    //        //orderno = Convert.ToInt64(orderno.ToString() + pid.ToString());
    //        //string url = "https://www.onlinepayment.com.my/NBepay/pay/themagz/?amount=" + TotalPrice + "&orderid=" + orderno.ToString() + "&returnurl=" + "http://www.themagz.net/client/paymentMobile.aspx?userid=" + UserID + "&email=" + Email + "&vcode=" + General.General.GetMD5(TotalPrice + "test5584" + orderno) + "&bill_email=" + Email;



    //        int orderid = ObjBookOrderBal.ManageCustomerCart(0);
    //        long orderno = Global.RandomNumber(111111111, 999999999);
    //        ObjBookPurchase.OrderID = orderno;
    //        ObjBookPurchase.PurchaseDate = DateTime.Now;
    //        ObjBookPurchase.UserID = UserID;
    //        ObjBookPurchase.Domain = "test5584";
    //        ObjBookPurchase.Vcode = "";
    //        int pid = ObjBookPurchase.InsertBookPurchase();
    //        orderno = Convert.ToInt64(orderno.ToString() + pid.ToString());
    //        string vcode = Security.md5Hash(TotalPrice + "themagz" + orderno + "7d5d89abbec6fc2c13411b0ed01565bb");
    //        string url = "https://www.onlinepayment.com.my/NBepay/pay/test5584/?amount=" + TotalPrice + "&orderid=" + orderno.ToString() + "&returnurl=" + Config.WebSiteMain + "client/paymentMobile.aspx?userid=" + UserID + "" + "&vcode=" + vcode + "&bill_email=" + Email;

    //        str = "[{\"Url\":\"" + url + "\"}]";

    //        Context.Response.Output.Write(str);

    //    }

    //}

    [WebMethod(Description = "To Send Email")]
    public void SendEmail(string EmailAddress, string subject, string body)
    {
        try
        {
            Global.SendEmail(EmailAddress, subject, body);
            retJSON = Success("Email is sent successfully");
        }
        catch (Exception)
        {

            retJSON = Message("Email Address not in valid format.");
        }



        Context.Response.Output.Write(retJSON);
    }

    public void InsertIphonePushNotification(string IphoneID, int UserID, int Department)
    {
        objBook.insertIphonePushNotification(IphoneID, UserID, Department);

        if (objBook.get_IphonePushNotification(IphoneID, UserID, Department).Rows.Count > 0)
        {
            // Context.Response.Output.Write(Message("Message already exists!"));
        }
        else
        {
            int i = objBook.insertIphonePushNotification(IphoneID, UserID, Department);
            // Context.Response.Output.Write(Message1(i.ToString()));
        }
    }

    public static string RemoveHTML(string strHTML)
    {
        string str = Regex.Replace(strHTML, "<(.|\n)*?>", "");
        return Regex.Replace(str, @"\s+", " ");
    }

    public string Message1(string MSG)
    {
        string str = null;
        str = "[{\"Message\":\"" + MSG + "\"}]";
        Context.Response.Output.Write(str);
        return str;
    }

    public string Message(string MSG)
    {
        string str = null;
        str = "[{\"code\":\"0\"}]";
        return str;
    }

    public string Success(string MSG)
    {
        string str = null;
        str = "[{\"code\":\"1\",\"message\": \"" + MSG + "\"}]";
        return str;
    }

    public string Success7(string Code, int Count, string MSG)
    {
        string str = null;
        str = "[{\"Code\":" + Code + ",\"Count\":" + Count + ",\"message\": \"" + MSG + "\"}]";
        Context.Response.Output.Write(str);
        return str;
    }

    public string Success6(string Code, string MSG)
    {
        string str = null;
        str = "[{\"Code\":" + Code + ",\"message\": \"" + MSG + "\"}]";
        Context.Response.Output.Write(str);
        return str;
    }

    public string Success25(string Code, string MSG, int Count)
    {
        string str = null;
        str = "[{\"Code\":" + Code + ",\"message\": \"" + MSG + "\" ,\"TotalCount\":" + Count + "}]";
        Context.Response.Output.Write(str);
        return str;
    }

    public string Success1(string MSG, int RegID)
    {
        string str = null;
        str = "[{\"code\":\"0\",\"message\": \"" + MSG + "\" ,\"UserID\":\"" + RegID + "\"}]";
        return str;
    }

    public string Success2(string Code, string Message, string Content)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\",\"Result\":" + RemoveHTML(Content) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success10(string Code, string Message, int Count, string Content)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\", \"CartCount\":\"" + Count + "\",\"Result\":" + RemoveHTML(Content) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success15(string Code, string Message, int Count, string Content)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\", \"Count\":\"" + Count + "\",\"Result\":" + RemoveHTML(Content) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success10(string Code, string Message, string CartCount, string Content)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\"\"CartCount\":\"" + RemoveHTML(CartCount) + "\",\"Result\":" + RemoveHTML(Content) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success4(string Code, string Message, string Content)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\",\"Result\": [{\"Categories\":" + RemoveHTML(Content) + "}]}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success3(string Code, string Message, string Content, string Content1, string Content2, string Content3)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\",\"Result\":" + RemoveHTML(Content) + ", \"Categories\":" + RemoveHTML(Content1) + ", \"Topsellers\":" + RemoveHTML(Content2) + ", \"OfferZone\":" + RemoveHTML(Content3) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success22(string Code, string Message, string Count, string Content, string Content1, string Content2, string Content3)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\",\"CartCount\":\"" + RemoveHTML(Count) + "\",\"Result\":" + RemoveHTML(Content) + ", \"Categories\":" + RemoveHTML(Content1) + ", \"Topsellers\":" + RemoveHTML(Content2) + ", \"OfferZone\":" + RemoveHTML(Content3) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success30(string Code, string Message, string Count, string Total, string Content, string Content1)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\",\"Count\":\"" + RemoveHTML(Count) + "\",\"Total\":\"" + Total + "\",\"Result\":" + RemoveHTML(Content1) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Success5(string Code, string Message, string Content, string Content1)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\" ,\"Total currency\": \"" + Content + "\",\"Result\":" + RemoveHTML(Content1) + "}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string SuccessCart(string Code, string Message, string Currency, string NoAddress, string ShippingDetail, string NationalShippingDetails, string defaultAddressId, string Result, string Count, string Result1)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\" ,\"Total currency\": \""
                  + Currency + "\" ,\"Total Address\": \""
                  + NoAddress + "\",\"ShippingDetail\":\""
                  + ShippingDetail + "\",\"NationalShippingDetails\":\""
                  + NationalShippingDetails + "\",\"defaultAddressId\":\""
                  + defaultAddressId +
                   "\",\"CartCount\":\"" + Count
                  + "\",\"Result\":" + RemoveHTML(Result) +
                  ",\"Address\":" + RemoveHTML(Result1) +
                  "}]";
        // + RemoveHTML(Result) + "\",\"Result1\":" + RemoveHTML(Result1) + "}]";

        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    //public string SuccessCart(string Code, string Message, string Currency, string NoAddress, string ShippingDetail, string NationalShippingDetails, string defaultAddressId, string Result, string Result1)
    //{
    //    string retJSON = "";
    //    retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\" ," +
    //        "\"Total currency\": \""+ Currency + "\" ," +
    //        "\"Total Address\": \""+ NoAddress + "\"," +
    //        "\"ShippingDetail\":\""+ ShippingDetail + "\"," +
    //        "\"NationalShippingDetails\":\""+ NationalShippingDetails + "\"," +
    //        "\"defaultAddressId\":\""+ defaultAddressId + "\"," +
    //        "\"Result\":" + RemoveHTML(Result) + "\"," +
    //        "\"Result1\":" + RemoveHTML(Result1) + "}]";
    //    Context.Response.Output.Write(retJSON);
    //    return retJSON;
    //}

    public string Fail(string Code, string Message)
    {
        string retJSON = "";
        retJSON = "[{\"Code\":" + Code + ",\"Message\":\"" + RemoveHTML(Message) + "\"}]";
        Context.Response.Output.Write(retJSON);
        return retJSON;
    }

    public string Fail1(string MSG, int RegID)
    {
        string str = null;
        str = "[{\"code\":\"0\",\"message\": \"" + MSG + "\" ,\"RegistrationID\":\"" + RegID + "\"}]";
        return str;
    }

    public string Fail2(string MSG, int RegID)
    {
        string str = null;
        str = "[{\"code\":\"2\",\"message\": \"" + MSG + "\" ,\"RegistrationID\":\"" + RegID + "\"}]";
        return str;
    }

    #region "JasonFunctions"

    public static string JSON_DataTable(DataTable Dt)
    {
        string[] StrDc = new string[Dt.Columns.Count];
        string HeadStr = string.Empty;

        for (int i = 0; i <= Dt.Columns.Count - 1; i++)
        {
            StrDc[i] = Dt.Columns[i].Caption;
            HeadStr += "\"" + StrDc[i] + "\" : \"" + StrDc[i] + i.ToString() + "¾" + "\",";
        }

        HeadStr = HeadStr.Substring(0, HeadStr.Length - 1);

        StringBuilder Sb = new StringBuilder();
        // Sb.Append("{\"SonicStudio\" : [");
        for (int i = 0; i <= Dt.Rows.Count - 1; i++)
        {
            string TempStr = HeadStr;
            Sb.Append("{");

            for (int j = 0; j <= Dt.Columns.Count - 1; j++)
            {
                TempStr = TempStr.Replace(Dt.Columns[j].ToString() + j.ToString() + "¾", HttpUtility.HtmlDecode(Dt.Rows[i][j].ToString().Replace("&quot;", "&#039;")));
            }
            Sb.Append(TempStr + "},");
        }

        Sb = new StringBuilder(Sb.ToString().Substring(0, Sb.ToString().Length - 1));
        // Sb.Append("]}");
        return "[" + Sb.ToString() + "]";

    }

    private static string XmlToJSON(XmlDocument xmlDoc)
    {
        StringBuilder sbJSON = new StringBuilder();
        sbJSON.Append("{ ");
        XmlToJSONnode(sbJSON, xmlDoc.DocumentElement, true);
        sbJSON.Append("}");
        return sbJSON.ToString();
    }

    private static void XmlToJSONnode(StringBuilder sbJSON, XmlElement node, bool showNodeName)
    {
        if (showNodeName)
            sbJSON.Append("\"" + SafeJSON(node.Name) + "\": ");
        sbJSON.Append("{");
        // Build a sorted list of key-value pairs
        //  where   key is case-sensitive nodeName
        //          value is an ArrayList of string or XmlElement
        //  so that we know whether the nodeName is an array or not.
        SortedList childNodeNames = new SortedList();

        //  Add in all node attributes
        if (node.Attributes != null)
            foreach (XmlAttribute attr in node.Attributes)
                StoreChildNode(childNodeNames, attr.Name, attr.InnerText);

        //  Add in all nodes
        foreach (XmlNode cnode in node.ChildNodes)
        {
            if (cnode is XmlText)
                StoreChildNode(childNodeNames, "value", cnode.InnerText);
            else if (cnode is XmlElement)
                StoreChildNode(childNodeNames, cnode.Name, cnode);
        }

        // Now output all stored info
        foreach (string childname in childNodeNames.Keys)
        {
            ArrayList alChild = (ArrayList)childNodeNames[childname];
            if (alChild.Count == 1)
                OutputNode(childname, alChild[0], sbJSON, true);
            else
            {
                sbJSON.Append(" \"" + SafeJSON(childname) + "\": [ ");
                foreach (object Child in alChild)
                    OutputNode(childname, Child, sbJSON, false);
                sbJSON.Remove(sbJSON.Length - 2, 2);
                sbJSON.Append(" ], ");
            }
        }
        sbJSON.Remove(sbJSON.Length - 2, 2);
        sbJSON.Append(" }");
    }

    private static void StoreChildNode(SortedList childNodeNames, string nodeName, object nodeValue)
    {
        // Pre-process contraction of XmlElement-s
        if (nodeValue is XmlElement)
        {
            // Convert  <aa></aa> into "aa":null
            //          <aa>xx</aa> into "aa":"xx"
            XmlNode cnode = (XmlNode)nodeValue;
            if (cnode.Attributes.Count == 0)
            {
                XmlNodeList children = cnode.ChildNodes;
                if (children.Count == 0)
                    nodeValue = null;
                else if (children.Count == 1 && (children[0] is XmlText))
                    nodeValue = ((XmlText)(children[0])).InnerText;
            }
        }
        // Add nodeValue to ArrayList associated with each nodeName
        // If nodeName doesn't exist then add it
        object oValuesAL = childNodeNames[nodeName];
        ArrayList ValuesAL;
        if (oValuesAL == null)
        {
            ValuesAL = new ArrayList();
            childNodeNames[nodeName] = ValuesAL;
        }
        else
            ValuesAL = (ArrayList)oValuesAL;
        ValuesAL.Add(nodeValue);
    }

    private static void OutputNode(string childname, object alChild, StringBuilder sbJSON, bool showNodeName)
    {
        if (alChild == null)
        {
            if (showNodeName)
                sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
            sbJSON.Append("null");
        }
        else if (alChild is string)
        {
            if (showNodeName)
                sbJSON.Append("\"" + SafeJSON(childname) + "\": ");
            string sChild = (string)alChild;
            sChild = sChild.Trim();
            sbJSON.Append("\"" + SafeJSON(sChild) + "\"");
        }
        else
            XmlToJSONnode(sbJSON, (XmlElement)alChild, showNodeName);
        sbJSON.Append(", ");
    }

    private static string SafeJSON(string sIn)
    {
        StringBuilder sbOut = new StringBuilder(sIn.Length);
        foreach (char ch in sIn)
        {
            if (Char.IsControl(ch) || ch == '\'')
            {
                int ich = (int)ch;
                sbOut.Append(@"\u" + ich.ToString("x4"));
                continue;
            }
            else if (ch == '\"' || ch == '\\' || ch == '/')
            {
                sbOut.Append('\\');
            }
            sbOut.Append(ch);
        }
        return sbOut.ToString();
    }
    #endregion

    public string Test(string Path)
    {
        if (!string.IsNullOrEmpty(Path))
        {
            Bitmap bitmap = new Bitmap((Server.MapPath("~/Book/4/Original10_1.jpg")));
            string base64;
            ImageFormat format = ImageFormat.Png;
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, format);
                base64 = Convert.ToBase64String(ms.ToArray());
            }
            return base64;
        }
        else
        {
            return "";
        }
    }

    #region Added on 22nd oct 2018 

    public class Region
    {
        public string ID, Name;
    }
    public class ClientBudget
    {
        public string ID, Name;
    }
    public class DisplayData
    {
        public List<Region> RegionList;
    }
    public class Data
    {
        public List<CountryList> CountryLt;
    }

    public class Result
    {
        public List<CityList> CityLst;
    }
    public class CityList
    {
        public string CityID, City, CountryID;
    }

    public class CountryList
    {
        public string CountryID, Name, RegionID;
    }
    [WebMethod(Description = "This webservice for RegionDetails")]
    public void RegionDetails()
    {
        dbconnection db = new dbconnection();
        DataTable dt = db.filltable("select *  from [dbo].[Region] Order by Region Asc ");
        var resultObj = new DisplayData();
        //Budget Details
        if (dt.Rows.Count > 0)
        {
            List<Region> objBudget = new List<Region>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                Region objbud = new Region();
                objbud.ID = dt.Rows[i]["Id"].ToString().Trim();
                objbud.Name = dt.Rows[i]["Region"].ToString().Trim();

                objBudget.Add(objbud);
            }
            resultObj.RegionList = objBudget;
        }
        else
            resultObj.RegionList = new List<Region>();

        if (resultObj != null)
        {
            var json = new JavaScriptSerializer().Serialize(resultObj);
            JasonRecordFound("listed!", dt.Rows.Count, json);
        }
        else
        {
            JasonInvalidToken("NO Record Found");
        }
    }

    [WebMethod(Description = "This webservice for CountryDetails")]
    public void CountryDetails(string Region)
    {
        dbconnection db = new dbconnection();
        DataTable dt = db.filltable("select * from tblcountry C inner join Region_Country R  on  R.Country_Id= C.countryid where R.RId= " + Region + " Order by countryname ASC");
        var Data = new Data();

        if (dt.Rows.Count > 0)
        {
            List<CountryList> objcoun = new List<CountryList>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                CountryList objcou = new CountryList();
                objcou.RegionID = dt.Rows[i]["RId"].ToString().Trim();
                objcou.CountryID = dt.Rows[i]["countryid"].ToString().Trim();
                objcou.Name = dt.Rows[i]["countryname"].ToString().Trim();

                objcoun.Add(objcou);
            }
            Data.CountryLt = objcoun;
        }
        else
            Data.CountryLt = new List<CountryList>();

        if (Data != null)
        {
            var json = new JavaScriptSerializer().Serialize(Data);
            JasonRecordFound("listed!", dt.Rows.Count, json);
        }
        else
        {
            JasonInvalidToken("NO Record Found");
        }
    }

    [WebMethod(Description = "This webservice for CityDetails")]
    public void CityDetails(int Country)
    {
        dbconnection db = new dbconnection();
        DataTable dt = db.filltable("select * from tbl_city where countryid =" + Country + " Order by city ASC");
        var Result = new Result();

        if (dt.Rows.Count > 0)
        {
            List<CityList> objcitylst = new List<CityList>();
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                CityList objcity = new CityList();
                objcity.CountryID = dt.Rows[i]["countryId"].ToString().Trim();
                objcity.CityID = dt.Rows[i]["Id"].ToString().Trim();
                objcity.City = dt.Rows[i]["city"].ToString().Trim();
                objcitylst.Add(objcity);
            }
            Result.CityLst = objcitylst;
        }
        else
            Result.CityLst = new List<CityList>();

        if (Result != null)
        {
            var json = new JavaScriptSerializer().Serialize(Result);
            JasonRecordFound("listed!", dt.Rows.Count, json);
        }
        else
        {
            JasonInvalidToken("NO Record Found");
        }
    }
    public void JasonRecordFound(string Alert, int TotalRecord, string Content)
    {
        string retJSON = "";
        retJSON = "[{\"code\":1,\"message\":\"" + RemoveHTML(Alert) + "\",\"totalrecord\":" + TotalRecord + ",\"result\":" + RemoveHTML(Content) + "}]";
        Context.Response.Output.Write(retJSON);
    }

    public void JasonInvalidToken(string Alert)
    {
        var JesonData = new JesonData()
        {
            code = "-3",
            message = RemoveHTML(Alert)
        };

        var json = new JavaScriptSerializer().Serialize(JesonData);
        Context.Response.Output.Write(json);
    }
    public class JesonData
    {
        public string code, message;
    }

    #endregion

}
