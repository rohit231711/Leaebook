#region Live file Code
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net;
using System.IO;
using BAL;
using System.Data;
using System.Text;
using System.Globalization;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Xml;


public partial class PDT_Default : System.Web.UI.Page
{
    string authToken, txToken, query;
    string strResponse;
    Security S = new Security();

    protected void Page_Load(object sender, EventArgs e)
    {
        BookOrderBAL ObjBookOrders = new BookOrderBAL();
        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
        if (!Page.IsPostBack)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            if (Session["CurrentCulture"] != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Session["CurrentCulture"].ToString());
            }

            // Used parts from https://www.paypaltech.com/PDTGen/
            // Visit above URL to auto-generate PDT script

            //authToken = "mAbIbsf7Zh-bkso8UVOrc_KgtV3NYP0vMeOWJ14pWlwbvekX5Loi7jSzvlm";//WebConfigurationManager.AppSettings["PDTToken"];
            authToken = WebConfigurationManager.AppSettings["PDTToken"];

            //read in txn token from querystring
            txToken = Request.QueryString["tx"];//Request.QueryString.Get("tx");
            //txToken = Request.QueryString.Get("token");


            query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);

            // Create the request back
            //string url = "https://www.paypal.com/cgi-bin/webscr";// WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            // Write the request back IPN strings
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            // Do the request to PayPal and get the response
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();

            Label2.Text = strResponse;
            // sanity check

            //Response.Write(strResponse);

            // If response was SUCCESS, parse response string and output details
            if (Request.QueryString["Success"] != null && Request.QueryString["Success"].ToString() == "1")
            {
                //try
                //{
                //if (strResponse.StartsWith("SUCCESS"))
                //{
                PDTHolder pdt = PDTHolder.Parse(strResponse);

                Label1.Text = string.Format("Thank you {0} {1} [{2}] for your payment of {3} {4}!",
                    pdt.PayerFirstName, pdt.PayerLastName, pdt.PayerEmail, pdt.GrossTotal, pdt.Currency);
                if (pdt.Custom == "" && Request.QueryString["item_number"] != null)
                {
                    pdt.Custom = Request.QueryString["item_number"].ToString();
                }
                Label3.Text = pdt.Custom;
                Label4.Text = pdt.InvoiceNumber.ToString();
                Label5.Text = pdt.TransactionId;

                //Response.Write("<p><h3>Your order has been received.</h3></p>");
                //Response.Write("<b>Details</b><br>");
                //Response.Write("<li>Name: " + pdt.PayerFirstName + pdt.PayerLastName + "</li>");
                //Response.Write("<li>Name: " + pdt.Custom + "</li>");
                //Response.Write("<li>Name: " + pdt.TransactionId + "</li>");

                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ObjBookOrders.LanguageID = 2;
                }
                else
                {
                    ObjBookOrders.LanguageID = 1;
                }


                ObjBookOrders.OrderNo = pdt.Custom;// Request.QueryString["item_number"].ToString();
                DataTable WishDT = ObjBookOrders.GetCartListByOrderNo();
                Shipment s = new Shipment();

                int result = 0;
                if (WishDT != null && WishDT.Rows.Count > 0)
                {
                    StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/Shipment1.xml"));
                    string text = streamReader.ReadToEnd();
                    var piceses = "";
                    decimal weight = 0;
                    var cnt = 0;
                    for (int i = 0; i < WishDT.Rows.Count; i++)
                    {
                        objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(System.DateTime.Now);
                        objPurchase.TransID = pdt.TransactionId;
                        result = objPurchase.MoveToBookPurchase(Request.QueryString["Address"], Request.QueryString["ShippingCharge"], Request.QueryString["ShippingType"], "Website");
                        if (Convert.ToBoolean(WishDT.Rows[i]["IsPaperBook"]))
                        {
                            var quantity = Convert.ToInt32(WishDT.Rows[i]["Qauntity"]);
                            for (int j = 0; j < quantity; j++)
                            {
                                cnt++;
                                try
                                {
                                    weight += Convert.ToDecimal(WishDT.Rows[i]["Weight"]);
                                    piceses += "<Piece><PieceID>" + (i + 1) + "</PieceID><PackageType>EE</PackageType><Weight>"
                                               + WishDT.Rows[i]["Weight"] + "</Weight><DimWeight>" + WishDT.Rows[i]["DimWeight"]
                                               + "</DimWeight><Width>" + WishDT.Rows[i]["Width"] + "</Width><Height>"
                                               + WishDT.Rows[i]["Height"] + "</Height><Depth>"
                                               + WishDT.Rows[i]["Depth"] + "</Depth></Piece>";
                                }
                                catch (Exception)
                                {
                                    weight += 1;
                                    piceses += "<Piece><PieceID>" + (i + 1) + "</PieceID><PackageType>EE</PackageType><Weight>"
                                               + "1.0" + "</Weight><DimWeight>" + "1200.0"
                                               + "</DimWeight><Width>" + "100" + "</Width><Height>"
                                               + "200" + "</Height><Depth>"
                                               + "300" + "</Depth></Piece>";
                                }
                            }
                        }
                    }
                    if (cnt > 0)
                    {
                        string xmlRequest = s.replaceXml(text, cnt.ToString(), piceses, weight.ToString(), Request.QueryString["ShippingCharge"]);
                        string response = s.sendRequest(xmlRequest);

                        try
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(response);

                            var ProfilePicture = xmlDoc.DocumentElement.SelectSingleNode("LabelImage/OutputImage").InnerText;
                            var billnumber = xmlDoc.DocumentElement.SelectSingleNode("AirwayBillNumber").InnerText;
                            string name = billnumber;
                            if (!string.IsNullOrEmpty(ProfilePicture))
                            {
                                Base64ToImage(ProfilePicture, Server.MapPath("~/ShippingFiles/") + name + ".pdf");
                            }
                            objPurchase.LanguageID = ObjBookOrders.LanguageID;
                            objPurchase.OrderID = Convert.ToInt64(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["order"])));
                            WishDT = objPurchase.GetCartListByOrderID();
                            lblAlert.Text = Localization.ResourceManager.GetString("Success", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                            lblMessage.Text = Localization.ResourceManager.GetString("purchaseBookSuccess", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                            h3.Attributes.CssStyle.Add("background-color", "#3B9A02");

                            SendCustomerOrderEmail(WishDT, "TransactionId", objPurchase.OrderID.ToString(), Server.MapPath("~/ShippingFiles/") + name + ".pdf", billnumber);
                        }
                        catch (Exception)
                        {
                            objPurchase.LanguageID = ObjBookOrders.LanguageID;
                            objPurchase.OrderID = Convert.ToInt64(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["order"])));
                            WishDT = objPurchase.GetCartListByOrderID();
                            lblAlert.Text = Localization.ResourceManager.GetString("Success", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                            lblMessage.Text = Localization.ResourceManager.GetString("purchaseBookSuccess", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                            h3.Attributes.CssStyle.Add("background-color", "#3B9A02");

                            SendCustomerOrderEmail(WishDT, "TransactionId", objPurchase.OrderID.ToString());
                        }
                    }
                    else
                    {
                        objPurchase.LanguageID = ObjBookOrders.LanguageID;
                        objPurchase.OrderID = Convert.ToInt64(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["order"])));
                        WishDT = objPurchase.GetCartListByOrderID();
                        lblAlert.Text = Localization.ResourceManager.GetString("Success", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                        lblMessage.Text = Localization.ResourceManager.GetString("purchaseBookSuccess", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                        h3.Attributes.CssStyle.Add("background-color", "#3B9A02");

                        SendCustomerOrderEmail(WishDT, "TransactionId", objPurchase.OrderID.ToString());
                    }
                }
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
            }
            else
            {
                Label1.Text = "Oooops, something went wrong...";
                lblAlert.Text = "Error";
                //   lblMessage.Text = "";
                lblMessage.Text = "Sorry,you have filled wrong information,transaction was failed,Please try again later.";
                h3.Attributes.CssStyle.Add("background-color", "red");
            }
        }
    }

    public void Base64ToImage(string base64String, string filename)
    {
        // Convert Base64 String to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);
        byte[] bytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        // Convert byte[] to PDF
        System.IO.FileStream stream = new FileStream(filename, FileMode.CreateNew);
        System.IO.BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(bytes, 0, bytes.Length);
        writer.Close();
        // Convert byte[] to Image
    }

    DataTable dt = new DataTable();
    public void SendCustomerOrderEmail(DataTable ebooksDT, string transactionID, string OrderID, string path, string billNumber)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();
            }
        }
        catch { }

        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b></h3><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No: </b> " + OrderID + "</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Tracking Number: </b> " + billNumber + "</span><br>");
        sb.Append("<table width='550' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        int qty = 0;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            qty += Convert.ToInt32(dr["Qauntity"].ToString());
            price += Convert.ToDouble(dr["Amount"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='57' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quantity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Qauntity"].ToString() + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["Amount"].ToString() + "</td>");
            sb.Append("</tr>");
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE tbl_BookPurchase set AppCode = " + billNumber + " where PurchaseID = " + ebooksDT.Rows[i]["PurchaseID"] + "");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Book(s):</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>" + qty + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (Request.QueryString["email"] != null)
        {
            Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString(), path);
            //Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString(), path);
            //Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString());
        }
        DataTable dtLIst = DAL.SqlHelper.ExecuteDataTable(CommandType.Text, "SELECT * FROM Registration WHERE IsActive = 1 AND UserType = 1 AND IsDeleted = 0");
        if (dtLIst.Rows.Count > 0)
        {
            if (dtLIst.Rows[0]["AlternetEmailAddress"].ToString() != "")
            {
                Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString() + ", " + dtLIst.Rows[0]["AlternetEmailAddress"].ToString(), "LEA eBooks | User Order information", sb.ToString());
            }
            else
            {
                Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString(), "LEA eBooks | User Order information", sb.ToString());
            }
        }
        //App.SendEmail(Global.UserEmail, "noreply@vrinsofts.com", "Order Confirmation", sb.ToString());
    }

    public void SendCustomerOrderEmail(DataTable ebooksDT, string transactionID, string OrderID)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();
            }
        }
        catch { }

        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b></h3><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:</span><br>");
        sb.Append("<span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No: </b> " + OrderID + "</span><br>");
        sb.Append("<table width='550' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        int qty = 0;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            qty += Convert.ToInt32(dr["Qauntity"].ToString());
            price += Convert.ToDouble(dr["Amount"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='57' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quantity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Qauntity"].ToString() + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["Amount"].ToString() + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Book(s):</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>" + qty + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (Request.QueryString["email"] != null)
        {
            Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString());
        }
        DataTable dtLIst = DAL.SqlHelper.ExecuteDataTable(CommandType.Text, "SELECT * FROM Registration WHERE IsActive = 1 AND UserType = 1 AND IsDeleted = 0");
        if (dtLIst.Rows.Count > 0)
        {
            if (dtLIst.Rows[0]["AlternetEmailAddress"].ToString() != "")
            {
                Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString() + ", " + dtLIst.Rows[0]["AlternetEmailAddress"].ToString(), "LEA eBooks | User Order information", sb.ToString());
            }
            else
            {
                Global.SendEmail(dtLIst.Rows[0]["EmailAddress"].ToString(), "LEA eBooks | User Order information", sb.ToString());
            }
        }
        //App.SendEmail(Global.UserEmail, "noreply@vrinsofts.com", "Order Confirmation", sb.ToString());
    }


}
#endregion

#region Testing Code

/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net;
using System.IO;
using BAL;
using System.Data;
using System.Text;
using System.Globalization;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Xml;

public partial class PDT_Default : System.Web.UI.Page
{
    string authToken, txToken, query;
    string strResponse;
    Security S = new Security();

    protected void Page_Load(object sender, EventArgs e)
    {
        BookOrderBAL ObjBookOrders = new BookOrderBAL();
        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
        if (!Page.IsPostBack)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;

            if (Session["CurrentCulture"] != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Session["CurrentCulture"].ToString());
            }

            // Used parts from https://www.paypaltech.com/PDTGen/
            // Visit above URL to auto-generate PDT script

            authToken = "mAbIbsf7Zh-bkso8UVOrc_KgtV3NYP0vMeOWJ14pWlwbvekX5Loi7jSzvlm";//WebConfigurationManager.AppSettings["PDTToken"];
            //mrnjz3y647p6xs8z$f9d029b49d00791797da31f1363e2e2a
            //read in txn token from querystring
            txToken = Request.QueryString.Get("tx");
            // txToken = "34852288H9684823N";

            query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
            //query = string.Format("cmd=_notify-synch&at={0}", authToken);

            // Create the request back
            //string url = "https://www.paypal.com/cgi-bin/webscr";// WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";// WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            // Write the request back IPN strings
            //StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            //stOut.Write(query);
            //stOut.Close();

            // Do the request to PayPal and get the response
            //StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            //strResponse = stIn.ReadToEnd();
            //stIn.Close();

            Label2.Text = strResponse;
            // sanity check

            // If response was SUCCESS, parse response string and output details
            if (Request.QueryString["Success"] != null && Request.QueryString["Success"].ToString() == "1")
            {
                //try
                //{
                //if (strResponse.StartsWith("SUCCESS"))
                //{
                //PDTHolder pdt = PDTHolder.Parse(strResponse);

                //Label1.Text = string.Format("Thank you {0} {1} [{2}] for your payment of {3} {4}!",
                //    pdt.PayerFirstName, pdt.PayerLastName, pdt.PayerEmail, pdt.GrossTotal, pdt.Currency);
                //if (pdt.Custom == "" && Request.QueryString["item_number"] != null)
                //{
                //    pdt.Custom = Request.QueryString["item_number"].ToString();
                //}
                //Label3.Text = pdt.Custom;
                //Label4.Text = pdt.InvoiceNumber.ToString();
                //Label5.Text = pdt.TransactionId;

                //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                //{
                //    ObjBookOrders.LanguageID = 2;
                //}
                //else
                //{
                //    ObjBookOrders.LanguageID = 1;
                //}

                //ObjBookOrders.OrderNo = pdt.Custom;// Request.QueryString["item_number"].ToString();
                //if (string.IsNullOrEmpty(pdt.Custom) && !string.IsNullOrEmpty(Request.QueryString["order"]))
                {
                    ObjBookOrders.OrderNo = S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["order"]));
                }
                DataTable WishDT = ObjBookOrders.GetCartListByOrderNo();
                Shipment s = new Shipment();

                int result = 0;
                if (WishDT != null && WishDT.Rows.Count > 0)
                {
                    StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/Shipment1.xml"));
                    string text = streamReader.ReadToEnd();
                    var piceses = "";
                    decimal weight = 0;
                    var cnt = 0;
                    for (int i = 0; i < WishDT.Rows.Count; i++)
                    {
                        objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(System.DateTime.Now);
                        //objPurchase.TransID = pdt.TransactionId;
                        objPurchase.TransID = "";
                        result = objPurchase.MoveToBookPurchase(Request.QueryString["Address"]);
                        if(Convert.ToBoolean(WishDT.Rows[i]["IsPaperBook"]))
                        {
                            var quantity = Convert.ToInt32(WishDT.Rows[i]["Qauntity"]);
                            for (int j = 0; j < quantity; j++)
                            {
                                cnt++;
                                try
                                {
                                    weight += Convert.ToDecimal(WishDT.Rows[i]["Weight"]);
                                    piceses += "<Piece><PieceID>" + (i + 1) + "</PieceID><PackageType>EE</PackageType><Weight>"
                                               + WishDT.Rows[i]["Weight"] + "</Weight><DimWeight>" + WishDT.Rows[i]["DimWeight"]
                                               + "</DimWeight><Width>" + WishDT.Rows[i]["Width"] + "</Width><Height>"
                                               + WishDT.Rows[i]["Height"] + "</Height><Depth>"
                                               + WishDT.Rows[i]["Depth"] + "</Depth></Piece>";
                                }
                                catch (Exception)
                                {
                                    weight += 1;
                                    piceses += "<Piece><PieceID>" + (i + 1) + "</PieceID><PackageType>EE</PackageType><Weight>"
                                               + "1.0" + "</Weight><DimWeight>" + "1200.0"
                                               + "</DimWeight><Width>" + "100" + "</Width><Height>"
                                               + "200" + "</Height><Depth>"
                                               + "300" + "</Depth></Piece>";
                                }
                            }
                        }
                    }
                    string xmlRequest = s.replaceXml(text, cnt.ToString(), piceses, weight.ToString(), Request.QueryString["ShippingCharge"]);
                    string response = s.sendRequest(xmlRequest);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(response);

                    var ProfilePicture = xmlDoc.DocumentElement.SelectSingleNode("LabelImage/OutputImage").InnerText;
                    var billnumber = xmlDoc.DocumentElement.SelectSingleNode("AirwayBillNumber").InnerText;
                    string name = Convert.ToString(DateTime.Now.Ticks);
                    if (!string.IsNullOrEmpty(ProfilePicture))
                    {
                        Base64ToImage(ProfilePicture, Server.MapPath("~/ShippingFiles/") + name + ".pdf");
                    }
                    objPurchase.LanguageID = ObjBookOrders.LanguageID;
                    objPurchase.OrderID = Convert.ToInt64(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["order"])));
                    WishDT = objPurchase.GetCartListByOrderID();
                    lblAlert.Text = Localization.ResourceManager.GetString("Success", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    lblMessage.Text = Localization.ResourceManager.GetString("purchaseBookSuccess", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    //lblAlert.Text = "Success";
                    //lblMessage.Text = " Thank you for purchasing Book,your order detail is mailed to you.";
                    h3.Attributes.CssStyle.Add("background-color", "#3B9A02");
                    //if (Session["website"] != null)
                    //{
                    //    //string retJSON = "";
                    //    //retJSON = "[{\"Code\":" + "01" + ",\"Message\":\"" + "RemoveHTML(Message)" + "\"}]";
                    //    //Context.Response.Output.Write(retJSON);
                    //    //Response.Write(retJSON);
                    //    Response.Redirect(@"~/OrderReport.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&payid=1");
                    //}
                    //else
                    //{
                    //    Response.Redirect(@"~/OrderReport.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&payid=1");
                    //}

                    //SendCustomerOrderEmail(WishDT, pdt.TransactionId, pdt.Custom);
                    SendCustomerOrderEmail(WishDT, "TransactionId", objPurchase.OrderID.ToString(), Server.MapPath("~/ShippingFiles/") + name + ".pdf", billnumber);
                    //CreatePDFInvoice(WishDT, pdt.TransactionId, pdt.Custom);
                }
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
            }
            else
            {
                Label1.Text = "Oooops, something went wrong...";
                lblAlert.Text = "Error";
                //   lblMessage.Text = "";
                lblMessage.Text = "Sorry,you have filled wrong information,transaction was failed,Please try again later.";
                h3.Attributes.CssStyle.Add("background-color", "red");
            }
        }
    }

    public void Base64ToImage(string base64String, string filename)
    {
        // Convert Base64 String to byte[]
        byte[] imageBytes = Convert.FromBase64String(base64String);
        byte[] bytes = Convert.FromBase64String(base64String);
        MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

        // Convert byte[] to PDF
        System.IO.FileStream stream = new FileStream(filename, FileMode.CreateNew);
        System.IO.BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(bytes, 0, bytes.Length);
        writer.Close();
        // Convert byte[] to Image
    }

    private void CreatePDFInvoice(DataTable ebooksDT, string transactionID, string OrderID)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();


            }
        }
        catch { }
        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b><br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No.</b> " + OrderID + "</h3><br>");
        sb.Append("<table width='493' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            //price += Convert.ToDouble(dr["FinalPrice1"].ToString());
            price += Convert.ToDouble(dr["FinalPrice1"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["FinalPrice1"].ToString() + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='493' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");

        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");

        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");

        StringReader sr = new StringReader(sb.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (Request.QueryString["email"] != null)
        {
            Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString());
        }
    }

    DataTable dt = new DataTable();

    public void SendCustomerOrderEmail(DataTable ebooksDT, string transactionID, string OrderID, string path,string billNumber)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();
            }
        }
        catch { }

        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b><br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No: </b> " + OrderID + "</h3><br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Tracking Number: </b> " + billNumber + "</h3><br>");
        sb.Append("<table width='550' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        int qty = 0;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            qty += Convert.ToInt32(dr["Qauntity"].ToString());
            price += Convert.ToDouble(dr["Amount"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='57' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Quantity&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Qauntity"].ToString() + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["Amount"].ToString() + "</td>");
            sb.Append("</tr>");
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "UPDATE tbl_BookPurchase set AppCode = " + billNumber + " where PurchaseID = " + ebooksDT.Rows[i]["PurchaseID"] + "");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Book(s):</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>" + qty + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width='550' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");
        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");
        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (Request.QueryString["email"] != null)
        {
            Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString(), path);
            //Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString(), path);
            //Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString());
        }
        //App.SendEmail(Global.UserEmail, "noreply@vrinsofts.com", "Order Confirmation", sb.ToString());
    }

}*/

#endregion

#region Live code
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Net;
using System.IO;
using BAL;
using System.Data;
using System.Text;
using System.Globalization;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class PDT_Default : System.Web.UI.Page
{
    string authToken, txToken, query;
    string strResponse;
    Security S = new Security();

    protected void Page_Load(object sender, EventArgs e)
    {
        BookOrderBAL ObjBookOrders = new BookOrderBAL();
        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
        if (!Page.IsPostBack)
        {
            if (Session["CurrentCulture"] != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Session["CurrentCulture"].ToString());
            }

            // Used parts from https://www.paypaltech.com/PDTGen/
            // Visit above URL to auto-generate PDT script

            authToken = "mAbIbsf7Zh-bkso8UVOrc_KgtV3NYP0vMeOWJ14pWlwbvekX5Loi7jSzvlm";//WebConfigurationManager.AppSettings["PDTToken"];
            //mrnjz3y647p6xs8z$f9d029b49d00791797da31f1363e2e2a
            //read in txn token from querystring
            txToken = Request.QueryString.Get("tx");
            // txToken = "34852288H9684823N";

            query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, authToken);
            //query = string.Format("cmd=_notify-synch&at={0}", authToken);

            // Create the request back
            //string url = "https://www.paypal.com/cgi-bin/webscr";// WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";// WebConfigurationManager.AppSettings["PayPalSubmitUrl"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            // Set values for the request back
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            // Write the request back IPN strings
            StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            stOut.Write(query);
            stOut.Close();

            // Do the request to PayPal and get the response
            StreamReader stIn = new StreamReader(req.GetResponse().GetResponseStream());
            strResponse = stIn.ReadToEnd();
            stIn.Close();

            Label2.Text = strResponse;
            // sanity check

            // If response was SUCCESS, parse response string and output details
            if (Request.QueryString["Success"] != null && Request.QueryString["Success"].ToString() == "1")
            {
                //try
                //{
                //if (strResponse.StartsWith("SUCCESS"))
                //{
                PDTHolder pdt = PDTHolder.Parse(strResponse);

                Label1.Text = string.Format("Thank you {0} {1} [{2}] for your payment of {3} {4}!",
                    pdt.PayerFirstName, pdt.PayerLastName, pdt.PayerEmail, pdt.GrossTotal, pdt.Currency);
                if (pdt.Custom == "" && Request.QueryString["item_number"] != null)
                {
                    pdt.Custom = Request.QueryString["item_number"].ToString();
                }
                Label3.Text = pdt.Custom;
                Label4.Text = pdt.InvoiceNumber.ToString();
                Label5.Text = pdt.TransactionId;

                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ObjBookOrders.LanguageID = 2;
                }
                else
                {
                    ObjBookOrders.LanguageID = 1;
                }

                ObjBookOrders.OrderNo = pdt.Custom;// Request.QueryString["item_number"].ToString();
                if (string.IsNullOrEmpty(pdt.Custom) && !string.IsNullOrEmpty(Request.QueryString["order"]))
                {
                    ObjBookOrders.OrderNo = S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["order"]));
                }
                DataTable WishDT = ObjBookOrders.GetCartListByOrderNo();

                int result = 0;
                if (WishDT != null && WishDT.Rows.Count > 0)
                {
                    for (int i = 0; i < WishDT.Rows.Count; i++)
                    {
                        objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);
                        objPurchase.PurchaseDate = Convert.ToDateTime(System.DateTime.Now);
                        objPurchase.TransID = pdt.TransactionId;
                        result = objPurchase.MoveToBookPurchase(Request.QueryString["Address"]);
                    }
                    objPurchase.LanguageID = ObjBookOrders.LanguageID;
                    objPurchase.OrderID = Convert.ToInt64(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["order"])));
                    WishDT = objPurchase.GetCartListByOrderID();
                    lblAlert.Text = Localization.ResourceManager.GetString("Success", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    lblMessage.Text = Localization.ResourceManager.GetString("purchaseBookSuccess", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    //lblAlert.Text = "Success";
                    //lblMessage.Text = " Thank you for purchasing Book,your order detail is mailed to you.";
                    h3.Attributes.CssStyle.Add("background-color", "#3B9A02");
                    //if (Session["website"] != null)
                    //{
                    //    //string retJSON = "";
                    //    //retJSON = "[{\"Code\":" + "01" + ",\"Message\":\"" + "RemoveHTML(Message)" + "\"}]";
                    //    //Context.Response.Output.Write(retJSON);
                    //    //Response.Write(retJSON);
                    //    Response.Redirect(@"~/OrderReport.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&payid=1");
                    //}
                    //else
                    //{
                    //    Response.Redirect(@"~/OrderReport.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&payid=1");
                    //}
                    
                    SendCustomerOrderEmail(WishDT, pdt.TransactionId, pdt.Custom);
                    //CreatePDFInvoice(WishDT, pdt.TransactionId, pdt.Custom);
                }
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
            }
            else
            {
                Label1.Text = "Oooops, something went wrong...";
                lblAlert.Text = "Error";
                //   lblMessage.Text = "";
                lblMessage.Text = "Sorry,you have filled wrong information,transaction was failed,Please try again later.";
                h3.Attributes.CssStyle.Add("background-color", "red");
            }
        }
    }

    private void CreatePDFInvoice(DataTable ebooksDT, string transactionID, string OrderID)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();


            }
        }
        catch { }
        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b><br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No.</b> " + OrderID + "</h3><br>");
        sb.Append("<table width='493' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            //price += Convert.ToDouble(dr["FinalPrice1"].ToString());
            price += Convert.ToDouble(dr["FinalPrice1"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["FinalPrice1"].ToString() + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='493' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");

        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");

        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");

        StringReader sr = new StringReader(sb.ToString());
        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        pdfDoc.Open();
        htmlparser.Parse(sr);
        pdfDoc.Close();
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-disposition", "attachment;filename=Invoice_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Write(pdfDoc);
        Response.End();
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (Request.QueryString["email"] != null)
        {
            Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString());
        }
    }

    DataTable dt = new DataTable();

    public void SendCustomerOrderEmail(DataTable ebooksDT, string transactionID, string OrderID)
    {
        StringBuilder sb = new StringBuilder();
        string UserName = "";
        string Email = "";
        string contactmail = "";
        string Author = "";
        string Ebookname = "";
        try
        {
            if (ebooksDT != null && ebooksDT.Rows.Count > 0)
            {
                UserName = ebooksDT.Rows[0]["FirstName"].ToString();
                Email = ebooksDT.Rows[0]["Email"].ToString();
                Author = ebooksDT.Rows[0]["Autoher"].ToString();
                Ebookname = ebooksDT.Rows[0]["Title"].ToString();
                contactmail = ebooksDT.Rows[0]["contactmail"].ToString();


            }
        }
        catch { }

        string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
          + "     <tr> "
          + "         <td  style=\";Font-size:20px;\"> "
          + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
        sb.Append(Result);
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b><br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:<br>");
        sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
        sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No.</b> " + OrderID + "</h3><br>");
        sb.Append("<table width='493' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
        sb.Append("<tbody>");
        double price = 0.0f;
        string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        foreach (DataRow dr in ebooksDT.Rows)
        {
            //price += Convert.ToDouble(dr["FinalPrice1"].ToString());
            price += Convert.ToDouble(dr["FinalPrice1"].ToString());
        }
        sb.Append("<tr style='margin-bottom: 10px;'>");
        sb.Append("<td width='70' align='left'>");
        sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
        sb.Append("</tr>");
        for (int i = 0; i < ebooksDT.Rows.Count; i++)
        {
            sb.Append("<tr style='margin-bottom: 10px;'>");
            sb.Append("<td width='70' align='left'>");
            sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString() + "></td>");
            sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> </td>");
            sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
            sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["FinalPrice1"].ToString() + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("<br>");

        sb.Append("<table width='493' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
        sb.Append("<tr>");

        sb.Append("<td width='70' align='left'>  &nbsp;</td>");
        sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
        sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
        sb.Append("</tr>");

        sb.Append("</tbody></table><br />");

        sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
        sb.Append("<tr>");
        sb.Append("<td>");
        sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
        sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</tbody>");
        sb.Append("</table>");
        sb.Append("</td>");
        sb.Append("</tr>");
        sb.Append("</table>");
        Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
        if (Request.QueryString["email"] != null)
        {
            Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
        }
        else
        {
            Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString());
        }


        //App.SendEmail(Global.UserEmail, "noreply@vrinsofts.com", "Order Confirmation", sb.ToString());
    }

}*/
#endregion