using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;
using System.Configuration;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class Includes_Editorschoice : System.Web.UI.UserControl
{
    string path = "";
    string img = "";
    BookBAL Obj_book = new BookBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            string l = (System.Threading.Thread.CurrentThread.CurrentCulture.Name).ToLower(); 
            if (l != null)
            {
                if (l== "es-es")
                {
                    Obj_book.LangaugeID = 2;
                }
                else if(l == "en-us")
                {
                    Obj_book.LangaugeID = 1;
                }
            }
            dt = Obj_book.get_editor_book_website();
            int a = dt.Rows.Count;
            string sPath = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            lnk_editor1.HRef = sPath.ToString();
            lnk_editor2.HRef = sPath.ToString();
            lnk_editor3.HRef = sPath.ToString();
            lnk_editor4.HRef = sPath.ToString();
            lnkimg_editor1.HRef = sPath.ToString();
            lnkimg_editor2.HRef = sPath.ToString();
            lnkimg_editor3.HRef = sPath.ToString();
            lnkimg_editor4.HRef = sPath.ToString();
            if (a > 0)
            {
                div_editor1.Visible = true;
                path = "Book/" + dt.Rows[0]["CategoryID"] + '/' + dt.Rows[0]["ImagePath"] + "";
                //if (File.Exists(((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + ""))))
                //{
                //    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString()+"Book/" + dt.Rows[0]["CategoryID"].ToString() + "/" + dt.Rows[0]["ImagePath"].ToString();
                    
                //}
                //else
                //{
                //    img = "../images/No_Image.jpg";
                //}
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "")))
                {
                    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book/" + dt.Rows[0]["CategoryID"].ToString() + "/" + dt.Rows[0]["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                    string newFile = img;
                    if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "");
                    }
                }
                else
                {
                    img = Config.WebSiteMain+"images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (Obj_book.LangaugeID == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[0]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");                    
                }
                else if (Obj_book.LangaugeID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[0]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[0]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                editor1.Src = img;
                lnk_editor1.HRef = bookDetailUrl;
                lnklbl_editor1.HRef = lnkimg_editor1.HRef = bookDetailUrl;
                lbl_title1.Text = dt.Rows[0]["Title"].ToString();
            }
            if (a > 1)
            {
                div_editor2.Visible = true;
                path = "Book/" + dt.Rows[1]["CategoryID"] + '/' + dt.Rows[1]["ImagePath"] + "";
                //if (File.Exists(((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + ""))))
                //{
                //    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString()+"Book/" + dt.Rows[0]["CategoryID"].ToString() + "/" + dt.Rows[0]["ImagePath"].ToString();

                //}
                //else
                //{
                //    img = "../images/No_Image.jpg";
                //}
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "")))
                {
                    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book/" + dt.Rows[1]["CategoryID"].ToString() + "/" + dt.Rows[1]["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                    string newFile = img;
                    if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "");
                    }
                }
                else
                {
                    img = Config.WebSiteMain+ "images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (Obj_book.LangaugeID == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[1]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                else if (Obj_book.LangaugeID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[1]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[1]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                editor2.Src = img;
                lnkimg_editor2.HRef = bookDetailUrl;
                lnklbl_editor2.HRef = lnk_editor2.HRef = bookDetailUrl;
                lbl_title2.Text = dt.Rows[1]["Title"].ToString();
            }
            if (a > 2)
            {
                div_editor3.Visible = true;
                path = "Book/" + dt.Rows[2]["CategoryID"] + '/' + dt.Rows[2]["ImagePath"] + "";
                //if (File.Exists(((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + ""))))
                //{
                //    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString()+"Book/" + dt.Rows[0]["CategoryID"].ToString() + "/" + dt.Rows[0]["ImagePath"].ToString();

                //}
                //else
                //{
                //    img = "../images/No_Image.jpg";
                //}
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "")))
                {
                    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book/" + dt.Rows[2]["CategoryID"].ToString() + "/" + dt.Rows[2]["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                    string newFile = img;
                    if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "");
                    }
                }
                else
                {
                    img = Config.WebSiteMain+"images/No_Image.jpg";
                }
                editor3.Src = img;
                var bookDetailUrl = "";
                if (Obj_book.LangaugeID == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[2]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                else if (Obj_book.LangaugeID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[2]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[2]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                lnkimg_editor3.HRef = bookDetailUrl;
                lnklbl_editor3.HRef = lnk_editor3.HRef = bookDetailUrl;
                lbl_title3.Text = dt.Rows[2]["Title"].ToString();
            }
            if (a > 3)
            {
                div_editor4.Visible = true;
                path = "Book/" + dt.Rows[3]["CategoryID"] + '/' + dt.Rows[3]["ImagePath"] + "";
                //if (File.Exists(((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + ""))))
                //{
                //    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString()+"Book/" + dt.Rows[0]["CategoryID"].ToString() + "/" + dt.Rows[0]["ImagePath"].ToString();

                //}
                //else
                //{
                //    img = "../images/No_Image.jpg";
                //}
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "")))
                {
                    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Book/" + dt.Rows[3]["CategoryID"].ToString() + "/" + dt.Rows[3]["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                    string newFile = img;
                    if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + "");
                    }
                }
                else
                {
                    img =Config.WebSiteMain+ "images/No_Image.jpg";
                }
                editor4.Src = img;
                var bookDetailUrl = "";
                if (Obj_book.LangaugeID == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[3]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                else if (Obj_book.LangaugeID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[3]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dt.Rows[3]["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":","___");
                }
                lnkimg_editor4.HRef = bookDetailUrl;
                lnklbl_editor4.HRef = lnk_editor4.HRef = bookDetailUrl;
                lbl_title4.Text = dt.Rows[3]["Title"].ToString();
            }            
        }
    }

    private void GenerateThumbnails(string sourcePath, string targetPath)
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

}