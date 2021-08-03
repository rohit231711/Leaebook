using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;
public partial class Partner_banner : System.Web.UI.Page
{
    BookBAL BB = new BookBAL();
    DataTable DT = new DataTable();
    Security s = new Security();
    protected int BookID
    {
        get
        {
            if (ViewState["BookID"] != null)
            {
                return Convert.ToInt32(ViewState["BookID"]);
            }
            return 0;
        }
        set { ViewState["BookID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                BookID = Convert.ToInt32(s.Decrypt(Request.QueryString["id"]));
                
                BindData();
                mltBanner.ActiveViewIndex = 0;
            }
        }
    }
  
    public void BindData()
    {
        BB.ID = BookID;
        DT = BB.GetReviewRatting();
        if (DT.Rows.Count > 0)
        {
            GrdList.DataSource = DT;
            GrdList.DataBind();
        }
        else
        {
            GrdList.DataSource = DT;
            GrdList.DataBind();
        }
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mltBanner.ActiveViewIndex = 0;
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        mltBanner.ActiveViewIndex = 1;
    }
    protected void GrdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            DeleteBanner(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == "Edit1")
        {
            //Response.Redirect("banner.aspx?id=" + e.CommandArgument);
        }
    }
    public void DeleteBanner(int ID)
    {
        BB.ID = ID;
        BB.DeleteReviewRatting();
        BindData();
    }
 }