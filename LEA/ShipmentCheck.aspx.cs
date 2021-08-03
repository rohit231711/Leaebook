using BAL;
using Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class ShipmentCheck : System.Web.UI.Page
{
    BookBAL ObjBook = new BookBAL();
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookDeliveryAddressBAL objBookAddDetail = new BookDeliveryAddressBAL();
    BookShippingBAL objShipping = new BookShippingBAL();
    Security S = new Security();
    double Amount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0 && Request.QueryString["UserID"] != "" && Request.QueryString["UserID"] != "0")
        {
            try
            {
                //ObjBookOrders.CustomerID = Convert.ToInt32(Request.QueryString["UserID"]);
                ObjBookOrders.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["UserID"])).ToString());
                ObjBookOrders.LanguageID = Convert.ToInt32(1);

                Session["CurrentCulture"] = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                DataTable dt = ObjBookOrders.GetCartList();

                double Amount = 0;
                int Qty = 0;
                string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"]);
                    Amount += Convert.ToDouble(dt.Rows[i]["FinalCartPrice1"]);
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
                }
                Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());
            }
            catch(Exception ex)
            {

            }
            
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                try
                {
                    int s = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                }
                catch
                {
                    string a = Request.Url.AbsoluteUri.ToString();
                    string old = Request.QueryString["id"].ToString();
                    Response.Redirect(Request.Url.AbsoluteUri.ToString().Replace(old, S.Encrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString()));
                }
            }
            BindCountry();
            BindData();
        }
    }
    
    private void BindCountry()
    {
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllCountry();

        if (dt.Rows.Count > 0)
        {
            ddlCountry.DataSource = dt;
            ddlCountry.DataTextField = "countryname";
            ddlCountry.DataValueField = "countryname";
            ddlCountry.DataBind();
        }

    }

    protected void BindData()
    {
        DataTable dt = new DataTable();
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            objBookAddDetail.UserID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
            ObjBookOrders.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
            ObjBookOrders.LanguageID = 1;

            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
            {
                if (Request.QueryString["l"].ToString() == "es-ES")
                {
                    ObjBookOrders.LanguageID = 2;
                }
                else if (Request.QueryString["l"].ToString() == "en-US")
                {
                    ObjBookOrders.LanguageID = 1;
                }
            }
            dt = ObjBookOrders.GetCartList();
        }
        else
        {
            BookBAL book = new BookBAL();
            if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
            {
                book.BookId = Session["AddToCart"].ToString();
                book.LangaugeID = 1;
                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
                {
                    if (Request.QueryString["l"].ToString() == "es-ES")
                    {
                        book.LanguageID = 2;
                    }
                    else if (Request.QueryString["l"].ToString() == "en-US")
                    {
                        book.LanguageID = 1;
                    }
                }
                dt = book.getBookDetails_AddToCart();
            }
        }

        if (dt != null && dt.Rows.Count > 0)
        {
            int Qty = 0;
            string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["IsFree"]) != true)
                {
                    //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"]); 
                    Amount += Convert.ToDouble(dt.Rows[i]["FinalCartPrice1"]);
                    if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                        Qty++;
                    if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                        Qty += Convert.ToInt32(dt.Rows[i]["Qauntity"]);
                }
            }
            lblAmount.Text = Amount.ToString();
            lblitems.Text = Qty + " item(s)";
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());

        }
        DataTable dtAdd = new DataTable();
        if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"].ToString() != "")
        {
            //objBookAddDetail.UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            objBookAddDetail.UserID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["UserID"])).ToString());
        }
        if(dt!=null && dt.Rows.Count>0)
        {
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            Repeater1.Visible = true;
        }
        else
        {
            //GeneratePages(0);
            Div1.Visible = false;
            Repeater1.Visible = false;
            Label1.Visible = true;
            lblAmount.Text = "0.00";
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                Label1.Text = "No se encontraron datos";
            }
            else
            {
                Label1.Text = "No data found";
            }
            //tot.Visible = false;

        }
        dtAdd = objBookAddDetail.GetBookAddressByUser();
        if (dtAdd != null && dtAdd.Rows.Count > 0)
        {
            rptRecords1.DataSource = dtAdd;
            rptRecords1.DataBind();
            rptRecords1.Visible = true;
        }
        else
        {
            chkout.Visible = false;
            rptRecords1.Visible = false;
            lblDefaultMessage.Visible = true;
            lblAmount.Text = "0.00";
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                lblDefaultMessage.Text = "No se encontró la dirección";
            }
            else
            {
                lblDefaultMessage.Text = "No address found";
            }
        }
    }

    protected void del(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            var id = e.CommandArgument;
            objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(id.ToString());
            objBookAddDetail.DeleteUserAddressDetail();
            Response.Redirect(Request.RawUrl);
        }
        RepeaterItem item = e.Item;

        Label lblName = (Label)item.FindControl("lblName");
        Label lblStreetAddress = (Label)item.FindControl("lblStreetAddress");
        Label lblLandmark = (Label)item.FindControl("lblLandmark");
        Label lblCity = (Label)item.FindControl("lblCity");
        Label lblState = (Label)item.FindControl("lblState");
        Label lblCountry = (Label)item.FindControl("lblCountry");
        Label lblCityCode = (Label)item.FindControl("lblCityCode");
        Label lblPhoneNumber = (Label)item.FindControl("lblPhoneNumber");

        TextBox txtName = (TextBox)item.FindControl("txtName");
        TextBox txtStreetAddress = (TextBox)item.FindControl("txtStreetAddress");
        TextBox txtLandmark = (TextBox)item.FindControl("txtLandmark");
        TextBox txtCity = (TextBox)item.FindControl("txtCity");
        TextBox txtState = (TextBox)item.FindControl("txtState");
        DropDownList ddlCountryRpt = (DropDownList)item.FindControl("ddlCountryRpt");
        TextBox txtPincode = (TextBox)item.FindControl("txtPincode");
        TextBox txtPhoneNumber = (TextBox)item.FindControl("txtPhoneNumber");

        LinkButton lnkUpdate = (LinkButton)item.FindControl("lnkUpdate");
        LinkButton lnkCancel = (LinkButton)item.FindControl("lnkCancel");
        LinkButton lnkEdit = (LinkButton)item.FindControl("LinkButton2");
        LinkButton lnkDelete = (LinkButton)item.FindControl("LinkButton1");
        
        if (e.CommandName == "edit")
        {
            lblName.Visible = false;
            lblStreetAddress.Visible = false;
            lblLandmark.Visible = false;
            lblCity.Visible = false;
            lblState.Visible = false;
            lblCountry.Visible = false;
            lblCityCode.Visible = false;
            lblPhoneNumber.Visible = false;

            txtName.Visible = true;
            txtStreetAddress.Visible = true;
            txtLandmark.Visible = true;
            txtCity.Visible = true;
            txtState.Visible = true;
            ddlCountryRpt.Visible = true;
            txtPincode.Visible = true;
            txtPhoneNumber.Visible = true;

            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
            lnkUpdate.Visible = true;
            lnkCancel.Visible = true;

            Country objCountry = new Country();
            DataTable dt = objCountry.SelectAllCountry();

            if (dt.Rows.Count > 0)
            {
                ddlCountryRpt.DataSource = dt;
                ddlCountryRpt.DataTextField = "countryname";
                ddlCountryRpt.DataValueField = "countryname";
                ddlCountryRpt.DataBind();
            }
            ddlCountryRpt.SelectedValue = lblCountry.Text;

        }
        if (e.CommandName == "update")
        {
            try
            {
                var id = e.CommandArgument;
                objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(id.ToString());
                objBookAddDetail.IsDefault = false;
                objBookAddDetail.UserID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["UserID"])).ToString());
                objBookAddDetail.Name = txtName.Text;
                objBookAddDetail.StreetAddress = txtStreetAddress.Text;
                objBookAddDetail.Landmark = txtLandmark.Text;
                objBookAddDetail.City = txtCity.Text;
                objBookAddDetail.State = txtState.Text;
                objBookAddDetail.Country = ddlCountryRpt.SelectedItem.Text;
                objBookAddDetail.Pincode = txtPincode.Text;
                objBookAddDetail.PhoneNumber = txtPhoneNumber.Text;

                objBookAddDetail.InsertBookAddress();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            BindData();
        }
        if (e.CommandName == "cancel")
        {
            BindData();
        }
    }

    protected void delCart(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                var lnk = e.CommandArgument.ToString().Split('@');
                ObjBookOrders.IseBook = Convert.ToBoolean(lnk[1]);
                ObjBookOrders.IspaperBook = Convert.ToBoolean(lnk[2]);
                int CommandArgument = Convert.ToInt32(lnk[0]);
                ObjBookOrders.BookID = CommandArgument;
                ObjBookOrders.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                ObjBookOrders.DeletefromCustomerCart();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                string s = Session["AddToCart"].ToString();
                //int CommandArgument = Convert.ToInt32(e.CommandArgument);
                var lnk = e.CommandArgument.ToString().Split('@');
                ObjBookOrders.IseBook = Convert.ToBoolean(lnk[1]);
                ObjBookOrders.IspaperBook = Convert.ToBoolean(lnk[2]);
                int CommandArgument = Convert.ToInt32(lnk[0]);
                string cmd = CommandArgument.ToString();
                s = s.Replace(cmd, "");
                Session["AddToCart"] = s;
                //BindData();
                Response.Redirect(Request.RawUrl);
            }

        }

        else if (e.CommandName == "move")
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                //int CommandArgument = Convert.ToInt32(e.CommandArgument);
                var lnk = e.CommandArgument.ToString().Split('@');
                int CommandArgument = Convert.ToInt32(lnk[0]);
                ObjBookOrders.BookID = CommandArgument;
                ObjBookOrders.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                ObjBookOrders.MoveItemfromUserCartToWishlist();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                string s = Session["AddToCart"].ToString();
                //int CommandArgument = Convert.ToInt32(e.CommandArgument);
                var lnk = e.CommandArgument.ToString().Split('@');
                int CommandArgument = Convert.ToInt32(lnk[0]);
                string cmd = CommandArgument.ToString();
                if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
                {
                    Session["AddToWishlist"] = Session["AddToWishlist"] + "," + cmd;
                }
                else
                {
                    Session["AddToWishlist"] = cmd;
                }
                s = s.Replace(cmd, "");
                Session["AddToCart"] = s;

                Response.Redirect("WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        objBookAddDetail = new BookDeliveryAddressBAL();
        objBookAddDetail.BookDeliveryAddID = -1;
        //objBookAddDetail.UserID = Convert.ToInt32(Request.QueryString["UserID"]);
        objBookAddDetail.UserID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["UserID"])).ToString());
        objBookAddDetail.IsDefault = false;
        objBookAddDetail.Name = txtname.Text;
        objBookAddDetail.StreetAddress = txtaddress.Text;
        objBookAddDetail.Landmark = txtlandmark.Text;
        objBookAddDetail.City = txtcity.Text;
        objBookAddDetail.State = txtstate.Text;
        //objBookAddDetail.Country = txtcountry.Text;
        objBookAddDetail.Country = ddlCountry.SelectedItem.Text;
        objBookAddDetail.Pincode = txtpincode.Text;
        objBookAddDetail.PhoneNumber = txtPhone.Text;

        var id = objBookAddDetail.InsertBookAddress();
        Response.Redirect(Request.RawUrl);
    }

    protected void lnkDeliver_Click(object sender, EventArgs e)
    {
        //var orderNo = "";
        //if(Repeater1.Items.Count>0)
        //{
        //    var lblOrder = (Label)Repeater1.Items[0].FindControl("lblOrderNo");
        //    orderNo = lblOrder.Text;
        //    orderNo = S.Encrypt(orderNo);
        //}
        Shipment s = new Shipment();
        LinkButton btn = (LinkButton)(sender);
        Session["addressDetail"] = btn.CommandArgument;
        Session["website"] = "true";

        BookDeliveryAddressBAL objBookAdd = new BookDeliveryAddressBAL();
        objBookAdd.BookDeliveryAddID = Convert.ToInt32(btn.CommandArgument);
        var dt = objBookAdd.GetDataByPK();
        var billnumber = "00";
        if (dt.Rows.Count > 0)
        {
            StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/Shipment1.xml"));
            string text = streamReader.ReadToEnd();

            var piceses = "<Piece><PieceID>1</PieceID><PackageType>EE</PackageType><Weight>10.0</Weight><DimWeight>1200.0</DimWeight>"
                         + "<Width>100</Width><Height>200</Height><Depth>300</Depth></Piece>";

            string xmlRequest = s.replaceXml(text, "1", piceses, "10", "100");
            string response = s.sendRequest(xmlRequest);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);

            var ProfilePicture = xmlDoc.DocumentElement.SelectSingleNode("LabelImage/OutputImage").InnerText;
            billnumber = xmlDoc.DocumentElement.SelectSingleNode("AirwayBillNumber").InnerText;
            if (!string.IsNullOrEmpty(ProfilePicture))
            {
                string name = Convert.ToString(DateTime.Now.Ticks);
                Base64ToImage(ProfilePicture, Server.MapPath("ShippingFiles/") + name + ".pdf");
            }
        }
        Response.Redirect(Request.RawUrl + "&bill=" + billnumber);
    }

    public void Base64ToImage(string base64String,string filename)
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

    public string PicturePath(string sFilename)
    {
        if (!File.Exists(Server.MapPath("~") + sFilename))
        {
            sFilename = @"../images/No_Image.jpg";
        }
        return sFilename;
    }

    protected void txtQuanitity_TextChanged(object sender, EventArgs e)
    {
        TextBox tb1 = ((TextBox)(sender));
        RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));

        TextBox txtQuanitity = (TextBox)rp1.FindControl("txtQuanitity");
        Label lblPrice = (Label)rp1.FindControl("lblPrice");
        var lblOriginalPrice = "0.00";
        HiddenField hfOrder = (HiddenField)rp1.FindControl("hdCartID");
        HiddenField hdBookID = (HiddenField)rp1.FindControl("hdBookID");
        ObjBook.BookID = Convert.ToInt64(hdBookID.Value);
        var dt = ObjBook.getBookDetails();
        lblOriginalPrice = dt.Rows[0]["PaperBookFinalPrice"].ToString();
        
        lblPrice.Text = Convert.ToString(Convert.ToDouble(lblOriginalPrice) * Convert.ToDouble(txtQuanitity.Text));
        string query = "UPDATE tbl_CustomerCart SET Qauntity=" + txtQuanitity.Text.Trim() + ",Amount=" + lblPrice.Text.Trim() + " WHERE OrderID = " + hfOrder.Value.Trim();
        DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, query);
        BindData();
    }

    protected void rptRecords1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            double Amount1 = 0;
            if (Repeater1.Items.Count > 0)
            {
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    Label lblCountry = (Label)item.FindControl("lblCountry");
                    HiddenField hfAddID = (HiddenField)item.FindControl("hfAddID");
                    HiddenField hdPaper = (HiddenField)Repeater1.Items[i].FindControl("hdPaper");
                    HiddenField hdBookID = (HiddenField)Repeater1.Items[i].FindControl("hdBookID");
                    if (Convert.ToBoolean(hdPaper.Value))
                        Amount1 += GetShippingCharge(hdBookID.Value, hfAddID.Value);
                }
            }
            Label lblShipping = (Label)item.FindControl("lblShipping");
            lblShipping.Text = Amount1.ToString();
        }
    }

    private double GetShippingCharge(string bookid,string address)
    {
        DataTable dt = new DataTable();
        objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(address);
        dt = objBookAddDetail.GetDataByPK();

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

    public string getRemoveString()
    {
        //OnClientClick="return confirm('" + '<%# ResourceManager.GetString("Are you sure you want to Remove?",System.Threading.Thread.CurrentThread.CurrentCulture.Name) %>' + "'"); ";
        return "return confirm('" + Localization.ResourceManager.GetString("Are you sure you want to Remove?", System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');";
        //return "";
    }

    public string getWishString()
    {
        //OnClientClick='<%# "return confirm('" + ResourceManager.GetString("Are you sure you want to move to wishlist?",System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');"%>'
        return "return confirm('" + Localization.ResourceManager.GetString("Are you sure you want to move to wishlist?", System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');";
        //return "";
    }

    public string getKeyFromBook(bool paper, bool ebook)
    {
        if (paper && ebook)
        {
            return "bothBook";
        }
        else if (paper)
        {
            return "paperBook";
        }
        else if (ebook)
        {
            return "eBook";
        }
        return "";
    }

    public int getMaxQuantity(string bId)
    {
        ObjBook = new BookBAL();
        ObjBook.BookID = Convert.ToInt32(bId);
        var Dt = ObjBook.SelectBookByID();
        if (Dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Dt.Rows[0]["Quantity"].ToString()))
                return Convert.ToInt32(Dt.Rows[0]["Quantity"].ToString());
            //return 1;
            else
                return 0;
        }
        return 1;
    }

    public int getMinQuantity(string bId)
    {
        ObjBook = new BookBAL();
        ObjBook.BookID = Convert.ToInt32(bId);
        var Dt = ObjBook.SelectBookByID();
        if (Dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Dt.Rows[0]["Quantity"].ToString()))
                return 1;
            else
                return 0;
        }
        return 1;
    }

    public int getQuantity(string bId,string quantity)
    {
        ObjBook = new BookBAL();
        ObjBook.BookID = Convert.ToInt32(bId);
        var Dt = ObjBook.SelectBookByID();
        if (Dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Dt.Rows[0]["Quantity"].ToString()))
            {
                if (Convert.ToInt32(Dt.Rows[0]["Quantity"].ToString()) == 0)
                    return 0;
                else if (Convert.ToInt32(quantity) > Convert.ToInt32(Dt.Rows[0]["Quantity"].ToString()))
                    return Convert.ToInt32(Dt.Rows[0]["Quantity"].ToString());
                else
                    return Convert.ToInt32(quantity);
                // quantity;
            }
            //return 1;
            else
                return 0;
        }
        return 1;
    }

}