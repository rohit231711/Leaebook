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
    int pageSize = 50;
    int pageIndex = 1;
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
            bindGlobalCat();
        }        
    }

    private void BindData()
    {
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        String Search = "";
        Int32 CatID = 0;
        viewallbook = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        newrelease = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        buynowtitle = Localization.ResourceManager.GetString("buynow", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        isfreetitle = Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", System.Threading.Thread.CurrentThread.CurrentCulture.Name);

        BookBAL Obj_Book1 = new BookBAL();
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book1.LanguageID = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book1.LanguageID = 1;
            }
        }
        else
        {
            Obj_Book1.LanguageID = 1;
        }
        if (Session["BlobalCatID"] != null)
        {
            divfilter.Attributes.Add("style", "display:none");
            CatID = Convert.ToInt32(Session["BlobalCatID"].ToString());
            Session.Remove("BlobalCatID");
        }
        else
        {
            divfilter.Attributes.Add("style", "display:block");
        }
        if (Session["BlobalTextSearch"] != null)
        {
            Search = Session["BlobalTextSearch"].ToString();
            Session.Remove("BlobalTextSearch");
        }        
        Obj_Book1.EndIndex = -1;
        Obj_Book1.StartIndex = 1;

        DataTable dt = Obj_Book1.get_all_book_websiteGloblalSearch(CatID,Search, Convert.ToInt32(ddlSortby.SelectedValue), "New Releases");
        
        //string a1 = "<div class='panel'>" +
        //            "</div>";
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
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    str = str + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                str = str + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                //if (Convert.ToBoolean(dr["IsFree"]) == true)
                //{
                //    str = str + "<div class='namkl'>" + isfreetitle.ToString() + "</div>";
                //}
                //else
                //{
                //    str = str + "<div class='namkl'>$" + dr["FinalPrice"] + "</div>";
                //}
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
            str = str + "<div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div>";
        }
        else
        {
            str = "<div style=\"Color:red;font-size: 15px; font-family: 'abeezeeregular';padding: 20px;\">No Record Found</div>";
        }

        //div_book.InnerHtml = a1 + str;
        div_book.InnerHtml = str;
       
    }

    [WebMethod]
    public static string BindDatas(int Sortby, string For)
    {
        Security S = new Security();
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        String Search = "";
        Int32 CatID = 0;
        viewallbook = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        newrelease = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        buynowtitle = Localization.ResourceManager.GetString("buynow", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        isfreetitle = Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", System.Threading.Thread.CurrentThread.CurrentCulture.Name);

        BookBAL Obj_Book1 = new BookBAL();
        if (HttpContext.Current.Request.QueryString["l"] != null && HttpContext.Current.Request.QueryString["l"].ToString() != "")
        {
            if (HttpContext.Current.Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book1.LanguageID = 2;
            }
            else if (HttpContext.Current.Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book1.LanguageID = 1;
            }
        }
        else
        {
            Obj_Book1.LanguageID = 1;
        }
        CatID = 0;
        Search = null;        
        Obj_Book1.EndIndex = -1;
        Obj_Book1.StartIndex = 1;

        DataTable dt = Obj_Book1.get_all_book_websiteGloblalSearch(CatID, Search, Sortby, For);

        //string a1 = "<div class='panel'>" +
        //            "</div>";
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
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    str = str + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                str = str + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                //if (Convert.ToBoolean(dr["IsFree"]) == true)
                //{
                //    str = str + "<div class='namkl'>" + isfreetitle.ToString() + "</div>";
                //}
                //else
                //{
                //    str = str + "<div class='namkl'>$" + dr["FinalPrice"] + "</div>";
                //}
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
            str = str + "<div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div><div class='namkl'>&nbsp;</div>";
        }
        if (dt.Rows.Count == 0)
        {
            string NoData = "<div style=\"Color:red;font-size: 15px; font-family: 'abeezeeregular';padding: 20px;\">No Record Found</div>";
            //+ ResourceManager.GetString("Nodatafound", culture) + "</div>";
            return NoData;
        }
        return str;
    }
    protected void btn_search(object sender, EventArgs e)
    {
        Session["BlobalCatID"] = drpglobalcat.SelectedValue.ToString();
        Session["BlobalTextSearch"] = txt_Globalsearch.Text.ToString();
        drpglobalcat.SelectedIndex = 0;
        txt_Globalsearch.Text = "";
        Response.Redirect(Config.WebSiteMain + "AllBooks.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    }

    public void bindGlobalCat()
    {
        var category = "All Categories";//todas las categorias
        DataTable dt = new DataTable();
        Category_Locale objCat_Locate = new Category_Locale();
        int language = 1;
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                language = 2;
                category = "todas las categorias";
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                language = 1;
                category = "All Categories";
            }
        }
        dt = objCat_Locate.Get_Category_Locale_leftmenu(language);
        if (dt.Rows.Count > 0)
        {
            drpglobalcat.DataSource = dt;
            drpglobalcat.DataTextField = "CategoryName";
            drpglobalcat.DataValueField = "CategoryID";
            drpglobalcat.DataBind();

            ListItem li = new ListItem(category, "0");
            drpglobalcat.Items.Insert(0, li);
        }
    }




   
}