using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using BAL;
using System.Data;
public partial class Admin_Default : System.Web.UI.Page
{
    RegistrationBAL objRegistration = new RegistrationBAL();

    DataTable dt = new DataTable();
    HttpCookie cook;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            {
                txtUsername.Text = Request.Cookies["UserName"].Value;
                txtPassword.Attributes["value"] = Request.Cookies["Password"].Value;
                chklogin.Checked = true;
            }

            lblCopyRights.Text = "Copyright © " + System.DateTime.Now.Year + " Lea";

            if (Request.QueryString["LogOut"] == "1")
            {
                dvalert.Style.Add("display", "");
                lblAlert.Text = "You have successfully logout!";
            }
        }
        txtUsername.Focus();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // if (txtUsername.Text.Trim().ToLower() == "admin" && txtPassword.Text.Trim() == "admin@" + DateTime.Now.AddHours(8).ToString("ddMMyyyy"))
        //if (txtUsername.Text.Trim().ToLower() == "admin" && txtPassword.Text.Trim() == "admin")
        //{
        //    Session["RegistrationID"] = 1;
        //    objRegistration.RegistrationID = 1;
        //    objRegistration.UpdateLogin();
        //    Session["Username"] = txtUsername.Text.Trim();
        //    Response.Redirect("dashboard.aspx");
        //}
        //else if (txtUsername.Text.Trim() != "" && txtPassword.Text.Trim() != "")
        //{
        //    objRegistration.EmailAddress = txtUsername.Text.Trim(); ;
        //    objRegistration.Password = txtPassword.Text.Trim();
        //    DataTable dt = objRegistration.AdminLogin();
        //    if (dt.Rows.Count > 0)
        //    {
        //        objRegistration.RegistrationID = Convert.ToInt64(dt.Rows[0]["RegistrationID"].ToString());
        //        objRegistration.UpdateLogin();
        //        if (Convert.ToInt64(dt.Rows[0]["RegistrationID"].ToString()) != 0)
        //        {
        //            Session["RegistrationID"] = dt.Rows[0]["RegistrationID"].ToString();
        //            Session["Username"] = dt.Rows[0]["EmailAddress"].ToString();
        //            Response.Redirect("dashboard.aspx");
        //        }
        //    }
        //    else
        //    {
        //        dvalert.Style.Add("display", "");
        //        lblAlert.Text = "Enter valid email address or password.";
        //    }
        //}

        //else
        //{
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "fail", "alert('');", true);
        //    dvalert.Style.Add("display", "");
        //    lblAlert.Text = "user name and password does not match.";
        //}

        if (txtUsername.Text != "" && txtPassword.Text != "")
        {
            objRegistration.UserName = txtUsername.Text;
            objRegistration.Password = txtPassword.Text;
            // objRegistration.RegistrationID = 0;
            //dt = objRegistration.LoginValidation();
            dbconnection db = new dbconnection();
            DataTable dt = db.filltable("select * from Registration Where UserType=1 and UserName='"+ txtUsername.Text + "' and Password= '"+txtPassword.Text+ "' ");
            //if(dt.Rows.Count > 0)
            //{
            //    if (dt.Rows[0]["UserType"].ToString() == "1" )
            //    {
            //        Response.Redirect("dashboard.aspx?l=en-US");
            //    }
            //    else 
            //    {
            //        Response.Redirect("dashboard.aspx?l=en-US");
            //    }
            //}
            //else
            //{
            //    lblAlert.Text = "Invalid username or password!";
            //}
            if (dt.Rows.Count > 0)
            {
                Session["AdminRegistrationID"] = Convert.ToInt32(dt.Rows[0]["RegistrationID"]);
                objRegistration.RegistrationID = Convert.ToInt32(Session["AdminRegistrationID"]);
                Session["AdminUsername"] = dt.Rows[0]["UserName"];
                objRegistration.LastLoginUpdate();
                
                
                //if (chklogin.Checked == true)
                //{
                //    cook = new HttpCookie("AdminInfo");
                //    cook.Values.Add("USER_NAME", txtUsername.Text.Trim());
                //    cook.Values.Add("PASSWORD", txtPassword.Text.Trim());
                //    cook.Expires = DateTime.Now.AddMonths(1);
                //    Response.Cookies.Add(cook);
                //}

                if (chklogin.Checked)
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(5);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(5);
                    Response.Cookies["UserName"].Value = txtUsername.Text.Trim();
                    Response.Cookies["Password"].Value = txtPassword.Text.Trim();
                }
                else
                {
                    Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                }

                if (dt.Rows[0]["UserType"].ToString() == "1" || dt.Rows[0]["UserType"].ToString() == "2")
                {
                    Response.Redirect("dashboard.aspx?l=en-US");
                }
                else if (dt.Rows[0]["UserType"].ToString() == "3")
                {
                    Response.Redirect("dashboard.aspx?l=en-US");
                }
            }
            else
            {
                txtUsername.Text = null;
                txtPassword.Text = null;
                dvalert.Visible = true;
                lblAlert.Text = "Invalid username or password!";
                txtUsername.Focus();
            }
        }
        else
        {
            dvalert.Visible = true;
            lblAlert.Text = "Please enter username and password!";
            txtUsername.Focus();
        }
    }


}