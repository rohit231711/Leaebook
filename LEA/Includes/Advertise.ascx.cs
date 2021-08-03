using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Configuration;
using System.IO;

public partial class Includes_Advertise : System.Web.UI.UserControl
{
    string path = "";
    string img = "";
    AdvertisementsBAL Obj_ad = new AdvertisementsBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            Obj_ad.LanguageID = 1;
            dt = Obj_ad.SelectAdvertisementWebSite();
            if (dt.Rows.Count > 0)
            {
                string a = ",";
                foreach(DataRow dr in dt.Rows)
                {
                    if (dr["AdvertisementImage"] != "")
                    {
                        a = a + dr["AdvertisementImage"] + "@@";
                        if (dr["Title"].ToString() == "")
                        {
                            a = a + " " + "@@";
                        }
                        else
                        {
                            a = a + dr["Title"] + "@@";
                        }
                        if (dr["LinkUrl"].ToString() == "")
                        {
                            a = a + "#" + "@@,";
                        }
                        else
                        {
                            a = a + dr["LinkUrl"] + "@@,";
                        }                        
                    }
                }
                hndimglst.Value = a;
                div_ad1.Visible = true;
                lbl_title1.Text = dt.Rows[0]["Title"].ToString();                

                path = "Advertisements/" + dt.Rows[0]["AdvertisementImage"].ToString();
                img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Advertisements/" + dt.Rows[0]["AdvertisementImage"].ToString();
                linkadd1.HRef = dt.Rows[0]["LinkUrl"].ToString();
                if (File.Exists(((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + ""))))
                {
                   
                }
                else
                {
                    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Advertisements/noimage.jpg";
                }

                //img_ad1.Src = img.ToString();
                img_ad1.Src = "../Advertisements/" + dt.Rows[0]["AdvertisementImage"].ToString();
            }
            if (dt.Rows.Count > 1)
            {
                div_ad2.Visible = true;
                lbl_Title2.Text = dt.Rows[1]["Title"].ToString();
                linkadd1.HRef = dt.Rows[1]["LinkUrl"].ToString();

                path = "Advertisements/" + dt.Rows[1]["AdvertisementImage"].ToString();
                if (File.Exists(((HttpContext.Current.Request.PhysicalApplicationPath + "/" + path + ""))))
                {
                    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Advertisements/" + dt.Rows[1]["AdvertisementImage"].ToString();
                }
                else
                {
                    img = ConfigurationManager.AppSettings["SiteUrlMain"].ToString() + "Advertisements/noimage.jpg";
                }

                //img_ad2.Src = img.ToString();
                img_ad2.Src = "../Advertisements/" + dt.Rows[1]["AdvertisementImage"].ToString();
            }
        }
    }
}