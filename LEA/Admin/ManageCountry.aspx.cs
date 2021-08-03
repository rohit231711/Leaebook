using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageCountry : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();
    Country objCountry = new Country();
    MenuBAL objmenu = new MenuBAL();
    DataTable DT = new DataTable();

    CategoryBAL objCategory = new CategoryBAL();
    
    int pageSize = 15;
    int pageIndex = 1;
    int totalRecords;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DefaultSort();
            BindGrid();
            //BindDropDownGrid();

            if (Request.QueryString["add"] == "true")
            {

                Global.Alert(this, "Country added successfully.");

            }
            //BindCategoryAndCategory();

        }
    }

    protected string SortColumn
    {
        get
        {
            if (ViewState["SortColumn"] != null)
            {
                return ViewState["SortColumn"].ToString();
            }
            return "CategoryID";
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

    public void DefaultSort()
    {
        SortColumn = "Name";
        SortStatus = "";
    }

    private void BindGrid()
    {
        string SortColumn = ViewState["SortColumn"].ToString();
        string SortStatus = ViewState["SortStatus"].ToString();

        var dTable = objCountry.SelectAllCountry();

        var tblFiltered = dTable.Select("countryname like '%" + txtSearch.Text + "%'").ToList();
        if (tblFiltered.Count != 0)
        {
            dTable = tblFiltered.CopyToDataTable();
        }
        else
        {
            dTable = null;
        }

        gvCountry.DataSource = dTable;
        gvCountry.DataBind();
        
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
    }

    //public void DefaultSort()
    //{
    //    ViewState["SortStatus"] = "DESC";
    //    ViewState["SortColumn"] = "CreatedOn";
    //}
    
    protected void gvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCountry.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("AddCountry.aspx?CountryID=" + CommandArgument + "&EDIT=true");
        }

       
        //if (e.CommandName == "Delete1")
        //{
        //    int CommandArgument = Convert.ToInt32(e.CommandArgument);
        //    objBook = new BookBAL();
        //    objBook.BookID = CommandArgument;
        //    objBook.DeleteBook();
        //    DeleteFilesFromServerFolder(CommandArgument);
        //    BindGrid();
        //    Global.Alert(this, "Book has been deleted.");
        //}
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}