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
    public class CategoryBAL : CategoryPAL
    {
        #region Public Methods
        public int InsertUpdateCategory()
        {
            SqlParameter[] Params = new SqlParameter[10];

            Params[0] = new SqlParameter("@ID", CategoryID);
            Params[0].Direction = ParameterDirection.ReturnValue; 

            Params[1] = new SqlParameter("@LanguageID", LanguageID);

            Params[2] = new SqlParameter("@CategoryName", CategoryName);

            Params[3] = new SqlParameter("@IsActive", IsActive);


            Params[5] = new SqlParameter("@CImagePath", CImagePath);

            Params[6] = new SqlParameter("@CategoryID", CategoryID);

            Params[7] = new SqlParameter("@Title", Title);

            Params[8] = new SqlParameter("@Description", Description);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Category_InsertUpdate", Params);
            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[0].Value);
            return result;
        }

        public int InsertUpdateLocalization()
        {
            SqlParameter[] Params = new SqlParameter[8];

            Params[0] = new SqlParameter("@ID", CategoryID);
            Params[0].Direction = ParameterDirection.ReturnValue;

            Params[1] = new SqlParameter("@LocalizationID", LanguageID);

            Params[2] = new SqlParameter("@Value", CategoryName);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "InsertUpdateLocalization", Params);
            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[0].Value);
            return result;
        }





        public int UpdateCategory()
        {
            SqlParameter[] Params = new SqlParameter[4];

            Params[0] = new SqlParameter("@CategoryID", CategoryID);

            Params[1] = new SqlParameter("@CategoryName", CategoryName);

            Params[2] = new SqlParameter("@IsActive", IsActive);

            Params[3] = new SqlParameter("@CImagePath", CImagePath);
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Category_UpdateByPK", Params);

            return result;

        }

        public int DeleteCategory()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CategoryID", CategoryID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Category_DeleteByPK", Params);

            return result;
        }



        public DataTable SelectCategoryByID()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CategoryID", CategoryID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Category_GetByPK", Params);

            return dt;

        }

        public DataTable GetLocalization()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetLocalization", Params);

            return dt;

        }

        public DataTable GetLocalizationKeyValue()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@SortColumn", SortColumn);
            index++;

            Params[index] = new SqlParameter("@SortStatus", SortStatus);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetLocalizationKeyValue", Params);

            return dt;

        }

        public DataTable Check_categoryDuplication()
        {
            DataTable dt = new DataTable();

            SqlParameter[] Params = new SqlParameter[6];
            Int16 index = 0;

            Params[0] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            Params[1] = new SqlParameter("@CategoryName", CategoryName);
            index++;

            Params[2] = new SqlParameter("@CategoryID", CategoryID);
            index++;

            Params[3] = new SqlParameter("@Title", Title==null ? "Title": Title);
            index++;

            Params[4] = new SqlParameter("@Description", Description==null ? "Description" : Description);
            index++;


            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_check_duplicat_category_website", Params);
            return dt;

        }

        public DataTable SelectAllCartegory()
        {
            DataTable dt = null;

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;
            Params[0] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Category_GetAll", Params);
            return dt;

        }

        public DataTable SelectAllCategoryPaging(int PageIndex, int pageSize)
        {
            SqlParameter[] Params = new SqlParameter[7];
            Int16 index = 0;

            Params[0] = new SqlParameter("@PageIndex", PageIndex);
            index++;

            Params[1] = new SqlParameter("@PageSize", pageSize);
            index++;

            //Params[2] = new SqlParameter("@RecordCount", SqlDbType.Int);
            //Params[2].Direction = ParameterDirection.Output;

            //Params[3] = new SqlParameter("@OrderBy", OrderBy);
            //index++;

            Params[2] = new SqlParameter("@CategoryID", CategoryID);
            index++;

            Params[3] = new SqlParameter("@CategoryNameEng", CategoryNameEng);
            index++;

            Params[4] = new SqlParameter("@SortColumn", SortColumn);
            index++;

            Params[5] = new SqlParameter("@SortStatus", SortStatus);
            index++;

            Params[6] = new SqlParameter("@CategoryNameSpa", CategoryNameSpa);
            index++;



            //DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllCategoryPaging", Params);
            //totalCount = Convert.ToInt32(!String.IsNullOrEmpty(Params[2].Value.ToString()) ? Convert.ToInt32(Params[2].Value.ToString()) : 0);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAllCategoryPaging", Params);
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];

        }

        public DataTable wsSelectAllCartegory()
        {
            DataTable dt = null;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_Category_GetAll");
            return dt;
        }

        #endregion
    }
}
