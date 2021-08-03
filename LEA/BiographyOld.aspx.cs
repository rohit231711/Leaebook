using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.IO;
using System.Text.RegularExpressions;
using Localization;

public partial class BiographyOld : System.Web.UI.Page
{
    BookBAL Obj_Book = new BookBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            addtoLibrary();
            BindData();
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                addtocart();
            }
        }
    }

    public void addtoLibrary()
    {
        int count=0;
        if (Request.QueryString["fbid"] != null && Request.QueryString["fbid"].ToString() != "")
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                objPurchase.BookID = Convert.ToInt16(Request.QueryString["fbid"].ToString());
                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                {
                    objPurchase.LanguageID = 2;
                }
                else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
                {
                    objPurchase.LanguageID = 1;
                }
                DataTable DT = objPurchase.getUserLibrary();
                if (DT != null && DT.Rows.Count > 0)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == objPurchase.BookID)
                        {
                            count++;
                        }

                    }
                    if (count > 0)
                    {
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);

                        }
                    }
                    else
                    {
                        objPurchase.MoveToBookPurchase_freebook_website();
                        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);

                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);

                        }
                        
                        //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["fbid"].ToString());
                    }
                }
                else
                {
                    
                    objPurchase.MoveToBookPurchase_freebook_website();
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);

                    }

                    //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["fbid"].ToString());
                }
            }
            else
            {
                Response.Redirect("Login.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }
    }

    public void addtocart()
    {
        BookOrderBAL Obj_bookOrder = new BookOrderBAL();
        int count = 0;
        int cnt1 = 0;
        int result = 0;
        if (Session["UserID"] != null && Session["UserID"].ToString() != "")
        {
            BookPurchaseBAL objPurchase = new BookPurchaseBAL();
            objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
            objPurchase.PurchaseDate = System.DateTime.Now;
            objPurchase.OrderID = 0;
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                objPurchase.BookID = Convert.ToInt16(Request.QueryString["id"].ToString());
            }
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            {
                objPurchase.LanguageID = 2;
            }
            else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
            {
                objPurchase.LanguageID = 1;
            }
            DataTable Dt = objPurchase.getUserLibrary();
            if (Dt != null && Dt.Rows.Count > 0)
            {
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
                    {
                        cnt1++;
                    }

                }
                if (cnt1 > 0)
                {
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('Usted ya ha comprado este libro electrónico. Qué quieres mover en mi biblioteca?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('You have already purchased this ebook. do you want to move in my library?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

                    }
                }
                else
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
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                            }
                        }
                        else
                        {
                            result = Obj_bookOrder.InsertCustomerCart1();
                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");
                        }
                    }
                    else
                    {
                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                        result = Obj_bookOrder.InsertCustomerCart1();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

                    }
                }
            }

            else
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
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                        }
                    }
                    else
                    {
                        result = Obj_bookOrder.InsertCustomerCart1();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");
                    }
                }
                else
                {
                    string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                    Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                    result = Obj_bookOrder.InsertCustomerCart1();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

                }
            }
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

        }
        else
        {
            if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
            {
                int cnt = 0;
                string[] str = Session["AddToCart"].ToString().Split(',');
                foreach (string Add in str)
                {

                    if (Add == Request.QueryString["id"].ToString())
                    {
                        cnt++;
                    }
                }
                if (cnt > 0)
                {
                    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
                    }
                }
                else
                {
                    Session["AddToCart"] = Session["AddToCart"] + "," + Request.QueryString["id"].ToString();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
            }
            else
            {
                Session["AddToCart"] = Request.QueryString["id"].ToString();
                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        }
    }

    public void BindData()
    {
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        viewallbook = Localization.ResourceManager.GetString("newrelease");
        newrelease = Localization.ResourceManager.GetString("newrelease");
        buynowtitle = Localization.ResourceManager.GetString("buynow");
        isfreetitle = Localization.ResourceManager.GetString("Free");
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary");
        DataTable dt = new DataTable();
        Obj_Book.LangaugeID = 1;
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book.LangaugeID = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book.LangaugeID = 1;
            }
        }
        if (Request.QueryString["catid"] != null && Request.QueryString["catid"].ToString() != "")
        {
            Obj_Book.CategoryID = Convert.ToInt32(Request.QueryString["catid"]);
            CategoryBAL cat = new CategoryBAL();
            cat.CategoryID = Convert.ToInt32(Request.QueryString["catid"]);
            if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
            {
                if (Request.QueryString["l"].ToString() == "es-ES")
                {
                    cat.LanguageID = 2;
                }
                else if (Request.QueryString["l"].ToString() == "en-US")
                {
                    cat.LanguageID = 1;
                }
            }
            DataTable DT = cat.SelectCategoryByID();
            if (DT != null && DT.Rows.Count > 0)
            {
                lblcat.Text = DT.Rows[0]["CategoryName"].ToString();
                this.Page.Title = lblcat.Text;
            }
        }

        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
        //{
            
        //    viewallbook.Value = "Ver todos eBook";
        //}
        //else
        //{
            
        //    viewallbook.Value = "View All eBook";
        //}

        dt = Obj_Book.getBookByCategory();

        string a1 = "<div class='panel'>" +
                        "<div class='titnewre'>" + newrelease.ToString() + " </div>" +
                        "<a href='Category.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='titnewre1'>" + viewallbook.ToString() + "</a>" +
                    "</div>";
        string str = "";

        if (dt.Rows.Count > 0)
        {
            int i = 1;
            str = str + "<div id='div_Books'>";
            foreach (DataRow dr in dt.Rows)
            {
                if (i % 3 == 0)
                {
                    str = str + "<div class='box1book boxbolast'>";
                }
                else
                {
                    str = str + "<div class='box1book'>";
                }
                i++;

                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                }
                else
                {
                    img = "images/No_Image.jpg";
                }

                if (Convert.ToInt32(dr["DiscountPrice"]) == 0 && Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    str = str + "<div class='bookimg'>" +
                              "<a href='Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"]) > 0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                str = str + "<div class='namkl2'><a href = 'Book-Detail.aspx?" + "&title=" + HttpUtility.UrlEncode(Convert.ToString(dr["Title"])) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' style='color:#616161'>" + dr["Title1"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<div class='namkl'>"+isfreetitle.ToString()+"</div>";
                }
                else
                {
                    str = str + "<div class='namkl'>$" + dr["FinalPrice"] + "</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<a href='Biography.aspx?fbid=" + dr["BookID"] + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>"+addtolibrarytitle.ToString()+"</a>" +
                    "</div>";
                }
                else
                {
                    str = str + "<a href='Biography.aspx?id=" + dr["BookID"] + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&catid=" + Request.QueryString["catid"] + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
                    "</div>";
                }
            }
            str = str + "</div>";
        }
        if (dt.Rows.Count == 0)
        {
            //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No se encontraron datos.');", true);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('No data Found.');", true);
            //}
            ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Nodatafound", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
        }
        div_book.InnerHtml = a1 + str;
    }
}