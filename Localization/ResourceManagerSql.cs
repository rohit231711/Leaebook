using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Caching;

namespace Localization
{
    /// <summary>
    /// The ResourceManagerSql is an implementation of the ResourceManager driven by SQL server
    /// </summary>
    public class ResourceManagerSql : ResourceManager
    {
        #region Fields and Properties
        private string connectionString;
        private int cacheDuration;
        #endregion

        #region Constructors
        public ResourceManagerSql(NameValueCollection parameters)
        {
            try
            {
                if (parameters == null || parameters["connectionString"] == null)
                {
                    throw new ApplicationException("ResourceManagerSql requires connectionString attribute in configuraiton.");
                }
                connectionString = parameters["connectionString"];

                //load the optional cacheDuration parameter, else we'll cache for 30 minutes
                if (parameters["cacheDuration"] != null)
                {
                    cacheDuration = Convert.ToInt32(parameters["cacheDuration"]);
                }
                else
                {
                    cacheDuration = 30;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Provider API

        protected override string RetrieveString(string key, string cultureName = "")
        {
            string str = "";
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
                str = messages[key];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return str;
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
                if (!string.IsNullOrEmpty(cultureName))
                {
                    currentCulture = cultureName;
                }
                string defaultCulture = LocalizationConfiguration.GetConfig().DefaultCultureName;
                string cacheKey = "SQLLocalization:" + defaultCulture + ':' + currentCulture;
                NameValueCollection resources = (NameValueCollection)HttpRuntime.Cache[cacheKey];
                //if (resources == null)
                //{
                resources = LoadResources(defaultCulture, currentCulture);
                HttpRuntime.Cache.Insert(cacheKey, resources, null, DateTime.Now.AddMinutes(cacheDuration), Cache.NoSlidingExpiration);
                //}
                return resources;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private NameValueCollection LoadResources(string defaultCulture, string currentCulture)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            NameValueCollection resources = new NameValueCollection();
            try
            {
                connection = new SqlConnection(connectionString);
                command = new SqlCommand("LoadResources", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DefaultCulture", SqlDbType.Char, 5).Value = defaultCulture;
                command.Parameters.Add("@CurrentCulture", SqlDbType.Char, 5).Value = currentCulture;
                connection.Open();

                reader = command.ExecuteReader(CommandBehavior.SingleResult);
                int nameOrdinal = reader.GetOrdinal("Name");
                int valueOrdinal = reader.GetOrdinal("Value");
                while (reader.Read())
                {
                    resources.Add(reader.GetString(nameOrdinal), reader.GetString(valueOrdinal));
                }
            }
            catch
            {

            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            return resources;
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
                string cacheKey = "SQLLocalizationImages:" + defaultCulture + ':' + currentCulture;
                Hashtable resources = (Hashtable)HttpRuntime.Cache[cacheKey];
                if (resources == null)
                {
                    resources = LoadImages(defaultCulture, currentCulture);
                    HttpRuntime.Cache.Insert(cacheKey, resources, null, DateTime.Now.AddMinutes(cacheDuration), Cache.NoSlidingExpiration);
                }
                return resources;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Hashtable LoadImages(string defaultCulture, string currentCulture)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader reader = null;
            Hashtable resources = new Hashtable();
            try
            {
                connection = new SqlConnection(connectionString);
                command = new SqlCommand("LoadImages", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@DefaultCulture", SqlDbType.Char, 5).Value = defaultCulture;
                command.Parameters.Add("@CurrentCulture", SqlDbType.Char, 5).Value = currentCulture;
                connection.Open();
                reader = command.ExecuteReader(CommandBehavior.SingleResult);
                int nameOrdinal = reader.GetOrdinal("Name");
                int heightOrdinal = reader.GetOrdinal("Height");
                int widthOrdinal = reader.GetOrdinal("Width");
                int altOrdinal = reader.GetOrdinal("Alt");
                while (reader.Read())
                {
                    LocalizedImageData data = new LocalizedImageData();
                    data.Height = reader.GetInt32(heightOrdinal);
                    data.Width = reader.GetInt32(widthOrdinal);
                    data.Alt = reader.GetString(altOrdinal);
                    resources.Add(reader.GetString(nameOrdinal), data);
                }
            }
            finally
            {
                if (connection != null)
                {
                    connection.Dispose();
                }
                if (command != null)
                {
                    command.Dispose();
                }
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }
            return resources;
        }

        #endregion
    }
}