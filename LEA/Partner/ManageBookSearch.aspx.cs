using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Text;
using System.Web;

public partial class Partner_ManageBookSearch : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();
    CategoryBAL objCategory = new CategoryBAL();
    MenuBAL objmenu = new MenuBAL();
    DataTable DT = new DataTable();
    int pageSize = 20;
    int pageIndex = 1;
    int totalRecords;
    Boolean view = false, edit = false, delete = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCategoryAndCategory();
        }
    }

    private void BindCategoryAndCategory()
    {
        objCategory.LanguageID = 1;
        ddlCategorydropdwon.DataSource = objCategory.SelectAllCartegory();
        ddlCategorydropdwon.DataTextField = "CategoryName";
        ddlCategorydropdwon.DataValueField = "CategoryID";
        ddlCategorydropdwon.DataBind();
        ddlCategorydropdwon.Items.Insert(0, new ListItem("Select", "0"));

        //ddlBookType.DataSource = objCategory.SelectAllCategory();
        //ddlBookType.DataTextField = "CategoryName";
        //ddlBookType.DataValueField = "CategoryID";
        //ddlBookType.DataBind();
        //ddlBookType.Items.Insert(0, new ListItem("Select", "0"));
      
       // BindPubliser();
    }

    protected void btnsearchbtn_Click(object sender, EventArgs e)
    {
        if (ddlCategorydropdwon.SelectedValue != "0")
            Session["CategoryID"] = ddlCategorydropdwon.SelectedValue;
        if (txtBookTitle.Value != "")
            Session["Title"] = txtBookTitle.Value;
        if (txtAuthorName.Value != "")
            Session["Autoher"] = txtAuthorName.Value;
        if (txteBookLanguage.Value != "")
            Session["Language"] = txteBookLanguage.Value;
        if (txtFinalPrice.Value != "")
            Session["FinalPrice"] = txtFinalPrice.Value;
        if (txtCreatedOn.Value != "")
            Session["CreatedOn"] = txtCreatedOn.Value;
        ScriptManager.RegisterStartupScript(Page, typeof(Page), "fancyboxClose", "fancyboxClose();", true);
    }
   
    //private void BindPubliser()
    //{
    //    DataTable dt = objBook.SelectAllBook();

    //    string[] array = new string[dt.Rows.Count];
    //    for (int j = 0; j < dt.Rows.Count; j++)
    //    {

    //        bool Duplicate = false;
    //        if (array.Length > 0)
    //        {

    //            for (int i = 0; i < array.Length; i++)
    //            {
    //                if (dt.Rows[j]["Publisher"].ToString() == array[i])
    //                {
    //                    Duplicate = true;
    //                    break;
    //                }
    //            }
    //        }
    //        array[j] = dt.Rows[j]["Publisher"].ToString();
    //        if (Duplicate)
    //        {
    //            dt.Rows.Remove(dt.Rows[j]);
    //        }
    //    }

    //    ddlPublisher.DataSource = dt;
    //    ddlPublisher.DataTextField = "Publisher";
    //    ddlPublisher.DataValueField = "Publisher";
    //    ddlPublisher.DataBind();
    //    ddlPublisher.Items.Insert(0, new ListItem("Select", "0"));
    //}

    protected void ddlCategorydropdwon_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void ddlBookType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}