using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Localization;

public partial class Includes_Header_SP_EN : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
         {
        string defaultCulture = LocalizationConfiguration.GetConfig().DefaultCultureName;
        string cacheKey = "SQLLocalization:" + defaultCulture + ':' + "en-US";
        HttpRuntime.Cache.Remove(cacheKey);

         string defaultCulture1 = LocalizationConfiguration.GetConfig().DefaultCultureName;
        string cacheKey1 = "SQLLocalization:" + defaultCulture + ':' + "es-ES";
        HttpRuntime.Cache.Remove(cacheKey1);

        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                divspen.Attributes["class"] = "langma";
                sp.Attributes["class"] = "active";
                en.Attributes["class"] = "last";
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                divspen.Attributes["class"] = "langma1";
                en.Attributes["class"] = "active";
                sp.Attributes["class"] = "last";
            }
        }
        bool expression = Request.QueryString.Count > 0;
        if (expression)
        {
            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "es-ES")
            {
                if (Request.Url.ToString().Contains("CMS_Content.aspx") || Request.Url.ToString().Contains("ContactUs.aspx")
                    || Request.Url.ToString().Contains("Blog.aspx") || Request.Url.ToString().Contains("Category.aspx")
                    || Request.Url.ToString().Contains("Special-Offer.aspx") || Request.Url.ToString().Contains("Book-Detail.aspx")
                    || Request.Url.ToString().Contains("Index.aspx")|| Request.Url.ToString().Contains("Login.aspx"))
            {
                    sp.HRef = Request.RawUrl.ToString();
                    en.HRef = "/us" + Request.RawUrl.ToString().Replace("-Us", "");
                    if (Request.Url.ToString().Contains("Book-Detail.aspx"))
                    {
                        sp.HRef = Request.RawUrl.ToString();
                        en.HRef = Request.RawUrl.ToString().Replace("/es", "/us");
                        Session["Url"] = en.HRef;
                    }
                }
                else
                {
                    //Update by - Mahipatsinh
                    sp.HRef = Request.RawUrl.ToString();
                    en.HRef = "/us" + Request.RawUrl.ToString();

                    //sp.HRef = Request.Url.ToString();
                    //en.HRef = Request.Url.ToString().Replace("es-ES", "en-US");
                    //sp.HRef = "/";
                    //en.HRef = "/us/";
                }
            }
            else if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() == "en-US")
            {
                if (Request.Url.ToString().Contains("CMS_Content.aspx") || Request.Url.ToString().Contains("ContactUs.aspx")
                    || Request.Url.ToString().Contains("Blog.aspx") || Request.Url.ToString().Contains("Category.aspx")
                    || Request.Url.ToString().Contains("Special-Offer.aspx") || Request.Url.ToString().Contains("Book-Detail.aspx")
                    || Request.Url.ToString().Contains("Index.aspx") || Request.Url.ToString().Contains("Login.aspx"))
                {
                    en.HRef = Request.RawUrl.ToString();
                    sp.HRef = Request.RawUrl.ToString().Replace("/us", "");
                    if (Request.Url.ToString().Contains("Book-Detail.aspx"))
                    {
                        //en.HRef = Request.RawUrl.ToString();
                        //sp.HRef = Request.RawUrl.ToString().Replace("/us", "/es");
                        //sp.HRef= Request.RawUrl.ToString().Replace("/es/", "");
                        en.HRef = Request.RawUrl.ToString();
                        sp.HRef = Request.RawUrl.ToString().Replace("/us/", "/es/");
                    }
                }
                else
                {
                    //Update by - Mahipatsinh
                    sp.HRef = Request.RawUrl.ToString();
                    en.HRef = "/us" + Request.RawUrl.ToString();
                    //en.HRef = Request.Url.ToString();
                    //sp.HRef = Request.Url.ToString().Replace("en-US", "es-ES");
                    //sp.HRef = "/";
                    //en.HRef = "/us/";
                    //if (Request.QueryString["catid"] != null && Request.QueryString["catid"] == "")
                    //{
                    //    sp.HRef = Request.RawUrl.ToString();
                    //    en.HRef = "/us" + Request.RawUrl.ToString();
                    //}
                }
            }
            else
            {
                //Update by - Mahipatsinh
                sp.HRef = Request.RawUrl.ToString();
                en.HRef = "/us" + Request.RawUrl.ToString();
                //sp.HRef = Request.Url.ToString() + "&l=es-ES";
                //en.HRef = Request.Url.ToString() + "&l=en-US";
                //sp.HRef = "/";
                //en.HRef = "/us/";
            }
        }
        else
        {
            //Update by - Mahipatsinh
            sp.HRef = Request.RawUrl.ToString();
            en.HRef = "/us" + Request.RawUrl.ToString();
            //sp.HRef = Request.Url.ToString() + "?l=es-ES";
            //en.HRef = Request.Url.ToString() + "?l=en-US";
            //sp.HRef = "/";
            //en.HRef = "/us/";
        }
    }
}