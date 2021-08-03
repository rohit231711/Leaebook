using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.Web.Services;

public partial class MyLibrary : System.Web.UI.Page
{
    BookPurchaseBAL BPB = new BookPurchaseBAL();
    BookBAL Obj_Book = new BookBAL();
    Security S = new Security();
    int bookid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserName"] != null && Session["UserName"].ToString() != "")
            {
                //bookid = Convert.ToInt32(Request.QueryString["bookid"]);
                RegistrationBAL RB = new RegistrationBAL();
                DataTable dT = new DataTable();
                RB.UserName = Session["UserName"].ToString();
                dT = RB.GetByUserName();
                if (dT != null && dT.Rows.Count > 0)
                {
                    BPB.UserID = Convert.ToInt32(dT.Rows[0]["RegistrationID"]);
                }
                BPB.UserID = Convert.ToInt32(Session["UserID"]);
            }
            BindData();
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
    private static void GenerateThumbnails_Static(string sourcePath, string targetPath)
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

    public void BindData()
    {
        string view = "";
        string isfreetitle = "";
        isfreetitle = Localization.ResourceManager.GetString("Free");
        view = Localization.ResourceManager.GetString("view");
        DataTable dt = new DataTable();
        int count = 0;
        BPB.LanguageID = 1;
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                BPB.LanguageID = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                BPB.LanguageID = 1;
            }
        }
        dt = BPB.getUserLibrary_Pagination(null, null);
        string a1;
        if (Request.QueryString["l"].ToString() == "es-ES")
            a1 = "<div class='panel'>" + "<div class='titnewre'>Mi biblioteca</div>" + "<a href='#' class='titnewre1'>Ver todos eBook</a>" + "</div>";
        else
            a1 = "<div class='panel'>" + "<div class='titnewre'>My Library</div>" + "<a href='#' class='titnewre1'>View All eBook</a>" + "</div>";
        string str = "";
        if (dt.Rows.Count > 0)
        {

            //Hashtable hashtable = new Hashtable();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                //if (!hashtable.ContainsKey(dr["BookID"]))
                //{
                //hashtable.Add(dr["BookID"], "Book");
                if (i % 3 == 0)
                {
                    str = str + "<div class='box1book boxbolast'>";
                }
                else
                {
                    str = str + "<div class='box1book'>";
                }
                i++;

                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
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
                var bookDetailUrl = "";
                if (BPB.LanguageID == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else if (BPB.LanguageID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0)
                {
                    //if(FileLevelControlBuilderAttribute.Ex)
                    // 
                    if (dr["IsDelete"].ToString() == "False")
                    {
                        str = str + "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases '> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                    }
                    else
                    {
                        str = str + "<a href='#?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases '> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                    }
                }
                else
                {
                    if (dr["IsDelete"].ToString() == "False")
                    {
                        str = str + "<div class='bookimg'>" +"<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                        if (Convert.ToBoolean(dr["IsFree"]) != true)
                        {
                            str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                        }
                        str = str + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='bookimg'>" +
                    "<a href='#" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                        if (Convert.ToBoolean(dr["IsFree"]) != true)
                        {
                            str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                        }
                        str = str + "</div>";
                    }
                }
                if (dr["IsDelete"].ToString() == "False")
                {
                    str = str + "<div class='namkl2'><a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases' style='color:#616161'>" + dr["BookTitle1"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                }
                else
                {
                    str = str + "<div class='namkl2'><a href='#" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases' style='color:#616161'>" + dr["BookTitle1"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<div class='namkl'>" + isfreetitle.ToString() + "</div>";
                }
                else
                {
                    str = str + "<div class='namkl'>$" + dr["Amount"] + "</div>";
                }

                //str = str + "<a href='Book-Detail.aspx?" + dr["BookID"] + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>View</a>" +
                if (dr["IsDelete"].ToString() == "False")
                {
                    str = str + "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases' class='boxbut'>" + view.ToString() + "</a>" +
                    "</div>";
                }
                else
                {
                    //  str = str + "<a href='MyLibrary.aspx?"+" class='boxbut'>" + view.ToString() + "</a>" +
                    //"</div>";
                    //  Response.Write("<script>alert('This Book Was Deleted...')</script>");
                }
                //}
            }
        }
        if (dt.Rows.Count == 0)
        {
            //Message1("You already have this book in your cart");
            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No data Found.');", true);
        }
        div_book.InnerHtml = a1 + str;
    }
    [WebMethod]
    public static string BindData(string LoadMore)
    {
        BookBAL BPB = new BookBAL();
        string view = "";
        string isfreetitle = "";
        isfreetitle = Localization.ResourceManager.GetString("Free");
        view = Localization.ResourceManager.GetString("view");
        DataTable dt = new DataTable();
        BPB.LanguageID = 1;
        BPB.UserID = HttpContext.Current.Session["UserID"] == null ? 0 : Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
        if (HttpContext.Current.Request.QueryString["l"] != null && HttpContext.Current.Request.QueryString["l"].ToString() != "")
        {
            if (HttpContext.Current.Request.QueryString["l"].ToString() == "es-ES")
            {
                BPB.LanguageID = 2;
            }
            else if (HttpContext.Current.Request.QueryString["l"].ToString() == "en-US")
            {
                BPB.LanguageID = 1;
            }
        }
        if (LoadMore != "LoadMore")
        {
            if (HttpContext.Current.Request.QueryString["PageNo"] != null && HttpContext.Current.Request.QueryString["PageNo"].ToString() != "")
            {
                BPB.EndIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["PageNo"].ToString()) * 9;
                BPB.StartIndex = (BPB.StartIndex - 9) + 1;
            }
            else
            {
                HttpContext.Current.Session["num"] = "9";
                BPB.EndIndex = 9;
                BPB.StartIndex = 1;
            }
        }
        else if (LoadMore == "LoadMore")
        {
            BPB.StartIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 1;
            BPB.EndIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 9;
            HttpContext.Current.Session["num"] = BPB.EndIndex.ToString();
        }
        dt = BPB.getUserLibrary_Pagination();
        string str = "";
        if (dt.Rows.Count > 0)
        {
            //Hashtable hashtable = new Hashtable();
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                //if (!hashtable.ContainsKey(dr["BookID"]))
                //{
                //    hashtable.Add(dr["BookID"], "Book");
                if (i % 3 == 0)
                {
                    str = str + "<div class='box1book boxbolast'>";
                }
                else
                {
                    str = str + "<div class='box1book'>";
                }
                i++;

                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                    string newFile = img;
                    if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnails_Static(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "");
                    }
                }
                else
                {
                    img = "images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (BPB.LanguageID == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else if (BPB.LanguageID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0)
                {
                    //if(FileLevelControlBuilderAttribute.Ex)
                    // 
                    if (dr["IsDelete"].ToString() == "False")
                    {
                        str = str + "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases '> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                    }
                    else
                    {
                        str = str + "<a href='#?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases '> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                    }
                }
                else
                {
                    if (dr["IsDelete"].ToString() == "False")
                    {
                        str = str + "<div class='bookimg'>" +
                    "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                        if (Convert.ToBoolean(dr["IsFree"]) != true)
                        {
                            str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                        }
                        str = str + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='bookimg'>" +
                    "<a href='#" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                        if (Convert.ToBoolean(dr["IsFree"]) != true)
                        {
                            str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                        }
                        str = str + "</div>";
                    }
                }
                if (dr["IsDelete"].ToString() == "False")
                {
                    str = str + "<div class='namkl2'><a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases' style='color:#616161'>" + dr["BookTitle1"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                }
                else
                {
                    str = str + "<div class='namkl2'><a href='#" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases' style='color:#616161'>" + dr["BookTitle1"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<div class='namkl'>" + isfreetitle.ToString() + "</div>";
                }
                else
                {
                    str = str + "<div class='namkl'>$" + dr["Amount"] + "</div>";
                }
                if (dr["IsDelete"].ToString() == "False")
                {
                    str = str + "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["BookTitle1"])).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___") + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1&TabType=New Releases' class='boxbut'>" + view.ToString() + "</a>" +
                    "</div>";
                }
                else
                {
                    //  str = str + "<a href='MyLibrary.aspx?"+" class='boxbut'>" + view.ToString() + "</a>" +
                    //"</div>";
                    //  Response.Write("<script>alert('This Book Was Deleted...')</script>");
                }
                //}
            }
        }
        if (dt.Rows.Count < 9)
        {
            str += "<div id=\"divNoData\" onload=\"hideAlert()\"></div>";
        }
        if (dt.Rows.Count == 0)
        {
            string NoData = "<div id=\"divNoData\" style=\"Color:red;\" onload=\"hideAlert()\">No Record Found</div>";
            return NoData;
        }
        return str;
    }
}