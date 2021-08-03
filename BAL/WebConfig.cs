using System;
using System.Configuration;

/// <summary>
/// Summary description for WebConfig
/// </summary>
public static class Config
{
 
    public static readonly string SMTP = ConfigurationManager.AppSettings["SMTP"];
    public static readonly string FROMEMAIL = ConfigurationManager.AppSettings["FROMEMAIL"];
    public static readonly string FROMPWD = ConfigurationManager.AppSettings["FROMPWD"];
    public static readonly string   WebSite = ConfigurationManager.AppSettings["SiteUrl"];
    public static readonly string WebSiteMain = ConfigurationManager.AppSettings["SiteUrlMain"];
    public static readonly string EmailTemplate = ConfigurationManager.AppSettings["EmailTemplate"];
    public static readonly string Copyrights = ConfigurationManager.AppSettings["Copyrights"];
    public static readonly string EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
    public static readonly string PushApiKey = ConfigurationManager.AppSettings["ApiKey"];
    public static readonly string PushSenderID = ConfigurationManager.AppSettings["PushSenderID"];
    
    
}
