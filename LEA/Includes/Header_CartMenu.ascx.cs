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
using System.Threading;
using System.Globalization;

public partial class Includes_Header_CartMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["UserName"].ToString() != "")
        {
            lblsignin.Key = "welcome";
            lbl_username.Text = Session["UserName"].ToString();
        }
        else
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                lblsignin.Key = "welcome";
                lbl_username.Text = "User";
            }
            else
                lblsignin.Key = "signin";
        }

        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();
            Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"]);
            DataTable WishDT = Obj_bookOrder.GetCartList();

            if (WishDT != null && WishDT.Rows.Count > 0)
            {
                cntcart.Text = WishDT.Rows.Count.ToString();
            }
            else
            {
                cntcart.Text = "0";
            }
        }
        else
        {
            if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
            {
                int cnt = 0;
                string[] str = Session["AddToCart"].ToString().Split(',');
                foreach (string Add in str)
                {
                    if (Add == "")
                    {

                    }
                    else
                    {
                        cnt++;
                    }
                }
                if (cnt > 0)
                {
                    cntcart.Text = cnt.ToString();
                }
                else
                {
                    cntcart.Text = "0";
                }
            }
            else
            {
                cntcart.Text = "0";
            }
        }
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            lbllogout.Key = "logout";

        }
        else
        {
            lbllogout.Visible = false;
            lblmyaccount.Visible = false;
        }

        if (Session["UserID"] != null)
        {
            checkoutlink.HRef = Config.WebSiteMain + "Checkout.aspx?id=" + Session["UserID"].ToString() + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            checkoutlink1.HRef = Config.WebSiteMain + "Checkout.aspx?id=" + Session["UserID"].ToString() + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            //img.HRef = "../Checkout.aspx?id=" + Session["UserID"].ToString() + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        }
        else
        {
            checkoutlink.HRef = Config.WebSiteMain + (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US" ? "us/" : "") + "Checkout";
            checkoutlink1.HRef = Config.WebSiteMain + (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US" ? "us/" : "") + "Checkout";
            //img.HRef = "../Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        }
    }
}