using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Text;
using System.Data;
public partial class Client_payment : System.Web.UI.Page
{
    BookPurchaseBAL ObjBookPurchase = new BookPurchaseBAL();
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    DataTable dt = new DataTable();
    HttpCookie cook;
    public string SessionID { get; set; }
    public string UserName
    {
        get;
        set;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            cook = new HttpCookie("UserSessionID");

            if (Request.Cookies["UserSessionID"] != null)
            {
                SessionID = Request.Cookies["UserSessionID"]["SessionID"].ToString();
            }

            else
            {
                if (Request.QueryString["sessionid"] != null)
                {
                    SessionID = Request.QueryString["sessionid"].ToString();
                }

            }
            string tranID, orderid, status, domain, amount, currency, paydate, appcode, skey, key0, key1, email;
            tranID = Request.Form["tranID"];
            orderid = Request.Form["orderid"];
            status = Request.Form["status"];
            domain = Request.Form["domain"];
            amount = Request.Form["amount"];
            currency = Request.Form["currency"];
            paydate = Request.Form["paydate"];
            appcode = Request.Form["appcode"];
            skey = Request.Form["skey"];

            key0 = Security.md5Hash(tranID + orderid + status + domain + amount + currency);
            key1 = Security.md5Hash(paydate + domain + key0 + appcode + "7d5d89abbec6fc2c13411b0ed01565bb");
            ObjBookPurchase.OrderID = Convert.ToInt64(orderid);
            ObjBookPurchase.PurchaseDate = Convert.ToDateTime(paydate);

            if (ObjBookPurchase.PurchaseDate == DateTime.MinValue)
            {
                ObjBookPurchase.PurchaseDate = DateTime.Now;
            }

            ObjBookPurchase.Amount = amount;
            ObjBookPurchase.Status = status;
            ObjBookPurchase.Domain = domain;
            ObjBookPurchase.AppCode = appcode;
            ObjBookPurchase.TransID = tranID;
            ObjBookPurchase.Vcode = skey;
            if (!string.IsNullOrEmpty(orderid))
            {
                ObjBookPurchase.PurchaseID = Convert.ToInt64(orderid.Remove(0, 9));
                ObjBookPurchase.InsertBookPurchase();
            }
            if (status == "00")
            {
                SendCustomerOrderEmail(paydate, orderid, SessionID);

                cook = new HttpCookie("UserSessionID");
                Request.Cookies["UserSessionID"].Expires = DateTime.Now.AddDays(-1);


                Request.Cookies.Add(Request.Cookies["UserSessionID"]);


                cook.Values.Add("SessionID", Guid.NewGuid().ToString());


                cook.Expires = DateTime.MaxValue;
                Response.Cookies.Add(cook);

                ObjBookOrderBal.SessionID = SessionID;
                ObjBookOrderBal.DeleteUserCart();
                lblAlert.Text = "Success";
                lblMessage.Text = " Thank you for purchasing Book,your order detail is mailed to you.";
                h3.Attributes.CssStyle.Add("background-color", "#3B9A02");

            }
            else
            {
                lblAlert.Text = "Error";
                //   lblMessage.Text = "";
                lblMessage.Text = "Sorry,you have filled wrong information,transaction was failed,Please try again later.";
                h3.Attributes.CssStyle.Add("background-color", "red");
            }
        }

    }

    public void SendCustomerOrderEmail(string paydate, string orderid, string SessionID)
    {
        StringBuilder sb = new StringBuilder();
        ObjBookOrderBal.SessionID = SessionID;

        dt = ObjBookOrderBal.SelectCustomerCart();
        if (Session["UserSession"] != null)
        {
            DataTable dt1 = Session["UserSession"] as DataTable;
            UserName = dt1.Rows[0]["FirstName"].ToString();
        }
        sb.Append("<table cellspacing='25' cellpadding='0' border='0'>");
        sb.Append("<tbody>");
        sb.Append("<tr>");

        sb.Append("<td valign='top'>");
        sb.Append("<img style='margin-bottom: 20px;' alt='themagz' src='http://themagz.net/client/images/logo_green.png'");

        sb.Append("<h1 style='font-family: Arial, Helvetica, sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51); margin-bottom: 25px;'>");
        sb.Append("Your &nbsp;theMagz.net | &nbsp;Order Confirmation</h1>");
        sb.Append("<h2 style='font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal;color: rgb(0, 80, 161);'>");
        sb.Append("Hi&nbsp;" + UserName + ",</h2>");
        sb.Append("<div style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal;color: rgb(51, 51, 51);'>Thank you for your order on " + Convert.ToDateTime(paydate).ToLongDateString() + "  (Ref No. " + orderid + "). </div><br>");
        sb.Append("<table width='493' height='120' cellspacing='0' cellpadding='0' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        float price = 0.0f;
        foreach (DataRow dr in dt.Rows)
        {


            price += Convert.ToInt64(dr["Price"]);


        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;' src=" + dt.Rows[i]["TitleImage"].ToString() + "></td>");
            sb.Append("<td width='283' valign='middle'><span style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;" + dt.Rows[i]["Title"].ToString() + "<br></span> </td>");
            sb.Append("<td width='65' valign='middle' align='center' style='font-family: Arial, Helvetica, sans-serif;'");
            sb.Append("<td width='65' valign='middle' align='center' style='font-family: Arial, Helvetica, sans-serif;font-size: 12px; font-weight: normal; color: rgb(62, 62, 62);'></td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Arial, Helvetica, sans-serif;font-size: 12px; font-weight: normal; color: rgb(62, 62, 62);'>" + Math.Round(Convert.ToDecimal(dt.Rows[i]["Price"].ToString())) + " RM" + "</td>");
            sb.Append("</tr>");


        }



        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='493' height='auto' cellspacing='0' cellpadding='0' border='0' align='center'> <tbody>");
        sb.Append("<tr>");

        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Arial, Helvetica, sans-serif;font-size: 14px; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Arial, Helvetica, sans-serif; font-size: 14px; color: rgb(0, 80, 161); font-weight: bold;'>RM " + price + "</td>");
        sb.Append("</tr>");

        sb.Append("</tbody></table><br><br>");


        sb.Append("<table width='492' cellspacing='0' cellpadding='0' border=''0' align='center'><tbody>");
        sb.Append("<tr>");

        sb.Append("<td>");

        sb.Append("<h2 style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: rgb(62, 58, 54); margin-top: 20px; margin-bottom: 5px;'>Questions?</h2>");
        sb.Append(" <div style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("If you have any questions about your order, please contact our customer support team by going to <a href='http://themagz.net' target='_blank' style='color: #333333'>http://themagz.net</a></div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        if (Request.QueryString["email"] != null)
        {
            Global.SendEmail(Request.QueryString["email"].ToString(), "theMagz.net | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Global.UserEmail, "theMagz.net | Your Order Confirmation", sb.ToString());
        }

        //App.SendEmail(Global.UserEmail, "noreply@vrinsofts.com", "Order Confirmation", sb.ToString());
    }


}