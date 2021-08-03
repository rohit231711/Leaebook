using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class RegistrationPAL
    {

        #region Private Variables
        private Int64 _RegistrationID;
        private String _FirstName;
        private String _LastName;
        private String _Deviceid;
        private String _DeviceType;
        private String _UserName;
        private String _EmailAddress;
        private String _Password;
        private DateTime _CreatedDate;
        private Boolean _IsActive;
        private Boolean _IsNewsLetter;
        private String _ActivationID;
        private String _BookID;


        private String _ToDate;
        private String _FromDate;
        private String _BirthdayDate;
        private String _Publisher;
        
        private String _BookTitle;
        private Int64 _GenderID;
        private Int64 _LanguageID;
        public String Publisher
        {
            get
            {
                return _Publisher;
            }
            set
            {
                _Publisher = value;
            }
        }
        public String BookTitle
        {
            get
            {
                return _BookTitle;
            }
            set
            {
                _BookTitle = value;
            }
        }

        private String _SortColumn;
        private String _SortStatus;
        private Int64 _Countryid;
        #endregion

        #region Public Properties
        public Int64 RegistrationID
        {
            get
            {
                return _RegistrationID;
            }
            set
            {
                _RegistrationID = value;
            }
        }
        public Int64 Countryid
        {
            get
            {
                return _Countryid;
            }
            set
            {
                _Countryid = value;
            }
        }
        public Int64 LanguageID
        {
            get
            {
                return _LanguageID;
            }
            set
            {
                _LanguageID = value;
            }
        }
        public Int64 GenderID
        {
            get
            {
                return _GenderID;
            }
            set
            {
                _GenderID = value;
            }
        }
        public String BookID
        {
            get
            {
                return _BookID;
            }
            set
            {
                _BookID = value;
            }
        }
        public String ToDate
        {
            get
            {
                return _ToDate;
            }
            set
            {
                _ToDate = value;
            }
        }
        public String FromDate
        {
            get
            {
                return _FromDate;
            }
            set
            {
                _FromDate = value;
            }
        }

        public String BirthdayDate
        {
            get
            {
                return _BirthdayDate;
            }
            set
            {
                _BirthdayDate = value;
            }
        }
        public String FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        public String ActivationID
        {
            get
            {
                return _ActivationID;
            }
            set
            {
                _ActivationID = value;
            }
        }
        public String UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }

        public String LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }

        public String Deviceid
        {
            get
            {
                return _Deviceid;
            }
            set
            {
                _Deviceid = value;
            }
        }

        public String DeviceType
        {
            get
            {
                return _DeviceType;
            }
            set
            {
                _DeviceType = value;
            }
        }

        public String EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
                _EmailAddress = value;
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
        public String SortColumn
        {
            get
            {
                return _SortColumn;
            }
            set
            {
                _SortColumn = value;
            }
        }
        public String SortStatus
        {
            get
            {
                return _SortStatus;
            }
            set
            {
                _SortStatus = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }
        public Boolean IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }
        public Boolean IsNewsLetter
        {
            get
            {
                return _IsNewsLetter;
            }
            set
            {
                _IsNewsLetter = value;
            }
        }
        public int totalCount
        {
            get;
            set;
        }
        public int UserType
        {
            get;
            set;
        }

        public Boolean NewIssues
        {
            get;
            set;
        }

        public Boolean Renewals
        {
            get;
            set;
        }

        public Boolean AppUpdates
        {
            get;
            set;
        }
        public String FacebookEmail
        {
            get;
            set;
        }
        #endregion

    }
}
