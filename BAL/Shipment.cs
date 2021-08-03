using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BAL
{
    public class Shipment
    {
        ShippingSettingsBAL objSetting = new ShippingSettingsBAL();
        //static data
        public static string SiteID = "xmleditcinco";
        public static string Password = "I64XtmJUzF";

        #region XML Methods
        //USE ONLY FOR XML-METHOD
        public string replaceXml(string xml, string NumberOfPieces, string pieces, string totalWeight, string DeclaredValue)
        {
            var dt = objSetting.GetAllShippingseetings();
            xml = xml.Replace("${SiteID}", dt.Rows[0]["SiteID"].ToString());
            xml = xml.Replace("${Password}", dt.Rows[0]["Password"].ToString());
            xml = xml.Replace("${ShipperAccountNumber}", dt.Rows[0]["ShipperAccountNumber"].ToString());
            xml = xml.Replace("${BillingAccountNumber}", dt.Rows[0]["BillingAccountNumber"].ToString());
            xml = xml.Replace("${DutyAccountNumber}", dt.Rows[0]["DutyAccountNumber"].ToString());
            xml = xml.Replace("${CompanyName}", dt.Rows[0]["CompanyName"].ToString());
            xml = xml.Replace("${AddressLine1}", dt.Rows[0]["AddressLine1"].ToString());
            xml = xml.Replace("${AddressLine2}", dt.Rows[0]["AddressLine2"].ToString());
            xml = xml.Replace("${AddressLine3}", dt.Rows[0]["AddressLine3"].ToString());
            xml = xml.Replace("${City}", dt.Rows[0]["City"].ToString());
            xml = xml.Replace("${PostalCode}", dt.Rows[0]["PostalCode"].ToString());
            xml = xml.Replace("${CountryCode}", dt.Rows[0]["CountryCode"].ToString());
            xml = xml.Replace("${CountryName}", dt.Rows[0]["CountryName"].ToString());
            xml = xml.Replace("${PersonName}", dt.Rows[0]["PersonName"].ToString());
            xml = xml.Replace("${PhoneNum}", dt.Rows[0]["PhoneNumber"].ToString());
            xml = xml.Replace("${Email}", dt.Rows[0]["Email"].ToString());
            xml = xml.Replace("${PhoneExtension}", dt.Rows[0]["PhoneExtension"].ToString());
            xml = xml.Replace("${FaxNumber}", dt.Rows[0]["FaxNumber"].ToString());
            xml = xml.Replace("${Telex}", dt.Rows[0]["Telex"].ToString());
            xml = xml.Replace("${ShipperID}", dt.Rows[0]["ShipperID"].ToString());
            xml = xml.Replace("${ShipperCompanyName}", dt.Rows[0]["ShipperCompanyName"].ToString());
            xml = xml.Replace("${ShipperRegisteredAccount}", dt.Rows[0]["ShipperRegisteredAccount"].ToString());
            xml = xml.Replace("${ShipperAddressLine1}", dt.Rows[0]["ShipperAddressLine1"].ToString());
            xml = xml.Replace("${ShipperAddressLine2}", dt.Rows[0]["ShipperAddressLine2"].ToString());
            xml = xml.Replace("${ShipperCity}", dt.Rows[0]["ShipperCity"].ToString());
            xml = xml.Replace("${ShipperPostalCode}", dt.Rows[0]["ShipperPostalCode"].ToString());
            xml = xml.Replace("${ShipperCountryCode}", dt.Rows[0]["ShipperCountryCode"].ToString());
            xml = xml.Replace("${ShipperCountryName}", dt.Rows[0]["ShipperCountryName"].ToString());
            xml = xml.Replace("${ShipperPersonName}", dt.Rows[0]["ShipperPersonName"].ToString());
            xml = xml.Replace("${ShipperPhoneNumber}", dt.Rows[0]["ShipperPhoneNumber"].ToString());
            xml = xml.Replace("${ShipperPhoneExtension}", dt.Rows[0]["ShipperPhoneExtension"].ToString());
            xml = xml.Replace("${ShipperFaxNumber}", dt.Rows[0]["ShipperFaxNumber"].ToString());
            xml = xml.Replace("${ShipperTelex}", dt.Rows[0]["ShipperTelex"].ToString());
            xml = xml.Replace("${ShipperEmail}", dt.Rows[0]["ShipperEmail"].ToString());

            xml = xml.Replace("${DeclaredValue}", DeclaredValue);
            xml = xml.Replace("${NumberOfPieces}",NumberOfPieces);
            xml = xml.Replace("${TotalWeight}", totalWeight);
            xml = xml.Replace("${Pieces}", pieces);
            xml = xml.Replace("${Date}", DateTime.Now.ToString("yyyy-MM-dd"));

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
