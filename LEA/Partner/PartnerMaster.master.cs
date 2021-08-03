using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BAL;
public partial class Partner_Partnermaster : System.Web.UI.MasterPage
{
    
    RegistrationBAL objRegistration = new RegistrationBAL();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCopyRights.Text = "Copyright © " + System.DateTime.Now.Year + " LEA eBooks";
            if (Session["PartnerUsername"] == null || Session["PartnerRegistrationID"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                name.Text = Session["PartnerUsername"].ToString();
            }

            if (Session["PartnerRegistrationID"] != null)
            {
                objRegistration.RegistrationID = Convert.ToInt32(Session["PartnerRegistrationID"]);
                DataTable dt = objRegistration.SelectRegistraionByID();


                if (dt.Rows.Count > 0)
                {
                    ltrlLastLogin.Text = Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]).ToString("yyyy-MM-dd hh:mm tt");
                }

            }

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            
            if (aDashboard.HRef == sRet)
            {
                aDashboard.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aBook.HRef == sRet|| a1.HRef == sRet)
            {
                aBook.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aOrderManag.HRef == sRet || aOrderHistory.HRef == sRet)
            {
                aOrder.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aReport.HRef == sRet || aReport1.HRef == sRet || a10.HRef == sRet)
            {
                aReport.Attributes.Add("data-class", "main avtive_menu");
            }
            else
            {
                aDashboard.Attributes.Add("data-class", "main avtive_menu");
            }
           
        }

    }
    protected void lkbLogout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.Clear();
        Session.RemoveAll();
        Response.Redirect("Default.aspx?LogOut=1");
    }

}
