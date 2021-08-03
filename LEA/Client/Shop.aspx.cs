using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_Default : System.Web.UI.Page
{
    BookBAL ObjBook;

    CategoryBAL ObjCatgory = new CategoryBAL();
    DataTable dt;
    protected int TotalRows
    {
        get
        {
            if (ViewState["Rows"] != null)
            {
                return Convert.ToInt32(ViewState["Rows"]);
            }
            return -1;
        }
        set { ViewState["Rows"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            BindTop10Maagazines();
            BindNewMaagazines();
            BindFeaturedMaagazines();
            BindBookCategory();

        }

    }
    public void BindBookCategory()
    {

        rptcategory.DataSource = ObjCatgory.SelectAllCartegory();

        rptcategory.DataBind();
    }
    public void BindTop10Maagazines()
    {
        ObjBook = new BookBAL();
        dt = ObjBook.SelectTop10Books();
        TotalRows = dt.Rows.Count;
        if (dt != null && dt.Rows.Count > 0)
        {
            pnlTopten.Visible = true;
            rptBooklist.DataSource = dt;
            rptBooklist.DataBind();
        }
        else
        {
            pnlTopten.Visible = false;
        }
        dt = null;
    }
    public void BindFeaturedMaagazines()
    {

        ObjBook = new BookBAL();
        ObjBook.IsFeatured = 1;
        ObjBook.IsPublish = 1;
        dt = ObjBook.BookIssueList();
        TotalRows = dt.Rows.Count;

        rptFeatured.DataSource = dt;
        rptFeatured.DataBind();
        dt = null;
    }
    public void BindNewMaagazines()
    {
        ObjBook = new BookBAL();
        ObjBook.IsNewArrival = 1;
        ObjBook.IsPublish = 1;
        dt = ObjBook.BookIssueList();
        TotalRows = dt.Rows.Count;

        rptNewArrivals.DataSource = dt;
        rptNewArrivals.DataBind();
        dt = null;
    }


    #region UrlRewrite
    public static string GenerateBookURL(object Title, object strId, object folder)
    {
        string strTitle = Title.ToString();

        strTitle = strTitle.Trim();
        strTitle = strTitle.Trim('-');

        strTitle = strTitle.ToLower();
        char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();

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