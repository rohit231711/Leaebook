using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BAL
{
    public class Tracking
    {
        public static string SiteID = "xmleditcinco";
        public static string Password = "I64XtmJUzF";
        ShippingSettingsBAL objSetting = new ShippingSettingsBAL();

        #region XML Methods
        //USE ONLY FOR XML-METHOD
        public string replaceXml(string xml, string AWBNumber)
        {
            var dt = objSetting.GetAllShippingseetings();
            xml = xml.Replace("${SiteID}", dt.Rows[0]["SiteID"].ToString());
            xml = xml.Replace("${Password}", dt.Rows[0]["Password"].ToString());
            xml = xml.Replace("${AWBNumber}", AWBNumber);

            return xml;
        }

        public string sendRequest(string xmlData)
        {
            //WebRequest requestRate = HttpWebRequest.Create("https://xmlpitest-ea.dhl.com/XMLShippingServlet");
            WebRequest requestRate = HttpWebRequest.Create("https://xmlpi-ea.dhl.com/XMLShippingServlet");
            requestRate.ContentType = "application/x-www-form-urlencoded";
            requestRate.Method = "POST";

            using (var stream = requestRate.GetRequestStream())
            {
                var arrBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(xmlData);
                stream.Write(arrBytes, 0, arrBytes.Length);
                stream.Close();
            }

            WebResponse responseRate = requestRate.GetResponse();
            var respStream = responseRate.GetResponseStream();
            var reader = new StreamReader(respStream, System.Text.Encoding.ASCII);
            string responseString = reader.ReadToEnd();
            respStream.Close();

            //HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://xmlpi-ea.dhl.com/XMLShippingServlet"); //insert your interface here
            //req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded";
            //string postData = "load=" + xmlData;
            //req.ContentLength = postData.Length;

            //StreamWriter stOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
            //stOut.Write(postData);
            //stOut.Close();

            //WebResponse resp = req.GetResponse();
            //Stream respStream = resp.GetResponseStream();
            //StreamReader respReader = new StreamReader(respStream, Encoding.UTF8);
            //string responseString = respReader.ReadToEnd();

            return responseString;
        }

        #endregion
    }
}
