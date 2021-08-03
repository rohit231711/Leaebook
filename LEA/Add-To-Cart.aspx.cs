 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;
using System.Web.Security;
using System.Text.RegularExpressions;

public partial class Add_To_Cart : System.Web.UI.Page
{
    BookBAL Obj_book = new BookBAL();

    BookOrderBAL Obj_bookOrder = new BookOrderBAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                Bind_detail();
                Bind_BookRatting();
            }
        }        
    }

    public void Bind_BookRatting()
    {
        DataTable dt = new DataTable();
        Obj_book.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
        dt = Obj_book.get_book_ratting();
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["total_review"].ToString() == "1")
            {
                lbl_Totalrating.Text = "(" + dt.Rows[0]["total_review"] + " rating)";
            }
            else
            {
                lbl_Totalrating.Text = "(" + dt.Rows[0]["total_review"] + " ratings)";
            }

            int totalstar = Convert.ToInt32(dt.Rows[0]["ratting"].ToString());
            if (totalstar > 0)
            {
                img_rate1.Src = "images/big-ylw.png";
            }
            if (totalstar > 1)
            {
                img_rate2.Src = "images/big-ylw.png";
            }
            if (totalstar > 2)
            {
                img_rate3.Src = "images/big-ylw.png";
            }
            if (totalstar > 3)
            {
                img_rate4.Src = "images/big-ylw.png";
            }
            if (totalstar > 4)
            {
                img_rate5.Src = "images/big-ylw.png";
            }
        }        
    }

    public void Bind_detail()
    {
        DataTable dt = new DataTable();
        Obj_book.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
        Obj_book.LangaugeID = 1;
        dt =Obj_book.getBookDetails();
        if (dt.Rows.Count > 0)
        {
            lbl_bookname.Text = dt.Rows[0]["Title"].ToString();
            lbl_author.Text = dt.Rows[0]["Autoher"].ToString();
            lbl_price.Text = dt.Rows[0]["Price"].ToString();
            lbl_finalprice.Text = dt.Rows[0]["FinalPrice"].ToString();
            string img = "";
            string fileName = "Book/" + dt.Rows[0]["CategoryID"] + '/' + dt.Rows[0]["ImagePath"] + "";
            if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
            {
                img = "Book/" + dt.Rows[0]["CategoryID"] + '/' + dt.Rows[0]["ImagePath"] + "";
            }
            else
            {
                img = "images/No_Image.jpg";
            }

            img_book.Src = img;

            string str = "";
            str = str + "<div class='descpnl'>Description</div>" +
                        "<p>" + dt.Rows[0]["Description"].ToString() + "</p>";
            div_description.InnerHtml = str;                                  
        }
    }

    protected void btn_WishList_Click(object sender, EventArgs e)
    {
        //Obj_bookOrder.OrderDate = System.DateTime.Now;
        //Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
        //Obj_bookOrder.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
        //Obj_bookOrder.InsertCustomerWishList1();
        //Response.Redirect("WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);

        int count = 0;
        int result = 0;
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            Session["AddToWishlist"] = null;
            Obj_bookOrder.OrderDate = System.DateTime.Now;
            Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
            Obj_bookOrder.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
            //Obj_bookOrder.InsertCustomerCart1();



            DataTable DT = Obj_bookOrder.GetWishList();
            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                    {
                        count++;
                    }

                }
                if (count > 0)
                {
                    //Message1("You already have this book in your WishList");
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your wishlist');", true);
                }
                else
                {
                    result = Obj_bookOrder.InsertCustomerWishList1();
                    Response.Redirect("WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
                }
            }
            else
            {
                result = Obj_bookOrder.InsertCustomerWishList1();
                Response.Redirect("WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
            }
            //Response.Redirect("WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
        }
        else
        {

            if (Session["AddToWishlist"] != null && Session["AddToWishlist"].ToString() != "")
            {
                Session["AddToWishlist"] = Session["AddToWishlist"] + "," + Request.QueryString["id"].ToString();
            }
            else
            {
                Session["AddToWishlist"] = Request.QueryString["id"].ToString();
            }
            Response.Redirect("WishList.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
    
    }

    protected void btn_addcart_Click(object sender, EventArgs e)
    {
        int count = 0;
        int result = 0;
        if (Session["UserID"] != null)
        {
            Session["AddToCart"] = null;
            Obj_bookOrder.OrderDate = System.DateTime.Now;
            Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
            Obj_bookOrder.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
            //Obj_bookOrder.InsertCustomerCart1();



            DataTable DT = Obj_bookOrder.GetCartList();
            if (DT != null && DT.Rows.Count > 0)
            {
                Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
                    {
                        count++;
                    }

                }
                if (count > 0)
                {
                    //Message1("You already have this book in your cart");
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        //Global.Alert(this, "Tu mensaje ha sido enviado con éxito a su dirección de correo electrónico");
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cart');", true);
                        
                    }
                    else
                    {
                        //Global.Alert(this, "Your message has been sent successfully to your email address");
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Libro ya está añadido a la cesta.');", true);
                        
                    }
                   
                }
                else
                {
                    result = Obj_bookOrder.InsertCustomerCart1();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
                }
            }
            else
            {
                string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                result = Obj_bookOrder.InsertCustomerCart1();
                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
            }
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Obj_bookOrder.CustomerID + "");
        }
        else
        {

            if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
            {
                Session["AddToCart"] = Session["AddToCart"] + "," + Request.QueryString["id"].ToString();
            }
            else
            {
                Session["AddToCart"] = Request.QueryString["id"].ToString();
            }
            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
        
    }    
  
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        //Insert_Review
        Obj_book.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
        //Obj_book.UserID = Convert.ToInt32(Session["UserID"].ToString());
        Obj_book.BookID = Convert.ToInt32(Request.QueryString["id"].ToString());
        if (Session["UserID"] != null)
        {
            Obj_book.UserID = Convert.ToInt32(Session["UserID"].ToString());
        }
        else
        {
            Obj_book.UserID = 0;
        }
        Obj_book.Name = txt_nickname.Text;
        Obj_book.Summary = txt_summary.Text.ToString();
        Obj_book.Review = txt_review.Text;
        if (rdo_1.Checked)
        {
            Obj_book.Ratting = 1;
        }
        if (rdo_2.Checked)
        {
            Obj_book.Ratting = 2;
        }
        if (rdo_3.Checked)
        {
            Obj_book.Ratting = 3;
        }
        if (rdo_4.Checked)
        {
            Obj_book.Ratting = 4;
        }
        if (rdo_5.Checked)
        {
            Obj_book.Ratting = 5;
        }
        Obj_book.Insert_ReviewRatting();
        Response.Redirect("Add-To-Cart.aspx?id=" + Request.QueryString["id"].ToString() + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    }    
}