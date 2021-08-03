using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Threading;
using System.Globalization;
using System.Text;
using System.Web.Configuration;

public partial class Payment : System.Web.UI.Page
{
    public static string HiddenFilds;
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookDeliveryAddressBAL objAddress = new BookDeliveryAddressBAL();
    BookShippingBAL objShipping = new BookShippingBAL();
    Security S = new Security();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString.Count > 0 && Request.QueryString["UserID"] != "" && Request.QueryString["UserID"] != "0")
        {
            ObjBookOrders.CustomerID = Convert.ToInt32(Request.QueryString["UserID"]);
            ObjBookOrders.LanguageID = Convert.ToInt32(1);

            Session["CurrentCulture"] = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            DataTable dt = ObjBookOrders.GetCartList();

            decimal Amount = 0;
            var Amount1 = Request.QueryString["ShippingCharge"];//decimal Amount1 = Decimal.Round(Convert.ToDecimal(Request.QueryString["ShippingCharge"]), 2);
            int Qty = 0;
            string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                //    Amount1 += GetShippingCharge(dt.Rows[i]["BookID"].ToString());
                //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"]);
                Amount += Decimal.Round(Convert.ToDecimal(dt.Rows[i]["FinalCartPrice1"]), 2);
                Qty = Qty + 1;
            }

            if (dt != null && dt.Rows.Count > 0)
            {
                long OrderNo = 0;
                if (dt.Rows[0]["OrderNo"] != null && dt.Rows[0]["OrderNo"].ToString() != "")
                {
                    try
                    {
                        OrderNo = Convert.ToInt64(dt.Rows[0]["OrderNo"].ToString());
                    }
                    catch
                    {
                        OrderNo = 0;
                    }
                }
                HiddenFilds = " <input type=\"hidden\" name=\"cmd\" value=\"_xclick\" />"
                            + "<input type=\"hidden\" name=\"tx\" value=\"TransactionID\"> "
                            + "   <input type=\"hidden\" name=\"business\" value=\"bysidesv@gmail.com\" />"
                            + "   <input type=\"hidden\" name=\"no_shipping\" value=\"" + Qty + "\" />"
                            + "   <input type=\"hidden\" name=\"shipping\" value=\"" +Amount1+ "\" />"
                            + "   <input type=\"hidden\" name=\"current_culture\" value=\"" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "\" />"
                            + "   <input type=\"hidden\" name=\"item_number\" value=\"" + OrderNo + "\" />"
                            + "   <input type=\"hidden\" name=\"item_name\" id=\"amount\" value=\"Books\" />"
                            + "   <input type=\"hidden\" name=\"amount\" value=\"" +Amount + "\" />"
                + "   <input type=\"hidden\" name=\"cancel_return\" value=\"" + Request.Url.AbsoluteUri.ToString().Replace("http", "https").Replace("Payment", "PaymentNew").Replace("UserID", "id") + "\"/>  "
                            + "   <input type=\"hidden\" name=\"return\" value=\"" + Config.WebSite + "PDT/PDTSuccess.aspx?Address=" + Request.QueryString["Address"] + "&order=" + HttpUtility.UrlEncode(OrderNo.ToString()) + "&ShippingCharge=" + Amount + "&ShippingType=" + Request.QueryString["ShippingType"] + "&Success=1" + "\"/>  " + ""
                + "   <input type=\"hidden\" name=\"custom\" value=\"" + OrderNo + "\" />"
                + "   <input type=\"hidden\" name=\"InvoiceNumber\" value=\"" + OrderNo + "\" />"
                + "   <input type=\"hidden\" name=\"image\" value=\"" + Config.WebSite + "images/LEAlogo.png\" />";
                HiddenFilds = HiddenFilds.Trim();
                Session["OrderNo"] = OrderNo;






                #region For make URl For New payment

                string PaymentbaseUrl = WebConfigurationManager.AppSettings["Paymenturl"];


                if(Request.QueryString["totalAmount"]!=null)
                {
                    Amount = Convert.ToDecimal(Request.QueryString["totalAmount"]);
                }

                if(Request.QueryString["ShippingCharge"]!=null)
                {
                    Amount = Amount + Convert.ToDecimal(Request.QueryString["ShippingCharge"]);
                }


                string BrainTreePaymenturl = PaymentbaseUrl + "?amount=" + HttpUtility.UrlEncode(Amount.ToString()) +
                                                               "&qty=" + HttpUtility.UrlEncode(Qty.ToString()) +
                                                               "&address=" + HttpUtility.UrlEncode(Request.QueryString["Address"]) +
                                                               "&order=" + HttpUtility.UrlEncode(OrderNo.ToString()) +
                                                               "&shippingcharge=" + HttpUtility.UrlEncode(Amount.ToString()) +
                                                               "&shippingtype=" + HttpUtility.UrlEncode(Request.QueryString["ShippingType"]);



                Response.Redirect(BrainTreePaymenturl);
                #endregion













            }
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());

        }

    }

   

    private double GetShippingCharge(string bookid)
    {
        DataTable dt = new DataTable();
        objAddress.BookDeliveryAddID = Convert.ToInt32(Request.QueryString["Address"]);
        dt = objAddress.GetDataByPK();

        if (dt.Rows.Count > 0)
        {
            objShipping.BookID = Convert.ToInt32(bookid);
            objShipping.CountryID = Convert.ToInt32(dt.Rows[0]["countryid"]);
            dt = objShipping.getChargebyBookAndCountry();
            if (dt.Rows.Count > 0)
            {
                try
                {
                    return Convert.ToDouble(dt.Rows[0]["ShippingCharge"]);
                }
                catch
                {
                    return 0;
                }
            }
        }
        return 0;
    }
}