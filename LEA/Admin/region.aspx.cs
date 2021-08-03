using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_region : System.Web.UI.Page
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

                this.PopulateHobbies();

                add.Visible = false;
                grd.Visible = true;
            }
            catch (Exception ex)
            {
                Global.Alert(this.Page, ex.Message);
            }
        }
    }

    private void PopulateHobbies()
    {
        DataTable dtc = d.filltable("select * from [dbo].[tblcountry] where IsActive = 1 and [countryid] not in (select [Country_Id] from [dbo].[Region_Country]) order by countryname asc");
        if (dtc.Rows.Count > 0)
        {
            for (int i = 0; dtc.Rows.Count > i; i++)
            {
                ListItem item = new ListItem();
                item.Text = dtc.Rows[i]["countryname"].ToString();
                item.Value = dtc.Rows[i]["countryid"].ToString();
                chkHobbies.Items.Add(item);
            }
        }

        chkHobbies.Attributes.Add("class","");
    }

    private void PopulateHobbies_data(int id)
    {
        chkHobbies.Items.Clear();

        DataTable dtc = d.filltable("region_getregiondata '"+id+"' ");
        if (dtc.Rows.Count > 0)
        {
            for (int i = 0; dtc.Rows.Count > i; i++)
            {
                ListItem item = new ListItem();
                item.Text = dtc.Rows[i]["countryname"].ToString();
                item.Value = dtc.Rows[i]["countryid"].ToString();

                if (dtc.Rows[i]["status1"].ToString() == "0")
                { item.Selected = Convert.ToBoolean(0); }
                else
                { item.Selected = Convert.ToBoolean(1); }

                chkHobbies.Items.Add(item);
            }
        }
    }

    public void fillgrd()
    {
        d.fillgrid("regionlist", grd_services);
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

                    d.executedml("update [dbo].[Region] set IsDelete = 1 where [Id] = '" + ID + "' ;  delete from [dbo].[Region_Country] where rid = '" + ID + "'  ");
                    fillgrd();
                }
            }

            Global.Alert(this.Page, "Region has been deleted.");
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

                    d.executedml("update [dbo].[Region]  set IsActive = 1 where [Id] = '" + ID + "' ");
                    fillgrd();
                }
            }
            Global.Alert(this.Page, "Region has been activated.");
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

                    d.executedml("update [dbo].[Region]  set IsActive = 0 where [Id] = '" + ID + "' ");
                    fillgrd();
                }
            }
            Global.Alert(this.Page, "Region has been inactivated.");
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
            txtregion.Text = "";
            chkHobbies.Items.Clear();

            PopulateHobbies();

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
                DataTable dt = d.filltable("[dbo].[InsertRegions] '"+txtregion.Text.Trim()+"' ");
                if (dt.Rows.Count > 0)
                {
                    int rid = Convert.ToInt32(dt.Rows[0][0].ToString());

                    foreach (ListItem item in chkHobbies.Items)
                    {
                        if (item.Selected == true)
                        {
                            d.executedml("insert into [dbo].[Region_Country] values ('"+rid+"','"+ item.Value + "')");
                        }
                    }
                }

                add.Visible = false;
                grd.Visible = true;

                txtregion.Text = "";
                chkHobbies.Items.Clear();

                PopulateHobbies();

                fillgrd();

                Global.Alert(this.Page, "Insert Successfully.");
                #endregion
            }

            if (command == "edit")
            {
                #region
                d.executedml("update [dbo].[Region] set [Region] = '" + txtregion.Text.Trim() + "' ,[Modify_date] = '"+System.DateTime.Now.ToString("yyyy-MM-dd")+"' where id = '"+id+ "' ; delete from [dbo].[Region_Country] where rid = '"+id+"' ");

                int rid = Convert.ToInt32(id);

                foreach (ListItem item in chkHobbies.Items)
                {
                    if (item.Selected == true)
                    {
                        d.executedml("insert into [dbo].[Region_Country] values ('" + rid + "','" + item.Value + "')");
                    }
                }

                add.Visible = false;
                grd.Visible = true;

                txtregion.Text = "";
                chkHobbies.Items.Clear();

                PopulateHobbies();

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
            d.executedml("update [dbo].[Region] set IsDelete = 1 where [Id] = '" + val1 + "' ");
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
            DataTable dt = d.filltable("select * from [dbo].[Region] where [Id] = '" + val1 + "' ");

            txtregion.Text = dt.Rows[0]["Region"].ToString();

            PopulateHobbies_data(id);

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
                d.fillgrid("select   r.* ,(select count(id) from[dbo].[Region_Country] where Rid = r.Id) as Total from [dbo].[Region] r where r.IsDelete = 0 order by Region desc", grd_services);
                sort = "desc";
            }
            else if (sort == "desc")
            {
                d.fillgrid("select   r.* ,(select count(id) from[dbo].[Region_Country] where Rid = r.Id) as Total from [dbo].[Region] r where r.IsDelete = 0 order by Region asc", grd_services);
                sort = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }
}