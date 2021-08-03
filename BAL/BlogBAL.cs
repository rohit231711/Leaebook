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
    public class BlogBAL : BlogPAL
    {
        public DataTable SelectBlogByID()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BlogID", BlogID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Blog_GetByPK", Params);

            return dt;

        }

        public DataSet SelectBlogAll()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

           

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataSet dt = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "Blog_GetAll", Params);

            return dt;

        }

        public int InsertUpdateBlog()
        {
            SqlParameter[] Params = new SqlParameter[8];

            Params[0] = new SqlParameter("@ID", BlogID);
            Params[0].Direction = ParameterDirection.ReturnValue;

            Params[1] = new SqlParameter("@LanguageID", LanguageID);

            Params[2] = new SqlParameter("@Title", Title);

            Params[3] = new SqlParameter("@IsActive", ISActive);


            Params[4] = new SqlParameter("@BlogImage", BlogImage);

            Params[5] = new SqlParameter("@Description", Description);

            Params[6] = new SqlParameter("@BlogID", BlogID);

            Params[7] = new SqlParameter("@CreatedDate", CreatedDate);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Blog_InsertUpdate", Params);
            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[0].Value);
            return result;
        }

        public int DeleteBlog()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BlogID", BlogID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Blog_DeleteByPK", Params);

            return result;
        }

        public DataTable SelectAllBlogPaging(int PageIndex, int pageSize)
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

            Params[2] = new SqlParameter("@BlogID", BlogID);
            index++;

            Params[3] = new SqlParameter("@TitleEng", TitleEnglish);
            index++;

            Params[4] = new SqlParameter("@TitleSpa", TitleSpanish);
            index++;

            Params[5] = new SqlParameter("@SortColumn", SortColumn);
            index++;

            Params[6] = new SqlParameter("@SortStatus", SortStatus);
            index++;



            //DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllCategoryPaging", Params);
            //totalCount = Convert.ToInt32(!String.IsNullOrEmpty(Params[2].Value.ToString()) ? Convert.ToInt32(Params[2].Value.ToString()) : 0);
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAllBlogPaging", Params);
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];

        }
    }
}
