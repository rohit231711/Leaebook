using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAL
{
    public class BookPAL
    {
        #region Private Variables
        private Int64 _BookID;
        private String _BookId;
        private Int64 _CategoryID;
        private String _Title;
        private String _SearchText;
        private DateTime _CreatedDate;
        private Boolean _IsActive;
        private Boolean _IsFree;
        private Int32 _CreatedBy;
        private DateTime _CreatedOn;
        private Int32 _UpdatedBy;
        private DateTime _UpdatedOn;
        private Boolean _Published;
        private Int64 _ID;
        private String _ImagePath;
        private Boolean _IsTitled;
        private Int64 _OrderIndex { get; set; }
        private Boolean _SpecialOffer { get; set; }
        private DateTime? _SpecialOfferStart { get; set; }
        private DateTime? _SpecialOfferEnd { get; set; }
        private String _DealerEmail { get; set; }

        public String PaperBookPrice { get; set; }
        public String PaperBookDiscount { get; set; }
        public String PaperBookFinalPrice { get; set; }
        public Boolean IseBook { get; set; }
        public Boolean IsPaperBook { get; set; }
        public Boolean IsFreePaper { get; set; }
        public int CurrentPage { get; set; }
        public int Rows { get; set; }
        public int CategoryID { get; set; }
        public String CategoryName { get; set; } 
        public int LanguageID { get; set; }
        public int WsIsFree { get; set; }
        public int CountryID { get; set; }
        public String Language { get; set; }
        public String Description { get; set; }
        public String DescriptionImages { get; set; }
        public String Autoher { get; set; }
        public string Price { get; set; }
        public int IsFeatured { get; set; }
        public int IsNewArrival { get; set; }
        public int IsPublish { get; set; }
        public int IsTop10 { get; set; }
        public int LangaugeID { get; set; }
        public float DiscountedPrice { get; set; }
        public string FinalPrice { get; set; }
        public DateTime PublishDate { get; set; }
        public String ISOCode { get; set; }
        public String Publisher { get; set; }
        public Int32 OrderID { get; set; }
        public Int32 IsSpecial { get; set; }
        public Int32 ExplorerPdfStartNo { get; set; }
        public Int32 ExplorerPdfEndNo { get; set; }
        public Int32 StartIndex { get; set; }
        public Int32 EndIndex { get; set; }
        private String _Weight;
        private String _DimWeight;
        private String _Width;
        private String _Height;
        private String _Depth;

        public String Depth
        {
            get { return _Depth; }
            set { _Depth = value; }
        }
        
        public String Height
        {
            get { return _Height; }
            set { _Height = value; }
        }
        
        public String Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        
        public String DimWeight
        {
            get { return _DimWeight; }
            set { _DimWeight = value; }
        }
        
        public String Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        
        private Int32 _Quantity;
        public Int32 Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private Int32 _PartnerID;
        public Int32 PartnerID
        {
            get { return _PartnerID; }
            set { _PartnerID = value; }
        }
                

        #region review        
            private int _UserID;
            private string _Name;
            private String _Summary;
            private String _Review;
            private int _Ratting;
        #endregion
        #endregion

        #region Public Properties
        public Int64 BookID
        {
            get
            {
                return _BookID;
            }
            set
            {
                _BookID = value;
            }
        }

        public Int64 OrderIndex
        {
            get
            {
                return _OrderIndex;
            }
            set
            {
                _OrderIndex = value;
            }
        }

        public String BookId
        {
            get
            {
                return _BookId;
            }
            set
            {
                _BookId = value;
            }
        }
   
        public String DealerEmail
        {
        get{
            return _DealerEmail;
        }
            set
            {
                _DealerEmail = value;
            }
        }
        public String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public String SearchText
        {
            get
            {
                return _SearchText;
            }
            set
            {
                _SearchText = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }
        public DateTime? SpecialOfferStart
        {
            get
            {
                return _SpecialOfferStart;
            }
            set
            {
                _SpecialOfferStart = value;
            }
        }
        public DateTime? SpecialOfferEnd
        {
            get
            {
                return _SpecialOfferEnd;
            }
            set
            {
                _SpecialOfferEnd = value;
            }
        }
        public Boolean SpecialOffer
        {
            get
            {
                return _SpecialOffer;
            }
            set
            {
                _SpecialOffer = value;
            }
        }
        public Boolean IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }
        public Boolean IsFree
        {
            get
            {
                return _IsFree;
            }
            set
            {
                _IsFree = value;
            }
        }
        public Int32 CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
            }
        }
        public DateTime CreatedOn
        {
            get
            {
                return _CreatedOn;
            }
            set
            {
                _CreatedOn = value;
            }
        }
        public Int32 UpdatedBy
        {
            get
            {
                return _UpdatedBy;
            }
            set
            {
                _UpdatedBy = value;
            }
        }
        public DateTime UpdatedOn
        {
            get
            {
                return _UpdatedOn;
            }
            set
            {
                _UpdatedOn = value;
            }
        }
        public Boolean Published
        {
            get
            {
                return _Published;
            }
            set
            {
                _Published = value;
            }
        }
        public int totalCount
        {
            get;
            set;
        }
        public Int64 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        public String ImagePath
        {
            get
            {
                return _ImagePath;
            }
            set
            {
                _ImagePath = value;
            }
        }
        public Boolean IsTitled
        {
            get
            {
                return _IsTitled;
            }
            set
            {
                _IsTitled = value;
            }
        }
        
        #endregion

        #region review            
            public int UserID
            {
                get
                {
                    return _UserID;
                }
                set
                {
                    _UserID = value;
                }
            }
            public string Name
            {
                get
                {
                    return _Name;
                }
                set
                {
                    _Name = value;
                }
            }

            public String Summary
            {
                get
                {
                    return _Summary;
                }
                set
                {
                    _Summary = value;
                }
            }
            public String Review
            {
                get
                {
                    return _Review;
                }
                set
                {
                    _Review = value;
                }
            }
            public int Ratting
            {
                get
                {
                    return _Ratting;
                }
                set
                {
                    _Ratting = value;
                }
            }            
        #endregion
    }
}
