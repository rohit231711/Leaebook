using BAL;
using Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AllBooks : System.Web.UI.Page
{
    Security S = new Security();
    BookBAL Obj_Book = new BookBAL();
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
            BindData();

        }
    }

    public void BindData()
    {
        BookBAL Obj_Book1 = new BookBAL();
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        String Search = "";
        Int32 CatID = 0;
        viewallbook = Localization.ResourceManager.GetString("viewallbook", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        newrelease = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        //buynowtitle = Localization.ResourceManager.GetString("buynow", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        buynowtitle = Localization.ResourceManager.GetString("addtocart", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        isfreetitle = Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        DataTable dt = new DataTable();
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book.LanguageID = Language = 2;
                string Title = "Ebooks de los reconocidos escritores salvadoreños Roque Dalton y Marvin Galeas";
                string Description = "La mejor colección de libros electrónicos de Roque Dalton y Marvin Galeas. LEA es la primera librería virtual de El Salvador. Tenems una gran colección de libros de famosos escritores, incluyendo  libros de Roque Dalton y Marvin Galeas.";
                this.Page.Title = Title;
                this.Page.MetaDescription = Description;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book.LanguageID = Language = 1;
                string Title = "Online bookstore El Salvador, Libros de Roque Dalton & Marvin Galeas – LEA";
                string Description = "Best eBook Collection of Roque Dalton & Marvin Galeas. LEA is the leading Virtual Bookstore in El Salvador. We have great eBook collection of famous writer including Libros de Roque Dalton & Marvin Galeas.";
                this.Page.Title = Title;
                this.Page.MetaDescription = Description;
            }
        }
        else
        {
            Obj_Book.LanguageID = Language = 1;
        }

        if (Session["BlobalCatID"] != null)
        {
            divfilter.Attributes.Add("style", "display:none");
            CatID = Convert.ToInt32(Session["BlobalCatID"].ToString());
            //Session.Remove("BlobalCatID");
        }
        else
        {
            divfilter.Attributes.Add("style", "display:block");
            //Session.Remove("BlobalCatID");
        }
        if (Session["BlobalTextSearch"] != null)
        {
            Search = Session["BlobalTextSearch"].ToString();
            //Session.Remove("BlobalTextSearch");
        }

        if (Request.QueryString["PageNo"] != null && Request.QueryString["PageNo"].ToString() != "")
        {
            Obj_Book.EndIndex = Convert.ToInt32(Request.QueryString["PageNo"].ToString()) * 8;
            //Obj_Book.EndIndex = -1;
            Obj_Book.StartIndex = (Obj_Book.EndIndex - 8) + 1;
        }
        else
        {
            HttpContext.Current.Session["num"] = "11";
            //Obj_Book.EndIndex = -1;
            Obj_Book.EndIndex = 8;
            Obj_Book.StartIndex = 1;
        }

        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book1.LanguageID = Language = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book1.LanguageID = Language = 1;
            }
            else
            {
                Obj_Book1.LanguageID = Language = 1;
            }
        }
        if (Session["BlobalCatID"] != null)
        {
            Bind_SearchData();
        }
        else
        {
            dt = Obj_Book.get_all_book_website2(Convert.ToInt32(ddlSortby.SelectedValue), "New Releases");
            string a1 = "";
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
                        ////img = "book/" + dr["categoryid"] + '/' + dr["imagepath"].tostring().replace(".jpg", "_1.jpg") + "";
                        ////string newfile = img;
                        ////if (!file.exists((httpcontext.current.request.physicalapplicationpath + "/" + img + "")))
                        ////{
                        ////    string image = httpcontext.current.request.physicalapplicationpath + "/" + filename + "";
                        ////    stream fs = new filestream(image, system.io.filemode.open, system.io.fileaccess.read);
                        ////    generatethumbnails(image, httpcontext.current.request.physicalapplicationpath + "/" + filename + "");
                        ////}
                        img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
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
                    }
                    else if (Language == 1)
                    {
                        bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                        bookDetailUrl += "/" + dr["TabType"];
                    }
                    else
                    {
                        bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                        bookDetailUrl += "/" + dr["TabType"];
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
            if (dt.Rows.Count < 8)
            {
                str += "<div id=\"divNoData\" style=\"Color:red;\" onload=\"hideAlert()\">No Record Found</div>";
            }
            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Nodatafound", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
            }
           
            div_book.InnerHtml = a1 + str;
        }
    }



    public void Bind_SearchData()
    {
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        viewallbook = Localization.ResourceManager.GetString("viewallbook", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        newrelease = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        //buynowtitle = Localization.ResourceManager.GetString("buynow", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        buynowtitle = Localization.ResourceManager.GetString("addtocart", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        isfreetitle = Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        int CatID = 0;
        String Search = "";
        BookBAL Obj_Book1 = new BookBAL();
        if (Session["BlobalCatID"] != null)
        {
            divfilter.Attributes.Add("style", "display:none");
            CatID = Convert.ToInt32(Session["BlobalCatID"].ToString());
            //Session.Remove("BlobalCatID");
        }
        else
        {
            divfilter.Attributes.Add("style", "display:block");
        }
        if (Session["BlobalTextSearch"] != null)
        {
            Search = Session["BlobalTextSearch"].ToString();
           // Session.Remove("BlobalTextSearch");
        }
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book1.LanguageID = Language = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book1.LanguageID = Language = 1;
            }
            else
            {
                Obj_Book1.LanguageID = Language = 1;
            }
        }
        if (Request.QueryString["PageNo"] != null && Request.QueryString["PageNo"].ToString() != "")
        {
            Obj_Book.EndIndex = Convert.ToInt32(Request.QueryString["PageNo"].ToString()) * 8;
            //Obj_Book.EndIndex = -1;
            Obj_Book.StartIndex = (Obj_Book.EndIndex - 8) +1;
        }
        else
        {
            HttpContext.Current.Session["num"] = "8";
            //Obj_Book.EndIndex = -1;
            Obj_Book.EndIndex = 4;
            Obj_Book.StartIndex = 1;
        }
        DataTable dt = Obj_Book1.get_all_book_websiteGloblalSearch(CatID, Search, Convert.ToInt32(ddlSortby.SelectedValue), "New Releases");
        string str1 = "";
        if (dt.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if (i % 4 == 0)
                {
                    str1 = str1 + "<div class='box1book boxbolast'>";
                }
                else
                {
                    str1 = str1 + "<div class='box1book'>";
                }
                i++;

                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                }
                else
                {
                    img = "images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (Obj_Book1.LanguageID == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else if (Obj_Book1.LanguageID == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0 && Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str1 = str1 + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    str1 = str1 + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str1 = str1 + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str1 = str1 + "</div>";
                }
                str1 = str1 + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                if (Convert.ToBoolean(dr["IseBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFree"]) == true)
                    {
                        str1 = str1 + "<div class='namkl' style=\"width: 50%;float: left;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str1 = str1 + "<div class='namkl' style=\"width: 50%;float: left;\">$" + dr["FinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str1 = str1 + "<div class='namkl' style=\"width: 50%;float: left;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsPaperBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFreePaper"]) == true)
                    {
                        str1 = str1 + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str1 = str1 + "<div class='namkl' style=\"width: 45%;float: left;text-align: right;padding-right: 5px;\">$" + dr["PaperBookFinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str1 = str1 + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str1 = str1 + "<a href='Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                    "</div>";

                }
                else
                {
                    str1 = str1 + "<a href='Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }
            }
            str1 = str1 + "<div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div>";
        }
        else
        {
            str1 = "<div style=\"Color:red;font-size: 15px; font-family: 'abeezeeregular';padding: 20px;\">No Record Found</div>";
        }
        loadmore_div.Visible = false;
        div_book.InnerHtml = str1;
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
        //buynowtitle = Localization.ResourceManager.GetString("buynow", culture);
        buynowtitle = Localization.ResourceManager.GetString("addtocart", culture);
        isfreetitle = Localization.ResourceManager.GetString("Free", culture);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", culture);
        DataTable dt = new DataTable();

        Obj_Book.LanguageID = Language;
        //Obj_Book.StartIndex = lastIndex;
        //lastIndex = Obj_Book.EndIndex = lastIndex + 9;
        string a1 = "";
        if (LoadMore != "LoadMore")
        {
            if (For == "SortBy" || For == "New Releases" || For == "ebook" || For == "Paper book")
            {
                if (HttpContext.Current.Request.QueryString["PageNo"] != null && HttpContext.Current.Request.QueryString["PageNo"].ToString() != "")
                {
                    Obj_Book.EndIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["PageNo"].ToString()) * 8;
                    Obj_Book.StartIndex = (Obj_Book.EndIndex - 8) + 1;
                }
                else
                {
                    HttpContext.Current.Session["num"] = "8";
                    Obj_Book.EndIndex = 8;
                    Obj_Book.StartIndex = 1;
                }
            }
        }
        else if (LoadMore == "LoadMore")
        {
            if (For == "New Releases")
            {
                Obj_Book.StartIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) +1;
                Obj_Book.EndIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 16;
                // HttpContext.Current.Session["num"] = Obj_Book.EndIndex.ToString();
                HttpContext.Current.Session["num"] = Convert.ToInt32(Obj_Book.EndIndex);
            }
            else
            {
                Obj_Book.StartIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 1;
                Obj_Book.EndIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 16;
                // HttpContext.Current.Session["num"] = Obj_Book.EndIndex.ToString();
                HttpContext.Current.Session["num"] = Convert.ToInt32(Obj_Book.EndIndex) + 1;
            }
        }

        dt = Obj_Book.get_all_book_website2(Sortby, For);


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
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                }
                else
                {
                    img = "images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (Language == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else if (Language == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0 && Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    //str = str + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + culture + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    //str = str + "<div class='bookimg'>" + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + culture + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    str = str + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                //str = str + "<div class='namkl2'><a href = 'Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + culture + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                str = str + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                if (For == "ebook" || For == "New Releases")
                {
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
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 50%;float: left;\">&nbsp;</div>";
                }
                if (For == "Paper book" || For == "New Releases")
                {
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
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<a href='Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + culture + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                "</div>";
                }
                else
                {
                    str = str + "<a href='Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + culture + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }

            }
            //str = str + "</div>";
        }
        if (dt.Rows.Count < 8)
        {
            str += "<div id=\"divNoData\" onload=\"hideAlert()\"></div>";
        }
        //if (dt.Rows.Count == 0)
        //{
        //    string NoData = "<div id=\"divNoData\" style=\"Color:red;\" onload=\"hideAlert()\">No Record Found</div>";
        //    //+ ResourceManager.GetString("Nodatafound", culture) + "</div>";
        //    return NoData;
        //}
        return str;
    }

    public static int Language;

    private void GenerateThumbnails(string sourcePath, string targetPath)
    {
        //System.IO.FileStream fs = new System.IO.FileStream(sourcePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        //var image = System.Drawing.Image.FromStream(fs);
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
                //File.Delete(targetPath);
                thumbnailImg.Save(targetPath.Replace("\\/", "\\").Replace("/", "\\").Replace(".jpg", "_1.jpg"), ImageFormat.Jpeg);
                //System.IO.File.Copy(filePath, Server.MapPath("~/Uploads"));
            }
        }
        catch (Exception ex)
        { }
    }

}