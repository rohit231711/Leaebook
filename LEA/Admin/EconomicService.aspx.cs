using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;

public partial class Admin_EconomicService : System.Web.UI.Page
{
    dbconnection d = new dbconnection();
    service sc = new service();
    service_dal sd = new service_dal();

    static string command = "insert";
    static int id = 0;

    static string sort = "asc";
    static string sort1 = "asc";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                fillgrd();
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
        d.fillgrid("select *,'$ '+cast(price as varchar) as Price1 ,cast([From_Weight] as varchar)+' To '+cast([To_Weight] as varchar) as weightr from[dbo].[ECONOMIC_SERVICE] where [IsDelete] = 0 order by[From_Weight] asc", grd_services);
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            add.Visible = false;
            grd.Visible = true;
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (command == "insert")
            {
                DataTable dtc = d.filltable("checkentry_economic '"+txtfrwe.Text+"','"+txttowe.Text+"' ");
                if (dtc.Rows.Count == 0)
                {
                    sc.From_Weight = txtfrwe.Text;
                    sc.To_Weight = txttowe.Text;
                    sc.Price = txtprice.Text;

                    sd.insert_economic_service(sc);

                    clr();
                    add.Visible = false;
                    grd.Visible = true;

                    fillgrd();
                }
                else
                {
                    Global.Alert(this.Page, "Entry already exist.");
                }
            }

            if (command == "edit")
            {
                DataTable dtc = d.filltable("checkentry_economic1 '" + txtfrwe.Text + "','" + txttowe.Text + "','"+id+"' ");
                if (dtc.Rows.Count == 0)
                {
                    sc.From_Weight = txtfrwe.Text;
                    sc.To_Weight = txttowe.Text;
                    sc.Price = txtprice.Text;
                    sc.Id = id;

                    sd.update_economic_service(sc);

                    clr();
                    add.Visible = false;
                    grd.Visible = true;

                    fillgrd();
                }
                else
                {
                    Global.Alert(this.Page, "Entry already exist.");
                }
                
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    public void clr()
    {
        txtfrwe.Text = "";
        txttowe.Text = "";
        txtprice.Text = "";
    }

    protected void lnkPurchase_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string val1 = e.CommandName.ToString();
            id = Convert.ToInt32(val1);
            DataTable dt = d.filltable("select * from [dbo].[ECONOMIC_SERVICE] where [Id] = '" + val1 + "' ");

            txtfrwe.Text = dt.Rows[0]["From_Weight"].ToString().Replace(",",".");
            txttowe.Text = dt.Rows[0]["To_Weight"].ToString().Replace(",", ".");
            txtprice.Text = dt.Rows[0]["Price"].ToString().Replace(",", ".");

            add.Visible = true;
            grd.Visible = false;

            command = "edit";

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
            d.executedml("update [dbo].[ECONOMIC_SERVICE] set IsDelete = 1 where [Id] = '" + val1 + "' ");
            fillgrd();
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

                    d.executedml("update [dbo].[ECONOMIC_SERVICE] set IsDelete = 1 where [Id] = '" + ID + "' ");
                    fillgrd();
                }
            }
            Global.Alert(this.Page, "User has been deleted.");
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

                    d.executedml("update [dbo].[ECONOMIC_SERVICE] set IsActive = 1 where [Id] = '" + ID + "' ");
                    fillgrd();
                }
            }
            Global.Alert(this.Page, "User has been activated.");
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

                    d.executedml("update [dbo].[ECONOMIC_SERVICE] set IsActive = 0 where [Id] = '" + ID + "' ");
                    fillgrd();
                }
            }
            Global.Alert(this.Page, "User has been inactivated.");
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

    protected void lnkweight_Click(object sender, EventArgs e)
    {
        try
        {
            if (sort == "asc")
            {
                d.fillgrid("select *,'$ '+cast(price as varchar) as Price1 ,cast([From_Weight] as varchar)+' To '+cast([To_Weight] as varchar) as weightr from[dbo].[ECONOMIC_SERVICE] where [IsDelete] = 0 order by [From_Weight] desc", grd_services);
                sort = "desc";
            }
            else if(sort == "desc")
            {
                d.fillgrid("select *,'$ '+cast(price as varchar) as Price1 ,cast([From_Weight] as varchar)+' To '+cast([To_Weight] as varchar) as weightr from[dbo].[ECONOMIC_SERVICE] where [IsDelete] = 0 order by [From_Weight] asc", grd_services);
                sort = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }

    protected void lnkweight_Click1(object sender, EventArgs e)
    {
        try
        {
            if (sort1 == "asc")
            {
                d.fillgrid("select *,'$ '+cast(price as varchar) as Price1 ,cast([From_Weight] as varchar)+' To '+cast([To_Weight] as varchar) as weightr from[dbo].[ECONOMIC_SERVICE] where [IsDelete] = 0 order by Price desc", grd_services);
                sort1 = "desc";
            }
            else if (sort1 == "desc")
            {
                d.fillgrid("select *,'$ '+cast(price as varchar) as Price1 ,cast([From_Weight] as varchar)+' To '+cast([To_Weight] as varchar) as weightr from[dbo].[ECONOMIC_SERVICE] where [IsDelete] = 0 order by Price asc", grd_services);
                sort1 = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }
}