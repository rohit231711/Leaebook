using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class BookShippingPAL
    {
        public Int64 BookShippingID { get; set; }
        public Int64 BookID { get; set; }
        public Int64 CountryID { get; set; }
        public String ShippingCharge { get; set; }
        public Int64 Result { get; set; }
    }
}
