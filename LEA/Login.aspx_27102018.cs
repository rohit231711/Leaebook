using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

public partial class Login : System.Web.UI.Page
{
    BAL_Account obj_login = new BAL_Account();
    protected void Page_Load(object sender, EventArgs e)
    {
        //string requestText = "<?xml version='1.0' encoding='UTF-8'?><req:ShipmentRequest xmlns:req='http://www.dhl.com' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.dhl.com ship-val-global-req.xsd' schemaVersion='1.0'><Request><ServiceHeader><MessageTime>2015-12-01T09:30:47-05:00</MessageTime><MessageReference>1234567890123456789012345678901</MessageReference><SiteID>xmleditcinco</SiteID><Password>I64XtmJUzF</Password></ServiceHeader></Request><RegionCode>AM</RegionCode><RequestedPickupTime>Y</RequestedPickupTime><NewShipper>Y</NewShipper><LanguageCode>en</LanguageCode><PiecesEnabled>Y</PiecesEnabled><Billing><ShipperAccountNumber>753871175</ShipperAccountNumber><ShippingPaymentType>S</ShippingPaymentType><BillingAccountNumber>753871175</BillingAccountNumber><DutyPaymentType>S</DutyPaymentType><DutyAccountNumber>753871175</DutyAccountNumber></Billing><Consignee><CompanyName>IBM Singapore Pte Ltd</CompanyName><AddressLine>9 Changi Business Park Central 1</AddressLine><AddressLine>3th Floor</AddressLine><AddressLine>The IBM Place</AddressLine><City>Singapore</City><PostalCode>486048</PostalCode><CountryCode>SG</CountryCode><CountryName>Singapore</CountryName><Contact><PersonName>Mrs Orlander</PersonName><PhoneNumber>506-851-2271</PhoneNumber><PhoneExtension>7862</PhoneExtension><FaxNumber>506-851-7403</FaxNumber><Telex>506-851-7121</Telex><Email>anc@email.com</Email></Contact></Consignee><Commodity><CommodityCode>cc</CommodityCode><CommodityName>cn</CommodityName></Commodity><Dutiable><DeclaredValue>200.00</DeclaredValue><DeclaredCurrency>USD</DeclaredCurrency><ScheduleB>3002905110</ScheduleB><ExportLicense>D123456</ExportLicense><ShipperEIN>112233445566</ShipperEIN><ShipperIDType>S</ShipperIDType><ImportLicense>ImportLic</ImportLicense><ConsigneeEIN>ConEIN2123</ConsigneeEIN><TermsOfTrade>DAP</TermsOfTrade></Dutiable><Reference><ReferenceID>AM international shipment</ReferenceID><ReferenceType>St</ReferenceType></Reference><ShipmentDetails><NumberOfPieces>1</NumberOfPieces><Pieces><Piece><PieceID>1</PieceID><PackageType>EE</PackageType><Weight>10.0</Weight><DimWeight>1200.0</DimWeight><Width>100</Width><Height>200</Height><Depth>300</Depth></Piece></Pieces><Weight>10.0</Weight><WeightUnit>L</WeightUnit><GlobalProductCode>P</GlobalProductCode><LocalProductCode>P</LocalProductCode><Date>" + DateTime.Now.ToString("yyyy-MM-dd") + "</Date><Contents>AM international shipment contents</Contents><DoorTo>DD</DoorTo><DimensionUnit>I</DimensionUnit><InsuredAmount>1200.00</InsuredAmount><PackageType>EE</PackageType><IsDutiable>Y</IsDutiable><CurrencyCode>USD</CurrencyCode></ShipmentDetails><Shipper><ShipperID>751008818</ShipperID><CompanyName>IBM Corporation</CompanyName><RegisteredAccount>751008818</RegisteredAccount><AddressLine>1 New Orchard Road</AddressLine><AddressLine>Armonk</AddressLine><City>New York</City><Division>ny</Division><DivisionCode>ny</DivisionCode><PostalCode>10504</PostalCode><CountryCode>US</CountryCode><CountryName>United States Of America</CountryName><Contact><PersonName>Mr peter</PersonName><PhoneNumber>1 905 8613402</PhoneNumber><PhoneExtension>3403</PhoneExtension><FaxNumber>1 905 8613411</FaxNumber><Telex>1245</Telex><Email>test@email.com</Email></Contact></Shipper><SpecialService><SpecialServiceType>I</SpecialServiceType></SpecialService><EProcShip>N</EProcShip><LabelImageFormat>PDF</LabelImageFormat></req:ShipmentRequest>";
        
        //Tracking XML
        //string requestText = @"<?xml version='1.0' encoding='UTF-8'?><req:KnownTrackingRequest xmlns:req='http://www.dhl.com' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.dhl.com	TrackingRequestKnown.xsd'><Request>	<ServiceHeader>	<MessageTime>2016-03-30T11:28:56-08:00</MessageTime><MessageReference>1234567890123456789012345678</MessageReference><SiteID>xmleditcinco</SiteID>			<Password>I64XtmJUzF</Password>		</ServiceHeader>	</Request>	<LanguageCode>SV</LanguageCode>	<AWBNumber>3879063370</AWBNumber>	<LevelOfDetails>ALL_CHECK_POINTS</LevelOfDetails></req:KnownTrackingRequest>";
        //string requestText = @"<?xml version='1.0' encoding='UTF-8'?><req:KnownTrackingRequest xmlns:req='http://www.dhl.com' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.dhl.com	TrackingRequestKnown.xsd'><Request><ServiceHeader><MessageTime>2002-06-25T11:28:56-08:00</MessageTime><MessageReference>1234567890123456789012345678</MessageReference><SiteID>xmleditcinco</SiteID><Password>I64XtmJUzF</Password></ServiceHeader></Request><LanguageCode>SV</LanguageCode><AWBNumber>3879127232</AWBNumber><LevelOfDetails>ALL_CHECK_POINTS</LevelOfDetails></req:KnownTrackingRequest>";
        //string requestText = @"<?xml version='1.0' encoding='UTF-8'?><req:KnownTrackingRequest xmlns:req='http://www.dhl.com' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.dhl.com  TrackingRequestKnown.xsd'><Request><ServiceHeader><MessageTime>2002-06-25T11:28:56-08:00</MessageTime><MessageReference>1234567890123456789012345678</MessageReference><SiteID>xmleditcinco</SiteID><Password>I64XtmJUzF</Password></ServiceHeader></Request><LanguageCode>en</LanguageCode><AWBNumber>8564385550</AWBNumber><LevelOfDetails>ALL_CHECK_POINTS</LevelOfDetails><PiecesEnabled>S</PiecesEnabled> </req:KnownTrackingRequest>";

        //GetQuote XML
        //string requestText = @"<?xml version='1.0' encoding='UTF-8'?><p:DCTRequest xmlns:p='http://www.dhl.com' xmlns:p1='http://www.dhl.com/datatypes' xmlns:p2='http://www.dhl.com/DCTRequestdatatypes' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.dhl.com DCT-req.xsd '><GetQuote><Request><ServiceHeader><MessageTime>2002-08-20T11:28:56.000-08:00</MessageTime><MessageReference>1234567890123456789012345678901</MessageReference><SiteID>xmleditcinco</SiteID><Password>I64XtmJUzF</Password></ServiceHeader></Request><From><CountryCode>SG</CountryCode><Postalcode>100000</Postalcode></From><BkgDetails><PaymentCountryCode>SG</PaymentCountryCode><Date>2016-07-06</Date><ReadyTime>PT10H21M</ReadyTime><ReadyTimeGMTOffset>+01:00</ReadyTimeGMTOffset><DimensionUnit>CM</DimensionUnit><WeightUnit>KG</WeightUnit><Pieces><Piece><PieceID>1</PieceID><Height>1</Height><Depth>1</Depth><Width>1</Width><Weight>5.0</Weight></Piece></Pieces> <PaymentAccountNumber>CASHSIN</PaymentAccountNumber>	  <IsDutiable>Y</IsDutiable><NetworkTypeCode>AL</NetworkTypeCode><QtdShp><GlobalProductCode>D</GlobalProductCode><LocalProductCode>D</LocalProductCode>		<QtdShpExChrg><SpecialServiceType>AA</SpecialServiceType></QtdShpExChrg></QtdShp></BkgDetails><To><CountryCode>US</CountryCode><Postalcode>20505</Postalcode></To><Dutiable><DeclaredCurrency>EUR</DeclaredCurrency><DeclaredValue>1.0</DeclaredValue></Dutiable></GetQuote></p:DCTRequest>";
        string requestText = @"<?xml version='1.0' encoding='UTF-8'?><p:DCTRequest xmlns:p='http://www.dhl.com' xmlns:p1='http://www.dhl.com/datatypes' xmlns:p2='http://www.dhl.com/DCTRequestdatatypes' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.dhl.com DCT-req.xsd '><GetQuote><Request><ServiceHeader><MessageTime>2002-08-20T11:28:56.000-08:00</MessageTime><MessageReference>1234567890123456789012345678901</MessageReference><SiteID>xmleditcinco</SiteID><Password>I64XtmJUzF</Password></ServiceHeader></Request><From><CountryCode>US</CountryCode><Postalcode>10504</Postalcode></From><BkgDetails><PaymentCountryCode>SG</PaymentCountryCode><Date>2016-07-07</Date><ReadyTime>PT10H21M</ReadyTime><ReadyTimeGMTOffset>+01:00</ReadyTimeGMTOffset><DimensionUnit>CM</DimensionUnit><WeightUnit>KG</WeightUnit><Pieces><Piece><PieceID>1</PieceID><Height>1</Height><Depth>1</Depth><Width>1</Width><Weight>5.0</Weight></Piece></Pieces> <PaymentAccountNumber>CASHSIN</PaymentAccountNumber>	  <IsDutiable>Y</IsDutiable><NetworkTypeCode>AL</NetworkTypeCode><QtdShp><GlobalProductCode>P</GlobalProductCode><LocalProductCode>P</LocalProductCode><QtdShpExChrg><SpecialServiceType>II</SpecialServiceType></QtdShpExChrg></QtdShp></BkgDetails><To><CountryCode>SG</CountryCode><Postalcode>486048</Postalcode></To><Dutiable><DeclaredCurrency>EUR</DeclaredCurrency><DeclaredValue>1.0</DeclaredValue></Dutiable></GetQuote></p:DCTRequest>";

        WebRequest requestRate = HttpWebRequest.Create("http://xmlpi-ea.dhl.com/XMLShippingServlet");
        requestRate.ContentType = "application/x-www-form-urlencoded";
        requestRate.Method = "POST";

        using (var stream = requestRate.GetRequestStream())
        {
            var arrBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(requestText);
            stream.Write(arrBytes, 0, arrBytes.Length);
            stream.Close();
        }

        WebResponse responseRate = requestRate.GetResponse();
        var respStream = responseRate.GetResponseStream();
        var reader = new StreamReader(respStream, System.Text.Encoding.ASCII);
        string strResponse = reader.ReadToEnd();
        respStream.Close();

        txt_email.Focus();
        this.Form.DefaultButton = btn_login.UniqueID;

        if (Request.QueryString["logout"] != null && Request.QueryString["logout"].ToString() != "")
        {
            if (Request.QueryString["logout"].ToString() == "1")
            {
                Session.RemoveAll();
            }
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["rid"] != null && Request.QueryString["rid"].ToString() == "1")
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Tu mensaje ha sido enviado con éxito.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Your message has been sent successfully.');", true);
                }
            }
        }
        if (Session["UserName"] != null && Session["UserName"] != null)
        {
            Response.Redirect(Config.WebSiteMain + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "");
        }
    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        obj_login.UserName = txt_email.Text.ToString().Trim();
        obj_login.Password = txt_password.Text.ToString().Trim();
        DataTable dt = new DataTable();
        dt = obj_login.Check_Login();
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["IsActive"]) == false)
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Tu cuenta no está activada todavía así que por favor revise su correo electrónico para activar la cuenta.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert(' Your account is not activated yet so please check your email for activate the account.');", true);
                }
            }
            else
            {
                Session["UserName"] = dt.Rows[0]["FirstName"].ToString();
                Session["UserID"] = dt.Rows[0]["RegistrationID"].ToString();
                if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                {
                    string BookId = Session["AddToCart"].ToString();
                    string[] str = BookId.Split(',');
                    BookOrderBAL ObjBookOrders = new BookOrderBAL();
                    BookBAL Obj_Book = new BookBAL();
                    foreach (string BookID in str)
                    {
                        int result = 0;
                        int count = 0;
                        ObjBookOrders.CustomerID = Convert.ToInt32(Session["UserID"]);
                        ObjBookOrders.BookID = Convert.ToInt32(BookID);
                        ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);

                        DataTable dtBookDetail = new DataTable();
                        Obj_Book.BookID = Convert.ToInt32(BookID);
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

                        DataTable DT = ObjBookOrders.GetCartList();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            ObjBookOrders.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                {
                                    count++;
                                }

                            }
                            if (count > 0)
                            {
                                //Message1("You already have this book in your cart");

                            }
                            else
                            {
                                var cnt1 = 0;
                                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                                objPurchase.UserID = Convert.ToInt64(Session["UserID"].ToString());

                                DataTable Dt = objPurchase.getUserLibrary();
                                if (Dt != null && Dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < Dt.Rows.Count; i++)
                                    {
                                        var tableBook = Obj_Book.getBookDetails();
                                        var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                                        var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                                        if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                        {
                                            if (iseBook == Convert.ToBoolean(Dt.Rows[i]["IseBook"]))// && isPaperBook == Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                                cnt1++;
                                        }

                                    }
                                }

                                if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                                {
                                    if (cnt1 == 0)
                                    {
                                        ObjBookOrders.IseBook = true;
                                        ObjBookOrders.IspaperBook = false;
                                        ObjBookOrders.Quantity = 0;
                                        result = ObjBookOrders.InsertCustomerCart1();
                                    }
                                    ObjBookOrders.IseBook = false;
                                    ObjBookOrders.IspaperBook = true;
                                    ObjBookOrders.Quantity = 1;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && cnt1 == 0)
                                {
                                    ObjBookOrders.IseBook = true;
                                    ObjBookOrders.IspaperBook = false;
                                    ObjBookOrders.Quantity = 0;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                                {
                                    ObjBookOrders.IseBook = false;
                                    ObjBookOrders.IspaperBook = true;
                                    ObjBookOrders.Quantity = 1;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                //result = ObjBookOrders.InsertCustomerCart1();
                            }
                        }
                        else
                        {
                            var cnt1 = 0;
                            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                            string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                            objPurchase.UserID = Convert.ToInt64(Session["UserID"].ToString());
                            ObjBookOrders.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");

                            DataTable Dt = objPurchase.getUserLibrary();
                            if (Dt != null && Dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < Dt.Rows.Count; i++)
                                {
                                    var tableBook = Obj_Book.getBookDetails();
                                    var iseBook = Convert.ToBoolean(tableBook.Rows[0]["IseBook"]);
                                    var isPaperBook = Convert.ToBoolean(tableBook.Rows[0]["IsPaperBook"]);
                                    if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                    {
                                        if (iseBook == Convert.ToBoolean(Dt.Rows[i]["IseBook"]))// && isPaperBook == Convert.ToBoolean(Dt.Rows[i]["IsPaperBook"]))
                                            cnt1++;
                                    }

                                }
                            }

                            if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                if (cnt1 == 0)
                                {
                                    ObjBookOrders.IseBook = true;
                                    ObjBookOrders.IspaperBook = false;
                                    ObjBookOrders.Quantity = 0;
                                    result = ObjBookOrders.InsertCustomerCart1();
                                }
                                ObjBookOrders.IseBook = false;
                                ObjBookOrders.IspaperBook = true;
                                ObjBookOrders.Quantity = 1;
                                result = ObjBookOrders.InsertCustomerCart1();
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IseBook"].ToString()) && cnt1 == 0)
                            {
                                ObjBookOrders.IseBook = true;
                                ObjBookOrders.IspaperBook = false;
                                ObjBookOrders.Quantity = 0;
                                result = ObjBookOrders.InsertCustomerCart1();
                            }
                            else if (Convert.ToBoolean(dtBookDetail.Rows[0]["IsPaperBook"].ToString()))
                            {
                                ObjBookOrders.IseBook = false;
                                ObjBookOrders.IspaperBook = true;
                                ObjBookOrders.Quantity = 1;
                                result = ObjBookOrders.InsertCustomerCart1();
                            }
                            //result = ObjBookOrders.InsertCustomerCart1();
                        }
                    }
                }


                if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
                {
                    string BookId = Session["AddToWishlist"].ToString();
                    string[] str = BookId.Split(',');
                    BookOrderBAL ObjBookOrders = new BookOrderBAL();
                    foreach (string BookID in str)
                    {
                        int result = 0;
                        int count = 0;
                        ObjBookOrders.CustomerID = Convert.ToInt32(Session["UserID"]);
                        ObjBookOrders.BookID = Convert.ToInt32(BookID);
                        ObjBookOrders.OrderDate = Convert.ToDateTime(System.DateTime.Now);

                        DataTable DT = ObjBookOrders.GetWishList();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToInt32(DT.Rows[i]["BookID"]) == ObjBookOrders.BookID)
                                {
                                    count++;
                                }

                            }
                            if (count > 0)
                            {
                                //Message1("You already have this book in your WishList");
                            }
                            else
                            {
                                result = ObjBookOrders.InsertCustomerWishList1();
                            }
                        }
                        else
                        {
                            result = ObjBookOrders.InsertCustomerWishList1();
                        }
                    }
                }
                if (Session["RedirectUrl"] != null)
                {
                    Response.Redirect(Session["RedirectUrl"].ToString());
                }
                else
                {
                    //Response.Redirect("Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"] + "");
                    Response.Redirect(Config.WebSiteMain + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "");
                }                    
            }
        }
        else
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Dirección de correo electrónico o la contraseña son incorrectos.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Email Address or password you entered is incorrect.');", true);
            }
            txt_email.Text = null;
            txt_password.Text = null;
            txt_email.Focus();
        }
    }
}