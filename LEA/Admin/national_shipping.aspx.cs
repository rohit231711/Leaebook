using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_national_shipping : System.Web.UI.Page
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

   

    public void fillgrd()
    {
        d.fillgrid("select * from National_Shipping_Cost where isactive = 1", grd_services);
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

    //protected void lkbDelete_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        foreach (GridViewRow gvr in this.grd_services.Rows)
    //        {
    //            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
    //            {
    //                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);

    //                d.executedml("update [dbo].[Region] set IsDelete = 1 where [Id] = '" + ID + "' ;  delete from [dbo].[Region_Country] where rid = '" + ID + "'  ");
    //                fillgrd();
    //            }
    //        }

    //        Global.Alert(this.Page, "Region has been deleted.");
    //    }
    //    catch (Exception ex)
    //    {
    //        Global.Alert(this.Page, ex.Message);
    //    }
    //}

    //protected void btnActive_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        foreach (GridViewRow gvr in this.grd_services.Rows)
    //        {
    //            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
    //            {
    //                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);

    //                d.executedml("update [dbo].[Region]  set IsActive = 1 where [Id] = '" + ID + "' ");
    //                fillgrd();
    //            }
    //        }
    //        Global.Alert(this.Page, "Region has been activated.");
    //    }
    //    catch (Exception ex)
    //    {
    //        Global.Alert(this.Page, ex.Message);
    //    }
    //}

    //protected void btnInactive_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        foreach (GridViewRow gvr in this.grd_services.Rows)
    //        {
    //            if (((CheckBox)gvr.FindControl("chkMember")).Checked == true)
    //            {
    //                var ID = Convert.ToInt32(((HiddenField)gvr.FindControl("hfRegistrationID")).Value);

    //                d.executedml("update [dbo].[Region]  set IsActive = 0 where [Id] = '" + ID + "' ");
    //                fillgrd();
    //            }
    //        }
    //        Global.Alert(this.Page, "Region has been inactivated.");
    //    }
    //    catch (Exception ex)
    //    {
    //        Global.Alert(this.Page, ex.Message);
    //    }
    //}

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            txtcity.Text = "";
            txtship.Text = "";

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
        string strQuery = "update National_Shipping_Cost  set [shipping_cost] = '" + txtship.Text.Replace(",", ".") + "' ,[modifydate] = '" + System.DateTime.Now.ToString("yyyy-MM-dd") + "' where id = '" + id + "'";
        try
        {
            if (command == "insert")
            {
                #region
                //DataTable dt = d.filltable("[dbo].[InsertRegions] '" + txtregion.Text.Trim() + "' ");
                //if (dt.Rows.Count > 0)
                //{
                //    int rid = Convert.ToInt32(dt.Rows[0][0].ToString());

                //    foreach (ListItem item in chkHobbies.Items)
                //    {
                //        if (item.Selected == true)
                //        {
                //            d.executedml("insert into [dbo].[Region_Country] values ('" + rid + "','" + item.Value + "')");
                //        }
                //    }
                //}

                //add.Visible = false;
                //grd.Visible = true;

                //txtregion.Text = "";
                //chkHobbies.Items.Clear();

                //PopulateHobbies();

                //fillgrd();

                //Global.Alert(this.Page, "Insert Successfully.");
                #endregion
            }

            if (command == "edit")
            {
                #region

                d.executedml(strQuery);
                
                add.Visible = false;
                grd.Visible = true;

                txtcity.Text = "";
                txtship.Text = "";
                
                fillgrd();

                Global.Alert(this.Page, "Update Successfully.");
                #endregion
            }

        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, strQuery);
        }
    }

    //protected void lnkdel_Command(object sender, CommandEventArgs e)
    //{
    //    try
    //    {
    //        string val1 = e.CommandName.ToString();
    //        id = Convert.ToInt32(val1);
    //        d.executedml("update [dbo].[Region] set IsDelete = 1 where [Id] = '" + val1 + "' ");
    //        fillgrd();
    //    }
    //    catch (Exception ex)
    //    {
    //        Global.Alert(this.Page, ex.Message);
    //    }
    //}

    protected void lnkPurchase_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string val1 = e.CommandName.ToString();
            id = Convert.ToInt32(val1);
            DataTable dt = d.filltable("select * from National_Shipping_Cost where [Id] = '" + val1 + "' ");

            txtship.Text = dt.Rows[0]["shipping_cost"].ToString().Replace(",", ".");
            txtcity.Text = dt.Rows[0]["city"].ToString();

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
                d.fillgrid("select * from National_Shipping_Cost where isactive = 1 order by city desc", grd_services);
                sort = "desc";
            }
            else if (sort == "desc")
            {
                d.fillgrid("select * from National_Shipping_Cost where isactive = 1 order by city asc", grd_services);
                sort = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void grd_services_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var txt = e.Row.FindControl("lblscost") as Label;
            txt.Text = txt.Text.Replace(",", ".");

        }
    }
}