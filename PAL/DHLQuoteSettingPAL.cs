using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class DHLQuoteSettingPAL
    {
        public int ID { get; set; }
        public string SiteID { get; set; }
        public string Password { get; set; }
        public string FromCountryCode { get; set; }
        public int FromCodeCity { get; set; }
        public string FromPostalcode { get; set; }
        public string FromCity { get; set; }
        public string PaymentCountryCode { get; set; }
        public string ReadyTime { get; set; }
        public string ReadyTimeGMTOffset { get; set; }
        public string DimensionUnit { get; set; }
        public string WeightUnit { get; set; }
        public string PaymentAccountNumber { get; set; }
        public string IsDutiable { get; set; }
        public string NetworkTypeCode { get; set; }
        public string GlobalProductCode { get; set; }
        public string LocalProductCode { get; set; }
        public string SpecialServiceType { get; set; }
        public string DeclaredCurrency { get; set; }
        public string DeclaredValue { get; set; }

    }
}
