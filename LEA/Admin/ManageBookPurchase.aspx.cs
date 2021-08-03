using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using RKLib.ExportData;
using System.Globalization;
using System.Threading;

public partial class Admin_ManageBookPurchase : System.Web.UI.Page
{
    BookPurchaseBAL objbook = new BookPurchaseBAL();
    RegistrationBAL objRegistration = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
    DataTable DT = new DataTable();
    int pageSize = 15;
    int pageIndex = 1;
    int totalRecords;
    Boolean view = false, edit = false, delete = false;
    #region //Events


    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
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
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 6)
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

    protected void gvRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
    {
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
            accessrights();
            BindGrid(pageIndex);
        }
    }

    protected void gvRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
    {      
        if (e.Row.RowType == DataControlRowType.Header)
        {
            LinkButton lkbFirstName = e.Row.FindControl("lkbFirstName") as LinkButton;
            LinkButton lkbEmailAddress = e.Row.FindControl("lkbEmailAddress") as LinkButton;
            LinkButton lkbRegisteredDate = e.Row.FindControl("lkbRegisteredDate") as LinkButton;
            //  Image imgFlightNumber = e.Row.FindControl("imgFlightNumber") as Image;
            if (e.Row.RowType == DataControlRowType.Header)
            {

                if (ViewState["SortColumn"].ToString() == "EmailAddress")
                {
                    // imgAirportShortCode.Visible = true;
                    // imgFlightNumber.Visible = false;

                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Down.Png";

                        lkbEmailAddress.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Up.Png";
                        lkbEmailAddress.CssClass = "table-header-repeat-down";
                    }
                }
                if (ViewState["SortColumn"].ToString() == "name")
                {
                    // imgAirportShortCode.Visible = true;
                    // imgFlightNumber.Visible = false;

                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Down.Png";

                        lkbFirstName.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Up.Png";
                        lkbFirstName.CssClass = "table-header-repeat-down";
                    }
                }
                if (ViewState["SortColumn"].ToString() == "PurchaseDate")
                {
                    // imgAirportShortCode.Visible = true;
                    // imgFlightNumber.Visible = false;

                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Down.Png";

                        lkbRegisteredDate.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Up.Png";
                        lkbRegisteredDate.CssClass = "table-header-repeat-down";
                    }
                }
            }
        }
    }

    protected void gvRegistration_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRegistration.PageIndex = e.NewPageIndex;
        BindGrid(pageIndex);
    }

    protected void gvRegistration_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (dir == SortDirection.Ascending)
        {
            dir = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        //DataView sortedView = new DataView(BindGrid());
        //sortedView.Sort = e.SortExpression + " " + sortingDirection;
        //gvRegistration.DataSource = sortedView;
        //gvRegistration.DataBind();
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
                ViewState["dirState"] = SortDirection.Descending;
            }
            return (SortDirection)ViewState["dirState"];
        }
        set
        {
            ViewState["dirState"] = value;
        }
    }
    #endregion

    protected void imgbtnexcel_Click(object sender, EventArgs e)
    {

            string filename = "BookPurchaseReport_" + DateTime.Now.ToString("dd/MM/yyyy") + ".xls";
            objbook.UserType = 3;
            objbook.SortColumn = "FirstName";
            objbook.SortStatus = "ASC";
            objbook.PageIndex = 1;
            objbook.PageSize = 1;
            DataSet ds_table = objbook.get_BookPurchase_Excel();
            DataTable dTable = ds_table.Tables[0];
            DataTable dt_count = ds_table.Tables[1];

            DataView dv = dTable.DefaultView;

            dTable = dv.ToTable(false, "name", "EmailAddress", "eBookName", "CategoryName", "Amount", "PurchaseDate");
            dTable.Columns["name"].ColumnName = "Name";
            dTable.Columns["EmailAddress"].ColumnName = "Email";
            dTable.Columns["eBookName"].ColumnName = "eBook Title";
            dTable.Columns["CategoryName"].ColumnName = "Category";
            dTable.Columns["Amount"].ColumnName = "Price";
            dTable.Columns["PurchaseDate"].ColumnName = "Purchase Date";
            

            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");

            objExport.ExportDetails(dTable, Export.ExportFormat.Excel, filename);
        
    }

    #region //Methods

    private void BindGrid(int pageIndex)
    {
        DataTable dTable = new DataTable();
        if (hndname.Value != "")
        {
            objbook.name = hndname.Value;
        }
        if (hndemail.Value != "")
        {
            objbook.email = hndemail.Value;
        }
        if (hndeBookName.Value != "")
        {
            objbook.eBookName = hndeBookName.Value;
        }
        if (hndCategoryName.Value != "")
        {
            objbook.CategoryName = hndCategoryName.Value;
        }
        if (hndAmount.Value != "")
        {
            objbook.amount = hndAmount.Value;
        }
        if (hndFromdate.Value != "")
        {
            objbook.FromDate = hndFromdate.Value;
        }
        if (hndTodate.Value != "")
        {
            objbook.ToDate = hndTodate.Value;
        }

        objbook.UserType = 3;
        objbook.SortColumn = Convert.ToString(ViewState["SortColumn"]);
        objbook.SortStatus = Convert.ToString(ViewState["SortStatus"]);
        objbook.PageIndex = pageIndex;
        objbook.PageSize = pageSize;
        DataSet ds_table = objbook.get_BookPurchase();
        dTable = ds_table.Tables[0];
        DataTable dt_count = ds_table.Tables[1];
        //lblPayment.Text = Convert.ToDouble(dt_count.Rows[0]["total"]).ToString("0.00");

        if (dTable.Rows.Count > 1 && dTable != null)
        {
            lblPayment.Text = Convert.ToDouble(dt_count.Rows[0]["total"]).ToString("0.00");
        }
        else
        {
            lblPayment.Text = 0.ToString("0.00");
        }
        //DataSet dTable = objbook.get_BookPurchase();
        gvRegistration.DataSource = dTable;
        ViewState["datatable"] = dTable;
        gvRegistration.DataBind();


        totalRecords = Convert.ToInt32(dt_count.Rows[0]["totalrow"].ToString());
        this.PopulatePager(totalRecords, pageIndex);
           
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

    public void DefaultSort()
    {
        ViewState["SortColumn"] = "PurchaseDate";
        ViewState["SortStatus"] = "DESC";
    }  
    
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.BindGrid(pageIndex);
    }
    #endregion
}