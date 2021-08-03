using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using System.Data.SqlClient;
using System.Data;
using DAL;
using System.Net;
using System.IO;

namespace BAL
{
    public class RegistrationBAL : RegistrationPAL
    {
        #region Public Methods

        

        public int InsertRegistration()
        {


            SqlParameter[] Params = new SqlParameter[18];

            Params[0] = new SqlParameter("@FirstName", FirstName);

            Params[1] = new SqlParameter("@LastName", LastName);

            Params[2] = new SqlParameter("@EmailAddress", EmailAddress);

            Params[3] = new SqlParameter("@Password", Password);

            Params[4] = new SqlParameter("@IsActive", IsActive);

            Params[5] = new SqlParameter("@IsNewsLetter", IsNewsLetter);

            Params[6] = new SqlParameter("@UserType", UserType);

            Params[7] = new SqlParameter("@NewIssues", NewIssues);

            Params[8] = new SqlParameter("@Renewals", Renewals);

            Params[9] = new SqlParameter("@AppUpdates", AppUpdates);

     

            Params[10] = new SqlParameter("@ID", RegistrationID);
            Params[10].Direction = ParameterDirection.Output;

            Params[11] = new SqlParameter("@ActivationID", ActivationID);
            Params[12] = new SqlParameter("@FacebookEmail", FacebookEmail);

            Params[13] = new SqlParameter("@UserName", UserName);

            Params[14] = new SqlParameter("@GenderID", GenderID);
            Params[15] = new SqlParameter("@Birthdaydate", BirthdayDate);
            Params[16] = new SqlParameter("@Countryid", Countryid);
            Params[17] = new SqlParameter("@LanguageID", LanguageID);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Registration_Insert", Params);
            int result = Convert.ToInt32(Params[10].Value == DBNull.Value ? "0" : Params[10].Value);
            return result;

        }



        public int UpdateRegistration()
        {

            SqlParameter[] Params = new SqlParameter[15];

            Params[0] = new SqlParameter("@FirstName", FirstName);

            Params[1] = new SqlParameter("@LastName", LastName);

            Params[2] = new SqlParameter("@EmailAddress", EmailAddress);

            Params[3] = new SqlParameter("@Password", Password);

            Params[4] = new SqlParameter("@IsActive", IsActive);

            Params[5] = new SqlParameter("@IsNewsLetter", IsNewsLetter);

            Params[6] = new SqlParameter("@RegistrationID", RegistrationID);

            Params[7] = new SqlParameter("@UserType", UserType);

            Params[8] = new SqlParameter("@UserName", UserName);

            Params[9] = new SqlParameter("@Countryid", Countryid);

            Params[10] = new SqlParameter("@GenderId", GenderID);

            Params[11] = new SqlParameter("@Birthdate", BirthdayDate);

            Params[12] = new SqlParameter("@LanguageId", LanguageID);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Registration_UpdateByPK", Params);

            return result;

        }

        public int DeleteRegistration()
        {

            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Registration_DeleteByPK", Params);

            return result;
        }

        public DataTable viewCartDetails()
        {

            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "UserViewCart", Params);

            return dt;
        }

        public DataTable SelectRegistraionByID()
        {
            SqlParameter[] Params = new SqlParameter[1];


            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Registration_GetByPK", Params);

            return dt;

        }

        public DataTable ActiveAccount()
        {
            SqlParameter[] Params = new SqlParameter[2];


            Params[0] = new SqlParameter("@NewActivationID", Guid.NewGuid().ToString());

            Params[1] = new SqlParameter("@ActivationID", ActivationID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ActiveAccount_GetByActiveKey", Params);

            return dt;

        }

        public DataSet GetSubscriberAndPurchaseUsers(int pageIndex, int pageSize, int Type)
        {

            SqlParameter[] Params = new SqlParameter[5];


            Params[0] = new SqlParameter("@Type", Type);
            Params[1] = new SqlParameter("@ToDate", ToDate);
            Params[2] = new SqlParameter("@FromDate", FromDate);
            Params[3] = new SqlParameter("@Publisher", Publisher);
            Params[4] = new SqlParameter("@Title", BookTitle);
            Params[4] = new SqlParameter("@SortColumn", SortColumn);
            Params[4] = new SqlParameter("@SortStatus", SortStatus);

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "GetSubscriberAndPurchaseUsers", Params);

            return ds;

        }

        public DataSet GetSubscriberAndPurchaseUsersPartner(string PartnerID, int pageIndex, int pageSize, int Type)
        {

            SqlParameter[] Params = new SqlParameter[6];

            Params[0] = new SqlParameter("@PartnerID", PartnerID);
            Params[1] = new SqlParameter("@Type", Type);
            Params[2] = new SqlParameter("@ToDate", ToDate);
            Params[3] = new SqlParameter("@FromDate", FromDate);
            Params[4] = new SqlParameter("@Publisher", Publisher);
            Params[5] = new SqlParameter("@Title", BookTitle);
            Params[5] = new SqlParameter("@SortColumn", SortColumn);
            Params[5] = new SqlParameter("@SortStatus", SortStatus);

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "GetSubscriberAndPurchaseUsersForPartner", Params);

            return ds;

        }
        
        public DataTable LoginValidation()
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@username", UserName);
            index++;

            Params[index] = new SqlParameter("@password", Password);
            index++;

            //Params[index] = new SqlParameter("@userid", RegistrationID);
            //index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[Sp_Admin_login]", Params);
        }

        public int LastLoginUpdate()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@userid", RegistrationID);
            index++;

            return SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Sp_Update_Lastlogin", Params);
        }

        public DataTable SelectAllRegistration()
        {
            DataTable dt = null;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Registration_GetAll");
            return dt;
        }

        public DataTable SelectAllRegistrationPaging(int pageIndex, int pageSize)
        {
            SqlParameter[] Params = new SqlParameter[10];

            Params[0] = new SqlParameter("@PageIndex", pageIndex);

            Params[1] = new SqlParameter("@PageSize", pageSize);

            //Params[2] = new SqlParameter("@RecordCount", SqlDbType.Int);
            //Params[2].Direction = ParameterDirection.Output;

            //Params[3] = new SqlParameter("@OrderBy", OrderBy);

            //Params[4] = new SqlParameter("@Sorting", Sorting);
            Params[3] = new SqlParameter("@UserType", UserType);

            if (!string.IsNullOrEmpty(UserName))
                Params[4] = new SqlParameter("@Name", UserName);
            if (!string.IsNullOrEmpty(EmailAddress))
                Params[5] = new SqlParameter("@Email", EmailAddress);

            Params[6] = new SqlParameter("@SortColumn", SortColumn);
            Params[7] = new SqlParameter("@SortStatus", SortStatus);
            Params[8] = new SqlParameter("@ToDate", ToDate);
            Params[9] = new SqlParameter("@FromDate", FromDate);
            //DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllRegistrationPaging", Params);
            //totalCount = Convert.ToInt32(!String.IsNullOrEmpty(Params[2].Value.ToString()) ? Convert.ToInt32(Params[2].Value.ToString()) : 0);
            //return dt;

            


            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAllRegistrationPaging", Params);
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];
        }

        public DataTable SelectAllRegistrationPartner(int pageIndex, int pageSize)
        {
            SqlParameter[] Params = new SqlParameter[10];

            Params[0] = new SqlParameter("@PageIndex", pageIndex);

            Params[1] = new SqlParameter("@PageSize", pageSize);

            Params[3] = new SqlParameter("@UserType", UserType);

            if (!string.IsNullOrEmpty(UserName))
                Params[4] = new SqlParameter("@Name", UserName);
            if (!string.IsNullOrEmpty(EmailAddress))
                Params[5] = new SqlParameter("@Email", EmailAddress);

            Params[6] = new SqlParameter("@SortColumn", SortColumn);
            Params[7] = new SqlParameter("@SortStatus", SortStatus);
            Params[8] = new SqlParameter("@ToDate", ToDate);
            Params[9] = new SqlParameter("@FromDate", FromDate);

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAllRegistrationPartner", Params);
            totalCount = Convert.ToInt32(ds.Tables[1].Rows[0][0]);

            return ds.Tables[0];
        }

        public DataTable AdminLogin()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@EmailAddress", EmailAddress);
            index++;

            Params[index] = new SqlParameter("@Password", Password);
            index++;
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_AdminLogin", Params);
        }
        public void UpdateLogin()
        {
            SqlParameter[] Params = new SqlParameter[1];


            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UpdateLogin", Params);



        }
        #endregion

        #region  //web Method region

        public DataTable GetSubscriberUserEmailsByBook()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;
            Params[index] = new SqlParameter("@BookID", BookID);


            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetSubscriberUserEmailsByBook", Params);

        }
        public DataTable Login()
        {
            SqlParameter[] Params = new SqlParameter[9];
            Int16 index = 0;


            Params[index] = new SqlParameter("@FacebookEmail", FacebookEmail);
            index++;

            Params[index] = new SqlParameter("@EmailAddress", EmailAddress);
            index++;

            Params[index] = new SqlParameter("@UserName", UserName);
            index++;

            Params[index] = new SqlParameter("@Password", Password);
            index++;

            Params[index] = new SqlParameter("@Deviceid", Deviceid);
            index++;

            Params[index] = new SqlParameter("@DeviceType", DeviceType);
            index++;


            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_Login", Params);
        }

        public DataTable ForgotPassword()
        {
            SqlParameter[] Params = new SqlParameter[2];

            Params[0] = new SqlParameter("@EmailAddress", EmailAddress);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_ForgotPassword", Params);
        }

        public void ChangePassword()
        {
            SqlParameter[] Params = new SqlParameter[6];

            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            Params[1] = new SqlParameter("@Password", Password);


            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_ChangePassword", Params);



        }

        public DataTable GetOneByEmail()
        {
            int index = 0;

            SqlParameter[] Params = new SqlParameter[1];

            Params[index] = new SqlParameter("@Email", EmailAddress);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetOneByEmail", Params);
        }

        public DataTable GetByUserName()
        {
            int index = 0;

            SqlParameter[] Params = new SqlParameter[1];

            Params[index] = new SqlParameter("@UserName", UserName);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Registration_GetByUserName", Params);
        }

        public int UpdateNotification()
        {

            SqlParameter[] Params = new SqlParameter[4];

            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            Params[1] = new SqlParameter("@NewIssues", NewIssues);

            Params[2] = new SqlParameter("@Renewals", Renewals);

            Params[3] = new SqlParameter("@AppUpdates", AppUpdates);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_UpdateNotification", Params);

            return result;

        }

        public DataTable GetNotificationSetting()
        {
            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_GetnotificationSetting", Params);
        }

        public DataTable GetIphoneIDForIphone(int newissue, int newupdate, int depart)
        {
            SqlParameter[] Params = new SqlParameter[3];


            Params[0] = new SqlParameter("@newIssue", newissue);
            Params[1] = new SqlParameter("@newUpdate", newupdate);

            Params[2] = new SqlParameter("@depart", depart);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[proc_GetUsersNotification]", Params);
        }

        public DataTable GetUserDetails ()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", RegistrationID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "pro_GetUserDetails", Params);
        }

        public DataTable FacebookLogin()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@FacebookEmail", FacebookEmail);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_Registration_Insert", Params);
        }

        public string SendNotification(string deviceId, string message)
        {
            string GoogleAppID = Config.PushApiKey;
            // string GoogleAppID = "AIzaSyCw8Ohn3EMbg0uTnv_L_IbIw76Vfg491SI";
            var SENDER_ID = Config.PushSenderID;
            var value = message;
            var deviceid = deviceId;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", GoogleAppID));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            string postData = "collapse_key=score_update&time_to_live=108&" +
            "delay_while_idle=1&data.message=" + value + "&data.time=" +
            System.DateTime.Now.ToString() + "&registration_id=" + deviceId + "";
            Console.WriteLine(postData);
            Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            tRequest.ContentLength = byteArray.Length;

            Stream dataStream = tRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse tResponse = tRequest.GetResponse();

            dataStream = tResponse.GetResponseStream();

            StreamReader tReader = new StreamReader(dataStream);

            String sResponseFromServer = tReader.ReadToEnd();

            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;

        }
        public DataTable GetChart1(int month)
        {

            int index = 0;
            SqlParameter[] Params = new SqlParameter[1];
            Params[index] = new SqlParameter("@Month", month);
            index++;
        
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetUsersByMonth", Params);

        }
        #endregion
    }
}
