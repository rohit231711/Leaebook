using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Cyotek.GhostScript.PdfConversion;
using Cyotek.GhostScript;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net.Mime;

using System.Text.RegularExpressions;


public partial class Admin_AddBook : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();

    DataTable DT = new DataTable();
    RegistrationBAL objuser = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        
        accessrights();
        if (!IsPostBack)
        {


            BindCategory();
            BindCountry();
            BindLanguage();
            if (Request.QueryString["BookID"] != null)
            {
                objBook.BookID = Convert.ToInt64(Request.QueryString["BookID"]);
                DataTable dt = objBook.SelectBookByID();
                if (dt.Rows.Count > 0)
                {
                    //objCategory.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"].ToString());
                    //DataTable dt1 = objCategory.SelectCategoryByID();
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    ddlCategory.SelectedValue = dt1.Rows[0]["CategoryID"].ToString();
                    //    Categorylist();
                    //}
                    ddlCategory.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
                    ddlCountry.SelectedValue = dt.Rows[0]["CountryID"].ToString();
                    ddlLanguage.SelectedValue = dt.Rows[0]["Language"].ToString();
                    txtDesc.Text = dt.Rows[0]["Description"].ToString();
                    txtTitle.Text = dt.Rows[0]["Title"].ToString();
                    txtPrice.Text = Convert.ToDouble(dt.Rows[0]["Price"]).ToString("0.00");
                    chkIsFree.Checked = Convert.ToBoolean(dt.Rows[0]["IsFree"].ToString());
                    chkIsactive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    chkIsFeartued.Checked = Convert.ToBoolean(dt.Rows[0]["IsFeatured"].ToString());
                    txtDiscount.Text = (dt.Rows[0]["DiscountPrice"].ToString());
                    txtDate.Text = Convert.ToDateTime(dt.Rows[0]["PublishDate"].ToString()).ToString("MM/dd/yyyy");
                    txtPublicsher.Text = dt.Rows[0]["Publisher"].ToString();
                }
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
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 5)
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
  

    private void BindCategory()
    {
        CategoryBAL objCategory = new CategoryBAL();
        DataTable dt = objCategory.SelectAllCartegory();
        if (dt.Rows.Count > 0)
        {
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("Select", "0"));
        }
    }
    private void BindCountry()
    {
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllActiveCountry();
        if (dt.Rows.Count > 0)
        {
            ddlCountry.DataSource = dt;
            ddlCountry.DataTextField = "countryname";
            ddlCountry.DataValueField = "countryid";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("Select", "0"));
            ddlLanguage.SelectedIndex = 0;
        }
    }
    private void BindLanguage()
    {
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllLanguage();
        if (dt.Rows.Count > 0)
        {
            ddlLanguage.DataSource = dt;
            ddlLanguage.DataTextField = "Language";
            ddlLanguage.DataValueField = "ID";
            ddlLanguage.DataBind();
            ddlLanguage.Items.Insert(0, new ListItem("Select", "0"));
            ddlLanguage.SelectedIndex = 0;
        }
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (fuPdfUpload.HasFile)
        {
            int i = fuPdfUpload.PostedFile.FileName.LastIndexOf(".");
            string ext = fuPdfUpload.PostedFile.FileName.Substring(i + 1);
            if (!ext.Equals(""))
            {
                if ((ext.ToLower().Trim() == "jpg") || (ext.ToLower().Trim() == "jpeg") || (ext.ToLower().Trim() == "bmp") || (ext.ToLower().Trim() == "gif") || (ext.ToLower().Trim() == "png"))
                {

                }
                else
                {
                    Response.Write("<script>alert('Please select valid logo');</script>");
                    return;
                }
            }
            else
            {
                Response.Write("<script>alert('Please select valid logo');</script>");
                return;
            }
        }
        objBook.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
      
        objBook.Title = txtTitle.Text.Trim();
        objBook.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        objBook.LangaugeID = Convert.ToInt32(ddlLanguage.SelectedValue);
        objBook.Description = txtDesc.Text;
        objBook.Price = txtPrice.Text;
        objBook.IsActive = chkIsactive.Checked;
        objBook.IsFree = chkIsFree.Checked;
        objBook.IsFeatured = Convert.ToInt32(chkIsFeartued.Checked);
        objBook.Published = true;
        objBook.DiscountedPrice = float.Parse(txtDiscount.Text);
        objBook.PublishDate = Convert.ToDateTime(txtDate.Text);
        objBook.Publisher = txtPublicsher.Text.Trim();
        objBook.UpdatedOn = DateTime.Now;
        int Main = 0;
        {
            if (Request.QueryString["EDIT"] != null)
            {
                if (Convert.ToBoolean(Request.QueryString["EDIT"]))
                {
                    objBook.BookID = Convert.ToInt64(BookID);
                    objBook.UpdatedOn = DateTime.UtcNow.AddHours(8);
                    objBook.UpdatedBy = Global.RegistrationID;
                    //int a = objBook.UpdateBook();
                    Main = Convert.ToInt32(BookID);
                }
            }
            else
            {

                objBook.CreatedBy = Global.RegistrationID;
                //int a = objBook.InsertBook();
              //Main = a;

            }

        }
        if (fuPdfUpload.HasFile)
        {

            int i = fuPdfUpload.PostedFile.FileName.LastIndexOf(".");
            string ext = fuPdfUpload.PostedFile.FileName.Substring(i + 1);
            if (!ext.Equals(""))
            {
                if ((ext.ToLower().Trim() == "jpg") || (ext.ToLower().Trim() == "jpeg") || (ext.ToLower().Trim() == "bmp") || (ext.ToLower().Trim() == "gif") || (ext.ToLower().Trim() == "png"))
                {
                    string subPath = "~/Book/" + Convert.ToInt32(Main);
                    if (!Directory.Exists(Server.MapPath(subPath)))
                    {
                        Directory.CreateDirectory(Server.MapPath(subPath));
                    }
                    if (File.Exists(Server.MapPath(subPath + "/" + "PublisherLogo.jpg")))
                    {
                        File.Delete(Server.MapPath(subPath + "/" + "PublisherLogo.jpg"));
                    }
                    fuPdfUpload.SaveAs(Server.MapPath(subPath + "/" + "PublisherLogo.jpg"));//+ ext.ToLower().Trim()));
                }
            }

        }
        Response.Redirect("ManageBook.aspx");
    }

    public int getNumberOfPdfPages(string fileName)
    {
        using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
        {
            Regex regex = new Regex(@"/Type\s*/Page[^s]");
            MatchCollection matches = regex.Matches(sr.ReadToEnd());
            return matches.Count;
        }
    }


    private string RenameFile(String oldFilename, String newFilename)
    {
        FileInfo file = new FileInfo(oldFilename);

        if (file.Exists)
        {
            File.Move(oldFilename, newFilename);
        }
        return newFilename;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {

    }
    public int BookID
    {
        get
        {
            int id = 0;
            if (Request.QueryString["BookID"] != null || Request.QueryString["BookID"] != "")
            {
                id = Convert.ToInt32(Request.QueryString["BookID"]);
                return id;
            }
            return id;
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
   

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtDate.Text) < DateTime.Now)
        {
            txtDate.Text = string.Empty;
            ScriptManager.RegisterStartupScript(Page, GetType(), "JsStatus", "validdate();", true);
        }
    }
    protected void txtDate_DataBinding(object sender, EventArgs e)
    {

    }
}