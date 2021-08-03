using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Cyotek.GhostScript.PdfConversion;
using Cyotek.GhostScript;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net.Mime;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Xml;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Threading;

public partial class Admin_MyAccount : System.Web.UI.Page
{
    DataTable DT = new DataTable();

    RegistrationBAL objUser = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();

    //public int RegistrationID
    //{
    //    get
    //    {
    //        int id = 0;
    //        if (Request.QueryString["RegistrationID"] != null || Request.QueryString["RegistrationID"] != "")
    //        {
    //            id = Convert.ToInt32(Request.QueryString["RegistrationID"]);
    //            return id;
    //        }
    //        return id;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            accessrights();

            if (Session["AdminRegistrationID"] != null)
            {
                objUser = new RegistrationBAL();
                objUser.RegistrationID = Convert.ToInt32(Session["AdminRegistrationID"]);
                DataTable dt = objUser.SelectRegistraionByID();
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtFirstname.Text = dt.Rows[0]["FirstName"].ToString();
                    txtLastname.Text = dt.Rows[0]["LastName"].ToString();
                    txtEmail.Text = dt.Rows[0]["EmailAddress"].ToString();
                    txtUsername.Text = dt.Rows[0]["UserName"].ToString();
                    
                }
            }
        }
    }

    private void accessrights()
    {
        int fl = 0;
        objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
        DT = objmenu.GetRightsByUser();
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 8)
                {
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 1)
                    {
                        fl = 1;
                    }
                }
            }
            if (fl == 0)
            {
                Response.Redirect("accessdenied.aspx");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int a;
        objUser = new RegistrationBAL();
        objUser.FirstName = txtFirstname.Text.Trim();
        objUser.LastName = txtLastname.Text.Trim();
        objUser.EmailAddress = txtEmail.Text.Trim();
        objUser.UserName = txtUsername.Text.Trim();
        objUser.IsActive = true;
        objUser.IsNewsLetter = true;
        objUser.UserType = 1;
        Session["AdminUsername"] = txtUsername.Text.Trim();


        objUser.ActivationID = System.Guid.NewGuid().ToString();

        if (Session["AdminRegistrationID"] != null)
            objUser.RegistrationID = Convert.ToInt32(Session["AdminRegistrationID"]);
        objUser.UpdateRegistration();
        Response.Redirect("Dashboard.aspx");

        
    }

}