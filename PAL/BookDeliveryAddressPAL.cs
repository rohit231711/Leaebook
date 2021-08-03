using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class BookDeliveryAddressPAL
    {
        private Int64 _BookDeliveryAddID;
        public Int64 UserID { get; set; }
        public Boolean IsDefault { get; set; }
        public String Name { get; set; }
        public String StreetAddress { get; set; }
        public String Landmark { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Country { get; set; }
        public String Pincode { get; set; }
        public String PhoneNumber { get; set; }

        public Int64 BookDeliveryAddID
        {
            get
            {
                return _BookDeliveryAddID;
            }
            set
            {
                _BookDeliveryAddID = value;
            }
        }
    }
}
