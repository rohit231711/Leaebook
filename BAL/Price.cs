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
   public class Price
    {
        #region Public Method Defined 

        public DataTable SelectAllPrice()
        {
            DataTable price = null;
            SqlParameter[] parms = new SqlParameter[0];
            price = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Price_GetAll", parms);
            return price;
        }

        public DataTable SelectAllActivePrice()
        {
            DataTable price = null;
            SqlParameter[] parms = new SqlParameter[0];
            price = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Price_GetAllActive", parms);
            return price;
        }

       public DataTable PriceGetDetailsByID(string CategoryID)
        {
            DataTable price = null;
            SqlParameter[] parms = new SqlParameter[1];
            Int16 index = 0;
            parms[index] = new SqlParameter("@CategoryID", CategoryID);
            price = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Price_GetAllDeatilsById", parms);
            return price;
        }
        #endregion




    }
}
