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

public partial class Admin_dashboard : System.Web.UI.Page
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
            GraphUsersPerMonth();
            GraphNoOfBooksPerMonth();
        }
    }
    private void accessrights()
    {
        objmenu.UserID = Convert.ToInt32(Session["AdminRegistrationID"]);
        DT = objmenu.GetRightsByUser();
        if (DT.Rows.Count > 0)
        {
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                if (Convert.ToInt32(DT.Rows[i]["MenuID"].ToString()) == 1)
                {
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 1)
                    {

                        edit = true;
                    }
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 2)
                    {

                        delete = true;
                    }
                    if (Convert.ToInt32(DT.Rows[i]["AccTypeID"].ToString()) == 3)
                    {
                        view = true;
                    }
                }
            }
        }
    }

    //public void BindChart1()
    //{
    //    DataTable ds = new DataTable();
    //    DataTable dt = new DataTable();
    //    //CashFlowBAL CashFlowBAL = new CashFlowBAL();
    //    dt = objuser.GetChart1();
    //    //dt = ds.Tables[0]; 
    //    string[] x = new string[dt.Rows.Count];
    //    int[] y = new int[dt.Rows.Count];
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        x[i] = dt.Rows[i][1].ToString();
    //        y[i] = Convert.ToInt32(dt.Rows[i][0]);
    //    }
    //    Chart1.Series[0].Points.DataBindXY(x, y);
    //    Chart1.Series[0].ChartType = SeriesChartType.Column;
    //    Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
        
    //    //Chart1.Legends[0].Enabled = true;
    //}

    //public void BindChart2()
    //{
    //    DataTable ds = new DataTable();
    //    DataTable dt = new DataTable();
    //    //CashFlowBAL CashFlowBAL = new CashFlowBAL();
    //    dt = objpurchase.GetChart2();
    //    //dt = ds.Tables[0]; 
    //    string[] x = new string[dt.Rows.Count];
    //    int[] y = new int[dt.Rows.Count];
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        x[i] = dt.Rows[i][1].ToString();
    //        y[i] = Convert.ToInt32(dt.Rows[i][0]);
    //    }
    //    Chart2.Series[0].Points.DataBindXY(x, y);
    //    Chart2.Series[0].ChartType = SeriesChartType.Column;
    //    Chart2.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;
    //    //Chart1.Legends[0].Enabled = true;
    //}

    public void GraphUsersPerMonth()
    {
        try
        {
            TenantTitle = "Registered Users";
            string TenantString = "4054, 4054, 4054, 4054, 4054";
            string GeneralString = "133, 156, 947, 408, 6";
            TenantString = "";
            GeneralString = "";
            int year = DateTime.Now.Year;
            //DateTime firstDay = new DateTime(year, 1, 1);
            DateTime lastDay = new DateTime(year, 12, 31);
            TenantCategories = "";
            for (int i = lastDay.Month - 1; i >= 0; i--)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                int month = lastDay.AddMonths(-i).Month;
                TenantCategories += "'" + lastDay.AddMonths(-i).ToString("MMMM") + "',";
                //DataSet DS = TB.GetTenantCount(month);
                DataTable dt = objuser.GetChart1(month);
                if (dt != null && dt.Rows.Count > 0)
                {

                    TenantString += dt.Rows[0]["NoOfUsers"].ToString() + ",";

                }
                else
                {
                    TenantString += "0" + ",";
                }
                
                
            }
            TenantString = TenantString.Remove(TenantString.Length - 1, 1);
            TenantSeries = "{name: 'Registered Users',data: [" + TenantString + "]}";
            TenantCategories = TenantCategories.Remove(TenantCategories.Length - 1, 1);
        }
        catch
        {
            //TenantTitle = DateTime.Now.Year + " / NEW AGREEMENTS ";
            //TenantSeries = "{name: 'Tenant Request',data: [123,123,123,123,123]}, {name: 'General Request',data: [123,123,123,123,123]}";
            //TenantCategories = "'Africa', 'America', 'Asia', 'Europe', 'Oceania'";
        }
    }

    public void GraphNoOfBooksPerMonth()
    {
        try
        {
            TenantTitle1 = "Selling Amount";
            string TenantString = "4054, 4054, 4054, 4054, 4054";
            decimal total = 0;
            TenantString = "";
            
            int year = DateTime.Now.Year;
            DateTime lastDay = new DateTime(year, 12, 31);
            TenantCategories1 = "";
            for (int i = lastDay.Month - 1; i >= 0; i--)
            {

                int month = lastDay.AddMonths(-i).Month;
                TenantCategories1 += "'" + lastDay.AddMonths(-i).ToString("MMMM") + "',";
                //DataSet DS = TB.GetTenantCount(month);
                DataSet dt = objpurchase.GetChart2(month);
                if (dt != null && dt.Tables[0].Rows.Count > 0)
                {

                    TenantString += dt.Tables[0].Rows[0]["Amount"].ToString() + ",";
                    total += Convert.ToDecimal(dt.Tables[0].Rows[0]["Amount"].ToString());
                }
                else
                {
                    TenantString += "0" + ",";
                }
                //if (dt != null && dt.Tables[1].Rows.Count > 0)
                //{
                //    //TotAmount = "$ " + dt.Tables[1].Rows[0]["TotalAmount"].ToString();
                //    TotAmount = "$ " + Decimal.Round(Convert.ToDecimal(dt.Tables[1].Rows[0]["TotalAmount"].ToString()), 2);
                //}
            }
            TenantString = TenantString.Remove(TenantString.Length - 1, 1);
            TenantSeries1 = "{name: 'Amount',data: [" + TenantString + "] }";
            TenantCategories1 = TenantCategories1.Remove(TenantCategories1.Length - 1, 1);
            TotAmount = "$ " + Decimal.Round(total, 2);
        }
        catch
        {
            //TenantTitle = DateTime.Now.Year + " / NEW AGREEMENTS ";
            //TenantSeries = "{name: 'Tenant Request',data: [123,123,123,123,123]}, {name: 'General Request',data: [123,123,123,123,123]}";
            //TenantCategories = "'Africa', 'America', 'Asia', 'Europe', 'Oceania'";
        }
    }

    public string TenantCategories { get; set; }

    public string TenantSeries { get; set; }

    public string TenantTitle { get; set; }

    public string TenantTitle1 { get; set; }

    public string TenantSeries1 { get; set; }

    public string TenantCategories1 { get; set; }
}