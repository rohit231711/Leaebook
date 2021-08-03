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
            long userid = Convert.ToInt64(Request.QueryString["userid"]);
            // email = Request.QueryString["emaiil"].ToString();
            key0 = General.General.GetMD5(tranID + orderid + status + domain + amount + currency);
            key1 = General.General.GetMD5(paydate + domain + key0 + appcode + "");
            ObjBookPurchase.OrderID = Convert.ToInt64(orderid);
            ObjBookPurchase.PurchaseDate = DateTime.Now;

            ObjBookPurchase.Amount = amount;
            ObjBookPurchase.Status = status;
            ObjBookPurchase.Domain = domain;
            ObjBookPurchase.AppCode = appcode;
            ObjBookPurchase.TransID = tranID;
            ObjBookPurchase.Vcode = skey;
            ObjBookPurchase.PurchaseID = Convert.ToInt64(orderid.Remove(0, 9));
            ObjBookPurchase.InsertBookPurchase();
            if (status == "00")
            {
                SendCustomerOrderEmail(paydate, orderid, userid.ToString());
                lbltext.Text = "Thank you for purchasing Book,your order is mailed to you";


                ObjBookOrderBal.CustomerID = userid;
                ObjBookOrderBal.DeleteUserCart();
            }
            else
            {
                lbltext.Text = "Sorry,you have filled wrong information,Please try again later.";
            }
        }

    }

    public void SendCustomerOrderEmail(string paydate, string orderid, string UserID)
    {
        StringBuilder sb = new StringBuilder();
        RegistrationBAL ObjBal = new RegistrationBAL();
        ObjBookOrderBal.CustomerID = Convert.ToInt64(UserID);

        dt = ObjBookOrderBal.SelectCustomerCart();

        ObjBal.RegistrationID = Convert.ToInt64(UserID);
        DataTable dtuser = ObjBal.SelectRegistraionByID();
        string email = "";
        if (dtuser.Rows.Count > 0)
        {
            UserName = dtuser.Rows[0]["FirstName"].ToString();
            email = dtuser.Rows[0]["EmailAddress"].ToString();
        }
        sb.Append("<table cellspacing='25' cellpadding='0' border='0'>");
        sb.Append("<tbody>");
        sb.Append("<tr>");

        sb.Append("<td valign='top'>");
        sb.Append("<img style='margin-bottom: 20px;' alt='zinio' src='http://themagz.net/client/images/logo_green.png'");

        sb.Append("<h1 style='font-family: Arial, Helvetica, sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51); margin-bottom: 25px;'>");
        sb.Append("Your &nbsp;theMagz.net | &nbsp;Order Confirmation</h1>");
        sb.Append("<h2 style='font-family: Arial, Helvetica, sans-serif; font-size: 14px; font-weight: normal;color: rgb(0, 80, 161);'>");
        sb.Append("Hi&nbsp;" + UserName + ",</h2>");
        sb.Append("<div style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal;color: rgb(51, 51, 51);'>Thank you for your order on " + Convert.ToDateTime(paydate).ToLongDateString() + "  (Ref No. " + orderid + "). </div>;<br>");
        sb.Append("<table width='493' height='120' cellspacing='0' cellpadding='0' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        float price = 0.0f;
        foreach (DataRow dr in dt.Rows)
        {


            price += Convert.ToInt64(dr["Price"] == DBNull.Value ? 0 : dr["Price"]);


        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<td width='70' align='left'><img border='0' title='' alt='' style='margin: 0px;' src=" + dt.Rows[i]["TitleImage"].ToString() + "></td>");
            sb.Append("<td width='283' valign='middle'><span style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: rgb(62, 62, 62);'>" + dt.Rows[i]["Title"].ToString() + "<br></span> </td>");
            sb.Append("<td width='65' valign='middle' align='center' style='font-family: Arial, Helvetica, sans-serif;'");
            sb.Append("<td width='65' valign='middle' align='center' style='font-family: Arial, Helvetica, sans-serif;'font-size: 12px; font-weight: normal; color: rgb(62, 62, 62);'></td>");
            sb.Append("<td width='75' valign='middle' align='center' style='font-family: Arial, Helvetica, sans-serif;font-size: 12px; font-weight: normal; color: rgb(62, 62, 62);'>" + Math.Round(Convert.ToDecimal(Convert.ToInt64(dt.Rows[i]["Price"] == DBNull.Value ? 0 : dt.Rows[i]["Price"]))) + "</td>");
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
        sb.Append("<td>");

        sb.Append("<h2 style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: bold;color: rgb(62, 58, 54); margin-top: 20px; margin-bottom: 5px;'>Questions?</h2>");
        sb.Append(" <div style='font-family: Arial, Helvetica, sans-serif; font-size: 12px; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("If you have any questions about your order, please contact our customer supportteam by going to <a href='http://themagz.net' target='_blank' style='color: #333333';rel='nofollow'>http://themagz.net</a></div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        if (email != "")
        {
            Global.SendEmail(email, "theMagz.net | Your Order Confirmation", sb.ToString());
        }


        //App.SendEmail(Global.UserEmail, "noreply@vrinsofts.com", "Order Confirmation", sb.ToString());
    }


}