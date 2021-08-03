using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class Admin_AddAdmin : System.Web.UI.Page
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
                    txtEmail.Text = dt.Rows[0]["EmailAddress"].ToString();
                    txtUserName.Text = dt.Rows[0]["FirstName"].ToString();
                    txtPassword.Attributes.Add("value", dt.Rows[0]["Password"].ToString());
                    //txtConfirm.Attributes.Add("value", dt.Rows[0]["Password"].ToString());
                    chkNewsLetter.Checked = Convert.ToBoolean(dt.Rows[0]["IsNewsLetter"].ToString());
                    hfUserType.Value = dt.Rows[0]["UserType"].ToString();
                    //txtDate1.Text = Convert.ToDateTime(dt.Rows[0]["BirthdayDate"].ToString()).ToString("dd/MM/yyyy");
                    txtDate1.Text = dt.Rows[0]["BirthdayDate"].ToString().Replace("12:00 AM", "");
                   // drpCountry.SelectedValue = dt.Rows[0]["BirthdayDate"].ToString();
                    drpGender.SelectedValue = dt.Rows[0]["GenderID"].ToString();
                    drpCountry.Items.FindByValue(dt.Rows[0]["Countryid"].ToString()).Selected = true;
                    //drpGender.Items.FindByValue(dt.Rows[0]["GenderID"].ToString()).Selected = true;
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
            drpCountry.Items.Insert(0, new ListItem("Select", "0"));
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
        if(!string.IsNullOrEmpty(hfUserType.Value))
        {
            switch(hfUserType.Value)
            {
                case "3":
                    Response.Redirect("ManageRegistration.aspx");
                    break;
                case "4" :
                    Response.Redirect("ManagePartner.aspx");
                    break;
                default:
                    Response.Redirect("dashboard.aspx");
                    break;
            }
        }
        else
        {
            Response.Redirect("ManageRegistration.aspx");
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int a;
        objUser = new RegistrationBAL();
        objUser.FirstName = txtUserName.Text.Trim();
        objUser.LastName = txtLastName.Text.Trim();
        objUser.EmailAddress = txtEmail.Text.Trim();
        objUser.Password = txtPassword.Text.Trim();
        objUser.UserName = txtUserName.Text.Trim();

        objUser.IsActive = true;
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