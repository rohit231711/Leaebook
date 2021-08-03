using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_AllBooks : System.Web.UI.Page
{

    CategoryBAL ObjCategory = new CategoryBAL();
    BookBAL ObjBook = new BookBAL();
    DataTable dt = new DataTable();
    DataTable dt1 = new DataTable();

    public string Catname
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["CatName"])) ? Convert.ToString(ViewState["CatName"]) : ""; }
        set { ViewState["CatName"] = value; }
    }
    public string SubCatName
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["SubCatName"])) ? Convert.ToString(ViewState["SubCatName"]) : ""; }
        set { ViewState["SubCatName"] = value; }
    }


    public int CatID
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["CatID"])) ? Convert.ToInt32(ViewState["CatID"]) : 0; }
        set { ViewState["CatID"] = value; }
    }
    public int SubCatID
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["SubCatID"])) ? Convert.ToInt32(ViewState["SubCatID"]) : 0; }
        set { ViewState["SubCatID"] = value; }
    }


    public string Type
    {
        get { return !string.IsNullOrEmpty(Convert.ToString(ViewState["Type"])) ? Convert.ToString(ViewState["Type"]) : ""; }
        set { ViewState["Type"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        if (!IsPostBack)
        {

            if (Request.QueryString["t"] != null)
            {
                Type = Convert.ToString(Request.QueryString["t"]);

            }

            if (Request.QueryString["catid"] != null)
            {
                CatID = Convert.ToInt32(Request.QueryString["catid"]);
            }
            if (Request.QueryString["subcatid"] != null)
            {
                SubCatID = Convert.ToInt32(Request.QueryString["subcatid"]);
            }
           
            if (Request.QueryString["t"] != null)
            {
                BindCategory();

            }
            BindBooks();

        }

    }

    public void BindBooks()
    {

        if (Type == "Featured")
        {
            ObjBook.IsFeatured = 1;
            ObjBook.CategoryID = CatID;
            ObjBook.CategoryID = SubCatID;
            dtlistBooks.DataSource = ObjBook.BookIssueList();
            dtlistBooks.DataBind();

        }
        else if (Type == "Top10")
        {
            ObjBook.CategoryID = CatID;
            ObjBook.CategoryID = SubCatID;
            dtlistBooks.DataSource = ObjBook.SelectTop10Books();
            dtlistBooks.DataBind();

        }
        else if (Type == "New Arrival")
        {
            ObjBook.IsNewArrival = 1;
            ObjBook.CategoryID = CatID;
            ObjBook.CategoryID = SubCatID;
            dtlistBooks.DataSource = ObjBook.BookIssueList();
            dtlistBooks.DataBind();



        }

    }
    public void BindCategory()
    {
        dtcat.DataSource = ObjCategory.SelectAllCartegory();
        dtcat.DataBind();

    }
    //public void BindCategory()
    //{
    //    ObjCategory.CategoryID = Convert.ToInt64(CatID);
    //    dtsubcat.DataSource = ObjCategory.SelectAllCategoryByCategory();

    //    dtsubcat.DataBind();


    //    if (CatID > 0)
    //    {
    //        ObjCategory.CategoryID = CatID;

    //        dt = ObjCategory.SelectCategoryByID();
    //        Catname = dt.Rows[0]["CategoryName"].ToString();
    //    }
    //    else
    //    {
    //        ObjCategory.CategoryID = SubCatID;
    //        dt = ObjCategory.SelectCategoryByID();
    //        ObjCategory.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
    //        CatID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
    //        dt1 = ObjCategory.SelectCategoryByID();
    //        if (dt.Rows.Count > 0)
    //        {

    //            SubCatName = dt.Rows[0]["CategoryName"].ToString();
    //            Catname = dt1.Rows[0]["CategoryName"].ToString();
    //        }
    //    }


    //    atosub.HRef = "AllBooks.aspx?t=" + Type + "&catid=" + CatID + "&subcatid=" + SubCatID;
    //    atocat.HRef = "AllBooks.aspx?t=" + Type + "&catid=" + CatID;

    //    dtcat.Visible = false;
    //    dtsubcat.Visible = true;



    //}


}