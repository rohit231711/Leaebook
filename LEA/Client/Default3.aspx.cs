using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_Default3 : System.Web.UI.Page
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

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        ObjRegistration.FacebookEmail = "";
        ObjRegistration.EmailAddress = txtEmail.Text;
        ObjRegistration.Password = txtPassword.Text;
        if (chkRemPass.Checked == true)
        {
            cook.Values.Add("USEREMAIL", txtEmail.Text.Trim());
            cook.Values.Add("PASSWORD", txtPassword.Text.Trim());

            cook.Expires = DateTime.MaxValue;
            Response.Cookies.Add(cook);

        }
        dt = ObjRegistration.Login();
        if (dt.Rows.Count > 0)
        {

            Session["UserSession"] = dt;
            UserName = dt.Rows[0]["FirstName"].ToString();
            Global.UserEmail = txtEmail.Text;





            ObjBookOrderBal.SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
            ObjBookOrderBal.CustomerID = Convert.ToInt64(dt.Rows[0]["RegistrationID"]);
            ObjBookOrderBal.UpdataeCustomerIDinCart();

            if (Request.Cookies["UserSessionID"]["SessionID"] != null)
            {
                Request.Cookies["UserSessionID"]["SessionID"] = Guid.NewGuid().ToString();
                Request.Cookies.Set(Request.Cookies["UserSessionID"]);
            }


            //Response.Redirect(Request.Url.ToString());
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asd", "alert('User Does not exists.')", true);
            txtEmail.Text = "";
            txtPassword.Text = "";
        }
    }
}