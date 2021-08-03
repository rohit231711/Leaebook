using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class Partner_OrderManagement : System.Web.UI.Page
{
    BookPurchaseBAL objBookPurchase = new BookPurchaseBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindGrid();
        }
    }

    private void BindGrid()
    {
        DataTable dt = new DataTable();
        objBookPurchase.PartnerID = Convert.ToInt32(Session["PartnerRegistrationID"]);
        dt = objBookPurchase.getPendingPartnerBooks();
        gvBookStatus.DataSource = dt;
        gvBookStatus.DataBind();
    }

    protected void gvBookStatus_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            Label lblOrderStatus = (Label)e.Row.FindControl("lblOrderStatus");
            DropDownList ddlOrderStatus = (DropDownList)e.Row.FindControl("ddlOrderStatus");

            ddlOrderStatus.SelectedValue = lblOrderStatus.Text;
        }
    }
    protected void lkbChange_Click(object sender, EventArgs e)
    {
        if (gvBookStatus.Rows.Count > 0)
        {
            for (int i = 0; i < gvBookStatus.Rows.Count; i++)
            {
                var ddl = (DropDownList)gvBookStatus.Rows[i].Cells[6].FindControl("ddlOrderStatus");
                var lbl = (Label)gvBookStatus.Rows[i].Cells[6].FindControl("lblOrderStatus");
                var hfPurchaseID = (HiddenField)gvBookStatus.Rows[i].Cells[2].FindControl("hfPurchaseID");
                if (!string.IsNullOrEmpty(hfPurchaseID.Value))
                {
                    if (lbl.Text != ddl.SelectedValue)
                    {
                        objBookPurchase.UpdateOrderStatus(ddl.SelectedValue, hfPurchaseID.Value);
                        objBookPurchase.InsertOrderEntry(hfPurchaseID.Value, ddl.SelectedValue);
                    }
                }
            }
        }
        Response.Redirect("OrderManagement.aspx");
    }
}