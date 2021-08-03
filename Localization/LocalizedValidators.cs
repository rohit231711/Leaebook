using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace Localization
{


    #region RequiredFieldValidator
    public class LocalizedRequiredFieldValidator : RequiredFieldValidator, ILocalized
    {
        #region fields and properties
        private string key;
        private bool colon = false;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public bool Colon
        {
            get { return colon; }
            set { colon = value; }
        }
        #endregion


        protected override void Render(HtmlTextWriter writer)
        {

            try
            {
                string value = ResourceManager.GetString(key);
                if (colon)
                {
                    value += ResourceManager.Colon;
                }
                base.ErrorMessage = LocalizedUtility.ReplaceParameters(Controls, value);
                base.Render(writer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    #endregion


    #region RegularExpressionValidator
    public class LocalizedRegularExpressionValidator : RegularExpressionValidator, ILocalized
    {
        #region fields and properties
        private string key;
        private bool newLine = false;
        private bool colon = false;

        public bool NewLine
        {
            get { return newLine; }
            set { newLine = value; }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public bool Colon
        {
            get { return colon; }
            set { colon = value; }
        }
        #endregion


        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                string value = ResourceManager.GetString(key);
                if (colon)
                {
                    value += ResourceManager.Colon;
                }
                base.ErrorMessage = LocalizedUtility.ReplaceParameters(Controls, value);
                base.Render(writer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    #endregion


    #region CompareValidator
    public class LocalizedCompareValidator : CompareValidator, ILocalized
    {
        #region fields and properties
        private string key;
        private bool newLine = false;
        private bool colon = false;

        public bool NewLine
        {
            get { return newLine; }
            set { newLine = value; }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public bool Colon
        {
            get { return colon; }
            set { colon = value; }
        }
        #endregion


        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                string value = ResourceManager.GetString(key);
                if (colon)
                {
                    value += ResourceManager.Colon;
                }
                base.ErrorMessage = LocalizedUtility.ReplaceParameters(Controls, value);
                base.Render(writer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    #endregion
}