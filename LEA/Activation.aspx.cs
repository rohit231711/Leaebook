using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BAL;


public partial class _Default : System.Web.UI.Page
{
    BAL_Account obj_login = new BAL_Account();
    RegistrationBAL objUser = new RegistrationBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["login"] != null && Request.QueryString["login"].ToString() == "1")
            {
                objUser.ActivationID = Request.QueryString["login"].ToString();
                DataTable dt1 = objUser.ActiveAccount();
                if (dt1.Rows.Count > 0)
                {
                    lblName.Text = dt1.Rows[0]["FirstName"].ToString();
                    hndEmail.Value = dt1.Rows[0]["EmailAddress"].ToString();
                    hndPassword.Value = dt1.Rows[0]["Password"].ToString();
                }

                obj_login.UserName = hndEmail.Value.ToString().Trim();
                obj_login.Password = hndPassword.Value.ToString().Trim();
                DataTable dt = new DataTable();
                dt = obj_login.Check_Login();
                if (dt.Rows.Count > 0)
                {
                    Session["UserName"] = dt.Rows[0]["FirstName"].ToString();
                    Session["UserID"] = dt.Rows[0]["RegistrationID"].ToString();    
                    Response.Redirect("Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
            }

            if (Request.QueryString["id"] != null)
            {
                HyperLink1.NavigateUrl = "Activation.aspx?login=" + Request.QueryString["id"].ToString();
                objUser.ActivationID = Request.QueryString["id"].ToString();
                DataTable dt = objUser.ActiveAccount();
                if (dt.Rows.Count > 0)
                {
                    lblName.Text = dt.Rows[0]["FirstName"].ToString();
                    hndEmail.Value = dt.Rows[0]["EmailAddress"].ToString();
                    hndPassword.Value = dt.Rows[0]["Password"].ToString();
                }
            }
            else
            {
                Response.Redirect("Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);                
            }
        }        
    }
}