using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;

public partial class Blog_Detail : System.Web.UI.Page
{
    DataTable dsTable;
    BlogBAL objBlog = new BlogBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        BindData();
    }

    private void BindData()
    {       
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
        {
            objBlog.LanguageID = 2;
        }
        else
        {
            objBlog.LanguageID = 1;
        }

        objBlog.BlogID = Convert.ToInt32(Request.QueryString["BolgID"]);
        dsTable = objBlog.SelectBlogByID();
        if (dsTable != null && dsTable.Rows.Count > 0)
        {
            lbltit.Text = dsTable.Rows[0]["Title"].ToString();
            rptRecords1.DataSource = dsTable;
            rptRecords1.DataBind();

            rptRecords1.Visible = true;
        }
        else
        {
            //GeneratePages(0);

            rptRecords1.Visible = false;

        }
    }

    public string PicturePath(string sFilename)
    {
        string sFilename1 = sFilename;
        if (!File.Exists(Server.MapPath("~") + sFilename1))
        {
            sFilename = "images/No_Image.jpg";
        }
        return sFilename;
    }
}