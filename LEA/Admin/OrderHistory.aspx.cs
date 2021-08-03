using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OrderHistory : System.Web.UI.Page
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
        dt = objBookPurchase.getDeliveredBooks(txtSearch.Text.Trim(), txtStartDate.Text.Trim(), txtEndDate.Text.Trim());
        gvBookStatus.DataSource = dt;
        gvBookStatus.DataBind();
    }

    protected void gvBookStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBookStatus.PageIndex = e.NewPageIndex; ;
        BindGrid();
    }

    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        LinkButton lnkbtn = sender as LinkButton;
        string filePath = "~/ShippingFiles/" + lnkbtn.CommandArgument.ToString() + ".pdf";
        if (System.IO.File.Exists(Server.MapPath(filePath)))
        {
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + lnkbtn.Text + ".pdf\"");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
        }
        else
        {
            Global.Alert(this.Page, "File Not Exists.");
        }
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
                    Price = Convert.ToDouble(row["Amount"]).ToString(),
                    Subtotal = Convert.ToDouble(row["Subtotal"]).ToString(),
                    Address = row["StreetAddress"].ToString() + "\n" + (row["Landmark"].ToString() != "" ? (row["Landmark"].ToString() + "\n") : "")
                            + row["City"].ToString() + " - " + row["Pincode"].ToString() + "\n" + row["State"].ToString() + "\n" +
                            row["Country"].ToString() + "\n" + row["PhoneNumber"].ToString(),
                    ShippingCharge = row["ShippingCharge"].ToString().Trim('$').Trim('"').Trim()
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
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}