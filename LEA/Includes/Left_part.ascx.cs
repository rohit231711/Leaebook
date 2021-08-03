using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using BAL;
using System.Data;
using System.IO;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.Configuration;

public partial class Includes_Left_menu : System.Web.UI.UserControl
{
    Category_Locale objCat_Locate = new Category_Locale();
    BookBAL Obj_book = new BookBAL();
    Security S = new Security();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_leftmenu();
            Bind_top_seller();
        }

    }

    private void GenerateThumbnails(string sourcePath, string targetPath)
    {
        try
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);
            {
                var newWidth = (int)(450);
                var newHeight = (int)(600);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath.Replace("\\/", "\\").Replace("/", "\\").Replace(".jpg", "_1.jpg"), ImageFormat.Jpeg);
            }
        }
        catch (Exception ex)
        { }
    }

    public void Bind_top_seller()
    {
        string addtolibrarytitle = "";
        string buynowtitle = "";
        string isfreetitle = "";
        isfreetitle = Localization.ResourceManager.GetString("Free");
        buynowtitle = Localization.ResourceManager.GetString("buynow");
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary");
        DataTable dt = new DataTable();
        Obj_book.LangaugeID = 1;

        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_book.LangaugeID = 2;
            }
            else
            {
                Obj_book.LangaugeID = 1;
            }
        }
        dt = Obj_book.get_top_seller_book_website();
        if (dt.Rows.Count > 0)
        {
            string img = "";

            string fileName = "Book/" + dt.Rows[0]["CategoryID"] + '/' + dt.Rows[0]["ImagePath"] + "";
            //if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
            //{               
            //    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book/" + dt.Rows[0]["CategoryID"].ToString() + "/" + dt.Rows[0]["ImagePath"].ToString();
            //}
            //else
            //{
            //    img = "../images/No_Image.jpg";
            //}
            if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
            {
                img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book/" + dt.Rows[0]["CategoryID"].ToString() + "/" + dt.Rows[0]["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                string newFile = img;
                if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                {
                    string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "";
                    Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "");
                }
            }
            else
            {
                img = "images/No_Image.jpg";
            }
            img_topseller.Src = img;
            var bookDetailUrl = "";
            if (Obj_book.LangaugeID == 1)
            {
                bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[0]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
            }
            else if (Obj_book.LangaugeID == 2)
            {
                bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[0]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
            }
            else
            {
                bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[0]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
            }
            bookname_link.HRef = bookDetailUrl;
            if (Convert.ToBoolean(dt.Rows[0]["IsFree"]) == true)
            {
                buy.Text = addtolibrarytitle.ToString();
                buynow_link.HRef = Config.WebSiteMain + "Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(dt.Rows[0]["BookID"].ToString())).ToString() + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            else
            {
                buy.Text = buynowtitle.ToString();
                buynow_link.HRef = Config.WebSiteMain + "Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(dt.Rows[0]["BookID"].ToString())).ToString() + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            bookimg_link.HRef = bookDetailUrl;
            lbl_booknmae.Text = dt.Rows[0]["Title"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["IsFree"]) == true)
            {
                lbl_amount.Text = isfreetitle.ToString();
            }
            else
            {
                if (dt.Rows[0]["Price"].ToString().Replace(",", ".") == "0")
                {
                    if (Convert.ToBoolean(dt.Rows[0]["IsFreePaper"]) == true)
                    {
                        lbl_amount1.Text = isfreetitle.ToString();
                    }
                    else
                    {
                        lbl_amount1.Text = "$" + dt.Rows[0]["PaperBookFinalPrice"].ToString().Replace(",", ".");
                    }
                }
                else
                    lbl_amount.Text = "$" + dt.Rows[0]["Price"].ToString().Replace(",", ".");
            }
        }
    }

    public void Bind_leftmenu()
    {
        DataTable dt = new DataTable();
        int language = 1;
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                language = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                language = 1;
            }
        }
        dt = objCat_Locate.Get_Category_Locale_leftmenu(language);
        if (dt.Rows.Count > 0)
        {
            int MenuOrder = 0;
            foreach (DataRow dr in dt.Rows)
            {

                HtmlGenericControl ili = new HtmlGenericControl("li");
                menu_ul.Controls.Add(ili);
                HtmlGenericControl ianchor = new HtmlGenericControl("a");

                MenuOrder = MenuOrder + 1;
                if (Request.QueryString["catid"] != null && Request.QueryString["catid"].ToString() != "")
                {
                    if (Request.QueryString["catid"].ToString() == dr["CategoryID"].ToString())
                    {
                        ianchor.Attributes["class"] = "active";
                    }
                }
                else
                {
                    if (MenuOrder == 1)
                    {
                        ianchor.Attributes["class"] = "active";
                    }
                }
                Session["CartID"] = dr["CategoryID"].ToString(); // cart id

                string AddressName1 = "", UrlMapped1 = "", CategoryName = "";
                string AddressName2 = "", UrlMapped2 = "";
                Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
                UrlMappingsSection section = (UrlMappingsSection)config.GetSection("system.web/urlMappings");
                if (language == 1)
                {
                    CategoryName = dr["CategoryEnglish"].ToString().TrimEnd().Replace(" ", "-").ToLower();
                    AddressName2 = "~/us/" + CategoryName;
                    UrlMapped2 = "~/Biography.aspx?l=en-US&catid=" + dr["CategoryID"].ToString();
                    section.UrlMappings.Remove(new UrlMapping(AddressName2, UrlMapped2));
                    section.UrlMappings.Add(new UrlMapping(AddressName2, UrlMapped2));
                    config.Save();
                    ianchor.Attributes.Add("href", Config.WebSiteMain + "us/" + CategoryName);
                    ianchor.InnerText = dr["CategoryName"].ToString();
                }
                else
                {
                    CategoryName = dr["CategoryEnglish"].ToString().TrimEnd().Replace(" ", "-").ToLower();
                    AddressName1 = "~/" + CategoryName;
                    UrlMapped1 = "~/Biography.aspx?l=es-ES&catid=" + dr["CategoryID"].ToString();
                    section.UrlMappings.Remove(new UrlMapping(AddressName1, UrlMapped1));
                    section.UrlMappings.Add(new UrlMapping(AddressName1, UrlMapped1));
                    config.Save();
                    ianchor.Attributes.Add("href", Config.WebSiteMain + CategoryName);
                    ianchor.InnerText = dr["CategoryName"].ToString();
                }
                Session["Catnm"] = dr["CategoryName"].ToString();
                ili.Controls.Add(ianchor);
            }
        }
    }
}