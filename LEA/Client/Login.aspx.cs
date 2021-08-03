using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_Login : System.Web.UI.Page
{
    RegistrationBAL ObjRegistration = new RegistrationBAL();
    DataTable dt = new DataTable();
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    HttpCookie cook;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (HttpContext.Current.Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["USEREMAIL"] != null && Request.Cookies["UserInfo"]["PASSWORD"] != null)
                {
                    txtEmail.Text = HttpContext.Current.Request.Cookies["UserInfo"]["USEREMAIL"].ToString();
                    txtPassword.Attributes.Add("Value", Request.Cookies["UserInfo"]["PASSWORD"].ToString());
                }
            }
        }

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


        if (dt.Rows.Count > 0 && dt.Rows[0]["IsActive"].ToString() == "1")
        {

            Session["UserSession"] = dt;
            //    UserName = dt.Rows[0]["FirstName"].ToString();
            Global.UserEmail = txtEmail.Text;



            if (Request.Cookies["UserSessionID"] != null)
            {

                ObjBookOrderBal.SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
            }
            ObjBookOrderBal.CustomerID = Convert.ToInt64(dt.Rows[0]["RegistrationID"]);
            ObjBookOrderBal.UpdataeCustomerIDinCart();
            HttpContext.Current.Session["RegistrationID"] = Convert.ToInt32(dt.Rows[0]["RegistrationID"]);

            if (Request.Cookies["UserSessionID"] != null)
            {
                Request.Cookies["UserSessionID"]["SessionID"] = Guid.NewGuid().ToString();
                Request.Cookies.Set(Request.Cookies["UserSessionID"]);
            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "CloseWindow", "facyboxClose();", true);

            //Response.Redirect(Request.Url.ToString());
        }
        else
        {
            if (dt.Rows.Count > 0 && dt.Rows[0]["IsActive"].ToString() == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asd", "alert('Your account is not activated yet so first activate your account by clicking on the activation link which has been sent to your email.')", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "asd", "alert('The username or password you entered is incorrect.')", true);//User Does not exists.
            }
            txtEmail.Text = "";
            txtPassword.Text = "";
        }
    }
}