using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class TrackOrder : System.Web.UI.Page
{
    RegistrationBAL objRegistration = new RegistrationBAL();
    BookPurchaseBAL objBookPurchase = new BookPurchaseBAL();
    Security S = new Security();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(Request.Url.ToString().Contains("/us/"))
        {
            Response.Redirect(Request.Url.ToString().Replace("/us", ""));
        }
        if(!IsPostBack)
        {
            mvTrack.SetActiveView(emailView);
        }
        if(Session["UserID"] != null)
        {
            objRegistration.RegistrationID = Convert.ToInt64(Session["UserID"].ToString());
            dt = objRegistration.SelectRegistraionByID();
            if(dt != null && dt.Rows.Count > 0)
            {
                txt_email.Text = dt.Rows[0]["EmailAddress"].ToString();
                email.Visible = false;
            }
        }
    }

    protected void btn_track_Click(object sender, EventArgs e)
    {
        if(txt_email.Text.Trim().Length > 0 && txt_orderid.Text.Trim().Length > 0)
        {
            objRegistration.EmailAddress = txt_email.Text;
            dt = objRegistration.GetOneByEmail();
            if(dt != null && dt.Rows.Count > 0)
            {
                var userid = dt.Rows[0]["RegistrationID"].ToString();
                var orderid = txt_orderid.Text;
                //var userFromOrder = txt_orderid.Text.Substring(0, userid.Length);
                //if(userid == userFromOrder)
                //{
                    mvTrack.SetActiveView(bookView);
                    objBookPurchase.UserID = Convert.ToInt64(userid);
                    objBookPurchase.OrderID = Convert.ToInt64(txt_orderid.Text);
                    dt = objBookPurchase.getBookPurchaseByOrderNo();
                    if(dt != null && dt.Rows.Count > 0)
                    {
                        rptRecords1.DataSource = dt; 
                        rptRecords1.DataBind();
                        
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Repeater inner = (Repeater)rptRecords1.Items[i].FindControl("innerRpt");
                            Panel innerRptDiv = (Panel)rptRecords1.Items[i].FindControl("innerRptDiv");
                            HiddenField hfPurchaseID = (HiddenField)rptRecords1.Items[i].FindControl("hfPurchaseID");
                            objBookPurchase.PurchaseID = Convert.ToInt64(hfPurchaseID.Value);
                            DataSet ds = objBookPurchase.GetOrderDetailbyID();
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                innerRptDiv.Visible = false;
                                inner.Visible = true;
                                inner.DataSource = ds.Tables[1];
                                inner.DataBind();
                            }
                            else
                            {
                                innerRptDiv.Visible = true;
                                inner.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        mvTrack.SetActiveView(emailValidation);
                        //lblError.Text = "Order id is not valid.";
                        lblError.Text = Localization.ResourceManager.GetString("Order id is not valid.", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    }
                //}
                //else
                //{
                //    mvTrack.SetActiveView(emailValidation);
                //    lblError.Text = "Order id is not belongs to you.";
                //}
            }
            else
            {
                mvTrack.SetActiveView(emailValidation);
                //lblError.Text = "Email Address is not valid.";
                lblError.Text = Localization.ResourceManager.GetString("Email Address is not valid.", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
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

    public string setHref(string bID)
    {
        return "Book-Detail.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "&id=" + S.Encrypt(bID) + "";
    }
}