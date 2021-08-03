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
public partial class Admin_AddCategory : System.Web.UI.Page
{
    CategoryBAL objCategory = new CategoryBAL();

    public static string Categories;
    DataTable DT = new DataTable();

    MenuBAL objmenu = new MenuBAL();
    #region  //Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Form.DefaultButton = btnSubmit.UniqueID;
            accessrights();
            if (CategoryID > 0)
            {
                objCategory = new CategoryBAL();
                objCategory.CategoryID = this.CategoryID;
                DataTable dt = objCategory.SelectCategoryByID();
                if (dt.Rows.Count > 0)
                {
                    imgcat.Visible = true;
                    hnfImage.Value = "1";
                    imgcat.ImageUrl = "~/Category/new_" + dt.Rows[0]["CImagePath"].ToString();
                }
            }
            bindCategories();
        }
    }

    private void bindCategories()
    {
        Categories = "";
        LanguageBAL LB = new LanguageBAL();
        DataTable DT = LB.GetLanguage();
        string Title_eng = txt_select.Text;
        string Title_spa = txt_spanish.Text;
        string Des_Englis = txt_desc.Text;
        string Des_Spanish = txt_desc1.Text;

        if (DT != null)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                objCategory.CategoryID = this.CategoryID;
                objCategory.LanguageID = Convert.ToInt64(DT.Rows[i]["ID"]);
                DataTable dt = objCategory.SelectCategoryByID();
                string CategoryValue = "";
                string Title = "";
                string Description = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    CategoryValue = dt.Rows[0]["CategoryName"].ToString();
                    
                    if(dt.Rows[0]["Title"] !=null)
                    {
                        Title = dt.Rows[0]["Title"].ToString();
                    }
                    else
                    {
                        if (objCategory.LanguageID == 1)
                        {
                            txt_select.Text = Title;
                        }
                        else
                        {
                            txt_spanish.Text = Title;
                        }
                    }
                    if (dt.Rows[0]["Description"] != null)
                    {
                        Description = dt.Rows[0]["Description"].ToString();
                    }
                    else
                    {
                        if (objCategory.LanguageID == 1)
                        {
                            txt_desc.Text = Description;
                        }
                        else
                        {
                            txt_desc1.Text = Description;
                        }
                    }
                    if (objCategory.LanguageID == 1)
                    {
                        txt_select.Text = Title;
                        txt_desc.Text = Description;

                    }
                    else
                    {
                        txt_spanish.Text = Title;
                        txt_desc1.Text = Description;
                    }

                }
                Categories += "   <tr class=\"light_bg\"> "
                             + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                             + "         <strong>Category name (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                    //+ "         <asp:TextBox CssClass=\"input_box\" ID=\"txtCategoryName\" runat=\"server\"></asp:TextBox> "
                             + " <input type=\"hidden\" name=\"languageid\" value=" + DT.Rows[i]["ID"] + " >"
                             + " <input type=\"text\" name=\"category\" class=\"input_box user1\" value=\"" + CategoryValue + "\" >"
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
        string subPath = "~/Category"; // your code goes here
        try
        {
            string[] Category = Request.Form.GetValues("category");
            string Title_eng = txt_select.Text;
            string Title_spa = txt_spanish.Text;
            string Des_Englis = txt_desc.Text;
            string Des_Spanish = txt_desc1.Text;
            //var x = true;
            //for(int i=0;i<Category.Length;i++)
            //{
            //    var cat = Category[i];
            //    if (string.IsNullOrEmpty(cat))
            //    {
            //        x = false;
            //    }
            //    else
            //        x = true;
            //}
            string fileName = "";
            if (fpUpload.HasFile)// && x)
            {
                bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));

                if (!IsExists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fileName = Path.GetFileName(fpUpload.FileName);
                bool IsExitsFile = System.IO.File.Exists(Server.MapPath(subPath + "/" + fileName));
                if (IsExitsFile)
                {
                    File.Delete(Server.MapPath(subPath + "/" + fileName));
                }
                string filename = Path.GetFileName(fpUpload.PostedFile.FileName);
                string targetPath = Server.MapPath(subPath + "/" + filename);
                Stream strm = fpUpload.PostedFile.InputStream;
                var targetFile = targetPath;
                GenerateThumbnails(strm, targetFile);

                //fpUpload.PostedFile.SaveAs(Server.MapPath(subPath + "/" + fileName));
                GenerateThumbnails(250, 400, (Server.MapPath(subPath + "/" + fileName)), subPath + "/" + "new_" + fileName);
                //GenerateThumbnails(420, 280, (Server.MapPath(subPath + "/" + fileName)), subPath + "/" + "ss_" + fileName);

                imgcat.ImageUrl = "~/Category/" + fileName;
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
                    }
                    catch
                    { }
                }
            }
            int count = 0;
            int CatID = 0;
            
            string[] LanguageID = Request.Form.GetValues("languageid");
            //string[] Title = Request.Form.GetValues("Title");
            //string[] Description = Request.Form.GetValues("Description");
            CatID = CategoryID;

            // check duplication

            for (int i = 0; i < LanguageID.Length; i++)
            {
                objCategory.LanguageID = Convert.ToInt32(LanguageID[i]);
                objCategory.CategoryName = Category[i];
                if(objCategory.LanguageID==1)
                {
                    objCategory.Title = Title_eng;
                }
                else
                {
                    objCategory.Title = Title_spa;
                }
                if (objCategory.LanguageID == 1)
                {
                    objCategory.Description = Des_Englis;
                }
                else
                {
                    objCategory.Description = Des_Spanish;
                }
               
                objCategory.CategoryID = CatID;
                
                DataTable DT = objCategory.Check_categoryDuplication();
                if (DT.Rows.Count > 0)
                {
                    count = count + 1;
                    break;
                }
            }

            if (count > 0)
            {
                Global.AlertNew(this, "Category alredy exists");
            }
            else
            {
                for (int i = 0; i < LanguageID.Length; i++)
                {
                    objCategory.IsActive = true;
                    objCategory.LanguageID = Convert.ToInt32(LanguageID[i]);
                    objCategory.CategoryName = Category[i];
                    if (objCategory.LanguageID == 1)
                    {
                        objCategory.Title = Title_eng;
                    }
                    else
                    {
                        objCategory.Title = Title_spa;
                    }
                    if (objCategory.LanguageID == 1)
                    {
                        objCategory.Description = Des_Englis;
                    }
                    else
                    {
                        objCategory.Description = Des_Spanish;
                    }

                    objCategory.CImagePath = fileName;
                    objCategory.CategoryID = CatID;
                    a = objCategory.InsertUpdateCategory();
                    CatID = a;
                }
            }

            //if (a > 0)
            //{
            //    if (CategoryID > 0)
            //        Response.Redirect("ManageCategory.aspx?edit=true");
            //    else
            //        Response.Redirect("ManageCategory.aspx?add=true");
            //    clear();
            //}
        }
        catch (Exception ee)
        {
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clear();
        Response.Redirect("ManageCategory.aspx");
    }
    #endregion

    #region  //Method
    private void clear()
    {
        //txtCategoryName.Text = string.Empty;
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

    private void GenerateThumbnails(Stream sourcePath, string targetPath)
    {
        using (var image = System.Drawing.Image.FromStream(sourcePath))
        {
            var newWidth = (int)(400);
            var newHeight = (int)(250);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(targetPath, image.RawFormat);
        }
    }
    #endregion

    #region  //Property
    public int CategoryID
    {
        get
        {
            int id = 0;
            if (Request.QueryString["CategoryID"] != null || Request.QueryString["CategoryID"] != "")
            {
                id = Convert.ToInt32(Request.QueryString["CategoryID"]);
                return id;
            }
            return id;
        }
    }
    #endregion
    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        Global.AlertNew(this, "Category clicked");
    }
}