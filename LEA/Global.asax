<%@ Application Language="C#" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Text" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="BAL" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Localization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void Application_BeginRequest(object sender, EventArgs e)

    {
        try
        {
            HttpContext incoming = HttpContext.Current;
            string oldpath = incoming.Request.Path.ToLower();

            string l = string.Empty;
            //string bookid = string.Empty;
            string title = string.Empty;
            string tabtype = string.Empty;

            Regex regex = new Regex(@"us/(.+)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches = regex.Matches(oldpath);

            if (matches.Count > 0)
            {
                if (oldpath.Split('/').Length > 3)
                {
                    tabtype = oldpath.Split('/')[3].ToString();
                }

                if (oldpath.Split('/').Length > 2)
                {
                    title = oldpath.Split('/')[2].ToString();
                }

                l = matches[0].Groups[0].ToString();
                //title = matches[0].Groups[1].ToString();
                title = HttpUtility.UrlEncode(title.ToString(), System.Text.Encoding.UTF8).Replace("+", "-").Replace(".", "_");
                if (l.Contains("/us"))
                {
                    l = "en-US";
                }
                else if (l.Contains("/es"))
                {
                    l = "es-ES";
                }
                else
                {
                    l = "en-US";
                }
                incoming.RewritePath(String.Concat("~/Book-Detail.aspx?l=", l, "&title=", title + "&tabtype=", tabtype), false);// ", &id=", bookid,                


            }

            regex = new Regex(@"es/(.+)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection matches1 = regex.Matches(oldpath);

            if (matches1.Count > 0)
            {
                if (oldpath.Split('/').Length > 3)
                {
                    tabtype = oldpath.Split('/')[3].ToString();
                }

                if (oldpath.Split('/').Length > 2)
                {
                    title = oldpath.Split('/')[2].ToString();
                }
                l = matches1[0].Groups[0].ToString();
                //bookid = matches[0].Groups[2].ToString();                
                //title = matches1[0].Groups[1].ToString();
                title = HttpUtility.UrlEncode(title.ToString(), System.Text.Encoding.UTF8).Replace("+", "-").Replace(".", "_");
                if (l.Contains("/us"))
                {
                    l = "en-US";
                }
                else if (l.Contains("/es"))
                {
                    l = "es-ES";
                }
                else
                {
                    l = "es-ES";
                }
                incoming.RewritePath(String.Concat("~/Book-Detail.aspx?l=", l, "&title=", title + "&tabtype=", tabtype), false);// "&id=", bookid,

            }
            //Edited : Anjali, 20-Jun-2014

            if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
            {
                string url = Request.ServerVariables["HTTP_HOST"] + HttpContext.Current.Request.RawUrl;
                if (url.Contains("/webservice.asmx"))
                {

                }
                else
                {
                    if (url.Contains("www."))
                    {
                        Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"]
                    + HttpContext.Current.Request.RawUrl);
                    }
                    else
                    {
                        Response.Redirect("https://www." + Request.ServerVariables["HTTP_HOST"]
                    + HttpContext.Current.Request.RawUrl);
                    }
                }
            }

            HttpContext context = ((HttpApplication)sender).Context;
            if (context.Request.CurrentExecutionFilePathExtension == ".aspx" || context.Request.CurrentExecutionFilePathExtension == ".ascx" || context.Request.CurrentExecutionFilePathExtension == ".asmx")
            {
                string lang = context.Request.QueryString["l"];
                string cultureName = Thread.CurrentThread.CurrentCulture.Name;
                if (lang != null)
                {
                    switch (lang.ToLower())
                    {
                        case "es-es":
                            cultureName = "es-ES";
                            break;
                        default:
                            cultureName = "en-US";
                            break;
                    }
                }
                else
                {
                    switch (cultureName.ToLower())
                    {
                        case "en-us":
                            cultureName = "es-ES";
                            break;
                        default:
                            cultureName = "en-US";
                            break;
                    }
                }
                LoadCulture(cultureName);
            }
            //End
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void LoadCulture(string strCurrentCulture = "")
    {
        try
        {
            string defaultCulture;

            defaultCulture = LocalizationConfiguration.GetConfig().DefaultCultureName;
            if (strCurrentCulture != "")
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(strCurrentCulture);
                }
                catch (Exception ex)
                {
                    if (!(ex is ArgumentNullException) && !(ex is ArgumentException))
                    {
                        throw;
                    }
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultCulture);
                }
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(defaultCulture);
            }
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    void Application_Start(object sender, EventArgs e)
    {
        //RegisterRoutes(RouteTable.Routes);

        // RouteConfig.RegisterRoutes(RouteTable.Routes);
        // Code that runs on application startup
        /*try
        {
            BookPurchaseBAL objBook = new BookPurchaseBAL();
            RegistrationBAL objuser = new RegistrationBAL();
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();
            string ContactEmail = "";
            WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
            DataTable DT = WSB.GetAllWebseetings();
            if (DT != null && DT.Rows.Count > 0)
            {
                ContactEmail = DT.Rows[0]["ContactUs"].ToString();
            }
            var dt = objBook.GetCartListDaily();
            Hashtable huser = new Hashtable();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (!huser.ContainsValue(dt.Rows[i]["CustomerID"]))
                {
                    huser.Add(i, dt.Rows[i]["CustomerID"]);
                }
            }
            if (huser.Count > 0)
            {
                foreach (DictionaryEntry entry in huser)
                {
                    var dtMail = objBook.MailLog_List(entry.Value.ToString());
                    if (dtMail.Rows.Count >= 0)
                    {
                        objBook.MailLog_AddEdit(entry.Value.ToString());
                        objuser.RegistrationID = Convert.ToInt64(entry.Value);
                        var dtUser = objuser.GetUserDetails();
                        Obj_bookOrder.CustomerID = Convert.ToInt64(entry.Value);
                        Obj_bookOrder.LanguageID = 1;

                        var dtBook = Obj_bookOrder.GetCartList();
                        string table = createTable(dtBook);

                        string body = string.Empty;
                        string file = Server.MapPath("~\\EmailTemplates\\Reminder.htm");
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(file))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{Email}", dtUser.Rows[0]["EmailAddress"].ToString());
                        body = body.Replace("{txt_firstname}", dtUser.Rows[0]["EmailAddress"].ToString());
                        body = body.Replace("{ContactEmail}", ContactEmail);
                        body = body.Replace("{book_table}", table);
                        System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);

                        //SendEmail(dtUser.Rows[0]["EmailAddress"].ToString(), "LEA eBooks | Reminder", strBody.ToString());
                    }
                }
            }
            
            System.Timers.Timer TimeSendBook = null;
            if (TimeSendBook == null)
            {
                TimeSendBook = new System.Timers.Timer();
                TimeSendBook.Interval = 30000;
                TimeSendBook.Enabled = true;
               // TimeSendBook.Elapsed += new System.Timers.ElapsedEventHandler(TodayPublishBook);
            }
            
            
        }
        catch (Exception ex)
        {
            throw ex;
        }*/


    }

    static void RegisterRoutes(RouteCollection routes)
    {

    }

    public string createTable(DataTable ebooksDT)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table style='width:65%'><tbody><tr style='margin-bottom: 10px;'>");
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
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Title"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + (!string.IsNullOrEmpty(ebooksDT.Rows[i]["Qauntity"].ToString()) ? ebooksDT.Rows[i]["Qauntity"].ToString() : "1") + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["Amount"].ToString() + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        return sb.ToString();
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

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(SMTP, Convert.ToInt32(PORT));
                client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(SMTPFrom, SMTPpass);
                System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress("sales@leaebook.com", "LEA eBook");
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
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

    void TodayPublishBook(object sender, System.Timers.ElapsedEventArgs e)
    {
        DataTable PublishBook = new DataTable();

        try
        {
            BAL.BookIssueBAL Bookbal = new BookIssueBAL();
            PublishBook = Bookbal.TodayPublishBookList();

            foreach (DataRow dr in PublishBook.Rows)
            {
                SendMail(dr["BookID"].ToString(), dr["Title"].ToString(), dr["BookTitle"].ToString());
            }
        }
        catch (Exception Ex)
        {

        }

    }

    private void SendMail(string BookID, string BookName, string BookIssueName)
    {

        DataTable SubscriberUsers = new DataTable();
        BAL.RegistrationBAL Reg = new RegistrationBAL();
        Reg.BookID = BookID;
        SubscriberUsers = Reg.GetSubscriberUserEmailsByBook();
        foreach (DataRow dr in SubscriberUsers.Rows)
        {
            string Strhtml = EmailBody(dr["FirstName"].ToString(), BookName, BookIssueName);
            App.SendEmail(dr["EmailAddress"].ToString(), Config.FROMEMAIL, "theMagz.net :: New Issue", Strhtml.ToString());
        }
    }

    private string EmailBody(string Name, string BookName, string BookIssueName)
    {

        //return "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\">"
        //        + "    <tbody>"
        //        + "        <tr>"
        //        + "            <td width=\"300\" valign=\"top\" style=\"width: 225.0pt; background: #f9f8f4; padding: 0in 0in 0in 0in\">"
        //        + "                <p class=\"MsoNormal\">"
        //        + "                    New Issue Lounch <u></u><u></u>"
        //        + "                </p>"
        //        + "            </td>"
        //        + "        </tr>"
        //        + "    </tbody>"
        //        + "</table>";

        string StringFormet =
                     "               <head> "
                     + "           <style type=\"text/css\"> "
                     + "               p.MsoNormal "
                     + "               { "
                     + "                   margin-top: 0in;"
                     + "                   margin-right: 0in;"
                     + "                   margin-bottom: 10.0pt; "
                     + "                   margin-left: 0in; "
                     + "                   line-height: 115%; "
                     + "                   font-size: 11.0pt; "
                     + "                   font-family: \"Calibri\" , \"sans-serif\"; "
                     + "               } "
                     + "               .style1 "
                     + "               { "
                     + "                   background-color: #7EC815; "
                     + "               } "
                     + "       h1 "
                     + "           {margin-right:0cm; "
                     + "           margin-left:0cm; "
                     + "           font-size:24.0pt; "
                     + "           font-family:\"Times New Roman\",\"serif\"; "
                     + "           } "
                     + "        div.MsoNormal "
                     + "           {margin-bottom:.0001pt; "
                     + "           font-size:12.0pt; "
                     + "           font-family:\"Times New Roman\",\"serif\"; "
                     + "                   margin-left: 0cm; "
                     + "                   margin-right: 0cm; "
                     + "                   margin-top: 0cm; "
                     + "               } "
                     + "               .style2 "
                     + "               { "
                     + "                   font-size: 11.0pt; "
                     + "                   font-family: Calibri, sans-serif; "
                     + "               } "
                     + "           </style> "
                     + "       </head> "
                     + "       <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"2\" style=\"background-color: #008000\"> "
                     + "           <tr> "
                     + "               <td style=\"background-color: rgb(122, 197, 12); color: #000000;\"> "
                     + "                   <a href=\"http://97.74.85.95:27/\"> "
                     + "                       <img src=\"\" alt=\"Mo-Focus\" title=\"Mo-Focus\" height=\"82\" "
                     + "                           style=\"height: 82px; width: 135px\" width=\"122\" /></a> "
                     + "               </td> "
                     + "           </tr> "
                     + "           <tr> "
                     + "               <td valign=\"top\" bgcolor=\"#E3AA5A\" style=\"background-color: #FFFFFF\"> "
                     + "                   <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\" "
                     + "                       style=\"background-color: #FFFFFF\"> "
                     + "                       <br /> "
                     + "                   <h1 style=\"line-height:26.25pt\"> "
                     + "                       <span style=\"font-size:19.5pt\">{BookIssueName}&nbsp;is ready to "
                     + "                       read on theMagz.net!!</span></h1> "
                     + "                   <p class=\"MsoNormal\"><br /> "
                     + "                           </p> "
                     + "                       <p> "
                     + "                           Hi {Name},</p> "
                     + "                   <p> "
                     + "                       Your new issue of {BookName}&nbsp;has arrived. You can read it instantly in  "
                     + "                       your browser, or with theMagz.net &#39;s top-rated apps for smartphone or tablet device.  "
                     + "                       Happy reading!</p> "

                     + "                   <table border=\"0\" cellpadding=\"0\" class=\"style2\" "
                     + "                       style=\"mso-cellspacing: 1.5pt; mso-yfti-tbllook: 1184; mso-padding-alt: 0cm 0cm 0cm 0cm\" "
                     + "                       width=\"350\"> "
                     + "                       <tr style=\"mso-yfti-irow:0;mso-yfti-firstrow:yes;mso-yfti-lastrow:yes; "
                     + "         height:37.5pt\"> "
                     + "                       </tr> "
                     + "                   </table> "
                     + "                   <p class=\"MsoNormal\"> "
                     + "                       <o:p>&nbsp;</o:p></p> "
                     + "                   <p class=\"MsoNormal\"> "
                     + "                       <o:p>&nbsp;</o:p></p> "
                     + "                   <div align=\"center\" class=\"MsoNormal\" style=\"text-align:center\"> "
                     + "                       <span style=\"mso-fareast-font-family:&quot;Times New Roman&quot;\"> "
                     + "                       <hr align=\"center\" noshade size=\"2\" style=\"color:#CCCCCC\" width=\"100%\" /> "
                     + "                       </span> "
                     + "                   </div> "
                     + "                   <p> "
                     + "                       This email, sent to was sent to notify you that a new issue is available. Please  "
                     + "                       do not reply to this email.</p> "
                     + "                   <span style=\"font-size:12.0pt;font-family:&quot;Times New Roman&quot;,&quot;serif&quot;;mso-fareast-font-family: "
                     + "       Calibri;mso-fareast-theme-font:minor-latin;mso-ansi-language:EN-IN;mso-fareast-language: "
                     + "       EN-IN;mso-bidi-language:AR-SA\">To subscribe to email updates about features, new products  "
                     + "                   and special offers from theMagz.net, click<a  "
                     + "                       href=\"\" "
                     + "                       target=\"_blank\"><span style=\"color:#32B1CC;text-decoration:none;text-underline: "
                     + "       none\"> here</span></a>.</span><br /> "
                     + "                   </font> "
                     + "               </td> "
                     + "           </tr> "
                     + "           <tr> "
                     + "               <td align=\"center\" bgcolor=\"Red\" style=\"background-color: #6AB506\"> "
                     + "                   <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#9b98d2\"><a href=\"http://97.74.85.95:27/\"> "
                     + "                       <span class=\"style1\">CopyRight @ theMagz.net</span></a></font> "
                     + "               </td> "
                     + "           </tr> "
                     + "       </table> ";
        StringFormet = StringFormet.Replace("{Name}", Name);
        StringFormet = StringFormet.Replace("{BookIssueName}", BookIssueName);
        StringFormet = StringFormet.Replace("{BookName}", BookName);
        return StringFormet;
    }


    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
