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
    public class ShippingBAL : ShippingPAL
    {
        public DataTable SelectShippingCharge()
        {
            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ShippingCharge_GetAll");

            return dt;
        }

        public DataTable getByPK()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ShipperID", ShipperID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ShippingCharge_GetByPK", Params);
            return dt;
        }

        public void deleteByPk()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ShipperID", ShipperID);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ShippingCharge_DeleteByPK", Params);
        }

        //ShippingCharge_DeleteByPK
        public int InsertShippingCharge()
        {
            SqlParameter[] Params = new SqlParameter[4];

            Params[0] = new SqlParameter("@ShipperID", ShipperID);

            Params[1] = new SqlParameter("@ShipperName", ShipperName);

            Params[2] = new SqlParameter("@ShippingCharge", ShippingCharge);

            Params[3] = new SqlParameter("@Result", Result);
            Params[3].Direction = ParameterDirection.ReturnValue;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "ShippingCharge_Insert", Params);

            return result;
        }
    }
}
