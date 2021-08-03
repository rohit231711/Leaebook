using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class playvideo : System.Web.UI.Page
{
    VedioBAL objVideo = new VedioBAL();
    DataTable dt = new DataTable();
    public string GetUrl { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["VideoURL"] != null)
            {
                dt = new DataTable();
                objVideo.ID = Convert.ToInt32(Request.QueryString["VideoURL"].ToString());
                dt = objVideo.SelectAllVideo();
                if (dt.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[0]["VideoPath"].ToString()))
                    {

                        GetUrl = "http://themagz.net/Video/" + dt.Rows[0]["VideoPath"].ToString();
                    }

                }

            }
            

        }
    }
}