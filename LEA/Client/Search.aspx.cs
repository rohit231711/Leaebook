using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
public partial class Client_Search : System.Web.UI.Page
{
    BookBAL ObjBookBal = new BookBAL();
    public string Title1
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(Request.QueryString["title"])) ? Convert.ToString(Request.QueryString["title"]) : ""; }
        set { ViewState["Title"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBooks();
        }
    }

    public void BindBooks()
    {


        ObjBookBal.Title = Title1;


        dtlistBooks.DataSource = ObjBookBal.BookIssueList();
        dtlistBooks.DataBind();

    }
}