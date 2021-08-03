using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using RKLib.ExportData;
using System.Data;
using System.Text;
using System.Globalization;
using System.Threading;

public partial class Admin_ManageBook : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();


    RegistrationBAL objRegister = new RegistrationBAL();

    MenuBAL objmenu = new MenuBAL();
    DataTable DT = new DataTable();
    int pageSize = 15;
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
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        accessrights();
        if (!IsPostBack)
        {
            hndDdlID.Value = "-1";
            DefaultSort();
            BindGrid(pageIndex);

        }
        //BindCategory();
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
        if (e.CommandName == "Edit1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("AddBookIssue.aspx?BookID=" + CommandArgument + "&EDIT=true");
        }
        if (e.CommandName == "Delete1")
        {

            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            objBook = new BookBAL();
            objBook.BookID = CommandArgument;
          //  objBook.DeleteBook();
            BindGrid(pageIndex);
            Global.Alert(this, "Book has been deleted.");


        }
        if (e.CommandName == "Purchase")
        {
            ImageButton img = (ImageButton)e.CommandSource;
            //string result = img.Attributes["data-isPur"].ToString();
            //if (result == "0")
            //{
            //    Response.Redirect("PurchaseReport.aspx?ID=" + e.CommandArgument.ToString());
            //}
            //else
            //{
                Response.Redirect("SubscribtionReport.aspx?ID=" + e.CommandArgument.ToString());
            //}

        }
        if (e.CommandName == "Subscription")
        {
            Response.Redirect("SubscribtionReport.aspx?ID=" + e.CommandArgument.ToString());
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

            //chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID);

            //LinkButton lkbEdit = (LinkButton)e.Row.FindControl("lkbEdit");
            //lkbEdit.Visible = edit;
            //LinkButton lkbDelete = (LinkButton)e.Row.FindControl("lkbDelete");
            //lkbDelete.Visible = edit;
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
            dir = SortDirection.Ascending;
            sortingDirection = "Asc";
        }

        //DataView sortedView = new DataView(BindGrid());
        //sortedView.Sort = e.SortExpression + " " + sortingDirection;
        //grdCustomer.DataSource = sortedView;
        //grdCustomer.DataBind();
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

        if (!ViewState["datatable"].ToString().Equals(""))
        {
            string filename = "OrderReport_"+DateTime.Now.ToString("dd/MM/yyyy")+".xls";
            DataTable dt = (DataTable)ViewState["datatable"];
            DataTable dtViewstate = dt;
            DataView dv = dt.DefaultView;
           
            dt = dv.ToTable(false, "FirstName", "EmailAddress");
            dt.Columns["FirstName"].ColumnName = "Name";
            DataColumn cl = new DataColumn("Status");
            dt.Columns.Add(cl);

            DataTable dt1 = new DataTable();
            for (int i = 0; i < dtViewstate.Rows.Count; i++)
            {
                ////DataRow dr = dt.NewRow();
               // if (dtViewstate.Rows[i]["IsPurchaser"].ToString() == "1")
                   dt.Rows[i]["Status"] = "Paid";
                //else
                    //dt.Rows[i]["Status"] = "Subscriber";

                //dt1.Rows.Add(dr);
            }

            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export("Web");

            objExport.ExportDetails(dt, Export.ExportFormat.Excel, filename);
        }
    }
    #region //Method
    private void BindGrid(int pageIndex)
    {

        DataTable dTable = new DataTable();

        objRegister.FromDate = HDNDATE1.Value;
        objRegister.ToDate = HDMDATE2.Value;

        objRegister.Publisher = HDNPUBLISHER.Value;
        objRegister.BookTitle = HDNTITLE.Value;
        objRegister.UserName = hndName.Value;
        objRegister.EmailAddress = hndEmail.Value;
        objRegister.SortColumn = ViewState["SortColumn"].ToString();
        objRegister.SortStatus = ViewState["SortStatus"].ToString();
        DataSet ds = objRegister.GetSubscriberAndPurchaseUsers(pageIndex, pageSize, Convert.ToInt32(hndDdlID.Value));

        dTable = ds.Tables[0];
        grdCustomer.DataSource = dTable;
        ViewState["datatable"] = dTable;
        grdCustomer.DataBind();
        totalRecords = this.objBook.totalCount;
        this.PopulatePager(totalRecords, pageIndex);
        hndBooktitle.Value = "";
        hndBooktype.Value = "";
        objBook.Title = "";
        hndEmail.Value = "";
        hndName.Value = "";
        objBook.CategoryID = 0;

        if (ds.Tables.Count > 1 && ds.Tables[1].Rows[0][0].ToString() != "")
        {
            lblPayment.Text = Convert.ToDouble(ds.Tables[1].Rows[0][0]).ToString("0.00");
        }
        else
        {
            lblPayment.Text = 0.ToString("0.00");
        }
        //return dTable;
    }

    public void DefaultSort()
    {
        ViewState["SortStatus"] = "DESC";
        ViewState["SortColumn"] = "UserName";


    }

    private void DeleteFlight()
    {
        objBook = new BookBAL();
        foreach (GridViewRow gvr in this.grdCustomer.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {

                objBook.BookID = Convert.ToInt32(((HiddenField)gvr.FindControl("gdnID")).Value);
                //objBook.DeleteBook();
            }
        }
        BindGrid(pageIndex);
        Global.Alert(this.Page, "Book has been deleted.");
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
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.Type = ddlType.SelectedValue;
    }
}