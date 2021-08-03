using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using System.Data.SqlClient;
using DAL;
using System.Data;

namespace BAL
{
    public class Country
    {
        #region Public Methods
        public DataTable SelectAllCountry()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[0];
            Int16 index = 0;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Country_GetAll", Params);
            return dt;
        }
        public DataTable SelectAllActiveCountry()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[0];
            Int16 index = 0;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Country_GetAllActive", Params);
            return dt;
        }
        public DataTable CountryGetDetailByID(string countryID)
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;
            Params[index] = new SqlParameter("@CountryID", countryID);
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Country_GetAllDetailByID", Params);
            return dt;
        }
        public void Country_Update(string countryID, string ISOCode, string CountryName, bool IsActive)
        {
            SqlParameter[] Params = new SqlParameter[4];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CountryID", countryID);
            index++;

            Params[index] = new SqlParameter("@ISOCode", ISOCode);
            index++;

            Params[index] = new SqlParameter("@CountryName", CountryName);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Country_UpdateAllDetailByID", Params);
            //return dt;
        }
        public void Country_Insert(string ISOCode, string CountryName, bool IsActive)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ISOCode", ISOCode);
            index++;

            Params[index] = new SqlParameter("@CountryName", CountryName);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Country_InsertDetail", Params);
            //return dt;
        }
        public DataTable wsSelectAllCountry()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[0];
            Int16 index = 0;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "wsCountry_GetAll", Params);
            return dt;
        }

        public DataTable SelectAllLanguage()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[0];
            Int16 index = 0;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Language_GetAll", Params);
            return dt;
        }
        #endregion
    }
}
