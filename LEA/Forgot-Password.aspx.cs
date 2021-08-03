using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;

public partial class Forgot_Password : System.Web.UI.Page
{
    RegistrationBAL objUser = new RegistrationBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtEmail.Value = "";
            txtEmail.Focus();
            this.Form.DefaultButton = btn_login.UniqueID;
        }
    }

    protected void btn_submit(object sender, EventArgs e)
    {
        lbl_error.Text = "";
        BAL_Account Obj_account = new BAL_Account();
        Obj_account.Email = txtEmail.Value;
        DataTable dt = Obj_account.Check_Email_Duplication();
        int emailcount = Convert.ToInt32(dt.Rows[0]["emailcount"].ToString());
        if (emailcount == 0)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Sorry, your Email is not found in our system.');", true);            
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                lbl_error.Text = "Lo sentimos, tu correo electrónico no se encuentra en nuestro sistema.";
            }
            else
            {
                lbl_error.Text = "Sorry, your Email is not found in our system.";
            }
        }
        else
        {
            string Email = txtEmail.Value;
            ForgotPassword(Email);            
        }
    }

    public void ForgotPassword(string EmailAddress)
    {
        objUser.EmailAddress = EmailAddress;

        if (Global.EmailAddressCheck(EmailAddress))
        {
            objUser.EmailAddress = EmailAddress;
            DataTable dt = objUser.ForgotPassword();
            if (dt.Rows.Count > 0 && dt != null)
            {

           //     string body =
           //"         <head>"
           //+ "     <style type=\"text/css\">"
           //+ "         p.MsoNormal "
           //+ "         { "
           //+ "             margin-top: 0in;"
           //+ "             margin-right: 0in; "
           //+ "             margin-bottom: 10.0pt; "
           //+ "             margin-left: 0in;  "
           //+ "             line-height: 115%; "
           //+ "             font-size: 11.0pt; "
           //+ "             font-family: \"Calibri\" , \"sans-serif\"; "
           //+ "         } "
           //+ "     </style> "
           //+ " </head>"
           //+ " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"2\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
           //+ "     <tr> "
           //+ "         <td  style=\"background-color:#001c4a;Font-size:20px; color:White;\"> "
           //         + "             <img src=\"http://203.124.107.14:35/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\"/> "


           //+ "<div style=\"margin-left: 55px; margin-top: -25px;\"><b >LEA eBooks</b></div>"
           // + "</td>"
           //+ "     </tr> "
           //+ "     <tr> "
           //+ "         <td valign=\"top\"> "
           //+ "             <font face=\"Arial, Helvetica, sans-serif\" size=\"2\" color=\"#0000\"> "
           //+ "                 <br /> "
           //+ "                 <span lang=\"EN-IN\"></span><p class=\"MsoNormal\"> "
           //+ "                     <span lang=\"EN-IN\">Your Credentials for LEA eBooks are as below."
           //+ "                         <br /> "
           //+ "                         <br /> "
           //+ "                         <b>Email</b>: " + dt.Rows[0]["EmailAddress"].ToString() + "<br /> "
           //         //+ "                         <b>User Name</b>: " + dt.Rows[0]["UserName"].ToString() + "<br /> "
           //+ "                         <b>Password</b>: " + dt.Rows[0]["Password"].ToString() + "</span></p> "
           //+ "                 <br /> "
           //+ "                 <p class=\"MsoNormal\"> "
           //+ "                     <span lang=\"EN-IN\">Regards,<br /> "
           //+ "                         LEA eBooks</span></p> "
           //+ "                 <br /> "
           //+ "                 <p class=\"MsoNormal\"> "
           //+ "                     <span>NOTE :</span> <span>If you have any questions or further query then please contact us:<br />"
           //+ "                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dt.Rows[0]["ContactEmail"].ToString() + ""
           //+ "                                  </span></p> "
           //+ "                 <br /> "
           //+ "             </font> "
           //+ "         </td> "
           //+ "     </tr> "
           //+ " </table> ";

                string body = string.Empty;
                string file = Server.MapPath("~\\EmailTemplates\\ForgotPassword.htm");
                using (StreamReader reader = new StreamReader(file))
                {
                    body = reader.ReadToEnd();
                }

                body = body.Replace("{EmailAddress}", dt.Rows[0]["EmailAddress"].ToString());
                body = body.Replace("{Password}", dt.Rows[0]["Password"].ToString());
                body = body.Replace("{ContactEmail}", dt.Rows[0]["ContactEmail"].ToString());

                //return body;


                int val = 0;
                System.Text.StringBuilder strBody = new System.Text.StringBuilder(body);

                try
                {
                    Global.SendEmail(EmailAddress, "LEA eBooks | Forgot Password", strBody.ToString());
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Tu cuenta ha sido enviada con éxito a su correo electrónico.');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Your account details has been sent successfully to your Email .');", true);
                    }
                    //Response.Redirect("Forgot-Password.aspx",false);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Dirección de correo electrónico no se encuentra.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Email Address not found.');", true);
                }
                Response.Redirect("Forgot-Password.aspx");
            }
        }
        else
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Dirección de correo electrónico no un formato válido.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Email Address not in valid format.');", true);
            }
            Response.Redirect("Forgot-Password.aspx");
        }
    }
}