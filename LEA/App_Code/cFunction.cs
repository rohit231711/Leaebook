using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;
using System.Threading;
using BAL;

/// <summary>
/// Summary description for cFunction
/// </summary>
public class cFunction
{
	public cFunction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string GetVal(string Key, string CurrentCulture = "")
    {
        string strVal = "";
        try
        {
            WebsiteSettingsBAL oWebsiteSettingsBAL = new WebsiteSettingsBAL();
            DataTable DT = oWebsiteSettingsBAL.GetAllWebseetings();
            if (DT != null && DT.Rows.Count > 0)
            {
                strVal = DT.Rows[0]["BrowserTitle"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return strVal;
    }
}