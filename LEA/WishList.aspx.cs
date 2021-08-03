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

public partial class WishList : System.Web.UI.Page
{
    BookBAL Obj_book = new BookBAL();
    Security S = new Security();
    BookOrderBAL Obj_bookOrder = new BookOrderBAL();
    double Amount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["UserID"] != null && Convert.ToInt32(Session["UserID"]) > 0)
            {
                BindData();
            }
            else
            {
                Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.Name);
            }
        }
    }
    protected void BindData()
    {
        DataTable dt = new DataTable();
        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
        {
            try
            {
                Obj_bookOrder.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
            }
            catch
            {
                Obj_bookOrder.CustomerID = Convert.ToInt32(HttpUtility.UrlDecode(Request.QueryString["id"]).ToString());
            }
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
            dt = Obj_bookOrder.GetWishList();
        }
        else
        {
            BookBAL book = new BookBAL();
            if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
            {
                book.BookId = Session["AddToWishlist"].ToString();
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
                dt = book.getBookDetails_AddToCart();
            }
        }
        if (dt != null && dt.Rows.Count > 0 )
        {
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dt.Rows[i]["IsFree"]) != true)
                {
                    Amount += Convert.ToDouble(dt.Rows[i]["FinalCartPrice1"]);
                }
            }
            lblAmount.Text = Amount.ToString();
            rptRecords1.DataSource = dt;
            rptRecords1.DataBind();

            rptRecords1.Visible = true;
        }
        else
        {
            wl.Visible = false;
            //GeneratePages(0);
            lblAmount.Text = "0.00";
            rptRecords1.Visible = false;
            //Label lblDefaultMessage = (Label)e.FindControl("lblDefaultMessage");
            lblDefaultMessage.Visible = true;
            tot.Visible = false;
            mov.Visible = false;
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                lblDefaultMessage.Text = "No tienes ningún libro electrónico en su lista de deseos";
            }
            else
            {
                lblDefaultMessage.Text = "You have no any eBook on your Wish list";
            }       
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void DeleteRecord(string ID)
    {
        try
        {
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();
            
            if (ID.Contains(","))
            {
                string[] ids = ID.Split(',');
                foreach (string id in ids)
                {
                    if (id != "")
                    {

                        Obj_bookOrder.WishID = Convert.ToInt32(id);
                        int result = Obj_bookOrder.DeleteItemfromUserWishList();
                    }
                }
            }
            else
            {
                Obj_bookOrder.WishID = Convert.ToInt32(ID);
                Obj_bookOrder.DeleteItemfromUserWishList();
            }
            
        }
        catch (Exception ex)
        { 
            
        }
     }

    protected void del(object source, RepeaterCommandEventArgs e)
    {

        BookOrderBAL ObjBookOrders = new BookOrderBAL();
        if (e.CommandName == "delete")
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                var argument = e.CommandArgument.ToString().Split(',');
                int CommandArgument = Convert.ToInt32(argument[0]);
                Obj_bookOrder.BookID = CommandArgument;
                try
                {
                    Obj_bookOrder.CustomerID = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"])).ToString());
                }
                catch
                {
                    Obj_bookOrder.CustomerID = Convert.ToInt32(HttpUtility.UrlDecode(Request.QueryString["id"]).ToString());
                }
                Obj_bookOrder.IspaperBook = Convert.ToBoolean(argument[1]);
                Obj_bookOrder.IseBook = Convert.ToBoolean(argument[2]);
                Obj_bookOrder.DeletefromWishList();
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                string s = Session["AddToWishlist"].ToString();
                var argument = e.CommandArgument.ToString().Split(',');
                int CommandArgument = Convert.ToInt32(argument[0]);
                Obj_bookOrder.BookID = CommandArgument;
                Obj_bookOrder.IspaperBook = Convert.ToBoolean(argument[1]);
                Obj_bookOrder.IseBook = Convert.ToBoolean(argument[2]);
                string cmd = CommandArgument.ToString();
                s = s.Replace(cmd, "");
                Session["AddToWishlist"] = s;
                //BindData();
                Response.Redirect(Request.RawUrl);
            }


        }

        else if (e.CommandName == "delete1")
        {
            ObjBookOrders.CustomerID = Convert.ToInt32(Session["UserID"]);
            ObjBookOrders.LanguageID = 1;
            string Title_Data = "";
            string BookID = e.CommandArgument.ToString();
            dbconnection db = new dbconnection();
            DataTable dts = db.filltable("Select Title from Book where BookID=" + BookID);

            if (dts.Rows.Count > 0)
            {
                Title_Data = dts.Rows[0]["Title"].ToString();
            }
            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
            {
                if (Request.QueryString["l"].ToString() == "es-ES")
                {
                    ObjBookOrders.LanguageID = 2;
                }
                else if (Request.QueryString["l"].ToString() == "en-US")
                {
                    ObjBookOrders.LanguageID = 1;
                }
            }
            string Data = Title_Data.ToString();
            var URL = HttpUtility.UrlDecode(Data.ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
            URL = URL.Replace("-", " "); URL = URL.Replace("___", ":");
            URL = URL.Replace("__", "-");
            URL = URL.Replace("_", ".");

            if (Request.QueryString["l"].ToString() == "en-US")
            {
                Response.Redirect(Config.WebSiteMain + "us/" + URL.Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___"));
            }
            else if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Response.Redirect(Config.WebSiteMain + "es/" + URL.Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___"));
            }
            else
            {
                Response.Redirect(Config.WebSiteMain + "us/" + URL.Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___"));
            }
        }

        else if (e.CommandName == "Title")
        {
            ObjBookOrders.CustomerID = Convert.ToInt32(Session["UserID"]);
            ObjBookOrders.LanguageID = 1;
            string Title_Data = "";
            string BookID = e.CommandArgument.ToString();
            dbconnection db = new dbconnection();
            DataTable dts = db.filltable("Select Title from Book where BookID=" + BookID);

            if (dts.Rows.Count > 0)
            {
                Title_Data = dts.Rows[0]["Title"].ToString();
            }
            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
            {
                if (Request.QueryString["l"].ToString() == "es-ES")
                {
                    ObjBookOrders.LanguageID = 2;
                }
                else if (Request.QueryString["l"].ToString() == "en-US")
                {
                    ObjBookOrders.LanguageID = 1;
                }
            }
            string Data = Title_Data.ToString();
            var URL = HttpUtility.UrlDecode(Data.ToString(), System.Text.Encoding.UTF8);//.Replace("-", " ").Replace("_", ".").Replace("__", "-").ToString();
            URL = URL.Replace("-", " "); URL = URL.Replace("___", ":");
            URL = URL.Replace("__", "-");
            URL = URL.Replace("_", ".");

            if (Request.QueryString["l"].ToString() == "en-US")
            {
                Response.Redirect(Config.WebSiteMain + "us/" + URL.Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___"));
            }
            else if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Response.Redirect(Config.WebSiteMain + "es/" + URL.Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___"));
            }
            else
            {
                Response.Redirect(Config.WebSiteMain + "us/" + URL.Replace("-", "__").Replace("+", "-").Replace(".", "_").Replace(":", "___"));
            }
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

    protected void move_to_cart(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null && Request.QueryString["id"] != "")
        {
            //Obj_bookOrder.CustomerID = Convert.ToInt32(Request.QueryString["id"]);
            //Obj_bookOrder.MoveToCustomerCart();
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
            int cnt1 = 0;
            int count = 0;
            Obj_bookOrder.CustomerID = Convert.ToInt32(Request.QueryString["id"]);
            try
            {
                Obj_bookOrder.CustomerID = Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
            }
            catch
            {
                Obj_bookOrder.CustomerID = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
            DataTable WishDT = Obj_bookOrder.GetWishList();
            int result = 0;
            if (WishDT != null && WishDT.Rows.Count > 0)
            {
                for (int i = 0; i < WishDT.Rows.Count; i++)
                {
                    //Obj_bookOrder.WishID = Convert.ToInt32(WishDT.Rows[i]["WishID"]);
                    //Obj_bookOrder.OrderDate = Convert.ToDateTime(System.DateTime.Now);

                    //result = Obj_bookOrder.MoveToCustomerCart();
                    Obj_bookOrder.WishID = Convert.ToInt32(WishDT.Rows[i]["WishID"]);
                    Obj_bookOrder.OrderDate = Convert.ToDateTime(System.DateTime.Now);
                    Obj_bookOrder.LanguageID = 1;
                    DataTable DT = Obj_bookOrder.GetCartList();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        cnt1 = 0;
                        for (int j = 0; j < DT.Rows.Count; j++)
                        {
                            if (Convert.ToInt32(DT.Rows[j]["BookID"]) == Convert.ToInt32(WishDT.Rows[i]["BookID"]))
                            {
                                count++;
                                cnt1++;
                                Obj_bookOrder.DeleteItemfromUserWishList();
                            }
                        }
                        if (cnt1 == 0)
                        {
                            result = Obj_bookOrder.MoveToCustomerCart();
                            //cnt++;
                        }
                    }
                    else
                    {
                        result = Obj_bookOrder.MoveToCustomerCart();
                        //cnt++;
                    }
                    
                }
                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Obj_bookOrder.CustomerID))) + "");
            }
            else
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No tienes ningún libro electrónico en su lista de deseos');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert(' You have no any eBook on your Wishlist');", true);
                }
            }
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
        }
        else
        {
            if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
            {
                if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
                {
                    Session["AddToCart"] = Session["AddToCart"] + "," + Session["AddToWishlist"];
                }
                else
                {
                    Session["AddToCart"] = Session["AddToWishlist"];
                }
                Session["AddToWishlist"] = null;
                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
            else
            {
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No tienes ningún libro electrónico en su lista de deseos');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert(' You have no any eBook on your Wishlist');", true);
                }
            }
        }
    }
    public string getRemoveString()
    {
        //OnClientClick="return confirm('" + '<%# ResourceManager.GetString("Are you sure you want to Remove?",System.Threading.Thread.CurrentThread.CurrentCulture.Name) %>' + "'"); ";
        return "return confirm('" + Localization.ResourceManager.GetString("Are you sure you want to Remove?", System.Threading.Thread.CurrentThread.CurrentCulture.Name) + "');";
        //return "";
    }
}