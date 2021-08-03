using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Globalization;

public partial class Admin_OrderManagement : System.Web.UI.Page
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
        dt = objBookPurchase.getPendingBooks(txtSearch.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
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

    protected void gvBookStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBookStatus.PageIndex = e.NewPageIndex; ;
        BindGrid();
    }

    [WebMethod]
    public static List<Data> ViewOrderDetails(string OrderId)
    {
        BookPurchaseBAL objBookPurchase = new BookPurchaseBAL();        
        DataSet ds = new DataSet();

        ds = objBookPurchase.ViewPendingBooks(OrderId);
        DataTable dt = ds.Tables[0];

        List<Data> data = new List<Data>();

        data = (from DataRow row in dt.Rows
                select new Data
                {
                    Title = row["Title"].ToString(),
                    Quantity = row["Qauntity"].ToString(),
                    Type = row["BookType"].ToString(),
                    Price = row["Amount"].ToString(),
                    //Price = Convert.ToDouble(decimal.Parse(row["Amount"].ToString().Replace(".", ","), NumberStyles.AllowDecimalPoint)),
                    Subtotal = Convert.ToDouble(row["Subtotal"]).ToString().Replace(",","."),
                    Address = row["StreetAddress"].ToString() + "\n" + (row["Landmark"].ToString() != "" ? (row["Landmark"].ToString() + "\n") : "")
                            + row["City"].ToString() + " - " + row["Pincode"].ToString() + "\n" + row["State"].ToString() + "\n" +
                            row["Country"].ToString() + "\n" + row["PhoneNumber"].ToString(),
                    ShippingCharge = row["ShippingCharge"].ToString().Replace(",", ".").Trim('$').Trim('"').Trim(),
                    ShippingType = row["ShippingType"].ToString()
                }).ToList();

        return data;                                                              
    }

    public class Data
    {
        public string Title { get; set; }
        public string Quantity { get; set; }
        public string Type { get; set; }
        public string Price { get; set; }
        public string Subtotal { get; set; }
        public string Address { get; set; }
        public string ShippingCharge { get; set; }
        public string ShippingType { get; set; }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}