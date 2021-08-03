using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;

public partial class Admin_ManageCms : System.Web.UI.Page
{
    CmsBAL objCms = new CmsBAL();
    DataTable DT = new DataTable();
    RegistrationBAL objuser = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
    Boolean view = false, edit = false, delete = false;
    string order = "";
    int pageSize = 15;
    int pageIndex = 1;
    int totalRecords=0;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            accessrights();
            DefaultSorting();
            BindGrid(pageIndex);
            if (Request.QueryString["add"] == "true")
            {

                Global.Alert(this, "Data saved successfully.");

            }
        }
    }
    private void DefaultSorting()
    {
        ViewState["SortStatus"] = "ASC";
    }

    private void BindGrid(int pageIndex)
    {
        order = Convert.ToString(ViewState["SortStatus"]);
        if (objCms.SelectAll(order) != null && objCms.SelectAll(order).Rows.Count > 0)
        {
            gvCms.DataSource = objCms.SelectAll(order);
            DataTable DT = objCms.SelectAll(order);
            gvCms.DataBind();
            //gvCms.DataSource = dTable;

            //gvCategory.DataBind();

            totalRecords = DT.Rows.Count;
            this.PopulatePager(totalRecords, pageIndex);
        }
        
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
        //rptPager.DataSource = pages;
        //rptPager.DataBind();
    }
    private void accessrights()
    {
        objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
        DT = objmenu.GetRightsByUser();
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 7)
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
    protected void gvCms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit1")
        {
            int CommandArgument = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("AddCms.aspx?CmsID=" + CommandArgument + "&EDIT=true");
            accessrights();
        }
        else if (e.CommandName == "sorting")
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
            //accessrights();
            order = ViewState["SortStatus"].ToString();
            DataTable DT = new DataTable();
            DT = objCms.SelectAll(order);
            if (DT != null && DT.Rows.Count > 0)
            {
                gvCms.DataSource = DT;
                gvCms.DataBind();
            }
        }
        else
        {
            accessrights();
            DefaultSorting();
            BindGrid(pageIndex);
            if (Request.QueryString["add"] == "true")
            {

                Global.Alert(this, "Data saved successfully.");

            }
        }
    }
    protected void lkbAdd_Click(object sender, EventArgs e)
    {

    }
    protected void gvCms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate))
        {
            LinkButton lkbEdit = (LinkButton)e.Row.FindControl("lkbEdit");
            lkbEdit.Visible = edit;

            Label lkb = (Label)e.Row.FindControl("lblTitle");
            if (lkb.Text == "Contactinfo")
            {
                e.Row.Visible = false;

            }
           // LinkButton lkbDelete = (LinkButton)e.Row.FindControl("lkbDelete");
           // lkbDelete.Visible = edit;
        }
    }
    protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCms.PageIndex = e.NewPageIndex;
        BindGrid(pageIndex);
    }
    //protected void Page_Changed(object sender, EventArgs e)
    //{
    //    int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
    //    this.BindGrid(pageIndex);
    //}
}