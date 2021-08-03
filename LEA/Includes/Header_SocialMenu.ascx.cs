using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;

public partial class Includes_Header_SocialMenu : System.Web.UI.UserControl
{
    WebsiteSettingsBAL WSB = new WebsiteSettingsBAL();
    DataTable DT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DT = WSB.GetAllWebseetings();
            if (DT != null && DT.Rows.Count > 0)
            {
                fb.HRef = DT.Rows[0]["FaceBookLink"].ToString();
                ti.HRef = DT.Rows[0]["TwiterLink"].ToString();
                gp.HRef = DT.Rows[0]["GoogleLink"].ToString();
                insta.HRef = DT.Rows[0]["Instagram_Link"].ToString();
            }
        }
    }
}