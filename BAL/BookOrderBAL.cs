using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PAL;
using System.Data.Sql;
using System.Data.SqlClient;
using DAL;
using System.Data;
namespace BAL
{
    public class BookOrderBAL : BookOrders
    {

        public int InsertCustomerCart1()
        {
            SqlParameter[] Params = new SqlParameter[8];
            Int16 index = 0;            

            Params[index] = new SqlParameter("@OrderDate", OrderDate);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;                       

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@OrderNo", OrderNo);
            index++;

            Params[index] = new SqlParameter("@IseBook", IseBook);
            index++;

            Params[index] = new SqlParameter("@IspaperBook", IspaperBook);
            index++;

            Params[index] = new SqlParameter("@Quantity", Quantity);
            index++;
            
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_AddCustomerCart", Params);

            return Convert.ToInt32(Params[2].Value);
        }

        public int MoveToCustomerCart()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderDate", OrderDate);
            index++;

            Params[index] = new SqlParameter("@WishID", WishID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_MoveToCustomerCart", Params);

            return Convert.ToInt32(Params[1].Value);

        }

        public int MoveToCustomerCart_OrderNo()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderDate", OrderDate);
            index++;

            Params[index] = new SqlParameter("@OrderNo", OrderNo);
            index++;

            Params[index] = new SqlParameter("@WishID", WishID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_MoveToCustomerCart_OrderNo", Params);

            return Convert.ToInt32(Params[2].Value);

        }



        public int InsertCustomerWishList1()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@AddedOn", OrderDate);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_AddCustomerWishList", Params);

            return Convert.ToInt32(Params[2].Value);
        }

        public int InsertCustomerWishList_BookType()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@AddedOn", OrderDate);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;
            Params[index] = new SqlParameter("@ebook", IseBook);
            index++;
            Params[index] = new SqlParameter("@PaperBook", IspaperBook);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[sp_AddCustomerWishList_BookType]", Params);

            return Convert.ToInt32(Params[2].Value);
        }

        public int InsertCustomerCart()
        {
            SqlParameter[] Params = new SqlParameter[20];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            Params[index].Direction = ParameterDirection.Output;

            index++;

            Params[index] = new SqlParameter("@OrderNo", OrderNo);
            index++;

            Params[index] = new SqlParameter("@OrderDate", OrderDate);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;
            Params[index] = new SqlParameter("@SessionID", SessionID);
            index++;
            Params[index] = new SqlParameter("@Amount", Amount);
            index++;
            Params[index] = new SqlParameter("@PaymentStatus", PaymentStatus);
            index++;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;
            ;
            Params[index] = new SqlParameter("@SubscribedBookID", SubscribedBookID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_CustomerCart_Insert", Params);

            return Convert.ToInt32(Params[0].Value);

        }

        public int InsertCustomerWishList()
        {
            SqlParameter[] Params = new SqlParameter[20];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", OrderID);
            Params[index].Direction = ParameterDirection.Output;

            index++;

            Params[index] = new SqlParameter("@AddedOn", OrderDate);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@Amount", Amount);
            index++;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[sp_CustomerWishList_Insert]", Params);

            return Convert.ToInt32(Params[0].Value);
        }

        public DataTable SelectCustomerWishList()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetCustomerWishList", Params);

            return dt;

        }

        public DataTable SelectCustomerCart()
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@SessionID", SessionID);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetCustomerCart", Params);

            return dt;

        }
        public int DeleteUserCart()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@SessionID", SessionID);
            index++;
            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@CustomerId", CustomerID);
                index++;
            }
            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_DeleteUserCart", Params);
            return result;

        }

        
        public DataTable GetissueCountInCart()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@sessionid", SessionID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetIssueCountInCart", Params);
            return dt;

        }


        public DataTable GetMaxOrderNo()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            if (!string.IsNullOrEmpty(SessionID))
            {
                Params[index] = new SqlParameter("@sessionid", SessionID);
                index++;
            }
            if (!string.IsNullOrEmpty(CustomerID.ToString()))
            {
                Params[index] = new SqlParameter("@CustomerID", CustomerID);
                index++;
            }
            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetMaxOrderNo", Params);
            return dt;

        }


        public void UpdataeCustomerIDinCart()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            if (!string.IsNullOrEmpty(SessionID))
            {
                Params[index] = new SqlParameter("@sessionid", SessionID);
                index++;
            }
            if (!string.IsNullOrEmpty(CustomerID.ToString()))
            {
                Params[index] = new SqlParameter("@CustomerID", CustomerID);
                index++;
            }
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "prc_UpdateCustomerCart", Params);


        }

        public int DeleteItemfromUserCart()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@orderid", OrderID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_DeleteItemInUserCart", Params);
            return result;

        }

        public int MoveItemfromUserCartToWishlist()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;


            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_MoveToCustomerWishList", Params);
            return result;

        }

        public int DeletefromCustomerCart()
        {
            SqlParameter[] Params = new SqlParameter[4];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@CustID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@IseBook", IseBook);
            index++;

            Params[index] = new SqlParameter("@IsPaperBook", IspaperBook);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_DeleteFromCaretlist", Params);
            return result;

        }

        public int DeletefromWishList()
        {
            SqlParameter[] Params = new SqlParameter[4];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@CustID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@IseBook", IseBook);
            index++;

            Params[index] = new SqlParameter("@IsPaperBook", IspaperBook);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_DeleteFromWishList", Params);
            return result;
        }

        public int DeleteItemfromUserWishList()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@WishID", WishID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_DeleteItemInUserWishList", Params);
            return result;

        }


        public int ManageCustomerCart(int IstoDelete)
        {



            SqlParameter[] Params = new SqlParameter[20];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            Params[index].Direction = ParameterDirection.InputOutput;

            index++;
            Params[index] = new SqlParameter("@OrderDate", OrderDate);
            index++;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;
            Params[index] = new SqlParameter("@SessionID", SessionID);
            index++;
            Params[index] = new SqlParameter("@Amount", Amount);
            index++;


            Params[index] = new SqlParameter("@BookID", BookID);
            index++;
            ;
            Params[index] = new SqlParameter("@BookID", SubscribedBookID);
            index++;

            Params[index] = new SqlParameter("@Delete", IstoDelete);
            index++;



            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[ws_sp_CustomerCart_Manage]", Params);


            if (OrderID < 0)
            {
                return Convert.ToInt32(Params[0].Value);
            }
            else
            {
                return Convert.ToInt32(OrderID);
            }


        }

        public DataTable ws_SelectCustomerCart()
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            if (!string.IsNullOrEmpty(SessionID))
            {
                Params[index] = new SqlParameter("@SessionID", SessionID);
                index++;
            }
            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@CustomerID", CustomerID);
                index++;
            }
            Params[index] = new SqlParameter("@SessionID", SessionID);
            index++;
            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;
            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_sp_GetCustomerCart", Params);

            return dt;

        }


        public DataTable CheckDuplicateItemInCart()
        {
            SqlParameter[] Params = new SqlParameter[4];
            Int16 index = 0;

            if (!string.IsNullOrEmpty(SessionID))
            {
                Params[index] = new SqlParameter("@SessionID", SessionID);
                index++;
            }
            if (SubscribedBookID > 0)
            {
                Params[index] = new SqlParameter("@BookID", SubscribedBookID);
                index++;
            }

            if (BookID > 0)
            {
                Params[index] = new SqlParameter("@BookID", BookID);
                index++;
            }


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_CheckIteminCart", Params);

            return dt;

        }


        public DataTable CheckIteminPurchased()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@userid", CustomerID);
            index++;

            if (BookID > 0)
            {
                Params[index] = new SqlParameter("@BookID", BookID);
                index++;
            }


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_CheckIteminPurchased", Params);

            return dt;

        }

        public DataTable CheckDuplicateItemInWishList()
        {
            SqlParameter[] Params = new SqlParameter[4];
            Int16 index = 0;

            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@CustomerID", CustomerID);
                index++;
            }


            if (BookID > 0)
            {
                Params[index] = new SqlParameter("@BookID", BookID);
                index++;
            }


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "CheckDuplicateIteminWishList", Params);

            return dt;

        }




        public DataTable CheckDuplicateItemInPurchased()
        {
            SqlParameter[] Params = new SqlParameter[4];
            Int16 index = 0;

            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@userid", CustomerID);
                index++;
            }
            if (SubscribedBookID > 0)
            {
                Params[index] = new SqlParameter("@BookID", SubscribedBookID);
                index++;
            }

            if (BookID > 0)
            {
                Params[index] = new SqlParameter("@BookID", BookID);
                index++;
            }


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_CheckIteminPurchased", Params);

            return dt;

        }



        public void UpdatePaymentStatus()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@SessionID", SessionID);
            index++;

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "prc_UpdatePaymentStatus", Params);

        }


        public DataSet UserPurchaseHistory()
        {

            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@CustomerID", CustomerID);
                index++;
            }
            if (OrderNo != "")
            {
                Params[index] = new SqlParameter("@Orderno", OrderNo);
                index++;
            }
            Params[index] = new SqlParameter("@URL", Config.WebSite);


            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "prc_UserPurchaseHistory", Params);

        }



        public DataSet GetLastOrder()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@CustomerID", CustomerID);
                index++;
            }



            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "prc_GetLastOrder", Params);

        }

        public DataTable GetCartList()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetCartList", Params);

            return dt;

        }

        public DataTable GetCartListByOrderNo()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderNo", OrderNo);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetCartListByOrderNo", Params);

            return dt;

        }

        public DataTable GetCartListByOrderID()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetCartListByOrderID", Params);

            return dt;

        }

        public DataTable GetWishList()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CustomerID", CustomerID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetWishList", Params);

            return dt;

        }

        public DataTable GetSubscriptions()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@UserId", CustomerID);
                index++;
            }

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;


            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[sp_GetUserSubscribtions]", Params);

        }

        public DataTable GetSubscriptionsForPartner(int PartnerID)
        {

            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            if (PartnerID > 0)
            {
                Params[index] = new SqlParameter("@PartnerID", PartnerID);
                index++;
            }

            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@UserId", CustomerID);
                index++;
            }

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;


            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[sp_GetUserSubscribtionsForPartner]", Params);

        }

        public DataTable get_CustomerBookOrderByID()
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;
            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@UserId", CustomerID);
                index++;
            }
            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;

            //Params[index] = new SqlParameter("@LanguageID", LanguageID);
            //index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[get_CustomerBookOrderByID]", Params);
        }

        public DataTable CheckCartlist()
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;
            if (CustomerID > 0)
            {
                Params[index] = new SqlParameter("@UserId", CustomerID);
                index++;
            }
            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            //Params[index] = new SqlParameter("@LanguageID", LanguageID);
            //index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "get_CheckCartlist", Params);
        }

    }
}
