using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddCountry : System.Web.UI.Page
{
    Country objCountry = new Country();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["CountryID"] != null)
            {
                hdnBookID.Value = Request.QueryString["CountryID"].ToString();
                DataTable dt = objCountry.CountryGetDetailByID(hdnBookID.Value);
                if(dt.Rows.Count > 0)
                {
                    txtCode.Text = dt.Rows[0]["ISOCode"].ToString();
                    txtName.Text = dt.Rows[0]["countryname"].ToString();
                    chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                }
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(hdnBookID.Value))
        {
            try
            {
                objCountry.Country_Update(hdnBookID.Value, txtCode.Text.Trim(), txtName.Text.Trim(), chkActive.Checked);
                Response.Redirect("ManageCountry.aspx");
            }
            catch(Exception ex)
            {

            }
        }
        else
        {
            try
            {
                objCountry.Country_Insert(txtCode.Text.Trim(), txtName.Text.Trim(), chkActive.Checked);
                Response.Redirect("ManageCountry.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Please fill all required fields');", true);                
            }
        }
    }
}