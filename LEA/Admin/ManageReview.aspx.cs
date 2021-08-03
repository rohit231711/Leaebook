using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Text;
using System.Web.Services;

public partial class Admin_ManageReview : System.Web.UI.Page
{
    BookBAL BL = new BookBAL();
    MenuBAL objmenu = new MenuBAL();
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

    protected void lkbDelete_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in this.gvReview.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {
                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfReviewID")).Value);
                DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE ReviewRatting SET IsDeleted = 1 WHERE ReviewID = " + ID);
            }
        }

        BindGrid(pageIndex);
        Global.Alert(this.Page, "Review has been deleted.");        
        Response.Redirect("ManageReview.aspx");


    }

    protected void gvReview_RowCommand(object sender, GridViewCommandEventArgs e)
    {        
        if (e.CommandName == "Delete1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);            
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE ReviewRatting SET IsDeleted = 1 WHERE ReviewID = " + CommandArgument);
            BindGrid(pageIndex);
            Global.Alert(this.Page, "Review has been deleted.");
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
            accessrights();
            BindGrid(pageIndex);
        }
    }

    protected void gvReview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkMember");
            CheckBox chkBxHeader = (CheckBox)this.gvReview.HeaderRow.FindControl("chkAll");

            chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID);
                        
            LinkButton lkbDelete = (LinkButton)e.Row.FindControl("lkbDelete");
            lkbDelete.Visible = edit;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            LinkButton lkbTitle = e.Row.FindControl("lkbTitle") as LinkButton;
            LinkButton lkbFirstName = e.Row.FindControl("lkbFirstName") as LinkButton;
            LinkButton lkbRatting = e.Row.FindControl("lkbRatting") as LinkButton;
            LinkButton lkbCreatedDate = e.Row.FindControl("lkbCreatedDate") as LinkButton;
            LinkButton lkbApprove = e.Row.FindControl("lkbApprove") as LinkButton;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["SortColumn"].ToString() == lkbTitle.CommandArgument)
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbTitle.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        lkbTitle.CssClass = "table-header-repeat-down";
                    }
                }
                else if (ViewState["SortColumn"].ToString() == lkbFirstName.CommandArgument)
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbFirstName.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        lkbFirstName.CssClass = "table-header-repeat-down";
                    }
                }
                else if (ViewState["SortColumn"].ToString() == lkbRatting.CommandArgument)
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbRatting.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        lkbRatting.CssClass = "table-header-repeat-down";
                    }
                }
                else if (ViewState["SortColumn"].ToString() == lkbCreatedDate.CommandArgument)
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbCreatedDate.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        lkbCreatedDate.CssClass = "table-header-repeat-down";
                    }
                }
                else if (ViewState["SortColumn"].ToString() == lkbApprove.CommandArgument)
                {
                    if (ViewState["SortStatus"].ToString() == "Desc")
                    {
                        lkbApprove.CssClass = "table-header-repeat-up";
                    }
                    else
                    {
                        lkbApprove.CssClass = "table-header-repeat-down";
                    }
                }
            }
        }
    }

    protected void gvReview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReview.PageIndex = e.NewPageIndex;
        BindGrid(pageIndex);
    }

    protected void gvReview_Sorting(object sender, GridViewSortEventArgs e)
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
        //gvReview.DataSource = sortedView;
        //gvReview.DataBind();
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

    #region //Methods
    private void BindGrid(int pageIndex)
    {        
        DataSet ds = BL.SelectAllReview(pageIndex, pageSize, txtsearch.Text.Trim(), Convert.ToString(ViewState["SortColumn"]), Convert.ToString(ViewState["SortStatus"]));

        gvReview.DataSource = ds.Tables[0];
        gvReview.DataBind();        

        totalRecords = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
        this.PopulatePager(totalRecords, pageIndex);

    }

    public void DefaultSort()
    {
        ViewState["SortColumn"] = "RR.CreatedDate";
        ViewState["SortStatus"] = "DESC";

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

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in this.gvReview.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {
                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfReviewID")).Value);
                DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE ReviewRatting SET Approve = 1 WHERE ReviewID = " + ID);
            }
        }
        BindGrid(pageIndex);
        Global.Alert(this.Page, "Review(s) has been approved.");
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gvr in this.gvReview.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {
                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfReviewID")).Value);
                DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE ReviewRatting SET Approve = 0 WHERE ReviewID = " + ID);
            }
        }
        BindGrid(pageIndex);
        Global.Alert(this.Page, "Review(s) has been rejected.");
    }

    [WebMethod]
    public static List<Data> ViewReviewDetails(string ReviewId)
    {
        BookBAL BL = new BookBAL();
        DataTable dt = new DataTable();

        dt = BL.SelectReviewById(Convert.ToInt32(ReviewId));        

        List<Data> data = new List<Data>();

        data = (from DataRow row in dt.Rows
                select new Data
                {
                    Title = row["Title"].ToString(),
                    Name = row["Name"].ToString(),
                    Summary = row["Summary"].ToString(),
                    Review = row["Review"].ToString(),
                    Ratting =row["Ratting"].ToString(),
                    CreatedDate = row["CreatedDate"].ToString() ,
                    Approve = Convert.ToInt32(row["Approve"]).ToString()
                }).ToList();

        return data;
    }

    public class Data
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Summary { get; set; }
        public string Review { get; set; }
        public string Ratting { get; set; }
        public string CreatedDate { get; set; }
        public string Approve { get; set; }
    }


    protected void Search_Click(object sender, EventArgs e)
    {
        BindGrid(pageIndex);
    }

    protected void Cancel_Click(object sender, EventArgs e)
    {
        txtsearch.Text = string.Empty;
        BindGrid(pageIndex);
    }
}