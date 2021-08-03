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

public partial class Admin_AddAdvertisements : System.Web.UI.Page
{
    AdvertisementsBAL Adv = new AdvertisementsBAL();
    public static string Advertisements;
    DataTable DT = new DataTable();
    MenuBAL objmenu = new MenuBAL();

    #region  //Property
    public int AdvertisementID
    {
        get
        {
            int id = 0;
            if (Request.QueryString["AdvertisementID"] != null || Request.QueryString["AdvertisementID"] != "")
            {
                id = Convert.ToInt32(Request.QueryString["AdvertisementID"]);
                return id;
            }
            return id;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Form.DefaultButton = btnSubmit.UniqueID;
            accessrights();
            if (AdvertisementID > 0)
            {
                Adv = new AdvertisementsBAL();
                Adv.AdvertisementID = this.AdvertisementID;
                DataTable dt = Adv.SelectAdvertisementByID();
                if (dt.Rows.Count > 0)
                {
                    imgcat.Visible = true;
                    hnfImage.Value = "1";
                    imgcat.ImageUrl = "~/Advertisements/" + dt.Rows[0]["AdvertisementImage"].ToString();
                    txtAdvertisementURL.Text = dt.Rows[0]["LinkURL"].ToString();
                }
            }
            bindAdvertisements();
        }
    }

    private void bindAdvertisements()
    {
        Advertisements = "";
        LanguageBAL LB = new LanguageBAL();
        DataTable DT = LB.GetLanguage();

        if (DT != null)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                Adv.AdvertisementID = this.AdvertisementID;
                Adv.LanguageID = Convert.ToInt64(DT.Rows[i]["ID"]);
                DataTable dt = Adv.SelectAdvertisementByID();
                string TitleValue = "";
                string DescriptionValue = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    TitleValue = dt.Rows[0]["Title"].ToString();
                    DescriptionValue = dt.Rows[0]["Description"].ToString();
                }
                Advertisements += "   <tr class=\"light_bg\"> "
                             + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                             + "         <strong>Advertisement Title (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                    //+ "         <asp:TextBox CssClass=\"input_box\" ID=\"txtCategoryName\" runat=\"server\"></asp:TextBox> "
                             + " <input type=\"hidden\" name=\"languageid\" value=" + DT.Rows[i]["ID"] + " >"
                             + " <input type=\"text\" name=\"Advertisement\" ID=\"" + DT.Rows[i]["Language"] + "\" class=\"input_box user1\" value=\"" + TitleValue + "\" >"
                             + "         <font class=\"required\">*</font> "
                             + "     </td> "
                             + " </tr> ";

                            // + "   <tr class=\"light_bg\"> "
                            //+ "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                            //+ "         <strong>Description (" + DT.Rows[i]["Language"] + ") :</strong> "
                            // + "     </td> "
                            // + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                            // + "         &nbsp; "
                            // + "     </td> "
                            // + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                            ////+ "         <asp:TextBox CssClass=\"input_box\" ID=\"txtCategoryName\" runat=\"server\"></asp:TextBox> "
                            // //+ " <input type=\"hidden\" name=\"languageid\" value=" + DT.Rows[i]["ID"] + " >"
                            // + " <textarea cols=\"80\"  name=\"Desc\" class=\"input_box user1\"  >" + DescriptionValue + "</textarea>"
                            // + "         <font class=\"required\">*</font> "
                            // + "     </td> "
                            // + " </tr> ";
            }
        }
    }

    private void accessrights()
    {
        int fl = 0;
        objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
        DT = objmenu.GetRightsByUser();
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 8)
                {
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 1)
                    {
                        fl = 1;
                    }
                }
            }
            if (fl == 0)
            {
                Response.Redirect("accessdenied.aspx");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int a = 0;
        string subPath = "~/Advertisements"; // your code goes here
        try
        {
            //objCategory.CategoryName = txtCategoryName.Text.Trim();
            string fileName = "";
            if (fpUpload.HasFile)
            {
                bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));
                if (!IsExists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                fileName = Path.GetFileName(fpUpload.FileName);
                bool IsExitsFile = System.IO.File.Exists(Server.MapPath(subPath + "/" + fileName));
                if (!IsExitsFile)
                {
                    fpUpload.PostedFile.SaveAs(Server.MapPath(subPath + "/" + fileName));
                    GenerateThumbnails(420, 280, (Server.MapPath(subPath + "/" + fileName)), subPath + "/" + "ss_" + fileName);
                }                
                imgcat.ImageUrl = "~/Advertisements/" + fileName;
            }
         
            if (Convert.ToBoolean(Request.QueryString["EDIT"]))
            {
                if (fpUpload.HasFile)
                {
                    try
                    {
                        if (!System.IO.Directory.Exists(Server.MapPath(subPath)))
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                        fileName = Path.GetFileName(imgcat.ImageUrl);


                        foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcessesByName(Server.MapPath(subPath + "/" + fileName)))
                        {
                            process.Kill();
                            process.WaitForExit();
                        }                 
                    }
                    catch
                    { }
                }                
            }
            fileName = Path.GetFileName(imgcat.ImageUrl);
            int ADVID = 0;
            string[] Advertisement = Request.Form.GetValues("Advertisement");
            string[] LanguageID = Request.Form.GetValues("languageid");
            //string[] Description = Request.Form.GetValues("Desc");
            ADVID = AdvertisementID;            
            
            for (int i = 0; i < LanguageID.Length; i++)
            {
                Adv.ISActive = true;
                Adv.AdvertisementImage = fileName;
                Adv.LanguageID = Convert.ToInt32(LanguageID[i]);
                Adv.Title = Advertisement[i];
                //Adv.Description = Description[i];
                Adv.Description = "";
                Adv.AdvertisementID = ADVID;
                Adv.LinkURL = txtAdvertisementURL.Text.ToString().Trim();
                a = Adv.InsertUpdateAdvertisement();
                ADVID = a;                                
            }            
            if (a > 0)
            {
                if (AdvertisementID > 0)
                    Response.Redirect("ManageAdvertisements.aspx");
                else
                    Response.Redirect("ManageAdvertisements.aspx?add=true");                
                clear();
            }
        }
        catch (Exception ex)
        {           
        }
    }

    private void clear()
    {
        //txtCategoryName.Text = string.Empty;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clear();
        Response.Redirect("ManageAdvertisements.aspx");
    }

    private void GenerateThumbnails(int height, int width, string sourcePath, string targetPath)
    {
        Bitmap image = new Bitmap(sourcePath);
        {
            var newWidth = (int)(width);
            var newHeight = (int)(height);
            var thumbnailImg = new Bitmap(newWidth, newHeight);
            var thumbGraph = Graphics.FromImage(thumbnailImg);
            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
            var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
            thumbGraph.DrawImage(image, imageRectangle);
            thumbnailImg.Save(Server.MapPath(targetPath));
        }
    }

}