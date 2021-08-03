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
    public class BookBAL : BookPAL
    {

        #region Public Methods


        public DataTable SelectBookByID()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Book_GetByPK", Params);

            return dt;

        }

        public DataTable SelectAllFreeBook()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAllFreeBooks", Params);

            return dt;

        }

        public DataSet GetallEbooks(int PageNo)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CategoryID", CategoryID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            Params[index] = new SqlParameter("@PageNo", PageNo);
            index++;

            DataSet dt = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "GetallEbooks", Params);

            return dt;

        }

        public DataTable GetReviewRatting()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", ID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetReviewRatting", Params);

            return dt;

        }

        public DataTable getBookDetails()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "getAllBookDetailsByID", Params);

            return dt;

        }




        public DataTable getBookDetails_AddToCart()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookId);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "getAllBookDetailsByID_AddToCart", Params);

            return dt;

        }

        public DataSet GetTOP_Coverpages_sellers()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[0] = new SqlParameter("@LangaugeID", LangaugeID);
            index++;

            Params[index] = new SqlParameter("@UserID", UserID);
            index++;

            return SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "[dbo].[GetTOP_Coverpages_sellers]", Params);
        }

        public DataSet SelectBookDetialsByID()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@MagazinID", BookID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;


            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "ws_sp_BookDetialsByID", Params);

            return ds;

        }

        public DataTable SelectAllBook()
        {
            DataTable dt = null;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Book_GetAll");
            return dt;
        }

        public DataTable BookList()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[13];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CategoryID", CategoryID);
            index++;

            Params[index] = new SqlParameter("@MagazinID", BookID);
            index++;


            Params[index] = new SqlParameter("@CurrentPage", CurrentPage);
            index++;

            Params[index] = new SqlParameter("@NoofRows", Rows);
            index++;
            if (WsIsFree != -1)
            {
                Params[index] = new SqlParameter("@IsFree", WsIsFree);
                index++;
            }
            if (IsFeatured != -1)
            {
                Params[index] = new SqlParameter("@IsFeatured", IsFeatured);
                index++;
            }
            if (IsNewArrival != -1)
            {
                Params[index] = new SqlParameter("@IsNewArrival", IsNewArrival);
                index++;
            }
            if (IsTop10 != -1)
            {
                Params[index] = new SqlParameter("@IsTop10", IsTop10);
                index++;
            }
            Params[index] = new SqlParameter("@Book", Title);
            index++;
            Params[index] = new SqlParameter("@ISOCode", ISOCode);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[ws_sp_BookList_test]", Params);
            return dt;
        }

        public int insertIphonePushNotification(string IphoneID, int UserID, int Department)
        {
            SqlParameter[] Params = new SqlParameter[5];
            Params[0] = new SqlParameter("@IphoneID", IphoneID);
            Params[1] = new SqlParameter("@UserID", UserID);
            Params[2] = new SqlParameter("@Department", Department);
            Params[3] = new SqlParameter("@ID", SqlDbType.BigInt);
            Params[3].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "IphonePushNotification_insert", Params);

            return Convert.ToInt32(Params[1].Value);
        }


        public DataTable get_IphonePushNotification(string iphoneid, int UserID, int Department)
        {
            SqlParameter[] Params = new SqlParameter[3];
            Params[0] = new SqlParameter("@iphoneID", iphoneid);
            Params[1] = new SqlParameter("@userid", UserID);
            Params[2] = new SqlParameter("@Department", Department);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "get_IphonePushNotification", Params);
        }

        public DataTable NewArrivalBooks()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[8];
            Int16 index = 0;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetRecentMagagines", Params);
            return dt;
        }

        public DataTable UserBookList(int UserID, int column, int order)
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[7];
            Int16 index = 0;

            Params[index] = new SqlParameter("@UserID", UserID);
            index++;
            if (column == 1)
            {
                Params[index] = new SqlParameter("@column", "m.Title");
                index++;
            }
            else
            {
                Params[index] = new SqlParameter("@column", "cc.OrderDate");
                index++;
            }
            if (order == 0)
            {
                Params[index] = new SqlParameter("@order", "asc");
                index++;
            }
            else
            {
                Params[index] = new SqlParameter("@order", "desc");
                index++;
            }
            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[sp_GetUserBook_test]", Params);
            return dt;
        }

        public DataTable UserBookReadingList(int UserID, int column, int order)
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[7];
            Int16 index = 0;

            Params[index] = new SqlParameter("@UserID", UserID);
            index++;
            if (column == 1)
            {
                Params[index] = new SqlParameter("@column", "mi.Title");
                index++;
            }
            else
            {
                Params[index] = new SqlParameter("@column", "cc.OrderDate");
                index++;
            }

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "[prc_GetUserReadingList]", Params);
            return dt;
        }

        public DataTable SelectAllBookPaging(int pageIndex, int pageSize, string SortColumn, string SortStatus)
        {
            SqlParameter[] Params = new SqlParameter[15];

            Params[0] = new SqlParameter("@PageIndex", pageIndex);

            Params[1] = new SqlParameter("@PageSize", pageSize);

            Params[2] = new SqlParameter("@RecordCount", SqlDbType.Int);
            Params[2].Direction = ParameterDirection.Output;

            //Params[3] = new SqlParameter("@OrderBy", OrderBy);

            //Params[4] = new SqlParameter("@Sorting", Sorting);

            Params[3] = new SqlParameter("@CategoryID", CategoryID);

            Params[4] = new SqlParameter("@Title", Title);



            Params[6] = new SqlParameter("@Publisher", Publisher);


            Params[7] = new SqlParameter("@Autoher", Autoher);
            Params[8] = new SqlParameter("@Language", Language);
            Params[9] = new SqlParameter("@FinalPrice", FinalPrice);
            Params[10] = new SqlParameter("@CreatedOn", SearchText);
            Params[11] = new SqlParameter("@SortColumn", SortColumn);
            Params[12] = new SqlParameter("@SortStatus", SortStatus);
            if (PartnerID > 0)
                Params[13] = new SqlParameter("@PartnerID", PartnerID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllBookPaging", Params);
            totalCount = Convert.ToInt32(!String.IsNullOrEmpty(Params[2].Value.ToString()) ? Convert.ToInt32(Params[2].Value.ToString()) : 0);
            return dt;

        }

        public int GetmaxBook()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_GetmaxBook"));
        }

        public int GetmaxBookImage()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_GetMaxImage"));
        }

        public DataTable GetBookDetail()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;
            Params[index] = new SqlParameter("@title", Title);
            index++;

            Params[index] = new SqlParameter("@id", BookID);
            index++;
            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetBookDetail", Params);
            return dt;
        }

        public DataTable GetBookDetailByTitle()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;
            Params[index] = new SqlParameter("@title", Title);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetBookDetailByTitle", Params);
            return dt;
        }
        public DataTable getUserLibrary_Pagination()
        {
            SqlParameter[] Params = new SqlParameter[4];
            Params[0] = new SqlParameter("@UserID", UserID);
            Params[1] = new SqlParameter("@LanguageID", LanguageID);
            Params[2] = new SqlParameter("@StartIndex", StartIndex);
            Params[3] = new SqlParameter("@EndIndex", EndIndex);
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "UPS_getUserLibrary", Params);
        }
        public DataTable BookIssueList()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[18];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CategoryID", CategoryID);
            index++;


            Params[index] = new SqlParameter("@MagazinID", BookID);
            index++;

            Params[index] = new SqlParameter("@CurrentPage", CurrentPage);
            index++;

            Params[index] = new SqlParameter("@NoofRows", Rows);
            index++;
            if (WsIsFree != 0)
            {
                Params[index] = new SqlParameter("@IsFree", WsIsFree);
                index++;
            }
            if (IsFeatured != 0)
            {
                Params[index] = new SqlParameter("@IsFeatured", IsFeatured);
                index++;
            }
            if (IsPublish != 0)
            {
                Params[index] = new SqlParameter("@IsPublished", IsPublish);
                index++;
            }
            if (IsNewArrival != 0)
            {
                Params[index] = new SqlParameter("@IsNewArrival", IsNewArrival);
                index++;
            }

            Params[index] = new SqlParameter("@SearchText", SearchText);
            index++;

            if (IsTop10 != 0)
            {
                Params[index] = new SqlParameter("@IsTop10", IsTop10);
                index++;
            }
            if (BookID != 0)
            {
                Params[index] = new SqlParameter("@BookID", BookID);
                index++;
            }

            if (IsSpecial != 0)
            {
                Params[index] = new SqlParameter("@IsSpecial", IsSpecial);
                index++;
            }
            Params[index] = new SqlParameter("@Book", Title);
            index++;
            if (LangaugeID != 0)
            {
                Params[index] = new SqlParameter("@LanguageID", LangaugeID);
                index++;
            }

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;
            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_BookIssuesList", Params);
            return dt;
        }

        public DataTable BookListDrop()
        {
            DataTable dt = null;
            SqlParameter[] Params = new SqlParameter[12];
            Int16 index = 0;

            Params[index] = new SqlParameter("@SubCateogryID", CategoryID);
            index++;

            dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_BookListByCategory", Params);
            return dt;
        }
        /*===========================Book Images================================================*/
        public int InsertBookImage()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@ImagePath", ImagePath);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);
            index++;

            Params[index] = new SqlParameter("@IsTitled", IsTitled);
            index++;

            Params[index] = new SqlParameter("@ID", SqlDbType.Int);
            Params[index].Direction = ParameterDirection.ReturnValue;
            index++;

            object result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Book_Image_Insert", Params);

            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[4].Value);

            return Convert.ToInt32(result);



        }

        public void InsertSearchTags(string search)
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@tags", search);
            index++;
            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            object result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "prc_InsertSearchTags", Params);





        }

        public DataTable GetSearchTags()
        {
            SqlParameter[] Params = new SqlParameter[0];
            Int16 index = 0;


            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "prc_GetSearchTags");

        }

        public int UpdateBookImage()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@ImagePath", ImagePath);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);
            index++;

            Params[index] = new SqlParameter("@IsTitled", IsTitled);
            index++;


            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Book_Image_UpdateByPK", Params);
            return result;

        }

        public int DeleteBookImage()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Book_Image_DeleteByPK", Params);
            return result;

        }

        public DataTable wsGetAllBookImageByBook()
        {

            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_GetAllBookImageByBook", Params);

            return dt;
        }
        public DataTable wsGetAllBookPdfURLByBookID()
        {

            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "ws_GetAllBookPdfURLByBookID", Params);

            return dt;
        }

        public DataTable GetAllBookImageByBook()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAllBookImageByBook", Params);

            return dt;
        }

        public DataTable GetDynamicFieldsByBookIDLanguageID()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;


            Params[index] = new SqlParameter("@LanguageID", LangaugeID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetDynamicFieldsByBookIDLanguageID", Params);

            return dt;
        }

        public int InsertUpdateBook()
        {
            SqlParameter[] Params = new SqlParameter[8];

            Params[0] = new SqlParameter("@ID", BookID);
            Params[0].Direction = ParameterDirection.ReturnValue;

            Params[1] = new SqlParameter("@LanguageID", LanguageID);

            Params[2] = new SqlParameter("@Title", Title);

            Params[3] = new SqlParameter("@Description", Description);

            Params[5] = new SqlParameter("@BookID", BookID);

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Book_InsertUpdate", Params);
            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[0].Value);
            return result;
        }
        /*====================Book Isssue ================================================*/
        public int InsertBookIssue()
        {
            SqlParameter[] Params = new SqlParameter[40];
            Params[0] = new SqlParameter("@ID", BookID);
            Params[1] = new SqlParameter("@BookID", BookID);
            Params[2] = new SqlParameter("@Title", Title);
            Params[3] = new SqlParameter("@Price", Price);
            Params[4] = new SqlParameter("@IsActive", IsActive);
            Params[5] = new SqlParameter("@IsFree", IsFree);
            Params[6] = new SqlParameter("@IsFeatured", IsFeatured);
            Params[7] = new SqlParameter("@CreatedBy", CreatedBy);
            Params[8] = new SqlParameter("@UpdatedBy", UpdatedBy);
            Params[9] = new SqlParameter("@UpdatedOn", UpdatedOn);
            Params[10] = new SqlParameter("@Published", Published);
            Params[11] = new SqlParameter("@PublishedDate", DateTime.Now);
            Params[12] = new SqlParameter("@IsSpecial", IsSpecial);
            Params[13] = new SqlParameter("@ExplorerPdfStartNo", ExplorerPdfStartNo);
            Params[14] = new SqlParameter("@ExplorerPdfEndNo", ExplorerPdfEndNo);
            Params[15] = new SqlParameter("@Result", SqlDbType.BigInt);
            Params[16] = new SqlParameter("@DescriptionImages", DescriptionImages == null ? "" : DescriptionImages);
            Params[15].Direction = ParameterDirection.Output;
            Params[16] = new SqlParameter("@CategoryID", CategoryID);
            Params[17] = new SqlParameter("@Autoher", Autoher);
            Params[18] = new SqlParameter("@Discount", DiscountedPrice);
            Params[19] = new SqlParameter("@FinalPrice", FinalPrice);
            Params[20] = new SqlParameter("@Language", Language);
            Params[21] = new SqlParameter("@OrderIndex", OrderIndex);
            Params[22] = new SqlParameter("@SpecialOffer", SpecialOffer);
            Params[23] = new SqlParameter("@SpecialOfferStart", SpecialOfferStart);
            Params[24] = new SqlParameter("@SpecialOfferEnd", SpecialOfferEnd);
            Params[25] = new SqlParameter("@DealerEmail", DealerEmail);
            Params[26] = new SqlParameter("@PaperBookPrice", PaperBookPrice);
            Params[27] = new SqlParameter("@PaperBookDiscount", PaperBookDiscount);
            Params[28] = new SqlParameter("@PaperBookFinalPrice", PaperBookFinalPrice);
            Params[29] = new SqlParameter("@IseBook", IseBook);
            Params[30] = new SqlParameter("@IsPaperBook", IsPaperBook);
            Params[31] = new SqlParameter("@IsFreePaper", IsFreePaper);
            Params[32] = new SqlParameter("@Quantity", Quantity);
            Params[33] = new SqlParameter("@PartnerID", PartnerID);
            Params[34] = new SqlParameter("@Weight", Weight);
            Params[35] = new SqlParameter("@DimWeight", DimWeight);
            Params[36] = new SqlParameter("@Width", Width);
            Params[37] = new SqlParameter("@Height", Height);
            Params[38] = new SqlParameter("@Depth", Depth);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookIssue_Insert", Params);
            return Convert.ToInt32(Params[15].Value);

        }

        public int DeleteBookIssue()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookIssue_DeleteByPK", Params);
            return result;

        }

        public DataTable SelectAllBookIssuePaging(int pageIndex, int pageSize)
        {
            SqlParameter[] Params = new SqlParameter[6];

            Params[0] = new SqlParameter("@PageIndex", pageIndex);

            Params[1] = new SqlParameter("@PageSize", pageSize);

            Params[2] = new SqlParameter("@RecordCount", SqlDbType.Int);
            Params[2].Direction = ParameterDirection.Output;


            Params[3] = new SqlParameter("@BookID", BookID);



            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllBookIssuePaging", Params);
            totalCount = Convert.ToInt32(!String.IsNullOrEmpty(Params[2].Value.ToString()) ? Convert.ToInt32(Params[2].Value.ToString()) : 0);
            return dt;

        }

        public DataTable SelectAllBookIssueByBookID()
        {
            SqlParameter[] Params = new SqlParameter[2];

            Params[0] = new SqlParameter("@BookID", BookID);


            Params[1] = new SqlParameter("@CategoryID", CategoryID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllBookIssueByBookID", Params);

            return dt;

        }

        public DataTable SelectTop10Books()
        {
            int index = 0;
            SqlParameter[] Params = new SqlParameter[4];


            if (CategoryID > 0)
            {
                Params[index] = new SqlParameter("@CategoryID", CategoryID);
                index++;
            }

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;
            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "prc_GetTop10Books", Params);
            return dt;
        }

        public void ActiveBookIssue()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;
            Params[index] = new SqlParameter("@ID", ID);
            index++;
            Params[index] = new SqlParameter("@Active", IsActive);
            index++;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Active_BookIssue", Params);
        }

        public int InsertBookIssueImage()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@ImagePath", ImagePath);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);
            index++;

            Params[index] = new SqlParameter("@IsTitled", IsTitled);
            index++;

            Params[index] = new SqlParameter("@ID", SqlDbType.Int);


            Params[index].Direction = ParameterDirection.ReturnValue;
            index++;

            object result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookIssue_Image_Insert", Params);

            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[4].Value);

            return Convert.ToInt32(result);



        }

        public int DeleteBookIssueImage()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookIssue_Image_DeleteByPK", Params);
            return result;

        }

        public DataTable GetAllBookIssueImageByBook()
        {

            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAllBookIssueImageByBook", Params);

            return dt;
        }

        public int GetmaxBookIssueImage()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_GetMaxIssueImage"));
        }

        public int InsertBookIssueDescImage()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@ImagePath", ImagePath);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);
            index++;

            Params[index] = new SqlParameter("@IsTitled", IsTitled);
            index++;

            Params[index] = new SqlParameter("@ID", SqlDbType.Int);
            Params[index].Direction = ParameterDirection.ReturnValue;
            index++;

            object result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookIssueDesc_Image_Insert", Params);

            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[4].Value);

            return Convert.ToInt32(result);
        }

        public int GetmaxBookIssueDescImage()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_GetIssueMaxDescImage"));
        }
        #endregion

        public int InsertBookDescImage()
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@ImagePath", ImagePath);
            index++;

            Params[index] = new SqlParameter("@IsActive", IsActive);
            index++;

            Params[index] = new SqlParameter("@IsTitled", IsTitled);
            index++;

            Params[index] = new SqlParameter("@ID", SqlDbType.Int);
            Params[index].Direction = ParameterDirection.ReturnValue;
            index++;

            object result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BookDesc_Image_Insert", Params);

            if (!Convert.IsDBNull(new SqlParameter("@ID", SqlDbType.Int)))
                result = Convert.ToInt32(Params[4].Value);

            return Convert.ToInt32(result);
        }

        public int GetmaxBookDescImage()
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "proc_GetMaxDescImage"));
        }

        public int GetTotalBooksCount()
        {
            return Convert.ToInt32(DAL.SqlHelper.ExecuteScalar(CommandType.Text, "select COUNT(*) from Book"));
        }

        public void UpdateOrderIndex(string index, string bookID)
        {
            string str = "UPDATE Book SET OrderIndex = " + index + " WHERE BookID = " + bookID;
            SqlHelper.ExecuteScalar(CommandType.Text, str);
        }

        public void UpdateQuantity(string Quantity, string bookID)
        {
            string str = "UPDATE Book SET Quantity = " + Quantity + " WHERE BookID = " + bookID;
            SqlHelper.ExecuteScalar(CommandType.Text, str);
        }

        public int DeleteBook()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "BookIssue_Delete", Params));
        }

        public int DeleteReviewRatting()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@ID", ID);
            index++;
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.StoredProcedure, "Delete_ReviewRatting", Params));
        }


        public DataTable GetDescriptionImagesByIssue()
        {

            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            Params[index] = new SqlParameter("@URL", Config.WebSite);
            index++;


            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_GetIssueDescriptionImages", Params);

            return dt;
        }

        public DataTable SearchBook()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@Title", Title);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "GetAll_SearchedBook", Params);


        }

        /*====================== Book Order ====================================*/
        public DataTable SelectBookOrder()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "BookOrder_GetAll", Params);

            return dt;
        }

        public DataTable SelectAllBookIssueByOrderID()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@OrderID", OrderID);
            index++;

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectAllBookIssueByOrderID", Params);

            return dt;
        }

        public int OrderApprove()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@PurchaseID", ID);
            index++;

            int result = SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "proc_OrderApprove", Params);
            return result;

        }

        public DataTable GetBookIssueTitles()
        {
            SqlParameter[] Params = new SqlParameter[0];
            Int16 index = 0;
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "prc_GetBookTitles", Params);
        }

        public DataTable get_editor_book_website()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LangaugeID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Get_editor_book_website", Params);
        }

        public DataTable get_all_book_website()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Get_AllBook_website", Params);
        }


        public DataTable get_all_book_website1()
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            Params[index] = new SqlParameter("@StartIndex", StartIndex);
            index++;

            Params[index] = new SqlParameter("@EndIndex", EndIndex);
            index++;



            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Get_AllBook_website1", Params);
        }

        public DataTable get_all_book_website2(int SortBy, string For)
        {
            SqlParameter[] Params = new SqlParameter[5];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            Params[index] = new SqlParameter("@StartIndex", StartIndex);
            index++;

            Params[index] = new SqlParameter("@EndIndex", EndIndex);
            index++;

            Params[index] = new SqlParameter("@SortBy", SortBy);
            index++;

            Params[index] = new SqlParameter("@For", For);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Get_AllBook_website2", Params);
        }

        public DataTable get_all_specialoffer_book()
        {
            SqlParameter[] Params = new SqlParameter[3];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;

            Params[index] = new SqlParameter("@StartIndex", StartIndex);
            index++;

            Params[index] = new SqlParameter("@EndIndex", EndIndex);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Get_AllBook_specialoffer", Params);
        }

        public DataTable get_all_book_websiteGloblalSearch(Int32 CatID, String Search, int SortBy, string For/*, int PageIndex, int PageSize*/)
        {
            SqlParameter[] Params = new SqlParameter[8];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LanguageID);
            index++;
            if (CatID != 0)
            {
                Params[index] = new SqlParameter("@CatID", CatID);
                index++;
            }
            if (Search != "")
            {
                Params[index] = new SqlParameter("@Search", Search);
                index++;
            }

            Params[index] = new SqlParameter("@SortBy", SortBy);
            index++;

            Params[index] = new SqlParameter("@For", For);
            index++;

            //Params[index] = new SqlParameter("@PageIndex", PageIndex);
            //index++;

            //Params[index] = new SqlParameter("@PageSize", PageSize);
            //index++;
            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Get_AllBook_websiteGlobalSearch", Params);
        }


        public DataTable get_top_seller_book_website()
        {
            
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@LanguageID", LangaugeID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "sp_Get_Coverpages_sellers_website", Params);
        }


        public void Insert_ReviewRatting()
        {
            SqlParameter[] Params = new SqlParameter[7];

            Params[0] = new SqlParameter("@BookID", BookID);
            Params[1] = new SqlParameter("@UserID", UserID);
            Params[2] = new SqlParameter("@Name", Name);
            Params[3] = new SqlParameter("@Summary", Summary);
            Params[4] = new SqlParameter("@Review", Review);
            Params[5] = new SqlParameter("@Ratting", Ratting);
            Params[6] = new SqlParameter("@CreatedDate", CreatedDate);

            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "sp_Insert_ReviewRatting_website", Params);
        }

        public DataSet SelectAllReview(int pageIndex, int pageSize, string Search, string SortColumn, string SortStatus)
        {
            SqlParameter[] Params = new SqlParameter[5];

            Params[0] = new SqlParameter("@PageIndex", pageIndex);
            Params[1] = new SqlParameter("@PageSize", pageSize);
            Params[2] = new SqlParameter("@Search", Search);
            Params[3] = new SqlParameter("@SortColumn", SortColumn);
            Params[4] = new SqlParameter("@SortStatus", SortStatus);

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.StoredProcedure, "proc_SelectAllReview", Params);
            return ds;
        }

        public DataTable SelectReviewById(int ReviewId)
        {
            SqlParameter[] Params = new SqlParameter[1];

            Params[0] = new SqlParameter("@ReviewId", ReviewId);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "proc_SelectReviewById", Params);
            return dt;
        }

        public DataTable GerReviewList(int BookID)
        {
            SqlParameter[] Params = new SqlParameter[1];
            Params[0] = new SqlParameter("@BookID", BookID);

            DataTable dt = SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "get_book_ratting_website_List", Params);
            return dt;
        }

        public DataTable get_book_ratting()
        {
            SqlParameter[] Params = new SqlParameter[1];
            Int16 index = 0;

            Params[index] = new SqlParameter("@BookID", BookID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "get_book_ratting_website", Params);
        }

        public DataTable getBookByCategory()
        {
            SqlParameter[] Params = new SqlParameter[2];
            Int16 index = 0;

            Params[index] = new SqlParameter("@CategoryID", CategoryID);
            index++;

            Params[index] = new SqlParameter("@LanguageID", LangaugeID);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Book_GetByCategory", Params);
        }

        public DataTable getBookByCategoryLimited(int SortBy, string For)
        {
            SqlParameter[] Params = new SqlParameter[6];
            Int16 index = 0;

            if (CategoryID != 0)
            {
                Params[index] = new SqlParameter("@CategoryID", CategoryID);
                index++;
            }
            if (LangaugeID != 0)
            {
                Params[index] = new SqlParameter("@LanguageID", LangaugeID);
                index++;
            }

            Params[index] = new SqlParameter("@StartIndex", StartIndex);
            index++;

            Params[index] = new SqlParameter("@EndIndex", EndIndex);
            index++;

            Params[index] = new SqlParameter("@SortBy", SortBy);
            index++;

            Params[index] = new SqlParameter("@For", For);
            index++;

            return SqlHelper.ExecuteDataTable(CommandType.StoredProcedure, "Book_GetByCategoryNew", Params);
        }

    }
}
