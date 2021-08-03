using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;

public partial class Admin_PasswordChange : System.Web.UI.Page
{
    RegistrationBAL objRegistration = new RegistrationBAL();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            txtoldpassword.Text = "";
            objRegistration.RegistrationID = RegistrationID;
            dt = objRegistration.SelectRegistraionByID();
            if (dt.Rows.Count > 0)
            {
                hdnPassword.Value = dt.Rows[0]["Password"].ToString();
            }
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (txtoldpassword.Text != "" && txtnewpassword.Text != "" && txtconfirmpassword.Text != "")
        {
            objRegistration.RegistrationID = RegistrationID;
            dt = objRegistration.SelectRegistraionByID();
            if (dt.Rows.Count > 0)
            {                
               objRegistration.RegistrationID = RegistrationID;
               objRegistration.Password = txtnewpassword.Text.Trim();
               objRegistration.ChangePassword();               
               ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Password changed successfully.');", true);               
            }
        }
    }

    public int RegistrationID
    {
        get
        {
            int id = 0;
            if (Session["AdminRegistrationID"] != null || Session["AdminRegistrationID"] != "")
            {
                id = Convert.ToInt32(Session["AdminRegistrationID"]);
                return id;
            }
            return id;
        }
    }
}