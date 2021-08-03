using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewOrder : System.Web.UI.Page
{
    DataSet dt = new DataSet();
    BookPurchaseBAL ObjPurchase = new BookPurchaseBAL();
    public static string lblfree = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"]) > 0)
            {
                if (Request.QueryString["Purchase"] != null && Convert.ToInt32(Request.QueryString["Purchase"]) > 0)
                {
                    BindGrid(Convert.ToInt32(Request.QueryString["Purchase"]));
                }
            }
            else
            {
                Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.Name);
            }
        }
    }

    public string PicturePath(string sFilename)
    {
        if (!File.Exists(Server.MapPath("~") + sFilename))
        {
            sFilename = "images/No_Image.jpg";
        }
        return sFilename;
    }

    public void BindGrid(int PurchaseID)
    {
        ObjPurchase.LanguageID = 1;
        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            ObjPurchase.LanguageID = 2;
        }
        else
        {
            ObjPurchase.LanguageID = 1;
        }
        ObjPurchase.PurchaseID = PurchaseID;
        dt = ObjPurchase.GetOrderDetailbyID();
        if (dt.Tables[0].Rows.Count > 0)
        {
            rptRecords1.DataSource = dt.Tables[0];
            rptRecords1.DataBind();
            rptRecords1.Visible = true;
            for(int i = 0;i<dt.Tables[0].Rows.Count;i++)
            {
                Repeater inner = (Repeater)rptRecords1.Items[i].FindControl("innerRpt");
                Panel innerRptDiv = (Panel)rptRecords1.Items[i].FindControl("innerRptDiv");
                if (dt.Tables[1].Rows.Count > 0)
                {
                    innerRptDiv.Visible = false;
                    inner.Visible = true;
                    inner.DataSource = dt.Tables[1];
                    inner.DataBind();
                }
                else
                {
                    innerRptDiv.Visible = true;
                    inner.Visible = false;
                }
            }
        }
        //else
        //{
        //    divOrder.Visible = false;
        //    Divorderebook.Visible = false;
        //    lblDefaultMessage.Visible = true;
        //    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        //    {
        //        lblDefaultMessage.Text = "No tienes Detalle orden";
        //    }
        //    else
        //    {
        //        lblDefaultMessage.Text = "You have no order Detail";
        //    }
        //}
    }
}