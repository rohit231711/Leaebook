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
    public class AdvertisementsBAL : AdvertisementsPAL
    {
        public DataTable SelectAdvertisementByID()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@AdvertisementID", AdvertisementID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Advertisements_GetByPK", Params);

            return dt;

        }

        public int InsertAdvertisement()
        {
            SqlParameter[] Params = new SqlParameter[8];

            Params[0] = new SqlParameter("@AdvertisementImage", AdvertisementImage);
            //Params[0].Direction = ParameterDirection.ReturnValue;

            Params[1] = new SqlParameter("@LanguageID", LanguageID);

            Params[2] = new SqlParameter("@Title", Title);

            Params[3] = new SqlParameter("@IsActive", ISActive);


            Params[5] = new SqlParameter("@Description", Description);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Advertisements_Insert", Params);
            
            return result;
        }

        public int DeleteAdvertisement()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@AdvertisementID", AdvertisementID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Advertisements_DeleteByPK", Params);

            return result;
        }

        public int ActiveAdvertisement()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@AdvertisementID", AdvertisementID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Advertisements_ActiveByPK", Params);

            return result;
        }

        public int InsertUpdateAdvertisement()
        {
            SqlParameter[] Params = new SqlParameter[9];

            Params[0] = new SqlParameter("@ID", AdvertisementID);
            Params[0].Direction = ParameterDirection.ReturnValue;

            Params[1] = new SqlParameter("@LanguageID", LanguageID);

            Params[2] = new SqlParameter("@Title", Title);

            Params[3] = new SqlParameter("@IsActive", ISActive);

            Params[4] = new SqlParameter("@LinkURL", LinkURL);
            
            Params[5] = new SqlParameter("@AdvertisementImage", AdvertisementImage);

            Params[6] = new SqlParameter("@Description", Description);

            Params[7] = new SqlParameter("@AdvertisementID", AdvertisementID);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Advertisements_InsertUpdate", Params);
            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[0].Value);
            return result;
        }

        public DataTable SelectAllAdvertisementPaging(int PageIndex, int pageSize)
        {
            SqlParameter[] Params = new SqlParameter[8];
            Int16 index = 0;

            Params[0] = new SqlParameter("@PageIndex", PageIndex);
            index++;

            Params[1] = new SqlParameter("@PageSize", pageSize);
            index++;

            //Params[2] = new SqlParameter("@RecordCount", SqlDbType.Int);
            //Params[2].Direction = ParameterDirection.Output;

            //Params[3] = new SqlParameter("@OrderBy", OrderBy);
            //index++;

            Params[2] = new SqlParameter("@AdvertisementID", AdvertisementID);
            index++;

            Params[3] = new SqlParameter("@Title", TitleEnglish);
            index++;

            Params[4] = new SqlParameter("@SortColumn", SortColumn);
            index++;

            Params[5] = new SqlParameter("@SortStatus", SortStatus);
            index++;

            Params[6] = new SqlParameter("@Title1", TitleSpanish);
            index++;



            //DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllCategoryPaging", Params);
            //totalCount = Convert.ToInt32(!String.IsNullOrEmpty(Params[2].Value.ToString()) ? Convert.ToInt32(Params[2].Value.ToString()) : 0);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAllAdvertisementPaging", Params);
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];

        }

        public DataTable SelectAdvertisementWebSite()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;            

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Get_Advertisements_website", Params);            
        }
    }
}
