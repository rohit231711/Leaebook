using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class BlogPAL
    {
        #region Private Variables

        private Int64 _BlogID;
        private String _Title;
        private String _TitleEnglish;
        private String _TitleSpanish;
        private String _Description;
        private Int64 _LanguageID;
        private DateTime _CreatedDate;

        public string SortColumn { get; set; }
        public string SortStatus { get; set; }

        #endregion

        #region Public Variables

        public Int64 BlogID
        {
            get
            {
                return _BlogID;
            }
            set
            {
                _BlogID = value;
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

        public String TitleEnglish
        {
            get
            {
                return _TitleEnglish;
            }
            set
            {
                _TitleEnglish = value;
            }
        }

        public String TitleSpanish
        {
            get
            {
                return _TitleSpanish;
            }
            set
            {
                _TitleSpanish = value;
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

        public Boolean ISActive { get; set; }

        public String BlogImage { get; set; }

        public int totalCount
        {
            get;
            set;
        }

        #endregion
    }
}
