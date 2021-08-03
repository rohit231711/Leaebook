using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Web.UI.HtmlControls;

namespace Localization
{
    public class LocalizedTextBox : HtmlInputText
    {
        #region Fields and Properties
        private string placeolder_key;        
        public string PlaceholderKey
        {
            get { return placeolder_key; }
            set { placeolder_key = value; }
        }        
        #endregion

        protected override void Render(HtmlTextWriter writer)
        {
            try
            {
                string value = ResourceManager.GetString(placeolder_key);
                base.Attributes.Add("placeholder", value);
                base.Render(writer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
