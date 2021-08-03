using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using Cyotek.GhostScript.PdfConversion;
using Cyotek.GhostScript;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Net.Mime;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Xml;
using iTextSharp.text.pdf;
using System.Diagnostics;
using System.Threading;
using SwfDotNet.IO;
using SwfDotNet.IO.Tags;
using SwfDotNet.IO.Tags.Types;
using System.Web.Services;
using System.Web.Script.Services;
using iTextSharp.text;
using iDiTect.Converter;

public partial class Admin_AddBookIssue : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();
    BookShippingBAL objBookShip = new BookShippingBAL();

    DataTable DT = new DataTable();
    RegistrationBAL objuser = new RegistrationBAL();
    MenuBAL objmenu = new MenuBAL();
    BookIssueBAL mib = new BookIssueBAL();

    string destinationDirectory = System.Configuration.ConfigurationManager.AppSettings["DestinationDirectory"];
    private string userName = "User";
    private string uploadedPDFRepository = "";
    private string convertedSWFRepository = "";
    private int PdfStartpage = 1;
    private int pdfEndpage = 5;
    public string search_text = "";
    public static string BookTitleDescription;

    public Int64 BookID
    {
        get { return ViewState["BookID"] != null ? Convert.ToInt64(ViewState["BookID"]) : 0; }
        set { ViewState["BookID"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCountry();
            this.Form.DefaultButton = btnSubmit.UniqueID;
            txtDate1.Text = "";
            Error.Visible = false;
            BindDropDownOrder();
            BindCategory();
            if (Request.QueryString["BookID"] != null)
            {
                EditMode.Value = "true";
                objBook.BookID = Convert.ToInt64(Request.QueryString["BookID"]);
                hdnBookID.Value = Request.QueryString["BookID"].ToString();
                DataTable dt = objBook.SelectAllBookIssuePaging(1, 10);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["CategoryID"] != null && dt.Rows[0]["CategoryID"].ToString() != "")
                        objBook.BookID = Convert.ToInt32(objBook.ID);
                    DataTable dt2 = objBook.GetDescriptionImagesByIssue();

                    if (dt2.Rows.Count > 0)
                    {
                        var descriptionImageURL = dt2.Rows[0]["DescriptionImages"];

                        if (descriptionImageURL.ToString().Contains(objBook.BookID + "_"))
                        {
                            string SplitWord = "_" + objBook.BookID.ToString() + "_";
                            string[] imgname = descriptionImageURL.ToString().Split('_');
                            string Imagename = imgname[imgname.Count() - 1].ToString();
                            string DescriptionPage = Imagename.Split('.')[Imagename.Split('.').Count() - 2];
                            txtDescriptionPages.Text = 1.ToString();// DescriptionPage;
                        }
                    }

                    txtDate1.Text = Convert.ToDateTime(dt.Rows[0]["PublishDate"]).ToString("dd/MM/yyyy");
                    ViewState["DescriptionImages"] = Convert.ToString(dt.Rows[0]["DescriptionImages"]);
                    txtDiscount.Text = dt.Rows[0]["DiscountPrice"].ToString();
                    //if (!string.IsNullOrEmpty(dt.Rows[0]["FinalPrice"].ToString()))
                    //{
                    txtFinalPrice.Text = dt.Rows[0]["FinalPrice"].ToString();
                    //}

                    txtAuthorName.Text = dt.Rows[0]["Autoher"].ToString();
                    txtLanguage.Text = dt.Rows[0]["Language"].ToString();
                    ddlCategory.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
                    txtPrice.Text = dt.Rows[0]["Price"].ToString().Replace(',', '.');
                    chkIsFree.Checked = Convert.ToBoolean(dt.Rows[0]["IsFree"].ToString());
                    chkIsactive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
                    chkIsFeartued.Checked = Convert.ToBoolean(dt.Rows[0]["IsFeatured"].ToString());
                    chkIsSpecial.Checked = Convert.ToBoolean(dt.Rows[0]["isspecial"].ToString());
                    ddlOrderIndex.SelectedValue = dt.Rows[0]["OrderIndex"].ToString();
                    chkSpecial.Checked = Convert.ToBoolean(dt.Rows[0]["SpecialOffer"].ToString());
                    chkeBook.Checked = Convert.ToBoolean(dt.Rows[0]["IseBook"].ToString());
                    chkPapaerBook.Checked = Convert.ToBoolean(dt.Rows[0]["IsPaperBook"].ToString());
                    chkIsPaperFree.Checked = Convert.ToBoolean(dt.Rows[0]["IsFreePaper"].ToString());
                    if (!string.IsNullOrEmpty(dt.Rows[0]["Quantity"].ToString()))
                    {
                        txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                    }
                    txtWeight.Text = dt.Rows[0]["Weight"].ToString();
                    txtDimWeight.Text = dt.Rows[0]["DimWeight"].ToString();
                    txtWidth.Text = dt.Rows[0]["Width"].ToString();
                    txtHeight.Text = dt.Rows[0]["Height"].ToString();
                    txtDepth.Text = dt.Rows[0]["Depth"].ToString();
                    txtPaperBookDiscount.Text = dt.Rows[0]["PaperBookDiscount"].ToString().Replace(',', '.');
                    txtFinalPaperBookPrice.Text = dt.Rows[0]["PaperBookFinalPrice"].ToString().Replace(',', '.');
                    txtPaperBookPrice.Text = dt.Rows[0]["PaperBookPrice"].ToString().Replace(',', '.');
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "finalprice();", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFun", "paperBookfinalprice();", true);

                    if (!string.IsNullOrEmpty(dt.Rows[0]["SpecialOfferStart"].ToString()))
                    {
                        txtSpecialOfferStart.Text = Convert.ToDateTime(dt.Rows[0]["SpecialOfferStart"]).ToString("dd/MM/yyyy");
                    }
                    if (!string.IsNullOrEmpty(dt.Rows[0]["SpecialOfferEnd"].ToString()))
                    {
                        txtSpecialOfferEnd.Text = Convert.ToDateTime(dt.Rows[0]["SpecialOfferEnd"]).ToString("dd/MM/yyyy");
                    }
                    txtDealerEmail.Text = dt.Rows[0]["DealerEmail"].ToString();
                    if (dt.Rows[0]["ExplorerPdfStartNo"] != null && Convert.ToInt32(dt.Rows[0]["ExplorerPdfStartNo"]) > 0)
                    {
                        txtPDFStartPage.Text = dt.Rows[0]["ExplorerPdfStartNo"].ToString();
                    }
                    else
                    {
                        txtPDFStartPage.Text = 1.ToString();
                    }

                    if (dt.Rows[0]["ExplorerPdfEndNo"] != null && Convert.ToInt32(dt.Rows[0]["ExplorerPdfEndNo"]) > 0)
                    {
                        txtPDFEndPage.Text = dt.Rows[0]["ExplorerPdfEndNo"].ToString();
                    }
                    else
                    {
                        txtPDFEndPage.Text = 1.ToString();
                    }
                    imgebind();

                    imgbook.Visible = true;
                    imgbook.ImageUrl = "~/Book/" + ddlCategory.SelectedValue + "/" + dt.Rows[0]["BookImage"].ToString();
                    if (File.Exists(Server.MapPath("~/Book/" + ddlCategory.SelectedValue + "/" + Request.QueryString["BookID"] + ".pdf")))
                    {
                        a_book.HRef = "~/Book/" + ddlCategory.SelectedValue + "/" + Request.QueryString["BookID"] + ".pdf";
                        string filelocation = Server.MapPath("~/Book/" + ddlCategory.SelectedValue + "/" + Request.QueryString["BookID"] + ".pdf");
                        fuPdfUpload = new FileUpload();
                        fuPdfUpload.SaveAs(filelocation);
                        rblType.SelectedValue = "pdf";
                    }
                    else if (File.Exists(Server.MapPath("~/Book/" + ddlCategory.SelectedValue + "/" + Request.QueryString["BookID"] + ".epub")))
                    {
                        a_book.HRef = "~/Book/" + ddlCategory.SelectedValue + "/" + Request.QueryString["BookID"] + ".epub";
                        rblType.SelectedValue = "ePub";
                    }


                    


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMy", "BookTypeDiv();", true);
                }
            }
            bindCategories();
        }
    }

    private void BindCountry()
    {
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllActiveCountry();

        if (dt.Rows.Count > 0)
        {
            //ddlCountry.DataSource = dt;
            //ddlCountry.DataTextField = "countryname";
            //ddlCountry.DataValueField = "countryid";
            //ddlCountry.DataBind();
        }

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string AddmultiRow(string BusinessRuleID)
    {
        string option = "";
        string guid = "";
        string str = "";
        Country objCountry = new Country();
        DataTable dt = objCountry.SelectAllActiveCountry();
        var Guid = System.Guid.NewGuid();
        guid = Guid.ToString();
        DataView dvType = new DataView(dt);
        //dvType.RowFilter = " ListType=1";
        if (dvType != null)
        {
            for (int j = 0; j < dvType.Count; j++)
            {
                option = option + "<option value = " + dt.Rows[j]["countryid"].ToString() + ">" + dt.Rows[j]["countryname"].ToString() + "</option>";
            }
        }

        if (BusinessRuleID == "-1" || string.IsNullOrEmpty(BusinessRuleID))
        {
            str = str + "<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + Guid + "addnewmultiplerowpanel\">"
                           + "<tr>"
                           + "<td>"
                           + "<select class=\"select_box text-input\" name=\"ddlCountry\" id=\"ddlCountry_" + Guid + "\" class=\"\" style=\"width: 97%;\">"
                           + "<option value=0>Select Country</option>"
                           + option
                           + "</select>"
                           + "</td>"
                           + "<td>"
                           + "<input type=\"text\" id=\"txtCharge_" + Guid + "\" name=\"txtCharge\" value=\"0\" onkeypress=\"return isNumberKey(event)\" class=\"input_box user1 charge_" + Guid + "\"/>"
                           + "</td>";
            str = str + "</td>"
                         + "<td align=\"left\" class=\"vardana12BoldBlue\" style=\"width: 22px; height: 15px; background: url(../../images/add-location.png) no-repeat 0 0; float: left; width: 90px; height: 27px; text-decoration: none; margin-left: 15px; width: 22px;\"><a data-toggle=\"modal\" href=\"#myModal\" onclick=\"AddNewRow('addnew');\" style=\"padding-left: 20px; margin-top: 10px; margin-left: 0px; \" class=\"button90\"><i></i>"
                         + "</td>";
            str = str + "</tr>"
                + "</table>";

            return str + '@' + guid;
        }
        else if (BusinessRuleID == "addnew")
        {
            Guid = System.Guid.NewGuid();
            guid = Guid.ToString();

            str = str + "<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + Guid + "addnewmultiplerowpanel\">"
                           + "<tr>"
                           + "<td>"
                           + "<select class=\"select_box text-input\" name=\"ddlCountry\" id=\"ddlCountry_" + Guid + "\" class=\"\" style=\"width: 97%;\">"
                           + "<option value=0>Select Country</option>"
                           + option
                           + "</select>"
                           + "</td>"
                           + "<td>"
                           + "<input type=\"text\" id=\"txtCharge_" + Guid + "\" name=\"txtCharge\" value=\"0\" onkeypress=\"return isNumberKey(event)\" class=\"input_box user1 charge_" + Guid + "\"/>"
                           + "</td>";
            str = str + "</td>";
            str = str + "<td align=\"left\" class=\"vardana12BoldBlue\" style=\"width: 22px; height: 15px; background: url(../../images/erroe-cancel.png) no-repeat 0 0; float: left; width: 90px; height: 27px; text-decoration: none; margin-left: 15px; width: 22px;\"><a data-toggle=\"modal\" href=\"#myModal\" style=\"padding-left: 20px; margin-top: 10px; margin-left: 0px;\" class=\"button90\" onclick=\"return ValidationConfirmed('Are you sure you want to delete?','" + Guid + "','addnewmultiplerowpanel');\"><i></i>"
                             + "</td></tr>"
                             + "</table>";

            return str + '@' + guid;
        }
        else
        {
            BookShippingBAL objBookShip = new BookShippingBAL();
            DataTable dtBookCountry = new DataTable();
            string option1 = "";
            objBookShip.BookID = Convert.ToInt64(BusinessRuleID);
            dtBookCountry = objBookShip.getAllCountryByBook();
            if (dtBookCountry.Rows.Count > 0)
            {
                int i = 0;
                foreach (DataRow dr in dtBookCountry.Rows)
                {
                    if (dvType != null)
                    {
                        for (int j = 0; j < dvType.Count; j++)
                        {
                            if (Convert.ToInt32(dr["CountryID"]) == Convert.ToInt32(dvType[j]["countryid"]))
                            {
                                option1 = option1 + "<option value = " + dvType[j]["countryid"].ToString() + " selected>" + dvType[j]["countryname"].ToString() + "</option>";
                            }
                            else
                            {
                                option1 = option1 + "<option value = " + dvType[j]["countryid"].ToString() + ">" + dvType[j]["countryname"].ToString() + "</option>";
                            }
                        }
                    }

                    Guid = System.Guid.NewGuid();
                    guid = Guid.ToString();

                    str = str + "<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + Guid + "addnewmultiplerowpanel\">"
                   + "<tr>"
                   + "<td>"
                           + "<select class=\"select_box text-input\" name=\"ddlCountry\" id=\"ddlCountry_" + Guid + "\" class=\"\" style=\"width: 97%;\">"
                           + "<option value=0>Select Country</option>"
                           + option1
                           + "</select>"
                   + "</td>"
                           + "<td>"
                           + "<input type=\"text\" id=\"txtCharge_" + Guid + "\" name=\"txtCharge\" value=\" " + dr["ShippingCharge"].ToString() + "\" onkeypress=\"return isNumberKey(event)\" class=\"input_box user1 charge_" + Guid + "\"/>"
                           + "</td>";
                    str = str + "</td>";
                    if (i == 0)
                    {
                        str = str + "<td align=\"left\" class=\"vardana12BoldBlue\" style=\"width: 22px; height: 15px; background: url(../../images/add-location.png) no-repeat 0 0; float: left; width: 90px; height: 27px; text-decoration: none; margin-left: 15px; width: 22px;\"><a data-toggle=\"modal\" href=\"#myModal\" onclick=\"AddNewRow('addnew');\" style=\"padding-left: 20px; margin-top: 10px; margin-left: 0px; \" class=\"button90\"><i></i>"
                             + "</td></tr>"
                             + "</table>";
                    }
                    else
                    {
                        str = str + "<td align=\"left\" class=\"vardana12BoldBlue\" style=\"width: 22px; height: 15px; background: url(../../images/erroe-cancel.png) no-repeat 0 0; float: left; width: 90px; height: 27px; text-decoration: none; margin-left: 15px; width: 22px;\"><a data-toggle=\"modal\" href=\"#myModal\" style=\"padding-left: 20px; margin-top: 10px; margin-left: 0px;\" class=\"button90\" onclick=\"return ValidationConfirmed('Are you sure you want to delete?','" + Guid + "','addnewmultiplerowpanel');\"><i></i>"
                             + "</td></tr>"
                             + "</table>";
                    }
                    i++;
                }

            }
            else
            {
                Guid = System.Guid.NewGuid();
                guid = Guid.ToString();

                str = str + "<table cellpadding=\"0\" cellspacing=\"0\" class=\"" + Guid + "addnewmultiplerowpanel\">"
                           + "<tr>"
                           + "<td>"
                           + "<select class=\"select_box text-input\" name=\"ddlCountry\" id=\"ddlCountry_" + Guid + "\" class=\"\" style=\"width: 97%;\">"
                           + "<option value=0>Select Country</option>"
                           + option
                           + "</select>"
                           + "</td>"
                           + "<td>"
                           + "<input type=\"text\" id=\"txtCharge_" + Guid + "\" name=\"txtCharge\" value=\"0\" onkeypress=\"return isNumberKey(event)\" class=\"input_box user1 charge_" + Guid + "\"/>"
                           + "</td>";
                str = str + "</td>"
                             + "<td align=\"left\" class=\"vardana12BoldBlue\" style=\"width: 22px; height: 15px; background: url(../../images/add-location.png) no-repeat 0 0; float: left; width: 90px; height: 27px; text-decoration: none; margin-left: 15px; width: 22px;\"><a data-toggle=\"modal\" href=\"#myModal\" onclick=\"AddNewRow('addnew');\" style=\"padding-left: 20px; margin-top: 10px; margin-left: 0px; \" class=\"button90\"><i></i>"
                             + "</td></tr>"
                    + "</table>";
            }
            return str + '@' + guid;
           
        }
    }

    private void BindDropDownOrder()
    {
        int count = objBook.GetTotalBooksCount();
        ddlOrderIndex.Items.AddRange(Enumerable.Range(1, count + 1).Select(x => new System.Web.UI.WebControls.ListItem(x.ToString())).ToArray());
    }

    private void bindCategories()
    {
        BookTitleDescription = "";
        LanguageBAL LB = new LanguageBAL();
        DataTable DT = LB.GetLanguage();

        if (DT != null)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                string TitalValue = "";
                string DescriptionValue = "";

                if (Request.QueryString["BookID"] != null)
                {
                    objBook.BookID = Convert.ToInt64(Request.QueryString["BookID"]);
                    objBook.LangaugeID = Convert.ToInt32(DT.Rows[i]["ID"]);
                    DataTable dt = objBook.GetDynamicFieldsByBookIDLanguageID();
                    if (dt.Rows.Count > 0)
                    {
                        TitalValue = dt.Rows[0]["Title"].ToString();
                        DescriptionValue = dt.Rows[0]["Description"].ToString();
                    }
                }

                BookTitleDescription += "   <tr class=\"light_bg\"> "
                             + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                             + "         <strong> Book Title (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                             + "        <input type=\"hidden\" name=\"languageid\" value=" + DT.Rows[i]["ID"] + " >"
                             + "        <input type=\"text\" class=\"input_box user1 titleClass\" name=\"title\" value=\"" + TitalValue + "\" >"
                             + "        <font class=\"required\">*</font> "
                             + "     </td> "
                             + " </tr> "
                            + "   <tr class=\"light_bg\"> "
                            + "     <td class=\"popup_listing_border\" align=\"right\" valign=\"middle\" width=\"130\"> "
                            + "         <strong> Book Description (" + DT.Rows[i]["Language"] + ") :</strong> "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" height=\"37\" width=\"11\"> "
                             + "         &nbsp; "
                             + "     </td> "
                             + "     <td class=\"popup_listing_border\" align=\"left\" valign=\"middle\" width=\"459\"> "
                             + " <textarea cols=\"80\" style=\"height: 60px;width: 360px;\" name=\"Description\"  class=\"input_box DescriptionClass user1\"  >" + DescriptionValue + "</textarea>"
                             + "         <font class=\"required\">*</font> "
                             + "     </td> "
                             + " </tr> ";
            }
        }
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
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
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 5)
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

    private void imgebind()
    {
        objBook.BookID = Convert.ToInt64(Request.QueryString["ID"]);
        DataTable dtimage = objBook.GetAllBookIssueImageByBook();
        if (dtimage.Rows.Count > 0)
        {
            trimg.Visible = false;
            dlImages.DataSource = dtimage;
            dlImages.DataBind();
        }
        else
        {
            trimg.Visible = false;
        }
    }

    private void BindCategory()
    {
        CategoryBAL objCategory = new CategoryBAL();
        objCategory.LanguageID = 1;
        DataTable dt = objCategory.SelectAllCartegory();
        ddlCategory.Items.Clear();
        if (dt.Rows.Count > 0)
        {
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select", "0"));
            ddlCategory.SelectedIndex = 0;
        }
    }

    private void GenerateThumbnails(int height, int width, string sourcePath, string targetPath)
    {
        try
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
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(Server.MapPath(targetPath));
            }
        }
        catch (Exception)
        {
            
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //int pageno = 0;
        int count = 0;
        if (ddlCategory.SelectedIndex != 0 && txtAuthorName.Text.Trim().Length > 0 && txtLanguage.Text.Trim().Length > 0 && txtDealerEmail.Text.Trim().Length > 0
            && txtDate1.Text.Trim().Length > 0)
        {
            if (Request.QueryString["BookID"] != null && Request.QueryString["BookID"].ToString() != "")
                objBook.BookID = Convert.ToInt32(Request.QueryString["BookID"]);
            objBook.IsActive = chkIsactive.Checked;
            objBook.IsFree = chkIsFree.Checked;
            objBook.IsFeatured = Convert.ToInt32(chkIsFeartued.Checked);
            objBook.Published = true;
            objBook.PublishDate = DateTime.ParseExact(txtDate1.Text, "dd/MM/yyyy", null);
            objBook.IsSpecial = Convert.ToInt32(chkIsSpecial.Checked);
            objBook.ExplorerPdfStartNo = PdfStartpage;
            objBook.ExplorerPdfEndNo = pdfEndpage;
            objBook.UpdatedOn = DateTime.Now;
            objBook.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
            objBook.DescriptionImages = 1.ToString();
            objBook.Autoher = txtAuthorName.Text;
            objBook.OrderIndex = Convert.ToInt64(ddlOrderIndex.SelectedValue);
            objBook.DealerEmail = txtDealerEmail.Text;
            objBook.SpecialOffer = chkSpecial.Checked;
            objBook.IseBook = chkeBook.Checked;
            objBook.IsPaperBook = chkPapaerBook.Checked;
            objBook.IsFreePaper = chkIsPaperFree.Checked;
            if (chkPapaerBook.Checked)
            {
                objBook.Weight = txtWeight.Text;
                objBook.DimWeight = txtDimWeight.Text;
                objBook.Width = txtWidth.Text;
                objBook.Height = txtHeight.Text;
                objBook.Depth = txtDepth.Text;
            }
            if (!string.IsNullOrEmpty(txtQuantity.Text.Trim()))
                objBook.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
            else
                objBook.Quantity = 0;
            objBook.PartnerID = 0;

            if (txtSpecialOfferStart.Text.Trim().Length > 0)
            {
                objBook.SpecialOfferStart = Convert.ToDateTime(txtSpecialOfferStart.Text);
            }
            else
            {
                objBook.SpecialOfferStart = null;
            }
            if (txtSpecialOfferEnd.Text.Trim().Length > 0)
            {
                objBook.SpecialOfferEnd = Convert.ToDateTime(txtSpecialOfferEnd.Text);
            }
            else
            {
                objBook.SpecialOfferEnd = null;
            }

            if (chkIsFree.Checked == true)
            {
                txtPrice.Text = "";
                txtDiscount.Text = "";
                txtFinalPrice.Text = "";
                //txtPaperBookPrice.Text = "";
                //txtPaperBookDiscount.Text = "";
                //txtFinalPaperBookPrice.Text = "";
            }
            if (chkIsPaperFree.Checked == true)
            {
                //txtPrice.Text = "";
                //txtDiscount.Text = "";
                //txtFinalPrice.Text = "";
                txtPaperBookPrice.Text = "";
                txtPaperBookDiscount.Text = "";
                txtFinalPaperBookPrice.Text = "";
            }
            objBook.Price = txtPrice.Text != "" ? txtPrice.Text : "0";
            objBook.DiscountedPrice = txtDiscount.Text != "" ? Convert.ToInt64(txtDiscount.Text) : 0;
            objBook.FinalPrice = txtFinalPrice.Text;
            objBook.PaperBookPrice = txtPaperBookPrice.Text != "" ? txtPaperBookPrice.Text : "0";
            objBook.PaperBookDiscount = txtPaperBookDiscount.Text != "" ? txtDiscount.Text : "0";
            objBook.PaperBookFinalPrice = txtFinalPaperBookPrice.Text;
            objBook.Language = txtLanguage.Text;
            objBook.DescriptionImages = rblType.SelectedItem.Value;

            DataTable dt = objBook.SelectAllBook();
            if (dt != null && dt.Rows.Count > 0)
            {
                string[] title = Request.Form.GetValues("title");
                string[] languageid = Request.Form.GetValues("languageid");
                for (int i = 0; i < languageid.Length; i++)
                {
                    objBook.LanguageID = Convert.ToInt32(languageid[i]);
                    objBook.Title = title[i];

                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        if (!Convert.ToBoolean(dt.Rows[j]["IsDelete"].ToString()))
                        {
                            if (dt.Rows[j]["Title"].ToString() == objBook.Title)// && Convert.ToInt32(dt.Rows[j]["CategoryID"]) == Convert.ToInt32(ddlCategory.SelectedValue) && dt.Rows[j]["Autoher"].ToString() == txtAuthorName.Text && dt.Rows[j]["Language"].ToString() == txtLanguage.Text)
                            {
                                count++;
                            }
                        }
                    }
                }
                if (count > 0 && !Convert.ToBoolean(Request.QueryString["EDIT"]))
                {
                    //Response.Redirect(Request.RawUrl);
                    //Global.AlertNew(this.Page, "Book already exists");

                    // ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book already exists');", true);
                }
                else
                {
                    if (Request.QueryString["BookID"] != null && fuPdfUpload.HasFile)
                    {
                        int bID = Convert.ToInt32(Request.QueryString["BookID"]);
                        DeleteFilesFromServerFolder(bID);

                        objBookShip.BookID = Convert.ToInt64(Request.QueryString["BookID"].ToString());
                        objBookShip.deleteAllCountryByBook();

                        string[] ddlCountry = Request.Form.GetValues("ddlCountry");
                        string[] txtCharge = Request.Form.GetValues("txtCharge");

                        int countCountry = Convert.ToInt32(txtCharge == null ? 0 : txtCharge.Count());
                        for (int i = 0; i < countCountry; i++)
                        {
                            objBookShip.BookShippingID = -1;
                            objBookShip.BookID = Convert.ToInt64(Request.QueryString["BookID"].ToString());
                            objBookShip.CountryID = Convert.ToInt32(ddlCountry[i]);
                            if (txtCharge[i] == "")
                            {
                                objBookShip.ShippingCharge = "0";
                            }
                            else
                            {
                                objBookShip.ShippingCharge = txtCharge[i];

                            }
                            if (Convert.ToInt32(ddlCountry[i]) > 0)
                                objBookShip.InsertBookShippingDetail();
                        }
                    }
                    PDFtoJPG();
                    PDFtoSWF();
                }
            }
            else
            {
                PDFtoJPG();
                PDFtoSWF();
            }

            if (Request.QueryString["EDIT"] != null)
            {
                if (Convert.ToBoolean(Request.QueryString["EDIT"]))
                {

                    objBook.IsActive = chkIsactive.Checked;
                    objBook.IsFree = chkIsFree.Checked;
                    objBook.IsFeatured = Convert.ToInt32(chkIsFeartued.Checked);
                    objBook.Published = true;
                    objBook.PublishDate = DateTime.ParseExact(txtDate1.Text, "dd/MM/yyyy", null);
                    objBook.IsSpecial = Convert.ToInt32(chkIsSpecial.Checked);
                    objBook.ExplorerPdfStartNo = PdfStartpage;
                    objBook.ExplorerPdfEndNo = pdfEndpage;
                    objBook.UpdatedOn = DateTime.Now;
                    objBook.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
                    objBook.DescriptionImages = 1.ToString();
                    objBook.Autoher = txtAuthorName.Text;
                    objBook.OrderIndex = Convert.ToInt64(ddlOrderIndex.SelectedValue);
                    objBook.DealerEmail = txtDealerEmail.Text;
                    objBook.SpecialOffer = chkSpecial.Checked;
                    objBook.IseBook = chkeBook.Checked;
                    objBook.IsPaperBook = chkPapaerBook.Checked;
                    objBook.IsFreePaper = chkIsPaperFree.Checked;
                    if (chkPapaerBook.Checked)
                    {
                        objBook.Weight = txtWeight.Text;
                        objBook.DimWeight = txtDimWeight.Text;
                        objBook.Width = txtWidth.Text;
                        objBook.Height = txtHeight.Text;
                        objBook.Depth = txtDepth.Text;
                    }
                    if (!string.IsNullOrEmpty(txtQuantity.Text.Trim()))
                        objBook.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
                    else
                        objBook.Quantity = 0;
                    if (txtSpecialOfferStart.Text.Trim().Length > 0)
                    {
                        objBook.SpecialOfferStart = Convert.ToDateTime(txtSpecialOfferStart.Text);
                    }
                    else
                    {
                        objBook.SpecialOfferStart = null;
                    }
                    if (txtSpecialOfferEnd.Text.Trim().Length > 0)
                    {
                        objBook.SpecialOfferEnd = Convert.ToDateTime(txtSpecialOfferEnd.Text);
                    }
                    else
                    {
                        objBook.SpecialOfferEnd = null;
                    }
                    if (chkIsFree.Checked == true)
                    {
                        txtPrice.Text = "";
                        txtDiscount.Text = "";
                        txtFinalPrice.Text = "";
                    }
                    objBook.Price = txtPrice.Text != "" ? txtPrice.Text : "0";
                    objBook.DiscountedPrice = txtDiscount.Text != "" ? Convert.ToInt64(txtDiscount.Text) : 0;
                    objBook.FinalPrice = txtFinalPrice.Text;
                    objBook.Language = txtLanguage.Text;
                    objBook.PaperBookPrice = txtPaperBookPrice.Text != "" ? txtPaperBookPrice.Text : "0";
                    objBook.PaperBookDiscount = txtPaperBookDiscount.Text != "" ? txtDiscount.Text : "0";
                    objBook.PaperBookFinalPrice = txtFinalPaperBookPrice.Text;
                    objBook.DescriptionImages = rblType.SelectedItem.Value;
                    int a = 0;
                    DataTable dT = objBook.SelectAllBook();
                    if (dT != null && dT.Rows.Count > 0)
                    {
                        string[] title = Request.Form.GetValues("title");
                        string[] languageid = Request.Form.GetValues("languageid");
                        for (int i = 0; i < languageid.Length; i++)
                        {
                            objBook.LanguageID = Convert.ToInt32(languageid[i]);
                            objBook.Title = title[i];

                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j]["Title"].ToString() == objBook.Title)// && Convert.ToInt32(dt.Rows[j]["CategoryID"]) == Convert.ToInt32(ddlCategory.SelectedValue) && dt.Rows[j]["Autoher"].ToString() == txtAuthorName.Text && dt.Rows[j]["Language"].ToString() == txtLanguage.Text)
                                {
                                    count++;
                                }
                            }
                        }
                        if (count > 0 && !Convert.ToBoolean(Request.QueryString["EDIT"]))
                        {
                            //Response.Redirect(Request.RawUrl);
                            //Global.AlertNew(this.Page, "Book already exists");  
                            // ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Book already exists');", true);
                        }
                        else
                        {
                            a = objBook.InsertBookIssue();
                            if (a > 0)
                            {
                                movefileedittime(objBook.SelectBookByID(),Convert.ToInt32(ddlCategory.SelectedValue),a);
                            }
                           
                            if (a > 0)
                                ChangeOrderIndex(Convert.ToString(a), ddlOrderIndex.SelectedValue);

                            objBookShip.BookID = a;
                            objBookShip.deleteAllCountryByBook();

                            string[] ddlCountry = Request.Form.GetValues("ddlCountry");
                            string[] txtCharge = Request.Form.GetValues("txtCharge");

                            int countCountry = Convert.ToInt32(txtCharge == null ? 0 : txtCharge.Count());
                            for (int i = 0; i < countCountry; i++)
                            {
                                objBookShip.BookShippingID = -1;
                                objBookShip.BookID = a;
                                objBookShip.CountryID = Convert.ToInt32(ddlCountry[i]);
                                if (txtCharge[i] == "")
                                {
                                    objBookShip.ShippingCharge = "0";
                                }
                                else
                                {
                                    objBookShip.ShippingCharge = txtCharge[i];
                                }
                                if (Convert.ToInt32(ddlCountry[i]) > 0)
                                    objBookShip.InsertBookShippingDetail();
                            }
                        }
                    }
                    else
                    {
                        a = objBook.InsertBookIssue();
                        if (a > 0)
                            ChangeOrderIndex(Convert.ToString(a), ddlOrderIndex.SelectedValue);

                        objBookShip.BookID = a;
                        objBookShip.deleteAllCountryByBook();

                        string[] ddlCountry = Request.Form.GetValues("ddlCountry");
                        string[] txtCharge = Request.Form.GetValues("txtCharge");

                        int countCountry = Convert.ToInt32(txtCharge == null ? 0 : txtCharge.Count());
                        for (int i = 0; i < countCountry; i++)
                        {
                            objBookShip.BookShippingID = -1;
                            objBookShip.BookID = a;
                            objBookShip.CountryID = Convert.ToInt32(ddlCountry[i]);
                            if (txtCharge[i] == "")
                            {
                                objBookShip.ShippingCharge = "0";
                            }
                            else
                            {
                                objBookShip.ShippingCharge = txtCharge[i];
                            }
                            if (Convert.ToInt32(ddlCountry[i]) > 0)
                                objBookShip.InsertBookShippingDetail();
                        }
                    }
                    if (a > 0)
                    {
                        string subPath = "~/Book/" + ddlCategory.SelectedValue;
                        bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));

                        if (!IsExists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                        HttpFileCollection fileCollection = Request.Files;
                        int j = 0;
                        for (int i = 0; i < fileCollection.Count; i++)
                        {
                            //if (fileCollection.Keys[i].ToString().Contains("fpUpload"))
                            
                                if (fileCollection.Keys[i].ToString().Contains("fuPdfUpload"))
                               {
                                HttpPostedFile uploadfile = fileCollection[i];
                                string fileName = Path.GetFileName(uploadfile.FileName);
                                string extension = Path.GetExtension(uploadfile.FileName);
                                if (uploadfile.ContentLength > 0)
                                {
                                    Stream strm = fpUpload.PostedFile.InputStream;
                                    ++j;
                                    //uploadfile.SaveAs(Server.MapPath(subPath) + "/Original" + a.ToString() + "_" + j.ToString() + ".jpg");
                                    //ResizeImage RI = new ResizeImage();
                                    //RI.ReSizeImage(subPath + "/Temp_Original" + a.ToString() + "_" + j.ToString() + ".jpg", subPath + "/Original" + a.ToString() + "_" + j.ToString() + ".jpg");                            
                                    objBook = new BookBAL();
                                    objBook.BookID = a;
                                    RenameFile(fileName, a + "_" + objBook.GetmaxBookIssueImage());
                                    objBook.ImagePath = "Original" + a.ToString() + "_" + j.ToString() + ".jpg";
                                    objBook.IsActive = true;
                                    objBook.IsTitled = false;
                                    objBook.InsertBookIssueImage();
                                }
                            }
                        }
                        DescriptionImage(a);
                    }
                }
                else
                {
                    objBook.CreatedBy = Global.RegistrationID;
                    int a = objBook.InsertBookIssue();
                    if (a > 0)
                    {
                        ChangeOrderIndex(Convert.ToString(a), ddlOrderIndex.SelectedValue);
                        objBookShip.BookID = a;
                        objBookShip.deleteAllCountryByBook();

                        string[] ddlCountry = Request.Form.GetValues("ddlCountry");
                        string[] txtCharge = Request.Form.GetValues("txtCharge");

                        int countCountry = Convert.ToInt32(txtCharge == null ? 0 : txtCharge.Count());
                        for (int i = 0; i < countCountry; i++)
                        {
                            objBookShip.BookShippingID = -1;
                            objBookShip.BookID = a;
                            objBookShip.CountryID = Convert.ToInt32(ddlCountry[i]);
                            if (txtCharge[i] == "")
                            {
                                objBookShip.ShippingCharge = "0";
                            }
                            else
                            {
                                objBookShip.ShippingCharge = txtCharge[i];
                            }
                            if (Convert.ToInt32(ddlCountry[i]) > 0)
                                objBookShip.InsertBookShippingDetail();
                        }

                        string subPath = "~/Book/" + ddlCategory.SelectedValue;

                        bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));
                        if (!IsExists)
                            System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

                        HttpFileCollection fileCollection = Request.Files;
                        int j = 0;
                        for (int i = 0; i < fileCollection.Count; i++)
                        {
                            if (fileCollection.Keys[i].ToString().Contains("fpUpload"))
                            {
                                ++j;
                                HttpPostedFile uploadfile = fileCollection[i];
                                string fileName = Path.GetFileName(uploadfile.FileName);
                                string extension = Path.GetExtension(uploadfile.FileName);
                                if (uploadfile.ContentLength > 0)
                                {
                                    Stream strm = fpUpload.PostedFile.InputStream;

                                    //uploadfile.SaveAs(Server.MapPath(subPath) + "/Original" + a.ToString() + "_" + j.ToString() + ".jpg");
                                    string file = (Server.MapPath(subPath + "/" + a.ToString() + ".pdf"));
                                    this.convertpdftojpg(file, Server.MapPath(subPath) + "/Original" + a.ToString() + "_" + 1.ToString() + ".jpg");
                                    //ResizeImage RI = new ResizeImage();
                                    //RI.ReSizeImage(subPath + "/Temp_Original" + a.ToString() + "_" + j.ToString() + ".jpg", subPath + "/Original" + a.ToString() + "_" + j.ToString() + ".jpg");

                                    objBook = new BookBAL();
                                    objBook.BookID = a;
                                    objBook.ImagePath = "Original" + a.ToString() + "_" + j.ToString() + ".jpg";
                                    objBook.IsActive = true;
                                    objBook.IsTitled = false;
                                    objBook.InsertBookIssueImage();
                                }
                            }
                        }
                        DescriptionImage(a);
                    }
                }
            }
            ResizeImage();
            if (count > 0 && !Convert.ToBoolean(Request.QueryString["EDIT"]))
            {
                //Response.Redirect(Request.RawUrl);
                //Global.AlertNew(this.Page, "Book already exists");

                ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('eBook is already exists');", true);
            }
            else
            {
                Response.Redirect("ManageBook.aspx");
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "for", "alert('Please fill all required fields');", true);
        }
    }

    private void ResizeImage()
    {
        BookBAL Obj_Book = new BookBAL();
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
        Obj_Book.EndIndex = -1;
        Obj_Book.StartIndex = 1;
        Obj_Book.LanguageID = 1;
        dt = Obj_Book.get_all_book_website1();
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string img = "";
                string fileName = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                if (File.Exists((HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "")))
                {
                    img = "Book/" + dr["CategoryID"] + '/' + dr["ImagePath"] + "";
                    string image = HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "";
                    Stream fs = new FileStream(image, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    if (!File.Exists(img.Replace(".jpg", "_1.jpg")))
                    {
                        GenerateThumbnails(image, HttpContext.Current.Request.PhysicalApplicationPath + "/" + fileName + "");
                        return;
                    }
                }
            }
        }
    }

    private void GenerateThumbnails(string sourcePath, string targetPath)
    {
        try
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(sourcePath);
            {
                var newWidth = (int)(450);
                var newHeight = (int)(600);
                var thumbnailImg = new System.Drawing.Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath.Replace("\\/", "\\").Replace("/", "\\").Replace(".jpg", "_1.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        catch (Exception)
        {
            
        }
    }

    private void DeleteFilesFromServerFolder(int bookID)
    {
        try
        {
            DataTable dtBook = new DataTable();
            objBook = new BookBAL();
            objBook.BookID = bookID;

            dtBook = objBook.getBookDetails();
            if (dtBook != null && dtBook.Rows.Count > 0)
            {
                var catId = dtBook.Rows[0]["CategoryID"].ToString();
                string subPath = "~/Book/" + catId;
                bool IsExists = Directory.Exists(Server.MapPath(subPath));

                if (IsExists)
                {
                    string[] resultFiles;

                    string bookpath = Server.MapPath("../Book/" + catId + "/");
                    resultFiles = Directory.GetFiles(bookpath, "*.jpg");

                    for (int iRow = 0; iRow < resultFiles.Length; iRow++)
                    {
                        if (resultFiles[iRow].Contains("Temp_Original" + bookID) || resultFiles[iRow].Contains("Original" + bookID))
                        {
                            string sPath = resultFiles[iRow];
                            File.Delete(sPath);
                            return;
                        }
                    }

                    if (File.Exists(bookpath + bookID + ".pdf"))
                    {
                        File.Delete(bookpath + bookID + ".pdf");
                    }
                    if (File.Exists(bookpath + bookID + ".epub"))
                    {
                        File.Delete(bookpath + bookID + ".epub");
                    }
                    if (Directory.Exists(bookpath + bookID))
                    {
                        Directory.Delete(bookpath + bookID, true);
                    }
                }
            }
        }
        catch (Exception ex)
        { }
    }

    private void SaveTitalDescription(int BookID)
    {
        string[] title = Request.Form.GetValues("title");
        string[] Description = Request.Form.GetValues("Description");
        string[] languageid = Request.Form.GetValues("languageid");
        for (int i = 0; i < languageid.Length; i++)
        {
            objBook.BookID = BookID;
            objBook.LanguageID = Convert.ToInt32(languageid[i]);
            objBook.Title = title[i];
            objBook.Description = Description[i];
            objBook.InsertUpdateBook();
        }
    }

    public void deleteIssue(int ID)
    {
        if (ID != 0)
        {
            mib.ID = ID;
            if (File.Exists(Server.MapPath("../Book/" + ddlCategory.SelectedValue)))
            {
                string path = "";
                string PDFpath;
                string[] resultFiles;

                string imgpath = Server.MapPath("../Book/" + ddlCategory.SelectedValue + "/" + ID);
                resultFiles = Directory.GetFiles(imgpath, "*.swf");

                for (int iRow = 0; iRow < resultFiles.Length; iRow++)
                {
                    string sPath = resultFiles[iRow];
                    File.Delete(sPath);
                }

                string bookpath = Server.MapPath("../Book/" + ddlCategory.SelectedValue + "/");
                resultFiles = Directory.GetFiles(bookpath, "*.jpg");

                for (int iRow = 0; iRow < resultFiles.Length; iRow++)
                {
                    if (resultFiles[iRow].Contains("Temp_Original" + ID) || resultFiles[iRow].Contains("Original" + ID))
                    {
                        string sPath = resultFiles[iRow];
                        File.Delete(sPath);
                    }
                }
            }
        }
    }

    private void DeletetempMagz(string BookID)
    {
        string[] TempImage = Directory.GetFiles(Server.MapPath("../Book/" + BookID));
        foreach (string Imgesss in TempImage)
        {
            string temp = Imgesss.Split('\\')[Imgesss.Split('\\').Count() - 1];

            if (temp.ToLower().StartsWith("temp"))
            {
                try
                {
                    File.Delete(Imgesss);
                }
                catch
                {
                }
            }
        }
    }

    public void PDFtoSWF()
    {
        objBook.CreatedBy = Global.RegistrationID;

        if (BookID > 0)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
                objBook.ID = Convert.ToInt32(Request.QueryString["ID"]);

            string subPath = "~/Book/" + ddlCategory.SelectedValue;
            bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!IsExists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            }
            if (IsExists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath) + "/" + BookID);
            }
            if (fuPdfUpload.HasFile)
            {
                fuPdfUpload.SaveAs(Server.MapPath(subPath + "/" + BookID.ToString() + ".pdf"));
                string file = (Server.MapPath(subPath + "/" + BookID.ToString() + ".pdf"));
                FileConversion(BookID, subPath, file);
            }
        }
    }

    public void DescriptionImage(int BookID)
    {
        if (Request.QueryString["ID"] == null)
        {
            if (txtDescriptionPages.Text.ToString().Trim() != "")
            {
                string[] desStr = 1.ToString().Trim().Split(',');
                if (desStr.Length != 0)
                {
                    for (int p = 0; p <= desStr.Length - 1; p++)
                    {
                        string DescImg = "Original" + BookID.ToString() + "_" + desStr[p].ToString() + ".jpg";
                        objBook = new BookBAL();

                        objBook.BookID = BookID;
                        objBook.ImagePath = DescImg.Trim().Replace(" ", "");
                        objBook.IsActive = true;
                        objBook.IsTitled = false;
                        objBook.InsertBookIssueDescImage();
                    }
                }
            }
        }
    }

    private string RenameFile(String oldFilename, String newFilename)
    {
        FileInfo file = new FileInfo(oldFilename);

        if (file.Exists)
        {
            File.Move(oldFilename, newFilename);
        }
        return newFilename;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {

    }

    public int ID
    {
        get
        {
            int id = 0;
            if (Request.QueryString["ID"] != null || Request.QueryString["ID"] != "")
            {
                id = Convert.ToInt32(Request.QueryString["ID"]);
                return id;
            }
            return id;
        }
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void dlImages_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandArgument != null)
        {
            switch (e.CommandName)
            {
                case "delete1":
                    objBook.ID = Convert.ToInt32(e.CommandArgument);
                    objBook.DeleteBookImage();
                    objBook.ID = 0;
                    imgebind();
                    break;
            }
        }
    }

    #region PDF2SWF
    private void createUniqueUserRepository()
    {
        //Create Unique Number.
        userName += String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

        //Store the value of this Unique User Repository in the Session variable to be used through out the Current Session.
        Session["CurrentUsersRepositiory"] = userName;

        //Now create Repository for this user.
        Directory.CreateDirectory(Server.MapPath(destinationDirectory + userName));
        destinationDirectory += userName;

        //Create repository for PDF in side current User's Unique folder.
        uploadedPDFRepository = destinationDirectory + "/" + "UploadedPDF";
        Directory.CreateDirectory(Server.MapPath(uploadedPDFRepository));

        //Create repository for PDF in side current User's Unique folder.
        convertedSWFRepository = destinationDirectory + "/" + "ConvertedSWFRepositiory";
        Directory.CreateDirectory(Server.MapPath(convertedSWFRepository));
    }

    /// <summary>
    /// PDF to SWF converter process.
    /// </summary>
    public void FileConversion(Int64 BookID, string subPath, string fileToConvert)
    {
        try
        {
            System.Diagnostics.Process objSystemProcess = new System.Diagnostics.Process();

            objSystemProcess.StartInfo.UseShellExecute = false;
            objSystemProcess.StartInfo.CreateNoWindow = true;
            objSystemProcess.StartInfo.RedirectStandardError = true;
            objSystemProcess.StartInfo.RedirectStandardOutput = true;

            objSystemProcess.StartInfo.WorkingDirectory = HttpContext.Current.Server.MapPath("~");
            objSystemProcess.StartInfo.FileName = HttpContext.Current.Server.MapPath("~/pdf2swf/pdf2swf.exe");
            //objSystemProcess.StartInfo.Arguments = fileToConvert + " " + Server.MapPath(convertedSWFRepository) + "/" + "Page" + "%.swf";
            objSystemProcess.StartInfo.Arguments = "-F " + " " + "C:\\WINDOWS\\Fonts\\" + " " + "-s linksopennewwindow=0" + " " + "-T9 -s insertstop" + " -f " + " " + fileToConvert + " " + " -o " + " " + Server.MapPath(subPath + "/" + BookID.ToString()) + "/" + "%.swf" + " " + "-j 200";
            // objSystemProcess.StartInfo.Arguments = "-F " + " " + "C:\\WINDOWS\\Fonts\\" + " " + " -f " + " " + fileToConvert + " " + " -o " + " " + Server.MapPath(subPath + "/" + BookID.ToString()) + "/" + "%.swf";
            // p.StartInfo.Arguments = "-F " + "\"" + "C:\WINDOWS\Fonts" + "\"" + " " + fileToConvert + " " + Server.MapPath(convertedSWFRepository) + "/" + "Page" + "%.swf";

            //Start the conversion process.
            objSystemProcess.Start();
            objSystemProcess.WaitForExit(120000);
            objSystemProcess.Close();
            objSystemProcess.Dispose();
            //  insertFileRecord();

            #region New Code JPG2SWF

            /*string[] resultFiles;

            string bookpath = Server.MapPath("../Book/" + ddlCategory.SelectedValue + "/" + BookID + "/");
            resultFiles = Directory.GetFiles(bookpath, "*.swf");
            if (resultFiles.Length <= 0)
            {
                int pageno = new Pdf2Image().GetPdfPageCount(fileToConvert);

                for (int i = 1; i <= pageno; i++)
                {
                    string path = Server.MapPath("~/Book/" + ddlCategory.SelectedValue + "/");
                    string fileName = path + "Original" + BookID + "_" + i + ".jpg";
                    if (File.Exists(fileName))
                    {
                        System.Drawing.Image img = System.Drawing.Image.FromFile(fileName);
                        int posX = 0;
                        int posY = 0;
                        int imgWidth = img.Width;
                        int imgHeight = img.Height;
                        Swf swf = new Swf();
                        swf.Size = new Rect(0, 0, (posX + imgWidth) * 20, (posY + imgHeight) * 20);
                        swf.Version = 7;
                        swf.Header.Signature = "CWS";

                        swf.Tags.Add(new SetBackgroundColorTag(255, 255, 255));
                        ushort jpegId = swf.GetNewDefineId();
                        swf.Tags.Add(DefineBitsJpeg2Tag.FromImage(jpegId, img));

                        DefineShapeTag shapeTag = new DefineShapeTag();
                        shapeTag.CharacterId = swf.GetNewDefineId();
                        shapeTag.Rect = new Rect(posX * 20 - 1, posY * 20 - 1, (posX + imgWidth) * 20 - 1, (posY + imgHeight) * 20 - 1);
                        FillStyleCollection fillStyles = new FillStyleCollection();
                        fillStyles.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, ushort.MaxValue, new SwfDotNet.IO.Tags.Types.Matrix(0, 0, 20, 20)));
                        fillStyles.Add(new BitmapFill(FillStyleType.ClippedBitmapFill, jpegId, new SwfDotNet.IO.Tags.Types.Matrix(posX * 20 - 1, posY * 20 - 1,
                                       (20.0 * imgWidth) / img.Width, (20.0 * imgHeight) / img.Height)));
                        LineStyleCollection lineStyles = new LineStyleCollection();
                        ShapeRecordCollection shapes = new ShapeRecordCollection();
                        shapes.Add(new StyleChangeRecord(posX * 20 - 1, posY * 20 - 1, 2));
                        shapes.Add(new StraightEdgeRecord(imgWidth * 20, 0));
                        shapes.Add(new StraightEdgeRecord(0, imgHeight * 20));
                        shapes.Add(new StraightEdgeRecord(-imgWidth * 20, 0));
                        shapes.Add(new StraightEdgeRecord(0, -imgHeight * 20));
                        shapes.Add(new EndShapeRecord());
                        shapeTag.ShapeWithStyle = new ShapeWithStyle(fillStyles, lineStyles, shapes);
                        swf.Tags.Add(shapeTag);

                        swf.Tags.Add(new PlaceObject2Tag(shapeTag.CharacterId, 1, 0, 0));
                        swf.Tags.Add(new ShowFrameTag());
                        swf.Tags.Add(new EndTag());

                        SwfWriter writer = new SwfWriter(path + "/" + BookID + "/" + i + ".swf");
                        writer.Write(swf);
                        writer.Close();

                        img.Dispose();
                    }
                }
            }*/
            #endregion
        }
        catch (Exception ex)
        { }

    }


    public void ExtractPages(string sourcePdfPath, string outputPdfPath, int startPage, int endPage)
    {

        PdfReader reader = null;
        iTextSharp.text.Document sourceDocument = null;
        PdfCopy pdfCopyProvider = null;
        PdfImportedPage importedPage = null;

        try
        {
            // Intialize a new PdfReader instance with the contents of the source Pdf file:
            reader = new PdfReader(sourcePdfPath);

            // For simplicity, I am assuming all the pages share the same size
            // and rotation as the first page:
            sourceDocument = new iTextSharp.text.Document(reader.GetPageSizeWithRotation(startPage));

            // Initialize an instance of the PdfCopyClass with the source 
            // document and an output file stream:
            pdfCopyProvider = new PdfCopy(sourceDocument,
                new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

            sourceDocument.Open();

            // Walk the specified range and add the page copies to the output file:
            for (int i = startPage; i <= endPage; i++)
            {
                importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                pdfCopyProvider.AddPage(importedPage);
            }
            sourceDocument.Close();
            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region PDF2JPG
    public void PDFtoJPG()
    {
        if (Request.QueryString["BookID"] != null && Request.QueryString["BookID"].ToString() != "")
            objBook.BookID = Convert.ToInt32(Request.QueryString["BookID"]);
        objBook.IsActive = chkIsactive.Checked;
        objBook.IsFree = chkIsFree.Checked;
        objBook.IsFeatured = Convert.ToInt32(chkIsFeartued.Checked);
        objBook.Published = true;
        objBook.PublishDate = DateTime.ParseExact(txtDate1.Text, "dd/MM/yyyy", null);
        objBook.IsSpecial = Convert.ToInt32(chkIsSpecial.Checked);
        objBook.ExplorerPdfStartNo = PdfStartpage;
        objBook.ExplorerPdfEndNo = pdfEndpage;
        objBook.UpdatedOn = DateTime.Now;
        objBook.CategoryID = Convert.ToInt32(ddlCategory.SelectedValue);
        objBook.DescriptionImages = 1.ToString();
        objBook.Autoher = txtAuthorName.Text;
        objBook.OrderIndex = Convert.ToInt64(ddlOrderIndex.SelectedValue);
        objBook.DealerEmail = txtDealerEmail.Text;
        objBook.SpecialOffer = chkSpecial.Checked;
        objBook.IseBook = chkeBook.Checked;
        objBook.IsPaperBook = chkPapaerBook.Checked;
        objBook.IsFreePaper = chkIsPaperFree.Checked;
        if (chkPapaerBook.Checked)
        {
            objBook.Weight = txtWeight.Text;
            objBook.DimWeight = txtDimWeight.Text;
            objBook.Width = txtWidth.Text;
            objBook.Height = txtHeight.Text;
            objBook.Depth = txtDepth.Text;
        }
        if (!string.IsNullOrEmpty(txtQuantity.Text.Trim()))
            objBook.Quantity = Convert.ToInt32(txtQuantity.Text.Trim());
        else
            objBook.Quantity = 0;
        objBook.PartnerID = 0;
        if (txtSpecialOfferStart.Text.Trim().Length > 0)
        {
            objBook.SpecialOfferStart = Convert.ToDateTime(txtSpecialOfferStart.Text);
        }
        else
        {
            objBook.SpecialOfferStart = null;
        }
        if (txtSpecialOfferEnd.Text.Trim().Length > 0)
        {
            objBook.SpecialOfferEnd = Convert.ToDateTime(txtSpecialOfferEnd.Text);
        }
        else
        {
            objBook.SpecialOfferEnd = null;
        }
        if (chkIsFree.Checked == true)
        {
            txtPrice.Text = "";
            txtDiscount.Text = "";
            txtFinalPrice.Text = "";
        }
        objBook.Price = txtPrice.Text != "" ? txtPrice.Text : "0";
        objBook.DiscountedPrice = txtDiscount.Text != "" ? Convert.ToInt64(txtDiscount.Text) : 0;
        objBook.FinalPrice = txtFinalPrice.Text;
        objBook.PaperBookPrice = txtPaperBookPrice.Text != "" ? txtPaperBookPrice.Text : "0";
        objBook.PaperBookDiscount = txtPaperBookDiscount.Text != "" ? txtDiscount.Text : "0";
        objBook.PaperBookFinalPrice = txtFinalPaperBookPrice.Text;
        objBook.DescriptionImages = rblType.SelectedItem.Value;
        objBook.Language = txtLanguage.Text;
        objBook.CreatedBy = Global.RegistrationID;
        objBook.DescriptionImages = rblType.SelectedItem.Value;
        int a = objBook.InsertBookIssue();
        if (a > 0)
            ChangeOrderIndex(Convert.ToString(a), ddlOrderIndex.SelectedValue);

        objBookShip.BookID = a;
        objBookShip.deleteAllCountryByBook();

        string[] ddlCountry = Request.Form.GetValues("ddlCountry");
        string[] txtCharge = Request.Form.GetValues("txtCharge");

        int countCountry = Convert.ToInt32(txtCharge==null? 0: txtCharge.Count());
        for (int i = 0; i < countCountry; i++)
        {
            objBookShip.BookShippingID = -1;
            objBookShip.BookID = a;
            objBookShip.CountryID = Convert.ToInt32(ddlCountry[i]);
            if (txtCharge[i] == "")
            {
                objBookShip.ShippingCharge = "0";
            }
            else
            {
                objBookShip.ShippingCharge = txtCharge[i];
            }
            if (Convert.ToInt32(ddlCountry[i]) > 0)
                objBookShip.InsertBookShippingDetail();
        }

        SaveTitalDescription(a);
        BookID = a;
        if (a > 0)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
                objBook.ID = Convert.ToInt32(Request.QueryString["ID"]);

            string subPath = "~/Book/" + ddlCategory.SelectedValue;
            bool IsExists = System.IO.Directory.Exists(Server.MapPath(subPath));
            if (!IsExists)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
            }

            if (fuPdfUpload.HasFile)
            {
                deleteIssue(Convert.ToInt32(objBook.ID));
                fuPdfUpload.SaveAs(Server.MapPath(subPath + "/" + a.ToString() + ".pdf"));

                string file = (Server.MapPath(subPath + "/" + a.ToString() + ".pdf"));
                int pageno = new Pdf2Image().GetPdfPageCount(file);

                Boolean catchss = false;

                try
                {
                    //for (int i = 1; i <= pageno; i++)
                    //{
                        //if (i == 1)
                        //{
                            //System.Drawing.Image firstPage = new Pdf2Image(file).GetImageWithNo(i);
                            //firstPage.Save(Server.MapPath(subPath) + "/Original" + a.ToString() + "_" + i.ToString() + ".jpg");
                            this.convertpdftojpg(file, Server.MapPath(subPath) + "/Original" + a.ToString() + "_" + 1.ToString() + ".jpg");

                            objBook = new BookBAL();
                            objBook.BookID = a;
                            objBook.ImagePath = "Original" + a.ToString() + "_" + 1.ToString() + ".jpg";
                            objBook.IsActive = true;
                            objBook.IsTitled = false;
                            objBook.InsertBookIssueImage();

                       // }
                        //else
                       // {
                        //    break;
                       // }
                   // }
                }
                catch (Exception Ex)
                {
                    catchss = false;
                   
                }

                if (catchss)
                {
                    string PDFRepairPath = Server.MapPath("../Book/RepairPDF");
                    string filePath = Server.MapPath("../Book/" + ddlCategory.SelectedValue);// + Convert.ToInt32(ddlBook.SelectedValue);
                    string FileName = a + ".pdf";
                    string CorruptedFileName = Path.GetFileNameWithoutExtension(FileName) + "Corrupted.pdf";
                    string BatFileName = Path.GetFileNameWithoutExtension(FileName) + ".bat";
                    subPath = PDFRepairPath + "\\" + CorruptedFileName;
                    bool IsExists1 = System.IO.File.Exists(subPath);
                    if (!IsExists1)
                    {
                        Thread.Sleep(1500);
                        System.IO.File.Move(filePath + "\\" + FileName, PDFRepairPath + "\\" + CorruptedFileName);
                        Thread.Sleep(1500);
                        GenrateBatFile(PDFRepairPath, CorruptedFileName, FileName); //Generate Bat File
                        RepairPDF(PDFRepairPath, CorruptedFileName, BatFileName);//Repair PDF
                        Thread.Sleep(3000);
                        System.IO.File.Move(PDFRepairPath + "\\" + FileName.Replace(".pdf", "Corrupted.pdf"), filePath + "\\" + FileName);//move repaired file to main folder
                        Thread.Sleep(3000);
                        Pdf2JpgRepair(filePath, FileName);// convert PDF2JPG
                        File.Delete(PDFRepairPath + "\\" + CorruptedFileName);//Delete curruptedFile
                        File.Delete(PDFRepairPath + "\\" + BatFileName);//Delete Bat file
                        ExtractPages(filePath + "/" + a.ToString() + ".pdf", filePath + "/" + a.ToString() + "TOP5.pdf", PdfStartpage, pdfEndpage);// pageno < 5 ? pageno : 5);
                    }
                }
            }
            if(fpUploadePub.HasFile && fpUploadimage.HasFile)
            {
                deleteIssue(Convert.ToInt32(objBook.ID));
                fpUploadePub.SaveAs(Server.MapPath(subPath + "/" + a.ToString() + ".epub"));

                fpUploadimage.SaveAs(Server.MapPath(subPath) + "/Original" + a.ToString() + "_1.jpg");
                objBook = new BookBAL();
                objBook.BookID = a;
                objBook.ImagePath = "Original" + a.ToString() + "_1.jpg";
                objBook.IsActive = true;
                objBook.IsTitled = false;
                objBook.InsertBookIssueImage();
               
            }
            if (fpImagePaper.HasFile)
            {
                deleteIssue(Convert.ToInt32(objBook.ID));

                fpImagePaper.SaveAs(Server.MapPath(subPath) + "/Original" + a.ToString() + "_1.jpg");
                objBook = new BookBAL();
                objBook.BookID = a;
                objBook.ImagePath = "Original" + a.ToString() + "_1.jpg";
                objBook.IsActive = true;
                objBook.IsTitled = false;
                objBook.InsertBookIssueImage();
                
            }
            DescriptionImage(a);
        }
    }

    public void ChangeOrderIndex(string bookID, string orderIndex)
    {
        if (Convert.ToInt32(orderIndex) == 1)
        {
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "update book set OrderIndex = OrderIndex + 1 where BookID != " + bookID);
        }
        else if (Convert.ToInt32(orderIndex) > 1)
        {
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "update Book Set OrderIndex = (SELECT TOP 1 OrderIndex from Book where BookID = " + bookID + " ) where BookID = (SELECT TOP 1 BookID from Book where OrderIndex = " + orderIndex + " )");
            DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, "update book set OrderIndex = " + orderIndex + " where BookID = " + bookID + "");
        }
    }

    public void Pdf2JpgRepair(string FilePath, string filename)
    {
        deleteIssue(Convert.ToInt32(objBook.ID));

        string file = (FilePath + "\\" + filename);
        string subPath = "~/Book/" + ddlCategory.SelectedValue;// +Convert.ToInt32(ddlBook.SelectedValue);
        string filenameExt = Path.GetFileNameWithoutExtension(filename);
        int pageno = new Pdf2Image().GetPdfPageCount(file);
        pdfEndpage = new Pdf2Image().GetPdfPageCount(file);
        ExtractPages(FilePath + "\\" + filename, FilePath + "\\" + filenameExt + "TOP5.pdf", PdfStartpage, pdfEndpage);// pageno < 5 ? pageno : 5);
        for (int i = 1; i <= pageno; i++)
        {
            try
            {
                Bitmap firstPage = new Pdf2Image(FilePath + "\\" + filenameExt + "TOP5.pdf").GetImageWithNo(i);

                firstPage.Save(FilePath + "\\Temp_Original" + filenameExt + "_" + i.ToString() + ".jpg");
                ResizeImage RI = new ResizeImage();
                RI.ReSizeImage(subPath + "/Temp_Original" + filenameExt + "_" + i.ToString() + ".jpg", subPath + "/Original" + filenameExt + "_" + i.ToString() + ".jpg");

                objBook = new BookBAL();
                objBook.BookID = Convert.ToInt64(filenameExt);
                objBook.ImagePath = "Original" + filenameExt + "_" + i.ToString() + ".jpg";
                objBook.IsActive = true;
                objBook.IsTitled = false;
                objBook.InsertBookIssueImage();
            }
            catch
            {
            }
        }
    }

    public void RepairPDF(string PATH, string FileName, string BatchfileName)
    {
        string str_Path = PATH + "\\" + BatchfileName;
        ProcessStartInfo processInfo = new ProcessStartInfo(str_Path);
        processInfo.CreateNoWindow = true;
        processInfo.WorkingDirectory = PATH;
        processInfo.UseShellExecute = false;
        Process batchProcess = new Process();
        batchProcess.StartInfo = processInfo;
        batchProcess.Start();
        if (batchProcess.HasExited)
        {
            batchProcess.Kill();
        }
    }

    public void GenrateBatFile(string PATH, string CorruptedFileName, string FileName)
    {
        StreamWriter objFile1 = default(StreamWriter);
        objFile1 = File.CreateText(PATH + "\\" + Path.GetFileNameWithoutExtension(FileName) + ".bat");
        objFile1.Write("pdftk.exe \"" + CorruptedFileName + "\" output \"" + FileName + "\"");
        objFile1.Close();
    }
    #endregion

    #region new code for convert pdf to jpg 

    public void convertpdftojpg(string pdfFilepath,string jpgfilepath)
    {
        PdfToImageConverter converter = new PdfToImageConverter();
        converter.Load(File.ReadAllBytes(pdfFilepath));


        if(File.Exists(jpgfilepath))
        {
            File.Delete(jpgfilepath);
        }


        converter.GrayscaleImage = false;
        for (int i = 0; i <= converter.PageCount; i++)
        {
            if (i == 0)
            {
                System.Drawing.Image pageImage = converter.PageToImage(i);
                pageImage.Save(jpgfilepath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

    }
    #region new code for move file in 

    private void movefileedittime(DataTable dt ,int newCategoryid,int newbookid)
    {
        if(dt.Rows.Count>0)
        {
            if (Convert.ToInt32(dt.Rows[0]["CategoryID"]) != newCategoryid)
            {

                string sourcePath = "~/Book/" + (dt.Rows[0]["CategoryID"] != null ? dt.Rows[0]["CategoryID"].ToString() : "") + "/" + (dt.Rows[0]["BookID"] != null ? dt.Rows[0]["BookID"].ToString() : "");
                string targetPath = "~/Book/" + newCategoryid + "/" + newbookid;

                string subPath = "~/Book/" + newCategoryid;

                bool IsExistsdirect = File.Exists(Server.MapPath(subPath));
                if (!IsExistsdirect)
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                }

                bool iffileexist = System.IO.Directory.Exists(Server.MapPath(sourcePath+ ".pdf"));
                if (iffileexist)
                {
                    File.Copy(sourcePath + ".pdf", targetPath + ".pdf");
                }








            }







        }
    }
    #endregion
#endregion
}