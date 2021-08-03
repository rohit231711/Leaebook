﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;

public partial class Admin_ManageBook : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();

    CategoryBAL objCategory = new CategoryBAL();
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
                Global.Alert(this, "Book added successfully.");
            }
        }
    }

    private void BindDropDownGrid()
    {
        BookBAL objBook = new BookBAL();
        DataTable dt = new DataTable();
        objBook.StartIndex = 1;
        objBook.EndIndex = -1;
        objBook.LanguageID = 1;
        dt = objBook.get_all_book_website1();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < gvBook.Rows.Count; i++)
            {
                var ddl = (DropDownList)gvBook.Rows[i].Cells[8].FindControl("ddlOrderIndexGrid");
                ddl.Items.AddRange(Enumerable.Range(1, dt.Rows.Count).Select(e => new ListItem(e.ToString())).ToArray());
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
    protected void lkbDelete_Click(object sender, EventArgs e)
    {

        if (hndid.Value != "")
        {
            objBook.BookID = Convert.ToInt32(hndid.Value);
            objBook.DeleteBook();
            hndid.Value = "";
            BindGrid(pageIndex);
            Global.Alert(this.Page, "Book has been deleted.");
        }
        else
        {
            DeleteFlight();
        }

    }

    protected void gvBook_RowCommand(object sender, GridViewCommandEventArgs e)
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
            objBook.DeleteBook();
            DeleteFilesFromServerFolder(CommandArgument);
            BindGrid(pageIndex);
            Global.Alert(this, "Book has been deleted.");
        }

        if (e.CommandName == "Issue")
        {
            int i = Convert.ToInt32(e.CommandArgument.ToString());
            Response.Redirect("ReviewRatting.aspx?Id=" + e.CommandArgument.ToString());
        }

        if (e.CommandName == "sort")
        {
            ViewState["SortColumn"] = e.CommandArgument;
            if (ViewState["SortStatus"].ToString() == "Desc")
            {
                ViewState["SortStatus"] = "Desc";
            }
            else
            {
                ViewState["SortStatus"] = "Asc";
            }
            BindGrid(pageIndex);
        }
    }

    private void DeleteFilesFromServerFolder(int bookID)
    {
        try
        {
            DataTable dtBook = new DataTable();
            objBook = new BookBAL();
            objBook.BookID = bookID;

            dtBook = objBook.getBookDetails();
            if (dtBook != null && dtBook.Rows.Count > 0)
            {
                var catId = dtBook.Rows[0]["CategoryID"].ToString();
                string subPath = "~/Book/" + catId;
                bool IsExists = Directory.Exists(Server.MapPath(subPath));

                if (IsExists)
                {
                    string[] resultFiles;

                    string bookpath = Server.MapPath("../Book/" + catId + "/");
                    resultFiles = Directory.GetFiles(bookpath, "*.jpg");

                    for (int iRow = 0; iRow < resultFiles.Length; iRow++)
                    {
                        if (resultFiles[iRow].Contains("Temp_Original" + bookID) || resultFiles[iRow].Contains("Original" + bookID))
                        {
                            string sPath = resultFiles[iRow];
                            if (!resultFiles[iRow].Contains("_1.jpg"))
                                File.Delete(sPath);
                        }
                    }

                    //if (File.Exists(bookpath + bookID + ".pdf"))
                    //{
                    //    File.Delete(bookpath + bookID + ".pdf");
                    //}
                    if (Directory.Exists(bookpath + bookID))
                    {
                        Directory.Delete(bookpath + bookID, true);
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    protected void gvBook_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            CheckBox chkBxSelect = (CheckBox)e.Row.Cells[1].FindControl("chkMember");
            CheckBox chkBxHeader = (CheckBox)this.gvBook.HeaderRow.FindControl("chkAll");

            if (chkBxSelect.Attributes["onclick"] != null)
            {
                chkBxSelect.Attributes["onclick"] = string.Format("javascript:ChildClick(this,'{0}');", chkBxHeader.ClientID);
            }

            LinkButton lkbEdit = (LinkButton)e.Row.FindControl("lkbEdit");
            if (lkbEdit != null)
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
            //BindDropDownGrid();

            BookBAL objBook = new BookBAL();
            //DataTable dt = new DataTable();
            //objBook.StartIndex = 1;
            //objBook.EndIndex = -1;
            //objBook.LanguageID = 1;
            //dt = objBook.get_all_book_website1();

            int count = objBook.GetTotalBooksCount();
            ddl.Items.AddRange(Enumerable.Range(1, count).Select(x => new ListItem(x.ToString())).ToArray());

            ddl.SelectedValue = lblOrderGrid.Text;
        }

        if (e.Row.RowType == DataControlRowType.Header)
        {

            LinkButton lkbCategory = e.Row.FindControl("lkbCategory") as LinkButton;
            LinkButton lkbBook = e.Row.FindControl("lkbBook") as LinkButton;
            LinkButton lkbAuthorName = e.Row.FindControl("lkbAuthorName") as LinkButton;
            LinkButton lkbLanguage = e.Row.FindControl("lkbLanguage") as LinkButton;
            LinkButton lkbFinalPrice = e.Row.FindControl("lkbFinalPrice") as LinkButton;

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

    protected void gvBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBook.PageIndex = e.NewPageIndex;
        BindGrid(pageIndex);
    }

    protected void gvBook_Sorting(object sender, GridViewSortEventArgs e)
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
        //gvBook.DataSource = sortedView;
        //gvBook.DataBind();
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
        if (Session["CategoryID"] != null && Session["CategoryID"].ToString() != string.Empty)
        {
            objBook.CategoryID = Convert.ToInt32(Session["CategoryID"].ToString());
            //Session["CategoryID"] = null;
        }

        if (Session["CategoryID"] != null && Session["CategoryID"].ToString() != string.Empty)
        {
            objBook.CategoryID = Convert.ToInt32(Session["CategoryID"].ToString());
            //Session["CategoryID"] = null;
        }

        if (Session["Publisher"] != null && Session["Publisher"].ToString() != string.Empty)
        {
            objBook.Publisher = Session["Publisher"].ToString();
            //Session["Publisher"] = null;
        }

        if (Session["Title"] != null && Session["Title"].ToString() != string.Empty)
        {
            objBook.Title = Session["Title"].ToString();
            //Session["Title"] = null;
        }

        if (Session["Autoher"] != null && Session["Autoher"].ToString() != string.Empty)
        {
            objBook.Autoher = Session["Autoher"].ToString();
            //Session["Autoher"] = null;
        }

        if (Session["Language"] != null && Session["Language"].ToString() != string.Empty)
        {
            objBook.Language = Session["Language"].ToString();
            //Session["Language"] = null;
        }

        if (Session["FinalPrice"] != null && Session["FinalPrice"].ToString() != string.Empty)
        {
            objBook.FinalPrice = Session["FinalPrice"].ToString();
            //Session["FinalPrice"] = null;
        }

        if (Session["CreatedOn"] != null && Session["CreatedOn"].ToString() != string.Empty)
        {
            objBook.SearchText = Session["CreatedOn"].ToString();
            //Session["CreatedOn"] = null;
        }
        string SortColumn = ViewState["SortColumn"].ToString();
        string SortStatus = ViewState["SortStatus"].ToString();

        dTable = objBook.SelectAllBookPaging(pageIndex, pageSize, SortColumn, SortStatus);


        gvBook.DataSource = dTable;

        gvBook.DataBind();
        totalRecords = this.objBook.totalCount;
        this.PopulatePager(totalRecords, pageIndex);
        hndBooktitle.Value = "";
        hndBooktype.Value = "";
        objBook.Title = "";
        objBook.CategoryID = 0;

        if (Session["CategoryID"] != null || Session["Title"] != null || Session["Autoher"] != null
            || Session["Language"] != null || Session["FinalPrice"] != null || Session["CreatedOn"] != null)
        {
            aSeatChange.Visible = false;
            aClearSearch.Visible = true;
        }
        else
        {
            aSeatChange.Visible = true;
            aClearSearch.Visible = false;
        }

        //return dTable;
    }

    public void DefaultSort()
    {
        ViewState["SortStatus"] = "DESC";
        ViewState["SortColumn"] = "CreatedOn";


    }

    private void DeleteFlight()
    {
        objBook = new BookBAL();
        foreach (GridViewRow gvr in this.gvBook.Rows)
        {
            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
            {

                objBook.BookID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfBookID")).Value);
                objBook.DeleteBook();
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (gvBook.Rows.Count > 0)
        {
            for (int i = 0; i < gvBook.Rows.Count; i++)
            {
                var ddl = (DropDownList)gvBook.Rows[i].Cells[8].FindControl("ddlOrderIndexGrid");
                var lbl = (Label)gvBook.Rows[i].Cells[8].FindControl("lblOrderGrid");
                var txt = (TextBox)gvBook.Rows[i].Cells[9].FindControl("txtQuantity");
                if (!string.IsNullOrEmpty(lbl.ToolTip))
                {
                    BookBAL objBook = new BookBAL();
                    if (!string.IsNullOrEmpty(txt.Text))
                        objBook.UpdateQuantity(txt.Text, lbl.ToolTip);
                    //objBook.UpdateOrderIndex(ddl.SelectedValue, lbl.ToolTip);
                    if (lbl.Text != ddl.SelectedValue)
                        ChangeOrderIndex(lbl.ToolTip, ddl.SelectedValue);
                }
            }
        }
        Response.Redirect("ManageBook.aspx");
    }

    public void ChangeOrderIndex(string bookID, string orderIndex)
    {
        if (Convert.ToInt32(orderIndex) == 1)
        {
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "update book set OrderIndex = OrderIndex + 1 where BookID != " + bookID);
        }
        else if (Convert.ToInt32(orderIndex) > 1)
        {
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "update Book Set OrderIndex = (SELECT OrderIndex from Book where BookID = " + bookID + " ) where BookID = (SELECT BookID from Book where OrderIndex = " + orderIndex + " )");
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "update book set OrderIndex = " + orderIndex + " where BookID = " + bookID + "");
        }
    }

    protected void aClearSearch_Click(object sender, EventArgs e)
    {
        Session["CategoryID"] = null;
        Session["Publisher"] = null;
        Session["Title"] = null;
        Session["Autoher"] = null;
        Session["Language"] = null;
        Session["FinalPrice"] = null;
        Session["CreatedOn"] = null;
        //BindGrid(1);
        Response.Redirect("ManageBook.aspx");
    }
}