using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class Includes_banner : System.Web.UI.UserControl
{
    BannerBAL BB = new BannerBAL();
    DataTable DT = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now);
        Response.Cache.SetNoServerCaching();
        Response.Cache.SetNoStore();
        if (!IsPostBack)
        {
            BindData();
        }
    }

    public void BindData()
    {
        DT = BB.BannerList();
        if (DT.Rows.Count > 0)
        {
            rptRecords1.DataSource = DT;
            rptRecords1.DataBind();
        }
        else
        {
            rptRecords1.DataSource = DT;
            rptRecords1.DataBind();
        }
    }
}