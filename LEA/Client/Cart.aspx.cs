using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Configuration;
public partial class Client_Cart : System.Web.UI.Page
{
    HttpCookie cook;
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookPurchaseBAL ObjBookPurchase = new BookPurchaseBAL();
    DataTable dt = new DataTable();
    DataTable dtcart = new DataTable();
    PagedDataSource adsource;
    int pos;
    public string SessionID
    {
        get { return ViewState["UserCartSession"] != null ? Convert.ToString(ViewState["UserCartSession"]) : ""; }
        set { ViewState["UserCartSession"] = value; }
    }

    public Int64 UserID
    {
        get { return ViewState["UserID"] != null ? Convert.ToInt64(ViewState["UserID"]) : -1; }
        set { ViewState["UserID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cook = new HttpCookie("UserSessionID");



            if (Request.Cookies["UserSessionID"] != null)
            {
                SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
            }

            else
            {
                cook = new HttpCookie("UserSessionID");
                cook.Values.Add("SessionID", Guid.NewGuid().ToString());


                cook.Expires = DateTime.MaxValue;
                Response.Cookies.Add(cook);
                SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
            }


            if (Session["UserSession"] != null)
            {

                DataTable dt1 = new DataTable();
                dt1 = Session["UserSession"] as DataTable;
                UserID = Convert.ToInt64(dt1.Rows[0]["RegistrationID"]);

            }


            this.ViewState["vs"] = 0;

            pos = (int)this.ViewState["vs"];

            BindCart();
        }


    }
    public void BindCart()
    {

        ObjBookOrders.SessionID = SessionID;
        ObjBookOrders.CustomerID = UserID;
        dt = ObjBookOrders.SelectCustomerCart();
        if (dt.Rows.Count > 0)
        {
            dtMagagineCart.DataSource = dt;
            dtMagagineCart.DataBind();
        }
        else
        {
            dtMagagineCart.DataSource = null;
            dtMagagineCart.DataBind();
        }



        lbltotalIssues.Text = dt.Rows.Count.ToString();
        double price = 0.0f;
        foreach (DataRow dr in dt.Rows)
        {


            price += Convert.ToDouble(dr["Price"]);


        }
        lblTotalPrice.Text = price.ToString();
    }

    protected void dtBookCart_OnItemCommand(object source, DataListCommandEventArgs e)
    {

        if (e.CommandName == "Del")
        {
            ObjBookOrders.OrderID = Convert.ToInt32(e.CommandArgument);
            ObjBookOrders.SessionID = SessionID;
            ObjBookOrders.DeleteItemfromUserCart();

            BindCart();
        }
    }
    protected void dtMagagineCart_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        double price = 0.0f;
        if (lblTotalPrice.Text == "")
        {
            price = 0.0f;
        }
        else
        {
            price = Convert.ToDouble(lblTotalPrice.Text);
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblprice = e.Item.FindControl("lblprice") as Label;



            price += Convert.ToDouble(lblprice.Text);
        }
        lblTotalPrice.Text = price.ToString();
    }
    protected void btnfirst_Click(object sender, EventArgs e)
    {
        pos = 0;
        BindCart();
    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["vs"];
        pos -= 1;
        this.ViewState["vs"] = pos;
        BindCart();
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["vs"];
        pos += 1;
        this.ViewState["vs"] = pos;
        BindCart();
    }

    protected void btnlast_Click(object sender, EventArgs e)
    {
        pos = adsource.PageCount - 1;
        BindCart();
    }

    protected void btnCheckout_Click(object sender, EventArgs e)
    {

        if (Session["UserSession"] == null)
        {

            ClientScript.RegisterStartupScript(this.GetType(), "asd", "alert('Please login into your account');window.location.href='Index.aspx'", true);

        }
        else
        {

            ObjBookPurchase.SessionID = SessionID;
            long orderno = Convert.ToInt64(DateTime.Now.ToString("yyMMddhhmm") + UserID.ToString());//Convert.ToInt64(UserID.ToString() + DateTime.Now.ToString("ddMMyyyyhhmm"));//Global.RandomNumber(111111111, 999999999);
            ObjBookPurchase.OrderID = orderno;
            ObjBookPurchase.PurchaseDate = DateTime.Now;
            ObjBookPurchase.UserID = UserID;
            ObjBookPurchase.Domain = "themagz";
            ObjBookPurchase.Vcode = CreateRandomCode(32);//System.Guid.NewGuid().ToString();// "";
            int pid = ObjBookPurchase.InsertBookPurchase();
            //orderno = Convert.ToInt64(orderno.ToString() + pid.ToString());
            string vcode = CreateRandomCode(32);//Security.md5Hash(lblTotalPrice.Text + "themagz" + orderno + "7d5d89abbec6fc2c13411b0ed01565bb");
            Response.Redirect("https://www.onlinepayment.com.my/NBepay/pay/test5584/?amount=" + lblTotalPrice.Text + "&orderid=" + orderno.ToString() + "&returnurl=\"" + ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Client/payment.aspx?userid=" + Global.RegistrationID + "\"&Sessionid='" + SessionID + "'" + "&vcode=" + vcode + "&bill_email=" + Global.UserEmail);
        }
    }
    public string CreateRandomCode(int codeCount)
    {
        string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        string[] allCharArray = allChar.Split(',');
        string randomCode = "";
        int temp = -1;

        Random rand = new Random();
        for (int i = 0; i < codeCount; i++)
        {
            if (temp != -1)
            {
                rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
            }
            int t = rand.Next(36);
            if (temp != -1 && temp == t)
            {
                return CreateRandomCode(codeCount);
            }
            temp = t;
            randomCode += allCharArray[t];
        }
        return randomCode;
    }
}