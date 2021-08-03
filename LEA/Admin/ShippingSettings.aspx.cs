using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ShippingSettings : System.Web.UI.Page
{
    ShippingSettingsBAL SSB = new ShippingSettingsBAL();
    DataTable DT = new DataTable();
    MenuBAL objmenu = new MenuBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSiteID.Focus();
            this.Form.DefaultButton = btnSubmit.UniqueID;
            //accessrights();
            DT = SSB.GetAllShippingseetings();

            //Global.Alert(this, "Updated Successfully");

            if (DT != null && DT.Rows.Count > 0)
            {
                ViewState["ShippingSettingID"] = Convert.ToInt32(DT.Rows[0]["ID"]);
                txtSiteID.Text = DT.Rows[0]["SiteID"].ToString();
                txtPassword.Text = DT.Rows[0]["Password"].ToString();
                txtShipperAccountNumber.Text = DT.Rows[0]["ShipperAccountNumber"].ToString();
                txtBillingAccountNumber.Text = DT.Rows[0]["BillingAccountNumber"].ToString();
                txtDutyAccountNumber.Text = DT.Rows[0]["DutyAccountNumber"].ToString();
                txtCompanyName.Text = DT.Rows[0]["CompanyName"].ToString();
                txtAddressLine1.Text = DT.Rows[0]["AddressLine1"].ToString();
                txtAddressLine2.Text = DT.Rows[0]["AddressLine2"].ToString();
                txtAddressLine3.Text = DT.Rows[0]["AddressLine3"].ToString();
                txtCity.Text = DT.Rows[0]["City"].ToString();
                txtPostalCode.Text = DT.Rows[0]["PostalCode"].ToString();
                txtCountryCode.Text = DT.Rows[0]["CountryCode"].ToString();
                txtCountryName.Text = DT.Rows[0]["CountryName"].ToString();
                txtPersonName.Text = DT.Rows[0]["PersonName"].ToString();
                txtPhoneNumber.Text = DT.Rows[0]["PhoneNumber"].ToString();
                txtPhoneExtension.Text = DT.Rows[0]["PhoneExtension"].ToString();
                txtFaxNumber.Text = DT.Rows[0]["FaxNumber"].ToString();
                txtTelex.Text = DT.Rows[0]["Telex"].ToString();
                txtEmail.Text = DT.Rows[0]["Email"].ToString();
                txtShipperID.Text = DT.Rows[0]["ShipperID"].ToString();
                txtShipperCompanyName.Text = DT.Rows[0]["ShipperCompanyName"].ToString();
                txtShipperRegisteredAccount.Text = DT.Rows[0]["ShipperRegisteredAccount"].ToString();
                txtShipperAddressLine1.Text = DT.Rows[0]["ShipperAddressLine1"].ToString();
                txtShipperAddressLine2.Text = DT.Rows[0]["ShipperAddressLine2"].ToString();
                txtShipperCity.Text = DT.Rows[0]["ShipperCity"].ToString();
                txtShipperPostalCode.Text = DT.Rows[0]["ShipperPostalCode"].ToString();
                txtShipperCountryCode.Text = DT.Rows[0]["ShipperCountryCode"].ToString();
                txtShipperCountryName.Text = DT.Rows[0]["ShipperCountryName"].ToString();
                txtShipperPersonName.Text = DT.Rows[0]["ShipperPersonName"].ToString();
                txtShipperPhoneNumber.Text = DT.Rows[0]["ShipperPhoneNumber"].ToString();
                txtShipperPhoneExtension.Text = DT.Rows[0]["ShipperPhoneExtension"].ToString();
                txtShipperFaxNumber.Text = DT.Rows[0]["ShipperFaxNumber"].ToString();
                txtShipperTelex.Text = DT.Rows[0]["ShipperTelex"].ToString();
                txtShipperEmail.Text = DT.Rows[0]["ShipperEmail"].ToString();

            }

            if (Request.QueryString["Edit"] != null && Request.QueryString["Edit"].ToString() != "")
            {
                Global.AlertNew(this, "Updated Successfully");
            }
        }

    }

    //private void accessrights()
    //{
    //    int fl = 0;
    //    objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
    //    DT = objmenu.GetRightsByUser();
    //    if (DT.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < DT.Rows.Count; i++)
    //        {
    //            if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 8)
    //            {
    //                if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 1)
    //                {
    //                    fl = 1;
    //                }
    //            }
    //        }
    //        if (fl == 0)
    //        {
    //            Response.Redirect("accessdenied.aspx");
    //        }
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ViewState["ShippingSettingID"] != null)
        {
            SSB.ID = Convert.ToInt32(ViewState["ShippingSettingID"]);
            SSB.SiteID = txtSiteID.Text;
            SSB.Password = txtPassword.Text;
            SSB.ShipperAccountNumber = txtShipperAccountNumber.Text;
            SSB.BillingAccountNumber = txtBillingAccountNumber.Text;
            SSB.DutyAccountNumber = txtDutyAccountNumber.Text;
            SSB.CompanyName = txtCompanyName.Text;
            SSB.AddressLine1 = txtAddressLine1.Text;
            SSB.AddressLine2 = txtAddressLine2.Text;
            SSB.AddressLine3 = txtAddressLine3.Text;
            SSB.City = txtCity.Text;
            SSB.PostalCode = txtPostalCode.Text;
            SSB.CountryCode = txtCountryCode.Text;
            SSB.CountryName = txtCountryName.Text;
            SSB.PersonName = txtPersonName.Text;
            SSB.PhoneNumber = txtPhoneNumber.Text;
            SSB.PhoneExtension = txtPhoneExtension.Text;
            SSB.FaxNumber = txtFaxNumber.Text;
            SSB.Telex = txtTelex.Text;
            SSB.Email = txtEmail.Text;
            SSB.ShipperID = txtShipperID.Text;
            SSB.ShipperCompanyName = txtShipperCompanyName.Text;
            SSB.ShipperRegisteredAccount = txtShipperRegisteredAccount.Text;
            SSB.ShipperAddressLine1 = txtShipperAddressLine1.Text;
            SSB.ShipperAddressLine2 = txtShipperAddressLine2.Text;
            SSB.ShipperCity = txtShipperCity.Text;
            SSB.ShipperPostalCode = txtShipperPostalCode.Text;
            SSB.ShipperCountryCode = txtShipperCountryCode.Text;
            SSB.ShipperCountryName = txtShipperCountryName.Text;
            SSB.ShipperPersonName = txtShipperPersonName.Text;
            SSB.ShipperPhoneNumber = txtShipperPhoneNumber.Text;
            SSB.ShipperPhoneExtension = txtShipperPhoneExtension.Text;
            SSB.ShipperFaxNumber = txtShipperFaxNumber.Text;
            SSB.ShipperTelex = txtShipperTelex.Text;
            SSB.ShipperEmail = txtShipperEmail.Text;

            SSB.UpdateShippingsettings();
        }
        // Global.Alert(this, "Updated Successfully");

        Response.Redirect("ShippingSettings.aspx?Edit=true");

    }

}