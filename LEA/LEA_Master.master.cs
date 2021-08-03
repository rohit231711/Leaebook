using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LEA_Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string host = Request.Url.GetLeftPart(UriPartial.Path);
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;
      
        if (!IsPostBack)
        {
            bindGlobalCat();
            //if (Session["BlobalCatID"] != null)
            //{
            //    drpglobalcat.SelectedValue = Session["BlobalCatID"].ToString();

            //}
            //if (Session["BlobalTextSearch"] != null)
            //{
            //    txt_Globalsearch.Text = Session["BlobalTextSearch"].ToString();
            //}        
            if (Session["UserName"] == null && sRet != "Login.aspx")
            {
                switch (sRet)
                {
                    case "Account.aspx":
                    case "MyLibrary.aspx":
                        Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                        break;
                }
            }
        }
    }

    public void bindGlobalCat()
    {
        var category = "All Categories";//todas las categorias
        DataTable dt = new DataTable();
        Category_Locale objCat_Locate = new Category_Locale();
        int language = 1;
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                language = 2;
                category = "todas las categorias";
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                language = 1;
                category = "All Categories";
            }
        }
        dt = objCat_Locate.Get_Category_Locale_leftmenu(language);
        if (dt.Rows.Count > 0)
        {
            drpglobalcat.DataSource = dt;
            drpglobalcat.DataTextField = "CategoryName";
            drpglobalcat.DataValueField = "CategoryID";
            drpglobalcat.DataBind();

            ListItem li = new ListItem(category, "0");
            drpglobalcat.Items.Insert(0, li);            
        }
    }
    protected void btn_search(object sender, EventArgs e)
    {
        Session["BlobalCatID"] = drpglobalcat.SelectedValue.ToString();
        Session["BlobalTextSearch"] = txt_Globalsearch.Text.ToString();
        drpglobalcat.SelectedIndex = 0;
        txt_Globalsearch.Text = "";
        Response.Redirect(Config.WebSiteMain + "AllBooks.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        
    }
}
