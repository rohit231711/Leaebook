using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
    public class BookShippingBAL : BookShippingPAL
    {
        public DataTable getAllCountryByBook()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@BookID", BookID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "BookShippingDetail_GetByBook", Params);
            return dt;
        }

        public DataTable getChargebyBookAndCountry()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@BookID", BookID);
            Params[1] = new SqlParameter("@CountryID", CountryID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "BookShippingDetail_GetByBookAndCountry", Params);
            return dt;
        }

        public void deleteAllCountryByBook()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@BookID", BookID);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookShippingDetail_GetByBook", Params);
        }

        public int InsertBookShippingDetail()
        {
            SqlParameter[] Params = new SqlParameter[5];

            Params[0] = new SqlParameter("@BookShippingID", BookShippingID);
            Params[1] = new SqlParameter("@BookID", BookID);
            Params[2] = new SqlParameter("@CountryID", CountryID);
            Params[3] = new SqlParameter("@ShippingCharge", ShippingCharge);

            Params[4] = new SqlParameter("@Result", Result);
            Params[4].Direction = ParameterDirection.ReturnValue;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookShippingDetail_Insert", Params);

            return result;
        }
    }
}
