using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_Books : System.Web.UI.Page
{

    CategoryBAL ObjCategory = new CategoryBAL();
    BookBAL ObjBook = new BookBAL();
    DataTable dt = new DataTable();
    public string Catname
    {
        get;
        set;
    }
    public string SubCatName
    {
        get;
        set;
    }



    public int CatID
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["CatID"])) ? Convert.ToInt32(ViewState["CatID"]) : 0; }
        set { ViewState["CatID"] = value; }
    }
    public int SubCatID
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["SubCatID"])) ? Convert.ToInt32(ViewState["SubCatID"]) : 0; }
        set { ViewState["SubCatID"] = value; }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        if (!IsPostBack)
        {


            //if (Request.QueryString["subcatid"] != null)
            //{
            //    SubCatID = Convert.ToInt32(Request.QueryString["subcatid"].ToString());
            //    ObjCategory.CategoryID = SubCatID;
            //    dt = ObjCategory.SelectCategoryByID();
            //    if (dt.Rows.Count > 0)
            //    {
            //        SubCatName = dt.Rows[0]["CategoryName"].ToString();
            //        CatID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
            //        ObjCategory.CategoryID = CatID;
            //        Catname = ObjCategory.SelectCategoryByID().Rows[0]["CategoryName"].ToString();

            //        atosub.HRef = "Books.aspx?subcatid=" + SubCatID;
            //        atocat.HRef = "Books.aspx?catid=" + CatID;
            //    }

            //}
            if (Request.QueryString["catid"] != null)
            {
                CatID = Convert.ToInt32(Request.QueryString["catid"].ToString());

                Catname = ObjCategory.SelectAllCategoryPaging(1, 1).Rows[0]["CategoryName"].ToString();
                atocat.HRef = "Books.aspx?catid=" + CatID;
            }
            BindLanguage();
            BindBooks();
        }

        //BindBookCategory();


    }

    public void BindBooks()
    {


        ObjBook.CategoryID = CatID;
        ObjBook.CategoryID = SubCatID; ;
        ObjBook.IsPublish = 1;
        dtlistBooks.DataSource = ObjBook.BookIssueList();
        dtlistBooks.DataBind();

    }
    public void BindLanguage()
    {
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllLanguage();
        if (dt.Rows.Count > 0)
        {
            ddlLanguage.DataSource = dt;
            ddlLanguage.DataTextField = "Language";
            ddlLanguage.DataValueField = "ID";
            ddlLanguage.DataBind();
            ddlLanguage.Items.Insert(0, new ListItem("Select", "0"));
            ddlLanguage.SelectedIndex = 0;
        }
    }
    
    public void ddlLanguage_OnSelectedIndexChanged(object sender, EventArgs e)
    {



        if (CatID != 0)
        {
            ObjBook.CategoryID = CatID;

        }
        if (SubCatID != 0)
        {
            ObjBook.CategoryID = SubCatID; ;
        }
        ObjBook.LangaugeID = Convert.ToInt32(ddlLanguage.SelectedValue);
        ObjBook.IsPublish = 1;
        dtlistBooks.DataSource = ObjBook.BookIssueList();
        dtlistBooks.DataBind();


    }
    #region UrlRewrite
    public static string GenerateBookURL(object Title, object strId, object folder)
    {
        string strTitle = Title.ToString();

        //#region Generate SEO Friendly URL based on Title

        strTitle = strTitle.Trim();
        strTitle = strTitle.Trim('-');

        strTitle = strTitle.ToLower();
        char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
        strTitle = strTitle.Replace("c#", "C-Sharp");
        strTitle = strTitle.Replace("vb.net", "VB-Net");
        strTitle = strTitle.Replace("asp.net", "Asp-Net");
        strTitle = strTitle.Replace(".", "-");
        for (int i = 0; i < chars.Length; i++)
        {
            string strChar = chars.GetValue(i).ToString();
            if (strTitle.Contains(strChar))
            {
                strTitle = strTitle.Replace(strChar, string.Empty);
            }
        }
        strTitle = strTitle.Replace(" ", "-");

        strTitle = strTitle.Replace("--", "-");
        strTitle = strTitle.Replace("---", "-");
        strTitle = strTitle.Replace("----", "-");
        strTitle = strTitle.Replace("-----", "-");
        strTitle = strTitle.Replace("----", "-");
        strTitle = strTitle.Replace("---", "-");
        strTitle = strTitle.Replace("--", "-");
        strTitle = strTitle.Trim();
        strTitle = strTitle.Trim('-');
        // strTitle = "Books/" + strTitle + ".aspx";
        strTitle = folder + strTitle + ".aspx";
        return strTitle;
    }
    #endregion
}