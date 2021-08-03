using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net.Mime;
using System.Text.RegularExpressions;
public partial class Admin_AddCategory : System.Web.UI.Page
{ 
    CategoryBAL objCategory = new CategoryBAL();

    public static string Lables;
    DataTable DT = new DataTable();

    MenuBAL objmenu = new MenuBAL();
    #region  //Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {            
            this.Form.DefaultButton = btnSubmit.UniqueID;
            bindLables();
        }
    }
    private void bindLables()
    {
        Lables = "";
        LanguageBAL LB = new LanguageBAL();
        DataTable DT = LB.GetLanguage();

        if (DT != null)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                Lables += "   <tr> "
                   + "     <td class=\"popup_listing_border_search\" colspan=\"3\" id=\"searchmsg\" align=\"center\" height=\"5\" style=\"font-size: 22px; font-weight: bold;    padding: 5px 0 !important;    text-align: left;\"> " + DT.Rows[i]["Language"] 
                   + "     </td> "
                   + " </tr>";

                objCategory.LanguageID = Convert.ToInt64(DT.Rows[i]["ID"]);
                DataTable dt = objCategory.GetLocalization();


                if (Request.QueryString["name"] != "")
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "Name like '" + Request.QueryString["name"] + "'";
                    dt = dv.ToTable();
                }

                string CategoryValue = "";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    Lables += "   <tr class=\"light_bg\"> "
                                 + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                                 + "         <strong>" + dt.Rows[j]["Name"] + " :</strong> "
                                 + "     </td> "
                                 + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                                 + "         &nbsp; "
                                 + "     </td> "
                                 + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                                 + " <input type=\"hidden\" name=\"id\" value=" + dt.Rows[j]["LocalizationID"] + " >"
                                 + " <input type=\"text\" name=\"value\" class=\"input_box user1\" value=\"" + dt.Rows[j]["Value"] + "\" >"
                                 + "         <font class=\"required\">*</font> "
                                 + "     </td> "
                                 + " </tr> ";
                }
            }
        }
    }

   

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
          
            int CatID = 0;
            string[] id = Request.Form.GetValues("id");
            string[] value = Request.Form.GetValues("value");
            for (int i = 0; i < value.Length; i++)
            {

                objCategory.IsActive = true;
                objCategory.LanguageID = Convert.ToInt32(id[i]);
                objCategory.CategoryName = value[i];

                objCategory.InsertUpdateLocalization();
              
            }
            Response.Redirect("ManageLables.aspx?edit=true");
            bindLables();
        }
        catch (Exception ee)
        {
           
        }


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageLables.aspx");
    }
    #endregion

   
}