using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Web.Services;

public partial class Client_Account : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    RegistrationBAL objReg = new RegistrationBAL();
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    public Int32 UserID
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["UserID"])) ? Convert.ToInt32(ViewState["UserID"]) : 0; }
        set { ViewState["UserID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Session["UserSession"] != null)
            {

                dt = Session["UserSession"] as DataTable;
                if (dt.Rows.Count > 0)
                {

                    UserID = Convert.ToInt32(dt.Rows[0]["RegistrationID"].ToString());
                    objReg.RegistrationID = UserID;
                    dt = new DataTable();

                    dt = objReg.SelectRegistraionByID();
                    lblFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                    lblLastName.Text = dt.Rows[0]["LastName"].ToString();
                    lblEmailAddress.Text = dt.Rows[0]["EmailAddress"].ToString();
                    txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                    txtLastName.Text = dt.Rows[0]["LastName"].ToString();
                    pnlPaymenthistory.Visible = false;
                    pnlProfile.Visible = true;
                    pnlSubscriptions.Visible = false;
                    apurchase.Attributes.Remove("class");
                    aAccount.Attributes.Add("class", "lnkhover");

                    aSub.Attributes.Remove("class");
                }

            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
    }
    protected void lkbSave_Click(object sender, EventArgs e)
    {
        if (Session["UserSession"] != null)
        {
            dt = new DataTable();
            dt = Session["UserSession"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (txtCurrentPass.Text.Trim() == dt.Rows[0]["Password"].ToString())
                {
                    objReg.RegistrationID = Convert.ToInt64(dt.Rows[0]["RegistrationID"].ToString());
                    objReg.Password = txtNewPass.Text.Trim();
                    objReg.ChangePassword();
                    ScriptManager.RegisterStartupScript(this, GetType(), "changePass",
                "alert('Password changed successfully.');", true);

                }
            }
        }
        else
        {
            Response.Redirect("Index.aspx");
        }
    }

    protected void aPaymentClick(object sender, EventArgs e)
    {
        pnlPaymenthistory.Visible = true;
        pnlProfile.Visible = false;
        pnlSubscriptions.Visible = false;
        ObjBookOrderBal.CustomerID = UserID;
        dt = ObjBookOrderBal.UserPurchaseHistory().Tables[0];
        gvPaymentHistory.DataSource = dt;
        gvPaymentHistory.DataBind();
        aAccount.Attributes.Remove("class");
        apurchase.Attributes.Add("class", "lnkhover");
        aSub.Attributes.Remove("class");



    }
    protected void aSubClick(object sender, EventArgs e)
    {
        pnlPaymenthistory.Visible = false;
        pnlProfile.Visible = false;
        pnlSubscriptions.Visible = true;
        BindSubscriptions();
        



    }

    protected void aAccountClick(object sender, EventArgs e)
    {
        pnlPaymenthistory.Visible = false;
        pnlProfile.Visible = true;
        pnlSubscriptions.Visible = false;
        apurchase.Attributes.Remove("class");
        aAccount.Attributes.Add("class", "lnkhover");

        aSub.Attributes.Remove("class");

    }

    protected void lkbUpdate_Click(object sender, EventArgs e)
    {
        if (Session["UserSession"] != null)
        {
            dt = new DataTable();
            dt = Session["UserSession"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    objReg.RegistrationID = Convert.ToInt64(dt.Rows[0]["RegistrationID"].ToString());
                    dt = new DataTable();

                    dt = objReg.SelectRegistraionByID();
                    objReg.RegistrationID = Convert.ToInt64(dt.Rows[0]["RegistrationID"].ToString());
                    objReg.FirstName = txtFirstName.Text.Trim();
                    objReg.LastName = txtLastName.Text.Trim();
                    objReg.EmailAddress = dt.Rows[0]["EmailAddress"].ToString();
                    objReg.Password = dt.Rows[0]["Password"].ToString();
                    objReg.IsActive = !string.IsNullOrEmpty(dt.Rows[0]["IsActive"].ToString()) ? Global.ConvertToBool(dt.Rows[0]["IsActive"].ToString()) : false;
                    objReg.IsNewsLetter = !string.IsNullOrEmpty(dt.Rows[0]["IsNewsLetter"].ToString()) ? Global.ConvertToBool(dt.Rows[0]["IsNewsLetter"].ToString()) : false;
                    objReg.UserType = Convert.ToInt16(dt.Rows[0]["UserType"].ToString());
                    objReg.UpdateRegistration();
                    ScriptManager.RegisterStartupScript(this, GetType(), "changePass",
                "alert('Details changed successfully.');", true);
                    lblFirstName.Text = txtFirstName.Text;
                    lblLastName.Text = txtLastName.Text;
                    txtFirstName.Text = string.Empty;
                    txtLastName.Text = string.Empty;

                }



            }
        }
        else
        {
            Response.Redirect("Index.aspx");
        }
    }
    [WebMethod]
    public static bool checkPassword(string password)
    {
        DataTable dt = new DataTable();
        dt = HttpContext.Current.Session["UserSession"] as DataTable;
        if (dt.Rows.Count > 0)
        {
            if (password != dt.Rows[0]["Password"].ToString())
            {
                return true;
            }

        }
        return false;



    }

    public void BindSubscriptions()
    {
        ObjBookOrderBal.CustomerID = UserID;
        dt = ObjBookOrderBal.GetSubscriptions();
        if (dt.Rows.Count > 0)
        {
            grdSubscriptions.DataSource = dt;
            grdSubscriptions.DataBind();
        }

        aAccount.Attributes.Remove("class");
        apurchase.Attributes.Remove("class");
        aSub.Attributes.Add("class", "lnkhover");
    }

    protected void gvPaymentHistory_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPaymentHistory.PageIndex = e.NewPageIndex;

        ObjBookOrderBal.CustomerID = UserID;
        dt = ObjBookOrderBal.UserPurchaseHistory().Tables[0];
        gvPaymentHistory.DataSource = dt;
        gvPaymentHistory.DataBind();
        aAccount.Attributes.Remove("class");
        apurchase.Attributes.Add("class", "lnkhover");
    }
    protected void grdSubscriptions_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSubscriptions.PageIndex = e.NewPageIndex;

        BindSubscriptions();
    }
    
}