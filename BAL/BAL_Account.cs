using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using System.Data.SqlClient;
using DAL;
using System.Data;

namespace BAL
{    
    public class BAL_Account : AccountPAL
    {
        public DataTable Check_Login()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@Email", UserName);
            index++;

            Params[index] = new SqlParameter("@Password", Password);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Login_Chk_website", Params);
        }

        public int InsertRegistration()
        {
            SqlParameter[] Params = new SqlParameter[23];
            Int16 index = 0;

            Params[index] = new SqlParameter("@FirstName", FirstName);
            index++;

            Params[index] = new SqlParameter("@LastName", LastName);
            index++;

            Params[index] = new SqlParameter("@EmailAddress", Email);
            index++;

            Params[index] = new SqlParameter("@Password", Password);
            index++;

            Params[index] = new SqlParameter("@CreatedDate", CreatedDate);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);
            index++;

            Params[index] = new SqlParameter("@IsNewsLetter", IsNewsLetter);
            index++;

            Params[index] = new SqlParameter("@UserType", UserType);
            index++;

            Params[index] = new SqlParameter("@LoginDate", LoginDate);
            index++;

            Params[index] = new SqlParameter("@LastLoginDate", LastLoginDate);
            index++;

            Params[index] = new SqlParameter("@NewIssues", NewIssues);
            index++;

            Params[index] = new SqlParameter("@Renewals", Renewals);
            index++;

            Params[index] = new SqlParameter("@AppUpdates", AppUpdates);
            index++;

            Params[index] = new SqlParameter("@FacebookEmail", FacebookEmail);
            index++;

            Params[index] = new SqlParameter("@ActivationID", ActivationID);
            index++;

            Params[index] = new SqlParameter("@UserName", UserName);
            index++;

            Params[index] = new SqlParameter("@GenderID", GenderID);
            index++;

            Params[index] = new SqlParameter("@BirthdayDate", BirthdayDate);
            index++;

            Params[index] = new SqlParameter("@Countryid", Countryid);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            Params[index] = new SqlParameter("@RegistrationID", SqlDbType.Int);
            Params[index].Direction = ParameterDirection.ReturnValue;
            

            object result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Insert_Registration_website", Params);

            if (!Convert.IsDBNull(new SqlParameter("@RegistrationID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[index].Value);

            return Convert.ToInt32(result);
        }

        public DataTable Check_User_Duplication()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@UserName", UserName);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "get_UserCount_byName_website", Params);
        }

        public DataTable Check_Email_Duplication()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@RegistrationID", RegistrationID);
            index++;

            Params[index] = new SqlParameter("@Email", Email);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "get_Emailcount_website", Params);
        }

        public void update_user_profile()
        {
            SqlParameter[] Params = new SqlParameter[8];
            Int16 index = 0;

            Params[index] = new SqlParameter("@RegistrationID", RegistrationID);
            index++;

            Params[index] = new SqlParameter("@FirstName", FirstName);
            index++;

            Params[index] = new SqlParameter("@LastName", LastName);
            index++;

            Params[index] = new SqlParameter("@Email", Email);
            index++;

            Params[index] = new SqlParameter("@GenderID", GenderID);
            index++;

            Params[index] = new SqlParameter("@BirthdayDate", BirthdayDate);
            index++;

            Params[index] = new SqlParameter("@Countryid", Countryid);
            index++;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "update_User_Profile_website", Params);
        }

        public DataTable Get_Registration_Detail_ByID()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@RegistrationID", RegistrationID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_get_Registration_byId_website", Params);
        }

        public void Change_User_Password()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@RegistrationID", RegistrationID);
            index++;

            Params[index] = new SqlParameter("@Password", Password);
            index++;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Change_Password_website", Params);
        }

        public int LastLoginUpdate()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@userid", RegistrationID);
            index++;

            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_Update_Lastlogin", Params);
        }

    }
}
