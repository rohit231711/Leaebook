using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Admin_AddCms : System.Web.UI.Page
{
    CmsBAL objCms = new CmsBAL();
    DataTable DT = new DataTable();
    public static string CMS;
    MenuBAL objmenu = new MenuBAL();
    public int CmsID
    {
        get
        {
            int id = -1;
            if (Request.QueryString["CmsID"] != null && Request.QueryString["CmsID"] != "")
            {
                id = Convert.ToInt32(Request.QueryString["CmsID"]);
                return id;
            }
            return id;
        }
        set
        {
            CmsID = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Form.DefaultButton = btnSubmit.UniqueID;
            accessrights();
            meta.Visible = false;
            if (CmsID > 0)
            {

                objCms.ID = this.CmsID;
                //string[] LanguageID = Request.Form.GetValues("languageid");
                //string[] Title = Request.Form.GetValues("Title");
                DataTable dt = objCms.SelectcmsByID();
                if (dt.Rows.Count > 0)
                {
                    //Title.Text = dt.Rows[0]["Title"].ToString();
                    //CKEditor1.Text= dt.Rows[0]["Description"].ToString();
                    txtMetaDescription.Text = dt.Rows[0]["MetaDescription"].ToString();
                    txtMetaKeyWord.Text = dt.Rows[0]["MetaKeyWord"].ToString();
                    txtMetaTitle.Text = dt.Rows[0]["MetaTitle"].ToString();
                    
                }
            }
            bindCMS();
        }
    }

    private void bindCMS()
    {
        CMS = "";
        LanguageBAL LB = new LanguageBAL();
        DataTable DT = LB.GetLanguage();

        if (DT != null)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                objCms.ID = this.CmsID;
                objCms.LanguageID = Convert.ToInt64(DT.Rows[i]["ID"]);
                DataTable dt = objCms.SelectcmsByID();
                string TitleValue  = "";
                string Description = "";
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    TitleValue = dt.Rows[0]["Title"].ToString();
                    Description = dt.Rows[0]["Description"].ToString();
                }
                CMS += "   <tr class=\"light_bg\"> "
                             + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                             + "         <strong>Title (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                    //+ "         <asp:TextBox CssClass=\"input_box\" ID=\"txtCategoryName\" runat=\"server\"></asp:TextBox> "
                             + " <input type=\"hidden\" name=\"languageid\" value=" + DT.Rows[i]["ID"] + " >"
                             + " <input type=\"text\" name=\"Title\" class=\"input_box user1\" value=\"" + TitleValue + "\" >"
                             + "         <font class=\"required\">*</font> "
                             + "     </td> "
                             + " </tr>" 
                
                              +"  <tr class=\"light_bg\"> "
                             + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                             + "         <strong>Description (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                    //+ "         <asp:TextBox CssClass=\"input_box\" ID=\"txtCategoryName\" runat=\"server\"></asp:TextBox> "
                          //   + " <input type=\"CKEditorControl\" name=\"Description\"  value=\"" + Description + "\" >"
                          + "<textarea cols=\"80\" id=\"Editor1\"  name=\"editor" + (i + 1) + "\" rows=\"10\"  class=\"editor\" Style=\"width: 75%; margin-top: 10px;\"  >" + Description + "</textarea>"
                             + "         <font class=\"required\">*</font> "
                             + "     </td> "
                             + " </tr>";
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
        int a = 0;
        //objCms.Title = txtTitle
        //objCms.Description = Editor1.Text.Trim();
        string[] LanguageID = Request.Form.GetValues("languageid");
        string[] Title = Request.Form.GetValues("Title");
        for (int i = 0; i < LanguageID.Length; i++)
        {
            
            
            string[] editor = Request.Form.GetValues("editor" + (i + 1));
            objCms.Title = Title[i];
            objCms.Description = editor[0];
            objCms.IsActive = true;
            objCms.LanguageID = Convert.ToInt32(LanguageID[i]);
            objCms.MetaDesc = txtMetaDescription.Text;
            objCms.MetaKeyword = txtMetaKeyWord.Text;
            objCms.Metatitle = txtMetaTitle.Text;
            objCms.IsActive = true;
            objCms.ID = CmsID;
            a = objCms.InsertUpdatecrm();
        }
            if (a > 0)
        {
            Response.Redirect("ManageCms.aspx?add=true");
        }

    }


}