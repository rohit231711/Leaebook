using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DHLQuoteSettings : System.Web.UI.Page
{
    DHLQuoteSettingBAL SSB = new DHLQuoteSettingBAL();
    DataTable DT = new DataTable();
    MenuBAL objmenu = new MenuBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSiteID.Focus();
            this.Form.DefaultButton = btnSubmit.UniqueID;
            //accessrights();
            DT = SSB.GetAllDHLQuoteSetting();

            //Global.Alert(this, "Updated Successfully");

            if (DT != null && DT.Rows.Count > 0)
            {
                ViewState["ShippingSettingID"] = Convert.ToInt32(DT.Rows[0]["ID"]);
                txtSiteID.Text = DT.Rows[0]["SiteID"].ToString();
                txtPassword.Text = DT.Rows[0]["Password"].ToString();
                txtSiteID.Text = DT.Rows[0]["SiteID"].ToString();
                txtPassword.Text = DT.Rows[0]["Password"].ToString();
                txtFromCountryCode.Text = DT.Rows[0]["FromCountryCode"].ToString();
                
                rblCode.SelectedValue = DT.Rows[0]["FromCodeCity"].ToString();
                txtFromCodeCity.Text = DT.Rows[0]["FromPostalcode"].ToString();
                txtFromCodeCity.Text = DT.Rows[0]["FromCity"].ToString();
                
                txtPaymentCountryCode.Text = DT.Rows[0]["PaymentCountryCode"].ToString();
                txtReadyTime.Text = DT.Rows[0]["ReadyTime"].ToString();
                txtReadyTimeGMTOffset.Text = DT.Rows[0]["ReadyTimeGMTOffset"].ToString();
                txtDimensionUnit.Text = DT.Rows[0]["DimensionUnit"].ToString();
                txtWeightUnit.Text = DT.Rows[0]["WeightUnit"].ToString();
                txtPaymentAccountNumber.Text = DT.Rows[0]["PaymentAccountNumber"].ToString();
                txtIsDutiable.Text = DT.Rows[0]["IsDutiable"].ToString();
                txtNetworkTypeCode.Text = DT.Rows[0]["NetworkTypeCode"].ToString();
                txtGlobalProductCode.Text = DT.Rows[0]["GlobalProductCode"].ToString();
                txtLocalProductCode.Text = DT.Rows[0]["LocalProductCode"].ToString();
                txtSpecialServiceType.Text = DT.Rows[0]["SpecialServiceType"].ToString();
                txtDeclaredCurrency.Text = DT.Rows[0]["DeclaredCurrency"].ToString();
                txtDeclaredValue.Text = DT.Rows[0]["DeclaredValue"].ToString();

            }

            if (Request.QueryString["Edit"] != null && Request.QueryString["Edit"].ToString() != "")
            {
                Global.AlertNew(this, "Updated Successfully");
            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["ShippingSettingID"] != null)
        {
            SSB.ID = Convert.ToInt32(ViewState["ShippingSettingID"]);

            SSB.SiteID = txtSiteID.Text;
            SSB.Password = txtPassword.Text;
            SSB.FromCountryCode = txtFromCountryCode.Text;
            SSB.FromCodeCity = Convert.ToInt32(rblCode.SelectedItem.Value);
            
            if (Convert.ToInt32(rblCode.SelectedItem.Value) == 1)
                SSB.FromPostalcode = txtFromCodeCity.Text;
            else
                SSB.FromCity = txtFromCodeCity.Text;

            SSB.PaymentCountryCode = txtPaymentCountryCode.Text;
            SSB.ReadyTime = txtReadyTime.Text;
            SSB.ReadyTimeGMTOffset = txtReadyTimeGMTOffset.Text;
            SSB.DimensionUnit = txtDimensionUnit.Text;
            SSB.WeightUnit = txtWeightUnit.Text;
            SSB.PaymentAccountNumber = txtPaymentAccountNumber.Text;
            SSB.IsDutiable = txtIsDutiable.Text;
            SSB.NetworkTypeCode = txtNetworkTypeCode.Text;
            SSB.GlobalProductCode = txtGlobalProductCode.Text;
            SSB.LocalProductCode = txtLocalProductCode.Text;
            SSB.SpecialServiceType = txtSpecialServiceType.Text;
            SSB.DeclaredCurrency = txtDeclaredCurrency.Text;
            SSB.DeclaredValue = txtDeclaredValue.Text;


            SSB.UpdateDHLQuoteSetting();
        }
        // Global.Alert(this, "Updated Successfully");

        Response.Redirect("DHLQuoteSettings.aspx?Edit=true");

    }

}