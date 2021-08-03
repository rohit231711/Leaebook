using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;

namespace Localization
{
    /// <summary>
    /// The ResourceManagerXml is an implementation of the ResourceManager driven by XML files
    /// </summary>
    public class ResourceManagerXml : ResourceManager
    {
        #region Fields and Properties
        private string fileName;
        #endregion


        #region Constructors
        public ResourceManagerXml(NameValueCollection parameters)
        {
            if (parameters == null || parameters["languageFilePath"] == null)
            {
                throw new ApplicationException("ResourceManagerXml requires fileName attribute in configuraiton.");
            }
            fileName = HttpContext.Current.Server.MapPath(parameters["languageFilePath"]);
        }
        #endregion


        #region Provider API
        /// <remarks>
        ///   When in DEBUG mode, an ApplicationException will be thrown if the key isn't found
        /// </remarks>
        protected override string RetrieveString(string key, string cultureName = "")
        {
            try
            {
                NameValueCollection messages = GetResources(cultureName);
                if (messages[key] == null)
                {
                    messages[key] = string.Empty;
#if DEBUG
                    throw new ApplicationException("Resource value not found for key: " + key);
#endif
                }
                return messages[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override LocalizedImageData RetrieveImage(string key, string cultureName = "")
        {
            try
            {
                Hashtable imageData = GetImages(cultureName);
                if (imageData[key] == null)
                {
                    imageData[key] = new LocalizedImageData(0, 0, string.Empty);
#if DEBUG
                    throw new ApplicationException("Resource value not found for key: " + key);
#endif
                }
                return (LocalizedImageData)imageData[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Private Methods

        private NameValueCollection GetResources(string cultureName)
        {
            try
            {
                string currentCulture = ResourceManager.CurrentCultureName;
                if ( !string.IsNullOrEmpty( cultureName))
                {
                    currentCulture = cultureName;
                }
                string defaultCulture = LocalizationConfiguration.GetConfig().DefaultCultureName;

                string cacheKey = "Localization:" + defaultCulture + ':' + currentCulture;
                if (HttpRuntime.Cache[cacheKey] == null)
                {
                    NameValueCollection resource = new NameValueCollection();
                    LoadResources(resource, defaultCulture, cacheKey);
                    if (defaultCulture != currentCulture)
                    {
                        try
                        {
                            LoadResources(resource, currentCulture, cacheKey);
                        }
                        catch (FileNotFoundException)
                        { }
                    }
                }
                return (NameValueCollection)HttpRuntime.Cache[cacheKey];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadResources(NameValueCollection resource, string culture, string cacheKey)
        {
            try
            {
                string file = string.Format("{0}\\{1}\\Resource.xml", fileName, culture);
                XmlDocument xml = new XmlDocument();
                xml.Load(file);
                foreach (XmlNode n in xml.SelectSingleNode("Resource"))
                {
                    if (n.NodeType != XmlNodeType.Comment)
                    {
                        resource[n.Attributes["name"].Value] = n.InnerText;
                    }
                }
                HttpRuntime.Cache.Insert(cacheKey, resource, new CacheDependency(file), DateTime.MaxValue, TimeSpan.Zero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Hashtable GetImages(string cultureName)
        {
            try
            {
                string currentCulture = ResourceManager.CurrentCultureName;
                if (!string.IsNullOrEmpty(cultureName))
                {
                    currentCulture = cultureName;
                }
                string defaultCulture = LocalizationConfiguration.GetConfig().DefaultCultureName;

                string cacheKey = "LocalizationImage:" + defaultCulture + ':' + currentCulture;
                if (HttpRuntime.Cache[cacheKey] == null)
                {
                    Hashtable resource = new Hashtable();
                    LoadImage(resource, defaultCulture, cacheKey);
                    if (defaultCulture != currentCulture)
                    {
                        try
                        {
                            LoadImage(resource, currentCulture, cacheKey);
                        }
                        catch (FileNotFoundException)
                        { }
                    }
                }
                return (Hashtable)HttpRuntime.Cache[cacheKey];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadImage(Hashtable resource, string culture, string cacheKey)
        {
            try
            {
                string file = string.Format("{0}\\{1}\\Images.xml", fileName, culture);
                XmlDocument xml = new XmlDocument();
                xml.Load(file);
                foreach (XmlNode n in xml.SelectSingleNode("Images"))
                {
                    if (n.NodeType != XmlNodeType.Comment)
                    {
                        LocalizedImageData data = new LocalizedImageData();
                        data.Alt = n.InnerText;
                        data.Height = Convert.ToInt32(n.Attributes["height"].Value);
                        data.Width = Convert.ToInt32(n.Attributes["width"].Value);
                        resource[n.Attributes["name"].Value] = data;
                    }
                }
                HttpRuntime.Cache.Insert(cacheKey, resource, new CacheDependency(file), DateTime.MaxValue, TimeSpan.Zero);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}