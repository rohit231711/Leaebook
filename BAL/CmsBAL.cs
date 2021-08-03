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
    public class CmsBAL : CmsPAL
    {
        #region Public Methods
        public int InsertUpdatecrm()
        {
        
            SqlParameter[] Params = new SqlParameter[10];

            Params[0] = new SqlParameter("@ID", ID);

            Params[1] = new SqlParameter("@Title", Title);

            Params[2] = new SqlParameter("@Description", Description);

            Params[3] = new SqlParameter("@IsActive", IsActive);

            Params[4] = new SqlParameter("@Metatitle", Metatitle);

            Params[5] = new SqlParameter("@Metadesc ", MetaDesc);

            Params[6] = new SqlParameter("@Metakeyword", MetaKeyword);

            Params[7] = new SqlParameter("@LanguageID", LanguageID);

            Params[8] = new SqlParameter("@ReturnVal", SqlDbType.Int);
            Params[8].Direction = ParameterDirection.InputOutput;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "cms_AddEdit", Params);
            if (!Convert.IsDBNull(new SqlParameter("@ReturnVal", SqlDbType.Int)))
                result = Convert.ToInt32(Params[7].Value); return result;
          
        }

        public DataTable SelectAll(string Order)
        {
            DataTable dt = null;

            SqlParameter[] Params = new SqlParameter[10];

            Params[0] = new SqlParameter("@SortingOrder", Order);

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "cms_GetAll", Params);
            return dt;
        }

        public DataTable SelectAll()
        {
            DataTable dt = null;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "cms_GetAll");
            return dt;
        }

        public DataTable SelectcmsByID()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "cms_GetByPK",Params);
            return dt;
        }
        public DataTable CMSByID(int CMSID)
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", CMSID);
            index++;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "cms_GetByPK", Params);
            return dt;
        }
        #endregion          
    }
}
