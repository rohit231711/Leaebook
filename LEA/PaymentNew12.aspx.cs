using BAL;
using Localization;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Web.Services;
using System.Web.UI.HtmlControls;

public partial class PaymentNew : Page
{
    BookBAL ObjBook = new BookBAL();
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookDeliveryAddressBAL objBookAddDetail = new BookDeliveryAddressBAL();
    BookShippingBAL objShipping = new BookShippingBAL();
    Security S = new Security();
    dbconnection d = new dbconnection();

    static decimal bweight = Convert.ToDecimal(0);
    decimal Amount;
    static decimal dhlamt = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["errorPayment"] != null)
        {
            Label1.Visible = true;
            Label1.Text = "Please add delivery address.";
            Label1.Attributes.Add("style", "color: red;font-family: 'abeezeeregular';font-size: 15px;");
            Session["errorPayment"] = null;
        }
        if (Session["condition"] != null)
        {
            Label1.Visible = true;
            Label1.Text = Session["condition"].ToString();
            Label1.Attributes.Add("style", "color: red;font-family: 'abeezeeregular';font-size: 15px;");
            Session["condition"] = null;
        }
        if (Session["UserName"] == null)// && Session["UserName"].ToString() == "")
        {
            Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            //Response.Redirect("Login.aspx");
        }
        if (Request.QueryString.Count > 0 && Request.QueryString["UserID"] != "" && Request.QueryString["UserID"] != "0")
        {
            try
            {
                //ObjBookOrders.CustomerID = Convert.ToInt32(Request.QueryString["UserID"]);
                ObjBookOrders.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["UserID"])).ToString());
                ObjBookOrders.LanguageID = Convert.ToInt32(1);

                Session["CurrentCulture"] = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                DataTable dt = ObjBookOrders.GetCartList();

                decimal Amount = 0;
                int Qty = 0;
                string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"]);
                    Amount += Decimal.Round(Convert.ToDecimal(dt.Rows[i]["FinalCartPrice1"]), 2);
                    Qty = Qty + 1;
                }
                lblAmount.Text = Amount.ToString();
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
            catch (Exception ex)
            {

            }

        }

        if (!IsPostBack)
        {
            chknational.Checked = true;
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

            // BindCountry();
            BindRegion();

            BindData();

            BindOrderData();
        }
    }

    private void BindCountry()
    {
        Country objCountry = new Country();
        DataTable dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + ddlregion.SelectedValue + "')");

        if (dt.Rows.Count > 0)
        {
            ddlCountry.DataSource = dt;
            ddlCountry.DataTextField = "countryname";
            ddlCountry.DataValueField = "countryname";
            ddlCountry.DataBind();
        }
        ddlCountry.Items.Insert(0, "Select Country");
    }

    private void BindCountryrpt(int id)
    {
        Country objCountry = new Country();
        DataTable dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + id + "')");

        if (dt.Rows.Count > 0)
        {
            ddlCountry.DataSource = dt;
            ddlCountry.DataTextField = "countryname";
            ddlCountry.DataValueField = "countryname";
            ddlCountry.DataBind();
        }
    }

    private void BindRegion()
    {
        Country objCountry = new Country();
        DataTable dt = d.filltable("regionlist");

        if (dt.Rows.Count > 0)
        {
            ddlregion.DataSource = dt;
            ddlregion.DataTextField = "Region";
            ddlregion.DataValueField = "id";
            ddlregion.DataBind();
        }
        ddlregion.Items.Insert(0, "Select Region");

    }

    protected void BindData()
    {
        Div1.Visible = true;
        aBack.Visible = true;
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



            if (dt.Rows.Count > 0)
            {
                bweight = Convert.ToDecimal(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["IsPaperBook"].ToString() == "True")
                    {
                        bweight = bweight + Convert.ToDecimal(dt.Rows[i]["bweight"].ToString());
                    }
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["IsPaperBook"].ToString() == "True")
                    {
                        serdiv.Visible = true;
                        break;
                    }
                    else
                    { serdiv.Visible = false; }
                }
            }
            else
            { serdiv.Visible = false; }
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
            Amount = 0;
            string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["IsFree"]) != true)
                {
                    //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"]); 
                    Amount += Decimal.Round(Convert.ToDecimal(dt.Rows[i]["FinalCartPrice1"]), 2);
                    if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                        Qty++;
                    if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                        Qty += Convert.ToInt32(dt.Rows[i]["Qauntity"]);
                }
            }
            lblAmount1.Text = lblAmount.Text = Amount.ToString().Replace(",", ".");
            lblitems.Text = Qty + " item(s)";
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());

        }
        DataTable dtAdd = new DataTable();
        if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"].ToString() != "")
        {
            //objBookAddDetail.UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            objBookAddDetail.UserID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["UserID"])).ToString());
        }
        if (dt != null && dt.Rows.Count > 0)
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
            lblAmount1.Text = lblAmount.Text = "0.00";
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
            CheckBox chkDeliver = (CheckBox)rptRecords1.Items[0].FindControl("chkDeliver");
            chkDeliver.Checked = true;
            chkDeliver.Enabled = false;

            HiddenField hfAddID = (HiddenField)rptRecords1.Items[0].FindControl("hfAddID");
            HiddenField hfAmount = (HiddenField)rptRecords1.Items[0].FindControl("hfAmount");

            string Amount1 = GetShippingCharge(hfAddID.Value, hfAmount);
            chknational.Checked = true;
            if (Amount1.Contains("$"))
            {
                chkdhl.Enabled = true;
            }
            else
            {
                chkdhl.Enabled = false;
                chkdhl.Checked = false;
            }
        }
        else
        {
            chkout.Visible = false;
            rptRecords1.Visible = false;
            lblDefaultMessage.Visible = true;
            //lblAmount1.Text = lblAmount.Text = "0.00";
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                lblDefaultMessage.Text = "No se encontró la dirección";
            }
            else
            {
                lblDefaultMessage.Text = "No address found";
            }
        }

        decimal amt = Convert.ToDecimal(0);
        if (serdiv.Visible == true)
        {

            foreach (RepeaterItem i in rptRecords1.Items)
            {
                CheckBox txtExample = (CheckBox)i.FindControl("chkDeliver");
                if (txtExample.Checked == true)
                {
                    Label cou = (Label)i.FindControl("lblCountry");
                    Label cit = (Label)i.FindControl("lblCity");

                    string country_c = cou.Text;

                    DataTable dtrc = d.filltable("select rc.RId, r.* from[dbo].[Region_Country] rc inner join [dbo].[Region] r on r.Id = rc.RId where [Country_Id] = (select [CountryId] from [dbo].[tblcountry] where [countryname] = '" + country_c + "' and isactive = 1)");
                    if (dtrc.Rows.Count > 0)
                    {
                        if (dtrc.Rows[0]["Region"].ToString().ToLower() == "central america")//region
                        {
                            #region if region
                            if (country_c.ToLower() == "el salvador")//country
                            {
                                #region if country
                                if (Convert.ToDecimal(bweight) >= Convert.ToDecimal(2))
                                { }
                                else
                                {
                                    DataTable dt_nsc = d.filltable("select * from  National_Shipping_Cost where isactive = 1");
                                    if (dt_nsc.Rows.Count > 0)
                                    {
                                        if ((cit.Text).ToLower() == dt_nsc.Rows[0]["city"].ToString().ToLower())//city "san salvador"
                                        {
                                            #region if city

                                            if (Convert.ToDecimal(bweight) == Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + val;//3.72
                                            }
                                            else if (Convert.ToDecimal(bweight) > Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + (val * (bweight / Convert.ToDecimal(0.5)));
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region no city
                                            if (Convert.ToDecimal(bweight) == Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + val;
                                                //amt = amt + Convert.ToDecimal(dt_nsc.Rows[1]["shipping_cost"].ToString().Replace(",", "."));//4.52
                                            }
                                            else if (Convert.ToDecimal(bweight) > Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + (val * (bweight / Convert.ToDecimal(0.5)));
                                            }
                                            #endregion
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                if (dta.Rows.Count > 0)
                                {
                                    var x = dta.Rows[0]["Price"].ToString();
                                    var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                    amt = amt + val;
                                    // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                }
                            }
                            #endregion
                        }
                        else if (dtrc.Rows[0]["Region"].ToString().ToLower() == "united states & canada")//region
                        {
                            #region if region
                            if (country_c.ToLower() == "florida")//country
                            {
                                if ((cit.Text).ToLower() == "miami")//city "miami"
                                {
                                    #region if city
                                    DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '0' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                    if (dta.Rows.Count > 0)
                                    {
                                        var x = dta.Rows[0]["Price"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + val;
                                        // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region no city
                                    DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                    if (dta.Rows.Count > 0)
                                    {
                                        var x = dta.Rows[0]["Price"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + val;
                                        // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                    }
                                    #endregion
                                }

                            }
                            else
                            {
                                DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                if (dta.Rows.Count > 0)
                                {
                                    var x = dta.Rows[0]["Price"].ToString();
                                    var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                    amt = amt + val;
                                    // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                            if (dta.Rows.Count > 0)
                            {
                                var x = dta.Rows[0]["Price"].ToString();
                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                amt = amt + val;
                                //  amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                            }
                        }
                    }
                }
            }
        }

        //  lblShipAmount.Text = "Total Weight : " + Math.Round(bweight, 2) + " and Shipping Amount : " +  Math.Round(amt, 2) + " ";
        // lblShipAmount.Text ="National Currier Shipping Amount : " + dhlamt + "    Shipping Amount : " + Math.Round(amt, 2) + " ";
        lblShipAmount.Text = "$ " + Math.Round(amt, 2).ToString().Replace(",", ".") + " "; //National Currier Shipping Amount : 
        lblShipAmount1.Text = Math.Round(amt, 2).ToString().Replace(",", ".");



        #region weight wise money
        //        if (rblTest.SelectedItem.Text == "Economic")//economic
        //        {
        //            DataTable dta = d.filltable("select * from [dbo].[ECONOMIC_SERVICE] where '" + bweight + "' between [From_Weight] and [To_Weight] and [IsActive] = 1 and [IsDelete] = 0 ");
        //            if (dta.Rows.Count > 0)
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ " + dta.Rows[0]["Price"].ToString();
        //                lblShipAmount1.Text = dta.Rows[0]["Price"].ToString();
        //            }
        //            else
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ 0.00";
        //                lblShipAmount1.Text = "0.00";
        //            }
        //        }

        //        if (rblTest.SelectedItem.Text == "International")//international
        //        {
        //            DataTable dta = d.filltable("select * from [dbo].[International_SERVICE] where '" + bweight + "' between [From_Weight] and [To_Weight] and [IsActive] = 1 and [IsDelete] = 0 ");
        //            if (dta.Rows.Count > 0)
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ " + dta.Rows[0]["Price"].ToString();
        //                lblShipAmount1.Text = dta.Rows[0]["Price"].ToString();
        //            }
        //            else
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ 0.00";
        //                lblShipAmount1.Text = "0.00";
        //            }
        //        }

        //        if (rblTest.SelectedItem.Text == "International EMS-Currier")//international currier
        //        {
        //            string ci = "";
        //            foreach (RepeaterItem i in rptRecords1.Items)
        //            {
        //                CheckBox txtExample = (CheckBox)i.FindControl("chkDeliver");
        //                if (txtExample.Checked == true)
        //                {
        //                    Label cou = (Label)i.FindControl("lblCountry");
        //                    ci = cou.Text;
        //                }
        //            }

        //            DataTable dtci = d.filltable("select * from [dbo].[tblcountry] where [countryname] = '" + ci + "'");
        //            int cid = Convert.ToInt32(dtci.Rows[0][0].ToString());

        //            DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where '" + bweight + "' between [From_Weight] and [To_Weight] and [IsActive] = 1 and [IsDelete] = 0 and [Country_Id] = '" + cid + "' ");
        //            if (dta.Rows.Count > 0)
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ " + dta.Rows[0]["Price"].ToString();
        //                lblShipAmount1.Text = dta.Rows[0]["Price"].ToString();
        //            }
        //            else
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ 0.00";
        //                lblShipAmount1.Text = "0.00";
        //            }
        //        }

        //        if (rblTest.SelectedItem.Text == "Special")//special
        //        {
        //            DataTable dta = d.filltable("select * from [dbo].[Special_SERVICE] where '" + bweight + "' between [From_Weight] and [To_Weight] and [IsActive] = 1 and [IsDelete] = 0 ");
        //            if (dta.Rows.Count > 0)
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ " + dta.Rows[0]["Price"].ToString();
        //                lblShipAmount1.Text = dta.Rows[0]["Price"].ToString();
        //            }
        //            else
        //            {
        //                lblShipAmount.Text = "Total Weight : " + bweight + " and Shipping Amount : $ 0.00" ;
        //                lblShipAmount1.Text = "0.00";
        //            }
        //        }

        //        lblAmount.Text = (Convert.ToDecimal(lblAmount.Text) + Convert.ToDecimal(lblShipAmount1.Text)).ToString();
        #endregion

        if (rptRecords1.DataSource != null)
        {
            rblTest.Items[2].Enabled = true;
        }
        else
        { rblTest.Items[2].Enabled = false; }
    }

    protected void BindData1()
    {
        Div1.Visible = true;
        aBack.Visible = true;
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



            if (dt.Rows.Count > 0)
            {
                bweight = Convert.ToDecimal(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["IsPaperBook"].ToString() == "True")
                    {
                        bweight = bweight + Convert.ToDecimal(dt.Rows[i]["bweight"].ToString());
                    }
                }

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["IsPaperBook"].ToString() == "True")
                    {
                        serdiv.Visible = true;
                        break;
                    }
                    else
                    { serdiv.Visible = false; }
                }
            }
            else
            { serdiv.Visible = false; }
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
            Amount = 0;
            string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["IsFree"]) != true)
                {
                    //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"]); 
                    Amount += Decimal.Round(Convert.ToDecimal(dt.Rows[i]["FinalCartPrice1"]), 2);
                    if (Convert.ToBoolean(dt.Rows[i]["IseBook"]))
                        Qty++;
                    if (Convert.ToBoolean(dt.Rows[i]["IsPaperBook"]))
                        Qty += Convert.ToInt32(dt.Rows[i]["Qauntity"]);
                }
            }
            lblAmount1.Text = lblAmount.Text = Amount.ToString().Replace(",", ".");
            lblitems.Text = Qty + " item(s)";
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());

        }
        DataTable dtAdd = new DataTable();
        if (Request.QueryString["UserID"] != null && Request.QueryString["UserID"].ToString() != "")
        {
            //objBookAddDetail.UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            objBookAddDetail.UserID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["UserID"])).ToString());
        }

        if (dt != null && dt.Rows.Count > 0)
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
            lblAmount1.Text = lblAmount.Text = "0.00";
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


        //dtAdd = objBookAddDetail.GetBookAddressByUser();
        //if (dtAdd != null && dtAdd.Rows.Count > 0)
        //{
        //    rptRecords1.DataSource = dtAdd;
        //    rptRecords1.DataBind();
        //    rptRecords1.Visible = true;
        //    CheckBox chkDeliver = (CheckBox)rptRecords1.Items[0].FindControl("chkDeliver");
        //    chkDeliver.Checked = true;
        //}
        //else
        //{
        //    chkout.Visible = false;
        //    rptRecords1.Visible = false;
        //    lblDefaultMessage.Visible = true;
        //    //lblAmount1.Text = lblAmount.Text = "0.00";
        //    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        //    {
        //        lblDefaultMessage.Text = "No se encontró la dirección";
        //    }
        //    else
        //    {
        //        lblDefaultMessage.Text = "No address found";
        //    }
        //}

        decimal amt = Convert.ToDecimal(0);
        if (serdiv.Visible == true)
        {

            foreach (RepeaterItem i in rptRecords1.Items)
            {
                CheckBox txtExample = (CheckBox)i.FindControl("chkDeliver");

                if (txtExample.Checked == true)
                {
                    HiddenField hfAddID = (HiddenField)i.FindControl("hfAddID");
                    HiddenField hfAmount = (HiddenField)i.FindControl("hfAmount");

                    string Amount1 = GetShippingCharge(hfAddID.Value, hfAmount);
                    chknational.Checked = true;
                    if (Amount1.Contains("$"))
                    {
                        chkdhl.Enabled = true;
                    }
                    else
                    {
                        chkdhl.Enabled = false;
                        chkdhl.Checked = false;
                    }

                    Label cou = (Label)i.FindControl("lblCountry");
                    Label cit = (Label)i.FindControl("lblCity");

                    string country_c = cou.Text;

                    DataTable dtrc = d.filltable("select rc.RId, r.* from[dbo].[Region_Country] rc inner join [dbo].[Region] r on r.Id = rc.RId where [Country_Id] = (select [CountryId] from [dbo].[tblcountry] where [countryname] = '" + country_c + "' and isactive = 1)");
                    if (dtrc.Rows.Count > 0)
                    {
                        if (dtrc.Rows[0]["Region"].ToString().ToLower() == "central america")//region
                        {
                            #region if region
                            if (country_c.ToLower() == "el salvador")//country
                            {
                                #region if country
                                if (Convert.ToDecimal(bweight) >= Convert.ToDecimal(2))
                                { }
                                else
                                {
                                    DataTable dt_nsc = d.filltable("select * from  National_Shipping_Cost where isactive = 1");
                                    if (dt_nsc.Rows.Count > 0)
                                    {
                                        if ((cit.Text).ToLower() == dt_nsc.Rows[0]["city"].ToString().ToLower())//city "san salvador"
                                        {
                                            #region if city

                                            if (Convert.ToDecimal(bweight) == Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + val;
                                                //amt = amt + Convert.ToDecimal(dt_nsc.Rows[0]["shipping_cost"].ToString().Replace(",", "."));//3.72
                                            }
                                            else if (Convert.ToDecimal(bweight) > Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + (val * (bweight / Convert.ToDecimal(0.5)));
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region no city
                                            if (Convert.ToDecimal(bweight) == Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + val;
                                                // amt = amt + Convert.ToDecimal(dt_nsc.Rows[1]["shipping_cost"].ToString().Replace(",", "."));//4.52
                                            }
                                            else if (Convert.ToDecimal(bweight) > Convert.ToDecimal(0.5))
                                            {
                                                var x = dt_nsc.Rows[0]["shipping_cost"].ToString();
                                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                                amt = amt + (val * (bweight / Convert.ToDecimal(0.5)));
                                            }
                                            #endregion
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                if (dta.Rows.Count > 0)
                                {
                                    var x = dta.Rows[0]["Price"].ToString();
                                    var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                    amt = amt + val;
                                    //  amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                }
                            }
                            #endregion
                        }
                        else if (dtrc.Rows[0]["Region"].ToString().ToLower() == "united states & canada")//region
                        {
                            #region if region
                            if (country_c.ToLower() == "florida")//country
                            {
                                if ((cit.Text).ToLower() == "miami")//city "miami"
                                {
                                    #region if city
                                    DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '0' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                    if (dta.Rows.Count > 0)
                                    {
                                        var x = dta.Rows[0]["Price"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + val;
                                        // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region no city
                                    DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                    if (dta.Rows.Count > 0)
                                    {
                                        var x = dta.Rows[0]["Price"].ToString();
                                        var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                        amt = amt + val;
                                        // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                    }
                                    #endregion
                                }

                            }
                            else
                            {
                                DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                                if (dta.Rows.Count > 0)
                                {
                                    var x = dta.Rows[0]["Price"].ToString();
                                    var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                    amt = amt + val;
                                    // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            DataTable dta = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Region_Id] = '" + Convert.ToInt32(dtrc.Rows[0]["id"].ToString()) + "' and [From_Weight] = '" + bweight.ToString().Replace(",", ".") + "' and [IsDelete] = 0 and [IsActive] = 1 ");
                            if (dta.Rows.Count > 0)
                            {
                                var x = dta.Rows[0]["Price"].ToString();
                                var val = decimal.Parse(x, NumberStyles.AllowDecimalPoint);
                                amt = amt + val;
                                // amt = amt + Convert.ToDecimal(dta.Rows[0]["Price"].ToString().Replace(",", "."));
                            }
                        }
                    }
                }
            }
        }

        //lblShipAmount.Text = "Total Weight : " + Math.Round(bweight, 2) + " and Shipping Amount : " + Math.Round(amt, 2) + " ";
        lblShipAmount.Text = " $ " + Math.Round(amt, 2).ToString().Replace(",", ".") + " "; //National Currier Shipping Amount :
        lblShipAmount1.Text = Math.Round(amt, 2).ToString().Replace(",", ".");



        if (rptRecords1.DataSource != null)
        {
            rblTest.Items[2].Enabled = true;
        }
        else
        { rblTest.Items[2].Enabled = false; }
    }

    protected void lnk_vis_Click(object sender, EventArgs e)
    {
        LinkButton ddl = sender as LinkButton;
        RepeaterItem item = ddl.Parent as RepeaterItem;

        TextBox txtcity = item.FindControl("txtcity12") as TextBox;
        DropDownList ddlcityi = item.FindControl("txtCity") as DropDownList;

        txtcity.Attributes.Add("placeholder", Localization.ResourceManager.GetString("cityplaceholder", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
        if (txtcity.Visible == true)
        {
            txtcity.Visible = false;
            ddlcityi.Visible = true;
        }
        else
        {
            txtcity.Visible = true;
            ddlcityi.Visible = false;
        }
    }

    protected void ddlCountryRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        RepeaterItem item = ddl.Parent as RepeaterItem;
        DropDownList tb = item.FindControl("ddlCountryRpt") as DropDownList;
        string a = tb.SelectedValue;

        DropDownList tb1 = item.FindControl("txtCity") as DropDownList;
        tb1.Items.Clear();
        DataTable dtd = d.filltable("select tc.* from [dbo].[tbl_city] tc inner join [dbo].[tblcountry] t on t.[countryid] = tc.[countryid] where tc.isdelete = 0 and tc.isactive = 1 and t.[countryname] = '" + a + "' order by tc.city asc");
        if (dtd.Rows.Count > 0)
        {
            tb1.DataTextField = "city";
            tb1.DataValueField = "id";
            tb1.DataSource = dtd;
            tb1.DataBind();
            //tb1.Items.Insert(0, "Select City");
            //tb1.Items.Insert(dtd.Rows.Count + 1, "Write your city");
            if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
            {
                tb1.Items.Insert(0, "Select City");
                tb1.Items.Insert(dtd.Rows.Count + 1, "Write your city");
            }
            else
            {
                tb1.Items.Insert(0, "Ciudad selecta");
                tb1.Items.Insert(dtd.Rows.Count + 1, "Escribe tu ciudad");
            }
        }
        else
        {
            if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
            {
                tb1.Items.Insert(0, "Select City");
                tb1.Items.Insert(dtd.Rows.Count + 1, "Write your city");
            }
            else
            {
                tb1.Items.Insert(0, "Ciudad selecta");
                tb1.Items.Insert(dtd.Rows.Count + 1, "Escribe tu ciudad");
            }
        }

        TextBox txtcity = item.FindControl("txtcity12") as TextBox;
        DropDownList ddlcityi = item.FindControl("txtCity") as DropDownList;
        txtcity.Visible = false;
        ddlcityi.Visible = true;
    }

    protected void del(object source, RepeaterCommandEventArgs e)
    {
        Repeater1.Visible = false;
        Div1.Visible = false;
        aBack.Visible = false;
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

        LinkButton lnk_vis = (LinkButton)item.FindControl("lnk_vis");

        Label locallizationname = (Label)item.FindControl("locallizationname");
        Label locallizationaddress1 = (Label)item.FindControl("locallizationaddress1");
        Label locallizationaddress2 = (Label)item.FindControl("locallizationaddress2");
        Label locallizationregion = (Label)item.FindControl("locallizationregion");
        Label locallizationcountry = (Label)item.FindControl("locallizationcountry");
        Label locallizationstate = (Label)item.FindControl("locallizationstate");
        Label locallizationcity = (Label)item.FindControl("locallizationcity");
        Label locallizationpincode = (Label)item.FindControl("locallizationpincode");
        Label locallizationPhone = (Label)item.FindControl("locallizationPhone");


        TextBox txtName = (TextBox)item.FindControl("txtName");
        TextBox txtcity12 = (TextBox)item.FindControl("txtcity12");

        //TextBox txtName = (TextBox)item.FindControl("txtName");
        TextBox txtStreetAddress = (TextBox)item.FindControl("txtStreetAddress");
        TextBox txtLandmark = (TextBox)item.FindControl("txtLandmark");
        DropDownList txtCity = (DropDownList)item.FindControl("txtCity");
        TextBox txtState = (TextBox)item.FindControl("txtState");
        DropDownList ddlCountryRpt = (DropDownList)item.FindControl("ddlCountryRpt");
        DropDownList ddlregrpt = (DropDownList)item.FindControl("ddlregrpt");

        TextBox txtPincode = (TextBox)item.FindControl("txtPincode");
        TextBox txtPhoneNumber = (TextBox)item.FindControl("txtPhoneNumber");

        LinkButton lnkUpdate = (LinkButton)item.FindControl("lnkUpdate");
        LinkButton lnkCancel = (LinkButton)item.FindControl("lnkCancel");
        LinkButton lnkEdit = (LinkButton)item.FindControl("LinkButton2");
        LinkButton lnkDelete = (LinkButton)item.FindControl("LinkButton1");
        LinkButton lnkDeliver = (LinkButton)item.FindControl("lnkDeliver");

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

            locallizationPhone.Visible = true;
            locallizationpincode.Visible = true;
            locallizationcity.Visible = true;
            locallizationcountry.Visible = true;
            locallizationstate.Visible = true;
            locallizationregion.Visible = true;
            locallizationaddress2.Visible = true;
            locallizationaddress1.Visible = true;
            locallizationname.Visible = true;
            txtName.Visible = true;            
            txtStreetAddress.Visible = true;
            txtLandmark.Visible = true;
            txtCity.Visible = true;
            txtState.Visible = true;
            ddlCountryRpt.Visible = true;
            ddlregrpt.Visible = true;
            txtPincode.Visible = true;
            txtPhoneNumber.Visible = true;
            lnk_vis.Visible = true;
            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
            lnkUpdate.Visible = true;
            lnkCancel.Visible = true;
            lnkDeliver.Visible = false;


            DataTable dtr = d.filltable("select rc.RId , r.* from [dbo].[Region_Country] rc inner join [dbo].[Region] r on r.Id = rc.RId where [Country_Id] = (select [CountryId] from [dbo].[tblcountry] where LOWER([countryname]) = '" + lblCountry.Text.ToLower() + "' and isactive = 1) ");
            if (dtr.Rows.Count > 0)
            {
                // BindRegion();
                DataTable dt1 = d.filltable("regionlist");

                if (dt1.Rows.Count > 0)
                {
                    ddlregrpt.DataSource = dt1;
                    ddlregrpt.DataTextField = "Region";
                    ddlregrpt.DataValueField = "id";
                    ddlregrpt.DataBind();
                }
                ddlregrpt.Items.Insert(0, "Select Region");
                ddlregrpt.SelectedValue = dtr.Rows[0][0].ToString();
                //BindCountryrpt(Convert.ToInt32(ddlregrpt.SelectedValue));
                DataTable dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + Convert.ToInt32(ddlregrpt.SelectedValue) + "')");

                int columnNumber = 1; //Put your column X number here
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][columnNumber] = dt.Rows[i][columnNumber].ToString().ToLower();
                }

                if (dt.Rows.Count > 0)
                {
                    ddlCountryRpt.DataSource = dt;
                    ddlCountryRpt.DataTextField = "countryname";
                    ddlCountryRpt.DataValueField = "countryname";
                    ddlCountryRpt.DataBind();
                }

                //ddlCountryRpt.SelectedValue = lblCountry.Text.ToLower();
                //if (lblCountry.Text != null)
                //{
                //    ddlCountryRpt.SelectedValue = lblCountry.Text.ToLower().ToString();
                //}
            }
            else
            {
                DataTable dt1 = d.filltable("regionlist");

                if (dt1.Rows.Count > 0)
                {
                    ddlregrpt.DataSource = dt1;
                    ddlregrpt.DataTextField = "Region";
                    ddlregrpt.DataValueField = "id";
                    ddlregrpt.DataBind();
                }
                ddlregrpt.Items.Insert(0, "Select Region");
            }


            txtCity.Items.Clear();
            DataTable dtd = d.filltable("select tc.* from [dbo].[tbl_city] tc inner join [dbo].[tblcountry] t on t.[countryid] = tc.[countryid] where tc.isdelete = 0 and tc.isactive = 1 and t.[countryname] = '" + ddlCountryRpt.SelectedValue + "' order by tc.city asc");
            if (dtd.Rows.Count > 0)
            {
                txtCity.DataTextField = "city";
                txtCity.DataValueField = "city";
                txtCity.DataSource = dtd;
                txtCity.DataBind();
                //txtCity.Items.Insert(0, "Select City");
                //txtCity.Items.Insert(dtd.Rows.Count + 1, "Write your city");
                if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
                {
                    txtCity.Items.Insert(0, "Select City");
                    txtCity.Items.Insert(dtd.Rows.Count + 1, "Write your city");
                }
                else
                {
                    txtCity.Items.Insert(0, "Ciudad selecta");
                    txtCity.Items.Insert(dtd.Rows.Count + 1, "Escribe tu ciudad");
                }
            }
            else
            {
                if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
                {
                    txtCity.Items.Insert(0, "Select City");
                    txtCity.Items.Insert(dtd.Rows.Count + 1, "Write your city");
                }
                else
                {
                    txtCity.Items.Insert(0, "Ciudad selecta");
                    txtCity.Items.Insert(dtd.Rows.Count + 1, "Escribe tu ciudad");
                }
            }

            try
            {
                txtCity.SelectedValue = lblCity.Text;
                txtCity.Visible = true;
                txtcity12.Visible = false;
            }
            catch (Exception ex)
            {
                txtcity12.Visible = true;
                txtCity.Visible = false;
                txtcity12.Text = lblCity.Text;
            }
            //Country objCountry = new Country();
            //DataTable dt = objCountry.SelectAllActiveCountry();

            //if (dt.Rows.Count > 0)
            //{
            //    ddlCountryRpt.DataSource = dt;
            //    ddlCountryRpt.DataTextField = "countryname";
            //    ddlCountryRpt.DataValueField = "countryname";
            //    ddlCountryRpt.DataBind();
            //}
            //ddlCountryRpt.SelectedValue = lblCountry.Text;

            if (ddlregrpt.SelectedItem.Text.ToUpper() == "CENTRAL AMERICA")
            {

                //LocalizedLiteral11.Visible = false;

                locallizationpincode.Visible = false;
                txtPincode.Visible = false;

            }
            else
            {
                locallizationpincode.Visible = true;
                txtPincode.Visible = true;
            }


        }
        if (e.CommandName == "update")
        {
            try
            {
                var id = e.CommandArgument;
                objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(id.ToString());
                objBookAddDetail.IsDefault = false;
                objBookAddDetail.UserID = Convert.ToInt32(Session["UserID"].ToString());
                objBookAddDetail.Name = txtName.Text;
                objBookAddDetail.StreetAddress = txtStreetAddress.Text;
                objBookAddDetail.Landmark = txtLandmark.Text;

                // objBookAddDetail.City = txtCity.Text;
                if (txtcity12.Visible == true)
                {
                    DataTable dtc = d.filltable("select * from [dbo].[tblcountry] where [countryname] = '" + ddlCountryRpt.Text + "' ");
                    if (dtc.Rows.Count > 0)
                    {
                        d.executedml("[dbo].[Insert_city] '" + txtcity12.Text + "' ,'" + dtc.Rows[0][0].ToString() + "' ");
                    }

                    objBookAddDetail.City = txtcity12.Text;
                }
                else
                {
                    objBookAddDetail.City = txtCity.SelectedItem.Text;
                }


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
        //Repeater1.Visible = true;
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

        // objBookAddDetail.City = txtcity.Text;

        if (txtcity.Visible == true)
        {
            objBookAddDetail.City = txtcity.Text;
            DataTable dtc = d.filltable("select * from [dbo].[tblcountry] where [countryname] = '" + ddlCountry.SelectedValue + "' ");
            if (dtc.Rows.Count > 0)
            {
                d.executedml("[dbo].[Insert_city] '" + txtcity.Text + "' ,'" + dtc.Rows[0][0].ToString() + "' ");
            }
        }
        else
        {
            objBookAddDetail.City = ddlcityi.SelectedItem.Text;
        }

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
        LinkButton btn = (LinkButton)(sender);
        Session["addressDetail"] = btn.CommandArgument;
        Session["website"] = "true";
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            if (Request.QueryString["id"] != null && Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))) > 0)
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                ObjBookOrders.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                DataTable WishDT = ObjBookOrders.GetCartList();
                //int result = 0;
                int cnt = 1;

                var piceses = "";
                for (int i = 0; i < WishDT.Rows.Count; i++)
                {
                    objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);

                    if (Convert.ToBoolean(WishDT.Rows[i]["IsPaperBook"]))
                    {
                        var quantity = Convert.ToInt32(WishDT.Rows[i]["Qauntity"]);
                        for (int j = 0; j < quantity; j++)
                        {
                            try
                            {
                                piceses += "<Piece><PieceID>" + cnt + "</PieceID>"
                                        + "<Height>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Height"].ToString()) ? WishDT.Rows[i]["Height"].ToString() : "1") + "</Height>"
                                        + "<Depth>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Depth"].ToString()) ? WishDT.Rows[i]["Depth"].ToString() : "1") + "</Depth>"
                                        + "<Width>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Width"].ToString()) ? WishDT.Rows[i]["Width"].ToString() : "1") + "</Width>"
                                        + "<Weight>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Weight"].ToString()) ? WishDT.Rows[i]["Weight"].ToString() : "1") + "</Weight>"
                                        + "</Piece>";
                            }
                            catch (Exception)
                            {
                                piceses += "<Piece><PieceID>" + cnt + "</PieceID>"
                                        + "<Height>1</Height>"
                                        + "<Depth>1</Depth>"
                                        + "<Width>1</Width>"
                                        + "<Weight>5.0</Weight></Piece>";
                            }
                            cnt++;
                        }
                    }
                }

                DataTable dt = new DataTable();
                objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(btn.CommandArgument);
                dt = objBookAddDetail.GetDataByPK();

                var ShippingCharge = "0";
                var TotalAmount = "0";
                try
                {
                    GetQuote gq = new GetQuote();
                    StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/GetQuote.xml"));
                    string text = streamReader.ReadToEnd();

                    string xmlRequest = gq.replaceXml(text, dt.Rows[0]["ISOCode"].ToString(), piceses, dt.Rows[0]["Pincode"].ToString(),dt.Rows[0]["City"].ToString());
                    string response = gq.sendRequest(xmlRequest);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(response);
                    try
                    {
                        var condition = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/Note/Condition/ConditionData").InnerText;
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", condition), true);
                        Session["condition"] = condition.Replace("origin postcode 10504 or", "");
                        Response.Redirect(Request.RawUrl);
                    }
                    catch (Exception)
                    {

                    }
                    ShippingCharge = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/ShippingCharge").InnerText;
                    TotalAmount = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/QtdSInAdCur/TotalAmount").InnerText;
                }
                catch (Exception)
                {
                }

                var ShippingType = "";

                if (chkdhl.Checked == true)
                {
                    ShippingType = "DHL";
                }
                else if(chknational.Checked == true)
                {
                    ShippingType = "National";
                }

                if (WishDT != null && WishDT.Rows.Count > 0)
                {
                    Response.Redirect("Payment.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&UserID=" + ObjBookOrders.CustomerID + "&ShippingCharge=" + ShippingCharge + "&Address=" + btn.CommandArgument + "&ShippingType=" + ShippingType + "");
                    //Response.Redirect("PaymentNew.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&UserID=" + ObjBookOrders.CustomerID + "");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Please Click on Continue to shopping", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["id"] + "");
                }

            }
            else
            {
                if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                {
                    Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "");
                }
                else
                {
                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No hay productos en cartlist. Por favor, haz clic en Continuar con la compra de las compras continúan');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No Items in cartlist. Please Click on Continue to shopping for shopping continue');", true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Please Click on Continue to shopping", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "");
        }
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
        if (!string.IsNullOrEmpty(tb1.Text))
        {
            if (Convert.ToInt32(tb1.Text) != 0 && Convert.ToInt32(tb1.Text) <= Convert.ToInt32(tb1.ToolTip))
            {
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
            }
        }
        BindData();
    }

    protected void rptRecords1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        RepeaterItem item = e.Item;
        if (item.ItemType == ListItemType.AlternatingItem || item.ItemType == ListItemType.Item)
        {
            CheckBox chkDeliver = (CheckBox)e.Item.FindControl("chkDeliver");
            string te = chkDeliver.ToolTip;
            string script = "SetSingleRadioButton('" + chkDeliver.ClientID + "',this)";
            chkDeliver.Attributes.Add("onclick", script);

            HiddenField hfAddID = (HiddenField)item.FindControl("hfAddID");
            HiddenField hfAmount = (HiddenField)item.FindControl("hfAmount");
            LinkButton lnkDeliver = (LinkButton)item.FindControl("lnkDeliver");
            Label lblShipping = (Label)item.FindControl("lblShipping");
            string Amount1 = GetShippingCharge(hfAddID.Value, hfAmount);

            lblShipping.Text = Amount1;
            if (Amount1.Contains("$"))
            {
                lblShipping.ForeColor = Color.Green;
                chkDeliver.Visible = lnkDeliver.Visible = true;
                //lblShipping.Visible = false;
            }
            else
            {
                lblShipping.ForeColor = Color.Red;
                // chkDeliver.Visible = lnkDeliver.Visible = false;
            }
        }
    }

    private string GetShippingCharge(string address, HiddenField hf)
    {
        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
        ObjBookOrders.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
        DataTable WishDT = ObjBookOrders.GetCartList();
        //int result = 0;
        int cnt = 1;

        var piceses = "";
        for (int i = 0; i < WishDT.Rows.Count; i++)
        {
            objPurchase.OrderID = Convert.ToInt32(WishDT.Rows[i]["OrderID"]);

            if (Convert.ToBoolean(WishDT.Rows[i]["IsPaperBook"]))
            {
                var quantity = Convert.ToInt32(WishDT.Rows[i]["Qauntity"]);
                for (int j = 0; j < quantity; j++)
                {
                    try
                    {
                        piceses += "<Piece><PieceID>" + cnt + "</PieceID>"
                                + "<Height>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Height"].ToString()) ? WishDT.Rows[i]["Height"].ToString() : "1") + "</Height>"
                                + "<Depth>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Depth"].ToString()) ? WishDT.Rows[i]["Depth"].ToString() : "1") + "</Depth>"
                                + "<Width>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Width"].ToString()) ? WishDT.Rows[i]["Width"].ToString() : "1") + "</Width>"
                                + "<Weight>" + (!string.IsNullOrEmpty(WishDT.Rows[i]["Weight"].ToString()) ? WishDT.Rows[i]["Weight"].ToString() : "1") + "</Weight>"
                                + "</Piece>";
                    }
                    catch (Exception)
                    {
                        piceses += "<Piece><PieceID>" + cnt + "</PieceID>"
                                + "<Height>1</Height>"
                                + "<Depth>1</Depth>"
                                + "<Width>1</Width>"
                                + "<Weight>5.0</Weight></Piece>";
                    }
                    cnt++;
                }
            }
        }

        DataTable dt = new DataTable();
        objBookAddDetail.BookDeliveryAddID = Convert.ToInt32(address);
        dt = objBookAddDetail.GetDataByPK();

        var ShippingCharge = "0";
        var TotalAmount = "0";
        hf.Value = "0";
        try
        {
            GetQuote gq = new GetQuote();
            StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/GetQuote.xml"));
            string text = streamReader.ReadToEnd();

            string xmlRequest = gq.replaceXml(text, dt.Rows[0]["ISOCode"].ToString(), piceses, dt.Rows[0]["Pincode"].ToString(), dt.Rows[0]["City"].ToString());
            string response = gq.sendRequest(xmlRequest);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response);
            try
            {
                var condition = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/Note/Condition/ConditionData").InnerText;
                //ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", condition), true);
                lbldhl1.Text = "";
                lbldhl.Text = "";
                return condition.Replace("origin postcode 10504 or", "");                
                //Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {

            }
            ShippingCharge = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/ShippingCharge").InnerText;
            TotalAmount = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/QtdSInAdCur/TotalAmount").InnerText;
            hf.Value = TotalAmount;
            lbldhl1.Text = "$ " + TotalAmount;
            lbldhl.Text = TotalAmount;
            return ResourceManager.GetString("chargeWithAddress", Thread.CurrentThread.Name) + " $ " + TotalAmount;
        }
        catch (Exception)
        {
            lbldhl1.Text = "$ " + Math.Round(Convert.ToDouble(TotalAmount), 2).ToString();
            lbldhl.Text = TotalAmount;
        }
        hf.Value = "0";
        return ResourceManager.GetString("chargeWithAddress", Thread.CurrentThread.Name) + " $ " + "0";
    }

    private void GetCharge(string Country, int bookid, string pincode, string quanitity, string City)
    {
        GetQuote gq = new GetQuote();

        StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/GetQuote.xml"));
        string text = streamReader.ReadToEnd();

        var piceses = "<Piece><PieceID>1</PieceID><Height>1</Height><Depth>1</Depth><Width>1</Width><Weight>5.0</Weight></Piece>";

        string xmlRequest = gq.replaceXml(text, Country, piceses, pincode, City);
        string response = gq.sendRequest(xmlRequest);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(response);

        var ShippingCharge = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/ShippingCharge").InnerText;
        var TotalAmount = xmlDoc.DocumentElement.SelectSingleNode("GetQuoteResponse/BkgDetails/QtdShp/QtdSInAdCur/TotalAmount").InnerText;
        //<BkgDetails>/<QtdShp>/<ShippingCharge>
        //<BkgDetails>/<QtdShp>/<QtdSInAdCur>/<TotalAmount>
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
            BindOrderData();
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
            BindOrderData();
        }
        return 1;
    }

    public int getQuantity(string bId, string quantity)
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
                BindOrderData();
            }
            //return 1;
            else
                return 0;
        }
        return 1;
    }

    protected void chkDeliver_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)(sender));
        //chk.Enabled = false;
        RepeaterItem rp1 = ((RepeaterItem)(chk.NamingContainer));

        for (int i = 0; i < rptRecords1.Items.Count; i++)
        {
            CheckBox chkDeliver = (CheckBox)rptRecords1.Items[i].FindControl("chkDeliver");
            if (chk.Checked)
            {
                if (chkDeliver.ClientID == chk.ClientID)
                {
                    chkDeliver.Enabled = false;
                    chkDeliver.Checked = true;
                }
                else
                {
                    chkDeliver.Enabled = true;
                    chkDeliver.Checked = false;
                }
            }

        }

        HiddenField hfAmount = (HiddenField)rp1.FindControl("hfAmount");
        HiddenField hfAddID = (HiddenField)rp1.FindControl("hfAddID");

        Label lst = (Label)rp1.FindControl("lblState");

        GetShippingCharge(hfAddID.Value, hfAmount);
        // lblAmount1.Text = lblAmount.Text = (Decimal.Parse(lblAmount.Text) + Decimal.Parse(hfAmount.Value)).ToString();
        // rptRecords1_ItemDataBound(rptRecords1, null);

        BindData1();

    }

    protected void btn_login_Click(object sender, EventArgs e)
    {
        var checkPaper = 0;
        if (Repeater1.Items.Count > 0)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                var hdPaper = (HiddenField)Repeater1.Items[i].FindControl("hdPaper");
                if (Convert.ToBoolean(hdPaper.Value))
                {
                    checkPaper = 1;
                    break;
                }
            }
        }
        if (checkPaper == 1)
        {
            if (rptRecords1.Items.Count > 0)
            {
                var ShippingType = "";

                if (chkdhl.Checked == true)
                {
                    ShippingType = "DHL";
                }
                else if (chknational.Checked == true)
                {
                    ShippingType = "National";
                }

                for (int i = 0; i < rptRecords1.Items.Count; i++)
                {
                    var hfAmount = rptRecords1.Items[i].FindControl("hfAmount") as HiddenField;
                    var hfAdd = rptRecords1.Items[i].FindControl("hfAddID") as HiddenField;
                    var chk = rptRecords1.Items[i].FindControl("chkDeliver") as CheckBox;

                    if (chk != null && chk.Checked)
                    {
                        if (hfAmount != null && hfAdd != null)
                        {
                            var ShippingCharge = hfAmount.Value;
                            var address = hfAdd.Value;

                            if (chkdhl.Checked == true)
                            {
                                Response.Redirect("Payment.aspx?l=" + Thread.CurrentThread.CurrentCulture.Name + "&UserID=" + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))) + "&ShippingCharge=" + lbldhl.Text + "&Address=" + address + "&ShippingType="+ ShippingType + "");
                            }
                            else
                            {
                                Response.Redirect("Payment.aspx?l=" + Thread.CurrentThread.CurrentCulture.Name + "&UserID=" + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))) + "&ShippingCharge=" + lblShipAmount1.Text + "&Address=" + address + "&ShippingType=" + ShippingType + "");
                            }


                        }
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Please add delivery address.');</script>");
                ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", "Please add delivery address."), true);
                Session["errorPayment"] = "1";
                Response.Redirect(Request.RawUrl);
            }
        }
        else
        {
            Response.Redirect("Payment.aspx?l=" + Thread.CurrentThread.CurrentCulture.Name + "&UserID=" + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))) + "&ShippingCharge=0.00&Address=00&&ShippingType=NULL");
        }
    }

    protected void rblTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindCountry();
        BindData();
        if (ddlregion.SelectedItem.Text.ToUpper() == "CENTRAL AMERICA")
        {
            txtpincode.Text = "00000";
            txtpincode.Enabled = false;
            txtpincode.Visible = false;
            llPincode.Visible = false;
        }
        else
        {
            txtpincode.Text = "";
            txtpincode.Enabled = true;
            txtpincode.Visible = true;
            llPincode.Visible = true;
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');", true);
    }

    protected void ddlregrpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindCountryrpt(ddlreg);
        DropDownList ddl = sender as DropDownList;
        RepeaterItem item = ddl.Parent as RepeaterItem;
        DropDownList tb = item.FindControl("ddlregrpt") as DropDownList;
        string a = tb.SelectedValue;
        // BindCountryrpt(Convert.ToInt32(a));

        TextBox txtPincode = item.FindControl("txtPincode") as TextBox;
        LocalizedLiteral llPincode = item.FindControl("LocalizedLiteral11") as LocalizedLiteral;
        if (tb.SelectedItem.Text.ToUpper() == "CENTRAL AMERICA")
        {
            txtPincode.Text = "00000";
            txtPincode.Enabled = false;
            txtPincode.Visible = false;
            txtPincode.Style.Add("display", "none");
            llPincode.Visible = false;
        }
        else
        {
            txtPincode.Text = "";
            txtPincode.Enabled = true;
            txtPincode.Visible = true;
            txtPincode.Style.Add("display", "block");
            llPincode.Visible = true;
        }

        DropDownList tb1 = item.FindControl("ddlCountryRpt") as DropDownList;
        DataTable dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + a + "')");

        if (dt.Rows.Count > 0)
        {
            tb1.DataSource = dt;
            tb1.DataTextField = "countryname";
            tb1.DataValueField = "countryname";
            tb1.DataBind();
        }
        tb1.Items.Insert(0, "Select Country");
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_cou();
        BindData();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');", true);
    }
    public void fill_cou()
    {
        ddlcityi.Items.Clear();
        DataTable dtd = d.filltable("select tc.* from [dbo].[tbl_city] tc inner join [dbo].[tblcountry] t on t.[countryid] = tc.[countryid] where tc.isdelete = 0 and tc.isactive = 1 and t.[countryname] = '" + ddlCountry.SelectedValue + "' order by tc.city asc");
        if (dtd.Rows.Count > 0)
        {
            ddlcityi.DataTextField = "city";
            ddlcityi.DataValueField = "id";
            ddlcityi.DataSource = dtd;
            ddlcityi.DataBind();
            if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
            {
                ddlcityi.Items.Insert(0, "Select City");
                ddlcityi.Items.Insert(dtd.Rows.Count + 1, "Write your city");
            }
            else
            {
                ddlcityi.Items.Insert(0, "Ciudad selecta");
                ddlcityi.Items.Insert(dtd.Rows.Count + 1, "Escribe tu ciudad");
            }
        }
        else
        {
            if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
            {
                ddlcityi.Items.Insert(0, "Select City");
                ddlcityi.Items.Insert(dtd.Rows.Count + 1, "Write your city");
            }
            else
            {
                ddlcityi.Items.Insert(0, "Ciudad selecta");
                ddlcityi.Items.Insert(dtd.Rows.Count + 1, "Escribe tu ciudad");
            }
        }
        txtcity.Visible = false;
        ddlcityi.Visible = true;
    }

    protected void lnkother_Click(object sender, EventArgs e)
    {
        if (txtcity.Visible == true)
        {
            txtcity.Visible = false;
            ddlcityi.Visible = true;
        }
        else
        {
            txtcity.Visible = true;
            ddlcityi.Visible = false;
        }

        BindData();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');", true);
    }

    //protected void chkdhl_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (chkdhl.Checked == true)
    //        {
    //            lblAmount.Text = (Convert.ToDecimal(lblAmount.Text)  + dhlamt).ToString();
    //        }
    //        else
    //        {
    //            lblAmount.Text = (Convert.ToDecimal(lblAmount.Text) + Convert.ToDecimal(lblShipAmount1.Text) - dhlamt).ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    { }
    //}

    //protected void chkdhl_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        rptRecords1_ItemDataBound(rptRecords1, null);
    //        BindData();
    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');val();", true);
    //    }
    //    catch (Exception ex)
    //    { }
    //}
    protected void ddlcityi_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcityi.SelectedItem.Text == "Write your city" || ddlcityi.SelectedItem.Text == "Escribe tu ciudad")
        {
            lnkother_Click(lnkother, null);
            txtcity.Focus();
            txtcity.Attributes.Add("placeholder", Localization.ResourceManager.GetString("cityplaceholder", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
        }
        else
        {
            txtpincode.Focus();
        }
        BindData();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');", true);
    }
    protected void txtCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        RepeaterItem item = ddl.Parent as RepeaterItem;
        DropDownList tb = item.FindControl("ddlCountryRpt") as DropDownList;
        LinkButton lnk_vis = item.FindControl("lnk_vis") as LinkButton;
        TextBox txtcity12 = item.FindControl("txtcity12") as TextBox;
        TextBox txtPincode = item.FindControl("txtPincode") as TextBox;
        DropDownList tb1 = item.FindControl("txtCity") as DropDownList;

        //if (tb1.SelectedItem.Text == "Write your city")
        if (tb1.SelectedItem.Text == "Write your city" || tb1.SelectedItem.Text == "Escribe tu ciudad")
        {
            lnk_vis_Click(lnk_vis, null);
            txtcity12.Attributes.Add("placeholder", Localization.ResourceManager.GetString("cityplaceholder", System.Threading.Thread.CurrentThread.CurrentCulture.Name));
            txtcity12.Text = "";
            txtcity12.Focus();
        }
        else
        {
            txtPincode.Focus();
        }

    }
    
    protected void BindOrderData()
    {
        var Product = "";
        var ProductAmount = "";
        var SubtotalAmount = 0.00;
        if (Repeater1.Items.Count > 0)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                HtmlGenericControl title = (HtmlGenericControl)Repeater1.Items[i].FindControl("spantitle") as HtmlGenericControl;
                var Quanitity = Repeater1.Items[i].FindControl("txtQuanitity") as TextBox;
                var BookType = Repeater1.Items[i].FindControl("lblBookType") as Label;
                if (Convert.ToInt32(Quanitity.Text) > 0)
                {
                    Product += "<span>" + title.InnerText + " (" + Localization.ResourceManager.GetString(BookType.Text, System.Threading.Thread.CurrentThread.CurrentCulture.Name) + ")" + " X " + Quanitity.Text + "</span><br/>";
                }
                else
                {
                    Product += "<span>" + title.InnerText + " (" + Localization.ResourceManager.GetString(BookType.Text, System.Threading.Thread.CurrentThread.CurrentCulture.Name) + ")" + "</span><br/>";
                }                
                var PriceSymbol = Repeater1.Items[i].FindControl("lblsymbol") as Label;
                var Price = Repeater1.Items[i].FindControl("lblPrice") as Label;
                ProductAmount += "<span>" + PriceSymbol.Text + "" + Price.Text + "</span><br/>";                
                if(System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                {
                    var NewPrice = Price.Text.Trim();
                    SubtotalAmount = Convert.ToDouble(SubtotalAmount) + Convert.ToDouble(NewPrice);
                }
                else
                {
                    decimal NewPrice = decimal.Parse(Price.Text.Trim().Replace(".", ","), NumberStyles.AllowDecimalPoint);
                    SubtotalAmount = Convert.ToDouble(SubtotalAmount) + Convert.ToDouble(NewPrice);
                }                
            }
        }
        lblProduct.Text = Product;
        lblProductTotal.Text = ProductAmount;
        lblSubtotalAmount.Text = "$" + SubtotalAmount.ToString().Replace(',','.');
    }   
}