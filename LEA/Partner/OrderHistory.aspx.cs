using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Partner_OrderHistory : System.Web.UI.Page
{
    BookPurchaseBAL objBookPurchase = new BookPurchaseBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        DataTable dt = new DataTable();
        objBookPurchase.PartnerID = Convert.ToInt32(Session["PartnerRegistrationID"]);
        dt = objBookPurchase.getDeliveredPartnerBooks();
        gvBookStatus.DataSource = dt;
        gvBookStatus.DataBind();
    }

    protected void gvBookStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBookStatus.PageIndex = e.NewPageIndex; ;
        BindGrid();
    }
}