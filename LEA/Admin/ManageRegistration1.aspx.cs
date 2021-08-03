using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Text;

public partial class Admin_ManageRegistration : System.Web.UI.Page
{
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
        accessrights();
        if (!IsPostBack)
        {
          
            DefaultSort();
            BindGrid(pageIndex);

            if (Request.QueryString["add"] == "true")
            {

                Global.Alert(this, "User added successfully.");

            }

            if (Request.QueryString["EDIT"] == "true")
            {

                Global.Alert(this, "User edit successfully.");

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
        Global.Alert(this.Page, "User has been deleted.");
        DeleteFlight();
        Response.Redirect("ManageRegistration.aspx");


    }

    protected void gvRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            // Response.Redirect("javascript:void()");
             Response.Redirect("AddAdmin.aspx?RegistrationID=" + CommandArgument + "&EDIT=true");
        }
        if (e.CommandName == "Delete1")
        {

            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            objRegistration = new RegistrationBAL();
            objRegistration.RegistrationID = CommandArgument;
            objRegistration.DeleteRegistration();
            BindGrid(pageIndex);
            Global.Alert(this.Page, "User has been deleted.");
        }

        //if(e.CommandName=="ViewCart")
        //{
        //    int CommandArgument = Convert.ToInt32(e.CommandArgument);
        //    objRegistration = new RegistrationBAL();
        //    objRegistration.RegistrationID = CommandArgument;
        //    DataTable dts= new DataTable();
        //    dts = objRegistration.viewCartDetails();
        //    //GridView1.DataSource = dts;
        //    //GridView1.DataBind();
        //    // myModal.Visible = true;
        //   // myModal.Visible = true;
        //}

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
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkMember");
            CheckBox chkBxHeader = (CheckBox)this.gvRegistration.HeaderRow.FindControl("chkAll");

            chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID);

            LinkButton lkbEdit = (LinkButton)e.Row.FindControl("lkbEdit");
            if(lkbEdit !=null)
            {
                lkbEdit.Visible = edit;
            }
            
            LinkButton lkbDelete = (LinkButton)e.Row.FindControl("lkbDelete");
            if (lkbDelete != null)
            {
                lkbDelete.Visible = edit;
            }
            Label lblOrderGrid = (Label)e.Row.FindControl("lblOrderGrid");
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlOrderIndexGrid");
            BookBAL objBook = new BookBAL();
            
            int count = objBook.GetTotalBooksCount();
           // ddl.Items.AddRange(Enumerable.Range(1, count).Select(x => new ListItem(x.ToString())).ToArray());

            //ddl.SelectedValue = lblOrderGrid.Text;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            LinkButton lkbFirstName = e.Row.FindControl("lkbFirstName") as LinkButton;
            LinkButton lkbEmailAddress = e.Row.FindControl("lkbEmailAddress") as LinkButton;
            LinkButton lkbRegisteredDate = e.Row.FindControl("lkbRegisteredDate") as LinkButton;
            LinkButton lkbFinalPrice = e.Row.FindControl("lkbFinalPrice") as LinkButton;
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
                if (ViewState["SortColumn"].ToString() == "FirstName")
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
                if (ViewState["SortColumn"].ToString() == "Createddate")
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
        DataTable dTable = new DataTable();


        if (hndname.Value != "")
        {
            objRegistration.UserName = hndname.Value;
        }
        if (hndemail.Value != "")
        {
            objRegistration.EmailAddress = hndemail.Value;
        }
        objRegistration.UserType = 3;
        objRegistration.SortColumn = Convert.ToString(ViewState["SortColumn"]);
        objRegistration.SortStatus = Convert.ToString(ViewState["SortStatus"]);
        objRegistration.ToDate = string.Empty;
        objRegistration.FromDate = string.Empty;

        dTable = objRegistration.SelectAllRegistrationPaging(pageIndex, pageSize);


        gvRegistration.DataSource = dTable;

        gvRegistration.DataBind();


        totalRecords = this.objRegistration.totalCount;
        this.PopulatePager(totalRecords, pageIndex);

    }

    public void DefaultSort()
    {
        ViewState["SortColumn"] = "CreatedDate";
        ViewState["SortStatus"] = "DESC";

    }

    private void DeleteFlight()
    {
        objRegistration = new RegistrationBAL();
        foreach (GridViewRow gvr in this.gvRegistration.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {

                objRegistration.RegistrationID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);
                objRegistration.DeleteRegistration();
            }
        }
        BindGrid(pageIndex);
        Global.Alert(this.Page, "User has been deleted.");
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
    protected void btnActive_Click(object sender, EventArgs e)
    {
        objRegistration = new RegistrationBAL();
        foreach (GridViewRow gvr in this.gvRegistration.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {
                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);
                DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE Registration SET IsActive = 1 WHERE RegistrationID = " + ID);
            }
        }
        BindGrid(pageIndex);
        Global.Alert(this.Page, "User(s) has been active.");
    }
    protected void btnInactive_Click(object sender, EventArgs e)
    {
        objRegistration = new RegistrationBAL();
        foreach (GridViewRow gvr in this.gvRegistration.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {
                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);
                DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE Registration SET IsActive = 0 WHERE RegistrationID = " + ID);
            }
        }
        BindGrid(pageIndex);
        Global.Alert(this.Page, "User(s) has been inactive.");
    }

    
}