using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net.Mime;
using System.Text.RegularExpressions;


public partial class Admin_AddBlog : System.Web.UI.Page
{
    BlogBAL BB = new BlogBAL();
    public static string Blog;
    DataTable DT = new DataTable();
    MenuBAL objmenu = new MenuBAL();

    #region  //Property
    public int BlogID
    {
        get
        {
            int id = 0;
            if (Request.QueryString["BlogID"] != null || Request.QueryString["BlogID"] != "")
            {
                id = Convert.ToInt32(Request.QueryString["BlogID"]);
                return id;
            }
            return id;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            this.Form.DefaultButton = btnSubmit.UniqueID;
            accessrights();
            if (BlogID > 0)
            {
                BB = new BlogBAL();
                BB.BlogID = this.BlogID;
                DataTable dt = BB.SelectBlogByID();
                if (dt.Rows.Count > 0)
                {
                    imgcat.Visible = true;
                    hnfImage.Value = "1";
                    imgcat.ImageUrl = "~/Blog/" + dt.Rows[0]["BlogImage"].ToString();
                }
            }
            bindBlog();
        }
    }
    
    private void bindBlog()
    {
        Blog = "";
        LanguageBAL LB = new LanguageBAL();
        DataTable DT = LB.GetLanguage();

        if (DT != null)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                BB.BlogID = this.BlogID;
                BB.LanguageID = Convert.ToInt64(DT.Rows[i]["ID"]);
                DataTable dt = BB.SelectBlogByID();
                string TitleValue = "";
                string DescriptionValue = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    TitleValue = dt.Rows[0]["Title"].ToString();
                    DescriptionValue = dt.Rows[0]["Description"].ToString();
                     string tit = "Blog Title ( "+ DT.Rows[i]["Language"] +" )";
                }
                Blog += "   <tr class=\"light_bg\"> "
                             + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                             + "         <strong>Blog Title (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                    //+ "         <asp:TextBox CssClass=\"input_box\" ID=\"txtCategoryName\" runat=\"server\"></asp:TextBox> "
                             + " <input type=\"hidden\" name=\"languageid\" value=" + DT.Rows[i]["ID"] + " >"
                             + " <input type=\"text\" name=\"Blog\" ID=\""+DT.Rows[i]["Language"]+"\" class=\"input_box user1\" value=\"" + TitleValue + "\" >"
                             + "         <font class=\"required\">*</font> "
                             + "     </td> "
                             + " </tr> "

                             + "   <tr class=\"light_bg\"> "
                            + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                            + "         <strong>Description (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                    //+ "         <asp:TextBox CssClass=\"input_box\" ID=\"txtCategoryName\" runat=\"server\"></asp:TextBox> "
                    //+ " <input type=\"hidden\" name=\"languageid\" value=" + DT.Rows[i]["ID"] + " >"
                             + " <textarea cols=\"80\"  style=\"height: 60px;width: 360px;\" ID=\"" + DT.Rows[i]["Language"] + "\" name=\"Desc\" class=\"input_box user2\"  >" + DescriptionValue + "</textarea>"
                             + "         <font class=\"required\">*</font> "
                             + "     </td> "
                             + " </tr> ";
            }
        }
    }

    private void accessrights()
    {
        int fl = 0;
        objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
        DT = objmenu.GetRightsByUser();
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 8)
                {
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 1)
                    {
                        fl = 1;
                    }
                }
            }
            if (fl == 0)
            {
                Response.Redirect("accessdenied.aspx");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int a = 0;
        string subPath = "~/Blog"; // your code goes here
        try
        {
            //objCategory.CategoryName = txtCategoryName.Text.Trim();
            string fileName = "";
            if (fpUpload.HasFile)
            {

                bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));

                if (!IsExists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fileName = Path.GetFileName(fpUpload.FileName);
                bool IsExitsFile = System.IO.File.Exists(Server.MapPath(subPath + "/" + fileName));
                if (!IsExitsFile)
                {
                    fpUpload.PostedFile.SaveAs(Server.MapPath(subPath + "/" + fileName));
                    GenerateThumbnails(420, 280, (Server.MapPath(subPath + "/" + fileName)), subPath + "/" + "ss_" + fileName);
                }
                //objCategory.CImagePath = fileName;
                imgcat.ImageUrl = "~/Blog/" + fileName;
            }
            if (Convert.ToBoolean(Request.QueryString["EDIT"]))
            {
                if (fpUpload.HasFile)
                {
                    try
                    {
                        if (!System.IO.Directory.Exists(Server.MapPath(subPath)))
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        fileName = Path.GetFileName(imgcat.ImageUrl);


                        foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(Server.MapPath(subPath + "/" + fileName)))
                        {
                            process.Kill();
                            process.WaitForExit();
                        }

                        //File.Delete(Server.MapPath(subPath + "/" + fileName));
                    }
                    catch
                    { }
                }

            }


            int ADVID = 0;
            string[] Blog = Request.Form.GetValues("Blog");
            string[] LanguageID = Request.Form.GetValues("languageid");
            string[] Description = Request.Form.GetValues("Desc");
            ADVID = BlogID;


            for (int i = 0; i < LanguageID.Length; i++)
            {
                BB.ISActive = true;
                BB.BlogImage = fileName;
                BB.LanguageID = Convert.ToInt32(LanguageID[i]);
                BB.Title = Blog[i];
                BB.Description = Description[i];
                BB.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
                BB.BlogID = ADVID;

                a = BB.InsertUpdateBlog();

                ADVID = a;


            }
            if (a > 0)
            {
                Response.Redirect("ManageBlog.aspx?add=true");
                // Global.Alert(this, "Category added successfully.");
                clear();
            }
        }
        catch (Exception ee)
        {
            //if (Convert.ToBoolean(Request.QueryString["EDIT"]))
            //{
            //    a = objCategory.UpdateCategory();
            //    Response.Redirect("ManageCategory.aspx?add=true");
            //}

            //throw ee;
        }


    }

    private void clear()
    {
        //txtCategoryName.Text = string.Empty;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clear();
        Response.Redirect("ManageBlog.aspx");
    }

    private void GenerateThumbnails(int height, int width, string sourcePath, string targetPath)
    {
        Bitmap image = new Bitmap(sourcePath);
        {
            var newWidth = (int)(width);
            var newHeight = (int)(height);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(Server.MapPath(targetPath));
        }
    }
}