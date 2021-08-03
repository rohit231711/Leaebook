using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_city : System.Web.UI.Page
{
    dbconnection d = new dbconnection();

    static string command = "insert";
    static string sort = "asc";

    static int id = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                fillgrd();

                fill_cou();

                //this.PopulateHobbies();

                add.Visible = false;
                grd.Visible = true;
            }
            catch (Exception ex)
            {
                Global.Alert(this.Page, ex.Message);
            }
        }
    }

    //private void PopulateHobbies()
    //{
    //    DataTable dtc = d.filltable("select * from [dbo].[tblcountry] where IsActive = 1 and [countryid] not in (select [Country_Id] from [dbo].[Region_Country]) order by countryname asc");
    //    if (dtc.Rows.Count > 0)
    //    {
    //        for (int i = 0; dtc.Rows.Count > i; i++)
    //        {
    //            ListItem item = new ListItem();
    //            item.Text = dtc.Rows[i]["countryname"].ToString();
    //            item.Value = dtc.Rows[i]["countryid"].ToString();
    //            chkHobbies.Items.Add(item);
    //        }
    //    }

    //    chkHobbies.Attributes.Add("class", "");
    //}

    //private void PopulateHobbies_data(int id)
    //{
    //    chkHobbies.Items.Clear();

    //    DataTable dtc = d.filltable("region_getregiondata '" + id + "' ");
    //    if (dtc.Rows.Count > 0)
    //    {
    //        for (int i = 0; dtc.Rows.Count > i; i++)
    //        {
    //            ListItem item = new ListItem();
    //            item.Text = dtc.Rows[i]["countryname"].ToString();
    //            item.Value = dtc.Rows[i]["countryid"].ToString();

    //            if (dtc.Rows[i]["status1"].ToString() == "0")
    //            { item.Selected = Convert.ToBoolean(0); }
    //            else
    //            { item.Selected = Convert.ToBoolean(1); }

    //            chkHobbies.Items.Add(item);
    //        }
    //    }
    //}

    public void fillgrd()
    {
        d.fillgrid("citylist", grd_services);
    }

    public void fill_cou()
    {
      DataTable dtd =  d.filltable("select *  from [dbo].[tblcountry] where isactive = 1 order by countryname asc");
        if (dtd.Rows.Count > 0)
        {
            ddlcou.DataTextField = "countryname";
            ddlcou.DataValueField = "countryid";
            ddlcou.DataSource = dtd;
            ddlcou.DataBind();
        }
        
    }

    protected void lkbAdd_Click(object sender, EventArgs e)
    {
        try
        {
            command = "insert";
            add.Visible = true;
            grd.Visible = false;
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void lkbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in this.grd_services.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
                {
                    var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);

                    d.executedml("update [dbo].[tbl_city] set IsDelete = 1 where [Id] = '" + ID + "'   ");
                    fillgrd();
                }
            }

            Global.Alert(this.Page, "City has been deleted.");
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in this.grd_services.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
                {
                    var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);

                    d.executedml("update [dbo].[tbl_city]  set IsActive = 1 where [Id] = '" + ID + "' ");
                    fillgrd();
                }
            }
            Global.Alert(this.Page, "City has been activated.");
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void btnInactive_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in this.grd_services.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
                {
                    var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);

                    d.executedml("update [dbo].[tbl_city]  set IsActive = 0 where [Id] = '" + ID + "' ");
                    fillgrd();
                }
            }
            Global.Alert(this.Page, "City has been inactivated.");
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            txtcity.Text = "";
            ddlcou.SelectedIndex = -1;
           // PopulateHobbies();

            add.Visible = false;
            grd.Visible = true;
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void grd_services_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_services.PageIndex = e.NewPageIndex;
        fillgrd();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            if (command == "insert")
            {
                #region
                DataTable dt = d.filltable("[dbo].[Insert_city] '" + txtcity.Text.Trim() + "','"+ddlcou.SelectedValue+"' ");

                add.Visible = false;
                grd.Visible = true;

                txtcity.Text = "";
                ddlcou.SelectedIndex = -1;

              //  PopulateHobbies();

                fillgrd();

                Global.Alert(this.Page, "Insert Successfully.");
                #endregion
            }

            if (command == "edit")
            {
                #region
                d.executedml("update [dbo].[tbl_city] set [city] = '" + txtcity.Text.Trim() + "',[countryid] = '"+ddlcou.SelectedValue+ "' ,[modifydate] = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' where id = '" + id + "'  ");

                int rid = Convert.ToInt32(id);

                add.Visible = false;
                grd.Visible = true;

                txtcity.Text = "";
                ddlcou.SelectedIndex = -1;

               // PopulateHobbies();

                fillgrd();

                Global.Alert(this.Page, "Update Successfully.");
                #endregion
            }

        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void lnkdel_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string val1 = e.CommandName.ToString();
            id = Convert.ToInt32(val1);
            d.executedml("update [dbo].[tbl_city] set IsDelete = 1 where [Id] = '" + val1 + "' ");
            fillgrd();
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void lnkPurchase_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string val1 = e.CommandName.ToString();
            id = Convert.ToInt32(val1);
            DataTable dt = d.filltable("select * from [dbo].[tbl_city] where [Id] = '" + val1 + "' ");

            txtcity.Text = dt.Rows[0]["city"].ToString();
            ddlcou.SelectedValue = dt.Rows[0]["countryid"].ToString();

            //PopulateHobbies_data(id);

            add.Visible = true;
            grd.Visible = false;

            command = "edit";

        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void lnkreg_Click(object sender, EventArgs e)
    {
        try
        {
            if (sort == "asc")
            {
                d.fillgrid("select    r.* ,tc.countryname,(select [Region] from [dbo].[Region] where id = (select rid from [dbo].[Region_Country] where [Country_Id] = r.[CountryId])) as region from [dbo].tbl_city r inner join [dbo].[tblcountry] tc on tc.countryid = r.id where r.IsDelete = 0 order by region desc", grd_services);
                sort = "desc";
            }
            else if (sort == "desc")
            {
                d.fillgrid("select    r.* ,tc.countryname,(select [Region] from [dbo].[Region] where id = (select rid from [dbo].[Region_Country] where [Country_Id] = r.[CountryId])) as region from [dbo].tbl_city r inner join [dbo].[tblcountry] tc on tc.countryid = r.id where r.IsDelete = 0 order by region asc", grd_services);
                sort = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void lnkcou_Click(object sender, EventArgs e)
    {
        try
        {
            if (sort == "asc")
            {
                d.fillgrid("select    r.* ,tc.countryname,(select [Region] from [dbo].[Region] where id = (select rid from [dbo].[Region_Country] where [Country_Id] = r.[CountryId])) as region from [dbo].tbl_city r inner join [dbo].[tblcountry] tc on tc.countryid = r.id where r.IsDelete = 0 order by countryname desc", grd_services);
                sort = "desc";
            }
            else if (sort == "desc")
            {
                d.fillgrid("select    r.* ,tc.countryname,(select [Region] from [dbo].[Region] where id = (select rid from [dbo].[Region_Country] where [Country_Id] = r.[CountryId])) as region from [dbo].tbl_city r inner join [dbo].[tblcountry] tc on tc.countryid = r.id where r.IsDelete = 0 order by countryname asc", grd_services);
                sort = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void lnkcou_Click1(object sender, EventArgs e)
    {
        try
        {
            if (sort == "asc")
            {
                d.fillgrid("select    r.* ,tc.countryname,(select [Region] from [dbo].[Region] where id = (select rid from [dbo].[Region_Country] where [Country_Id] = r.[CountryId])) as region from [dbo].tbl_city r inner join [dbo].[tblcountry] tc on tc.countryid = r.id where r.IsDelete = 0 order by city desc", grd_services);
                sort = "desc";
            }
            else if (sort == "desc")
            {
                d.fillgrid("select    r.* ,tc.countryname,(select [Region] from [dbo].[Region] where id = (select rid from [dbo].[Region_Country] where [Country_Id] = r.[CountryId])) as region from [dbo].tbl_city r inner join [dbo].[tblcountry] tc on tc.countryid = r.id where r.IsDelete = 0 order by city asc", grd_services);
                sort = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }
}