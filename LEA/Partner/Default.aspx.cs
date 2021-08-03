using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using BAL;
using System.Data;
public partial class Partner_Default : System.Web.UI.Page
{
    RegistrationBAL objRegistration = new RegistrationBAL();

    DataTable dt = new DataTable();
    HttpCookie cook;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["PartnerUserName"] != null && Request.Cookies["PartnerPassword"] != null)
            {
                txtUserName.Text = Request.Cookies["PartnerUserName"].Value;
                txtPassword.Attributes["value"] = Request.Cookies["PartnerPassword"].Value;
                chklogin.Checked = true;
            }

            lblCopyRights.Text = "Copyright © " + System.DateTime.Now.Year + " eBooks";

            if (Request.QueryString["LogOut"] == "1")
            {
                dvalert.Style.Add("display", "");
                lblAlert.Text = "You have successfully logout!";
            }
        }
        txtUserName.Focus();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (txtUserName.Text != "" && txtPassword.Text != "")
        {
            objRegistration.UserName = txtUserName.Text;
            objRegistration.Password = txtPassword.Text;
            objRegistration.RegistrationID = 0;
            dt = objRegistration.LoginValidation();

            if (dt.Rows.Count > 0)
            {
                Session["PartnerRegistrationID"] = Convert.ToInt32(dt.Rows[0]["RegistrationID"]);
                objRegistration.RegistrationID = Convert.ToInt32(Session["PartnerRegistrationID"]);
                Session["PartnerUsername"] = dt.Rows[0]["FirstName"] + " " + dt.Rows[0]["LastName"];
                objRegistration.LastLoginUpdate();
                
                
                if (chklogin.Checked)
                {
                    Response.Cookies["PartnerUserName"].Expires = DateTime.Now.AddDays(5);
                    Response.Cookies["PartnerPassword"].Expires = DateTime.Now.AddDays(5);
                    Response.Cookies["PartnerUserName"].Value = txtUserName.Text.Trim();
                    Response.Cookies["PartnerPassword"].Value = txtPassword.Text.Trim();
                }
                else
                {
                    Response.Cookies["PartnerUserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["PartnerPassword"].Expires = DateTime.Now.AddDays(-1);
                }

                if (dt.Rows[0]["UserType"].ToString() == "4")
                {
                    Response.Redirect("dashboard.aspx?l=en-US");
                }
            }
            else
            {
                txtUserName.Text = null;
                txtPassword.Text = null;
                dvalert.Visible = true;
                lblAlert.Text = "Invalid username or password!";
                txtUserName.Focus();
            }
        }
        else
        {
            dvalert.Visible = true;
            lblAlert.Text = "Please enter username and password!";
            txtUserName.Focus();
        }
    }


}