using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using PAL;
using System.Data;
using DAL;
namespace BAL
{
    public class BookPurchaseBAL : BookPurchase
    {

        public int InsertBookPurchase()
        {


            SqlParameter[] Params = new SqlParameter[29];
            Int16 index = 0;


            if (UserID > 0)
            {
                Params[index] = new SqlParameter("@UserID", UserID);
                index++;
            }


            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@BookBookID", BookBookID);
            index++;
            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            Params[index] = new SqlParameter("@Amount", Amount);
            index++;
            Params[index] = new SqlParameter("@Domain", Domain);
            index++;
            Params[index] = new SqlParameter("@Status", Status);
            index++;
            Params[index] = new SqlParameter("@PurchaseDate", PurchaseDate);
            index++;
            Params[index] = new SqlParameter("@Vcode", Vcode);
            index++;

            Params[index] = new SqlParameter("@TransID", TransID);
            index++;
            Params[index] = new SqlParameter("@Appcode", AppCode);
            index++;
            Params[index] = new SqlParameter("@SessionID", SessionID);
            index++;


            Params[index] = new SqlParameter("@PurchaseID", PurchaseID);
            Params[index].Direction = ParameterDirection.InputOutput;
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_BookPurchase_InsertUpdate", Params);



            return Convert.ToInt32(Params[index - 1].Value);



        }

        public DataTable ReadIssue(int BookID)
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@BookID", BookID);

            //Params[1] = new SqlParameter("@PurchaseID", PurchaseID);
            Params[1] = new SqlParameter("@URL", Config.WebSite);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_ReadIssue", Params);


        }

        public DataTable getPendingBooks(string Search, string StartDate, string EndDate)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@Search", Search);
            index++;

            Params[index] = new SqlParameter("@StartDate", StartDate);
            index++;

            Params[index] = new SqlParameter("@EndDate", EndDate);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_BookPurchase_PendingBooks", Params);
        }

        public DataTable getDeliveredBooks(string Search, string StartDate, string EndDate)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@Search", Search);
            index++;

            Params[index] = new SqlParameter("@StartDate", StartDate);
            index++;

            Params[index] = new SqlParameter("@EndDate", EndDate);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_BookPurchase_DeliveredBooks", Params);
        }

        public DataTable getPendingPartnerBooks()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@PartnerID", PartnerID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_BookPurchase_PendingPartnerBooks", Params);
        }

        public DataTable getDeliveredPartnerBooks()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@PartnerID", PartnerID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_BookPurchase_DeliveredPartnerBooks", Params);
        }

        //sp_BookPurchase_PendingPartnerBooks
        //sp_BookPurchase_DeliveredPartnerBooks

        public void UpdateOrderStatus(string status, string purchaseID)
        {
            string str = "UPDATE tbl_BookPurchase SET OrderStatus = '" + status + "' WHERE PurchaseID = " + purchaseID;
            SqlHelper.ExecuteScalar(CommandType.Text, str);
        }

        public void InsertOrderEntry(string purchase, string status)
        {
            SqlParameter[] Params = new SqlParameter[2];
            Params[0] = new SqlParameter("@PurchaseID", purchase);
            Params[1] = new SqlParameter("@Status", status);

            SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "sp_BookOrderDetail_Insert", Params);
        }

        public DataTable GetUserBookList()
        {
            SqlParameter[] Params = new SqlParameter[4];

            Params[0] = new SqlParameter("@UserId", UserID);
            Params[1] = new SqlParameter("@Title", Title);
            //Params[1] = new SqlParameter("@PurchaseID", PurchaseID);
            Params[2] = new SqlParameter("@URL", Config.WebSite);
            Params[3] = new SqlParameter("@LanguageID", LanguageID);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_GetUserBookList", Params);
        }

        public DataTable GetAllOrderbyUserID()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@UserID", UserID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetAllOrderbyUserID", Params);
        }

        public DataSet GetOrderDetailbyID()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@PurchaseID", PurchaseID);

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "sp_GetOrderDetailbyID", Params);
        }

        public DataTable getBookPurchaseByOrderNo()
        {
            SqlParameter[] Params = new SqlParameter[3];

            Params[0] = new SqlParameter("@OrderNo", OrderID);

            Params[1] = new SqlParameter("@UserID", UserID);

            Params[2] = new SqlParameter("@LanguageID", LanguageID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetBookPurchaseByOrderNo", Params);
        }

        public DataTable getUserLibrary()
        {
            SqlParameter[] Params = new SqlParameter[3];

            Params[0] = new SqlParameter("@UserID", UserID);

            Params[1] = new SqlParameter("@LanguageID", LanguageID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "getUserLibrary", Params);
        }
        public DataTable getUserLibrary_Pagination(int? StartIndex, int? EndIndex)
        {
            SqlParameter[] Params = new SqlParameter[4];
            Params[0] = new SqlParameter("@UserID", UserID);
            Params[1] = new SqlParameter("@LanguageID", LanguageID);
            Params[2] = new SqlParameter("@StartIndex", StartIndex);
            Params[3] = new SqlParameter("@EndIndex", EndIndex);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "UPS_getUserLibrary", Params);
        }

        public int AddToBookPurchase()
        {

            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@CustomerID", UserID);
            index++;

            Params[index] = new SqlParameter("@PurchaseDate", PurchaseDate);
            index++;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            Params[index] = new SqlParameter("@TransactionId", TransID);
            index++;


            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_AddToBookPurchase", Params);

            return Convert.ToInt32(Params[1].Value);

        }

        public int MoveToBookPurchase()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@PurchaseDate", PurchaseDate);
            index++;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            Params[index] = new SqlParameter("@TransactionId", TransID);
            index++;

            Params[index] = new SqlParameter("@DeliveryAddress", "1");

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_MoveToBookPurchase", Params);

            return Convert.ToInt32(Params[1].Value);
        }

        public int MoveToBookPurchase(string address, string shippingcharge, string ShippingType, string PaymentFrom)
        {
            SqlParameter[] Params = new SqlParameter[8];
            Int16 index = 0;

            Params[index] = new SqlParameter("@PurchaseDate", PurchaseDate);
            index++;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            Params[index] = new SqlParameter("@TransactionId", TransID);
            index++;

            Params[index] = new SqlParameter("@DeliveryAddress", address);
            index++;

            Params[index] = new SqlParameter("@ShippingCharge", shippingcharge);
            index++;

            Params[index] = new SqlParameter("@ShippingType", ShippingType);
            index++;

            Params[index] = new SqlParameter("@PaymentFrom", PaymentFrom);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_MoveToBookPurchase", Params);

            return Convert.ToInt32(Params[1].Value);
        }

        public DataTable GetCartListByOrderID()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderNo", OrderID);
            index++;
            Params[index] = new SqlParameter("@LanguageID", LanguageID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetPurchaseListByOrderID", Params);
        }

        public DataSet get_BookPurchase()
        {
            SqlParameter[] Params = new SqlParameter[12];

            Params[0] = new SqlParameter("@PageSize", PageSize);
            Params[1] = new SqlParameter("@PageIndex", PageIndex);
            Params[2] = new SqlParameter("@SortColumn", SortColumn);
            Params[3] = new SqlParameter("@SortStatus", SortStatus);
            Params[4] = new SqlParameter("@UserType", UserType);
            Params[5] = new SqlParameter("@name", name);
            Params[6] = new SqlParameter("@email", email);
            Params[7] = new SqlParameter("@eBookName", eBookName);
            Params[8] = new SqlParameter("@CategoryName", CategoryName);
            Params[9] = new SqlParameter("@amount", amount);
            Params[10] = new SqlParameter("@FromDate", FromDate);
            Params[11] = new SqlParameter("@ToDate", ToDate);

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "get_BookPurchase_Detail", Params);
        }

        public DataSet get_BookPurchasePartner()
        {
            SqlParameter[] Params = new SqlParameter[13];

            Params[0] = new SqlParameter("@PartnerID", PartnerID);
            Params[1] = new SqlParameter("@PageSize", PageSize);
            Params[2] = new SqlParameter("@PageIndex", PageIndex);
            Params[3] = new SqlParameter("@SortColumn", SortColumn);
            Params[4] = new SqlParameter("@SortStatus", SortStatus);
            Params[5] = new SqlParameter("@UserType", UserType);
            Params[6] = new SqlParameter("@name", name);
            Params[7] = new SqlParameter("@email", email);
            Params[8] = new SqlParameter("@eBookName", eBookName);
            Params[9] = new SqlParameter("@CategoryName", CategoryName);
            Params[10] = new SqlParameter("@amount", amount);
            Params[11] = new SqlParameter("@FromDate", FromDate);
            Params[12] = new SqlParameter("@ToDate", ToDate);

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "get_BookPurchase_DetailForPartner", Params);
        }

        public DataSet get_BookPurchase_Excel()
        {
            SqlParameter[] Params = new SqlParameter[12];

            Params[0] = new SqlParameter("@PageSize", PageSize);
            Params[1] = new SqlParameter("@PageIndex", PageIndex);
            Params[2] = new SqlParameter("@SortColumn", SortColumn);
            Params[3] = new SqlParameter("@SortStatus", SortStatus);
            Params[4] = new SqlParameter("@UserType", UserType);
            Params[5] = new SqlParameter("@name", name);
            Params[6] = new SqlParameter("@email", email);
            Params[7] = new SqlParameter("@eBookName", eBookName);
            Params[8] = new SqlParameter("@CategoryName", CategoryName);
            Params[9] = new SqlParameter("@amount", amount);
            Params[10] = new SqlParameter("@FromDate", FromDate);
            Params[11] = new SqlParameter("@ToDate", ToDate);

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "get_BookPurchase_Detail_Excel", Params);
        }

        public DataSet get_CustomerDetailByOrder()
        {
            SqlParameter[] Params = new SqlParameter[6];

            Params[0] = new SqlParameter("@PageSize", PageSize);
            Params[1] = new SqlParameter("@PageIndex", PageIndex);
            Params[2] = new SqlParameter("@SortColumn", SortColumn);
            Params[3] = new SqlParameter("@SortStatus", SortStatus);
            Params[4] = new SqlParameter("@name", name);
            Params[5] = new SqlParameter("@email", email);

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "Get_Order_ReportCustomerDetail", Params);
        }

        public void MoveToBookPurchase_freebook_website()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CustomerID", UserID);
            index++;

            Params[index] = new SqlParameter("@PurchaseDate", PurchaseDate);
            index++;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Free_Book_purchase_website", Params);
        }
        public DataSet GetChart2(int month)
        {
            int index = 0;
            SqlParameter[] Params = new SqlParameter[1];
            Params[index] = new SqlParameter("@Month", month);
            index++;

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "GetPurchasedBooksByMonth", Params);


        }

        public DataSet GetPartnerChart(int month, int Partner)
        {
            int index = 0;
            SqlParameter[] Params = new SqlParameter[2];
            Params[index] = new SqlParameter("@Month", month);
            index++;
            Params[index] = new SqlParameter("@PartnerID", Partner);

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "GetPurchasedBooksByMonthForParner", Params);
        }

        public DataTable GetCartListDaily()
        {
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[dbo].[pro_GetCartList_Daily]");
        }

        public void MailLog_AddEdit(string RegistrationID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[pro_MailLog_AddEdit]", Params);
        }

        public DataTable MailLog_List(string RegistrationID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@RegistrationID", RegistrationID);

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[dbo].[pro_MailLog_List]", Params);
        }

        public DataSet ViewPendingBooks(string OrderId)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@OrderID", OrderId);

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "[dbo].[sp_BookPurchase_PendingBooksView]", Params);
        }
    }
}
