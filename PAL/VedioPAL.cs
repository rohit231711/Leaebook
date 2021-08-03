using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class VedioPAL
    {
        #region Private Variables
        private Int32 _ID;
        private String _VideoName;
        private String _VideoPath;
        private Boolean _IsActive;

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
        public String VideoName
        {
            get
            {
                return _VideoName;
            }
            set
            {
                _VideoName = value;
            }
        }
        public String VideoPath
        {
            get
            {
                return _VideoPath;
            }
            set
            {
                _VideoPath = value;
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
