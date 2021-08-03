using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Threading;
using System.Globalization;
//using System.Web.UI.DataVisualization.Charting;

public partial class Partner_dashboard : System.Web.UI.Page
{
    public static string TotAmount = "";
    DataTable DT = new DataTable();
    //public static string TenantTitle = "";
    RegistrationBAL objuser = new RegistrationBAL();
    BookPurchaseBAL objpurchase = new BookPurchaseBAL();
    MenuBAL objmenu = new MenuBAL();
    Boolean view = false, edit = false, delete = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        //TenantTitle = "Users";
       // ScriptManager.RegisterStartupScript(this, GetType(), "", "GraphDetail();", true);
        if (!IsPostBack)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            //BindChart1();
            //BindChart2();
            GraphNoOfBooksPerMonth();
        }
    }

    public void GraphNoOfBooksPerMonth()
    {
        try
        {
            TenantTitle1 = "Selling Amount";
            string TenantString = "";
            int year = DateTime.Now.Year;
            DateTime lastDay = new DateTime(year, 12, 31);
            TenantCategories1 = "";
            for (int i = lastDay.Month - 1; i >= 0; i--)
            {

                int month = lastDay.AddMonths(-i).Month;
                int PartnerID = Convert.ToInt32(Session["PartnerRegistrationID"].ToString());
                TenantCategories1 += "'" + lastDay.AddMonths(-i).ToString("MMMM") + "',";
                //DataSet DS = TB.GetTenantCount(month);
                DataSet dt = objpurchase.GetPartnerChart(month,PartnerID);
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {

                    TenantString += dt.Tables[0].Rows[0]["Amount"].ToString() + ",";

                }
                else
                {
                    TenantString += "0" + ",";
                }
                if (dt != null && dt.Tables[1].Rows.Count > 0)
                {
                    //TotAmount = "$ " + dt.Tables[1].Rows[0]["TotalAmount"].ToString();
                    TotAmount = "$ " + Decimal.Round(Convert.ToDecimal(dt.Tables[1].Rows[0]["TotalAmount"].ToString()), 2);
                }
            }
            TenantString = TenantString.Remove(TenantString.Length - 1, 1);
            TenantSeries1 = "{name: 'Amount',data: [" + TenantString + "] }";
            TenantCategories1 = TenantCategories1.Remove(TenantCategories1.Length - 1, 1);
        }
        catch
        {
        }
    }

    public string TenantCategories { get; set; }

    public string TenantSeries { get; set; }

    public string TenantTitle { get; set; }

    public string TenantTitle1 { get; set; }

    public string TenantSeries1 { get; set; }

    public string TenantCategories1 { get; set; }
}