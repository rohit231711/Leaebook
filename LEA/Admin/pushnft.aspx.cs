using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using System.Net;
using System.Text;
public partial class admin_pushnft : System.Web.UI.Page
{

    RegistrationBAL ObjRegistration = new RegistrationBAL();
    DataTable DT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        bool send = false;
        if (txtpushNotification.Text.ToString().Trim() != "")
        {

            DT = ObjRegistration.GetIphoneIDForIphone(Convert.ToInt32(rdbnew.Items[0].Selected), Convert.ToInt32(rdbnew.Items[1].Selected), 0);
            if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
            {
                int i = 0;
                for (i = 0; i <= DT.Rows.Count - 1; i++)
                {
                    string str = "http://www.vrinsofts.com/flipcatalogue/SonicStudio/psh.php?deviceToken=" + DT.Rows[i]["IphoneID"].ToString() + "&message=" + txtpushNotification.Text.ToString().Trim();
                    ReadkwHtml(str);

                }

            }
            DT = null;
            DT = ObjRegistration.GetIphoneIDForIphone(Convert.ToInt32(rdbnew.Items[0].Selected), Convert.ToInt32(rdbnew.Items[1].Selected), 1);
            if (DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "")
            {
                int i = 0;
                for (i = 0; i <= DT.Rows.Count - 1; i++)
                {

                    ObjRegistration.SendNotification(DT.Rows[i]["IphoneID"].ToString(), txtpushNotification.Text);
                }

            }

            send = true;
        }
        else
        {
            send = false;
        }
        if (send)
        {
            lblMessage.Text = "Message sent successfully";
        }
        else
        {
            lblMessage.Text = "Some error occur,please try again letter.";
        }
        txtpushNotification.Text = "";

    }

    public string ReadkwHtml(string URL)
    {
        WebClient objWebclient = new WebClient();
        Byte[] PageHTMLBytes = null;
        PageHTMLBytes = objWebclient.DownloadData(URL);
        UTF8Encoding oUTF8 = new UTF8Encoding();
        string data = oUTF8.GetString(PageHTMLBytes);
        return data;
    }

}