using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{    
    public class Category_Locale
    {
        public DataTable Get_Category_Locale_leftmenu(int LanguageID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;
            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Get_Category_Locale", Params);
        }
    }
}
