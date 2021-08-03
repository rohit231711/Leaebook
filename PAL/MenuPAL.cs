using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class MenuPAL
    {

        #region Private Variables
        private Int32 _MenuID;
        private Int32 _UserID;
        private String _MenuName;
        private Int32 _ParentID;
        #endregion

        #region Public Properties
        public Int32 MenuID
        {
            get
            {
                return _MenuID;
            }
            set
            {
                _MenuID = value;
            }
        }
        public Int32 UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }
        public String MenuName
        {
            get
            {
                return _MenuName;
            }
            set
            {
                _MenuName = value;
            }
        }
        public Int32 ParentID
        {
            get
            {
                return _ParentID;
            }
            set
            {
                _ParentID = value;
            }
        }
        #endregion

    }
}
