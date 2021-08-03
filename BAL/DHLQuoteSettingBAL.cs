using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using System.Data.SqlClient;
using System.Data;
using DAL;

namespace BAL
{
    public class DHLQuoteSettingBAL : DHLQuoteSettingPAL
    {
        public DataTable GetAllDHLQuoteSetting()
        {

            SqlParameter[] Params = new SqlParameter[1];

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAllDHLQuoteSetting", Params);

        }

        public void UpdateDHLQuoteSetting()
        {
            SqlParameter[] Params = new SqlParameter[20];

            Params[0] = new SqlParameter("@ID", ID);
            Params[1] = new SqlParameter("@SiteID", SiteID);
            Params[2] = new SqlParameter("@Password", Password);
            Params[3] = new SqlParameter("@FromCountryCode", FromCountryCode);
            Params[4] = new SqlParameter("@FromCodeCity", FromCodeCity);
            Params[5] = new SqlParameter("@FromPostalcode", FromPostalcode);
            Params[6] = new SqlParameter("@FromCity", FromCity);
            Params[7] = new SqlParameter("@PaymentCountryCode", PaymentCountryCode);
            Params[8] = new SqlParameter("@ReadyTime", ReadyTime);
            Params[9] = new SqlParameter("@ReadyTimeGMTOffset", ReadyTimeGMTOffset);
            Params[10] = new SqlParameter("@DimensionUnit", DimensionUnit);
            Params[11] = new SqlParameter("@WeightUnit", WeightUnit);
            Params[12] = new SqlParameter("@PaymentAccountNumber", PaymentAccountNumber);
            Params[13] = new SqlParameter("@IsDutiable", IsDutiable);
            Params[14] = new SqlParameter("@NetworkTypeCode", NetworkTypeCode);
            Params[15] = new SqlParameter("@GlobalProductCode", GlobalProductCode);
            Params[16] = new SqlParameter("@LocalProductCode", LocalProductCode);
            Params[17] = new SqlParameter("@SpecialServiceType", SpecialServiceType);
            Params[18] = new SqlParameter("@DeclaredCurrency", DeclaredCurrency);
            Params[19] = new SqlParameter("@DeclaredValue", DeclaredValue);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DHLQuoteSetting_Update", Params);

        }
    }
}
