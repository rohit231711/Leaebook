using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_BookDetail : System.Web.UI.Page
{
    BookBAL ObjBook = new BookBAL();
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookPurchaseBAL ObjBookPurchase = new BookPurchaseBAL();
    HttpCookie cook;
    DataTable dt = new DataTable();

    public int BookID
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

    public int CatID
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["CatID"])) ? Convert.ToInt32(ViewState["CatID"]) : 0; }
        set { ViewState["CatID"] = value; }
    }
    public int ID
    {
        get { return Request.QueryString["BookID"] != "" ? Convert.ToInt32(Request.QueryString["BookID"]) : -1; }
        set { ID = value; }
    }

    public string BookName
    {
        get;
        set;
    }

    public string BookSearch
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["is"] != null)
            {
                //   ObjBookOrders.BookID = ID;
                imgbtnBuy.ImageUrl = "images/cart.png";
            }



            if (Request.QueryString["search"] != null)
            {
                //   ObjBookOrders.BookID = ID;
                BookSearch = Request.QueryString["search"].ToString();
            }


            BindBookDetail();
        }

    }
    public void BindBookDetail()
    {

        DataTable dt1 = new DataTable();

        ObjBook.BookID = ID;
        ObjBook.Title = BookSearch;
        ObjBook.IsPublish = 1;
        dt = ObjBook.GetBookDetail();
        if (dt.Rows.Count > 0)
        {
            BookID = Convert.ToInt32(dt.Rows[0]["BookID"]);
            BookName = dt.Rows[0]["Title"].ToString();
            hypBookissues.NavigateUrl = "BookIssues.aspx?magid=" + BookID;
            lblPublisher.Text = dt.Rows[0]["Publisher"].ToString();

            string subPath = "~/Book/" + Convert.ToInt32(BookID);
            Server.MapPath(subPath + "/" + "PublisherLogo.jpg");
            if (System.IO.File.Exists(Server.MapPath(subPath + "/" + "PublisherLogo.jpg")))
            {
                imgPublisher.Visible = true;
                imgPublisher.ImageUrl = subPath + "/" + "PublisherLogo.jpg";
            }
            lblLanguage.Text = dt.Rows[0]["Language"].ToString();
            lblCountry.Text = dt.Rows[0]["countryname"].ToString();
            bigimage.ImageUrl = dt.Rows[0]["TitleImage"].ToString();
            hdnCategoryID.Value = dt.Rows[0]["CategoryID"].ToString();
            CatID = Convert.ToInt32(hdnCategoryID.Value);
            lblCategoryName.Text = dt.Rows[0]["CategoryName"].ToString();
            BindBooks();
            litdetail.Text = dt.Rows[0]["Description"].ToString();
            ancmag.HRef = "Books.aspx?catid=" + hdnCategoryID.Value;
            CurrentPrice.Text =  "  RM " + dt.Rows[0]["singleissueprice"].ToString();
        }
        else
        {
            Response.Redirect("Index.aspx");
        }
        dt1 = ObjBook.GetDescriptionImagesByIssue();
        rptBooks.DataSource = dt1;
        rptBooks.DataBind();


        if (Request.QueryString["is"] != null)
        {
            lblIssuePrice.Text = "price " + dt.Rows[0]["singleissueprice"].ToString() + " RM ";
        }
        else
        {
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["Issues"]) > 1)
                {
                    lblissues.Text = dt.Rows[0]["Issues"].ToString();
                    lblIssuePrice.Text = dt.Rows[0]["IssuesPrice"].ToString();
                    //  imgbtnBuy.ImageUrl = "images/cart.png";
                    long a = 0;
                    //lblpriceperissue.Text = "RM " + Convert.ToString(Math.Round(Convert.ToDecimal(lblIssuePrice.Text.Trim()) / Convert.ToDecimal(lblissues.Text))) + " per issue";
                    lblissues.Text += "  issues for RM";
                }
                else
                {
                    lblissues.Text = dt.Rows[0]["Issues"].ToString();
                    lblIssuePrice.Text = Math.Round(Convert.ToDecimal(dt.Rows[0]["IssuesPrice"]), 2).ToString();
                    imgbtnBuy.ImageUrl = "images/cart.png";
                    long a = 0;
                    // lblpriceperissue.Text = "RM." + Convert.ToString(Math.Round(Convert.ToDecimal(lblIssuePrice.Text.Trim()) / Convert.ToDecimal(lblissues.Text))) + " per issue";
                    lblissues.Text += "  issue for RM";

                }
            }
        }
    }
    public static string GenerateBookURL(string Title, string strId, string folder)
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
        strTitle = folder + strId + ".aspx";
        return strTitle;
    }
    protected void imgbtnBuy_Click(object sender, EventArgs e)
    {

        if (Request.Cookies["UserSessionID"] != null)
        {
            ObjBookOrders.SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
        }

        else
        {
            cook = new HttpCookie("UserSessionID");
            cook.Values.Add("SessionID", Guid.NewGuid().ToString());


            cook.Expires = DateTime.MaxValue;
            Response.Cookies.Add(cook);
            ObjBookOrders.SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();

        }

        if (Session["UserSession"] != null)
        {

            DataTable dt1 = new DataTable();
            dt1 = Session["UserSession"] as DataTable;
            ObjBookOrders.CustomerID = Convert.ToInt64(dt1.Rows[0]["RegistrationID"]);
            //   ObjBookOrders.SessionID = "";
        }
        ObjBookOrders.Amount = Convert.ToString(lblIssuePrice.Text.Replace("price", "").Replace("Rs.", "").Trim());
        ObjBookOrders.OrderDate = DateTime.Now;
        ObjBookOrders.OrderNo = ObjBookOrders.GetMaxOrderNo().Rows[0][0].ToString();
        ObjBookOrders.PaymentStatus = false;
        if (Request.QueryString["is"] != null)
        {
            ObjBookOrders.BookID = ID;
            // imgbtnBuy.ImageUrl = "images/cart.png";
        }
        else
        {
            ObjBookOrders.SubscribedBookID = BookID;
            ObjBookOrders.BookID = ID;
        }

        if (ObjBookOrders.CheckIteminPurchased().Rows[0][0].ToString() == "1")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('This item is already in Purchase list.');window.location.href='" + Request.Url + "'", true);
        }
        else if (ObjBookOrders.CheckDuplicateItemInCart().Rows[0][0].ToString() == "1")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('This item is already in cart.');window.location.href='" + Request.Url + "'", true);
        }
        else
        {
            int CartId = ObjBookOrders.InsertCustomerCart();
            Response.Redirect("Cart.aspx");
        }



    }
    protected void imgbtnSingleBuy_Click(object sender, EventArgs e)
    {

        if (Request.Cookies["UserSessionID"] != null)
        {
            ObjBookOrders.SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
        }

        else
        {
            cook = new HttpCookie("UserSessionID");
            cook.Values.Add("SessionID", Guid.NewGuid().ToString());


            cook.Expires = DateTime.MaxValue;
            Response.Cookies.Add(cook);
            ObjBookOrders.SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();

        }

        if (Session["UserSession"] != null)
        {

            DataTable dt1 = new DataTable();
            dt1 = Session["UserSession"] as DataTable;
            ObjBookOrders.CustomerID = Convert.ToInt64(dt1.Rows[0]["RegistrationID"]);
            //   ObjBookOrders.SessionID = "";
        }
        ObjBookOrders.Amount = Convert.ToString(lblIssuePrice.Text.ToLower().Replace("rs.", "").Replace("price", "").Replace("rm.", "").Trim());
        ObjBookOrders.OrderDate = DateTime.Now;
        ObjBookOrders.OrderNo = ObjBookOrders.GetMaxOrderNo().Rows[0][0].ToString();
        ObjBookOrders.PaymentStatus = false;
        ObjBookOrders.BookID = ID;

        if (ObjBookOrders.CheckDuplicateItemInCart().Rows[0][0].ToString() == "1")
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('This item is already in cart.');window.location.href='" + Request.Url + "'", true);
        }
        else
        {
            int CartId = ObjBookOrders.InsertCustomerCart();
            Response.Redirect("Cart.aspx");
        }



    }
    protected void imgAddtoWishlist_Click(object sender, EventArgs e)
    {



        if (Session["UserSession"] != null)
        {

            DataTable dt1 = new DataTable();
            dt1 = Session["UserSession"] as DataTable;
            ObjBookOrders.CustomerID = Convert.ToInt64(dt1.Rows[0]["RegistrationID"]);
            ObjBookOrders.Amount = Convert.ToString(lblIssuePrice.Text.ToLower().Replace("rs.", "").Replace("price", "").Replace("rm.", "").Trim());
            ObjBookOrders.OrderDate = DateTime.Now;

            ObjBookOrders.BookID = ID;

            if (ObjBookOrders.CheckDuplicateItemInWishList().Rows[0][0].ToString() == "1")
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "asd1", "alert('This item is already in wishlist.');window.location.href='" + Request.Url + "'", true);
            }
            else
            {
                int CartId = ObjBookOrders.InsertCustomerWishList();
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "asd1", "alert('Book is added in wishlist.');window.location.href='" + Request.Url + "'", true);
            }

        }
        else
        {

            ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('Please login into your account.');window.location.href='" + Request.Url + "'", true);
        }




    }
    public void BindBooks()
    {
        BookBAL ObjBooks = new BookBAL();
        ObjBooks.CategoryID = CatID;
        ObjBooks.IsPublish = 1;
        rptNewArrivals.DataSource = ObjBooks.BookIssueList();
        rptNewArrivals.DataBind();



    }
}