using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Includes_Header_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        aIndex.HRef = Config.WebSiteMain + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        awhatwedo.HRef = Config.WebSiteMain + "CMS_Content.aspx?id=24&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        acategory.HRef = Config.WebSiteMain + "Category.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aspecialoffer.HRef = Config.WebSiteMain + "Special-Offer.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aMyLibrary.HRef = Config.WebSiteMain + "MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aContactUs.HRef = Config.WebSiteMain + "ContactUs.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        aBlog.HRef = Config.WebSiteMain + "Blog.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;



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
            aIndex1.HRef = "~/us/";
            aIndex.HRef = "~/us/";

            awhatwedo.HRef = "~/us/what-we-do";
            awhatwedo1.HRef = "~/us/what-we-do";
            aContactUs.HRef = "~/us/contact-us";
            aContactUs1.HRef = "~/us/contact-us";
            acategory.HRef = "~/us/category";
            acategory1.HRef = "~/us/category";
            aspecialoffer.HRef = "~/us/special-offer";
            aspecialoffer1.HRef = "~/us/special-offer";
            aBlog.HRef = "~/us/blog";
            aBlog1.HRef = "~/us/blog";
        }
        else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            //added by jalpa
            aIndex1.HRef = "~/";
            aIndex.HRef = "~/";

            awhatwedo.HRef = "~/what-we-do";
            awhatwedo1.HRef = "~/what-we-do";
            aContactUs.HRef = "~/contact-us";
            aContactUs1.HRef = "~/contact-us";
            acategory.HRef = "~/category";
            acategory.HRef = "~/category";
            aspecialoffer.HRef = "~/special-offer";
            aspecialoffer1.HRef = "~/special-offer";
            aBlog.HRef = "~/blog";
            aBlog1.HRef = "~/blog";
        }

        if (Session["UserName"] != null && Session["UserName"].ToString() != "")
        {
            lblmylibrary.Visible = true;
            aMyLibrary.HRef = Config.WebSiteMain + (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US" ? "us" : "") + "/MyLibrary";//MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        }
        else
        {
            lblmylibrary.Visible = true;
            aMyLibrary.HRef = Config.WebSiteMain + (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US" ? "us" : "") + "/Login";

            //liLib.Style.Add("width", "0px");
        }

        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
        string sRet = oInfo.Name;


        if ("Index.aspx" == sRet)
        {
            aIndex.Attributes["class"] = "active";
        }
        else if ("CMS_Content.aspx" == sRet)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() == "24")
            {
                awhatwedo.Attributes["class"] = "active";
            }
            else
            {
                aIndex.Attributes["class"] = "active";
            }
        }
        else if ("Category.aspx" == sRet)
        {
            acategory.Attributes["class"] = "active";
        }
        else if ("MyLibrary.aspx" == sRet)
        {
            aMyLibrary.Attributes["class"] = "active";
        }
        else if ("ContactUs.aspx" == sRet)
        {
            aContactUs.Attributes["class"] = "active";
        }
        else if ("Blog.aspx" == sRet)
        {
            aBlog.Attributes["class"] = "active";
        }

        else if ("Special-Offer.aspx" == sRet)
        {
            aspecialoffer.Attributes["class"] = "active";
        }
        else
        {
            aIndex.Attributes["class"] = "active";
        }

    }
}