using System;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.IO;

namespace Localization
{
    /// <summary>
    ///   Abstract ResourceManager Provider used to access localized content from any source
    ///   
    ///   To create your own implementation of the ResourceManager (say you wanted one to work off of an Access database),
    ///   create a new class which inherits from ResourceManager and implements the abstract methods (RetreiveString and RetrieveImage).
    ///   Your class also has to have a constructor that accepts a single parameter of type NameValueCollection, all attributes from the add element of the configuration file will be placed there
    /// </summary>
    /// <remarks>
    ///   In order to keep the ResourceManager compatible with the previous version,
    ///   we'll keep the static GetString method and Colon property as the main entry point. 
    ///   
    ///   To learn more about the provider pattern, check out http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnaspnet/html/asp02182004.asp
    /// </remarks>  
    public abstract class ResourceManager
    {
        #region Singleton Methods
        private static ResourceManager instance = null;
        /// <summary>
        /// Creates our instance using reflection when our class is first accessed
        /// </summary>
        static ResourceManager()
        {
            try
            {
                Provider provider = LocalizationConfiguration.GetConfig().Provider;
                Type type = Type.GetType(provider.Type);
                if (type == null)
                {
                    throw new ApplicationException(string.Format("Couldn't load type: {0}", provider.Type));
                }
                object[] arguments = new object[] { provider.Parameters };
                instance = (ResourceManager)Activator.CreateInstance(type, arguments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get an instance of the ResourceManager
        /// </summary>
        internal static ResourceManager Instance
        {
            get { return instance; }
        }
        #endregion

        #region Abstract Factory Methods
        /// <summary>
        /// Gets a value associated with the specified key
        /// </summary>
        /// <param name="key">The key of the resource desired</param>
        /// <returns>The value for the specifed key</returns>
        protected abstract string RetrieveString(string key, string cultureName = "");
        /// <summary>
        /// Gets a LocalizedImageData associated with the specified key
        /// </summary>
        /// <param name="key">The key of the data required</param>
        /// <returns>The value for the specified key</returns>
        protected abstract LocalizedImageData RetrieveImage(string key, string cultureName = "");
        #endregion

        #region Public Static Methods
        /// <summary>
        ///     Retrieves the RFC 1766 name of the current culture
        /// </summary>
        /// <remarks>
        ///     This would more likely belong in an utility/global class, but for simplicities sake, I've put it here
        /// </remarks>
        public static string CurrentCultureName
        {
            get { return Thread.CurrentThread.CurrentCulture.Name; }
        }
        /// <summary>
        ///   Wrapper to GetString() to get a colon
        /// </summary>
        /// <remarks>
        ///   Just to showcase how any wrapper/shortcuts can be made and easily exposed
        /// </remarks>
        public static string Colon
        {
            get { return GetString("colon"); }
        }
        /// <summary>
        ///     Returns the localized resource
        /// </summary>
        /// <param name="key" type="string">    
        ///   The name of the resource we want    
        /// </param>
        public static string GetString(string key, string cultureName = "")
        {
            return Instance.RetrieveString(key,cultureName);
        }
        /// <summary>
        ///   Returns the data necessary to display a localized image
        /// </summary>
        /// <param name="key" type="string">The key for the image</param>
        public static LocalizedImageData GetImage(string key, string cultureName = "")
        {
            return Instance.RetrieveImage(key, cultureName);
        }
        #endregion

        #region JavaScript Utility Classes
        public static void RegisterLocaleResource(params string[] keys)
        {
            try
            {
                if (keys == null || keys.Length == 0)
                {
                    return;
                }
                Page page = HttpContext.Current.Handler as Page;
                if (page == null)
                {
                    throw new InvalidOperationException("RegisterResourceManager must be called from within a page");
                }

                //StringBuilder sb = new StringBuilder("<script language=\"JavaScript\">");

                StringBuilder sb = new StringBuilder();
                sb.Append(Environment.NewLine);
                foreach (string key in keys)
                {
                    if (key != null && key != "")
                    {
                        sb.Append("ResourceManager.AddString('");
                        sb.Append(PrepareStringForJavaScript(key));
                        sb.Append("', '");
                        sb.Append(PrepareStringForJavaScript(ResourceManager.GetString(key)));
                        sb.Append("');");
                        sb.Append(Environment.NewLine);
                    }
                }

                //sb.Append("</script>");
                //page.RegisterStartupScript("RM:" + string.Join(":", keys), sb.ToString());
                //page.ClientScript.RegisterStartupScript(typeof(ResourceManager), "RM:" + string.Join(":", keys), sb.ToString());

                string text = File.ReadAllText(HttpContext.Current.Server.MapPath("/Languages/") + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "/Resource.js");
                text = sb.ToString().Replace("\r", "").Replace("\n", "");
                File.WriteAllText(HttpContext.Current.Server.MapPath("/Languages/") + System.Threading.Thread.CurrentThread.CurrentCulture.Name + "/Resource.js", text);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string PrepareStringForJavaScript(string input)
        {
            string str = "";
            try
            {
                if (input == null || input.Length == 0)
                {
                    str = string.Empty;
                }
                else
                {
                    str = input.Replace("\\", "\\\\").Replace("'", "\\'");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
        }
        #endregion
    }
}