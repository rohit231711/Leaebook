using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Includes_header : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        aIndex1.HRef = Config.WebSiteMain + "Index.aspx?l=" + System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        if (System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en-US")
        {
            aIndex1.HRef = "~/us/";
        }
        else
        {
            aIndex1.HRef = "~/";
        }
    }
}