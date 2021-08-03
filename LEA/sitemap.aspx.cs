using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class sitemap : System.Web.UI.Page
{
    Category_Locale objCat_Locate = new Category_Locale();
    BookBAL Obj_book = new BookBAL();
    Security S = new Security();
    DataSet dsTable;
    BlogBAL objBlog = new BlogBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (Request.QueryString["l"] == null)
            {
                //Response.Redirect(url);
                Response.Redirect(url + "?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
            Bind_leftmenu();
            BindData();
        }
        aIndex1.HRef = Config.WebSiteMain + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        awhatwedo1.HRef = Config.WebSiteMain + "CMS_Content.aspx?id=24&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        acategory1.HRef = Config.WebSiteMain + "Category.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aspecialoffer1.HRef = Config.WebSiteMain + "Special-Offer.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aMyLibrary1.HRef = Config.WebSiteMain + "MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aContactUs1.HRef = Config.WebSiteMain + "ContactUs.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aBlog1.HRef = Config.WebSiteMain + "Blog.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;

        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
        {
            //added by jalpa
            // aIndex1.HRef = "~/us/";
            awhatwedo1.HRef = "~/us/what-we-do";
            aContactUs1.HRef = "~/us/contact-us";
            acategory1.HRef = "~/us/category";
            aspecialoffer1.HRef = "~/us/special-offer";
            aBlog1.HRef = "~/us/blog";
            aboutus.HRef = "~/us/about-us";
            deliveryinfo.HRef = "~/us/delivery-information";
            privacy.HRef = "~/us/privacy-policy";
            terms.HRef = "~/us/terms";
            refundPolicy.HRef = "~/us/refund-policy";
            latestnews.HRef = "~/us/latest-news";
            unsubscribe.HRef = "~/us/unsubscribe";
            //sitemap.HRef = "~/us/sitemap";
        }
        else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            //added by jalpa
            // aIndex1.HRef = "~/";
            awhatwedo1.HRef = "~/what-we-do";
            acategory1.HRef = "~/category";
            aContactUs1.HRef = "~/contact-us";
            aspecialoffer1.HRef = "~/special-offer";
            aBlog1.HRef = "~/blog";
            aboutus.HRef = "~/about-us";
            deliveryinfo.HRef = "~/delivery-information";
            privacy.HRef = "~/privacy-policy";
            terms.HRef = "~/terms";
            refundPolicy.HRef = "~/refund-policy";
            latestnews.HRef = "~/latest-news";
            unsubscribe.HRef = "~/unsubscribe";
            //sitemap.HRef = "~/sitemap";
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

                Session["CartID"] = dr["CategoryID"].ToString(); // cart id
                ianchor.Attributes.Add("href", Config.WebSiteMain + "Biography.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&catid=" + dr["CategoryID"].ToString() + "");
                ianchor.InnerText = dr["CategoryName"].ToString();
                ili.Controls.Add(ianchor);
            }
        }
    }

    private void BindData()
    {
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
        {
            objBlog.LanguageID = 2;
        }
        else
        {
            objBlog.LanguageID = 1;
        }
        dsTable = objBlog.SelectBlogAll();
        if (dsTable.Tables.Count > 0 && dsTable.Tables[0].Rows.Count > 0)
        {
            rptRecords1.DataSource = dsTable;
            rptRecords1.DataBind();
            rptRecords1.Visible = true;
        }
        else
        {
            rptRecords1.Visible = false;
        }
    }
}