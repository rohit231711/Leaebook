using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
public partial class cms : System.Web.UI.Page
{
    CmsBAL objCms = new CmsBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
            {

                objCms.ID = Convert.ToInt32(Request.QueryString["id"]);
                objCms.LanguageID = 1;
                DataTable dt = objCms.SelectcmsByID();

                if (dt != null && dt.Rows.Count > 0)
                {                 
                    Response.Write("<div style='text-align: justify;'>" + dt.Rows[0]["Description"].ToString() + "</div>");
                }
                else
                {
                    Response.Write("No record(s) found.");
                }

            }
        }
    }
}