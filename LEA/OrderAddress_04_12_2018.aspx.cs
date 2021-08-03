using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderAddress : System.Web.UI.Page
{
    BookBAL ObjBook = new BookBAL();
    BookOrderBAL ObjBookOrders = new BookOrderBAL();
    BookDeliveryAddressBAL objBookAddDetail = new BookDeliveryAddressBAL();
    BookShippingBAL objShipping = new BookShippingBAL();
    Security S = new Security();
    double Amount;
    dbconnection d = new dbconnection();

    public void fill_cou()
    {
       
        ddlcityi.Items.Clear();
        DataTable dtd = d.filltable("select tc.* from [dbo].[tbl_city] tc inner join [dbo].[tblcountry] t on t.[countryid] = tc.[countryid] where tc.isactive = 1 and t.[countryname] = '" + ddlCountry.SelectedValue + "' order by tc.city asc");
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
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != "" && Session["UserID"] != "0")
        {
            try
            {
                ObjBookOrders.CustomerID = Convert.ToInt32(Session["UserID"]);
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
            catch (Exception ex)
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
            BindRegion();
            BindData();
        }
    }

    private void BindCountry()
    {
        if (ddlregion.SelectedValue == "Select Region")
        {
            ddlcityi.Items.Insert(0, "Select City");
            ddlCountry.Items.Clear();
            ddlCountry.Items.Insert(0, "Select Country");
        }
        else if (ddlregion.SelectedValue == "seleccione región")
        {
            ddlcityi.Items.Insert(0, "Ciudad selecta");
            ddlCountry.Items.Clear();
            ddlCountry.Items.Insert(0, "seleccionar país");
        }
        else
        {
            Country objCountry = new Country();
            ddlCountry.Items.Clear();
            DataTable dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + ddlregion.SelectedValue + "')order by countryname asc");

            if (dt.Rows.Count > 0)
            {
                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "countryname";
                ddlCountry.DataValueField = "countryname";
                ddlCountry.DataBind();
            }

            if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
            {
                ddlCountry.Items.Insert(0, "Select Country");
            }
            else
            {
                ddlCountry.Items.Insert(0, "seleccionar país");
            }

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
        if (Thread.CurrentThread.CurrentCulture.ToString().ToLower() == "en-us")
        {
            ddlregion.Items.Insert(0, "Select Region");
        }
        else
        {
            ddlregion.Items.Insert(0, "seleccione región");
        }

    }


    protected void BindData()
    {
        DataTable dtAdd = new DataTable();
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            objBookAddDetail.UserID = Convert.ToInt32(Session["UserID"]);
        }
        
        dtAdd = objBookAddDetail.GetBookAddressByUser();
        if (dtAdd != null && dtAdd.Rows.Count > 0)
        {
            rptRecords1.DataSource = dtAdd;
            rptRecords1.DataBind();
            rptRecords1.Visible = true;
            txtname.Visible = true;
        }
        else
        {
            chkout.Visible = false;
            rptRecords1.Visible = false;
            lblDefaultMessage.Visible = true;
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
       // Label lblreg = (Label)item.FindControl("lblreg");
        Label lblCityCode = (Label)item.FindControl("lblCityCode");
        Label lblPhoneNumber = (Label)item.FindControl("lblPhoneNumber");
        Label lb_name = (Label)item.FindControl("Label1");
        Label lb_street = (Label)item.FindControl("lb_street");
        Label lb_Landmark = (Label)item.FindControl("lb_Landmark");
        Label lb_City = (Label)item.FindControl("lb_City");
        Label lb_State = (Label)item.FindControl("lb_State");
        Label lb_pincode = (Label)item.FindControl("lb_pincode");
        Label lb_phone = (Label)item.FindControl("lb_phone");
        Label lb_country = (Label)item.FindControl("lb_country");
        LinkButton lnk_vis = (LinkButton)item.FindControl("lnk_vis");
        Label lb_region = (Label)item.FindControl("lb_region");


        TextBox txtName = (TextBox)item.FindControl("txtName");
        TextBox txtcity12 = (TextBox)item.FindControl("txtcity12");

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

        if (e.CommandName == "edit")
        {
            lb_name.Visible = true;
            lb_street.Visible = true;
            lb_Landmark.Visible = true;
            lb_City.Visible = true;
            lb_pincode.Visible = true;
            lb_phone.Visible = true;
            lb_country.Visible = true;
            lb_State.Visible = true;
            lb_region.Visible = true;

            lblName.Visible = false;
            lblStreetAddress.Visible = false;
            lblLandmark.Visible = false;
            lblCity.Visible = false;
            lblState.Visible = false;
            lblCountry.Visible = false;
           // lblreg.Visible = false;
            lblCityCode.Visible = false;
            lblPhoneNumber.Visible = false;
            lnk_vis.Visible = true;
            txtName.Visible = true;
            txtStreetAddress.Visible = true;
            txtLandmark.Visible = true;
            txtCity.Visible = true;
            txtState.Visible = true;
            ddlCountryRpt.Visible = true;
            
            ddlregrpt.Visible = true;
            txtPincode.Visible = true;
            txtPhoneNumber.Visible = true;

            lnkEdit.Visible = false;
            lnkDelete.Visible = false;
            lnkUpdate.Visible = true;
            lnkCancel.Visible = true;


            DataTable dtr = d.filltable("select rc.RId , r.* from [dbo].[Region_Country] rc inner join [dbo].[Region] r on r.Id = rc.RId where [Country_Id] = (select [CountryId] from[dbo].[tblcountry] where [countryname] = '" + lblCountry.Text + "' and isactive = 1) ");
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
                ddlregrpt.Items.Insert(0, "Select");
                ddlregrpt.SelectedValue = dtr.Rows[0][0].ToString();
                //BindCountryrpt(Convert.ToInt32(ddlregrpt.SelectedValue));
                DataTable dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + Convert.ToInt32(ddlregrpt.SelectedValue) + "')");

                if (dt.Rows.Count > 0)
                {
                    ddlCountryRpt.DataSource = dt;
                    ddlCountryRpt.DataTextField = "countryname";
                    ddlCountryRpt.DataValueField = "countryname";
                    ddlCountryRpt.DataBind();
                }

                ddlCountryRpt.SelectedValue = lblCountry.Text;
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
                ddlregrpt.Items.Insert(0, "Select");
            }

            txtCity.Items.Clear();
            DataTable dtd = d.filltable("select tc.* from [dbo].[tbl_city] tc inner join [dbo].[tblcountry] t on t.[countryid] = tc.[countryid] where tc.isactive = 1 and t.[countryname] = '" + ddlCountryRpt.SelectedValue + "' order by tc.city asc");
            if (dtd.Rows.Count > 0)
            {
                txtCity.DataTextField = "city";
                txtCity.DataValueField = "city";
                txtCity.DataSource = dtd;
                txtCity.DataBind();
            }
            txtCity.Items.Insert(0, "Select");

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
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        objBookAddDetail = new BookDeliveryAddressBAL();
        objBookAddDetail.BookDeliveryAddID = -1;
        objBookAddDetail.UserID = Convert.ToInt32(Session["UserID"]);
        objBookAddDetail.IsDefault = false;
        objBookAddDetail.Name = txtname.Text;
        objBookAddDetail.StreetAddress = txtaddress.Text;
        objBookAddDetail.Landmark = txtlandmark.Text;

        if (txtcity.Visible == true)
        {
            objBookAddDetail.City = txtcity.Text;
            DataTable dtc = d.filltable("select * from [dbo].[tblcountry] where [countryname] = '"+ ddlCountry.SelectedValue + "' ");
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

    public string getRemoveString()
    {
        //OnClientClick="return confirm('" + '<%# ResourceManager.GetString("Are you sure you want to Remove?",System.Threading.Thread.CurrentThread.CurrentCulture.Name) %>' + "'"); ";
        return "return confirm('" + Localization.ResourceManager.GetString("Are you sure you want to Remove?", System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');";
        //return "";
    }


    protected void ddlregion_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BindCountryrpt(ddlreg);
        DropDownList ddl = sender as DropDownList;
        RepeaterItem item = ddl.Parent as RepeaterItem;
        DropDownList tb = item.FindControl("ddlregrpt") as DropDownList;
        string a = tb.SelectedValue;
        // BindCountryrpt(Convert.ToInt32(a));

        TextBox txtPincode = item.FindControl("txtPincode") as TextBox;
        if (tb.SelectedItem.Text.ToUpper() == "CENTRAL AMERICA")
        {
            txtPincode.Text = "00000";
            txtPincode.Enabled = false;
        }
        else
        {
            txtPincode.Text = "";
            txtPincode.Enabled = true;
        }
        DropDownList tb1 = item.FindControl("ddlCountryRpt") as DropDownList;
        DataTable dt = d.filltable("select * from [dbo].[tblcountry] where [IsActive] = 1 and [countryid] in (select [Country_Id] from [dbo].[Region_Country] where rid = '" + a + "')  order by countryname asc");

        if (dt.Rows.Count > 0)
        {
            tb1.DataSource = dt;
            tb1.DataTextField = "countryname";
            tb1.DataValueField = "countryname";
            tb1.DataBind();
        }

        tb1.Items.Insert(0,"Select");
        fill_cou();
        DropDownList tb2 = item.FindControl("txtCity") as DropDownList;
        tb2.Items.Clear();
        DataTable dtd = d.filltable("select tc.* from [dbo].[tbl_city] tc inner join [dbo].[tblcountry] t on t.[countryid] = tc.[countryid] where tc.isactive = 1 and t.[countryname] = '" + a + "' order by tc.city asc");
        if (dtd.Rows.Count > 0)
        {
            tb2.DataTextField = "city";
            tb2.DataValueField = "id";
            tb2.DataSource = dtd;
            tb2.DataBind();
        }
        tb2.Items.Insert(0, "Select");
    }


    protected void ddlregion_SelectedIndexChanged1(object sender, EventArgs e)
    {
        
        BindCountry();
        BindData();
        fill_cou();
        if (ddlregion.SelectedItem.Text.ToUpper() == "CENTRAL AMERICA")
        {
            txtpincode.Text = "00000";
            txtpincode.Enabled = false;
        }
        else
        {
            txtpincode.Text = "";
            txtpincode.Enabled = true;
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');", true);
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fill_cou();
        BindData();
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');", true);
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

    protected void ddlCountryRpt_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddl = sender as DropDownList;
        RepeaterItem item = ddl.Parent as RepeaterItem;
        DropDownList tb = item.FindControl("ddlCountryRpt") as DropDownList;
        string a = tb.SelectedValue;

        DropDownList tb1 = item.FindControl("txtCity") as DropDownList;
        tb1.Items.Clear();
        DataTable dtd = d.filltable("select tc.* from [dbo].[tbl_city] tc inner join [dbo].[tblcountry] t on t.[countryid] = tc.[countryid] where tc.isactive = 1 and t.[countryname] = '" + a + "' order by tc.city asc");
        if (dtd.Rows.Count > 0)
        {
            tb1.DataTextField = "city";
            tb1.DataValueField = "id";
            tb1.DataSource = dtd;
            tb1.DataBind();
        }
        tb1.Items.Insert(0, "Select");
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
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "toggle_visibility('dialog');", true);
    }
}