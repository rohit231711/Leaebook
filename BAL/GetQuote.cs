using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace BAL
{
    public class GetQuote
    {
        DHLQuoteSettingBAL objSetting = new DHLQuoteSettingBAL();
        //static data
        public static string SiteID = "xmleditcinco";
        public static string Password = "I64XtmJUzF";

        #region XML Methods
        //USE ONLY FOR XML-METHOD
        public string replaceXml(string xml, string CountryCode, string pieces, string Postalcode, string City)
        {
            var dt = objSetting.GetAllDHLQuoteSetting();
            xml = xml.Replace("${SiteID}", dt.Rows[0]["SiteID"].ToString());
            xml = xml.Replace("${Password}", dt.Rows[0]["Password"].ToString());

            xml = xml.Replace("${CountryCode}", CountryCode);
            if (Postalcode != "")
            {
                xml = xml.Replace("${Postalcode}", "<Postalcode>" + Postalcode + "</Postalcode>");
            }
            else
            {
                xml = xml.Replace("${Postalcode}", "<Postalcode></Postalcode>");
            }
            xml = xml.Replace("${City}", City);
            xml = xml.Replace("${Pieces}", pieces);
            xml = xml.Replace("${Date}", DateTime.Now.ToString("yyyy-MM-dd"));

            xml = xml.Replace("${FromCountryCode}", dt.Rows[0]["FromCountryCode"].ToString());
            if (dt.Rows[0]["FromCodeCity"].ToString() == "1")
                xml = xml.Replace("${CityPostalcode}", "<Postalcode>" + dt.Rows[0]["FromPostalcode"].ToString() + "</Postalcode>");
            else if (dt.Rows[0]["FromCodeCity"].ToString() == "2")
                xml = xml.Replace("${CityPostalcode}", "<City>" + dt.Rows[0]["FromCity"].ToString() + "</City>");

            xml = xml.Replace("${PaymentCountryCode}", dt.Rows[0]["PaymentCountryCode"].ToString());
            xml = xml.Replace("${ReadyTime}", dt.Rows[0]["ReadyTime"].ToString());
            xml = xml.Replace("${ReadyTimeGMTOffset}", dt.Rows[0]["ReadyTimeGMTOffset"].ToString());
            xml = xml.Replace("${DimensionUnit}", dt.Rows[0]["DimensionUnit"].ToString());
            xml = xml.Replace("${WeightUnit}", dt.Rows[0]["WeightUnit"].ToString());
            xml = xml.Replace("${PaymentAccountNumber}", dt.Rows[0]["PaymentAccountNumber"].ToString());
            xml = xml.Replace("${IsDutiable}", dt.Rows[0]["IsDutiable"].ToString());
            xml = xml.Replace("${NetworkTypeCode}", dt.Rows[0]["NetworkTypeCode"].ToString());

            xml = xml.Replace("${GlobalProductCode}", dt.Rows[0]["GlobalProductCode"].ToString());
            xml = xml.Replace("${LocalProductCode}", dt.Rows[0]["LocalProductCode"].ToString());
            xml = xml.Replace("${SpecialServiceType}", dt.Rows[0]["SpecialServiceType"].ToString());

            xml = xml.Replace("${DeclaredCurrency}", dt.Rows[0]["DeclaredCurrency"].ToString());
            xml = xml.Replace("${DeclaredValue}", dt.Rows[0]["DeclaredValue"].ToString());

            return xml;
        }

        public string sendRequest(string xmlData)
        {
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
