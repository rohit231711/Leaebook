using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Admin_AddPartner : System.Web.UI.Page
{
    RegistrationBAL objUser = new RegistrationBAL();

    DataTable DT = new DataTable();
    RegistrationBAL objuser = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindCountry();
            accessrights();

            if (Request.QueryString["RegistrationID"] != null)
            {
                objUser = new RegistrationBAL();
                objUser.RegistrationID = RegistrationID;
                DataTable dt = objUser.SelectRegistraionByID();
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                    txtLastName.Text = dt.Rows[0]["LastName"].ToString();
                    txtUserName.Text = dt.Rows[0]["UserName"].ToString();
                    txtUserName.Enabled = false;
                    txtEmail.Text = dt.Rows[0]["EmailAddress"].ToString();
                    txtName.Text = dt.Rows[0]["FirstName"].ToString();
                    txtPassword.Attributes.Add("value", dt.Rows[0]["Password"].ToString());
                    //txtConfirm.Attributes.Add("value", dt.Rows[0]["Password"].ToString());
                    chkNewsLetter.Checked = Convert.ToBoolean(dt.Rows[0]["IsNewsLetter"].ToString());
                    chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    hfUserType.Value = dt.Rows[0]["UserType"].ToString();
                    txtDate1.Text = dt.Rows[0]["BirthdayDate"].ToString();
                    // drpCountry.SelectedValue = dt.Rows[0]["BirthdayDate"].ToString();
                    // drpGender.SelectedValue = dt.Rows[0]["GenderID"].ToString();
                    drpCountry.Items.FindByValue(dt.Rows[0]["Countryid"].ToString()).Selected = true;
                    drpGender.Items.FindByValue(dt.Rows[0]["GenderID"].ToString()).Selected = true;
                }
            }
        }
    }
    private void BindCountry()
    {
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllActiveCountry();

        if (dt.Rows.Count > 0)
        {
            drpCountry.DataSource = dt;
            drpCountry.DataTextField = "countryname";
            drpCountry.DataValueField = "countryid";
            drpCountry.DataBind();
            //ddlCategory.Items.Insert(0, new ListItem("Select", ""));
            //ddlCategory.SelectedIndex = 0;
        }

    }
    private void accessrights()
    {
        int fl = 0;
        objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
        DT = objmenu.GetRightsByUser();
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 8)
                {
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 1)
                    {
                        fl = 1;
                    }
                }
            }
            if (fl == 0)
            {
                Response.Redirect("accessdenied.aspx");
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        Response.Redirect("ManagePartner.aspx");

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int a;
        objUser = new RegistrationBAL();
        objUser.FirstName = txtName.Text.Trim();
        objUser.LastName = txtLastName.Text.Trim();
        objUser.EmailAddress = txtEmail.Text.Trim();
        objUser.Password = txtPassword.Text.Trim();
        objUser.UserName = txtUserName.Text.Trim();

        objUser.IsActive = chkActive.Checked;
        objUser.IsNewsLetter = chkNewsLetter.Checked;
        objUser.UserType = 0;
        objUser.GenderID = Convert.ToInt64(drpGender.SelectedValue);
        objUser.BirthdayDate = txtDate1.Text;
        objUser.Countryid = Convert.ToInt64(drpCountry.SelectedValue);
        objUser.ActivationID = System.Guid.NewGuid().ToString();


        if (Convert.ToBoolean(Request.QueryString["EDIT"]))
        {

            objUser.RegistrationID = RegistrationID;
            objUser.UpdateRegistration();


            Response.Redirect("ManageRegistration.aspx?edit=true");

        }

    }

    protected void btnSubmitNew_Click(object sender, EventArgs e)
    {
        BAL_Account Obj_account = new BAL_Account();
        Obj_account.Email = txtEmail.Text.Trim();
         

        if (Convert.ToBoolean(Request.QueryString["EDIT"]))
        {
            Obj_account.RegistrationID = Convert.ToInt32(Request.QueryString["RegistrationID"]);
            Obj_account.UserName = "";//txt_username.Text.Trim();
            DataTable dt = Obj_account.Check_Email_Duplication();
            int emailcount = Convert.ToInt32(dt.Rows[0]["emailcount"].ToString());
            dt.Clear();
            dt = Obj_account.Check_User_Duplication();
            int usercount = 0;
            if (emailcount == 0 && usercount == 0)
            {
                objUser = new RegistrationBAL();
                objUser.FirstName = txtName.Text.Trim();
                objUser.LastName = txtLastName.Text.Trim();
                objUser.EmailAddress = txtEmail.Text.Trim();
                objUser.Password = txtPassword.Text.Trim();
                objUser.UserName = txtUserName.Text.Trim();

                objUser.IsActive = chkActive.Checked;
                objUser.IsNewsLetter = chkNewsLetter.Checked;
                objUser.UserType = 0;
                objUser.GenderID = Convert.ToInt64(drpGender.SelectedValue);
                objUser.BirthdayDate = txtDate1.Text;
                objUser.Countryid = Convert.ToInt64(drpCountry.SelectedValue);
                objUser.ActivationID = System.Guid.NewGuid().ToString();


                if (Convert.ToBoolean(Request.QueryString["EDIT"]))
                {

                    objUser.RegistrationID = RegistrationID;
                    objUser.UpdateRegistration();


                    Response.Redirect("ManagePartner.aspx");

                }
            }
            else
            {
                if (emailcount > 0)
                {
                    txtEmail.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('EmailId " + txtEmail.Text.ToString() + " is already exist.');", true);
                }
            }
        }
        else
        {
            Obj_account.RegistrationID = 0;
            Obj_account.UserName = "";//txt_username.Text.Trim();
            DataTable dt = Obj_account.Check_User_Duplication();
            int usercount = Convert.ToInt32(dt.Rows[0]["usercount"].ToString());
            dt = Obj_account.Check_Email_Duplication();
            int emailcount = Convert.ToInt32(dt.Rows[0]["emailcount"].ToString());
            dt.Clear();
            dt = Obj_account.Check_User_Duplication();
            usercount = 0;
            if (emailcount == 0 && usercount == 0)
            {
                BAL_Account objUser = new BAL_Account();

                objUser.FirstName = txtName.Text.ToString().Trim();
                objUser.LastName = "";//txt_lastname.Text.ToString().Trim();
                objUser.Email = txtEmail.Text.Trim();
                objUser.Password = txtPassword.Text.Trim();
                objUser.CreatedDate = System.DateTime.Now;
                objUser.IsActive = false;
                objUser.IsNewsLetter = chkNewsLetter.Checked;
                objUser.UserType = 4;
                objUser.LoginDate = System.DateTime.Now;
                objUser.LastLoginDate = System.DateTime.Now;
                objUser.NewIssues = false;
                objUser.Renewals = false;
                objUser.AppUpdates = false;
                objUser.FacebookEmail = "";
                objUser.ActivationID = System.Guid.NewGuid().ToString();
                objUser.UserName = txtUserName.Text.ToString().Trim();
                objUser.GenderID = Convert.ToInt32(drpGender.SelectedValue);
                objUser.BirthdayDate = Convert.ToDateTime(txtDate1.Text.ToString());
                objUser.Countryid = Convert.ToInt32(drpCountry.SelectedValue);
                objUser.LanguageID = 1;

                int ID = objUser.InsertRegistration();
                sendmail(objUser.ActivationID);

                Response.Redirect("ManagePartner.aspx");
            }
            else
            {
                if (emailcount > 0)
                {
                    txtEmail.Text = "";
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('EmailId " + txtEmail.Text.ToString() + " is already exist.');", true);
                }
            }
        }
    }

    public void sendmail(string ID)
    {
        string ContactEmail = "";
        WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
        DataTable DT = WSB.GetAllWebseetings();
        if (DT != null && DT.Rows.Count > 0)
        {
            ContactEmail = DT.Rows[0]["ContactUs"].ToString();
        }
        if (ID.ToString() != "")
        {

            string Email = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&loginid=" + ID;


            string body = string.Empty;
            string file = Server.MapPath("~\\EmailTemplates\\ActivationPartner.htm");
            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{Email}", Email);
            body = body.Replace("{txt_firstname}", txtName.Text);
            body = body.Replace("{ContactEmail}", ContactEmail);
            body = body.Replace("{SiteUrl}", Config.WebSiteMain + @"Partner/Default.aspx");
            System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);
            Global.SendEmail(txtEmail.Text, "LEA eBooks | Activation", strBody.ToString());

        }
    }

    public int RegistrationID
    {
        get
        {
            int id = 0;
            if (Request.QueryString["RegistrationID"] != null || Request.QueryString["RegistrationID"] != "")
            {
                id = Convert.ToInt32(Request.QueryString["RegistrationID"]);
                return id;
            }
            return id;
        }
    }
}