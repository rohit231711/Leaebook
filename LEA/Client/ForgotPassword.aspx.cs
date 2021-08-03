using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_ForgotPassword : System.Web.UI.Page
{
    RegistrationBAL ObjRegistrationBal = new RegistrationBAL();
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ForgotPassword(txtEmail.Text);
    }
    public void ForgotPassword(string EmailAddress)
    {
        

        if (Global.EmailAddressCheck(EmailAddress))
        {
            ObjRegistrationBal.EmailAddress = EmailAddress;
            DataTable dt = ObjRegistrationBal.ForgotPassword();
            if (dt.Rows.Count > 0 && dt != null)
            {
                 string body = "";
                 body = body + "<table>";
                 body = body + "<tr>";
                 body = body + "<td>Dear User, ";
                 body = body + "</td>";
                 body = body + "</tr>";
                 body = body + "<tr>";
                 body = body + "<td> Your Credentials for theMagz.net are as below. ";
                 body = body + "</td>";
                 body = body + "</tr>";
                 body = body + "<tr>";
                 body = body + "<td> URL: " + Config.WebSiteMain + "/Client/Index.aspx";
                 body = body + "</td>";
                 body = body + "</tr>";
                 body = body + "<tr>";
                 body = body + "<td> User Name : " + dt.Rows[0]["EmailAddress"].ToString() + " ";
                 body = body + "</td>";
                 body = body + "</tr>";
                 body = body + "<tr>";
                 body = body + "<td> User Password : " + dt.Rows[0]["Password"].ToString() + " ";
                 body = body + "</td>";
                 body = body + "</tr>";
                 body = body + "<tr>";
                 body = body + "<td>Thanks & Regards,";
                 body = body + "</td>";
                 body = body + "</tr>";
                 body = body + "<tr>";
                 body = body + "<td>theMagz.net";
                 body = body + "</td>";
                 body = body + "</tr>";
                 body = body + "</table>";
                 try
                 {
                     Global.SendEmail(EmailAddress, "Forgot Password", body);
                    // ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('Your Password is  Send successfully.')", true);
                    txtEmail.Text = "";
                    lblMessage.Text = "Your password is sent successfully";

                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                lblMessage.Text = "User does not exist.";
            }
        }



    }
}