using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class CmsPAL
    {
        #region Private Variables
        private Int64 _ID;
        private String _Title;
        private String _Description;
        private Boolean _IsActive;
        private Int64 _LanguageID;

        public string Metatitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        #endregion

        #region Public Properties
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
        public String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
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

        #endregion
    }
}
