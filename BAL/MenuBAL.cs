using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAL;
using DAL;
namespace BAL
{
    public class MenuBAL:MenuPAL
    {
        public DataTable Getaccesstype()
        {
            SqlParameter[] Params = new SqlParameter[0];
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_getaccesstype", Params);
        }
        public int AddMenuRightsUser(int accessID)
        {
            SqlParameter[] Params = new SqlParameter[4];
            Int16 index = 0;

            Params[index] = new SqlParameter("@UserID",UserID);
            index++;

            Params[index] = new SqlParameter("@MenuID",MenuID);
            index++;

            Params[index] = new SqlParameter("@AccessType",accessID);
            index++;

            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "AddMenuRightsUser", Params);

        }
        public DataTable GetRightsByUser()
        {
            SqlParameter[] Params = new SqlParameter[1];
            if (UserID != -1)
                Params[0] = new SqlParameter("@UserID", UserID);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetRightsByUser", Params);
        }
        public int DeleteMenuAccessRights()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@UserID", UserID);
            index++;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_remove_accessright", Params);
            return 0;
        }
        public DataTable GetAllMenu()
        {
            SqlParameter[] Params = new SqlParameter[0];
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAllMenu", Params);
        }
    }
}
