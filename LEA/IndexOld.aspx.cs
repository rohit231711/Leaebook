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
using System.Collections;
using Localization;

public partial class IndexOld : System.Web.UI.Page
{
    
    
    BookBAL Obj_Book = new BookBAL();
    Security S = new Security();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (Request.QueryString["l"] == null)
            {
                Response.Redirect(url+"?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
            addtoLibrary();
            BindData();
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
            {
                addtocart();
            }
            //if (Request.QueryString["regid"] != null && Request.QueryString["regid"].ToString() != "")
            //{
            //    if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Tu mensaje ha sido enviado con éxito.');", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Your message has been sent successfully.');", true);
            //    }
            //}

            if (Request.QueryString["loginid"] != null)
            {
                BAL_Account obj_login = new BAL_Account();
                RegistrationBAL objUser = new RegistrationBAL();
                objUser.ActivationID = Request.QueryString["loginid"].ToString();
                DataTable dt = objUser.ActiveAccount();
                if (dt.Rows.Count > 0)
                {
                   obj_login.UserName = dt.Rows[0]["EmailAddress"].ToString();
                   obj_login.Password = dt.Rows[0]["Password"].ToString();
                    DataTable dt1 = new DataTable();
                    dt1 = obj_login.Check_Login();
                    if (dt1.Rows.Count > 0)
                    {
                        Session["UserName"] = dt1.Rows[0]["FirstName"].ToString();
                        Session["UserID"] = dt1.Rows[0]["RegistrationID"].ToString();

                       
                            //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Su cuenta ha sido activada successfullfy. Ahora se puede acceder a su cuenta.');", true);
                            //}
                            //else
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Your account has been activated successfully');", true);
                            //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("account has been activated", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                        
                    }
                }
            }
        }
    }

    public void addtoLibrary()
    {
        int count = 0;
        if (Request.QueryString["fbid"] != null && Request.QueryString["fbid"].ToString() != "")
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                objPurchase.BookID = Convert.ToInt16(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["fbid"])).ToString());
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
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{
                        // ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su biblioteca');", true);
                        //}
                        //else
                        //{
                        // ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your library');", true);
                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book already exists in your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
                    }
                    else
                    {
                        objPurchase.MoveToBookPurchase_freebook_website();
                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                        //{

                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro agregado a su biblioteca');", true);

                        //}
                        //else
                        //{

                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library');", true);

                        //}
                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book added successfully to your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);

                        //Response.Redirect("MyLibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Request.QueryString["fbid"].ToString());
                    }
                }
                else
                {

                    objPurchase.MoveToBookPurchase_freebook_website();
                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
                    //{

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro agregado a su biblioteca');", true);

                    //}
                    //else
                    //{

                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book added successfully to your library');", true);

                    //}
                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Book added successfully to your library", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
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
        int count = 0;
        int result = 0;

        if (ViewState["IsFree"] != null && ViewState["IsFree"].ToString() == "True")
        {
            addtoLibrary();
        }
        else
        {
            BookOrderBAL Obj_bookOrder = new BookOrderBAL();

            int cnt1 = 0;

            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                BookPurchaseBAL objPurchase = new BookPurchaseBAL();
                objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
                objPurchase.PurchaseDate = System.DateTime.Now;
                objPurchase.OrderID = 0;
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    objPurchase.BookID = Convert.ToInt16(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
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
                        Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
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
                                //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Security.AES256Encrypt(Session["UserID"].ToString()) + "");
                                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                            }
                        }
                        else
                        {
                            string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                            Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                            result = Obj_bookOrder.InsertCustomerCart1();
                            //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Security.AES256Encrypt(Session["UserID"].ToString()) + "");
                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
                        }
                    }
                }

                else
                {
                    Session["AddToCart"] = null;
                    Obj_bookOrder.OrderDate = System.DateTime.Now;
                    Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
                    Obj_bookOrder.BookID = Convert.ToInt32(Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString());
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
                            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Security.AES256Encrypt(Session["UserID"].ToString()) + "");
                        }
                    }
                    else
                    {
                        string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
                        Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
                        result = Obj_bookOrder.InsertCustomerCart1();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Security.AES256Encrypt(Session["UserID"].ToString()) + "");

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

                        if (Add == Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString())
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
                        Session["AddToCart"] = Session["AddToCart"] + "," + Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    }
                }
                else
                {
                    Session["AddToCart"] = Convert.ToInt32(S.Decrypt(HttpUtility.UrlDecode(Request.QueryString["id"]))).ToString();
                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                }
                //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
            }
        }
    }
    
    //public void addtocart()
    //{
    //    BookOrderBAL Obj_bookOrder = new BookOrderBAL();
    //    int count = 0;
    //    int cnt1=0;
    //    int result = 0;
    //    if (Session["UserID"] != null && Session["UserID"].ToString() != "")
    //    {
    //        BookPurchaseBAL objPurchase = new BookPurchaseBAL();
    //        objPurchase.UserID = Convert.ToInt32(Session["UserID"]);
    //        objPurchase.PurchaseDate = System.DateTime.Now;
    //        objPurchase.OrderID = 0;
    //        if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
    //        {
    //            objPurchase.BookID = Convert.ToInt16(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString());
    //        }
    //        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //        {
    //            objPurchase.LanguageID = 2;
    //        }
    //        else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
    //        {
    //            objPurchase.LanguageID = 1;
    //        }
    //        DataTable Dt = objPurchase.getUserLibrary();
    //        if (Dt != null && Dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < Dt.Rows.Count; i++)
    //            {
    //                if (Convert.ToInt32(Dt.Rows[i]["BookID"]) == objPurchase.BookID)
    //                {
    //                    cnt1++;
    //                }

    //            }
    //            if (cnt1 > 0)
    //            {
    //                if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                {

    //                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('Usted ya ha comprado este libro electrónico. Qué quieres mover en mi biblioteca?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

    //                }
    //                else
    //                {

    //                    ScriptManager.RegisterStartupScript(this, GetType(), "for", "if(confirm('You have already purchased this ebook. do you want to move in my library?')){ window.location = 'mylibrary.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "';}", true);

    //                }
    //            }
    //            else
    //            {
    //                Session["AddToCart"] = null;
    //                Obj_bookOrder.OrderDate = System.DateTime.Now;
    //                Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
    //                Obj_bookOrder.BookID = Convert.ToInt32(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString());
    //                //Obj_bookOrder.InsertCustomerCart1();

    //                DataTable DT = Obj_bookOrder.GetCartList();
    //                if (DT != null && DT.Rows.Count > 0)
    //                {
    //                    Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
    //                    for (int i = 0; i < DT.Rows.Count; i++)
    //                    {
    //                        if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
    //                        {
    //                            count++;
    //                        }
    //                    }
    //                    if (count > 0)
    //                    {
    //                        //Message1("You already have this book in your cart");
    //                        //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                        //{
    //                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
    //                        //}
    //                        //else
    //                        //{
    //                        //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
    //                        //}
    //                        ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Bookisalreadyinyourcartlist", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
    //                    }
    //                    else
    //                    {
    //                        result = Obj_bookOrder.InsertCustomerCart1();
    //                        Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");
    //                    }
    //                }
    //                else
    //                {
    //                    string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
    //                    Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
    //                    result = Obj_bookOrder.InsertCustomerCart1();
    //                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString() + "");

    //                }
    //            }
    //        }

    //        else
    //        {
    //            Session["AddToCart"] = null;
    //            Obj_bookOrder.OrderDate = System.DateTime.Now;
    //            Obj_bookOrder.CustomerID = Convert.ToInt32(Session["UserID"].ToString());
    //            Obj_bookOrder.BookID = Convert.ToInt32(HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString());
    //            //Obj_bookOrder.InsertCustomerCart1();

    //            DataTable DT = Obj_bookOrder.GetCartList();
    //            if (DT != null && DT.Rows.Count > 0)
    //            {
    //                Obj_bookOrder.OrderNo = DT.Rows[0]["OrderNo"].ToString();
    //                for (int i = 0; i < DT.Rows.Count; i++)
    //                {
    //                    if (Convert.ToInt32(DT.Rows[i]["BookID"]) == Obj_bookOrder.BookID)
    //                    {
    //                        count++;
    //                    }
    //                }
    //                if (count > 0)
    //                {
    //                    //Message1("You already have this book in your cart");
    //                    //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                    //{
    //                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
    //                    //}
    //                    //else
    //                    //{
    //                    //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
    //                    //}
    //                    ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Bookisalreadyinyourcartlist", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
    //                }
    //                else
    //                {
    //                    result = Obj_bookOrder.InsertCustomerCart1();
    //                    Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString().ToString() + "");
    //                }
    //            }
    //            else
    //            {
    //                string str1 = (Session["UserID"].ToString() + (System.DateTime.Now).ToString("MM/dd/yyyy hh:mm:ss"));
    //                Obj_bookOrder.OrderNo = Regex.Replace(str1, "[^0-9A-Za-z]+", "");
    //                result = Obj_bookOrder.InsertCustomerCart1();
    //                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(Session["UserID"]))).ToString().ToString() + "");

    //            }
    //        }
    //        //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + Session["UserID"].ToString() + "");

    //    }
    //    else
    //    {
    //        if (Session["AddToCart"] != null && Session["AddToCart"].ToString() != "")
    //        {
    //            int cnt = 0;
    //            string[] str = Session["AddToCart"].ToString().Split(',');
    //            foreach (string Add in str)
    //            {

    //                if (Add == HttpUtility.UrlEncode(S.Decrypt(Request.QueryString["id"])).ToString())
    //                {
    //                    cnt++;
    //                }
    //            }
    //            if (cnt > 0)
    //            {
    //                //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
    //                //{
    //                //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Libro ya se ha agregado en su cartlist');", true);
    //                //}
    //                //else
    //                //{
    //                //    ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book is already added in your cartlist');", true);
    //                //}
    //                ScriptManager.RegisterStartupScript(this, GetType(), "for", String.Format("alert('{0}');", ResourceManager.GetString("Bookisalreadyinyourcartlist", System.Threading.Thread.CurrentThread.CurrentCulture.Name)), true);
    //            }
    //            else
    //            {
    //                Session["AddToCart"] = Session["AddToCart"] + "," + HttpUtility.UrlEncode(S.Encrypt(Request.QueryString["id"])).ToString();
    //                Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //            }
    //        }
    //        else
    //        {
    //            Session["AddToCart"] = HttpUtility.UrlEncode(S.Encrypt(Request.QueryString["id"])).ToString();
    //            Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //        }
    //        //Response.Redirect("Checkout.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
    //    }
    //}

    //public void pageno(int totItems)
    //{
    //    int pgCount = 0;
    //    // Calculate total numbers of pages
    //    if (totItems % 9 == 0)
    //    {
    //        pgCount = (totItems / 9);
    //    }
    //    else
    //    {
    //        pgCount = (totItems / 9) + 1;
    //    }
    //    string page = "";
    //    if (pgCount > 0)
    //    {

    //        page =
    //                "<table style=\"padding-top: 50px; float: right;width: 100%;\"><tr>";
    //        for (int i = 1; i <= pgCount; i++)
    //        {
    //            page += "<td><a class=\"jp-number\" href='IndexOld.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&PageNo=" + i + "'> " + i + " </a></td>";
    //        }
    //        page += "<tr></table>";
    //    }
    //    PageNo.InnerHtml = page;
    //}


    public void BindData()
    {
        string isfreetitle = "";
        string buynowtitle = "";
        string addtolibrarytitle = "";
        string newrelease = "";
        string viewallbook = "";
        viewallbook = Localization.ResourceManager.GetString("viewallbook", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        newrelease = Localization.ResourceManager.GetString("newrelease", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        buynowtitle = Localization.ResourceManager.GetString("buynow", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        isfreetitle = Localization.ResourceManager.GetString("Free", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
        addtolibrarytitle = Localization.ResourceManager.GetString("AddtoLibrary", System.Threading.Thread.CurrentThread.CurrentCulture.Name);       
        DataTable dt = new DataTable();
        if (Request.QueryString["l"] != null && Request.QueryString["l"].ToString() != "")
        {
            if (Request.QueryString["l"].ToString() == "es-ES")
            {
                Obj_Book.LanguageID = 2;
            }
            else if (Request.QueryString["l"].ToString() == "en-US")
            {
                Obj_Book.LanguageID = 1;
            }
        }
        else
        {
            Obj_Book.LanguageID = 1;
        }

        if (Request.QueryString["PageNo"] != null && Request.QueryString["PageNo"].ToString() != "")
        {
            //Obj_Book.EndIndex = Convert.ToInt32(Request.QueryString["PageNo"].ToString()) * 9;
            Obj_Book.EndIndex = -1;
            Obj_Book.StartIndex = (Obj_Book.EndIndex - 9) + 1;
        }
        else
        {
            //Obj_Book.EndIndex = 9;
            Obj_Book.EndIndex = -1;
            Obj_Book.StartIndex = 1;
        }        
        dt = Obj_Book.get_all_book_website1();
        if (dt != null && dt.Rows.Count > 0)
        {
            int count = Convert.ToInt32(dt.Rows[0]["count"]);
            //pageno(count);
        }
      
        string a1 = "<div class='panel'>" +
                        "<div class='titnewre'>" + newrelease.ToString() + "</div>" +
                        "<a href='AllBooks.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='titnewre1'>" + viewallbook.ToString() + "</a>" +
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
                    str = str + "<a href='Book-Detail.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "'  alt='' height='306' width='223'/></a>";
                }
                else
                {
                    str = str + "<div class='bookimg'>" +
                              "<a href='Book-Detail.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "'> <img src='" + img + "' alt='' height='306' width='223' /></a>";
                    if (Convert.ToBoolean(dr["IsFree"]) != true && Convert.ToInt32(dr["DiscountPrice"])>0)
                    {
                        str = str + "<div class='ad'>" + dr["DiscountPrice"] + "%<br /> off</div>";
                    }
                    str = str + "</div>";
                }
                str = str + "<div class='namkl2'><a href = 'Book-Detail.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' style='color:#616161'>" + dr["Title"] + "</a></div>" +
                      "<div class='author'>" + dr["Autoher"] + "</div>";
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<div class='namkl'>" + isfreetitle.ToString() + "</div>";
                }
                else
                {
                    str = str + "<div class='namkl'>$" + dr["FinalPrice"] + "</div>";
                }
                if (Convert.ToBoolean(dr["IsFree"]) == true)
                {
                    str = str + "<a href='IndexOld.aspx?fbid=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + addtolibrarytitle.ToString() + "</a>" +
                "</div>";
                }
                else
                {
                    str = str + "<a href='IndexOld.aspx?id=" + HttpUtility.UrlEncode(S.Encrypt(Convert.ToString(dr["BookID"]))) + "&l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "' class='boxbut'>" + buynowtitle.ToString() + "</a>" +
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