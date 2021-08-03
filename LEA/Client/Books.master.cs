using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_Books : System.Web.UI.MasterPage
{
    RegistrationBAL ObjRegistration = new RegistrationBAL();
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    DataTable dt = new DataTable();
    HttpCookie cook;
    public string UserName
    {
        get { return ViewState["UserName"] != null ? Convert.ToString(ViewState["UserName"]) : ""; }
        set { ViewState["UserName"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserSession"] != null)
            {
                DataTable dt = Session["UserSession"] as DataTable;
                UserName = dt.Rows[0]["FirstName"].ToString();
            }
           


        }
    }

    
    protected void alogout_Clck(object sender, EventArgs e)
    {

        Response.Redirect("Index.aspx?log=1");


    }
}
