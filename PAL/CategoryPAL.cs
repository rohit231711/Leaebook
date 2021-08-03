using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class CategoryPAL
    {
        #region Private Variables
        private Int64 _CategoryID;
        private Int64 _LanguageID;
        private String _CategoryName;
        private String _CategoryNameEng;
        private String _CategoryNameSpa;
        private String _Title;
        private String _Description;

        public string SortColumn { get; set; }
        public string SortStatus { get; set; }

        #endregion

        #region Public Properties
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


        public Int64 CategoryID
        {
            get
            {
                return _CategoryID;
            }
            set
            {
                _CategoryID = value;
            }
        }
        public String CategoryName
        {
            get
            {
                return _CategoryName;
            }
            set
            {
                _CategoryName = value;
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

        public String CategoryNameEng
        {
            get
            {
                return _CategoryNameEng;
            }
            set
            {
                _CategoryNameEng = value;
            }
        }

        public String CategoryNameSpa
        {
            get
            {
                return _CategoryNameSpa;
            }
            set
            {
                _CategoryNameSpa = value;
            }
        }

        public Boolean IsActive
        {
            get;
            set;

        }

        public int totalCount
        {
            get;
            set;
        }
        public String CImagePath
        {
            get
           ;
            set
          ;
        }
        #endregion
    }
}
