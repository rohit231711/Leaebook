using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;

public partial class Admin_ManageOrder : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindgrid();
        }
    }
    private void bindgrid()
    {
        DataTable dt = objBook.SelectBookOrder();
        if (dt.Rows.Count > 0)
        {
            gvOrderList.DataSource = dt;
            gvOrderList.DataBind();
        }
        else
        {
            gvOrderList.DataSource = null;
            gvOrderList.DataBind();
        }
    }
    protected void gvOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void gvOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != null)
        {
            switch (e.CommandName)
            {
                case "Edit1":

                    break;
                case "View1":
                    break;

                case "Approve":
                    objBook.ID = Convert.ToInt32(e.CommandArgument);
                    objBook.OrderApprove();
                    bindgrid();
                    break;
            }
        }
    }
}