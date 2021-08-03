using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_WishList : System.Web.UI.Page
{
    HttpCookie cook;
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookPurchaseBAL ObjBookPurchase = new BookPurchaseBAL();
    DataTable dt = new DataTable();
    DataTable dtWishList = new DataTable();
    PagedDataSource adsource;
    int pos;

    public Int64 UserID
    {
        get { return ViewState["UserID"] != null ? Convert.ToInt64(ViewState["UserID"]) : -1; }
        set { ViewState["UserID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            if (Session["UserSession"] != null)
            {

                DataTable dt1 = new DataTable();
                dt1 = Session["UserSession"] as DataTable;
                UserID = Convert.ToInt64(dt1.Rows[0]["RegistrationID"]);

                BindWishList();

            }

            else
            {
                divshow.Visible = true;
                dtBookWishList.Visible = false;
               // ClientScript.RegisterClientScriptBlock(this.GetType(), "asd1", "alert('Please login into your account to see your wishlist.');", true);
            }
          
        }


    }
    public void BindWishList()
    {

      
        ObjBookOrders.CustomerID = UserID;
        dt = ObjBookOrders.SelectCustomerWishList();


        if (dt.Rows.Count > 0)
        {
            dtBookWishList.DataSource = dt;
            dtBookWishList.DataBind();
        }
        else
        {
            dtBookWishList.DataSource = null;
            dtBookWishList.DataBind();
        }
      

        lbltotalIssues.Text = dt.Rows.Count.ToString();
        float price = 0.0f;
        foreach (DataRow dr in dt.Rows)
        {


            price += Convert.ToInt64(dr["Price"]);


        }
        lblTotalPrice.Text = price.ToString();
    }

    protected void dtBookWishList_OnItemCommand(object source, DataListCommandEventArgs e)
    {

        if (e.CommandName == "Del")
        {
            ObjBookOrders.OrderID = Convert.ToInt32(e.CommandArgument);
          
            ObjBookOrders.DeleteItemfromUserWishList();

            BindWishList();
        }
    }
    protected void dtBookWishList_ItemDataBound(object sender, DataListItemEventArgs e)
    {

        float price = 0.0f;
        if (lblTotalPrice.Text == "")
        {
            price = 0.0f;
        }
        else
        {
            price = Convert.ToInt64(lblTotalPrice.Text);
        }
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Label lblprice = e.Item.FindControl("lblprice") as Label;



            price += Convert.ToInt64(lblprice.Text);
        }
        lblTotalPrice.Text = price.ToString();
    }
    protected void btnfirst_Click(object sender, EventArgs e)
    {
        pos = 0;
        BindWishList();
    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["vs"];
        pos -= 1;
        this.ViewState["vs"] = pos;
        BindWishList();
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["vs"];
        pos += 1;
        this.ViewState["vs"] = pos;
        BindWishList();
    }

    protected void btnlast_Click(object sender, EventArgs e)
    {
        pos = adsource.PageCount - 1;
        BindWishList();
    }

  
}