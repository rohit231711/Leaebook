using System.Web.UI;
using System.Web.UI.WebControls;
using System;

namespace Localization
{
   public class LocalizedButton : Button, ILocalized
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
         //for (int i = 0; i < Controls.Count; i++)
         //{
         //    Parameter parameter = Controls[i] as Parameter;
         //    if (parameter != null)
         //    {
         //        string k = parameter.Key;
         //        string v = parameter.Value;
         //        value = value.Replace('{' + k.ToUpper() + '}', v);
         //    }
         //}
         //base.Text = value;
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