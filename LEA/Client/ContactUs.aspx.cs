using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_ContactUs : System.Web.UI.Page
{
    CmsBAL objCMS = new CmsBAL();
    DataTable ds = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPage();
        }
    }

    #region // Bind Method

    public void BindPage()
    {


        ds = objCMS.SelectAll();
        if (ds.Rows.Count > 0)
        {
            for (int CntRow = 0; CntRow < ds.Rows.Count; CntRow++)
            {
                if (Convert.ToString(ds.Rows[CntRow]["Title"]).Contains("Contact Us"))
                {
                    Page.Title = Convert.ToString(ds.Rows[CntRow]["Title"]);
                    divcms.InnerHtml = Server.HtmlDecode(Convert.ToString(ds.Rows[CntRow]["Description"]));

                    if (Convert.ToString(ds.Rows[CntRow]["Description"]).Trim().Length > 100)
                    {
                        ltlMetaKeyword.Text = "<meta name='keyword' content='" + Convert.ToString(ds.Rows[CntRow]["MetaKeyword"]) + "'>";
                        ltlMetaDescription.Text = "<meta name='keyword' content='" + Convert.ToString(ds.Rows[CntRow]["MetaDescription"]) + "'>";
                    }
                }
            }
        }
        else
        {

        }
    }
    #endregion
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        EmailSender objemail = new EmailSender();
        System.Text.StringBuilder strBody = new System.Text.StringBuilder(General.General.ReadFile(Server.MapPath(Config.EmailTemplate + "Contactus.htm")));
        objemail.ContactUsEmail(txtFullName.Text, txtTelephone.Text, "Contact Us", txtEmail.Text, txtMessage.Text, TxtAddress.Text, strBody);
        ClearData();
        ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('Your details have been submitted successfully.')", true);
    }
    private void ClearData()
    {
        txtEmail.Text = string.Empty;
        txtFullName.Text = string.Empty;
        TxtAddress.Text = string.Empty;
        txtMessage.Text = string.Empty;
        txtTelephone.Text = string.Empty;

    }
}