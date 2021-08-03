using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;

public partial class OrderReport : System.Web.UI.Page
{
    DataTable dt = new DataTable();    
    BookOrderBAL ObjBookOrderBal = new BookOrderBAL();
    public static string lblfree = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Session["CurrentCulture"] != null)
            //{
            //    System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Session["CurrentCulture"].ToString());
            //}
            if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"]) > 0)
            {
                BindGrid(Convert.ToInt32(Session["UserID"]));                
            }
            if (Request.QueryString["payid"] != null && Request.QueryString["payid"].ToString() != "")
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Gracias por la compra de libros electrónicos, el detalle de la orden es enviada por correo.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Thank you for purchasing eBook,your order detail is mailed to you.');", true);
                }
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
       
        ObjBookOrderBal.LanguageID = 1;
        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        {
            ObjBookOrderBal.LanguageID = 2;
        }
        else
        {
            ObjBookOrderBal.LanguageID = 1;
        }
        ObjBookOrderBal.CustomerID = CustomerID;
        dt = ObjBookOrderBal.get_CustomerBookOrderByID();
        if (dt.Rows.Count > 0)
        {
            
            rptRecords1.DataSource = dt;
            rptRecords1.DataBind();
            rptRecords1.Visible = true;
            //if (Convert.ToBoolean(dt.Rows[0]["IsFree"]) == true)
            //{
            //    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            //    {
            //        lblfree = "gratis";
            //    }
            //    else
            //    {
            //        lblfree = "Free";
            //    }

            //    //lblfree = Localization.ResourceManager.GetString("Free");
            //}
                
            //else
            //{
            //    lblfree = "$" + dt.Rows[0]["price"];
            //}
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