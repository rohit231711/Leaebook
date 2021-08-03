using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class ShippingSettingsBAL : ShippingSettingsPAL
    {
        public DataTable GetAllShippingseetings()
        {

            SqlParameter[] Params = new SqlParameter[1];

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAllShippingsettings", Params);

        }

        public void UpdateShippingsettings()
        {
            SqlParameter[] Params = new SqlParameter[36];

            Params[0] = new SqlParameter("@ID", ID);
            Params[1] = new SqlParameter("@SiteID",SiteID);
            Params[2] = new SqlParameter("@Password",Password);
            Params[3] = new SqlParameter("@ShipperAccountNumber",ShipperAccountNumber);
            Params[4] = new SqlParameter("@BillingAccountNumber",BillingAccountNumber);
            Params[5] = new SqlParameter("@DutyAccountNumber",DutyAccountNumber);
            Params[6] = new SqlParameter("@CompanyName",CompanyName);
            Params[7] = new SqlParameter("@AddressLine1",AddressLine1);
            Params[8] = new SqlParameter("@AddressLine2",AddressLine2);
            Params[9] = new SqlParameter("@AddressLine3",AddressLine3);
            Params[10] = new SqlParameter("@City",City);
            Params[11] = new SqlParameter("@PostalCode",PostalCode);
            Params[12] = new SqlParameter("@CountryCode",CountryCode);
            Params[13] = new SqlParameter("@CountryName",CountryName);
            Params[14] = new SqlParameter("@PersonName",PersonName);
            Params[15] = new SqlParameter("@PhoneNumber",PhoneNumber);
            Params[16] = new SqlParameter("@PhoneExtension",PhoneExtension);
            Params[17] = new SqlParameter("@FaxNumber",FaxNumber);
            Params[18] = new SqlParameter("@Telex",Telex);
            Params[19] = new SqlParameter("@Email",Email);
            Params[20] = new SqlParameter("@ShipperID",ShipperID);
            Params[21] = new SqlParameter("@ShipperCompanyName",ShipperCompanyName);
            Params[22] = new SqlParameter("@ShipperRegisteredAccount",ShipperRegisteredAccount);
            Params[23] = new SqlParameter("@ShipperAddressLine1",ShipperAddressLine1);
            Params[24] = new SqlParameter("@ShipperAddressLine2",ShipperAddressLine2);
            Params[25] = new SqlParameter("@ShipperCity",ShipperCity);
            Params[26] = new SqlParameter("@ShipperPostalCode",ShipperPostalCode);
            Params[27] = new SqlParameter("@ShipperCountryCode",ShipperCountryCode);
            Params[28] = new SqlParameter("@ShipperCountryName",ShipperCountryName);
            Params[29] = new SqlParameter("@ShipperPersonName",ShipperPersonName);
            Params[30] = new SqlParameter("@ShipperPhoneNumber",ShipperPhoneNumber);
            Params[31] = new SqlParameter("@ShipperPhoneExtension",ShipperPhoneExtension);
            Params[32] = new SqlParameter("@ShipperFaxNumber",ShipperFaxNumber);
            Params[33] = new SqlParameter("@ShipperTelex", ShipperTelex);
            Params[34] = new SqlParameter("@ShipperEmail", ShipperEmail);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Shippingsettings_Update", Params);
           
        }
    }
}
