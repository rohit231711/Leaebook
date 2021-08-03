using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OrderHistory : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    BookPurchaseBAL ObjPurchase = new BookPurchaseBAL();
    public static string lblfree = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"]) > 0)
            {
                BindGrid(Convert.ToInt32(Session["UserID"]));
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

    public void BindGrid(int CustomerID)
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
        ObjPurchase.UserID = CustomerID;
        dt = ObjPurchase.GetAllOrderbyUserID();
        if (dt.Rows.Count > 0)
        {

            rptRecords1.DataSource = dt;
            rptRecords1.DataBind();
            rptRecords1.Visible = true;
        }
        else
        {
            divOrder.Visible = false;
            Divorderebook.Visible = false;
            lblDefaultMessage.Visible = true;
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                lblDefaultMessage.Text = "No tienes Detalle orden";
            }
            else
            {
                lblDefaultMessage.Text = "You have no order Detail";
            }
        }
    }

}