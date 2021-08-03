using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web;

using System.Data;
using General;

public class EmailSender
{

    public bool RegistrationMail(string strName, string strUsername, string strPassword, string stremail, StringBuilder strhtml, string VerificationUrl, string VerificationCode)
    {
        //        System.Text.StringBuilder strBody = new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "register.htm")));

        strhtml.Replace("{%[SiteName]%}", Config.WebSiteMain);
        strhtml.Replace("{%[SiteUrl]%}", VerificationUrl);
        strhtml.Replace("{%[Code]%}", VerificationCode);
        strhtml.Replace("{%[CopyRight]%}", Config.Copyrights);
        strhtml.Replace("{%[UserName]%}", strUsername);
    
        strhtml.Replace("{%[Password]%}", strPassword);
        strhtml.Replace("{%[Name]%}", strName);
        strhtml.Replace("{%[EmailAddr]%}", stremail);

        return App.SendEmail(stremail, Config.FROMEMAIL, "Lea | Activation", strhtml.ToString());
    }
    public bool ForgotPasswordEmail(string strName, string strUsername, string strPassword, string stremail, StringBuilder strhtml)
    {
        //        System.Text.StringBuilder strBody = new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "register.htm")));

        strhtml.Replace("{%[SiteName]%}", Config.WebSiteMain);
        strhtml.Replace("{%[Name]%}", strName);
        strhtml.Replace("{%[CopyRight]%}", Config.Copyrights);
        strhtml.Replace("{%[UserName]%}", strUsername);
        strhtml.Replace("{%[Password]%}", strPassword);

        strhtml.Replace("{%[EmailAddr]%}", stremail);

        return App.SendEmail(stremail, Config.FROMEMAIL, "Lea::ForgotPassword", strhtml.ToString());
    }


    public bool ContactUsEmail(string strName, string phno, string subject, string stremail, string msg,string address, StringBuilder strhtml)
    {
        //        System.Text.StringBuilder strBody = new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "register.htm")));


        strhtml.Replace("{%[SiteName]%}", Config.WebSiteMain);

        strhtml.Replace("{%[CopyRight]%}", Config.Copyrights);

        strhtml.Replace("{%[FullName]%}", strName);
        strhtml.Replace("{%[TelNo]%}", phno);

        strhtml.Replace("{%[Email]%}", stremail);
        strhtml.Replace("{%[Message]%}", msg);
        strhtml.Replace("{%[Address]%}", address);

        return App.SendEmail(stremail,Config.FROMEMAIL, subject, strhtml.ToString());
    }
    public bool AboutUsEmail(string strFName, string strLName, string Country, string state, string phno, string subject, string stremail, StringBuilder strhtml)
    {

        strhtml.Replace("{%[SiteName]%}", Config.WebSiteMain);
        strhtml.Replace("{%[CopyRight]%}", Config.Copyrights);
        strhtml.Replace("{%[FName]%}", strFName);
        strhtml.Replace("{%[LName]%}", strLName);
        strhtml.Replace("{%[TelNo]%}", phno);
        strhtml.Replace("{%[Country]%}", Country);
        strhtml.Replace("{%[Email]%}", stremail);
    
        strhtml.Replace("{%[State]%}", state);
        return App.SendEmail(Config.FROMEMAIL, stremail, subject, strhtml.ToString());
    }

}