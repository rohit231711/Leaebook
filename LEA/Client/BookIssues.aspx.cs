using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
public partial class Client_BookIssues : System.Web.UI.Page
{
    BookBAL ObjBook = new BookBAL();
    protected int BookID
    {
        get
        {
            if (ViewState["BookID"] != null)
            {
                return Convert.ToInt32(ViewState["BookID"]);
            }
            return -1;
        }
        set { ViewState["BookID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["magid"] != null)
            {
            BookID = Convert.ToInt32(Request.QueryString["magid"]);
            ObjBook.IsPublish = 1;
            ObjBook.BookID = BookID;
            dtlistBooks.DataSource = ObjBook.BookIssueList();
            dtlistBooks.DataBind();
            }
        }

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