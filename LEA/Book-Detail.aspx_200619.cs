using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Collections.Specialized;
using System.Web.Services;

public partial class Book_Detail : System.Web.UI.Page
{
    BookBAL Obj_book = new BookBAL();
    Security S = new Security();
    BookOrderBAL Obj_bookOrder = new BookOrderBAL();
    WebsiteSettingsBAL objWebSetting = new WebsiteSettingsBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null)
        {
            Check_User_Session.Value = "No Session";
        }
        else
        {
            Check_User_Session.Value = "Session";
        }
        if (Session["ErrorText"] != null)
        {
            lblError.Text = Session["ErrorText"].ToString();
            Session["ErrorText"] = null;
        }
        if (!IsPostBack)
        {
            if (Session["RedirectUrl"] != null)
            {
                Session.Remove("RedirectUrl");
            }
            this.Form.DefaultButton = btn_submit.UniqueID;
            if (Request.QueryString["mylib"] != null && Request.QueryString["mylib"].ToString() != "")
            {
                div_addtocard.Visible = false;
                aBuyBook.Visible = false;
            }
            if (Request.QueryString["title"] != null && Request.QueryString["title"].ToString() != "")
            {
                //this.Page.Title = Request.QueryString["title"].ToString(); 
                if (Request.QueryString["tabtype"] != null)
                {
                    var tabtype = Request.QueryString["tabtype"].ToString();
                }
                Session["Url"] = Request.QueryString["title"];
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
        //Obj_book.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
        Obj_book.LanguageID = 1;
        //Obj_book.Title = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8).Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();

        var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
        setTitle = setTitle.Replace("-", " ");
        setTitle = setTitle.Replace("___", ":");
        setTitle = setTitle.Replace("__", "-");
        setTitle = setTitle.Replace("_", ".");
        //lblError.Text = setTitle;
        Obj_book.Title = setTitle;
        this.Page.Title = setTitle.ToUpper();

        dt = Obj_book.GetBookDetailByTitle();
        if (dt.Rows.Count > 0)
        {
            Obj_book.BookID = Convert.ToInt32(dt.Rows[0]["BookID"]);
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
                lblRatingoutofall.Text = totalstar.ToString() + "/5";
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
        string desc = "Description";

        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            free = "gratis";
            desc = "Descripción";
        }
        else

        {
            free = "Free";
            desc = "Description";
        }
        DataTable dt = new DataTable();

        var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
        setTitle = setTitle.Replace("-", " ");
        setTitle = setTitle.Replace("__", "-");
        setTitle = setTitle.Replace("_", ".");
        //lblError.Text = setTitle;
        Obj_book.Title = setTitle;

        //Obj_book.Title = Request.QueryString["title"].ToString().Replace("-", " ").Replace("_", ".").ToString();
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
        {
            Obj_book.LanguageID = 2;
        }
        else
        {
            Obj_book.LanguageID = 1;
        }
        dt = Obj_book.GetBookDetailByTitle();
        //if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        //{
        //    Obj_book.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
        //    dt = Obj_book.getBookDetails();
        //}

        if (dt.Rows.Count > 0)
        {
            var bookId = Obj_book.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
            

            eBook.Visible = Convert.ToBoolean(dt.Rows[0]["IseBook"].ToString());
            paperBook.Visible = Convert.ToBoolean(dt.Rows[0]["IsPaperBook"].ToString());
            imgeBook.AlternateText = lbliseBook.Text = dt.Rows[0]["IseBook"].ToString() == "True" ? "Yes" : "No";
            lbleBook.ForeColor = lbliseBook.ForeColor = dt.Rows[0]["IseBook"].ToString() == "True" ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            imgPaperBook.AlternateText = lblispaperBook.Text = dt.Rows[0]["IsPaperBook"].ToString() == "True" ? "Yes" : "No";
            lblispaperBook.ForeColor = lblpaperBook.ForeColor = dt.Rows[0]["IsPaperBook"].ToString() == "True" ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            if (dt.Rows[0]["IsPaperBook"].ToString() == "True")
            {
                if (!string.IsNullOrEmpty(dt.Rows[0]["Quantity"].ToString()))
                {
                    if (dt.Rows[0]["Quantity"].ToString() != "0")
                    {
                        lblStock.Text = "In Stock (" + dt.Rows[0]["Quantity"].ToString() + ")";
                        lblStock.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblStock.Text = "Out Of Stock";
                        lblStock.ForeColor = System.Drawing.Color.Red;
                        paperBook.Visible = false;
                    }
                }
                else
                {
                    lblStock.Text = "Out Of Stock";
                    lblStock.ForeColor = System.Drawing.Color.Red;
                    paperBook.Visible = false;
                }
            }
            imgeBook.ImageUrl = eBook.Visible ? "images/icon_completed.png" : "images/erroe-cancel.png";
            imgPaperBook.ImageUrl = paperBook.Visible ? "images/icon_completed.png" : "images/erroe-cancel.png";




            if (eBook.Visible)
            {
                ddlBookType.Items.Add(new ListItem("eBook", "1"));
                if (Request.QueryString["tabtype"] != null)
                {
                    if (Request.QueryString["tabtype"].ToString().ToLower() == ("eBook").ToLower())
                    {
                        ddlBookType.SelectedValue = "1";
                        paperBook.Attributes.Add("style", "display:none");

                    }
                }
            }
            if (paperBook.Visible)
            {
                ddlBookType.Items.Add(new ListItem("Paper Book", "2"));
                if (Request.QueryString["tabtype"] != null)
                {
                    if (Request.QueryString["tabtype"].ToString().ToLower() == ("Paper Book").ToLower())
                    {
                        ddlBookType.SelectedValue = "2";
                        eBook.Attributes.Add("style", "display:none");
                    }
                }
            }
            if (eBook.Visible && paperBook.Visible)
            {
                ddlBookType.Items.Add(new ListItem("eBook + Paper Book", "0"));
            }
            if (!eBook.Visible && !paperBook.Visible)
            {
                ddlBookType.Visible = false;
                aBuyBook.Visible = false;
                //btn_addcart.Visible = false;
                lnk_wishlist.Visible = false;
            }
            var freeeBook = Convert.ToBoolean(dt.Rows[0]["IsFree"].ToString());
            var freePaperBook = Convert.ToBoolean(dt.Rows[0]["IsFreePaper"].ToString());
            if (freeeBook)
            {
                aBuyBook.Visible = false;
                //btn_addcart.Visible = false;
                lnk_wishlist.Visible = false;
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
                //lbladdtolib.Visible = true;
                ViewState["IsFree"] = "True";
                aBuyBook.HRef = Config.WebSiteMain + "Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            else if (Convert.ToDouble(dt.Rows[0]["DiscountPrice"]) != 0.0)
            {
                lbl_price.Text = "$" + dt.Rows[0]["Price"].ToString();
                lbl_finalprice.Text = "$" + dt.Rows[0]["FinalPrice"].ToString();
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);
            }
            else
            {
                divprice.Visible = false;
                lbl_finalprice.Text = "$" + dt.Rows[0]["FinalPrice"].ToString();
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);

            }

            if (Convert.ToBoolean(dt.Rows[0]["IsFreePaper"]) == true && paperBook.Visible)
            {
                lbl_paperBookprice.Text = "";

                lbl_paperBookfinalprice.Text = free.ToString();
                ViewState["IsFree"] = "True";
                aBuyBook.HRef = Config.WebSiteMain + "Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(bookId))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            else if (dt.Rows[0]["PaperBookDiscount"].ToString() != "0")//(Convert.ToDouble(dt.Rows[0]["PaperBookDiscount"]) != 0.0)
            {
                lbl_paperBookprice.Text = "$" + dt.Rows[0]["PaperBookPrice"].ToString().Replace(",", ".");
                lbl_paperBookfinalprice.Text = "$" + dt.Rows[0]["PaperBookFinalPrice"].ToString().Replace(",", ".");
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);
            }
            else
            {
                divpaperBookprice.Visible = false;
                lbl_paperBookfinalprice.Text = "$" + dt.Rows[0]["PaperBookFinalPrice"].ToString().Replace(",", ".");
                aBuyBook.ServerClick += new EventHandler(btn_addcart_Click);
                lbladdtocart.Visible = true;
                // aBuyBook.Visible = true;
                div_wislist.Visible = true;
                lnk_wishlist.Visible = true;
                if (Convert.ToBoolean(dt.Rows[0]["IsFree"]) == true && eBook.Visible)
                {
                    lbladdtolib.Visible = true;
                }
            }

            if (lnk_wishlist.Visible == false && lbladdtocart.Visible == false && lbladdtolib.Visible == false && aBuyBook.Visible == false)
            {
                divadd.Attributes.Add("style", "border-bottom: 0px");
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

            img_book.Src = Config.WebSiteMain + img;

            string str = "";
            str = str + "<div class='descpnl'>" + desc + "</div>" +
                        "<p>" + dt.Rows[0]["Description1"].ToString() + "</p>";
            div_description.InnerHtml = str;

            #region Bind Review List

            if (Session["UserID"] != null)
            {
                DataTable dtGetUserReview = DAL.SqlHelper.ExecuteDataTable(CommandType.Text, "SELECT * FROM ReviewRatting WHERE UserID = " + Convert.ToInt32(Session["UserID"].ToString()) + " AND BookID = " + Convert.ToInt32(lblpro_id.Text));
                if (dtGetUserReview.Rows.Count == 0)
                {
                    lnk_writereview.Visible = true;
                }
            }
            else
            {
                Session["RedirectUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
                lnk_writereview.Visible = true;
            }



            string reviewlist = "";

            DataTable dtList = Obj_book.GerReviewList(Convert.ToInt32(lblpro_id.Text));
            if (dtList.Rows.Count > 0)
            {
                if (dtList.Rows.Count > 2)
                {
                    divReadmore.Visible = true;
                }
                else
                {
                    divReadmore.Visible = false;
                }
                for (int i = 0; i < dtList.Rows.Count; i++)
                {
                    reviewlist += "<div style=\"Padding-top:20px;\">";

                    reviewlist += "<div>" + dtList.Rows[i]["Name"].ToString().Trim() + "</div>";
                    reviewlist += "<div style=\"float: left; width: 100%;margin-bottom: 10px;\">";
                    reviewlist += "<div style=\"float:left;padding-right: 2%;\"><img src=\"" + Config.WebSiteMain + "images/" + dtList.Rows[i]["Ratting"].ToString().Trim() + "_star.png\" height=\"35px\" ></div>";
                    reviewlist += "<div style=\"padding: 6px;\"><span>" + Convert.ToDateTime(dtList.Rows[i]["CreatedDate"].ToString()).ToString("dd MMMM yyyy") + "</span></div>";
                    reviewlist += "</div>";
                    reviewlist += "<div><span>" + dtList.Rows[i]["Summary"].ToString() + "</span></div>";
                    reviewlist += "<div style=\"margin-top: 10px;\"><span>" + dtList.Rows[i]["Review"].ToString() + "</span></div>";
                    reviewlist += "</div>";
                }
            }
            else
            {
                divReadmore.Visible = false;
                reviewlist += "<div style=\"color:red;padding-top:20px;\">No record found</div>";
            }
            ltrReviewList.InnerHtml = reviewlist;
            #endregion
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
                if (Request.QueryString["title"] != null && Request.QueryString["title"].ToString() != "")
                {

                    var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
                    setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
                    setTitle = setTitle.Replace("__", "-");
                    setTitle = setTitle.Replace("_", ".");
                    //lblError.Text = setTitle;
                    Obj_book.Title = setTitle;

                    if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
                    {
                        Obj_book.LanguageID = 2;
                    }
                    else
                    {
                        Obj_book.LanguageID = 1;
                    }
                    DataTable dt = Obj_book.GetBookDetailByTitle();
                    if (dt.Rows.Count > 0)
                    {
                        //objPurchase.BookID = Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                        objPurchase.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
                    }
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
                            if (Convert.ToBoolean(Dt.Rows[i]["Isebook"]))
                            {
                                cnt1++;
                            }
                        }

                    }
                    if (cnt1 > 0)
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('Usted ya ha comprado este libro electrónico.')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('You have already purchased this ebook.')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);
                        }
                    }
                    else
                    {
                        Session["AddToWishlist"] = null;
                        Obj_bookOrder.OrderDate = System.DateTime.Now;
                        Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());

                        var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
                        setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
                        setTitle = setTitle.Replace("__", "-");
                        setTitle = setTitle.Replace("_", ".");
                        //lblError.Text = setTitle;
                        Obj_book.Title = setTitle;

                        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
                        {
                            Obj_book.LanguageID = 2;
                        }
                        else
                        {
                            Obj_book.LanguageID = 1;
                        }
                        DataTable dt = Obj_book.GetBookDetailByTitle();

                        //Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                        if (dt.Rows.Count > 0)
                        {
                            Obj_bookOrder.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
                            Obj_bookOrder.IseBook = Convert.ToBoolean(dt.Rows[0]["IseBook"].ToString());
                            Obj_bookOrder.IspaperBook = Convert.ToBoolean(dt.Rows[0]["IsPaperBook"].ToString());
                        }

                        DataTable DT = Obj_bookOrder.GetWishList();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                //if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                                //{
                                //    count++;
                                //}
                                if (Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                {
                                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                                    {
                                        count++;
                                    }
                                }

                            }
                            if (count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(ddlBookType.SelectedValue))
                                {
                                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(ddlBookType.SelectedValue))
                                    {
                                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);

                                        }
                                        else
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                                        }
                                    }
                                    else
                                    {
                                        if (ddlBookType.SelectedItem.Value == "1")
                                        {
                                            result = Obj_bookOrder.InsertCustomerWishList1();
                                        }
                                        else if (ddlBookType.SelectedItem.Value == "2")
                                        {
                                            result = Obj_bookOrder.InsertCustomerWishList1();
                                        }
                                        else
                                        {
                                            result = Obj_bookOrder.InsertCustomerWishList1();
                                        }
                                        wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                                        Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                                    }
                                }
                                // result = Obj_bookOrder.InsertCustomerWishList1();
                            }
                           
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(ddlBookType.SelectedValue))
                            {
                                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);
                                    
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                                }
                            }
                            else
                            {
                                if (ddlBookType.SelectedItem.Value == "1")
                                {
                                    result = Obj_bookOrder.InsertCustomerWishList1();
                                }
                                else if (ddlBookType.SelectedItem.Value == "2")
                                {
                                    result = Obj_bookOrder.InsertCustomerWishList1();
                                }
                                else
                                {
                                    result = Obj_bookOrder.InsertCustomerWishList1();
                                }
                                wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                                Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                            }
                            //result = Obj_bookOrder.InsertCustomerWishList1();
                           
                        }
                        //Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
                       
                    }
                }
                else
                {
                    Session["AddToWishlist"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    //Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());

                    var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
                    setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
                    setTitle = setTitle.Replace("__", "-");
                    setTitle = setTitle.Replace("_", ".");
                    //lblError.Text = setTitle;
                    Obj_book.Title = setTitle;

                    if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
                    {
                        Obj_book.LanguageID = 2;
                    }
                    else
                    {
                        Obj_book.LanguageID = 1;
                    }
                    DataTable dt = Obj_book.GetBookDetailByTitle();
                    if (dt.Rows.Count > 0)
                    {
                        Obj_bookOrder.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
                    }

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
                            if (string.IsNullOrEmpty(ddlBookType.SelectedValue))
                            {
                                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                                }
                            }
                            else
                            {
                                if (ddlBookType.SelectedItem.Value == "1")
                                {
                                    result = Obj_bookOrder.InsertCustomerWishList1();
                                }
                                else if (ddlBookType.SelectedItem.Value == "2")
                                {
                                    result = Obj_bookOrder.InsertCustomerWishList1();
                                }
                                else
                                {
                                    result = Obj_bookOrder.InsertCustomerWishList1();
                                }
                                wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                                Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                            }
                            //result = Obj_bookOrder.InsertCustomerWishList1();
                        }
                        
                    }
                    else
                    {
                        if (ddlBookType.SelectedItem.Value == "1")
                        {
                            result = Obj_bookOrder.InsertCustomerWishList1();
                        }
                        else if (ddlBookType.SelectedItem.Value == "1")
                        {
                            result = Obj_bookOrder.InsertCustomerWishList1();
                        }
                        else
                        {
                            result = Obj_bookOrder.InsertCustomerWishList1();
                        }
                        wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
                        Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
                        // result = Obj_bookOrder.InsertCustomerWishList1();

                    }
                  
                    //Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");

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
                    var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
                    setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
                    setTitle = setTitle.Replace("__", "-");
                    setTitle = setTitle.Replace("_", ".");
                    //lblError.Text = setTitle;
                    Obj_book.Title = setTitle;

                    if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
                    {
                        Obj_book.LanguageID = 2;
                    }
                    else
                    {
                        Obj_book.LanguageID = 1;
                    }
                    DataTable dt = Obj_book.GetBookDetailByTitle();
                    if (dt.Rows.Count > 0)
                    {
                        var bookID = (dt.Rows[0]["BookID"].ToString());

                        if (Add == bookID)
                        {
                            cnt++;
                        }
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
                    var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
                    setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
                    setTitle = setTitle.Replace("__", "-");
                    setTitle = setTitle.Replace("_", ".");
                    //lblError.Text = setTitle;
                    Obj_book.Title = setTitle;

                    if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
                    {
                        Obj_book.LanguageID = 2;
                    }
                    else
                    {
                        Obj_book.LanguageID = 1;
                    }
                    DataTable dt = Obj_book.GetBookDetailByTitle();
                    if (dt.Rows.Count > 0)
                    {
                        var bookID = (dt.Rows[0]["BookID"].ToString());
                        Session["AddToWishlist"] = Session["AddToWishlist"] + "," + Convert.ToInt32(bookID).ToString();
                        //Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                        if (Obj_book.LanguageID == 1)
                        {
                            Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=es-Es");
                        }
                        else
                        {
                            Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=en-Us");
                        }
                    }
                }
            }
            else
            {
                var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
                setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
                setTitle = setTitle.Replace("__", "-");
                setTitle = setTitle.Replace("_", ".");
                //lblError.Text = setTitle;
                Obj_book.Title = setTitle;

                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
                {
                    Obj_book.LanguageID = 2;
                }
                else
                {
                    Obj_book.LanguageID = 1;
                }
                DataTable dt = Obj_book.GetBookDetailByTitle();
                if (dt.Rows.Count > 0)
                {
                    var bookID = (dt.Rows[0]["BookID"].ToString());
                    Session["AddToWishlist"] = Convert.ToInt32(bookID).ToString();
                    if (Obj_book.LanguageID == 1)
                    {
                        Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=es-Es");
                    }
                    else
                    {
                        Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=en-Us");
                    }
                }
            }
        }
    }

    //protected void btn_WishList_Click(object sender, EventArgs e)
    //{
    //    int count = 0;
    //    int result = 0;
    //    int countebook = 0;
    //    int countpaperbook = 0;
    //    string wishid = "";
    //    if (Session["UserID"] != null && Session["UserID"].ToString() != "")
    //    {
    //        BookOrderBAL Obj_bookOrder = new BookOrderBAL();
    //        int cnt1 = 0;

    //        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
    //        {
    //            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
    //            objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
    //            objPurchase.PurchaseDate = System.DateTime.Now;
    //            objPurchase.OrderID = 0;
    //            if (Request.QueryString["title"] != null && Request.QueryString["title"].ToString() != "")
    //            {

    //                var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
    //                setTitle = setTitle.Replace("-", " ");setTitle = setTitle.Replace("___", ":");
    //                setTitle = setTitle.Replace("__", "-");
    //                setTitle = setTitle.Replace("_", ".");
    //                //lblError.Text = setTitle;
    //                Obj_book.Title = setTitle;

    //                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
    //                {
    //                    Obj_book.LanguageID = 2;
    //                }
    //                else
    //                {
    //                    Obj_book.LanguageID = 1;
    //                }
    //                DataTable dt = Obj_book.GetBookDetailByTitle();
    //                if (dt.Rows.Count > 0)
    //                {
    //                    //objPurchase.BookID = Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
    //                    objPurchase.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
    //                }
    //            }
    //            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //            {
    //                objPurchase.LanguageID = 2;
    //            }
    //            else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
    //            {
    //                objPurchase.LanguageID = 1;
    //            }
    //            DataTable Dt = objPurchase.getUserLibrary();
    //            if (Dt != null && Dt.Rows.Count > 0)
    //            {
    //                for (int i = 0; i < Dt.Rows.Count; i++)
    //                {
    //                    var tableBook = Obj_book.getBookDetails();
    //                    var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
    //                    var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
    //                    if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
    //                    {
    //                        if (iseBook == Convert.ToBoolean(Dt.Rows[i]["BookID"]))
    //                            cnt1++;
    //                    }

    //                }
    //                if (cnt1 > 0)
    //                {
    //                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('Usted ya ha comprado este libro electrónico. Qué quieres mover en mi biblioteca?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);
    //                    }
    //                    else
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('You have already purchased this ebook. do you want to move in my library?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);
    //                    }
    //                }
    //                else
    //                {
    //                    Session["AddToWishlist"] = null;
    //                    Obj_bookOrder.OrderDate = System.DateTime.Now;
    //                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());

    //                    var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
    //                    setTitle = setTitle.Replace("-", " ");setTitle = setTitle.Replace("___", ":");
    //                    setTitle = setTitle.Replace("__", "-");
    //                    setTitle = setTitle.Replace("_", ".");
    //                    //lblError.Text = setTitle;
    //                    Obj_book.Title = setTitle;

    //                    if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
    //                    {
    //                        Obj_book.LanguageID = 2;
    //                    }
    //                    else
    //                    {
    //                        Obj_book.LanguageID = 1;
    //                    }
    //                    DataTable dt = Obj_book.GetBookDetailByTitle();

    //                    //Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
    //                    if (dt.Rows.Count > 0)
    //                    {
    //                        Obj_bookOrder.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
    //                    }

    //                    DataTable DT = Obj_bookOrder.GetWishList();
    //                    if (DT != null && DT.Rows.Count > 0)
    //                    {
    //                        for (int i = 0; i < DT.Rows.Count; i++)
    //                        {
    //                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
    //                            {
    //                                count++;
    //                            }

    //                        }
    //                        if (count > 0)
    //                        {
    //                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
    //                        }
    //                        else
    //                        {

    //                            result = Obj_bookOrder.InsertCustomerWishList1();
    //                            wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
    //                            Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
    //                        }
    //                    }
    //                    else
    //                    {
    //                        result = Obj_bookOrder.InsertCustomerWishList1();
    //                        wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
    //                        Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
    //                    }
    //                    //Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");

    //                }
    //            }
    //            else
    //            {
    //                Session["AddToWishlist"] = null;
    //                Obj_bookOrder.OrderDate = System.DateTime.Now;
    //                Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
    //                //Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());

    //                var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
    //                setTitle = setTitle.Replace("-", " ");setTitle = setTitle.Replace("___", ":");
    //                setTitle = setTitle.Replace("__", "-");
    //                setTitle = setTitle.Replace("_", ".");
    //                //lblError.Text = setTitle;
    //                Obj_book.Title = setTitle;

    //                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
    //                {
    //                    Obj_book.LanguageID = 2;
    //                }
    //                else
    //                {
    //                    Obj_book.LanguageID = 1;
    //                }
    //                DataTable dt = Obj_book.GetBookDetailByTitle();
    //                if (dt.Rows.Count > 0)
    //                {
    //                    Obj_bookOrder.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
    //                }

    //                DataTable DT = Obj_bookOrder.GetWishList();
    //                if (DT != null && DT.Rows.Count > 0)
    //                {
    //                    for (int i = 0; i < DT.Rows.Count; i++)
    //                    {
    //                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
    //                        {
    //                            count++;
    //                        }

    //                    }
    //                    if (count > 0)
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
    //                    }
    //                    else
    //                    {
    //                        result = Obj_bookOrder.InsertCustomerWishList1();
    //                        wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
    //                        Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
    //                    }
    //                }
    //                else
    //                {
    //                    result = Obj_bookOrder.InsertCustomerWishList1();
    //                    wishid = HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID)));
    //                    Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + wishid + "");
    //                }
    //                //Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");

    //            }
    //        }
    //    }
    //    else
    //    {

    //        if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
    //        {
    //            int cnt = 0;
    //            string[] str = Session["AddToWishlist"].ToString().Split(',');
    //            foreach (string Add in str)
    //            {
    //                var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
    //                setTitle = setTitle.Replace("-", " ");setTitle = setTitle.Replace("___", ":");
    //                setTitle = setTitle.Replace("__", "-");
    //                setTitle = setTitle.Replace("_", ".");
    //                //lblError.Text = setTitle;
    //                Obj_book.Title = setTitle;

    //                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
    //                {
    //                    Obj_book.LanguageID = 2;
    //                }
    //                else
    //                {
    //                    Obj_book.LanguageID = 1;
    //                }
    //                DataTable dt = Obj_book.GetBookDetailByTitle();
    //                if (dt.Rows.Count > 0)
    //                {
    //                    var bookID = (dt.Rows[0]["BookID"].ToString());

    //                    if (Add == bookID)
    //                    {
    //                        cnt++;
    //                    }
    //                }
    //            }
    //            if (cnt > 0)
    //            {
    //                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                {
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su lista de deseos');", true);
    //                }
    //                else
    //                {
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
    //                }
    //            }
    //            else
    //            {
    //                var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
    //                setTitle = setTitle.Replace("-", " ");setTitle = setTitle.Replace("___", ":");
    //                setTitle = setTitle.Replace("__", "-");
    //                setTitle = setTitle.Replace("_", ".");
    //                //lblError.Text = setTitle;
    //                Obj_book.Title = setTitle;

    //                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
    //                {
    //                    Obj_book.LanguageID = 2;
    //                }
    //                else
    //                {
    //                    Obj_book.LanguageID = 1;
    //                }
    //                DataTable dt = Obj_book.GetBookDetailByTitle();
    //                if (dt.Rows.Count > 0)
    //                {
    //                    var bookID = (dt.Rows[0]["BookID"].ToString());
    //                    Session["AddToWishlist"] = Session["AddToWishlist"] + "," + Convert.ToInt32(bookID).ToString();
    //                    Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
    //            setTitle = setTitle.Replace("-", " ");setTitle = setTitle.Replace("___", ":");
    //            setTitle = setTitle.Replace("__", "-");
    //            setTitle = setTitle.Replace("_", ".");
    //            //lblError.Text = setTitle;
    //            Obj_book.Title = setTitle;

    //            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
    //            {
    //                Obj_book.LanguageID = 2;
    //            }
    //            else
    //            {
    //                Obj_book.LanguageID = 1;
    //            }
    //            DataTable dt = Obj_book.GetBookDetailByTitle();
    //            if (dt.Rows.Count > 0)
    //            {
    //                var bookID = (dt.Rows[0]["BookID"].ToString());
    //                Session["AddToWishlist"] = Convert.ToInt32(bookID).ToString();
    //                Response.Redirect(Config.WebSiteMain + "WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //            }
    //        }
    //    }
    //}

    public void addtoLibrary()
    {
        int count = 0;
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
            objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
            objPurchase.PurchaseDate = System.DateTime.Now;
            objPurchase.OrderID = 0;
            //objPurchase.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
            //Obj_book.Title = HttpUtility.UrlDecode(Request.QueryString["title"]).ToString();
            var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
            setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
            setTitle = setTitle.Replace("__", "-");
            setTitle = setTitle.Replace("_", ".");
            //lblError.Text = setTitle;
            Obj_book.Title = setTitle;
            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_book.LanguageID = 2;
            }
            else
            {
                Obj_book.LanguageID = 1;
            }
            DataTable dt = Obj_book.GetBookDetailByTitle();
            if (dt.Rows.Count > 0)
            {
                objPurchase.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
            }

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

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Ebook ya agregado en tu biblioteca');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Ebook already added in your library');", true);

                    }
                }
                else
                {
                    objPurchase.MoveToBookPurchase_freebook_website();
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('libro electronico agregado a su biblioteca');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('EBook added successfully to your library');", true);

                    }

                    //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["id"].ToString());
                }
            }
            else
            {
                objPurchase.MoveToBookPurchase_freebook_website();
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('libro electronico agregado a su biblioteca');", true);

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('EBook added successfully to your library');", true);

                }

                //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["id"].ToString());
            }
        }
        else
        {
            Response.Redirect(Config.WebSiteMain + "Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
    }

    //protected void btn_addcart_Click(object sender, EventArgs e)
    //{
    //    int count = 0;
    //    int result = 0;

    //    if (ViewState["IsFree"] != null && ViewState["IsFree"].ToString() == "True")
    //    {
    //        addtoLibrary();
    //    }
    //    else
    //    {
    //        BookOrderBAL Obj_bookOrder = new BookOrderBAL();
    //        int cnt1 = 0;

    //        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
    //        {
    //            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
    //            objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
    //            objPurchase.PurchaseDate = System.DateTime.Now;
    //            objPurchase.OrderID = 0;
    //            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
    //            {
    //                objPurchase.BookID = Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
    //            }
    //            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //            {
    //                objPurchase.LanguageID = 2;
    //            }
    //            else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
    //            {
    //                objPurchase.LanguageID = 1;
    //            }
    //            DataTable Dt = objPurchase.getUserLibrary();
    //            if (Dt != null && Dt.Rows.Count > 0)
    //            {
    //                for (int i = 0; i < Dt.Rows.Count; i++)
    //                {
    //                    if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
    //                    {
    //                        cnt1++;
    //                    }

    //                }
    //                if (cnt1 > 0)
    //                {

    //                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                    {

    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('Usted ya ha comprado este libro electrónico. Qué quieres mover en mi biblioteca?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

    //                    }
    //                    else
    //                    {

    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('You have already purchased this ebook. do you want to move in my library?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

    //                    }
    //                }
    //                else
    //                {
    //                    Session["AddToCart"] = null;
    //                    Obj_bookOrder.OrderDate = System.DateTime.Now;
    //                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
    //                    Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
    //                    //Obj_bookOrder.IseBook = eBook.Visible;
    //                    //Obj_bookOrder.IspaperBook = paperBook.Visible;
    //                    //Obj_bookOrder.Quantity = 1;
    //                    //Obj_bookOrder.InsertCustomerCart1();

    //                    DataTable DT = Obj_bookOrder.GetCartList();
    //                    if (DT != null && DT.Rows.Count > 0)
    //                    {
    //                        Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
    //                        for (int i = 0; i < DT.Rows.Count; i++)
    //                        {
    //                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
    //                            {
    //                                count++;
    //                            }
    //                        }
    //                        if (count > 0)
    //                        {
    //                            //Message1("You already have this book in your cart");
    //                            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                            {
    //                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
    //                            }
    //                            else
    //                            {
    //                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (ddlBookType.SelectedValue == "0")
    //                            {
    //                                Obj_bookOrder.IseBook = true;
    //                                Obj_bookOrder.IspaperBook = false;
    //                                Obj_bookOrder.Quantity = 0;
    //                                result = Obj_bookOrder.InsertCustomerCart1();
    //                                Obj_bookOrder.IseBook = false;
    //                                Obj_bookOrder.IspaperBook = true;
    //                                Obj_bookOrder.Quantity = 1;
    //                                result = Obj_bookOrder.InsertCustomerCart1();
    //                            }
    //                            else if (ddlBookType.SelectedValue == "1")
    //                            {
    //                                Obj_bookOrder.IseBook = true;
    //                                Obj_bookOrder.IspaperBook = false;
    //                                Obj_bookOrder.Quantity = 0;
    //                                result = Obj_bookOrder.InsertCustomerCart1();
    //                            }
    //                            else if (ddlBookType.SelectedValue == "2")
    //                            {
    //                                Obj_bookOrder.IseBook = false;
    //                                Obj_bookOrder.IspaperBook = true;
    //                                Obj_bookOrder.Quantity = 1;
    //                                result = Obj_bookOrder.InsertCustomerCart1();
    //                            }
    //                            //result = Obj_bookOrder.InsertCustomerCart1();
    //                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + S.Encrypt(Session["UserID"].ToString()) + "");
    //                        }
    //                    }
    //                    else
    //                    {
    //                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
    //                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
    //                        if (ddlBookType.SelectedValue == "0")
    //                        {
    //                            Obj_bookOrder.IseBook = true;
    //                            Obj_bookOrder.IspaperBook = false;
    //                            Obj_bookOrder.Quantity = 0;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                            Obj_bookOrder.IseBook = false;
    //                            Obj_bookOrder.IspaperBook = true;
    //                            Obj_bookOrder.Quantity = 1;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                        }
    //                        else if (ddlBookType.SelectedValue == "1")
    //                        {
    //                            Obj_bookOrder.IseBook = true;
    //                            Obj_bookOrder.IspaperBook = false;
    //                            Obj_bookOrder.Quantity = 0;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                        }
    //                        else if (ddlBookType.SelectedValue == "2")
    //                        {
    //                            Obj_bookOrder.IseBook = false;
    //                            Obj_bookOrder.IspaperBook = true;
    //                            Obj_bookOrder.Quantity = 1;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                        }
    //                        //result = Obj_bookOrder.InsertCustomerCart1();
    //                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + S.Encrypt(Session["UserID"].ToString()) + "");

    //                    }
    //                }
    //            }

    //            else
    //            {
    //                Session["AddToCart"] = null;
    //                Obj_bookOrder.OrderDate = System.DateTime.Now;
    //                Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
    //                Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
    //                //Obj_bookOrder.IseBook = eBook.Visible;
    //                //Obj_bookOrder.IspaperBook = paperBook.Visible;
    //                //Obj_bookOrder.Quantity = 1;
    //                //Obj_bookOrder.InsertCustomerCart1();

    //                DataTable DT = Obj_bookOrder.GetCartList();
    //                if (DT != null && DT.Rows.Count > 0)
    //                {
    //                    Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
    //                    for (int i = 0; i < DT.Rows.Count; i++)
    //                    {
    //                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
    //                        {
    //                            count++;
    //                        }
    //                    }
    //                    if (count > 0)
    //                    {
    //                        //Message1("You already have this book in your cart");
    //                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                        {
    //                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
    //                        }
    //                        else
    //                        {
    //                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        if (ddlBookType.SelectedValue == "0")
    //                        {
    //                            Obj_bookOrder.IseBook = true;
    //                            Obj_bookOrder.IspaperBook = false;
    //                            Obj_bookOrder.Quantity = 0;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                            Obj_bookOrder.IseBook = false;
    //                            Obj_bookOrder.IspaperBook = true;
    //                            Obj_bookOrder.Quantity = 1;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                        }
    //                        else if (ddlBookType.SelectedValue == "1")
    //                        {
    //                            Obj_bookOrder.IseBook = true;
    //                            Obj_bookOrder.IspaperBook = false;
    //                            Obj_bookOrder.Quantity = 0;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                        }
    //                        else if (ddlBookType.SelectedValue == "2")
    //                        {
    //                            Obj_bookOrder.IseBook = false;
    //                            Obj_bookOrder.IspaperBook = true;
    //                            Obj_bookOrder.Quantity = 1;
    //                            result = Obj_bookOrder.InsertCustomerCart1();
    //                        }
    //                        //result = Obj_bookOrder.InsertCustomerCart1();
    //                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + S.Encrypt(Session["UserID"].ToString()) + "");
    //                    }
    //                }
    //                else
    //                {
    //                    string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
    //                    Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
    //                    if (ddlBookType.SelectedValue == "0")
    //                    {
    //                        Obj_bookOrder.IseBook = true;
    //                        Obj_bookOrder.IspaperBook = false;
    //                        Obj_bookOrder.Quantity = 0;
    //                        result = Obj_bookOrder.InsertCustomerCart1();
    //                        Obj_bookOrder.IseBook = false;
    //                        Obj_bookOrder.IspaperBook = true;
    //                        Obj_bookOrder.Quantity = 1;
    //                        result = Obj_bookOrder.InsertCustomerCart1();
    //                    }
    //                    else if (ddlBookType.SelectedValue == "1")
    //                    {
    //                        Obj_bookOrder.IseBook = true;
    //                        Obj_bookOrder.IspaperBook = false;
    //                        Obj_bookOrder.Quantity = 0;
    //                        result = Obj_bookOrder.InsertCustomerCart1();
    //                    }
    //                    else if (ddlBookType.SelectedValue == "2")
    //                    {
    //                        Obj_bookOrder.IseBook = false;
    //                        Obj_bookOrder.IspaperBook = true;
    //                        Obj_bookOrder.Quantity = 1;
    //                        result = Obj_bookOrder.InsertCustomerCart1();
    //                    }
    //                    //result = Obj_bookOrder.InsertCustomerCart1();
    //                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + S.Encrypt(Session["UserID"].ToString()) + "");

    //                }
    //            }
    //            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

    //        }
    //        else
    //        {
    //            if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
    //            {
    //                int cnt = 0;
    //                string[] str = Session["AddToCart"].ToString().Split(',');
    //                foreach (string Add in str)
    //                {

    //                    if (Add == Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString())
    //                    {
    //                        cnt++;
    //                    }
    //                }
    //                if (cnt > 0)
    //                {
    //                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
    //                    }
    //                    else
    //                    {
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
    //                    }
    //                }
    //                else
    //                {
    //                    Session["AddToCart"] = Session["AddToCart"] + "," + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
    //                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //                }
    //            }
    //            else
    //            {
    //                Session["AddToCart"] = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
    //                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //            }
    //            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //        }
    //    }
    //}

    protected void btn_addcart_Click(object sender, EventArgs e)
    {
        int counteBook = 0;
        int countPaper = 0;
        int result = 0;
        //Obj_book.Title = HttpUtility.UrlDecode(Request.QueryString["title"]).ToString();
        var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
        setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
        setTitle = setTitle.Replace("__", "-");
        setTitle = setTitle.Replace("_", ".");
        //lblError.Text = setTitle;
        Obj_book.Title = setTitle;
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
        {
            Obj_book.LanguageID = 2;
        }
        else
        {
            Obj_book.LanguageID = 1;
        }
        DataTable dt = Obj_book.GetBookDetailByTitle();

        Session["Quantity"] = dt.Rows[0]["Quantity"].ToString();
        if (dt.Rows.Count > 0)
        {
            var bookId = Obj_book.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());
            //dt = Obj_book.getBookDetails();
            #region all cart entry

            if (ViewState["IsFree"] != null && ViewState["IsFree"].ToString() == "True")
            {
                if (ddlBookType.SelectedValue == Convert.ToString(1))
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
                        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                        {
                            try
                            {
                                Obj_book.BookID = objPurchase.BookID = bookId;// Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                            }
                            catch
                            {
                                Obj_book.BookID = objPurchase.BookID = bookId;//Convert.ToInt32(Request.QueryString["id"].ToString());
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

                            Session["AddToCart"] = null;
                            Obj_bookOrder.OrderDate = System.DateTime.Now;
                            Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                            Obj_bookOrder.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                            DataTable dtBookDetail = new DataTable();
                            Obj_book.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
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

                            DataTable DT = Obj_bookOrder.GetCartList();
                            if (DT != null && DT.Rows.Count > 0)
                            {
                                Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                                for (int i = 0; i < DT.Rows.Count; i++)
                                {
                                    var tableBook = Obj_book.getBookDetails();
                                    // var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                                    var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                                    var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                                    {
                                        if (iseBook == Convert.ToBoolean(DT.Rows[i]["IseBook"]))// && isPaperBook == Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                            counteBook++;
                                        if (isPaperBook == Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                            countPaper++;
                                    }
                                }


                                if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                                {
                                    var x = 0;
                                    if (counteBook == 0 && cnt1 == 0)
                                    {
                                        if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "1")
                                        {
                                            x++;
                                            Obj_bookOrder.IseBook = true;
                                            Obj_bookOrder.IspaperBook = false;
                                            Obj_bookOrder.Quantity = 0;
                                            result = Obj_bookOrder.InsertCustomerCart1();
                                        }
                                    }
                                    if (countPaper == 0 && dtBookDetail.Rows[0]["Quantity"].ToString() != "" && dtBookDetail.Rows[0]["Quantity"].ToString() != "0")
                                    {
                                        if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "2")
                                        {
                                            x++;
                                            Obj_bookOrder.IseBook = false;
                                            Obj_bookOrder.IspaperBook = true;
                                            Obj_bookOrder.Quantity = 1;
                                            result = Obj_bookOrder.InsertCustomerCart1();
                                        }
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
                                else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()) && countPaper == 0 && dtBookDetail.Rows[0]["Quantity"].ToString() != "" && dtBookDetail.Rows[0]["Quantity"].ToString() != "0")
                                {
                                    Obj_bookOrder.IseBook = false;
                                    Obj_bookOrder.IspaperBook = true;
                                    Obj_bookOrder.Quantity = 1;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                }
                                if (Convert.ToBoolean(dtBookDetail.Rows[0]["Quantity"].ToString() == "0"))
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock...');", true);

                                }
                                else
                                {
                                    if (result > 0)
                                        Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                                    else
                                    {
                                        if (Request.QueryString["l"].ToString() == "en-US")
                                            Response.Redirect(Config.WebSiteMain + "us/" + Request.QueryString["title"]);
                                        else if (Request.QueryString["l"].ToString() == "es-ES")
                                            Response.Redirect(Config.WebSiteMain + "es/" + Request.QueryString["title"]);
                                        else
                                            Response.Redirect(Config.WebSiteMain + "us/" + Request.QueryString["title"]);
                                        //Response.Redirect(Request.Url.ToString() + "");
                                    }
                                    //}
                                }
                            }
                            else
                            {
                                if (Convert.ToBoolean(dtBookDetail.Rows[0]["Quantity"].ToString() == "0"))
                                {
                                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                                    }
                                }
                                else
                                {
                                    string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                    Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                                    if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                                    {
                                        if (counteBook == 0 && cnt1 == 0)
                                        {
                                            if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "1")
                                            {
                                                Obj_bookOrder.IseBook = true;
                                                Obj_bookOrder.IspaperBook = false;
                                                Obj_bookOrder.Quantity = 0;
                                                result = Obj_bookOrder.InsertCustomerCart1();
                                            }
                                        }
                                        if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "2")
                                        {
                                            Obj_bookOrder.IseBook = false;
                                            Obj_bookOrder.IspaperBook = true;
                                            Obj_bookOrder.Quantity = 1;
                                            result = Obj_bookOrder.InsertCustomerCart1();
                                        }
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

                                    if (result > 0)
                                        Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                                    else
                                        Response.Redirect(Request.Url.ToString() + "");
                                }
                            }
                            //}
                        }

                        else
                        {
                            Session["AddToCart"] = null;
                            Obj_bookOrder.OrderDate = System.DateTime.Now;
                            Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                            Obj_bookOrder.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());

                            DataTable dtBookDetail = new DataTable();
                            Obj_book.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
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
                                        Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
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
                                    Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                                else
                                    Response.Redirect(Request.Url.ToString() + "");
                            }
                        }
                        //Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

                    }
                    else
                    {
                        if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                        {
                            int cnt = 0;
                            string[] str = Session["AddToCart"].ToString().Split(',');
                            foreach (string Add in str)
                            {

                                if (Add == bookId.ToString())//Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString())
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
                                if (Session["Quantity"] == "0")
                                {
                                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado...');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock...');", true);
                                    }
                                }
                                else
                                {
                                    Session["AddToCart"] = Session["AddToCart"] + "," + bookId;//Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                                    Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                                }
                            }
                        }
                        else
                        {
                            if (Session["Quantity"] == "0")
                            {
                                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado...');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock...');", true);
                                }
                            }
                            else
                            {
                                Session["AddToCart"] = bookId;//Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                                Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                            }
                        }
                        //Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    }
                }
                #endregion
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
                    if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                    {
                        try
                        {
                            Obj_book.BookID = objPurchase.BookID = bookId;// Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                        }
                        catch
                        {
                            Obj_book.BookID = objPurchase.BookID = bookId;//Convert.ToInt32(Request.QueryString["id"].ToString());
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

                        Session["AddToCart"] = null;
                        Obj_bookOrder.OrderDate = System.DateTime.Now;
                        Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                        Obj_bookOrder.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                        DataTable dtBookDetail = new DataTable();
                        Obj_book.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
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

                        DataTable DT = Obj_bookOrder.GetCartList();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                var tableBook = Obj_book.getBookDetails();
                                // var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                                var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                                var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                                if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                                {
                                    if (iseBook == Convert.ToBoolean(DT.Rows[i]["IseBook"]))// && isPaperBook == Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                        counteBook++;
                                    if (isPaperBook == Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                        countPaper++;
                                }
                            }


                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                var x = 0;
                                if (counteBook == 0 && cnt1 == 0)
                                {
                                    if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "1")
                                    {
                                        x++;
                                        Obj_bookOrder.IseBook = true;
                                        Obj_bookOrder.IspaperBook = false;
                                        Obj_bookOrder.Quantity = 0;
                                        result = Obj_bookOrder.InsertCustomerCart1();
                                    }
                                }
                                if (countPaper == 0 && dtBookDetail.Rows[0]["Quantity"].ToString() != "" && dtBookDetail.Rows[0]["Quantity"].ToString() != "0")
                                {
                                    if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "2")
                                    {
                                        x++;
                                        Obj_bookOrder.IseBook = false;
                                        Obj_bookOrder.IspaperBook = true;
                                        Obj_bookOrder.Quantity = 1;
                                        result = Obj_bookOrder.InsertCustomerCart1();
                                    }
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
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()) && countPaper == 0 && dtBookDetail.Rows[0]["Quantity"].ToString() != "" && dtBookDetail.Rows[0]["Quantity"].ToString() != "0")
                            {
                                Obj_bookOrder.IseBook = false;
                                Obj_bookOrder.IspaperBook = true;
                                Obj_bookOrder.Quantity = 1;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["Quantity"].ToString() == "0"))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock...');", true);

                            }
                            else
                            {
                                if (result > 0)
                                    Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                                else
                                {
                                    if (Request.QueryString["l"].ToString() == "en-US")
                                        Response.Redirect(Config.WebSiteMain + "us/" + Request.QueryString["title"]);
                                    else if (Request.QueryString["l"].ToString() == "es-ES")
                                        Response.Redirect(Config.WebSiteMain + "es/" + Request.QueryString["title"]);
                                    else
                                        Response.Redirect(Config.WebSiteMain + "us/" + Request.QueryString["title"]);
                                    //Response.Redirect(Request.Url.ToString() + "");
                                }
                                //}
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["Quantity"].ToString() == "0"))
                            {
                                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                                }
                            }
                            else
                            {
                                string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                                Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                                if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                                {
                                    if (counteBook == 0 && cnt1 == 0)
                                    {
                                        if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "1")
                                        {
                                            Obj_bookOrder.IseBook = true;
                                            Obj_bookOrder.IspaperBook = false;
                                            Obj_bookOrder.Quantity = 0;
                                            result = Obj_bookOrder.InsertCustomerCart1();
                                        }
                                    }
                                    if (ddlBookType.SelectedItem.Value == "0" || ddlBookType.SelectedItem.Value == "2")
                                    {
                                        Obj_bookOrder.IseBook = false;
                                        Obj_bookOrder.IspaperBook = true;
                                        Obj_bookOrder.Quantity = 1;
                                        result = Obj_bookOrder.InsertCustomerCart1();
                                    }
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

                                if (result > 0)
                                    Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                                else
                                    Response.Redirect(Request.Url.ToString() + "");
                            }
                        }
                        //}
                    }

                    else
                    {
                        Session["AddToCart"] = null;
                        Obj_bookOrder.OrderDate = System.DateTime.Now;
                        Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                        Obj_bookOrder.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());

                        DataTable dtBookDetail = new DataTable();
                        Obj_book.BookID = bookId;//Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
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
                                    Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
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
                                Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                            else
                                Response.Redirect(Request.Url.ToString() + "");
                        }
                    }
                    //Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

                }
                else
                {
                    if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                    {
                        int cnt = 0;
                        string[] str = Session["AddToCart"].ToString().Split(',');
                        foreach (string Add in str)
                        {

                            if (Add == bookId.ToString())//Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString())
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
                            if (Session["Quantity"] == "0")
                            {
                                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado...');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock...');", true);
                                }
                            }
                            else
                            {
                                Session["AddToCart"] = Session["AddToCart"] + "," + bookId;//Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                                Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                            }
                        }
                    }
                    else
                    {
                        if (Session["Quantity"] == "0")
                        {
                            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                            }
                        }
                        else
                        {
                            Session["AddToCart"] = bookId;//Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                            Session["AddToCardBookType"]= ddlBookType.SelectedValue;
                            Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                        }
                    }
                    //Response.Redirect(Config.WebSiteMain + "Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
            }
        }

        else
        {
            Response.Write("<script>alert('This Book Was Deleted...')</script>");
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        var sb = new StringBuilder();
        sb.Append("https://www.google.com/recaptcha/api/siteverify?secret=");
        var secretKey = "6LcnLEEUAAAAABhCY_vE9AdgVeg_LVvoWA6McR28";
        sb.Append(secretKey);
        sb.Append("&");
        sb.Append("response=");
        var reCaptchaResponse = Request["g-recaptcha-response"];
        sb.Append(reCaptchaResponse);
        sb.Append("&");
        sb.Append("remoteip=");
        var clientIpAddress = GetUserIp();
        sb.Append(clientIpAddress);

        using (var client = new WebClient())
        {
            var uri = sb.ToString();
            var json = client.DownloadString(uri);
            var serializer = new DataContractJsonSerializer(typeof(RecaptchaApiResponse));
            var ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            var result = serializer.ReadObject(ms) as RecaptchaApiResponse;

            if (result == null)
            {

            }
            else // If Yes
            {
                //api call contains errors
                if (result.ErrorCodes != null)
                {
                    if (result.ErrorCodes.Count > 0)
                    {
                        foreach (var error in result.ErrorCodes)
                        {

                        }
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por favor, compruebe Captcha');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Verify Captcha');", true);
                        }
                    }
                }
                else //api does not contain errors
                {
                    if (!result.Success) //captcha was unsuccessful for some reason
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Por favor, vuelva a verificar Captcha hay algún problema');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please Reverify Captcha there is some problem');", true);
                        }
                    }
                    else //---- If successfully verified. Do your rest of logic.
                    {
                        #region Save Review Code
                        //Insert_Review
                        //Obj_book.Title = HttpUtility.UrlDecode(Request.QueryString["title"]).ToString();
                        var setTitle = HttpUtility.UrlDecode(Request.QueryString["title"].ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
                        setTitle = setTitle.Replace("-", " "); setTitle = setTitle.Replace("___", ":");
                        setTitle = setTitle.Replace("__", "-");
                        setTitle = setTitle.Replace("_", ".");
                        //lblError.Text = setTitle;
                        Obj_book.Title = setTitle;
                        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
                        {
                            Obj_book.LanguageID = 2;
                        }
                        else
                        {
                            Obj_book.LanguageID = 1;
                        }
                        DataTable dt = Obj_book.GetBookDetailByTitle();
                        if (dt.Rows.Count > 0)
                        {
                            var bookId = Obj_book.BookID = Convert.ToInt32(dt.Rows[0]["BookID"].ToString());

                            Obj_book.BookID = bookId;//Convert.ToInt32(Request.QueryString["id"].ToString());
                            if (Session["UserID"] != null)
                            {
                                Obj_book.UserID = Convert.ToInt32(Session["UserID"].ToString());
                            }
                            else
                            {
                                Obj_book.UserID = 0;
                            }
                            //Obj_book.Name = txt_nickname.Text;
                            Obj_book.Name = "";
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
                            //Obj_book.CreatedDate = Convert.ToDateTime(hdnDatetime.Value);
                            Obj_book.CreatedDate = DateTime.Now;
                            Obj_book.Insert_ReviewRatting();
                            Response.Redirect(Request.RawUrl);
                        }
                        #endregion
                    }
                }

            }

        }
    }

    [DataContract]
    public class RecaptchaApiResponse
    {
        [DataMember(Name = "success")]
        public bool Success;

        [DataMember(Name = "error-codes")]
        public List<string> ErrorCodes;
    }

    private string GetUserIp()
    {
        var visitorsIpAddr = string.Empty;

        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
        {
            visitorsIpAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


        }
        else if (!string.IsNullOrEmpty(Request.UserHostAddress))
        {
            visitorsIpAddr = Request.UserHostAddress;
        }

        return visitorsIpAddr;
    }



}