using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
using System.Text.RegularExpressions;
using Localization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

public partial class Category : System.Web.UI.Page
{
    BookBAL Obj_Book = new BookBAL();
    Security S = new Security();
    CategoryBAL catBAL = new CategoryBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                objPurchase.BookID = Convert.ToInt16(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["fbid"])).ToString());

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
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);
                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book already exists in your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                    }
                    else
                    {
                        objPurchase.MoveToBookPurchase_freebook_website();
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);
                        //}                                        
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book added successfully to your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                    }
                }
                else
                {
                    objPurchase.MoveToBookPurchase_freebook_website();
                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);
                    //}                    
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book added successfully to your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
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
                objPurchase.BookID = Convert.ToInt16(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString());
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
                    Obj_bookOrder.BookID = Convert.ToInt32(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString());
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
                            //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                            //}
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Bookisalreadyinyourcartlist", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                        }
                        else
                        {
                            result = Obj_bookOrder.InsertCustomerCart1();
                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString().ToString() + "");
                        }
                    }
                    else
                    {
                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                        result = Obj_bookOrder.InsertCustomerCart1();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString().ToString() + "");

                    }
                }
            }

            else
            {
                Session["AddToCart"] = null;
                Obj_bookOrder.OrderDate = System.DateTime.Now;
                Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                Obj_bookOrder.BookID = Convert.ToInt32(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString());
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
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                        //}
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Bookisalreadyinyourcartlist", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                    }
                    else
                    {
                        result = Obj_bookOrder.InsertCustomerCart1();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString().ToString() + "");
                    }
                }
                else
                {
                    string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                    Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                    result = Obj_bookOrder.InsertCustomerCart1();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString().ToString() + "");

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

                    if (Add == HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString())
                    {
                        cnt++;
                    }
                }
                if (cnt > 0)
                {
                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Bookisalreadyinyourcartlist", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                }
                else
                {
                    Session["AddToCart"] = Session["AddToCart"] + "," + HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
            }
            else
            {
                Session["AddToCart"] = HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString();
                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
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
        {
            
        }
    }

    private void GenerateThumbnails(int height, int width, string sourcePath, string targetPath)
    {
        try
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
        catch (Exception ex)
        { }
    }

    private void BindData()
    {
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        viewallbook = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        newrelease = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        buynowtitle = Localization.ResourceManager.GetString("buynow", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        isfreetitle = Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        BookBAL Obj_Book1 = new BookBAL();
        catBAL.LanguageID = 1;
        Obj_Book1.LanguageID = 1;

        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book1.LanguageID = 2;
                catBAL.LanguageID = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book1.LanguageID = 1;
                catBAL.LanguageID = 1;
            }
        }
        else
        {
            Obj_Book1.LanguageID = 1;
            catBAL.LanguageID = 1;
        }

        DataTable DT = catBAL.SelectAllCartegory();

        if (DT != null && DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string subPath = "~/Category";
                string imgPath = "" + DT.Rows[i]["CImagePath"].ToString();
                if (!File.Exists(Server.MapPath(subPath + "/new_" + imgPath)) && !string.IsNullOrEmpty(imgPath))
                {
                    try
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "\\Category\\" + imgPath + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "\\Category\\" + imgPath + "");
                        //GenerateThumbnails(250, 400, (Server.MapPath(subPath + "/" + imgPath)), subPath + "/" + "new_" + imgPath);
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            rptRecords1.DataSource = DT;
            rptRecords1.DataBind();

            rptRecords1.Visible = true;
        }

        Obj_Book1.EndIndex = 8;
        Obj_Book1.StartIndex = 1;

        DataTable dt = Obj_Book1.get_all_book_website1();

        string a1 = "<div class='panel'>" +

                    "</div>";
        string str = "";

        if (dt.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if (i % 4 == 0)
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
                if (Obj_Book1.LangaugeID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    //bookDetailUrl += "/" + dr["BookID"];
                }
                else if (Obj_Book1.LangaugeID == 1)
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
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='/"+ img + "'  alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223'/></a>";
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
                    str = str + "<a href='" + System.Configuration.ConfigurationManager.AppSettings["SiteUrl"] + "Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                "</div>";
                }
                else
                {
                    str = str + "<a href='" + System.Configuration.ConfigurationManager.AppSettings["SiteUrl"] + "Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }
            }
            str = str + "<div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div>";
        }
        div_book.InnerHtml = a1 + str;
    }
}