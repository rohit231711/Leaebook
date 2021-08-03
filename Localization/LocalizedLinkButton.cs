using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace Localization
{
    public class LocalizedLinkButton : LinkButton, ILocalized
    {
        #region Fields and Properties
        private string key;
        private bool colon;
        private string confirmKey;

        public string ConfirmKey
        {
            get { return confirmKey; }
            set { confirmKey = value; }
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
                base.Render(writer);
                if (confirmKey != null)
                {
                    Attributes.Add("onClick", "return confirm('" + ResourceManager.GetString(confirmKey).Replace("'", "\'") + "');");
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