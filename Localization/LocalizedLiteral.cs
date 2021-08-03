using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace Localization
{
    public class LocalizedLiteral : Literal, ILocalized
    {

        #region fields and properties
        private string key;
        private bool colon = false;

        public bool Colon
        {
            get { return colon; }
            set { colon = value; }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                base.Text = ResourceManager.GetString(key);
                if (colon)
                {
                    base.Text += ResourceManager.Colon;
                }
                base.Render(writer);
                
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}