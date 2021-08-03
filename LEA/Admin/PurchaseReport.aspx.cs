using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Web.Services;

public partial class PurchaseReport : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    RegistrationBAL objReg = new RegistrationBAL();
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    RegistrationBAL registr = new RegistrationBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null && Convert.ToInt32(Request.QueryString["ID"]) > 0)
            {
                BindGrid(Convert.ToInt32(Request.QueryString["ID"]));
                registr.RegistrationID = Convert.ToInt32(Request.QueryString["ID"]);
                DataTable dt = registr.SelectRegistraionByID();
                if (dt.Rows.Count > 0)
                {
                    lblName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                    lblEmail.Text = dt.Rows[0]["EmailAddress"].ToString();
                }
            }
        }
    }



    protected void BindGrid(Int32 CustomerID)
    {
        ObjBookOrderBal.CustomerID = CustomerID;
        dt = ObjBookOrderBal.UserPurchaseHistory().Tables[0];
        gvPaymentHistory.DataSource = dt;
        gvPaymentHistory.DataBind();
        gvPaymentHistory.Visible = true;

        if (dt.Rows.Count == 0)
        {

            lblNoRecord.Visible = true;
        }
        else
        {
            lblNoRecord.Visible = false;
        }
    }

    protected void gvPaymentHistory_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPaymentHistory.PageIndex = e.NewPageIndex;

        if (Request.QueryString["ID"] != null && Convert.ToInt32(Request.QueryString["ID"]) > 0)
        {
            ObjBookOrderBal.CustomerID = Convert.ToInt32(Request.QueryString["ID"]);
        }
        dt = ObjBookOrderBal.UserPurchaseHistory().Tables[0];
        gvPaymentHistory.DataSource = dt;
        gvPaymentHistory.DataBind();
    }
}