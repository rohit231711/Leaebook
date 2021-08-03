using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class Includes_Footer : System.Web.UI.UserControl
{
    WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
    DataTable DT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            DT = WSB.GetAllWebseetings();
            if (DT != null && DT.Rows.Count > 0)
            {
                fb.HRef = DT.Rows[0]["FaceBookLink"].ToString();
                ti.HRef = DT.Rows[0]["TwiterLink"].ToString();
                gp.HRef = DT.Rows[0]["GoogleLink"].ToString();
                A1.HRef = DT.Rows[0]["Instagram_Link"].ToString();
            }
        }
        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
        {
            aboutus.HRef = "~/us/about-us";
            contactus.HRef = "~/us/contact-us";
            sitemap.HRef = "~/us/sitemap";
            deliveryinfo.HRef = "~/us/delivery-information";
            privacy.HRef = "~/us/privacy-policy";
            terms.HRef = "~/us/terms";
            refundPolicy.HRef = "~/us/refund-policy";
            latestnews.HRef = "~/us/latest-news";
            unsubscribe.HRef = "~/us/unsubscribe";
        }
        else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            aboutus.HRef = "~/about-us";
            contactus.HRef = "~/contact-us";
            sitemap.HRef = "~/sitemap";
            deliveryinfo.HRef = "~/delivery-information";
            privacy.HRef = "~/privacy-policy";
            terms.HRef = "~/terms";
            refundPolicy.HRef = "~/refund-policy";
            latestnews.HRef = "~/latest-news";
            unsubscribe.HRef = "~/unsubscribe";
        }
        partner.HRef = "~/PartnerRegister.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
    }
}