using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
/// <summary>
/// Summary description for Global
/// </summary>
/// 

public class Global
{
    public Global()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static void SendEmail(string To, string Subject, string Body)
    {
        try
        {
            if (EmailAddressCheck(To))
            {                
                string PORT = ConfigurationManager.AppSettings["PORT"].ToString().Trim();
                string SMTPFrom = ConfigurationManager.AppSettings["FROMEMAIL"].ToString().Trim();
                string SMTPpass = ConfigurationManager.AppSettings["FROMPWD"].ToString().Trim();
                string SMTP = ConfigurationManager.AppSettings["SMTP"].ToString().Trim();

                SmtpClient client = new SmtpClient(SMTP, Convert.ToInt32(PORT));
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(SMTPFrom, SMTPpass);
                client.EnableSsl = true;
                //MailAddress fromAddress = new MailAddress("sales@leaebook.com", "LEA eBook");
                MailAddress fromAddress = new MailAddress("noreply@leaebook.com", "LEA eBook");
                MailMessage message = new MailMessage();
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
           // throw exc;
        }
    }

    public static int SendEmail(string To, string Subject, string Body, string AttachURL)
    {
        try
        {
            if (EmailAddressCheck(To))
            {
                string PORT = ConfigurationManager.AppSettings["PORT"].ToString().Trim();
                string SMTPFrom = ConfigurationManager.AppSettings["FROMEMAIL"].ToString().Trim();
                string SMTPpass = ConfigurationManager.AppSettings["FROMPWD"].ToString().Trim();
                string SMTP = ConfigurationManager.AppSettings["SMTP"].ToString().Trim();

                SmtpClient client = new SmtpClient(SMTP, Convert.ToInt32(PORT));
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(SMTPFrom, SMTPpass);
                //client.EnableSsl = true;
                MailAddress fromAddress = new MailAddress("sales@leaebook.com", "LEA eBook");
                MailMessage message = new MailMessage();
                message.From = fromAddress;
                message.To.Add(To.ToString().Trim());

                string[] separators = { "@@" };
                string[] words = AttachURL.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (File.Exists(word))
                    {
                        Attachment A = new Attachment(word);
                        message.Attachments.Add(A);
                    }
                }

                message.Body = Body.ToString().Trim();
                message.Subject = Subject.ToString().Trim();
                message.IsBodyHtml = true;
                client.Send(message);
                message.Dispose();
            }
            return 0;
        }
        catch (Exception)
        {
            return 1;
        }
    }

    public static bool EmailAddressCheck(string emailAddress)
    {
        bool functionReturnValue = false;
        string pattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
        Match emailAddressMatch = Regex.Match(emailAddress, pattern);
        if (emailAddressMatch.Success)
        {
            functionReturnValue = true;
        }
        else
        {
            functionReturnValue = false;
        }
        return functionReturnValue;

    }

    public static void AlertNew(Control page, string Message)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
    }
    public static void Alert(Control page, string Message)
    {
        string jScriptValidator;
        jScriptValidator = "<script type=\"text/javascript\">" +
                            "$(document).ready(function() {" +
                    "$(\"#message-green\").show().fadeOut(5000);" +
                     "document.getElementById('succ').innerHTML = \"" + Message + "\" ";

        jScriptValidator += " }); </script>";
        // Page.RegisterStartupScript("regJSval", jScriptValidator);
        ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), jScriptValidator, false);
        // ScriptManager.RegisterStartupScript(page, page.GetType(), Guid.NewGuid().ToString(), "alert('" + Message + "');", true);
    }

    public static void SetPagerButtonStates(GridView gridView, GridViewRow gvPagerRow, Page page)
    {

        int pageIndex = gridView.PageIndex;
        int pageCount = gridView.PageCount;

        LinkButton btnFirst = (LinkButton)gvPagerRow.FindControl("btnFirst");
        LinkButton btnPrevious = (LinkButton)gvPagerRow.FindControl("btnPrevious");
        LinkButton btnNext = (LinkButton)gvPagerRow.FindControl("btnNext");
        LinkButton btnLast = (LinkButton)gvPagerRow.FindControl("btnLast");
        Literal lblCurrent = (Literal)gvPagerRow.FindControl("lblCurrent");
        Literal lblPages = (Literal)gvPagerRow.FindControl("lblPages");
        lblCurrent.Text = (pageIndex + 1).ToString();
        lblPages.Text = pageCount.ToString();
        btnFirst.Enabled = btnPrevious.Enabled = (pageIndex != 0);
        btnNext.Enabled = btnLast.Enabled = (pageIndex < (pageCount - 1));

        DropDownList ddlPageSelector = (DropDownList)gvPagerRow.FindControl("ddlPageSelector");
        ddlPageSelector.Items.Clear();
        for (int i = 1; i <= gridView.PageCount; i++)
        {
            ddlPageSelector.Items.Add(i.ToString());
        }

        ddlPageSelector.SelectedIndex = pageIndex;

        //Anonymous method (see another way to do this at the bottom)
        ddlPageSelector.SelectedIndexChanged += delegate
        {
            gridView.PageIndex = ddlPageSelector.SelectedIndex;
            gridView.DataBind();
        };

    }

    public static bool ConvertToBool(string value)
    {
        int val = 0;
        return (int.TryParse(value, out val) && val == 0) ? false : true;
    }
 
    public static int RegistrationID
    {
        get
        {
            int id = 0;
            if (HttpContext.Current.Session["RegistrationID"] != null || HttpContext.Current.Session["RegistrationID"] != "")
            {
                id = Convert.ToInt32(HttpContext.Current.Session["RegistrationID"]);
                return id;
            }
            return id;
        }
    }

    public static string UserEmail
    {
        get
        {
            string email = "";
            if (HttpContext.Current.Session["UserEmail"] != null || HttpContext.Current.Session["UserEmail"] != "")
            {
                email = Convert.ToString(HttpContext.Current.Session["UserEmail"]);
                return email;
            }
            return email;
        }
        set { HttpContext.Current.Session["UserEmail"] = value; }
    }

    public static string SessionID
    {
        get { return HttpContext.Current.Session["UserSessionID"] != null ? Convert.ToString(HttpContext.Current.Session["UserSessionID"]) : ""; }
        set { HttpContext.Current.Session["UserSessionID"] = value; }
    }
    public static long RandomNumber(int min, int max)
    {
        Random random = new Random();
        return Convert.ToInt64(random.Next(min, max));
    }
   
    // divyesh
    public enum LanguagePrefix
    {
        enUS = 1,
        esES = 2
    }
    // END divyesh

}

