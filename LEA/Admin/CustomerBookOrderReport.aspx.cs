using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;

public partial class Admin_CustomerBookOrderReport : System.Web.UI.Page
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
                registr.RegistrationID = Convert.ToInt32(Request.QueryString["ID"]);
                DataTable dt = registr.SelectRegistraionByID();
                BindGrid(Convert.ToInt32(Request.QueryString["ID"]));
                if (dt.Rows.Count > 0)
                {
                    lblName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                    lblEmail.Text = dt.Rows[0]["EmailAddress"].ToString();
                }
            }
        }
    }
    
    public string PicturePath(string sFilename)
    {
        if (!File.Exists(Server.MapPath("~") + sFilename))
        {
            sFilename = "../images/No_Image.jpg";
        }
        sFilename = "../" + sFilename;
        return sFilename;
    }
    public void BindGrid(int CustomerID)
    {
        ObjBookOrderBal.CustomerID = CustomerID;
        dt = ObjBookOrderBal.get_CustomerBookOrderByID();
        if (dt.Rows.Count > 0)
        {
            grdSubscriptions.DataSource = dt;
            grdSubscriptions.DataBind(); 
        }
        if (dt.Rows.Count == 0)
        {
            lblNoRecord.Visible = true;
        }
        else
        {
            lblNoRecord.Visible = false;
        }
    }
    protected void grdSubscriptions_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grdSubscriptions.PageIndex = e.NewPageIndex;

        //BindSubscriptions();
    }
}