using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BAL
{
    public class WebsiteSettingsBAL : WebsiteSettingsPAL
    {
        public DataTable GetAllWebseetings()
        {

            SqlParameter[] Params = new SqlParameter[1];

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAllWebsettings", Params);

        }

        public void UpdateWebsettings()
        {
            SqlParameter[] Params = new SqlParameter[9];

            Params[0] = new SqlParameter("@FaceBookLink", FaceBookLink);

            Params[1] = new SqlParameter("@TwiterLink", TwiterLink);

            Params[2] = new SqlParameter("@Pinterest_Link", Pinterest_Link);

            Params[3] = new SqlParameter("@GoogleLink", GoogleLink);

            Params[4] = new SqlParameter("@Instagram_Link", Instagram_Link);

            Params[5] = new SqlParameter("@webSettingID", WebSettingID);

            Params[6] = new SqlParameter("@ContactUs", ContactUs);

            Params[7] = new SqlParameter("@BookStorePhone", BookStorePhone);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Websettings_Update", Params);

        }
    }



  



}
