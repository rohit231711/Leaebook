using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BAL;
public partial class admin_adminmaster : System.Web.UI.MasterPage
{
    
    RegistrationBAL objRegistration = new RegistrationBAL();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblCopyRights.Text = "Copyright © " + System.DateTime.Now.Year + " Lea";
            if (Session["AdminUsername"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                name.Text = Session["AdminUsername"].ToString();
            }

            if (Session["AdminRegistrationID"] != null)
            {
                objRegistration.RegistrationID = Convert.ToInt32(Session["AdminRegistrationID"]);
                DataTable dt = objRegistration.SelectRegistraionByID();


                if (dt.Rows.Count > 0)
                {
                    ltrlLastLogin.Text = Convert.ToDateTime(dt.Rows[0]["LastLoginDate"]).ToString("yyyy-MM-dd hh:mm tt");
                }

            }

            //// var navigateUrlParams = item.NavigateUrl.Split('/');
            // if (Request.Url.AbsoluteUri.IndexOf(navigateUrlParams[navigateUrlParams.Length - 1]) != -1)
            // {
            //     item.Selected = true;
            // }

              string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            //return sRet;

            if (aDashboard.HRef == sRet)
            {
                //aDashboard.Attributes["class"] = "main avtive_menu";
                aDashboard.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aBook.HRef == sRet || a1.HRef == sRet)
            {
                aBook.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aCategory.HRef == sRet)
            {
                aCategory.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aUser.HRef == sRet)
            {
                aUser.Attributes.Add("data-class", "main avtive_menu");
            }
            //else if (aConfigure.HRef == sRet)
            //{
            //    aConfigure.Attributes.Add("data-class", "main avtive_menu");
            //}
            //else if (a5.HRef == sRet)
            //{
            //    a5.Attributes.Add("data-class", "main avtive_menu");
            //}
            else if (aReport.HRef == sRet || aReport1.HRef == sRet || a10.HRef == sRet)
            {
                aReport.Attributes.Add("data-class", "main avtive_menu");
            }
            //else if (a2.HRef == sRet)
            //{
            //    a2.Attributes.Add("data-class", "main avtive_menu");
            //}
            else if (a6.HRef == sRet)
            {
                a6.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (a18.HRef == sRet)
            {
                a18.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aPartner.HRef == sRet || addPartner.HRef == sRet)
            {
                aPartner.Attributes.Add("data-class", "main avtive_menu");
            }
            //else if (a8.HRef == sRet)
            //{
            //    a8.Attributes.Add("data-class", "main avtive_menu");
            //}
            else if (a9.HRef == sRet || a8.HRef == sRet || a2.HRef == sRet || a5.HRef == sRet
                    || aConfigure.HRef == sRet || aCountry.HRef == sRet || aregion.HRef == sRet || acity.HRef == sRet)
            {
                a9.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (a13.HRef == sRet || a16.HRef == sRet || ana.HRef == sRet)
            {
                a13.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aShipping.HRef == sRet || aShipping1.HRef == sRet)
            {
                aShipping.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (aOrderManag.HRef == sRet || aOrderHistory.HRef == sRet)
            {
                aOrder.Attributes.Add("data-class", "main avtive_menu");
            }
            else if (a11.HRef == sRet)
            {
                a11.Attributes.Add("data-class", "main avtive_menu");
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
