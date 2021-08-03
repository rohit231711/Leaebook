using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
public partial class Client_Explore : System.Web.UI.Page
{
    BookBAL ObjBook;

    CategoryBAL ObjCategory = new CategoryBAL();
    CategoryBAL ObjCatgory = new CategoryBAL();
    DataTable dt;
    PagedDataSource adsource;
    int pos;
    public string Catname
    {
        get;
        set;
    }
    public string SubCatName
    {
        get;
        set;
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLanguage();


            //if (Request.QueryString["subcatid"] != null)
            //{
            //    SubCatID = Convert.ToInt32(Request.QueryString["subcatid"].ToString());
            //    ObjCategory.CategoryID = SubCatID;
            //    dt = ObjCategory.SelectCategoryByID();
            //    CatID = Convert.ToInt32(ObjCategory.SelectCategoryByID().Rows[0]["CategoryID"]);
            //    if (dt.Rows.Count > 0)
            //    {
            //        SubCatName = dt.Rows[0]["CategoryName"].ToString();

            //        ObjCategory.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
            //        Catname = ObjCategory.SelectCategoryByID().Rows[0]["CategoryName"].ToString();

            //        atosub.HRef = "Explore.aspx?subcatid=" + SubCatID;
            //        atocat.HRef = "Explore.aspx?catid=" + CatID;
            //        DataList dtsubcat = (DataList)this.Master.FindControl("dtsubcat");
            //        DataList rpt = (DataList)this.Master.FindControl("rptcategory");
            //        Label lblBox = (Label)this.Master.FindControl("lblText");
            //        lblBox.Text = "Category";
            //        rpt.Visible = false;
            //        dtsubcat.Visible = true;
            //        ObjCategory.CategoryID = Convert.ToInt64(CatID);
            //        dtsubcat.DataSource = ObjCategory.SelectAllCategoryByCategory();

            //        dtsubcat.DataBind();

            //    }

            //}
            if (Request.QueryString["catid"] != null)
            {
                CatID = Convert.ToInt32(Request.QueryString["catid"].ToString());
                ObjCategory.CategoryID = CatID;
                Catname = ObjCategory.SelectAllCategoryPaging(1, 1).Rows[0]["CategoryName"].ToString();
                atocat.HRef = "Explore.aspx?catid=" + CatID;
                DataList dtsubcat = (DataList)this.Master.FindControl("dtsubcat");
                DataList rpt = (DataList)this.Master.FindControl("rptcategory");
                Label lblBox = (Label)this.Master.FindControl("lblText");
                lblBox.Text = "Sub Category";
                rpt.Visible = false;
                dtsubcat.Visible = true;
                //ObjCategory.CategoryID = Convert.ToInt64(CatID);
                //dtsubcat.DataSource = ObjCategory.SelectAllCategoryByCategory();

                //dtsubcat.DataBind();
            }



            this.ViewState["vs"] = 0;

            pos = (int)this.ViewState["vs"];
            BindFreeBooks();
        }
    }

    public void BindFreeBooks()
    {

        ObjBook = new BookBAL();
        //ObjBook.LangaugeID = Convert.ToInt32(ddlLanguage.SelectedValue);
        ObjBook.CategoryID = CatID;
        ObjBook.CategoryID = SubCatID; ;
        ObjBook.WsIsFree = 1;
        ObjBook.CurrentPage = 1;
        ObjBook.IsPublish = 1;
        ObjBook.Rows = 400000000;

        adsource = new PagedDataSource();


        dt = ObjBook.BookIssueList();
        adsource.DataSource = dt.DefaultView;
        adsource.PageSize = 4;
        adsource.AllowPaging = true;
        adsource.CurrentPageIndex = pos;

        btnfirst.Enabled = !adsource.IsFirstPage;
        btnprevious.Enabled = !adsource.IsFirstPage;
        btnlast.Enabled = !adsource.IsLastPage;
        btnnext.Enabled = !adsource.IsLastPage;

        dtlistBooks.DataSource = adsource;
        dtlistBooks.DataBind();

        dt = null;
    }
    protected void btnfirst_Click(object sender, EventArgs e)
    {
        pos = 0;
        BindFreeBooks();
    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["vs"];
        pos -= 1;
        this.ViewState["vs"] = pos;
        BindFreeBooks();
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        pos = (int)this.ViewState["vs"];
        pos += 1;
        this.ViewState["vs"] = pos;
        BindFreeBooks();
    }

    protected void btnlast_Click(object sender, EventArgs e)
    {
        adsource = new PagedDataSource();
        ObjBook = new BookBAL();
        //ObjBook.LangaugeID = Convert.ToInt32(ddlLanguage.SelectedValue);
        ObjBook.CategoryID = CatID;
        ObjBook.CategoryID = SubCatID; ;
        ObjBook.WsIsFree = 1;
        ObjBook.CurrentPage = 1;
        ObjBook.Rows = 400000000;


        dt = ObjBook.BookIssueList();
        adsource.DataSource = dt.DefaultView;
        adsource.PageSize = 4;
        pos = (adsource.DataSourceCount / adsource.PageSize);
        this.ViewState["vs"] = pos;
        BindFreeBooks();
    }
    public void BindLanguage()
    {
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllLanguage();
        if (dt.Rows.Count > 0)
        {
            //ddlLanguage.DataSource = dt;
            //ddlLanguage.DataTextField = "Language";
            //ddlLanguage.DataValueField = "ID";
            //ddlLanguage.DataBind();
            //ddlLanguage.Items.Insert(0, new ListItem("Select", "0"));
            //ddlLanguage.SelectedIndex = 0;
        }
    }
    public void ddlLanguage_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        BindFreeBooks();


    }
}