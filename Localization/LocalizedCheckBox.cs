using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace Localization
{
    public class LocalizedCheckBox : CheckBox, ILocalized
    {
        #region Fields and Properties
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
                base.Text = LocalizedUtility.ReplaceParameters(Controls, value);
                base.Render(writer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}