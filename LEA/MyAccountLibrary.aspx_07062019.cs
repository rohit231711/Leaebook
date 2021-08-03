using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public partial class MyAccountLibrary : System.Web.UI.Page
{
    BookPurchaseBAL BPB = new BookPurchaseBAL();
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

        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        //{

        //    view = "Ver";

        //}
        //else
        //{

        //    view = "View";

        //}

        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        //{
        //    free = "gratis";
        //}
        //else
        //{
        //    free = "Free";
        //}


        //if (Session["RegistrationID"] != null && Session["RegistrationID"].ToString() != "")
        //{
        //    BPB.UserID = Convert.ToInt32(Session["RegistrationID"]);
        //}
        dt = BPB.getUserLibrary();

        //if (dt.Rows[12].ToString() == bookid)
        //{
        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book altrady exist in your library.');", true);
        //}
        //string img = "images/limdbook.png";
        string a1;
        string a2;
        if (Request.QueryString["l"].ToString() == "es-ES")
        {
            a1 = "<div class='panel'>" +
                       "<div class='titnewre'>Mi biblioteca</div>" +
                       "<a href='#' class='titnewre1'>Ver todos eBook</a>" +
                   "</div>";
            a2 = a2 = "<div class='panel-1'>" +
                         "<div class='title'>Ebook Reader, te permite leer tus libros favoritos mientras viajas </div>" +
                         "<div class='image'><a href='https://www.leaebook.com/ReaderSetup/LEAeBooks.exe'><img src='images/mac.png'></a><a href='https://www.leaebook.com/ReaderSetup/LEAeBooks.exe'><img src='images/windows.png'></a></div>" +
                     "</div>";
        }
        else
        {
            a1 = "<div class='panel'>" +
               "<div class='titnewre'>My Library</div>" +
               "<a href='#' class='titnewre1'>View All eBook</a>" +
           "</div>";
            a2 = "<div class='panel-1'>" +
                             "<div class='title'>Ebook Reader, Let You Read Your Favorite Books On the Go</div>" +
                             "<div class='image'><a href='https://www.leaebook.com/ReaderSetup/LEAeBooks.exe'><img src='images/mac.png'></a><a href='https://www.leaebook.com/ReaderSetup/LEAeBooks.exe'><img src='images/windows.png'></a></div>" +
                         "</div>";
        }

        string str = "";


        if (dt.Rows.Count > 0)
        {
            //for (int i1 = 0; i1 < dt.Rows.Count; i1++)
            //{
            //    if (Convert.ToInt32(dt.Rows[i1]["BookID"]) == bookid)
            //    {
            //        count++;
            //        if (Convert.ToInt32(dt.Rows[i1]["BookID"]) == bookid)
            //        {
            //            //Message1("You already have this book in your cart");
            //            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book altrady exist in your library.');", true);
            //        }
            //        else if (Convert.ToInt32(dt.Rows[i1]["BookID"]) != bookid)
            //        {
            //            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library.');", true);
            //        }

            //    }

            //}

            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
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

                if (Convert.ToInt32(dr["DiscountPrice"]) == 0)
                {
                    //if(FileLevelControlBuilderAttribute.Ex)
                    //                    
                    str = str + "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1 '> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    str = str + "<div class='bookimg'>" +
            "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";

                }
                str = str + "<div class='namkl2'><a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1' style='color:#616161'>" + dr["BookTitle1"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<div class='namkl'>" + isfreetitle.ToString() + "</div>";
                }
                else
                {
                    str = str + "<div class='namkl'>$" + dr["Price"] + "</div>";
                }

                //str = str + "<a href='Book-Detail.aspx?" + dr["BookID"] + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>View</a>" +

                str = str + "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&mylib=1' class='boxbut'>" + view.ToString() + "</a>" +
                "</div>";
            }
        }
        if (dt.Rows.Count == 0)
        {
            //Message1("You already have this book in your cart");
            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No data Found.');", true);
        }
        div_book.InnerHtml = a1  + a2 + str;
    }
}