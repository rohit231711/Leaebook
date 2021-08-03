using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_MyLibrary : System.Web.UI.Page
{
    BookBAL ObjBook = new BookBAL();

    public Int32 UserID
    {
        get { return ViewState["Userid"] != null ? Convert.ToInt32(ViewState["Userid"]) : -1; }
        set { ViewState["Userid"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserSession"] != null)
            {
                DataTable dt1 = Session["UserSession"] as DataTable;
                UserID = Convert.ToInt32(dt1.Rows[0]["RegistrationID"]);
                BindBookTitles();
                BindBooks();
                divshow.Visible = false;
            }
            else
            {
                divshow.Visible = true;
                div1.Visible = false;
            }
        }
    }

    public void BindBookTitles()
    {
        ddlAllTitle.DataSource = ObjBook.GetBookIssueTitles();
        ddlAllTitle.DataTextField = "Title";
        ddlAllTitle.DataValueField = "ID";
        ddlAllTitle.DataBind();
    }

    public void BindBooks()
    {
        ObjBook.BookID = Convert.ToInt32(ddlAllTitle.SelectedValue == "" ? "0" : ddlAllTitle.SelectedValue);
        dtlistBooks.DataSource = ObjBook.UserBookList(UserID, 1, 1);
        dtlistBooks.DataBind();
    }

    protected void ddlAllTitle_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBooks();
    }
}