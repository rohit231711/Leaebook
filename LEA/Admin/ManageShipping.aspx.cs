using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageShipping : System.Web.UI.Page
{
    ShippingBAL objShipping = new ShippingBAL();

    int pageSize = 20;
    int pageIndex = 1;
    int totalRecords;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            DefaultSort();
            BindGrid(pageIndex);

            if (Request.QueryString["add"] == "true")
            {
                Global.Alert(this, "Shipper added successfully.");
            }
        }

    }

    protected void lkbDelete_Click(object sender, EventArgs e)
    {
        if (hndid.Value != "")
        {
            objShipping.ShipperID = Convert.ToInt32(hndid.Value);
            objShipping.deleteByPk();
            hndid.Value = "";
        }
        else
        {
            DeleteShipper();
        }
        objShipping.ShipperID = 0;
        BindGrid(pageIndex);
    }

    protected void gvShipping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("Shipping.aspx?ID=" + CommandArgument + "&EDIT=true");
        }
        if (e.CommandName == "Delete1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            objShipping = new ShippingBAL();
            objShipping.ShipperID = CommandArgument;
            objShipping.deleteByPk();
            BindGrid(pageIndex);
            Global.Alert(this, "Shipper has been deleted.");
        }
    }

    protected void btnsearchbtn_Click(object sender, EventArgs e)
    {
        BindGrid(pageIndex);
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindGrid(pageIndex);
    }
    
    private void BindGrid(int pageIndex)
    {
        DataTable dTable = new DataTable();
        dTable = objShipping.SelectShippingCharge();
        gvShipping.DataSource = dTable;
        gvShipping.DataBind();
    }

    public void DefaultSort()
    {
        ViewState["SortStatus"] = "Asc";
        ViewState["SortColumn"] = "Title";


    }
    private void DeleteShipper()
    {
        objShipping = new ShippingBAL();
        foreach (GridViewRow gvr in this.gvShipping.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {

                objShipping.ShipperID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfID")).Value);

                if (hndst.Value == "1")
                {
                    objShipping.deleteByPk();
                    objShipping.ShipperID = 0;
                }
                else
                {
                    objShipping.deleteByPk();
                    objShipping.ShipperID = 0;
                }
            }
        }

        BindGrid(pageIndex);
        Global.Alert(this.Page, "Shipper has been deleted.");
    }

}