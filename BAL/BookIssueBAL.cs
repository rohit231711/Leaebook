using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using System.Data;
using System.Data.SqlClient;
using DAL;
namespace BAL
{
    public class BookIssueBAL : BookIssuePAL
    {

        public DataTable GetImageMagazinIssueList()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_getAllIssueImages", Params);
        }
        public DataTable TodayPublishBookList()
        {

            SqlParameter[] Params = new SqlParameter[0];
            
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "TodayPublishBookList", Params);
        }
        public DataTable GetSmallImageMagazinIssue()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[sp_SmallIssueImages]", Params);
        }


        public void deleteImageMagazinIssueList()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_DeleteAllIssueImages", Params);
        }
    }
}
