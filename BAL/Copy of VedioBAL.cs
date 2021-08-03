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
    public class VedioBAL :VedioPAL
    {

        #region Public Methods
        public int InsertVideo()
        {
            SqlParameter[] Params = new SqlParameter[4];

            Params[0] = new SqlParameter("@VideoName",VideoName);
            Params[1] = new SqlParameter("@VideoPath",VideoPath);
            Params[2] = new SqlParameter("@IsActive", IsActive);
            Params[3] = new SqlParameter("@ID", SqlDbType.Int);
            Params[3].Direction = ParameterDirection.ReturnValue;
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Video_Insert", Params);
            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[3].Value); return result;
            
              
        }

        public int DeleteVideo()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Video_DeleteByPK", Params);

            return result;
        }

        public DataTable SelectAllVideo()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Video_GetByPK", Params);

            return dt;

        }

        public int GetMaxVideoID()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_GetMaxVideoID"));
          
        }

     
        #endregion
    }
}
