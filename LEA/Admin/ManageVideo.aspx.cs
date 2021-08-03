using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BAL;


public partial class Admin_ManageVideo : System.Web.UI.Page
{
    VedioBAL objvideo = new VedioBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindgrid();
        }
    }
    private void bindgrid()
    {
        DataTable dt = objvideo.SelectAllVideo();
        if (dt.Rows.Count > 0)
        {
            gvvideo.DataSource = dt;
            gvvideo.DataBind();
        }
        else
        {
            gvvideo.DataSource = null;
            gvvideo.DataBind();
        }
    }
    protected void lkbDelete_Click(object sender, EventArgs e)
    {

        if (hndvalue.Value != "")
        {
            string subPath = "~/Video";
            objvideo.ID = Convert.ToInt32(hndvalue.Value);
            DataTable dt = objvideo.SelectAllVideo();
            if (dt.Rows.Count > 0)
            {
                File.Delete(Server.MapPath(subPath + "/" + dt.Rows[0]["VideoPath"].ToString()));
            }
            objvideo.ID = Convert.ToInt32(hndvalue.Value);
            objvideo.DeleteVideo();
            objvideo.ID = 0;
            Global.Alert(this, "Video deleted successfully.");
            bindgrid();
        }
    }
}