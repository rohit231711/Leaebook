using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using BAL;
public partial class Admin_BookIssue : System.Web.UI.Page
{
    DataTable DT = new DataTable();
    BookBAL objBook = new BookBAL();
    BookIssueBAL mib = new BookIssueBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0 && Request.QueryString["catId"] != null)
            {
                objBook.CategoryID = Convert.ToInt32(Request.QueryString["catId"]);
                DT = objBook.SelectAllBookIssueByBookID();
                if (DT.Rows.Count > 0)
                {
                    dlBookIssue.DataSource = DT;
                    dlBookIssue.DataBind();
                   
                }
                else
                {
                    dlBookIssue.DataSource = null;
                    dlBookIssue.DataBind();
                }
            }
            if (Request.QueryString.Count > 0 && Request.QueryString["Id"] != null)
            {
                objBook.BookID = Convert.ToInt32(Request.QueryString["Id"]);
                DT = objBook.SelectAllBookIssueByBookID();
                if (DT.Rows.Count > 0)
                {
                    dlBookIssue.DataSource = DT;
                    dlBookIssue.DataBind();

                }
                else
                {
                    dlBookIssue.DataSource = null;
                    dlBookIssue.DataBind();
                }
            }
        }
    }
    protected void dlBookIssue_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Del")
        {
            deleteIssue(Convert.ToInt32(e.CommandArgument));
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
        else if (e.CommandName == "Edt")
        {
            Response.Redirect("AddBookIssue.aspx?BookID=" + e.CommandArgument.ToString());
        }

    }
    public void deleteIssue(int ID)
    {
        mib.ID = ID;
        DT = mib.GetImageMagazinIssueList();
        if (DT.Rows.Count > 0)
        {
            string path = "";
            string PDFpath;
            if (DT.Rows.Count > 0)
            {
                string PdfPath = Server.MapPath("../Book/" + DT.Rows[0]["Bookid"] + "/" + DT.Rows[0]["BookID"] + ".pdf");
                string PdfPathTop5 = Server.MapPath("../Book/" + DT.Rows[0]["Bookid"] + "/" + DT.Rows[0]["BookID"] + "TOP5.pdf");
                string BookPAth = Server.MapPath("../Book/" + DT.Rows[0]["Bookid"] + "/");
                string SwdBookPAth = Server.MapPath("../Book/" + DT.Rows[0]["Bookid"] + "/" + DT.Rows[0]["BookID"]);

                if (File.Exists(PdfPath))
                {

                    File.Delete(PdfPath);//Delete all images of issues.
                }
                if (File.Exists(PdfPathTop5))
                {
                    File.Delete(PdfPathTop5);//Delete all images of issues.
                }
                if (Directory.Exists(SwdBookPAth))
                {
                    string[] Resurl = Directory.GetFiles(SwdBookPAth);
                    foreach (string swf in Resurl)
                    {
                        File.Delete(swf);
                    }
                }


            }


            for (int i = 0; i <= DT.Rows.Count - 1; i++)
            {
                path = string.Empty;
                PDFpath = string.Empty;
                path = Server.MapPath("../Book/" + DT.Rows[i]["Bookid"] + "/" + DT.Rows[i]["ImagePath"]);
                PDFpath = Server.MapPath("../Book/" + DT.Rows[i]["Bookid"] + ".pdf");


                if (i == 0)
                    DeletetempMagz(DT.Rows[i]["Bookid"].ToString());

                if (File.Exists(path))
                {
                    File.Delete(path);//Delete all images of issues.
                }
                string sPath = path.Replace("Original", "s_");
                if (File.Exists(sPath))
                {
                    File.Delete(sPath);//Delete all images of issues.
                }

                string sspath = path.Replace("Original", "ss_");
                if (File.Exists(sspath))
                {
                    File.Delete(sspath);//Delete all images of issues.
                }
                string thpath = path.Replace("Original", "th_");
                if (File.Exists(thpath))
                {
                    File.Delete(thpath);//Delete all images of issues.
                }


                if (File.Exists(PDFpath))
                {
                    File.Delete(PDFpath);
                }
            }
            if (!string.IsNullOrEmpty(path))
                path = path.Replace("Original", "");
            if (File.Exists(path))
            {
                File.Delete(path);//Delete all images of issues.
            }
            //delete all issues
        }
        mib.deleteImageMagazinIssueList();
    }


    private void DeletetempMagz(string BookID)
    {
        string[] TempImage = Directory.GetFiles(Server.MapPath("../Book/" + BookID));
        foreach (string Imgesss in TempImage)
        {
            string temp = Imgesss.Split('\\')[Imgesss.Split('\\').Count() - 1];

            if (temp.ToLower().StartsWith("temp"))
            {
                try
                {
                    File.Delete(Imgesss);
                }
                catch
                {
                }
            }
        }
    }
}