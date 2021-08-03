using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class BookOrders
    {
        #region Data Members

        long _orderID;
        string _orderNo;
        DateTime _orderDate;
        long _customerID;
        string _sessionID;
        string _amount;
        bool _paymentStatus;
        string _transactionID;
        string _appCode;
        long _WishID;
        private Int64 _LanguageID;

        public Int64 BookID { get; set; }
        public Int64 SubscribedBookID { get; set; }
        public string Vcode { get; set; }
        public Boolean IseBook { get; set; }
        public Boolean IspaperBook { get; set; }
        public Int32 Quantity { get; set; }
        #endregion

        #region Properties

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

        public long OrderID
        {
            get { return _orderID; }
            set
            {
                if (_orderID != value)
                {
                    _orderID = value;

                }
            }
        }

        public long WishID
        {
            get { return _WishID; }
            set
            {
                if (_WishID != value)
                {
                    _WishID = value;

                }
            }
        }

        public string OrderNo
        {
            get { return _orderNo; }
            set
            {
                if (_orderNo != value)
                {
                    _orderNo = value;

                }
            }
        }

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set
            {
                if (_orderDate != value)
                {
                    _orderDate = value;

                }
            }
        }

        public long CustomerID
        {
            get { return _customerID; }
            set
            {
                if (_customerID != value)
                {
                    _customerID = value;

                }
            }
        }

        public string SessionID
        {
            get { return _sessionID; }
            set
            {
                if (_sessionID != value)
                {
                    _sessionID = value;

                }
            }
        }

        public string Amount
        {
            get { return _amount; }
            set
            {
                if (_amount != value)
                {
                    _amount = value;

                }
            }
        }

        public bool PaymentStatus
        {
            get { return _paymentStatus; }
            set
            {
                if (_paymentStatus != value)
                {
                    _paymentStatus = value;

                }
            }
        }

        public string TransactionID
        {
            get { return _transactionID; }
            set
            {
                if (_transactionID != value)
                {
                    _transactionID = value;

                }
            }
        }

        public string AppCode
        {
            get { return _appCode; }
            set
            {
                if (_appCode != value)
                {
                    _appCode = value;

                }
            }
        }


        #endregion

    }
}
