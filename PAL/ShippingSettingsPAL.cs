using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class ShippingSettingsPAL
    {
        #region Private Variables
        private Int64 _ID;
        private String _SiteID;
        private String _Password;
        private String _ShipperAccountNumber;
        private String _BillingAccountNumber;
        private String _DutyAccountNumber;
        private String _CompanyName;
        private String _AddressLine1;
        private String _AddressLine2;
        private String _AddressLine3;
        private String _City;
        private String _PostalCode;
        private String _CountryCode;
        private String _CountryName;
        private String _PersonName;
        private String _PhoneNumber;
        private String _PhoneExtension;
        private String _FaxNumber;
        private String _Telex;
        private String _Email;
        private String _ShipperID;
        private String _ShipperCompanyName;
        private String _ShipperRegisteredAccount;
        private String _ShipperAddressLine1;
        private String _ShipperAddressLine2;
        private String _ShipperCity;
        private String _ShipperPostalCode;
        private String _ShipperCountryCode;
        private String _ShipperCountryName;
        private String _ShipperPersonName;
        private String _ShipperPhoneNumber;
        private String _ShipperPhoneExtension;
        private String _ShipperFaxNumber;
        private String _ShipperTelex;
        private String _ShipperEmail;

        
        #endregion

        public String CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        
        public Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public String AddressLine1
        {
            get
            {
                return _AddressLine1;
            }
            set
            {
                _AddressLine1 = value;
            }
        }

        public String SiteID
        {
            get
            {
                return _SiteID;
            }
            set
            {
                _SiteID = value;
            }
        }

        public String Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        public String ShipperAccountNumber
        {
            get
            {
                return _ShipperAccountNumber;
            }
            set
            {
                _ShipperAccountNumber = value;
            }
        }

        public String BillingAccountNumber
        {
            get
            {
                return _BillingAccountNumber;
            }
            set
            {
                _BillingAccountNumber = value;
            }
        }

        public String DutyAccountNumber
        {
            get
            {
                return _DutyAccountNumber;
            }
            set
            {
                _DutyAccountNumber = value;
            }
        }

        public String ShipperEmail
        {
            get { return _ShipperEmail; }
            set { _ShipperEmail = value; }
        }

        public String ShipperTelex
        {
            get { return _ShipperTelex; }
            set { _ShipperTelex = value; }
        }

        public String ShipperFaxNumber
        {
            get { return _ShipperFaxNumber; }
            set { _ShipperFaxNumber = value; }
        }

        public String ShipperPhoneExtension
        {
            get { return _ShipperPhoneExtension; }
            set { _ShipperPhoneExtension = value; }
        }

        public String ShipperPhoneNumber
        {
            get { return _ShipperPhoneNumber; }
            set { _ShipperPhoneNumber = value; }
        }

        public String ShipperPersonName
        {
            get { return _ShipperPersonName; }
            set { _ShipperPersonName = value; }
        }

        public String ShipperCountryName
        {
            get { return _ShipperCountryName; }
            set { _ShipperCountryName = value; }
        }

        public String ShipperCountryCode
        {
            get { return _ShipperCountryCode; }
            set { _ShipperCountryCode = value; }
        }

        public String ShipperPostalCode
        {
            get { return _ShipperPostalCode; }
            set { _ShipperPostalCode = value; }
        }

        public String ShipperCity
        {
            get { return _ShipperCity; }
            set { _ShipperCity = value; }
        }

        public String ShipperAddressLine2
        {
            get { return _ShipperAddressLine2; }
            set { _ShipperAddressLine2 = value; }
        }

        public String ShipperAddressLine1
        {
            get { return _ShipperAddressLine1; }
            set { _ShipperAddressLine1 = value; }
        }

        public String ShipperRegisteredAccount
        {
            get { return _ShipperRegisteredAccount; }
            set { _ShipperRegisteredAccount = value; }
        }

        public String ShipperCompanyName
        {
            get { return _ShipperCompanyName; }
            set { _ShipperCompanyName = value; }
        }

        public String ShipperID
        {
            get { return _ShipperID; }
            set { _ShipperID = value; }
        }

        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public String Telex
        {
            get { return _Telex; }
            set { _Telex = value; }
        }

        public String FaxNumber
        {
            get { return _FaxNumber; }
            set { _FaxNumber = value; }
        }


        public String PhoneExtension
        {
            get { return _PhoneExtension; }
            set { _PhoneExtension = value; }
        }


        public String PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }


        public String PersonName
        {
            get { return _PersonName; }
            set { _PersonName = value; }
        }


        public String CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }


        public String CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }


        public String PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }


        public String City
        {
            get { return _City; }
            set { _City = value; }
        }


        public String AddressLine2
        {
            get { return _AddressLine2; }
            set { _AddressLine2 = value; }
        }
        public String AddressLine3
        {
            get { return _AddressLine3; }
            set { _AddressLine3 = value; }
        }
        

    }
}
