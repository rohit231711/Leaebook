using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using RKLib.ExportData;

public partial class Admin_Order_Report : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();
    BookPurchaseBAL objBookOrder = new BookPurchaseBAL();
    RegistrationBAL objRegister = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
    DataTable DT = new DataTable();
    int pageSize = 20;
    int pageIndex = 1;
    int totalRecords;
    Boolean view = false, edit = false, delete = false;

    public string Type
    {
        get
        {
            if (ViewState["Type"] != null)
            {
                return (string)ViewState["Type"];
            }
            return "-1";
        }
        set
        {
            ViewState["Type"] = value;
        }
    }

    #region //Events

    protected void Page_Load(object sender, EventArgs e)
    {
        accessrights();
        if (!IsPostBack)
        {       
            DefaultSort();
            BindGrid(pageIndex);
        }      
    }

    private void accessrights()
    {
        objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
        DT = objmenu.GetRightsByUser();
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 5)
                {
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 1)
                    {
                        edit = true;
                    }
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 2)
                    {

                        delete = true;
                    }
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 3)
                    {
                        view = true;
                    }
                }
            }
        }
    }

    protected void grdCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Order")
        {
            Response.Redirect("CustomerBookOrderReport.aspx?ID=" + e.CommandArgument.ToString());                                
        }

        if (e.CommandName == "Sort")
        {
            ViewState["SortColumn"] = e.CommandArgument;
            if (ViewState["SortStatus"].ToString() == "Desc")
            {
                ViewState["SortStatus"] = "Asc";
            }
            else
            {
                ViewState["SortStatus"] = "Desc";
            }
            BindGrid(pageIndex);
        }
    }

    protected void grdCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkMember");
            CheckBox chkBxHeader = (CheckBox)this.grdCustomer.HeaderRow.FindControl("chkAll");          
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            LinkButton lkbCategory = e.Row.FindControl("lkbCategory") as LinkButton;
            LinkButton lkbBook = e.Row.FindControl("lkbBook") as LinkButton;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SortColumn"].ToString() == "CategoryName")
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbCategory.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        lkbCategory.CssClass = "table-header-repeat-down";
                    }
                }
                if (ViewState["SortColumn"].ToString() == "Title")
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbBook.CssClass = "no-sort";
                    }
                    else
                    {
                        lkbBook.CssClass = "no-sort";
                    }
                }
                if (ViewState["SortColumn"].ToString() == "CategoryName")
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbCategory.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        lkbCategory.CssClass = "table-header-repeat-down";
                    }
                }
            }
        }
    }

    protected void grdCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdCustomer.PageIndex = e.NewPageIndex;
        BindGrid(pageIndex);
    }

    protected void grdCustomer_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Descending;
            sortingDirection = "DESC";
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

    #endregion

    #region //Property

    public SortDirection dir
    {
        get
        {
            if (ViewState["dirState"] == null)
            {
                ViewState["dirState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }

    #endregion
    
    #region //Method

    private void BindGrid(int pageIndex)
    {
        DataTable dTable = new DataTable();

        if (hndName.Value != "")
        {
            objBookOrder.name = hndName.Value;
        }
        if (hndEmail.Value != "")
        {
            objBookOrder.email = hndEmail.Value;
        }
        objBookOrder.SortColumn = Convert.ToString(ViewState["SortColumn"]);
        objBookOrder.SortStatus = Convert.ToString(ViewState["SortStatus"]);
        objBookOrder.PageIndex = pageIndex;
        objBookOrder.PageSize = pageSize;

        DataSet ds = objBookOrder.get_CustomerDetailByOrder();

        dTable = ds.Tables[0];
        grdCustomer.DataSource = dTable;
        ViewState["datatable"] = dTable;
        grdCustomer.DataBind();

        DataTable dt_count = ds.Tables[1];
        if (dt_count.Rows.Count > 0)
        {
            totalRecords = Convert.ToInt32(dt_count.Rows[0]["TotalRow"].ToString());
            this.PopulatePager(totalRecords, pageIndex);
        }
    }

    public void DefaultSort()
    {
        ViewState["SortStatus"] = "DESC";
        ViewState["SortColumn"] = "FirstName";
    }

    private void PopulatePager(int recordCount, int currentPage)
    {
        double dblPageCount = (double)((decimal)recordCount / decimal.Parse(pageSize.ToString()));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        List<ListItem> pages = new List<ListItem>();
        if (pageCount > 0)
        {
            pages.Add(new ListItem("First", "1", currentPage > 1));
            if (currentPage == 1 && pageCount <= 10)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            else if (currentPage == 1 && pageCount > 10)
            {
                for (int i = 1; i <= 10; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            else if (currentPage != 1 && pageCount <= 10)
            {
                for (int i = 1; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            else if (currentPage != 1 && pageCount > 10 && currentPage != pageCount)
            {
                int z = currentPage - 5;
                int x, y;
                if (z < 1)
                {
                    z = 1;
                    for (int i = z; i <= 10; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
                else if (z >= 1)
                {
                    if (pageCount < currentPage + 4)
                    {
                        y = (z - ((currentPage + 4) - pageCount));
                        x = pageCount;
                    }
                    else
                    {
                        x = currentPage + 4;
                        y = z;
                    }
                    for (int i = y; i <= x; i++)
                    {
                        pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                    }
                }
            }
            else if (currentPage == pageCount)
            {
                for (int i = pageCount - 9; i <= pageCount; i++)
                {
                    pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
                }
            }
            pages.Add(new ListItem("Last", pageCount.ToString(), currentPage < pageCount));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.BindGrid(pageIndex);
    }

    #endregion  
}