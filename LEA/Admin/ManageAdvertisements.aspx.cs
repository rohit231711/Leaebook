using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Text;


public partial class Admin_ManageAdvertisements : System.Web.UI.Page
{
    AdvertisementsBAL Adv = new AdvertisementsBAL();
    MenuBAL objmenu = new MenuBAL();

    protected string SortColumn
    {
        get
        {
            if (ViewState["SortColumn"] != null)
            {
                return ViewState["SortColumn"].ToString();
            }
            return "AdvertisementID";
        }
        set { ViewState["SortColumn"] = value; }
    }

    protected string SortStatus
    {
        get
        {
            if (ViewState["SortStatus"] != null)
            {
                return ViewState["SortStatus"].ToString();
            }
            return "Desc";
        }
        set { ViewState["SortStatus"] = value; }
    }

    DataTable DT = new DataTable();
    int pageSize = 15;
    int pageIndex = 1;
    int totalRecords;
    Boolean view = false, edit = false, delete = false;
    #region //Events
    protected void Page_Load(object sender, EventArgs e)
    {
        accessrights();
        if (!IsPostBack)
        {          
            DefaultSort();
            BindGrid(pageIndex);
            if (Request.QueryString["add"] == "true")
            {
                Global.Alert(this, "Advertisement added successfully.");
            }
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
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 3)
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

    protected void lkbDelete_Click(object sender, EventArgs e)
    {
        DeleteFlight();
        Global.Alert(this, "Advertisement deleted successfully.");
        accessrights();
        BindGrid(pageIndex);
    }

    protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("AddAdvertisements.aspx?AdvertisementID=" + CommandArgument + "&EDIT=true");
        }
        if (e.CommandName == "Delete1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            Adv = new AdvertisementsBAL();
            Adv.AdvertisementID = CommandArgument;
            Adv.DeleteAdvertisement();
            Adv.AdvertisementID = 0;
            accessrights();
            BindGrid(pageIndex);
            Global.Alert(this, "Advertisement has been deleted.");
        }
        if (e.CommandName == "Sort")
        {
            SortColumn = e.CommandArgument.ToString();
            if (SortStatus == "Desc")
            {
                SortStatus = "Asc";
            }
            else
            {
                SortStatus = "Desc";
            }
            BindGrid(pageIndex);
        }
    }

    protected void gvCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkMember");
            CheckBox chkBxHeader = (CheckBox)this.gvCategory.HeaderRow.FindControl("chkAll");
            chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID);
            LinkButton lkbEdit = (LinkButton)e.Row.FindControl("lkbEdit");
            lkbEdit.Visible = edit;
            LinkButton lkbDelete = (LinkButton)e.Row.FindControl("lkbDelete");
            lkbDelete.Visible = edit;
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //  Image imgAirportShortCode = e.Row.FindControl("imgAirportShortCode") as Image;
            LinkButton lkbCategory = e.Row.FindControl("lkbCategory") as LinkButton;
            //  Image imgFlightNumber = e.Row.FindControl("imgFlightNumber") as Image;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SortColumn"].ToString() == "Title")
                {
                    // imgAirportShortCode.Visible = true;
                    // imgFlightNumber.Visible = false;
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Down.Png";
                        lkbCategory.CssClass = "no-sort";
                    }
                    else
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Up.Png";
                        lkbCategory.CssClass = "no-sort";
                    }
                }
            }
        }
    }

    protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCategory.PageIndex = e.NewPageIndex;
        BindGrid(pageIndex);
    }

    protected void gvCategory_Sorting(object sender, GridViewSortEventArgs e)
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
        //DataView sortedView = new DataView(BindGrid(pageIndex));
        //sortedView.Sort = e.SortExpression + " " + sortingDirection;
        //gvCategory.DataSource = sortedView;
        //gvCategory.DataBind();
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
        if (hndAdv.Value != "")
        {
            Adv.TitleEnglish = hndAdv.Value;
        }
        if (hndAdv1.Value != "")
        {
            Adv.TitleSpanish = hndAdv1.Value;
        }
        Adv.SortColumn = SortColumn;
        Adv.SortStatus = SortStatus;
        dTable = Adv.SelectAllAdvertisementPaging(pageIndex, pageSize);
        gvCategory.DataSource = dTable;
        gvCategory.DataBind();
        totalRecords = this.Adv.totalCount;
        this.PopulatePager(totalRecords, pageIndex);
        //return dTable;
    }

    public void DefaultSort()
    {
        SortColumn = "AdvertisementID";
        SortStatus = "DESC";
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

    protected void statusactive(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in this.gvCategory.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {

                Adv.AdvertisementID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfCategoryID")).Value);
                Adv.ActiveAdvertisement();
            }
        }
        Global.Alert(this, "Advertisement deleted successfully.");
        accessrights();
        BindGrid(pageIndex);
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.BindGrid(pageIndex);
    }

    private void DeleteFlight()
    {
        Adv = new AdvertisementsBAL();
        foreach (GridViewRow gvr in this.gvCategory.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {
                Adv.AdvertisementID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfCategoryID")).Value);
                Adv.DeleteAdvertisement();
            }
        }
        BindGrid(pageIndex);        
    }

    #endregion
}