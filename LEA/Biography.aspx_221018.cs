using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;
using System.Text.RegularExpressions;
using Localization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Services;

public partial class Biography : System.Web.UI.Page
{
    BookBAL Obj_Book = new BookBAL();
    Security S = new Security();
    public static int Language;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var source = new Dictionary<int, string>();
            source.Add(-1, Localization.ResourceManager.GetString("sortby", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
            source.Add(1, Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
            source.Add(0, Localization.ResourceManager.GetString("paid", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
            ddlSortby.DataSource = source;
            ddlSortby.DataTextField = "Value";
            ddlSortby.DataValueField = "Key";
            ddlSortby.DataBind();

            addtoLibrary();
            BindData();
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                addtocart();
            }
        }
    }

    public void addtoLibrary()
    {
        int count = 0;
        if (Request.QueryString["fbid"] != null && Request.QueryString["fbid"].ToString() != "")
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                objPurchase.BookID = Convert.ToInt16(Request.QueryString["fbid"].ToString());
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    objPurchase.LanguageID = 2;
                }
                else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                {
                    objPurchase.LanguageID = 1;
                }
                DataTable DT = objPurchase.getUserLibrary();
                if (DT != null && DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == objPurchase.BookID)
                        {
                            count++;
                        }

                    }
                    if (count > 0)
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);

                        }
                    }
                    else
                    {
                        objPurchase.MoveToBookPurchase_freebook_website();
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);

                        }

                        //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["fbid"].ToString());
                    }
                }
                else
                {

                    objPurchase.MoveToBookPurchase_freebook_website();
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);

                    }

                    //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["fbid"].ToString());
                }
            }
            else
            {
                Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }
    }

    public void addtocart()
    {
        BookOrderBAL Obj_bookOrder = new BookOrderBAL();
        int count = 0;
        int cnt1 = 0;
        int result = 0;
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
            objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
            objPurchase.PurchaseDate = System.DateTime.Now;
            objPurchase.OrderID = 0;
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                objPurchase.BookID = Convert.ToInt16(Request.QueryString["id"].ToString());
            }
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                objPurchase.LanguageID = 2;
            }
            else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
            {
                objPurchase.LanguageID = 1;
            }
            DataTable Dt = objPurchase.getUserLibrary();
            if (Dt != null && Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                    {
                        cnt1++;
                    }

                }
                if (cnt1 > 0)
                {
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('Usted ya ha comprado este libro electrónico. Qué quieres mover en mi biblioteca?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('You have already purchased this ebook. do you want to move in my library?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

                    }
                }
                else
                {
                    Session["AddToCart"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    Obj_bookOrder.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    //Obj_bookOrder.InsertCustomerCart1();

                    DataTable DT = Obj_bookOrder.GetCartList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                            {
                                count++;
                            }
                        }
                        if (count > 0)
                        {
                            //Message1("You already have this book in your cart");
                            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                            }
                        }
                        else
                        {
                            result = Obj_bookOrder.InsertCustomerCart1();
                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");
                        }
                    }
                    else
                    {
                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                        result = Obj_bookOrder.InsertCustomerCart1();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

                    }
                }
            }

            else
            {
                Session["AddToCart"] = null;
                Obj_bookOrder.OrderDate = System.DateTime.Now;
                Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                Obj_bookOrder.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
                //Obj_bookOrder.InsertCustomerCart1();

                DataTable DT = Obj_bookOrder.GetCartList();
                if (DT != null && DT.Rows.Count > 0)
                {
                    Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                        {
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        //Message1("You already have this book in your cart");
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                        }
                    }
                    else
                    {
                        result = Obj_bookOrder.InsertCustomerCart1();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");
                    }
                }
                else
                {
                    string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                    Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                    result = Obj_bookOrder.InsertCustomerCart1();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

                }
            }
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

        }
        else
        {
            if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
            {
                int cnt = 0;
                string[] str = Session["AddToCart"].ToString().Split(',');
                foreach (string Add in str)
                {

                    if (Add == Request.QueryString["id"].ToString())
                    {
                        cnt++;
                    }
                }
                if (cnt > 0)
                {
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                    }
                }
                else
                {
                    Session["AddToCart"] = Session["AddToCart"] + "," + Request.QueryString["id"].ToString();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
            }
            else
            {
                Session["AddToCart"] = Request.QueryString["id"].ToString();
                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
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

    private static void GenerateThumbnailStatic(string sourcePath, string targetPath)
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
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        viewallbook = Localization.ResourceManager.GetString("newrelease");
        newrelease = Localization.ResourceManager.GetString("newrelease");
        buynowtitle = Localization.ResourceManager.GetString("buynow");
        isfreetitle = Localization.ResourceManager.GetString("Free");
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary");
        DataTable dt = new DataTable();
        Obj_Book.LangaugeID = 1;
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book.LangaugeID = Language = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book.LangaugeID = Language = 1;
            }
        }
        if (Request.QueryString["catid"] != null && Request.QueryString["catid"].ToString() != "")
        {
            HttpContext.Current.Session["catId"] = Obj_Book.CategoryID = Convert.ToInt32(Request.QueryString["catid"]);
            CategoryBAL cat = new CategoryBAL();
            cat.CategoryID = Convert.ToInt32(Request.QueryString["catid"]);
            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
            {
                if (Request.QueryString["l"].ToString() == "es-ES")
                {
                    cat.LanguageID = Language = 2;
                }
                else if (Request.QueryString["l"].ToString() == "en-US")
                {
                    cat.LanguageID = Language = 1;
                }
            }
            DataTable DT = cat.SelectCategoryByID();
            if (DT != null && DT.Rows.Count > 0)
            {
                lblcat.Text = DT.Rows[0]["CategoryName"].ToString();
                this.Page.Title = lblcat.Text;
            }
        }

        Obj_Book.EndIndex = 9;
        HttpContext.Current.Session["num"] = "9";
        Obj_Book.StartIndex = 1;

        dt = Obj_Book.getBookByCategoryLimited(Convert.ToInt32(ddlSortby.SelectedValue), "New Releases");
        string a1 = "";

        //a1 = "<div class='panel'>" +
        //                "<div class='titnewre'>" + newrelease.ToString() + " </div>" +
        //                "<a href='Category.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='titnewre1'>" + viewallbook.ToString() + "</a>" +
        //            "</div>";

        string str = "";

        if (dt.Rows.Count > 0)
        {
            int i = 1;
            str = str + "<div id='div_Books'>";
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
                var bookDetailUrl = "";
                if (Language == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                    //bookDetailUrl += "/" + dr["BookID"];
                }
                else if (Language == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                    //bookDetailUrl += "/" + dr["BookID"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                    //bookDetailUrl += "/" + dr["BookID"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0 && Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    //str = str + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "'  alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223'/></a>";
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223'/></a>";
                }
                else
                {
                    //str = str + "<div class='bookimg'>" + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "' alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223' /></a>";
                    str = str + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                //str = str + "<div class='namkl2'><a href = 'Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                str = str + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                if (Convert.ToBoolean(dr["IseBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFree"]) == true)
                    {
                        str = str + "<div class='namkl' style=\"width: 50%;float: left;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='namkl' style=\"width: 50%;float: left;\">$" + dr["FinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 50%;float: left;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsPaperBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFreePaper"]) == true)
                    {
                        str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='namkl' style=\"width: 45%;float: left;text-align: right;padding-right: 5px;\">$" + dr["PaperBookFinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<a href='Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                "</div>";
                }
                else
                {
                    str = str + "<a href='Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }
            }
            str = str + "</div>";
        }
        if(dt.Rows[0]["count"].ToString() == "9")
        {
            str += "<div id=\"divNoData\" onload=\"hideAlert()\"></div>";
        }
        if (dt.Rows.Count < 9)
        {            
            str += "<div id=\"divNoData\" onload=\"hideAlert()\"></div>";
        }
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Nodatafound", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
        }
        div_book.InnerHtml = a1 + str;
    }

    [WebMethod]
    public static string BindDatas(int Sortby, string For, string LoadMore)
    {
        BookBAL Obj_Book = new BookBAL();
        Security S = new Security();
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";

        if (HttpContext.Current.Request.QueryString["l"] != null && HttpContext.Current.Request.QueryString["l"].ToString() != "")
        {
            if (HttpContext.Current.Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book.LangaugeID = Language = 2;
            }
            else if (HttpContext.Current.Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book.LangaugeID = Language = 1;
            }
        }

        string culture = "en-US";
        if (Language == 2)
        {
            culture = "es-ES";
        }
        else if (Language == 1)
        {
            culture = "en-US";
        }
        else
        {
            culture = "en-US";
        }
        viewallbook = Localization.ResourceManager.GetString("viewallbook", culture);
        newrelease = Localization.ResourceManager.GetString("newrelease", culture);
        buynowtitle = Localization.ResourceManager.GetString("buynow", culture);
        isfreetitle = Localization.ResourceManager.GetString("Free", culture);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", culture);
        DataTable dt = new DataTable();

        Obj_Book.LangaugeID = Language;

        if (HttpContext.Current.Request.QueryString["catid"] != null && HttpContext.Current.Request.QueryString["catid"].ToString() != "")
        {
            HttpContext.Current.Session["catId"] = Obj_Book.CategoryID = Convert.ToInt32(HttpContext.Current.Request.QueryString["catid"]);                        
        }

        Obj_Book.CategoryID = Convert.ToInt32(HttpContext.Current.Session["catId"]);

        if (LoadMore != "LoadMore")
        {
            if (For == "SortBy" || For == "New Releases" || For == "ebook" || For == "Paper book")
            {
                if (HttpContext.Current.Request.QueryString["PageNo"] != null && HttpContext.Current.Request.QueryString["PageNo"].ToString() != "")
                {
                    Obj_Book.EndIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["PageNo"].ToString()) * 9;
                    Obj_Book.StartIndex = (Obj_Book.EndIndex - 9) + 1;
                }
                else
                {
                    HttpContext.Current.Session["num"] = "9";
                    Obj_Book.EndIndex = 9;
                    Obj_Book.StartIndex = 1;
                }
            }
        }
        else if (LoadMore == "LoadMore")
        {
            Obj_Book.StartIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 1;
            Obj_Book.EndIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 9;
            HttpContext.Current.Session["num"] = Obj_Book.EndIndex.ToString();
        }        

        dt = Obj_Book.getBookByCategoryLimited(Sortby, For);

        string str = "";

        if (dt.Rows.Count > 0)
        {
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
                        GenerateThumbnailStatic(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "");
                    }
                }
                else
                {
                    img = "images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (Language == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    //bookDetailUrl += "/" + dr["BookID"];
                }
                else if (Language == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    //bookDetailUrl += "/" + dr["BookID"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    //bookDetailUrl += "/" + dr["BookID"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0 && Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    //str = str + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "'  alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223'/></a>";
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223'/></a>";
                }
                else
                {
                    //str = str + "<div class='bookimg'>" + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "' alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223' /></a>";
                    str = str + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                //str = str + "<div class='namkl2'><a href = 'Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                str = str + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                if (Convert.ToBoolean(dr["IseBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFree"]) == true)
                    {
                        str = str + "<div class='namkl' style=\"width: 50%;float: left;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='namkl' style=\"width: 50%;float: left;\">$" + dr["FinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 50%;float: left;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsPaperBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFreePaper"]) == true)
                    {
                        str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='namkl' style=\"width: 45%;float: left;text-align: right;padding-right: 5px;\">$" + dr["PaperBookFinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<a href='Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                "</div>";
                }
                else
                {
                    str = str + "<a href='Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }
            }
            //str = str + "</div>";
        }        
        if (dt.Rows.Count < 9)
        {
            str += "<div id=\"divNoData\" onload=\"hideAlert()\"></div>";
        }
        if (dt.Rows.Count == 0)
        {
            //string NoData = "<div id=\"divNoData\" style=\"Color:red;\" onload=\"hideAlert()\">No Record Found</div>";
            string NoData = "";
            return NoData;
        }
        return str;
    }

}