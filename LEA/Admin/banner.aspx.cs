using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;
public partial class Admin_banner : System.Web.UI.Page
{
    BannerBAL BB = new BannerBAL();
    DataTable DT = new DataTable();
    protected int EditID
    {
        get
        {
            if (ViewState["EditID"] != null)
            {
                return Convert.ToInt32(ViewState["EditID"]);
            }
            return 0;
        }
        set { ViewState["EditID"] = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtTitle.Focus();
            this.Form.DefaultButton = btnSubmit.UniqueID;
            if (Request.QueryString["id"] != null)
            {
                EditID = Convert.ToInt32(Request.QueryString["id"]);
                GetRecord();
                mltBanner.ActiveViewIndex = 1;
            }
            else
            {
                if (Request.QueryString["edit"] == "true")
                {

                    Global.Alert(this, "Banner edited successfully.");
                    string display = "Banner edited successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                    BindData();
                    mltBanner.ActiveViewIndex = 0;
                }
                if (Request.QueryString["add"] == "true")
                {

                    Global.Alert(this, "Banner added successfully.");
                    string display = "Banner added successfully.";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                    BindData();
                    mltBanner.ActiveViewIndex = 0;
                }
                BindData();
                mltBanner.ActiveViewIndex = 0;
            }
        }
    }
    public void GetRecord()
    {
        BB.ID = EditID;
        DT = BB.BannerList();
        if (DT.Rows.Count > 0)
        {
            imgcat.Visible = true;
            txtTitle.Text = DT.Rows[0]["Title"].ToString();
            hnfImage.Value = "1";
            hdnImageName.Value = DT.Rows[0]["ImagePath"].ToString();
            imgcat.ImageUrl = "~/Banner/" + DT.Rows[0]["ImagePath"].ToString();

        }
        else
        {
            mltBanner.ActiveViewIndex = 0;
            imgcat.Visible = false;
        }
    }
    public void BindData()
    {
        BB.ID = EditID;
        DT = BB.BannerList();
        if (DT.Rows.Count > 0)
        {
            GrdList.DataSource = DT;
            GrdList.DataBind();
        }
        else
        {
            GrdList.DataSource = DT;
            GrdList.DataBind();
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string Extesion = "";
        BB.ID = EditID;
        BB.Title = txtTitle.Text;
        BB.Path = hdnImageName.Value;
        int o = BB.ManageBanner();

        if (EditID != 0)
        {
            if (Fubanner.HasFile)
            {
                string FilePath = Server.MapPath("../Banner/" + hdnImageName.Value.ToString());
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                    Extesion = System.IO.Path.GetExtension(Fubanner.FileName);
                    Fubanner.SaveAs(Server.MapPath("../Banner/" + o.ToString() + Extesion));
                    BB.ID = EditID;
                    BB.Path = o.ToString() + Extesion;
                    BB.ManageBanner();
                    
                }
            }
            Response.Redirect("banner.aspx?edit=true");
        }
        else
        {
            if (Fubanner.HasFile)
            {
                Extesion = System.IO.Path.GetExtension(Fubanner.FileName);
                Fubanner.SaveAs(Server.MapPath("../Banner/" + o.ToString() + Extesion));
                BB.Path = o.ToString() + Extesion;
                BB.ID = o;
                BB.ManageBanner();
                Response.Redirect("banner.aspx?add=true");
            }
        }

       
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        mltBanner.ActiveViewIndex = 0;
        Response.Redirect("banner.aspx");
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        mltBanner.ActiveViewIndex = 1;
    }
    protected void GrdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete1")
        {
            DeleteBanner(Convert.ToInt32(e.CommandArgument));
        }
        else if (e.CommandName == "Edit1")
        {
            Response.Redirect("banner.aspx?id=" + e.CommandArgument);
        }
    }
    public void DeleteBanner(int ID)
    {
        BB.ID = ID;
        BB.DeleteBanner();
        Response.Redirect("banner.aspx");
    }
}