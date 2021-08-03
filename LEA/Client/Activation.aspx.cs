using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using BAL;

public partial class Activation : System.Web.UI.Page
{
    RegistrationBAL objUser = new RegistrationBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            objUser.ActivationID = Request.QueryString["id"].ToString();


            DataTable dt = objUser.ActiveAccount();

            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
            }
            else
            {
                Response.Redirect("Index.aspx");
            }
        }
        else
        {
            Response.Redirect("Index.aspx");
        }

    }
}