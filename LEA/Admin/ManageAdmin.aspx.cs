﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Text;

public partial class Admin_ManageAdmin : System.Web.UI.Page
{
    RegistrationBAL objRegistration = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
   

    DataTable DT = new DataTable();
    int pageSize = 20;
    int pageIndex = 1;
    int totalRecords;
    Boolean view = false, edit = false, delete = false;
    #region //Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            accessrights();
            DefaultSort();
            BindGrid(pageIndex);

            if (Request.QueryString["add"] == "true")
            {

                Global.Alert(this, "User added successfully.");


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
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 8)
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
        Global.Alert(this, "User deleted successfully.");

    }

    protected void gvRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
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
        }
        if (e.CommandName == "Rights")
        {
            Response.Redirect("accessrights.aspx?id=" + e.CommandArgument);
               
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
            lkbEdit.Visible = edit;
            LinkButton lkbDelete = (LinkButton)e.Row.FindControl("lkbDelete");
            lkbDelete.Visible = edit;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            //  Image imgAirportShortCode = e.Row.FindControl("imgAirportShortCode") as Image;
            LinkButton lkbEmailAddress = e.Row.FindControl("lkbEmailAddress") as LinkButton;
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

                        lkbEmailAddress.CssClass = "no-sort";
                    }
                    else
                    {
                        //imgAirportShortCode.ImageUrl = "~/App_Themes/default/images/Up.Png";
                        lkbEmailAddress.CssClass = "no-sort";
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
       

        objRegistration.UserType = 2;
        dTable = objRegistration.SelectAllRegistrationPaging(pageIndex, pageSize);
        totalRecords = this.objRegistration.totalCount;

        gvRegistration.DataSource = dTable;

        gvRegistration.DataBind();

        totalRecords = this.objRegistration.totalCount;
        this.PopulatePager(totalRecords, pageIndex);

    }

    public void DefaultSort()
    {
        ViewState["SortColumn"] = "EmailAddress";
        ViewState["SortStatus"] = "Asc";

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
}