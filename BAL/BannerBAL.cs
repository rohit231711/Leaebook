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
    public class BannerBAL : BannerPAL
    {
        public DataTable BannerList()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@ID", ID);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_getBanner", Params);

        }
        public int ManageBanner()
        {

            SqlParameter[] Params = new SqlParameter[6];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            Params[index] = new SqlParameter("@Title", Title);
            index++;

            Params[index] = new SqlParameter("@Image", Path);
            index++;

            Params[index] = new SqlParameter("@Desc", Description);
            index++;

            Params[index] = new SqlParameter("@PID", SqlDbType.BigInt);
            Params[index].Direction = ParameterDirection.Output;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_manageBanner", Params);
            return Convert.ToInt32(Params[4].Value);

        }
        public void DeleteBanner()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_deletebanner", Params);
        }
    }
}
