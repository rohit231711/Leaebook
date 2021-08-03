using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
public partial class Admin_vieworder : System.Web.UI.Page
{
    BookBAL objBook = new BookBAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            if (Request.QueryString["id"] != null)
            {
                setdata();
            }
        }
    }
    private void setdata()
    {
        objBook.OrderID = Convert.ToInt32(Request.QueryString["id"].ToString());
        DataTable dt = objBook.SelectAllBookIssueByOrderID();
        if (dt.Rows.Count > 0)
        {
            dlBookIssue.DataSource = dt;
            dlBookIssue.DataBind();
        }
    }
}