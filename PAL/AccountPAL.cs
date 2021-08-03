using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class AccountPAL
    {
        #region Private Variables
        private String _Email;
        private String _Password;        
        private String _FirstName;
        private String _LastName;        
        private DateTime _CreatedDate;
        private Boolean _IsActive;
        private Boolean _IsNewsLetter;
        private Int32 _UserType;
        private DateTime _LoginDate;
        private DateTime _LastLoginDate;
        private Boolean _NewIssues;
        private Boolean _Renewals;
        private Boolean _AppUpdates;
        private String _FacebookEmail;
        private String _ActivationID;
        private String _UserName;
        private Int32 _GenderID;
        private DateTime _BirthdayDate;
        private Int32 _Countryid;
        private Int32 _LanguageID;
        private Int32 _RegistrationID;
        #endregion

        #region Public Properties

        public Int32 RegistrationID
        {
            get { return _RegistrationID; }
            set { _RegistrationID = value; }
        }
        public String Email
        {
            get{return _Email;}
            set{_Email = value;} 
        }
        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        public String FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public String LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public DateTime CreatedDate
        {
            get { return _CreatedDate; }
            set { _CreatedDate = value; }
        }
        public Boolean IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }
        public Boolean IsNewsLetter
        {
            get { return _IsNewsLetter; }
            set { _IsNewsLetter = value; }
        }
        public Int32 UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
        public DateTime LoginDate
        {
            get { return _LoginDate; }
            set { _LoginDate = value; }
        }
        public DateTime LastLoginDate
        {
            get { return _LastLoginDate; }
            set { _LastLoginDate = value; }
        }
        public Boolean NewIssues
        {
            get { return _NewIssues; }
            set { _NewIssues = value; }
        }
        public Boolean Renewals
        {
            get { return _Renewals; }
            set { _Renewals = value; }
        }
        public Boolean AppUpdates
        {
            get { return _AppUpdates; }
            set { _AppUpdates = value; }
        }
        public String FacebookEmail
        {
            get { return _FacebookEmail; }
            set { _FacebookEmail = value; }
        }
        public String ActivationID
        {
            get { return _ActivationID; }
            set { _ActivationID = value; }
        }
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public Int32 GenderID
        {
            get { return _GenderID; }
            set { _GenderID = value; }
        }
        public DateTime BirthdayDate
        {
            get { return _BirthdayDate; }
            set { _BirthdayDate = value; }
        }        
        public Int32 Countryid
        {
            get { return _Countryid; }
            set { _Countryid = value; }
        }
        public Int32 LanguageID
        {
            get { return _LanguageID; }
            set { _LanguageID = value; }
        }        
        #endregion
    }
}
