using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class CMS_Content : System.Web.UI.Page
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
                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
                {
                    if (Request.QueryString["l"].ToString() == "es-ES")
                    {
                        objCms.LanguageID = 2;
                    }
                    else if (Request.QueryString["l"].ToString() == "en-US")
                    {
                        objCms.LanguageID = 1;
                    }
                }
                DataTable dt = objCms.SelectcmsByID();

                if (dt != null && dt.Rows.Count > 0)
                {
                    
                   //lblaboutus1.Text = dt.Rows[0]["Title"].ToString();
                   this.Title = dt.Rows[0]["Title"].ToString();
                   title.Text = dt.Rows[0]["Title"].ToString();
                   cmsname.Text = dt.Rows[0]["Title"].ToString();
                   string str =  dt.Rows[0]["Description"].ToString();
                   div_content.InnerHtml = str;
                }
                else
                {
                    Response.Write("No record(s) found.");
                }
            }
        }
    }
}