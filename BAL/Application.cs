using System;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;
using System.IO;

using System.Text;
using System.Net.Mail;
using General;
public class App
{
  
    public static bool SendEmail(string To, string From, string Subject, string Body)
    {
        bool SendMailStatus = false;
        try
        {
            System.Net.Mail.MailMessage objmail = new System.Net.Mail.MailMessage();
            System.Net.Mail.SmtpClient mail = new System.Net.Mail.SmtpClient();
            objmail.From = new System.Net.Mail.MailAddress(From);
   
            objmail.To.Add(new System.Net.Mail.MailAddress(To));
            objmail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

         
            objmail.IsBodyHtml = true;

            objmail.Body = Body;
            objmail.Subject = Subject;
            objmail.Priority = System.Net.Mail.MailPriority.Normal;
            //mail.Host = Config.GS.SMTPServer;
            //mail.Host = "localhost";
            mail.Host = ConfigurationManager.AppSettings["SMTP"].ToString();
            mail.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["FROMEMAIL"].ToString(), ConfigurationManager.AppSettings["FROMPWD"].ToString());
            mail.Port = Convert.ToInt32(ConfigurationManager.AppSettings["PORT"].ToString());
            mail.EnableSsl = false;
            mail.Send(objmail);
            SendMailStatus = true;
         
        }
        catch (Exception exc)
        {
            string str = exc.Message.ToString();
            
        }
        return SendMailStatus;
    }
   
}