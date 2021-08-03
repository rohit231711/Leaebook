using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class WebsiteSettingsPAL
    {
        #region Private Variables
        private Int64 _WebSettingID;
        private String _FaceBookLink;
        private String _TwiterLink;
        private String _GoogleLink;
        private String _Pinterest_Link;
        private String _Instagram_Link;
        private String _ContactUs;
        private String _BookStorePhone;

        #endregion

        public Int64 WebSettingID
        {
            get
            {
                return _WebSettingID;
            }
            set
            {
                _WebSettingID = value;
            }
        }

        public String BookStorePhone
        {
            get
            {
                return _BookStorePhone;
            }
            set
            {
                _BookStorePhone = value;
            }
        }

        public String FaceBookLink
        {
            get
            {
                return _FaceBookLink;
            }
            set
            {
                _FaceBookLink = value;
            }
        }

        public String TwiterLink
        {
            get
            {
                return _TwiterLink;
            }
            set
            {
                _TwiterLink = value;
            }
        }

        public String GoogleLink
        {
            get
            {
                return _GoogleLink;
            }
            set
            {
                _GoogleLink = value;
            }
        }

        public String Pinterest_Link
        {
            get
            {
                return _Pinterest_Link;
            }
            set
            {
                _Pinterest_Link = value;
            }
        }

        public String Instagram_Link
        {
            get
            {
                return _Instagram_Link;
            }
            set
            {
                _Instagram_Link = value;
            }
        }

        public String ContactUs
        {
            get
            {
                return _ContactUs;
            }
            set
            {
                _ContactUs = value;
            }
        }
    }
}
