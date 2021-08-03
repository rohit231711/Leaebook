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
    HttpCookie cook;
    BookBAL ObjBook;
    DataTable dt;
    public string Featured
    {
        get { return ViewState["Featured"] != null ? Convert.ToString(ViewState["Featured"]) : ""; }
        set { ViewState["Featured"] = value; }
    }
    public string BestSeller
    {
        get { return ViewState["BestSeller"] != null ? Convert.ToString(ViewState["BestSeller"]) : ""; }
        set { ViewState["BestSeller"] = value; }
    }
    public string FeaturedID
    {
        get { return ViewState["FeaturedID"] != null ? Convert.ToString(ViewState["FeaturedID"]) : ""; }
        set { ViewState["FeaturedID"] = value; }
    }
    public string BestSellerID
    {
        get { return ViewState["BestSellerID"] != null ? Convert.ToString(ViewState["BestSellerID"]) : ""; }
        set { ViewState["BestSellerID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (Request.QueryString["log"] != null)
            {

                if (Session["UserSession"] != null)
                {
                    Session.Abandon();
                    Session.RemoveAll();
                    Session.Clear();
                }
                if (Request.Cookies["UserSessionID"] != null)
                {
                    cook = new HttpCookie("UserSessionID");
                    Request.Cookies["UserSessionID"].Expires = DateTime.Now.AddDays(-1);
                    Request.Cookies.Add(Request.Cookies["UserSessionID"]);
                    cook.Values.Add("SessionID", Guid.NewGuid().ToString());
                    cook.Expires = DateTime.MaxValue;
                    Response.Cookies.Add(cook);



                }



            }

            BindBooks();

        }
    }
    public void BindBooks()
    {
        ObjBook = new BookBAL();
        ObjBook.WsIsFree = 1;

        ObjBook.CurrentPage = 1;
        ObjBook.Rows = 10000;
        ObjBook.IsPublish = 1;
        dt = ObjBook.BookIssueList();
        string Countsss = dt.Rows.Count.ToString();
        dt.Rows.RemoveAt(dt.Rows.Count - 1);

        dtNewRealeases.DataSource = dt;
        dtNewRealeases.DataBind();
        
    }


}