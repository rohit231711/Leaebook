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
    public class LanguageBAL : LanguagePAL
    {

        #region Public Methods
        public DataTable GetLanguage()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetLanguage", Params);

            return dt;

        }

        public DataTable GetallLabels()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", ID);
            index++;


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Labels_GetAll", Params);

            return dt;

        }

        #endregion
    }
}
