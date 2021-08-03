using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Configuration;

public partial class Admin_WebsiteSettings : System.Web.UI.Page
{

    WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
    DataTable DT = new DataTable();
    MenuBAL objmenu = new MenuBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFaceBook.Focus();
            this.Form.DefaultButton = btnSubmit.UniqueID;
            accessrights();
            DT = WSB.GetAllWebseetings();

            txtBaseURL.Text = ConfigurationManager.AppSettings["BaseURL"].ToString();
            //Global.Alert(this, "Updated Successfully");

            if (DT != null && DT.Rows.Count > 0)
            {
                ViewState["WebSettingID"] = Convert.ToInt32(DT.Rows[0]["WebSettingID"]);
                txtFaceBook.Text = DT.Rows[0]["FaceBookLink"].ToString();
                txtGoogle.Text = DT.Rows[0]["GoogleLink"].ToString();
                txtInstagram.Text = DT.Rows[0]["Instagram_Link"].ToString();
                txtPinterest.Text = DT.Rows[0]["Pinterest_Link"].ToString();
                txtTwiter.Text = DT.Rows[0]["TwiterLink"].ToString();
                txtContactus.Text = DT.Rows[0]["ContactUs"].ToString();
                txtBookStorePhone.Text = DT.Rows[0]["BookStorePhone"].ToString();
            }

            if (Request.QueryString["Edit"] != null && Request.QueryString["Edit"].ToString() != "")
            {
                Global.AlertNew(this, "Updated Successfully");
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
        if (ViewState["WebSettingID"] != null)
        {
            WSB.WebSettingID = Convert.ToInt32(ViewState["WebSettingID"]);
            WSB.FaceBookLink = txtFaceBook.Text;
            WSB.GoogleLink = txtGoogle.Text;
            WSB.Instagram_Link = txtInstagram.Text;
            WSB.Pinterest_Link = txtPinterest.Text;
            WSB.TwiterLink = txtTwiter.Text;
            WSB.ContactUs = txtContactus.Text;
            WSB.BookStorePhone = txtBookStorePhone.Text;
            WSB.UpdateWebsettings();
        }
        ConfigurationManager.AppSettings["BaseURL"] = txtBaseURL.Text;

        Response.Redirect("WebsiteSettings.aspx?Edit=true");
    }
}