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
    public class BookDeliveryAddressBAL : BookDeliveryAddressPAL
    {

        public int InsertBookAddress()
        {
            SqlParameter[] Params = new SqlParameter[12];
            Params[0] = new SqlParameter("@BookDeliveryAddID", BookDeliveryAddID);
            //Params[0].Direction = ParameterDirection.Output;
            Params[1] = new SqlParameter("@UserID", UserID);
            Params[2] = new SqlParameter("@IsDefault", IsDefault);
            Params[3] = new SqlParameter("@Name", Name);
            Params[4] = new SqlParameter("@StreetAddress", StreetAddress);
            Params[5] = new SqlParameter("@Landmark", Landmark);
            Params[6] = new SqlParameter("@City", City);
            Params[7] = new SqlParameter("@State", State);
            Params[8] = new SqlParameter("@Country", Country);
            Params[9] = new SqlParameter("@Pincode", Pincode);
            Params[10] = new SqlParameter("@PhoneNumber", PhoneNumber);
            Params[11] = new SqlParameter("@Result", SqlDbType.BigInt);
            Params[11].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookDeliveryAddressDetail_Insert", Params);
            return Convert.ToInt32(Params[11].Value);

        }

        public DataTable GetDataByPK()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@BookDeliveryAddID", BookDeliveryAddID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "BookDeliveryAddressDetail_DatabyPK", Params);
        }

        public DataTable GetUserCountry()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@UserID", UserID);
            Params[1] = new SqlParameter("@Country", Country);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "BookDeliveryAddressDetail_ListByUserCountry", Params);
        }

        public DataTable GetBookAddressByUser()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@UserID", UserID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "BookDeliveryAddressDetail_ListByUser", Params);

            return dt;
        }

       

        //created on 19th oct 2018
        public DataTable GetBookAddressByUser_Default()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@UserID", UserID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "BookDeliveryAddress", Params);

            return dt;
        }

        public void SetDefaultAddress(string CustomerID, string AddressID)
        {
            SqlParameter[] Params = new SqlParameter[2];

            Params[0] = new SqlParameter("@CustomerID", CustomerID);
            Params[1] = new SqlParameter("@AddressID", AddressID);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_SetDefaultAddress", Params);
        }

        public void DeleteUserAddressDetail()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@BookDeliveryAddID", BookDeliveryAddID);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookDeliveryAddressDetail_DeleteByID", Params);
        }


    }
}
