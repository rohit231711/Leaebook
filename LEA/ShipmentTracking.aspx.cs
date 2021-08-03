using BAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class ShipmentTracking : System.Web.UI.Page
{
    Tracking t = new Tracking();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["track"] != null)
        {
            txtTrackNumber.Text = Request.QueryString["track"];
            btnTrack_Click(btnTrack, null);
        }
    }
    protected void btnTrack_Click(object sender, EventArgs e)
    {
        StreamReader streamReader = new StreamReader(Server.MapPath("~/XML/Tracking.xml"));
        string text = streamReader.ReadToEnd();
        string xmlRequest = t.replaceXml(text, txtTrackNumber.Text);
        string response = t.sendRequest(xmlRequest);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(response);

        try
        {
            var status = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/Status/ActionStatus").InnerText;
            if (status.ToLower() == "success")
            {
                divTrack.Visible = true;
                divNoTrack.Visible = false;
                lblWaybill.Text = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/AWBNumber").InnerText;
                lblInfo.Text = lblInfo1.Text = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/ShipmentEvent/ServiceEvent/Description").InnerText;
                lblOrigin1.Text = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/OriginServiceArea/ServiceAreaCode").InnerText;
                lblOrigin.Text = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/OriginServiceArea/ServiceAreaCode").InnerText + " "
                               + xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/OriginServiceArea/Description").InnerText;
                lblDestination.Text = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/DestinationServiceArea/ServiceAreaCode").InnerText + " "
                                    + xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/DestinationServiceArea/Description").InnerText;
                lblDate.Text = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/ShipmentEvent/Date").InnerText;
                lblTime.Text = xmlDoc.DocumentElement.SelectSingleNode("AWBInfo/ShipmentInfo/ShipmentEvent/Time").InnerText;

            }
            else
            {
                divTrack.Visible = false;
                divNoTrack.Visible = true;
            }
        }
        catch (Exception ex)
        {
            divTrack.Visible = false;
            divNoTrack.Visible = true;
        }
    }
}