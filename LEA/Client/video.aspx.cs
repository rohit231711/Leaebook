using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class Client_video : System.Web.UI.Page
{
    VedioBAL objVideo = new VedioBAL();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dt = new DataTable();
            dt = objVideo.SelectAllVideo();
            if (dt.Rows.Count > 0)
            {
                rptrVideo.DataSource = dt;
                rptrVideo.DataBind();
            }
        }
    }
}