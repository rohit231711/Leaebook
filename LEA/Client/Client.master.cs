using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Web.Services;
public partial class Client_Client : System.Web.UI.MasterPage
{

    RegistrationBAL ObjRegistration = new RegistrationBAL();
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    BookBAL ObjBookBal = new BookBAL();
    CategoryBAL ObjCatgory = new CategoryBAL();

    DataTable dt = new DataTable();
    HttpCookie cook;
    public string SessionID
    {
        get { return ViewState["UserCartSession"] != null ? Convert.ToString(ViewState["UserCartSession"]) : ""; }
        set { ViewState["UserCartSession"] = value; }
    }
    public string UserName
    {
        get { return ViewState["UserName"] != null ? Convert.ToString(ViewState["UserName"]) : ""; }
        set { ViewState["UserName"] = value; }
    }
    public string Tags
    {
        get { return ViewState["Tags"] != null ? Convert.ToString(ViewState["Tags"]) : ""; }
        set { ViewState["Tags"] = value; }
    }
    public int CartCount
    {
        get { return ViewState["CartCount"] != null ? Convert.ToInt32(ViewState["CartCount"]) : 0; }
        set { ViewState["CartCount"] = value; }
    }

    public Int64 UserID
    {
        get { return ViewState["UserID"] != null ? Convert.ToInt64(ViewState["UserID"]) : -1; }
        set { ViewState["UserID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["RegistrationID"] != null)
        {
            if (Convert.ToInt32(Session["RegistrationID"]) > 0)
            {
                lilogin.Visible = false;
            }
        }
        if (!IsPostBack)
        {

            if (Session["UserSession"] != null)
            {
                DataTable dt = Session["UserSession"] as DataTable;
                UserName = dt.Rows[0]["FirstName"].ToString();

                lilogin.Visible = false;

            }
            BindSpecialBooks();
            BindBookCategory();
        }
        BindTags();
        CountInCart();
        BindBanner();

        spShop.Attributes.Remove("class");
        spexplore.Attributes.Remove("class");
        sphome.Attributes.Remove("class");
        splib.Attributes.Remove("class");
        spwish.Attributes.Remove("class");
        aShop.Attributes.Remove("class");
        aexplore.Attributes.Remove("class");
        ahome.Attributes.Remove("class");
        alib.Attributes.Remove("class");
        awish.Attributes.Remove("class");


        if (Request.Url.AbsolutePath.Contains("Explore"))
        {
            spexplore.Attributes.Add("class", "active");
            aexplore.Attributes.Add("class", "active");
        }
        if (Request.Url.AbsolutePath.Contains("Index"))
        {
            sphome.Attributes.Add("class", "active");

            ahome.Attributes.Add("class", "active");
        }
        if (Request.Url.AbsolutePath.Contains("MyLibrary"))
        {
            splib.Attributes.Add("class", "active");

            alib.Attributes.Add("class", "active");
        }
        if (Request.Url.AbsolutePath.Contains("WishList"))
        {
            spwish.Attributes.Add("class", "active");
            awish.Attributes.Add("class", "active");
        }
        if (Request.Url.AbsolutePath.Contains("Shop"))
        {
            aShop.Attributes.Add("class", "active");
            spShop.Attributes.Add("class", "active");
        }


    }
    public void BindTags()
    {
        dt = ObjBookBal.GetSearchTags();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        int random = 0;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Random rnd = new Random();
            random = rnd.Next(20);
            sb.Append("<a style='float:left' href='Search.aspx?title=" + dt.Rows[i]["tagname"].ToString() + "'");
            sb.Append(" rel='" + dt.Rows[i]["Rep"].ToString() + "'>");
            sb.Append(dt.Rows[i]["tagname"].ToString());
            sb.Append("</a>");


        }
        Tags = sb.ToString();

    }
    protected void lnkWish_Click(object sender, EventArgs e)
    {

        if (Session["UserSession"] == null)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Please Login into your account.')", true);
        }
        else
        {
            Response.Redirect("WishList.aspx");
        }
    }
    public void BindSpecialBooks()
    {

        ObjBookBal = new BookBAL();
        ObjBookBal.IsSpecial = 1;
        ObjBookBal.IsPublish = 1;
        ObjBookBal.CurrentPage = 1;
        ObjBookBal.Rows = 100;
        dt = ObjBookBal.BookIssueList();
        if (dt.Rows.Count > 0)
        {
            rptSpecial.DataSource = dt;
            rptSpecial.DataBind();
        }

    }

    public void CountInCart()
    {
        cook = new HttpCookie("UserSessionID");



        if (Request.Cookies["UserSessionID"] != null)
        {
            SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
        }

        if (Session["UserSession"] != null)
        {

            DataTable dt1 = new DataTable();
            dt1 = Session["UserSession"] as DataTable;
            UserID = Convert.ToInt64(dt1.Rows[0]["RegistrationID"]);

        }

        ObjBookOrderBal.SessionID = SessionID;
        ObjBookOrderBal.CustomerID = UserID;
        dt = new DataTable();
        //  dtcart = ObjBookOrders.GetissueCountInCart();
        dt = ObjBookOrderBal.SelectCustomerCart();
        CartCount = dt.Rows.Count;
    }

    public void BindBanner()
    {
        BannerBAL objBennerBL = new BannerBAL();
        DataTable dsTable = new DataTable();
        dsTable = objBennerBL.BannerList();
        if (dsTable.Rows.Count > 0)
        {

            System.Text.StringBuilder strBuild = new System.Text.StringBuilder();

            for (int cntRow = 0; cntRow < dsTable.Rows.Count; cntRow++)
            {
                strBuild.Append("<a>");
                strBuild.Append("<img src='" + "../Banner/" + dsTable.Rows[cntRow]["ImagePath"].ToString() + "' alt='#htmlcaption'  />");
                strBuild.Append("</a>");
            }

            Image1 = strBuild.ToString();
        }
    }

    public void BindBookCategory()
    {

        rptcategory.DataSource = ObjCatgory.SelectAllCartegory();

        rptcategory.DataBind();
    }
    protected void imgAddtoWishlist_Click(object sender, EventArgs e)
    {







    }

    protected string Image1 { get; set; }

    #region UrlRewrite

    #endregion

    protected void rptSpecial_OnItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "Wish")
        {


            if (Session["UserSession"] != null)
            {

                DataTable dt1 = new DataTable();

                dt1 = Session["UserSession"] as DataTable;
                ObjBookOrderBal.CustomerID = Convert.ToInt64(dt1.Rows[0]["RegistrationID"]);
                ObjBookOrderBal.Amount = (e.Item.FindControl("hdnPrice") as HiddenField).Value;
                ObjBookOrderBal.OrderDate = DateTime.Now;

                ObjBookOrderBal.BookID = Convert.ToInt64(e.CommandArgument.ToString());

                if (ObjBookOrderBal.CheckDuplicateItemInWishList().Rows[0][0].ToString() == "1")
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "asd1", "alert('This item is already in wishlist.');window.location.href='" + Request.Url + "'", true);
                }
                else
                {
                    int CartId = ObjBookOrderBal.InsertCustomerWishList();
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "asd1", "alert('Book is added in wishlist.');window.location.href='" + Request.Url + "'", true);
                }

            }
            else
            {

                Response.Redirect("WishList.aspx");

                //  Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "asd", "alert('Please login into your account.');window.location.href='" + Request.Url + "'", true);
            }
        }
    }
    protected void btnFaceBook_Click(object sender, ImageClickEventArgs e)
    {
        string app_id = "216438618538882";// "
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "asd1", "$.fancybox({ 'titleShow': false,'type': 'iframe','href': '" +
            string.Format("https://www.facebook.com/dialog/feed?app_id={0}&display=popup&caption=An%20example%20caption&link={1}&redirect_uri={2}",
                app_id, Request.Url.AbsoluteUri, Request.Url.AbsoluteUri) + "','height': '300px','transitionIn': 'elastic','transitionOut': 'elastic','hideOnOverlayClick': false });", true);
    }

}
