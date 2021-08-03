using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class us_TrackOrder : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
            Response.Redirect("../TrackOrder.aspx?l=en-US");
        else if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "es-ES")
            Response.Redirect("../TrackOrder.aspx?l=es-ES");
    }
}