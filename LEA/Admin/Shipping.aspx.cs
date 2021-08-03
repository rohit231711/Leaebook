using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Threading;

public partial class Admin_Shipping : System.Web.UI.Page
{
    ShippingBAL objShip = new ShippingBAL();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(Request.QueryString["ID"] != null && Request.QueryString["EDIT"] != null)
            {
                if(Convert.ToBoolean(Request.QueryString["EDIT"]))
                {
                    objShip.ShipperID = Convert.ToInt32(Request.QueryString["ID"]);
                    dt = objShip.getByPK();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        hfID.Value = dt.Rows[0]["ShipperID"].ToString();
                        txtName.Text = dt.Rows[0]["ShipperName"].ToString();
                        txtAmount.Text = dt.Rows[0]["ShippingCharge"].ToString();
                    }
                }
            }
            
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        var id = 0;
        if(!string.IsNullOrEmpty(hfID.Value))
        {
            objShip.ShipperID = Convert.ToInt64(hfID.Value);
            objShip.ShipperName = txtName.Text;
            objShip.ShippingCharge = txtAmount.Text;
            id = objShip.InsertShippingCharge();
        }
        else
        {
            objShip.ShipperName = txtName.Text;
            objShip.ShippingCharge = txtAmount.Text;
            id = objShip.InsertShippingCharge();
        }
        if(id > 0)
        {
            //Error.Visible = true;
            //ErrorMessage.InnerText = "Information updated sucessfully.";
            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Information updated sucessfully.');", true);
            Response.Redirect("ManageShipping.aspx");
        }
    }
}