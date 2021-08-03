using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class BookPurchase
    {


        #region Data Members

        private Int64 _LanguageID;
        Int64 _purchaseID;
        Int64 _userID;
        Int64 _BookID;
        Int64 _BookBookID;
        DateTime _purchaseDate;
        public long OrderID { get; set; }
        public string Amount { get; set; }
        public string Domain { get; set; }
        public string Status { get; set; }
        public string AppCode { get; set; }
        public string TransID { get; set; }
        public string Vcode { get; set; }
        public string SessionID { get; set; }
        public string Title { get; set; }

        //manage 
        public int PageSize{ get; set; }
        public int PageIndex{ get; set; }
        public string SortColumn{ get; set; }
        public string SortStatus{ get; set; }
        public int UserType{ get; set; }
        public string name{ get; set; }
        public string email{ get; set; }
        public string eBookName{ get; set; }
        public string CategoryName{ get; set; }
        public string amount{ get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        #endregion

        #region Properties

        private int _PartnerID;
        public int PartnerID
        {
            get { return _PartnerID; }
            set { _PartnerID = value; }
        }

        public long PurchaseID
        {
            get { return _purchaseID; }
            set
            {
                if (_purchaseID != value)
                {
                    _purchaseID = value;

                }
            }
        }

        public Int64 LanguageID
        {
            get
            {
                return _LanguageID;
            }
            set
            {
                _LanguageID = value;
            }
        }

        public long UserID
        {
            get { return _userID; }
            set
            {
                if (_userID != value)
                {
                    _userID = value;

                }
            }
        }

        public long BookID
        {
            get { return _BookID; }
            set
            {
                if (_BookID != value)
                {
                    _BookID = value;

                }
            }
        }

        public long BookBookID
        {
            get { return _BookBookID; }
            set
            {
                if (_BookBookID != value)
                {
                    _BookBookID = value;

                }
            }
        }

        public DateTime PurchaseDate
        {
            get { return _purchaseDate; }
            set
            {
                if (_purchaseDate != value)
                {
                    _purchaseDate = value;

                }
            }
        }


        #endregion
    }
}
