using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Web.Services;
using System.Data;

public partial class Client_Default : System.Web.UI.Page
{
    RegistrationBAL ObjRegistration = new RegistrationBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RegistrationID"] != null && Convert.ToInt32(Session["RegistrationID"]) > 0)
            {
                Response.Redirect("Index.aspx");
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        ObjRegistration.FirstName = txtFname.Text;
        ObjRegistration.LastName = txtLname.Text;
        ObjRegistration.EmailAddress = txtEmail.Text;
        ObjRegistration.Password = txtPassword.Text;
        ObjRegistration.IsActive = false;
        ObjRegistration.UserType = 3;
        ObjRegistration.ActivationID = System.Guid.NewGuid().ToString();
        int RegisterID = ObjRegistration.InsertRegistration();

        ObjRegistration = new RegistrationBAL();
        ObjRegistration.RegistrationID = RegisterID;
        DataTable dt = ObjRegistration.SelectRegistraionByID();
        if (dt.Rows.Count != 0)
        {
            EmailSender objemail = new EmailSender();
            System.Text.StringBuilder strBody = new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "register.htm")));
            objemail.RegistrationMail(dt.Rows[0]["FirstName"].ToString() + "  " + dt.Rows[0]["LastName"].ToString(), "", dt.Rows[0]["Password"].ToString(), dt.Rows[0]["EmailAddress"].ToString(), strBody, Config.WebSiteMain + "Client/Activation.aspx?Id=" + dt.Rows[0]["ActivationID"].ToString(), dt.Rows[0]["ActivationID"].ToString());
        }

        // ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('Your account is Created successfully.')", true);
        //   lblMessage.Text = "Your account has been Created successfully";
        lblMessage.Text = "Your account has been created and an activation link has been sent to the email address you entered. You must activate the account by clicking on the activation link when you get the email before you can login.";
        ClearAll();


    }
    public void ClearAll()
    {
        txtEmail.Text = "";
        txtFname.Text = "";
        txtLname.Text = "";
        txtPassword.Text = "";
        txtretypepassword.Text = "";
        chk1.Checked = false;


    }

    #region Webservice

    [WebMethod]
    public static bool getUserExists(string email)
    {
        RegistrationBAL ObjRegistration = new RegistrationBAL();
        System.Data.DataTable dt = new System.Data.DataTable();
        ObjRegistration.EmailAddress = email;
        ObjRegistration.UserType = 3;
        dt = ObjRegistration.SelectAllRegistrationPaging(1, 1);
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        return false;

    }
    #endregion

}

