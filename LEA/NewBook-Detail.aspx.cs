using BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

public partial class NewBook_Detail : System.Web.UI.Page
{
    BookBAL Obj_book = new BookBAL();
    Security S = new Security();
    BookOrderBAL Obj_bookOrder = new BookOrderBAL();
    WebsiteSettingsBAL objWebSetting = new WebsiteSettingsBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
        {
            Session["bookID"] = "";
            Session["bookID"] = Request.QueryString["id"];
            if (Request.QueryString["l"] == "en-US")
            {
                Response.Redirect(Config.WebSiteMain + "us/book-detail");
            }
            else if (Request.QueryString["l"] == "es-ES")
            {
                Response.Redirect(Config.WebSiteMain + "book-detail");
            }
            else
            {
                Response.Redirect(Config.WebSiteMain + "us/book-detail");
            }
        }
        //if (Request.Url.ToString().Contains("/us/"))
        //{
        //    Response.Redirect(Request.Url.ToString().Replace("/us", ""));
        //}
        if (Session["ErrorText"] != null)
        {
            lblError.Text = Session["ErrorText"].ToString();
            Session["ErrorText"] = null;
        }
        if (!IsPostBack)
        {
            this.Form.DefaultButton = btn_submit.UniqueID;
            if (Request.QueryString["mylib"] != null && Request.QueryString["mylib"].ToString() != "")
            {
                div_addtocard.Visible = false;
                aBuyBook.Visible = false;
            }
            //if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            if (Session["bookID"] != null && Session["bookID"].ToString() != "")
            {
                Bind_detail();
                Bind_BookRatting();
            }
        }
    }

    public void Bind_BookRatting()
    {

        string rattext = "rating";
        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            rattext = "Puntuación";
        }
        else
        {
            rattext = "rating";
        }
        DataTable dt = new DataTable();
        Obj_book.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());
        dt = Obj_book.get_book_ratting();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["total_review"].ToString() == "1")
            {
                lbl_Totalrating.Text = "(" + dt.Rows[0]["total_review"] + "" + rattext + ")";
            }
            else
            {
                lbl_Totalrating.Text = "(" + dt.Rows[0]["total_review"] + " " + rattext + ")";
            }

            int totalstar = Convert.ToInt32(dt.Rows[0]["ratting"].ToString());
            if (totalstar > 0)
            {
                img_rate1.Src = "images/big-ylw.png";
            }
            if (totalstar > 1)
            {
                img_rate2.Src = "images/big-ylw.png";
            }
            if (totalstar > 2)
            {
                img_rate3.Src = "images/big-ylw.png";
            }
            if (totalstar > 3)
            {
                img_rate4.Src = "images/big-ylw.png";
            }
            if (totalstar > 4)
            {
                img_rate5.Src = "images/big-ylw.png";
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

    public void Bind_detail()
    {
        DataTable dtWeb = new DataTable();
        dtWeb = objWebSetting.GetAllWebseetings();
        if (dtWeb != null && dtWeb.Rows.Count > 0)
        {
            lblNumber.Text = dtWeb.Rows[0]["BookStorePhone"].ToString();
        }

        string free = "Free";

        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            free = "gratis";
        }
        else
        {
            free = "Free";
        }
        DataTable dt = new DataTable();
        var bookId = Obj_book.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
        {
            Obj_book.LanguageID = 2;
        }
        else
        {
            Obj_book.LanguageID = 1;
        }
        dt = Obj_book.getBookDetails();
        if (dt.Rows.Count > 0)
        {
            eBook.Visible = Convert.ToBoolean(dt.Rows[0]["IseBook"].ToString());
            paperBook.Visible = Convert.ToBoolean(dt.Rows[0]["IsPaperBook"].ToString());
            imgeBook.AlternateText = lbliseBook.Text = dt.Rows[0]["IseBook"].ToString() == "True" ? "Yes" : "No";
            imgPaperBook.AlternateText = lblispaperBook.Text = dt.Rows[0]["IsPaperBook"].ToString() == "True" ? "Yes" : "No";
            imgeBook.ImageUrl = eBook.Visible ? "images/icon_completed.png" : "images/erroe-cancel.png";
            imgPaperBook.ImageUrl = paperBook.Visible ? "images/icon_completed.png" : "images/erroe-cancel.png";

            if (eBook.Visible && paperBook.Visible)
            {
                ddlBookType.Items.Add(new ListItem("eBook + Paper Book", "0"));
            }
            if (eBook.Visible)
            {
                ddlBookType.Items.Add(new ListItem("eBook", "1"));
            }
            if (paperBook.Visible)
            {
                ddlBookType.Items.Add(new ListItem("Paper Book", "2"));
            }

            lbltit.Text = dt.Rows[0]["Title1"].ToString();
            lblpro_id.Text = Obj_book.BookID.ToString();
            lbl_bookname.Text = dt.Rows[0]["Title1"].ToString();
            lbl_author.Text = dt.Rows[0]["Autoher"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["IsFree"]) == true && eBook.Visible)
            {
                lbl_price.Text = "";

                lbl_finalprice.Text = free.ToString();
                div_wislist.Visible = false;

                lbladdtocart.Visible = false;
                lbladdtolib.Visible = true;

                ViewState["IsFree"] = "True";
                //btn_addcart.Text = "Add to Library";
                aBuyBook.HRef = "" + Config.WebSiteMain + "Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            else if (Convert.ToDouble(dt.Rows[0]["DiscountPrice"]) != 0.0)
            {
                lbl_price.Text = "$" + dt.Rows[0]["Price"].ToString();
                lbl_finalprice.Text = "$" + dt.Rows[0]["FinalPrice"].ToString();
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);
                //aBuyBook.HRef = "" + Config.WebSiteMain + "Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            else
            {
                divprice.Visible = false;
                lbl_finalprice.Text = "$" + dt.Rows[0]["FinalPrice"].ToString();
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);
                //aBuyBook.HRef = "" + Config.WebSiteMain + "Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }

            if (Convert.ToBoolean(dt.Rows[0]["IsFree"]) == true && paperBook.Visible)
            {
                lbl_paperBookprice.Text = "";

                lbl_paperBookfinalprice.Text = free.ToString();
                //div_wislist.Visible = false;

                //lbladdtocart.Visible = false;
                //lbladdtolib.Visible = true;

                ViewState["IsFree"] = "True";
                //btn_addcart.Text = "Add to Library";
                aBuyBook.HRef = "" + Config.WebSiteMain + "Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            else if (dt.Rows[0]["PaperBookDiscount"].ToString() != "0")//(Convert.ToDouble(dt.Rows[0]["PaperBookDiscount"]) != 0.0)
            {
                lbl_paperBookprice.Text = "$" + dt.Rows[0]["PaperBookPrice"].ToString();
                lbl_paperBookfinalprice.Text = "$" + dt.Rows[0]["PaperBookFinalPrice"].ToString();
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);
                //aBuyBook.HRef = "" + Config.WebSiteMain + "Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            else
            {
                divpaperBookprice.Visible = false;
                lbl_paperBookfinalprice.Text = "$" + dt.Rows[0]["PaperBookFinalPrice"].ToString();
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);
                //aBuyBook.HRef = "" + Config.WebSiteMain + "Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }


            string img = "";
            string fileName = "Book/" + dt.Rows[0]["CategoryID"] + '/' + dt.Rows[0]["ImagePath"] + "";
            if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
            {
                img = "Book/" + dt.Rows[0]["CategoryID"] + '/' + dt.Rows[0]["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
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

            img_book.Src = img;

            string str = "";
            str = str + "<div class='descpnl'>Description</div>" +
                        "<p>" + dt.Rows[0]["Description1"].ToString() + "</p>";
            div_description.InnerHtml = str;
        }
    }

    protected void btn_WishList_Click(object sender, EventArgs e)
    {
        int count = 0;
        int result = 0;
        string wishid = "";
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();

            int cnt1 = 0;

            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                if (Session["bookID"].ToString() != null && Session["bookID"].ToString().ToString() != "")
                {
                    objPurchase.BookID = Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());
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
                        Session["AddToWishlist"] = null;
                        Obj_bookOrder.OrderDate = System.DateTime.Now;
                        Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                        Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());

                        DataTable DT = Obj_bookOrder.GetWishList();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                                {
                                    count++;
                                }

                            }
                            if (count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
                            }
                            else
                            {
                                result = Obj_bookOrder.InsertCustomerWishList1();
                                wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                                Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                            }
                        }
                        else
                        {
                            result = Obj_bookOrder.InsertCustomerWishList1();
                            wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                            Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                        }
                        //Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");

                    }
                }
                else
                {
                    Session["AddToWishlist"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());

                    DataTable DT = Obj_bookOrder.GetWishList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                            {
                                count++;
                            }

                        }
                        if (count > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
                        }
                        else
                        {
                            result = Obj_bookOrder.InsertCustomerWishList1();
                            wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                            Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                        }
                    }
                    else
                    {
                        result = Obj_bookOrder.InsertCustomerWishList1();
                        wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                        Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                    }
                    //Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");

                }
            }
        }
        else
        {

            if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
            {
                int cnt = 0;
                string[] str = Session["AddToWishlist"].ToString().Split(',');
                foreach (string Add in str)
                {

                    if (Add == Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString())
                    {
                        cnt++;
                    }
                }
                if (cnt > 0)
                {
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su lista de deseos');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
                    }
                }
                else
                {
                    Session["AddToWishlist"] = Session["AddToWishlist"] + "," + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString();
                    Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
            }
            else
            {
                Session["AddToWishlist"] = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString();
                Response.Redirect("" + Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }
    }

    public void addtoLibrary()
    {
        int count = 0;
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
            objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
            objPurchase.PurchaseDate = System.DateTime.Now;
            objPurchase.OrderID = 0;
            objPurchase.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());


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

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro agregado a su biblioteca');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library');", true);

                    }
                }
                else
                {
                    objPurchase.MoveToBookPurchase_freebook_website();
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro agregado a su biblioteca');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library');", true);

                    }

                    //Response.Redirect("" + Config.WebSiteMain + "MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["bookID"].ToString().ToString());
                }
            }
            else
            {
                objPurchase.MoveToBookPurchase_freebook_website();
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro agregado a su biblioteca');", true);

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library');", true);

                }

                //Response.Redirect("" + Config.WebSiteMain + "MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["bookID"].ToString().ToString());
            }
        }
        else
        {
            Response.Redirect("" + Config.WebSiteMain + "Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
    }

    protected void btn_addcart_Click(object sender, EventArgs e)
    {
        int counteBook = 0;
        int countPaper = 0;
        int result = 0;

        if (ViewState["IsFree"] != null && ViewState["IsFree"].ToString() == "True")
        {
            addtoLibrary();
        }
        else
        {
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();

            int cnt1 = 0;

            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                if (Session["bookID"].ToString() != null && Session["bookID"].ToString().ToString() != "")
                {
                    try
                    {
                        Obj_book.BookID = objPurchase.BookID = Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());
                    }
                    catch
                    {
                        Obj_book.BookID = objPurchase.BookID = Convert.ToInt32(Session["bookID"].ToString().ToString());
                    }
                }
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    objPurchase.LanguageID = 2;
                    Obj_book.LanguageID = 2;
                }
                else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                {
                    objPurchase.LanguageID = 1;
                    Obj_book.LanguageID = 1;
                }
                DataTable Dt = objPurchase.getUserLibrary();
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        var tableBook = Obj_book.getBookDetails();
                        var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                        var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                        if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                        {
                            if (iseBook == Convert.ToBoolean(Dt.Rows[i]["IseBook"]))
                                cnt1++;
                        }

                    }
                    //if (cnt1 > 0)
                    //{
                    //    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //    {

                    //        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('Usted ya ha comprado este libro electrónico. Qué quieres mover en mi biblioteca?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

                    //    }
                    //    else
                    //    {

                    //        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('You have already purchased this ebook. do you want to move in my library?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

                    //    }
                    //}
                    //else
                    //{
                    Session["AddToCart"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());
                    DataTable dtBookDetail = new DataTable();
                    Obj_book.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());
                    Obj_book.LanguageID = 1;
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        Obj_book.LanguageID = 2;
                    }
                    else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                    {
                        Obj_book.LanguageID = 1;
                    }
                    dtBookDetail = Obj_book.getBookDetails();

                    //Obj_bookOrder.IseBook = Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString());
                    //Obj_bookOrder.IspaperBook = Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString());
                    //Obj_bookOrder.Quantity = 1;
                    //Obj_bookOrder.InsertCustomerCart1();

                    DataTable DT = Obj_bookOrder.GetCartList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            var tableBook = Obj_book.getBookDetails();
                            var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                            var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                            {
                                if (iseBook == Convert.ToBoolean(DT.Rows[i]["IseBook"]))// && isPaperBook == Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                    counteBook++;
                                if (isPaperBook == Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                    countPaper++;
                            }
                            //if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                            //{
                            //    count++;
                            //}
                        }
                        //if (count > 0)
                        //{
                        //    //Message1("You already have this book in your cart");
                        //    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                        //    }
                        //    else
                        //    {
                        //        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                        //    }
                        //}
                        //else
                        //{
                        if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                        {
                            var x = 0;
                            if (counteBook == 0 && cnt1 == 0)
                            {
                                x++;
                                Obj_bookOrder.IseBook = true;
                                Obj_bookOrder.IspaperBook = false;
                                Obj_bookOrder.Quantity = 0;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            if (countPaper == 0)
                            {
                                x++;
                                Obj_bookOrder.IseBook = false;
                                Obj_bookOrder.IspaperBook = true;
                                Obj_bookOrder.Quantity = 1;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            if (x == 0)
                            {
                                //Message1("You already have this book in your cart");
                                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist'); window.location='" + Request.Url + ";", true);
                                    Session["ErrorText"] = "Libro ya se ha agregado en su cartlist";
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist'); window.location='" + Request.Url + ";", true);
                                    Session["ErrorText"] = "Book is already added in your cartlist";
                                }
                            }
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && counteBook == 0 && cnt1 == 0)
                        {
                            Obj_bookOrder.IseBook = true;
                            Obj_bookOrder.IspaperBook = false;
                            Obj_bookOrder.Quantity = 0;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()) && countPaper == 0)
                        {
                            Obj_bookOrder.IseBook = false;
                            Obj_bookOrder.IspaperBook = true;
                            Obj_bookOrder.Quantity = 1;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        //result = Obj_bookOrder.InsertCustomerCart1();
                        //Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Security.AES256Encrypt(Session["UserID"].ToString()) + "");
                        if (result > 0)
                            Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                        else
                            Response.Redirect(Request.Url.ToString() + "");
                        //}
                    }
                    else
                    {
                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                        if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                        {
                            if (counteBook == 0 && cnt1 == 0)
                            {
                                Obj_bookOrder.IseBook = true;
                                Obj_bookOrder.IspaperBook = false;
                                Obj_bookOrder.Quantity = 0;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            Obj_bookOrder.IseBook = false;
                            Obj_bookOrder.IspaperBook = true;
                            Obj_bookOrder.Quantity = 1;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && counteBook == 0 && cnt1 == 0)
                        {
                            Obj_bookOrder.IseBook = true;
                            Obj_bookOrder.IspaperBook = false;
                            Obj_bookOrder.Quantity = 0;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                        {
                            Obj_bookOrder.IseBook = false;
                            Obj_bookOrder.IspaperBook = true;
                            Obj_bookOrder.Quantity = 1;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        //result = Obj_bookOrder.InsertCustomerCart1();
                        //Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Security.AES256Encrypt(Session["UserID"].ToString()) + "");
                        if (result > 0)
                            Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                        else
                            Response.Redirect(Request.Url.ToString() + "");
                    }
                    //}
                }

                else
                {
                    Session["AddToCart"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());

                    DataTable dtBookDetail = new DataTable();
                    Obj_book.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString());
                    Obj_book.LanguageID = 1;
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        Obj_book.LanguageID = 2;
                    }
                    else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                    {
                        Obj_book.LanguageID = 1;
                    }
                    dtBookDetail = Obj_book.getBookDetails();

                    //Obj_bookOrder.IseBook = Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString());
                    //Obj_bookOrder.IspaperBook = Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString());
                    //Obj_bookOrder.Quantity = 1;

                    //Obj_bookOrder.InsertCustomerCart1();

                    DataTable DT = Obj_bookOrder.GetCartList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                            {
                                counteBook++;
                            }
                        }
                        if (counteBook > 0)
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
                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                Obj_bookOrder.IseBook = true;
                                Obj_bookOrder.IspaperBook = false;
                                Obj_bookOrder.Quantity = 0;
                                result = Obj_bookOrder.InsertCustomerCart1();
                                Obj_bookOrder.IseBook = false;
                                Obj_bookOrder.IspaperBook = true;
                                Obj_bookOrder.Quantity = 1;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()))
                            {
                                Obj_bookOrder.IseBook = true;
                                Obj_bookOrder.IspaperBook = false;
                                Obj_bookOrder.Quantity = 0;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                Obj_bookOrder.IseBook = false;
                                Obj_bookOrder.IspaperBook = true;
                                Obj_bookOrder.Quantity = 1;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            //result = Obj_bookOrder.InsertCustomerCart1();
                            if (result > 0)
                                Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                            else
                                Response.Redirect(Request.Url.ToString() + "");
                        }
                    }
                    else
                    {
                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                        if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                        {
                            Obj_bookOrder.IseBook = true;
                            Obj_bookOrder.IspaperBook = false;
                            Obj_bookOrder.Quantity = 0;
                            result = Obj_bookOrder.InsertCustomerCart1();
                            Obj_bookOrder.IseBook = false;
                            Obj_bookOrder.IspaperBook = true;
                            Obj_bookOrder.Quantity = 1;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()))
                        {
                            Obj_bookOrder.IseBook = true;
                            Obj_bookOrder.IspaperBook = false;
                            Obj_bookOrder.Quantity = 0;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                        {
                            Obj_bookOrder.IseBook = false;
                            Obj_bookOrder.IspaperBook = true;
                            Obj_bookOrder.Quantity = 1;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        //result = Obj_bookOrder.InsertCustomerCart1();
                        if (result > 0)
                            Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                        else
                            Response.Redirect(Request.Url.ToString() + "");
                    }
                }
                //Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

            }
            else
            {
                if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                {
                    int cnt = 0;
                    string[] str = Session["AddToCart"].ToString().Split(',');
                    foreach (string Add in str)
                    {

                        if (Add == Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString())
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
                        Session["AddToCart"] = Session["AddToCart"] + "," + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString();
                        Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    }
                }
                else
                {
                    Session["AddToCart"] = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Session["bookID"].ToString()))).ToString();
                    Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
                //Response.Redirect("" + Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //Insert_Review
        Obj_book.BookID = Convert.ToInt32(Session["bookID"].ToString().ToString());
        if (Session["UserID"] != null)
        {
            Obj_book.UserID = Convert.ToInt32(Session["UserID"].ToString());
        }
        else
        {
            Obj_book.UserID = 0;
        }
        Obj_book.Name = txt_nickname.Text;
        Obj_book.Summary = txt_summary.Text.ToString();
        Obj_book.Review = txt_review.Text;
        if (rdo_1.Checked)
        {
            Obj_book.Ratting = 1;
        }
        if (rdo_2.Checked)
        {
            Obj_book.Ratting = 2;
        }
        if (rdo_3.Checked)
        {
            Obj_book.Ratting = 3;
        }
        if (rdo_4.Checked)
        {
            Obj_book.Ratting = 4;
        }
        if (rdo_5.Checked)
        {
            Obj_book.Ratting = 5;
        }
        Obj_book.Insert_ReviewRatting();
        Response.Redirect(Request.RawUrl);
    }
}