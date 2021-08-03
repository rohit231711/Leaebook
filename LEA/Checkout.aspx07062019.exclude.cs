using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
using System.Threading;
using System.Globalization;
using Localization;

public partial class Checkout : System.Web.UI.Page
{
    BookOrderBAL Obj_bookOrder = new BookOrderBAL();
    Security S = new Security();
    double Amount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                try
                {
                    int s = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                }
                catch
                {
                    string a = Request.Url.AbsoluteUri.ToString();
                    string old = Request.QueryString["id"].ToString();
                    Response.Redirect(Request.Url.AbsoluteUri.ToString().Replace(old, S.Encrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString()));
                }
            }
            if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"]) > 0)
            {
                Proceed_to_Checkout(null, null);
            }
            else
            {
                BindData();
            }
        }
    }

    protected void BindData()
    {
        DataTable dt = new DataTable();
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            Obj_bookOrder.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
            Obj_bookOrder.LanguageID = 1;

            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
            {
                if (Request.QueryString["l"].ToString() == "es-ES")
                {
                    Obj_bookOrder.LanguageID = 2;
                }
                else if (Request.QueryString["l"].ToString() == "en-US")
                {
                    Obj_bookOrder.LanguageID = 1;
                }
            }
            dt = Obj_bookOrder.GetCartList();
        }
        else
        {
            BookBAL book = new BookBAL();
            if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
            {
                book.OrderIndex = Convert.ToInt64(Session["AddToCardBookType"].ToString() != "" || Session["AddToCardBookType"].ToString() != null ? Session["AddToCardBookType"].ToString() : "1");
                book.BookId = Session["AddToCart"].ToString();
                book.LangaugeID = 1;
                if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
                {
                    if (Request.QueryString["l"].ToString() == "es-ES")
                    {
                        book.LanguageID = 2;
                    }
                    else if (Request.QueryString["l"].ToString() == "en-US")
                    {
                        book.LanguageID = 1;
                    }
                }

                dt = book.getBookDetailsByBookType_AddToCart();
            }
        }


        if (dt != null && dt.Rows.Count > 0)
        {
            string CurrentCultur = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["IsFree"]) != true)
                {
                    //Amount += Convert.ToDouble(dt.Rows[i]["FinalPrice1"]); 
                    Amount += Convert.ToDouble(dt.Rows[i]["FinalCartPrice1"]);

                    //Amount += (float)Convert.ToDecimal(dt.Rows[i]["FinalPrice1"]);
                }
                if (Amount == 0)
                {
                    Amount = Convert.ToDouble(dt.Rows[i]["FinalCartPrice1"]);
                }
            }

            lblAmount.Text = Amount.ToString();
            Thread.CurrentThread.CurrentCulture = new CultureInfo(CurrentCultur.ToString());

            rptRecords1.DataSource = dt;
            rptRecords1.DataBind();

            rptRecords1.Visible = true;
        }
        else
        {
            //GeneratePages(0);
            chkout.Visible = false;
            rptRecords1.Visible = false;
            lblDefaultMessage.Visible = true;
            lblAmount.Text = "0.00";
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                lblDefaultMessage.Text = "No se encontraron datos";
            }
            else
            {
                lblDefaultMessage.Text = "No data found";
            }
            //tot.Visible = false;

        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    protected void Delete(object sender, EventArgs e)
    {
        try
        {
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();
            string Result = "";
            if (ID.Contains(","))
            {
                string[] ids = ID.Split(',');
                foreach (string id in ids)
                {
                    if (id != "")
                    {

                        Obj_bookOrder.WishID = Convert.ToInt32(id);
                        int result = Obj_bookOrder.DeleteItemfromUserCart();
                    }
                }
            }
            else
            {
                Obj_bookOrder.WishID = Convert.ToInt32(ID);
                Obj_bookOrder.DeleteItemfromUserCart();
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void del(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                var lnk = e.CommandArgument.ToString().Split('@');
                Obj_bookOrder.IseBook = Convert.ToBoolean(lnk[1]);
                Obj_bookOrder.IspaperBook = Convert.ToBoolean(lnk[2]);
                int CommandArgument = Convert.ToInt32(lnk[0]);
                Obj_bookOrder.BookID = CommandArgument;
                Obj_bookOrder.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                Obj_bookOrder.DeletefromCustomerCart();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                string s = Session["AddToCart"].ToString();
                //int CommandArgument = Convert.ToInt32(e.CommandArgument);
                var lnk = e.CommandArgument.ToString().Split('@');
                Obj_bookOrder.IseBook = Convert.ToBoolean(lnk[1]);
                Obj_bookOrder.IspaperBook = Convert.ToBoolean(lnk[2]);
                int CommandArgument = Convert.ToInt32(lnk[0]);
                string cmd = CommandArgument.ToString();
                s = s.Replace(cmd, "");
                Session["AddToCart"] = s;
                //BindData();
                Response.Redirect(Request.RawUrl);
            }

        }

        else if (e.CommandName == "move")
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                //int CommandArgument = Convert.ToInt32(e.CommandArgument);
                var lnk = e.CommandArgument.ToString().Split('@');
                int CommandArgument = Convert.ToInt32(lnk[0]);
                Obj_bookOrder.BookID = CommandArgument;
                Obj_bookOrder.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                Obj_bookOrder.MoveItemfromUserCartToWishlist();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                string s = Session["AddToCart"].ToString();
                //int CommandArgument = Convert.ToInt32(e.CommandArgument);
                var lnk = e.CommandArgument.ToString().Split('@');
                int CommandArgument = Convert.ToInt32(lnk[0]);
                string cmd = CommandArgument.ToString();
                if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
                {
                    Session["AddToWishlist"] = Session["AddToWishlist"] + "," + cmd;
                }
                else
                {
                    Session["AddToWishlist"] = cmd;
                }
                s = s.Replace(cmd, "");
                Session["AddToCart"] = s;

                Response.Redirect("WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }

    }

    protected void Proceed_to_Checkout(object sender, EventArgs e)
    {
        Session["website"] = "true";
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            if (Request.QueryString["id"] != null && Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))) > 0)
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                Obj_bookOrder.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                DataTable WishDT = Obj_bookOrder.GetCartList();
                //int result = 0;
                if (WishDT != null && WishDT.Rows.Count > 0)
                {
                    //Response.Redirect("Payment.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&UserID=" + Obj_bookOrder.CustomerID + "");
                    Response.Redirect("PaymentNew.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&UserID=" + S.Encrypt(HttpUtility.UrlEncode(Obj_bookOrder.CustomerID.ToString())) + "&id=" + Request.QueryString["id"] + "");
                }
                else
                {
                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No hay productos en cartlist. Por favor, haz clic en Continuar con la compra de las compras continúan');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No Items in cartlist. Please Click on Continue to shopping for shopping continue');", true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Please Click on Continue to shopping", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                }

            }
            else
            {
                //Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "");
                if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                {

                    Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "");
                }
                else
                {
                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No hay productos en cartlist. Por favor, haz clic en Continuar con la compra de las compras continúan');", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No Items in cartlist. Please Click on Continue to shopping for shopping continue');", true);
                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Please Click on Continue to shopping", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                }
            }
        }
        else
        {
            Session["RedirectUrl"] = HttpContext.Current.Request.Url.AbsoluteUri;
            Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "");
        }
    }

    public string PicturePath(string sFilename)
    {
        if (!File.Exists(Server.MapPath("~") + sFilename))
        {
            sFilename = @"../images/No_Image.jpg";
        }
        return sFilename;
    }

    protected void txtQuanitity_TextChanged(object sender, EventArgs e)
    {
        TextBox tb1 = ((TextBox)(sender));
        RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));

        TextBox txtQuanitity = (TextBox)rp1.FindControl("txtQuanitity");
        Label lblPrice = (Label)rp1.FindControl("lblPrice");
        Label lblOriginalPrice = (Label)rp1.FindControl("lblOriginalPrice");

        lblPrice.Text = Convert.ToString(Convert.ToDouble(lblOriginalPrice.Text) * Convert.ToDouble(txtQuanitity.Text));
    }

    public string getRemoveString()
    {
        //OnClientClick="return confirm('" + '<%# ResourceManager.GetString("Are you sure you want to Remove?",System.Threading.Thread.CurrentThread.CurrentCulture.Name) %>' + "'"); ";
        return "return confirm('" + Localization.ResourceManager.GetString("Are you sure you want to Remove?", System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');";
        //return "";
    }

    public string getWishString()
    {
        //OnClientClick='<%# "return confirm('" + ResourceManager.GetString("Are you sure you want to move to wishlist?",System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');"%>'
        return "return confirm('" + Localization.ResourceManager.GetString("Are you sure you want to move to wishlist?", System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');";
        //return "";
    }

    public string getKeyFromBook()
    {
        if (Session["AddToCardBookType"] != null && Session["AddToCardBookType"].ToString() != "")
        {
            if (Session["AddToCardBookType"].ToString() == "0")
                return "bothBook";
            else if (Session["AddToCardBookType"].ToString() == "1")
                return "eBook";
            else if (Session["AddToCardBookType"].ToString() == "2")
                return "paperBook";
            else
                return "eBook";
        }
        else
            return "eBook";
    }
}