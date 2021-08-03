using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_AboutUs : System.Web.UI.Page
{
    CmsBAL ObjCms = new CmsBAL();
    DataTable ds = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPage();
        }
    }

    #region // Bind Method

    public void BindPage()
    {
        ObjCms = new CmsBAL();


        ds = ObjCms.SelectAll();
        if (ds.Rows.Count > 0)
        {
            for (int CntRow = 0; CntRow < ds.Rows.Count; CntRow++)
            {
                if (Convert.ToString(ds.Rows[CntRow]["Title"]).Contains("RefundPolicy"))
                {
                    Page.Title = Convert.ToString(ds.Rows[CntRow]["Title"]);
                    divcms.InnerHtml = Server.HtmlDecode(Convert.ToString(ds.Rows[CntRow]["Description"]));

                    if (Convert.ToString(ds.Rows[CntRow]["Description"]).Trim().Length > 100)
                    {
                        ltlMetaKeyword.Text = "<meta name='keyword' content='" + Convert.ToString(ds.Rows[CntRow]["MetaKeyword"]) + "'>";
                        ltlMetaDescription.Text = "<meta name='keyword' content='" + Convert.ToString(ds.Rows[CntRow]["MetaDescription"]) + "'>";
                    }
                }
            }
        }
        else
        {

        }
    }
    #endregion
}