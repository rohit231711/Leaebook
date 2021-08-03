using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Includes_account_leftmenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            aAccount.HRef = "../Account.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            aMyLibrary.HRef = "../MyAccountLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                aWishlist.HRef = "../WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString();
            }
            else
            {
                aWishlist.HRef = "../WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
            aOrderreport.HRef = "../OrderReport.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            aorderHistory.HRef = "../OrderHistory.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            aorderAddress.HRef = "../OrderAddress.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;

            string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;

            if ("Account.aspx" == sRet)
            {
                aAccount.Attributes["class"] = "active";              
            }
            else if ("MyAccountLibrary.aspx" == sRet)
            {
                aMyLibrary.Attributes["class"] = "active";
            }
            else if ("WishList.aspx" == sRet)
            {
                aWishlist.Attributes["class"] = "active";               
            }
            else if ("OrderReport.aspx" == sRet)
            {
                aOrderreport.Attributes["class"] = "active";                
            }
            else if ("OrderAddress.aspx" == sRet)
            {
                aorderAddress.Attributes["class"] = "active";
            }
            else if("OrderHistory.aspx" == sRet || "ViewOrder.aspx" == sRet)
            {
                aorderHistory.Attributes["class"] = "active";
            }
        }
    }
}