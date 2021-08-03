using BAL;
using Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    BookBAL Obj_Book = new BookBAL();
    Security S = new Security();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session.Clear();
        if (Request.Url.ToString().Contains("/us/"))
        {
            Response.Redirect(Request.Url.ToString().Replace("/us", ""));
        }

        //if (Request.Url.ToString().Contains("en-US"))
        //{
        //    Response.Redirect(Request.Url.ToString().Replace("/us", ""));
        //}

        //if (Session["ErrorText"] != null)
        //{
        //    lblError.Text = Session["ErrorText"].ToString();
        //    Session["ErrorText"] = null;
        //}
        Session["RedirectUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;

        if (!IsPostBack)
        {
            var source = new Dictionary<int, string>();
            source.Add(-1, Localization.ResourceManager.GetString("sortby", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
            source.Add(1, Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
            source.Add(0, Localization.ResourceManager.GetString("paid", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
            ddlSortby.DataSource = source;
            ddlSortby.DataTextField = "Value";
            ddlSortby.DataValueField = "Key";
            ddlSortby.DataBind();

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (Request.QueryString["l"] == null)
            {
                Response.Redirect(url + "?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
            addtoLibrary();
            //ResizeImage();
            BindData();
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                addtocart();
            }

            if (Request.QueryString["loginid"] != null)
            {
                BAL_Account obj_login = new BAL_Account();
                RegistrationBAL objUser = new RegistrationBAL();
                objUser.ActivationID = Request.QueryString["loginid"].ToString();
                DataTable dt = objUser.ActiveAccount();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["UserType"].ToString() == "4")
                    {
                        obj_login.UserName = dt.Rows[0]["EmailAddress"].ToString();
                        obj_login.Password = dt.Rows[0]["Password"].ToString();
                        DataTable dt1 = new DataTable();
                        dt1 = obj_login.Check_Login();
                        if (dt1.Rows.Count > 0)
                        {
                            //Session["UserName"] = dt1.Rows[0]["FirstName"].ToString();
                            //Session["UserID"] = dt1.Rows[0]["RegistrationID"].ToString();

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');window.location ='Partner/Default.aspx';", ResourceManager.GetString("account has been activated", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);

                        }
                    }
                    else
                    {
                        obj_login.UserName = dt.Rows[0]["EmailAddress"].ToString();
                        obj_login.Password = dt.Rows[0]["Password"].ToString();
                        DataTable dt1 = new DataTable();
                        dt1 = obj_login.Check_Login();
                        if (dt1.Rows.Count > 0)
                        {
                            Session["UserName"] = dt1.Rows[0]["FirstName"].ToString();
                            Session["UserID"] = dt1.Rows[0]["RegistrationID"].ToString();

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("account has been activated", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);

                        }
                    }
                }
            }
        }
    }

    public static string GetMACAddress1()
    {
        ManagementObjectSearcher objMOS = new ManagementObjectSearcher("Select * FROM Win32_NetworkAdapterConfiguration");
        ManagementObjectCollection objMOC = objMOS.Get();
        string macAddress = String.Empty;
        foreach (ManagementObject objMO in objMOC)
        {
            object tempMacAddrObj = objMO["MacAddress"];

            if (tempMacAddrObj == null) //Skip objects without a MACAddress
            {
                continue;
            }
            if (macAddress == String.Empty) // only return MAC Address from first card that has a MAC Address
            {
                macAddress = tempMacAddrObj.ToString();
            }
            objMO.Dispose();
        }
        //macAddress = macAddress.Replace(":", "");
        return macAddress;
    }

    private Color setColorLabel(string country)
    {
        Country objC = new Country();
        var dt = objC.SelectAllActiveCountry();
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["countryname"].ToString() == country)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString()))
                    {
                        return Color.ForestGreen;
                    }
                    else
                        return Color.Red;
                }
            }
            return Color.Red;
        }
        else
        {
            return Color.Red;
        }
        return Color.Red;
    }

    //private void SendMailCheck()
    //{
    //    BookPurchaseBAL objPurchase = new BookPurchaseBAL();

    //    objPurchase.LanguageID = 1;
    //    //objPurchase.OrderID = Convert.ToInt64(S.Decrypt(HttpUtility.UrlDecode("MIBdOD9SdWccpN0oCvcFTfANrq5R+Wj137PVn34tjVA=")));
    //    objPurchase.OrderID = Convert.ToInt64(S.Decrypt(HttpUtility.UrlDecode("IFIdSKtrNyAHqpczlb8rR%2f2I0lgAd9Z%2b%2fLSOxMjSZLETnYcCwZoTg%2f7WSx0UkMtZ")));
    //    DataTable WishDT = objPurchase.GetCartListByOrderID();
    //    if (WishDT != null && WishDT.Rows.Count > 0)
    //    {
    //        SendCustomerOrderEmail(WishDT, "XYZPQRABC", "55555");
    //    }
    //}

    //public void SendCustomerOrderEmail(DataTable ebooksDT, string transactionID, string OrderID)
    //{
    //    StringBuilder sb = new StringBuilder();
    //    string UserName = "";
    //    string Email = "";
    //    string contactmail = "";
    //    string Author = "";
    //    string Ebookname = "";
    //    try
    //    {
    //        if (ebooksDT != null && ebooksDT.Rows.Count > 0)
    //        {
    //            UserName = ebooksDT.Rows[0]["FirstName"].ToString();
    //            Email = ebooksDT.Rows[0]["Email"].ToString();
    //            Author = ebooksDT.Rows[0]["Autoher"].ToString();
    //            Ebookname = ebooksDT.Rows[0]["Title"].ToString();
    //            contactmail = ebooksDT.Rows[0]["contactmail"].ToString();


    //        }
    //    }
    //    catch { }

    //    string Result = " <table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px;border-color:gray;background-color:#eee\" > "
    //      + "     <tr> "
    //      + "         <td  style=\";Font-size:20px;\"> "
    //      + "<b   style=\"background-color:#001c4a;  display: block;    padding: 7px; \"><img src=\"https://www.leaebook.com/images/header_logo.png\" alt=\"Mo-Focus\" title=\"Mo-Focus\" style=\"height: 40px; width: 40px;\" /><div style=\"margin-left: 55px; margin-top: -25px; color:White;\">LEA eBooks</div></b>";
    //    sb.Append(Result);
    //    sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 18px; font-weight: normal;color: rgb(51, 51, 51);'>");
    //    sb.Append("<b>&nbsp;LEA eBooks | &nbsp;Order information</b><br>");
    //    sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
    //    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Hello&nbsp;" + UserName + ",<br>");
    //    sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
    //    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Thank you for your order !<br>");
    //    sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'>");
    //    sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;Please find below, the summary of your order:<br>");
    //    sb.Append("<h3 style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51);'><br>");
    //    sb.Append("<b>&nbsp;&nbsp;&nbsp;&nbsp;Order No.</b> " + OrderID + "</h3><br>");
    //    sb.Append("<table width='493' height='120' cellspacing='0' cellpadding='5' border='0' style='border-bottom: solid 1px #ccc;'>");
    //    sb.Append("<tbody>");
    //    double price = 0.0f;
    //    string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
    //    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
    //    foreach (DataRow dr in ebooksDT.Rows)

    //    {
    //        //price += Convert.ToDouble(dr["FinalPrice1"].ToString());
    //        price += Convert.ToDouble(dr["Amount"].ToString());
    //    }
    //    sb.Append("<tr style='margin-bottom: 10px;'>");
    //    sb.Append("<td width='70' align='left'>");
    //    sb.Append("<b style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Image</b></td>");
    //    sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;eBookName&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
    //    sb.Append("<td width='283' valign='middle'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Author&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|<br></span> </td>");
    //    sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold; color: rgb(62, 62, 62);'>&nbsp;&nbsp;&nbsp;Price</td>");
    //    sb.Append("</tr>");
    //    for (int i = 0; i < ebooksDT.Rows.Count; i++)
    //    {
    //        sb.Append("<tr style='margin-bottom: 10px;'>");
    //        sb.Append("<td width='70' align='left'>");
    //        sb.Append("<img border='0' title='' alt='' style='margin: 0px;height:150px;width:120px;' src=" + Config.WebSiteMain + "Book/" + ebooksDT.Rows[i]["CategoryID"].ToString() + "/" + ebooksDT.Rows[i]["ImagePath"].ToString().Replace(".jpg","_1.jpg") + "></td>");
    //        sb.Append("<td width='270' valign='middle' style = 'padding-left: 22px;'><span style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["eBookName"].ToString() + "<br></span> ");
    //        if(Convert.ToBoolean(ebooksDT.Rows[i]["IseBook"]))
    //        {
    //            sb.Append("Purchased As eBook");
    //        }
    //        else if (Convert.ToBoolean(ebooksDT.Rows[i]["IsPaperBook"]))
    //        {
    //            sb.Append("Purchased As Paper Book <br />");
    //            sb.Append("Quantity : " + ebooksDT.Rows[i]["Qauntity"]);
    //        }
    //        sb.Append("</td>");
    //        sb.Append("<td width='270' valign='middle'><span style='font-family:Calibri , sans-serif; font-size: 11.0pt;padding-left: 4px; font-weight: bold;color: rgb(62, 62, 62);'>" + ebooksDT.Rows[i]["Autoher"].ToString() + "<br></span> </td>");
    //        sb.Append("<td width='78' valign='middle' align='center' style='font-family: Calibri , sans-serif;font-size: 11.0pt; font-weight: normal; color: rgb(62, 62, 62);'>$" + ebooksDT.Rows[i]["Amount"].ToString() + "</td>");
    //        sb.Append("</tr>");
    //    }

    //    sb.Append("</tbody>");
    //    sb.Append("</table>");
    //    sb.Append("<br>");

    //    sb.Append("<table width='493' height='auto' cellspacing='5' cellpadding='5' border='0' align='center'> <tbody>");
    //    sb.Append("<tr>");

    //    sb.Append("<td width='70' align='left'>  &nbsp;</td>");
    //    sb.Append("<td valign='middle' align='right' style='font-family: Calibri , sans-serif;font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>Total Charged:</td>");
    //    sb.Append(" <td width='75' valign='middle' align='center' style='font-family: Calibri , sans-serif; font-size: 11.0pt; color: rgb(0, 80, 161); font-weight: bold;'>$ " + price + "</td>");
    //    sb.Append("</tr>");

    //    sb.Append("</tbody></table><br />");

    //    sb.Append("<table width=\"600\" border=\"0\" align=\"center\" cellspacing='0' cellpadding='0' border=''0' ><tbody>");
    //    sb.Append("<tr>");
    //    sb.Append("<td>");
    //    sb.Append(" <div style='font-family: Calibri , sans-serif; font-size: 11.0pt; font-weight: normal;color: rgb(51, 51, 51); text-align: justify;'>");
    //    sb.Append("<span>&nbsp;&nbsp;NOTE :</span> <span>If you have any questions or further query then please contact us:<br /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + contactmail + "</div>");
    //    sb.Append("</td>");
    //    sb.Append("</tr>");
    //    sb.Append("</tbody>");
    //    sb.Append("</table>");
    //    sb.Append("</td>");
    //    sb.Append("</tr>");
    //    sb.Append("</table>");
    //    Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
    //    if (Request.QueryString["email"] != null)
    //    {
    //        Global.SendEmail(Request.QueryString["email"].ToString(), "LEA eBooks | Your Order Confirmation", sb.ToString());
    //    }
    //    else
    //    {
    //        Global.SendEmail(Email, "LEA eBooks | Your Order information", sb.ToString());
    //    }


    //    //App.SendEmail(Global.UserEmail, "noreply@vrinsofts.com", "Order Confirmation", sb.ToString());
    //}

    public void addtoLibrary()
    {
        int count = 0;
        if (Request.QueryString["fbid"] != null && Request.QueryString["fbid"].ToString() != "")
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                objPurchase.BookID = Convert.ToInt16(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["fbid"])).ToString());
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    objPurchase.LanguageID = 2;
                }
                else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                {
                    objPurchase.LanguageID = 1;
                }
                DataTable DT = objPurchase.getUserLibrary();
                if (DT != null && DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == objPurchase.BookID)
                        {
                            count++;
                        }

                    }
                    if (count > 0)
                    {
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{
                        // ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);
                        //}
                        //else
                        //{
                        // ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);
                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book already exists in your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                    }
                    else
                    {
                        objPurchase.MoveToBookPurchase_freebook_website();
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{

                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro agregado a su biblioteca');", true);

                        //}
                        //else
                        //{

                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library');", true);

                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book added successfully to your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);

                        //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["fbid"].ToString());
                    }
                }
                else
                {

                    objPurchase.MoveToBookPurchase_freebook_website();
                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //{

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro agregado a su biblioteca');", true);

                    //}
                    //else
                    //{

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library');", true);

                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book added successfully to your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                    //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["fbid"].ToString());
                }
            }
            else
            {
                Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }
    }

    public void addtocart()
    {
        int counteBook = 0;
        int countPaper = 0;
        int result = 0;
        string Qunatity = "";
        if (ViewState["IsFree"] != null && ViewState["IsFree"].ToString() == "True")
        {
            addtoLibrary();
        }
        else
        {
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();

            int cnt1 = 0;

            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    try
                    {
                        var bookId = S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]).ToString());
                        Obj_Book.BookID = objPurchase.BookID = Convert.ToInt32(bookId);
                    }
                    catch
                    {
                        Obj_Book.BookID = objPurchase.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
                    }
                }
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    objPurchase.LanguageID = 2;
                    Obj_Book.LanguageID = 2;
                }
                else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                {
                    objPurchase.LanguageID = 1;
                    Obj_Book.LanguageID = 1;
                }
                DataTable Dt = objPurchase.getUserLibrary();
                if (Dt != null && Dt.Rows.Count > 0)
                {
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        var tableBook = Obj_Book.getBookDetails();
                        var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                        var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                        if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                        {
                            if (iseBook == Convert.ToBoolean(Dt.Rows[i]["IseBook"]))// && isPaperBook == Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                cnt1++;
                        }

                    }

                    Session["AddToCart"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                    DataTable dtBookDetail = new DataTable();
                    Obj_Book.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                    Obj_Book.LanguageID = 1;
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        Obj_Book.LanguageID = 2;
                    }
                    else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                    {
                        Obj_Book.LanguageID = 1;
                    }
                    dtBookDetail = Obj_Book.getBookDetails();

                    DataTable DT = Obj_bookOrder.GetCartList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        if (string.IsNullOrEmpty(Obj_bookOrder.OrderNo))
                        {
                            string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                            Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                            Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                        }
                        Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            var tableBook = Obj_Book.getBookDetails();
                            var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                            var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                            {
                                if (iseBook == Convert.ToBoolean(DT.Rows[i]["IseBook"]))// && isPaperBook == Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                    counteBook++;
                                if (isPaperBook == Convert.ToBoolean(DT.Rows[i]["IsPaperBook"]))
                                    countPaper++;
                            }

                        }

                        if (Convert.ToBoolean(dtBookDetail.Rows[0]["Quantity"].ToString() == "0"))
                        {
                            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado.');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock.');", true);
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                var x = 0;
                                if (counteBook == 0 && cnt1 == 0)
                                {
                                    x++;
                                    Obj_bookOrder.IseBook = true;
                                    Obj_bookOrder.IspaperBook = false;
                                    Obj_bookOrder.Quantity = 0;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                }
                                if (countPaper == 0 && dtBookDetail.Rows[0]["Quantity"].ToString() != "" && dtBookDetail.Rows[0]["Quantity"].ToString() != "0")
                                {
                                    x++;
                                    Obj_bookOrder.IseBook = false;
                                    Obj_bookOrder.IspaperBook = true;
                                    Obj_bookOrder.Quantity = 1;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                }
                                if (x == 0)
                                {
                                    //Message1("You already have this book in your cart");
                                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                                    }
                                }
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && counteBook == 0 && cnt1 == 0)
                            {
                                Obj_bookOrder.IseBook = true;
                                Obj_bookOrder.IspaperBook = false;
                                Obj_bookOrder.Quantity = 0;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()) && countPaper == 0 && dtBookDetail.Rows[0]["Quantity"].ToString() != "" && dtBookDetail.Rows[0]["Quantity"].ToString() != "0")
                            {
                                Obj_bookOrder.IseBook = false;
                                Obj_bookOrder.IspaperBook = true;
                                Obj_bookOrder.Quantity = 1;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(dtBookDetail.Rows[0]["Quantity"].ToString() == "0"))
                        {
                            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado...');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock...');", true);
                            }
                        }
                        else
                        {
                            string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                            Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                if (counteBook == 0 && cnt1 == 0)
                                {
                                    Obj_bookOrder.IseBook = true;
                                    Obj_bookOrder.IspaperBook = false;
                                    Obj_bookOrder.Quantity = 0;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                }
                                Obj_bookOrder.IseBook = false;
                                Obj_bookOrder.IspaperBook = true;
                                Obj_bookOrder.Quantity = 1;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && counteBook == 0 && cnt1 == 0)
                            {
                                Obj_bookOrder.IseBook = true;
                                Obj_bookOrder.IspaperBook = false;
                                Obj_bookOrder.Quantity = 0;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                Obj_bookOrder.IseBook = false;
                                Obj_bookOrder.IspaperBook = true;
                                Obj_bookOrder.Quantity = 1;
                                result = Obj_bookOrder.InsertCustomerCart1();
                            }
                            //result = Obj_bookOrder.InsertCustomerCart1();
                            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Security.AES256Encrypt(Session["UserID"].ToString()) + "");
                            //if (result > 0)
                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                            //else
                            //    Response.Redirect(Config.WebSiteMain + "Index.aspx?l=" + Request.QueryString["l"]); //Response.Redirect(Request.Url.ToString() + "");
                        }
                    }

                }

                else
                {
                    Session["AddToCart"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());

                    DataTable dtBookDetail = new DataTable();
                    Obj_Book.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
                    Obj_Book.LanguageID = 1;
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        Obj_Book.LanguageID = 2;
                    }
                    else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                    {
                        Obj_Book.LanguageID = 1;
                    }
                    dtBookDetail = Obj_Book.getBookDetails();

                    //Obj_bookOrder.IseBook = Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString());
                    //Obj_bookOrder.IspaperBook = Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString());
                    //Obj_bookOrder.Quantity = 1;

                    //Obj_bookOrder.InsertCustomerCart1();

                    DataTable DT = Obj_bookOrder.GetCartList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                            {
                                counteBook++;
                            }

                        }
                        if (counteBook > 0)
                        {
                            //Message1("You already have this book in your cart");
                            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                            }
                        }
                        else
                        {
                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["Quantity"].ToString() == "0"))
                            {
                                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Este libro está agotado...');", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('This Book is Out of Stock...');", true);
                                }
                            }
                            else
                            {
                                if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                                {
                                    Obj_bookOrder.IseBook = true;
                                    Obj_bookOrder.IspaperBook = false;
                                    Obj_bookOrder.Quantity = 0;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                    Obj_bookOrder.IseBook = false;
                                    Obj_bookOrder.IspaperBook = true;
                                    Obj_bookOrder.Quantity = 1;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                }
                                else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()))
                                {
                                    Obj_bookOrder.IseBook = true;
                                    Obj_bookOrder.IspaperBook = false;
                                    Obj_bookOrder.Quantity = 0;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                }
                                else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                                {
                                    Obj_bookOrder.IseBook = false;
                                    Obj_bookOrder.IspaperBook = true;
                                    Obj_bookOrder.Quantity = 1;
                                    result = Obj_bookOrder.InsertCustomerCart1();
                                }
                                //result = Obj_bookOrder.InsertCustomerCart1();
                                //if (result > 0)
                                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                                //else
                                //    Response.Redirect(Config.WebSiteMain + "Index.aspx?l=" + Request.QueryString["l"]); //Response.Redirect(Request.Url.ToString() + "");
                            }
                        }
                    }
                    else
                    {
                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                        if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                        {
                            Obj_bookOrder.IseBook = true;
                            Obj_bookOrder.IspaperBook = false;
                            Obj_bookOrder.Quantity = 0;
                            result = Obj_bookOrder.InsertCustomerCart1();
                            Obj_bookOrder.IseBook = false;
                            Obj_bookOrder.IspaperBook = true;
                            Obj_bookOrder.Quantity = 1;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()))
                        {
                            Obj_bookOrder.IseBook = true;
                            Obj_bookOrder.IspaperBook = false;
                            Obj_bookOrder.Quantity = 0;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                        {
                            Obj_bookOrder.IseBook = false;
                            Obj_bookOrder.IspaperBook = true;
                            Obj_bookOrder.Quantity = 1;
                            result = Obj_bookOrder.InsertCustomerCart1();
                        }
                        //result = Obj_bookOrder.InsertCustomerCart1();
                        //if (result > 0)
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                        //else
                        //    Response.Redirect(Config.WebSiteMain + "Index.aspx?l=" + Request.QueryString["l"]);//Response.Redirect(Request.Url.ToString() + "");
                    }
                }
                //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

            }
            else
            {
                if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                {
                    int cnt = 0;
                    string[] str = Session["AddToCart"].ToString().Split(',');
                    foreach (string Add in str)
                    {

                        if (Add == Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString())
                        {
                            cnt++;
                        }
                    }
                    if (cnt > 0)
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                        }
                    }
                    else
                    {
                        Session["AddToCart"] = Session["AddToCart"] + "," + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    }
                }
                else
                {


                    Session["AddToCart"] = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
                //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }
    }

    public static int Language;
    private void GenerateThumbnails(string sourcePath, string targetPath)
    {
        //System.IO.FileStream fs = new System.IO.FileStream(sourcePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        //var image = System.Drawing.Image.FromStream(fs);
        try
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);
            {
                var newWidth = (int)(450);
                var newHeight = (int)(600);
                var thumbnailImg = new Bitmap(newWidth, newHeight);

                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                //File.Delete(targetPath);
                thumbnailImg.Save(targetPath.Replace("\\/", "\\").Replace("/", "\\").Replace(".jpg", "_1.jpg"), ImageFormat.Jpeg);
                //System.IO.File.Copy(filePath, Server.MapPath("~/Uploads"));
            }
        }
        catch (Exception ex)
        { }
    }

    private static void GenerateThumbnailStatic(string sourcePath, string targetPath)
    {
        try
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);
            {
                var newWidth = (int)(450);
                var newHeight = (int)(600);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath.Replace("\\/", "\\").Replace("/", "\\").Replace(".jpg", "_1.jpg"), ImageFormat.Jpeg);
            }
        }
        catch (Exception ex)
        { }
    }

    private void ResizeImage()
    {
        DataTable dt = new DataTable();
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book.LanguageID = Language = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book.LanguageID = Language = 1;
            }
        }
        else
        {
            Obj_Book.LanguageID = Language = 1;
        }
        Obj_Book.EndIndex = -1;
        Obj_Book.StartIndex = 1;
        Obj_Book.LanguageID = 1;
        dt = Obj_Book.get_all_book_website1();
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                    string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "";
                    Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    if (!File.Exists(img.Replace(".jpg", "_1.jpg")))
                    {
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "");
                    }
                }
            }
        }
    }

    public void BindData()
    {
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        viewallbook = Localization.ResourceManager.GetString("viewallbook", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        newrelease = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        //buynowtitle = Localization.ResourceManager.GetString("buynow", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        buynowtitle = Localization.ResourceManager.GetString("addtocart", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        isfreetitle = Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        DataTable dt = new DataTable();
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book.LanguageID = Language = 2;
                string Title = "comprar libro electronico barato, el mejores eBooks Baratos El Salvador- LEA";
                string Description = "Encuentra lector de libros electrónicos baratos en El Salvador. Le presentamos el mejor libro electrónico barato en línea. Tenemos una gran colección de libros electrónicos de diferentes categorías como erotismo, obras de escritores, escritores profesionales reconocidos en América Latina, filosofía y salvadoreños.";
                this.Page.Title = Title;
                this.Page.MetaDescription = Description;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book.LanguageID = Language = 1;
                string Title = "Buy Cheap eBook Online, Best Cheap eBook El Salvador- LEA";
                string Description = "Find cheap electronic book reader in El Salvador. We present you the best cheap ebook online.we have a huge collection of electronic books of different categories such as eroticism, Latin American, Philosophy and Salvadoran works of escritoires recognized professional writers.";
                this.Page.Title = Title;
                this.Page.MetaDescription = Description;
            }
        }
        else
        {
            Obj_Book.LanguageID = Language = 1;
        }

        if (Request.QueryString["PageNo"] != null && Request.QueryString["PageNo"].ToString() != "")
        {
            Obj_Book.EndIndex = Convert.ToInt32(Request.QueryString["PageNo"].ToString()) * 9;
            //Obj_Book.EndIndex = -1;
            Obj_Book.StartIndex = (Obj_Book.EndIndex - 9) + 1;
        }
        else
        {
            HttpContext.Current.Session["num"] = "9";
            //Obj_Book.EndIndex = -1;
            Obj_Book.EndIndex = 9;
            Obj_Book.StartIndex = 1;
        }
        dt = Obj_Book.get_all_book_website2(Convert.ToInt32(ddlSortby.SelectedValue), "New Releases");

        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    int count = Convert.ToInt32(dt.Rows[0]["count"]);
        //    pageno(count);
        //}
        string a1 = "";
        //a1 = "<div class='panel'>" +
        //                "<div class='titnewre'>" + newrelease.ToString() + "</div>" +
        //                "<a href='AllBooks.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='titnewre1'>" + viewallbook.ToString() + "</a>" +
        //            "</div>";
        string str = "";


        if (dt.Rows.Count > 0)
        {
            int i = 1;
            str = str + "<div id='div_Books'>";
            foreach (DataRow dr in dt.Rows)
            {
                if (i % 3 == 0)
                {
                    str = str + "<div class='box1book boxbolast'>";
                }
                else
                {
                    str = str + "<div class='box1book'>";
                }
                i++;

                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = Config.WebSite + "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                    string newFile = img;
                    if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "");
                    }
                }
                else
                {
                    img = Config.WebSite + "images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (Language == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else if (Language == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0 && Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    //str = str + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "'  alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223'/></a>";
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223'/></a>";
                }
                else
                {

                    //str = str + "<div class='bookimg'>" + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "' alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223' /></a>";
                    str = str + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='" + dr["Title"].ToString().Trim() + "' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                //str = str + "<div class='namkl2'><a href = 'Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                str = str + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                if (Convert.ToBoolean(dr["IseBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFree"]) == true)
                    {
                        str = str + "<div class='namkl' style=\"width: 50%;float: left;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='namkl' style=\"width: 50%;float: left;\">$" + dr["FinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 50%;float: left;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsPaperBook"]))
                {
                    if (Convert.ToBoolean(dr["IsFreePaper"]) == true)
                    {
                        str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">" + isfreetitle.ToString() + "</div>";
                    }
                    else
                    {
                        str = str + "<div class='namkl' style=\"width: 45%;float: left;text-align: right;padding-right: 5px;\">$" + dr["PaperBookFinalPrice"].ToString().Replace(",", ".") + "</div>";
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">&nbsp;</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {

                    str = str + "<a href='Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                "</div>";

                }
                else if (Convert.ToBoolean(dr["IsFree"]) == false)
                {

                    str = str + "<a href='Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }

            }
            str = str + "</div>";
        }
        if (dt.Rows.Count < 9)
        {
            str += "<div id=\"divNoData\" style=\"Color:red;\" onload=\"hideAlert()\">No Record Found</div>";
        }
        if (dt.Rows.Count == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Nodatafound", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
        }
        div_book.InnerHtml = a1 + str;
    }

    [WebMethod]
    public static string BindDatas(int Sortby, string For, string LoadMore)
    {
        BookBAL Obj_Book = new BookBAL();
        Security S = new Security();
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";

        string culture = "en-US";
        if (Language == 2)
        {
            culture = "es-ES";
        }
        else if (Language == 1)
        {
            culture = "en-US";
        }
        else
        {
            culture = "en-US";
        }
        viewallbook = Localization.ResourceManager.GetString("viewallbook", culture);
        newrelease = Localization.ResourceManager.GetString("newrelease", culture);
        //buynowtitle = Localization.ResourceManager.GetString("buynow", culture);
        buynowtitle = Localization.ResourceManager.GetString("addtocart", culture);
        isfreetitle = Localization.ResourceManager.GetString("Free", culture);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", culture);
        DataTable dt = new DataTable();

        Obj_Book.LanguageID = Language;
        //Obj_Book.StartIndex = lastIndex;
        //lastIndex = Obj_Book.EndIndex = lastIndex + 9;
        string a1 = "";
        if (LoadMore != "LoadMore")
        {
            if (For == "SortBy" || For == "New Releases" || For == "ebook" || For == "Paper book")
            {
                if (HttpContext.Current.Request.QueryString["PageNo"] != null && HttpContext.Current.Request.QueryString["PageNo"].ToString() != "")
                {
                    Obj_Book.EndIndex = Convert.ToInt32(HttpContext.Current.Request.QueryString["PageNo"].ToString()) * 9;
                    Obj_Book.StartIndex = (Obj_Book.EndIndex - 9) + 1;
                }
                else
                {
                    HttpContext.Current.Session["num"] = "9";
                    Obj_Book.EndIndex = 9;
                    Obj_Book.StartIndex = 1;
                }
            }
        }
        else if (LoadMore == "LoadMore")
        {
            Obj_Book.StartIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 1;
            Obj_Book.EndIndex = Convert.ToInt32(HttpContext.Current.Session["num"]) + 9;
            HttpContext.Current.Session["num"] = Obj_Book.EndIndex.ToString();
        }

        dt = Obj_Book.get_all_book_website2(Sortby, For);

        string str = "";

        if (dt.Rows.Count > 0)
        {
            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if (i % 3 == 0)
                {
                    str = str + "<div class='box1book boxbolast'>";
                }
                else
                {
                    str = str + "<div class='box1book'>";
                }
                i++;

                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"].ToString().Replace(".jpg", "_1.jpg") + "";
                    string newFile = img;
                    if (!File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + img + "")))
                    {
                        string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "";
                        Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                        GenerateThumbnailStatic(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "");
                    }
                }
                else
                {
                    img = "images/No_Image.jpg";
                }
                var bookDetailUrl = "";
                if (Language == 1)
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else if (Language == 2)
                {
                    bookDetailUrl = Config.WebSiteMain + "es" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                else
                {
                    bookDetailUrl = Config.WebSiteMain + "us" + "/" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"]).ToString(), System.Text.Encoding.UTF8).Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___");
                    bookDetailUrl += "/" + dr["TabType"];
                }
                if (Convert.ToInt32(dr["DiscountPrice"]) == 0 && Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    //str = str + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + culture + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                    str = str + "<a href='" + bookDetailUrl + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    //str = str + "<div class='bookimg'>" + "<a href='Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + culture + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    str = str + "<div class='bookimg'>" + "<a href='" + bookDetailUrl + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                //str = str + "<div class='namkl2'><a href = 'Books-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + culture + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                str = str + "<div class='namkl2'><a href = '" + bookDetailUrl + "' style='color:#616161'>" + dr["Title"] + "</a></div>" + "<div class='author'>" + dr["Autoher"] + "</div>";
                if (For == "ebook" || For == "New Releases")
                {
                    if (Convert.ToBoolean(dr["IseBook"]))
                    {
                        if (Convert.ToBoolean(dr["IsFree"]) == true)
                        {
                            str = str + "<div class='namkl' style=\"width: 50%;float: left;\">" + isfreetitle.ToString() + "</div>";
                        }
                        else
                        {
                            str = str + "<div class='namkl' style=\"width: 50%;float: left;\">$" + dr["FinalPrice"].ToString().Replace(",", ".") + "</div>";
                        }
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 50%;float: left;\">&nbsp;</div>";
                }
                if (For == "Paper book" || For == "New Releases")
                {
                    if (Convert.ToBoolean(dr["IsPaperBook"]))
                    {
                        if (Convert.ToBoolean(dr["IsFreePaper"]) == true)
                        {
                            str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">" + isfreetitle.ToString() + "</div>";
                        }
                        else
                        {
                            str = str + "<div class='namkl' style=\"width: 45%;float: left;text-align: right;padding-right: 5px;\">$" + dr["PaperBookFinalPrice"].ToString().Replace(",", ".") + "</div>";
                        }
                    }
                }
                else
                {
                    str = str + "<div class='namkl' style=\"width: 45%;float: right;text-align: right;padding-right: 5px;\">&nbsp;</div>";
                }
                if (For == "Paper book")
                {
                    str = str + "<a href='Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + culture + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                     "</div>";

                }
                else if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    if (Convert.ToBoolean(dr["IseBook"]))
                    {
                        str = str + "<a href='Index.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + culture + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                           "</div>";
                    }
                }
                else
                {
                    str = str + "<a href='Index.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + culture + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }

            }
            //str = str + "</div>";
        }
        if (dt.Rows.Count < 9)
        {
            str += "<div id=\"divNoData\" onload=\"hideAlert()\"></div>";
        }
        if (dt.Rows.Count == 0)
        {
            string NoData = "<div id=\"divNoData\" style=\"Color:red;\" onload=\"hideAlert()\">No Record Found</div>";
            //+ ResourceManager.GetString("Nodatafound", culture) + "</div>";
            return NoData;
        }
        return str;
    }

}