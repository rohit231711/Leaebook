using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class LanguagePAL
    {
        #region Private Variables
        private Int32 _ID;
        private String _Language;
        private String _CultureCode;

        #endregion

        #region Public Properties
        public Int32 ID
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
        public String Language
        {
            get
            {
                return _Language;
            }
            set
            {
                _Language = value;
            }
        }
        public String CultureCode
        {
            get
            {
                return _CultureCode;
            }
            set
            {
                _CultureCode = value;
            }
        }
       
        #endregion
    }
}
