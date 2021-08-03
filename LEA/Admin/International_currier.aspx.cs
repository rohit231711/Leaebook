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

public partial class Admin_International_currier : System.Web.UI.Page
{
    dbconnection d = new dbconnection();
    service sc = new service();
    service_dal sd = new service_dal();

    static string command = "insert";
    static int id = 0;

    static string sort = "asc";
    static string sort1 = "asc";
    static string sort2 = "asc";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                fillgrd();
                DataTable dt = d.filltable("select [Region], [id] from [dbo].[Region] where [IsActive] = 1 and isdelete = 0 order by region");
                ddlcou.DataSource = dt;
                ddlcou.DataTextField = "Region";
                ddlcou.DataValueField = "id";
                ddlcou.DataBind();
                ddlcou.Items.Insert(0,"Select");

                GenerateNumbers();

                add.Visible = false;
                grd.Visible = true;
            }
            catch (Exception ex)
            {
                Global.Alert(this.Page, ex.Message);
            }
        }
    }

    private void GenerateNumbers()
    {
        for (double i = 0.5; i <= Convert.ToDouble(30.5);)
        {
            ListItem li = new ListItem();

            if (i == Convert.ToDouble(30.5))
            {
                li.Text = "30 +";
                li.Value = "30 +";
            }
            else
            {
                li.Text = i.ToString().Replace(",", ".");
                li.Value = i.ToString().Replace(",", ".");
            }

            txtfrwe.Items.Add(li);
            i = i + 0.5;
        }
    }

    public void clr()
    {
        txtfrwe.SelectedIndex = -1;
        ddlcou.SelectedIndex = -1;
      //  txttowe.Text = "";
        txtprice.Text = "";
    }

    public void fillgrd()
    {
        d.fillgrid("select [dbo].[International_currier_SERVICE].*,'$ '+cast(price as varchar) as Price1 ,[From_Weight]  as weightr,c.[Region] as cou from [dbo].[International_currier_SERVICE] inner join  [dbo].[Region] c on c.id = [dbo].[International_currier_SERVICE].[Region_Id] where [dbo].[International_currier_SERVICE].[IsDelete] = 0 and c.[IsActive] = 1 order by c.[Region],[From_Weight] asc", grd_services);
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var txt = e.Row.FindControl("lblweight") as Label;
            txt.Text = txt.Text.Replace(",", ".");

            var txt1 = e.Row.FindControl("lblprice") as Label;
            txt1.Text = txt1.Text.Replace(",", ".");

        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (command == "insert")
            {
                DataTable dtc = d.filltable("checkentry_int_currier '"+Convert.ToInt32(ddlcou.SelectedValue)+"' , '"+txtfrwe.SelectedValue+"' ");
                if (dtc.Rows.Count == 0)
                {
                    sc.From_Weight = txtfrwe.SelectedValue.Replace(",", ".");
                    //sc.To_Weight = txttowe.Text;
                    sc.Price = txtprice.Text;
                    sc.coid = Convert.ToInt32(ddlcou.SelectedValue.ToString());

                    sd.insert_International_currier_service(sc);

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
                DataTable dtc = d.filltable("checkentry_int_currier1 '" + Convert.ToInt32(ddlcou.SelectedValue) + "','"+ id + "','"+txtfrwe.SelectedValue+"' ");
                if (dtc.Rows.Count == 0)
                {
                    sc.From_Weight = txtfrwe.SelectedValue.Replace(",", ".");
                   // sc.To_Weight = txttowe.Text;
                    sc.Price = txtprice.Text;
                    sc.coid = Convert.ToInt32(ddlcou.SelectedValue.ToString());
                    sc.Id = id;

                    sd.update_International_currier_service(sc);

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

    protected void lnkPurchase_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string val1 = e.CommandName.ToString();
            id = Convert.ToInt32(val1);
            DataTable dt = d.filltable("select * from [dbo].[International_currier_SERVICE] where [Id] = '" + val1 + "' ");

            txtfrwe.SelectedValue = Math.Round(Convert.ToDouble(dt.Rows[0]["From_Weight"].ToString()), 1).ToString().Replace(",", ".");
            txtprice.Text = dt.Rows[0]["Price"].ToString().Replace(",", ".");
            ddlcou.SelectedValue = dt.Rows[0]["Region_Id"].ToString();

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
            d.executedml("update [dbo].[International_currier_SERVICE] set IsDelete = 1 where [Id] = '" + val1 + "' ");
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

                    d.executedml("update [dbo].[International_currier_SERVICE] set IsDelete = 1 where [Id] = '" + ID + "' ");
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

                    d.executedml("update [dbo].[International_currier_SERVICE] set IsActive = 1 where [Id] = '" + ID + "' ");
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

                    d.executedml("update [dbo].[International_currier_SERVICE] set IsActive = 0 where [Id] = '" + ID + "' ");
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
                d.fillgrid("select [dbo].[International_currier_SERVICE].*,'$ '+cast(price as varchar) as Price1 ,[From_Weight]  as weightr,c.[Region] as cou from [dbo].[International_currier_SERVICE] inner join  [dbo].[Region] c on c.id = [dbo].[International_currier_SERVICE].[Region_Id] where [dbo].[International_currier_SERVICE].[IsDelete] = 0 and c.[IsActive] = 1  order by [From_Weight] desc", grd_services);
                sort = "desc";
            }
            else if (sort == "desc")
            {
                d.fillgrid("select [dbo].[International_currier_SERVICE].*,'$ '+cast(price as varchar) as Price1 ,[From_Weight]  as weightr,c.[Region] as cou from [dbo].[International_currier_SERVICE] inner join  [dbo].[Region] c on c.id = [dbo].[International_currier_SERVICE].[Region_Id] where [dbo].[International_currier_SERVICE].[IsDelete] = 0 and c.[IsActive] = 1 order by [From_Weight] asc", grd_services);
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
                d.fillgrid("select [dbo].[International_currier_SERVICE].*,'$ '+cast(price as varchar) as Price1 ,[From_Weight]  as weightr,c.[Region] as cou from [dbo].[International_currier_SERVICE] inner join  [dbo].[Region] c on c.id = [dbo].[International_currier_SERVICE].[Region_Id] where [dbo].[International_currier_SERVICE].[IsDelete] = 0 and c.[IsActive] = 1  order by Price desc", grd_services);
                sort1 = "desc";
            }
            else if (sort1 == "desc")
            {
                d.fillgrid("select [dbo].[International_currier_SERVICE].*,'$ '+cast(price as varchar) as Price1 ,[From_Weight]  as weightr,c.[Region] as cou from [dbo].[International_currier_SERVICE] inner join  [dbo].[Region] c on c.id = [dbo].[International_currier_SERVICE].[Region_Id] where [dbo].[International_currier_SERVICE].[IsDelete] = 0 and c.[IsActive] = 1  order by Price asc", grd_services);
                sort1 = "asc";
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
            if (sort1 == "asc")
            {
                d.fillgrid("select [dbo].[International_currier_SERVICE].*,'$ '+cast(price as varchar) as Price1 ,[From_Weight]  as weightr,c.[Region] as cou from [dbo].[International_currier_SERVICE] inner join  [dbo].[Region] c on c.id = [dbo].[International_currier_SERVICE].[Region_Id] where [dbo].[International_currier_SERVICE].[IsDelete] = 0 and c.[IsActive] = 1  order by  c.[Region] desc", grd_services);
                sort1 = "desc";
            }
            else if (sort1 == "desc")
            {
                d.fillgrid("select [dbo].[International_currier_SERVICE].*,'$ '+cast(price as varchar) as Price1 ,[From_Weight]  as weightr,c.[Region] as cou from [dbo].[International_currier_SERVICE] inner join  [dbo].[Region] c on c.id = [dbo].[International_currier_SERVICE].[Region_Id] where [dbo].[International_currier_SERVICE].[IsDelete] = 0 and c.[IsActive] = 1  order by  c.[Region] asc", grd_services);
                sort1 = "asc";
            }
        }
        catch (Exception ex)
        {
            Global.Alert(this.Page, ex.Message);
        }
    }
}