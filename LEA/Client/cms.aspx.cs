using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class Client_cms : System.Web.UI.Page
{
    CmsBAL objCms = new CmsBAL();
    DataTable DT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cmsid"] != null)
            {
                objCms.ID = Convert.ToInt32(Request.QueryString["cmsid"]);
                DataTable dt = objCms.SelectcmsByID();
                if (dt.Rows.Count > 0)
                {
                    Page.Title = dt.Rows[0]["Title"].ToString();
                    content.InnerHtml = dt.Rows[0]["Description"].ToString();
                }
                else
                {
                    homeredirect();
                }
            }
            else
            {
                homeredirect();
            }
        }
    }

    public void homeredirect()
    {
        Response.Redirect("index.aspx");
    }
}