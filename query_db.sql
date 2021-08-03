USE [leatodo_dev]
GO

/****** Object:  Table [dbo].[WebsiteSetting]    Script Date: 7/27/2021 4:58:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebsiteSetting](
	[WebSettingID] [bigint] IDENTITY(1,1) NOT NULL,
	[BrowserTitle] [nvarchar](200) NOT NULL,
	[WebSiteTitle] [nvarchar](200) NOT NULL,
	[ClientEmail] [nvarchar](100) NULL,
	[SupportEmail] [nvarchar](100) NULL,
	[WebSiteAddress] [nvarchar](max) NULL,
	[WebsitePhone] [nvarchar](20) NULL,
	[WebsiteMobile] [nvarchar](20) NULL,
	[WebsiteFax] [nvarchar](20) NULL,
	[SMTPAddress] [nvarchar](200) NOT NULL,
	[SMTPPort] [nvarchar](20) NOT NULL,
	[SMTPEmail] [nvarchar](100) NOT NULL,
	[SMTPPassword] [nvarchar](100) NOT NULL,
	[SendEmailFrom] [nvarchar](100) NULL,
	[EmailHeader] [nvarchar](max) NULL,
	[EmailFooter] [nvarchar](max) NULL,
	[FaceBookLink] [nvarchar](200) NOT NULL,
	[TwiterLink] [nvarchar](200) NOT NULL,
	[GoogleLink] [nvarchar](200) NOT NULL,
	[Pinterest_Link] [nvarchar](200) NOT NULL,
	[Instagram_Link] [nvarchar](200) NOT NULL,
	[ContactUs] [nvarchar](100) NULL,
	[BookStorePhone] [nvarchar](20) NULL,
 CONSTRAINT [PK_tblWebsiteSetting] PRIMARY KEY CLUSTERED 
(
	[WebSettingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[tblcountry_Locale](
	[countryid_Locale] [bigint] IDENTITY(1,1) NOT NULL,
	[countryname] [varchar](30) NULL,
	[countryid] [int] NULL,
	[LanguageID] [int] NULL,
 CONSTRAINT [PK_tblcountry_Locale] PRIMARY KEY CLUSTERED 
(
	[countryid_Locale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[tblcountry](
	[countryid] [int] IDENTITY(1,1) NOT NULL,
	[countryname] [varchar](100) NULL,
	[countryimage] [varchar](20) NULL,
	[ISOCode] [varchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__tblcount__D32342B4F65CDD7D] PRIMARY KEY CLUSTERED 
(
	[countryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblcountry] ADD  CONSTRAINT [DF_tblcountry_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


CREATE TABLE [dbo].[tbl_Video](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[VideoName] [varchar](100) NULL,
	[VideoPath] [varchar](500) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_tbl_Video] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_Video] ADD  CONSTRAINT [DF_tbl_Video_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

CREATE TABLE [dbo].[tbl_SonicStudioMenu](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Page] [varchar](100) NULL,
	[PageName] [varchar](100) NULL,
	[ParentID] [bigint] NULL,
 CONSTRAINT [PK_tbl_SonicStudioMenu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[tbl_SearchTags](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Tagname] [varchar](100) NULL,
	[Createdon] [datetime] NULL,
	[BookID] [varchar](50) NULL,
 CONSTRAINT [PK_tbl_SearchTags] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[tbl_RightAccessToUser](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NULL,
	[MenuID] [bigint] NULL,
	[AccTypeID] [bigint] NULL,
	[IsDisplay] [bit] NULL,
 CONSTRAINT [PK_tbl_RightAccessToUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_RightAccessToUser]  WITH CHECK ADD  CONSTRAINT [FK_tbl_RightAccessToUser_Registration] FOREIGN KEY([UserID])
REFERENCES [dbo].[Registration] ([RegistrationID])
GO

ALTER TABLE [dbo].[tbl_RightAccessToUser] CHECK CONSTRAINT [FK_tbl_RightAccessToUser_Registration]
GO

ALTER TABLE [dbo].[tbl_RightAccessToUser]  WITH CHECK ADD  CONSTRAINT [FK_tbl_RightAccessToUser_tbl_AccessType] FOREIGN KEY([AccTypeID])
REFERENCES [dbo].[tbl_AccessType] ([ID])
GO

ALTER TABLE [dbo].[tbl_RightAccessToUser] CHECK CONSTRAINT [FK_tbl_RightAccessToUser_tbl_AccessType]
GO

ALTER TABLE [dbo].[tbl_RightAccessToUser]  WITH CHECK ADD  CONSTRAINT [FK_tbl_RightAccessToUser_tbl_SonicStudioMenu] FOREIGN KEY([MenuID])
REFERENCES [dbo].[tbl_SonicStudioMenu] ([ID])
GO

ALTER TABLE [dbo].[tbl_RightAccessToUser] CHECK CONSTRAINT [FK_tbl_RightAccessToUser_tbl_SonicStudioMenu]
GO


CREATE TABLE [dbo].[tbl_MailLog](
	[RegistrationID] [bigint] NULL,
	[MailDate] [datetime] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbl_IphonePushNotification](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IphoneID] [varchar](max) NULL,
	[UserID] [bigint] NULL,
	[Department] [int] NULL,
 CONSTRAINT [PK_tbl_IphonePushNotification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[tbl_CustomerWishList](
	[WishID] [bigint] IDENTITY(1,1) NOT NULL,
	[AddedOn] [datetime] NULL,
	[CustomerID] [bigint] NULL,
	[Amount] [nchar](10) NULL,
	[BookID] [bigint] NULL,
	[IseBook] [bit] NULL,
	[IsPaperBook] [bit] NULL,
	[Qauntity] [int] NULL,
 CONSTRAINT [PK_tbl_CustomerWishList] PRIMARY KEY CLUSTERED 
(
	[WishID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbl_CustomerCart_backup23022015](
	[OrderID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](50) NULL,
	[OrderDate] [datetime] NULL,
	[CustomerID] [bigint] NULL,
	[SessionID] [varchar](50) NULL,
	[Amount] [nchar](10) NULL,
	[PaymentStatus] [bit] NULL,
	[BookID] [bigint] NULL,
	[IsPurchased] [bit] NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tbl_CustomerCart](
	[OrderID] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderNo] [varchar](50) NULL,
	[OrderDate] [datetime] NULL,
	[CustomerID] [bigint] NULL,
	[SessionID] [varchar](50) NULL,
	[Amount] [nchar](10) NULL,
	[PaymentStatus] [bit] NULL,
	[BookID] [bigint] NULL,
	[IsPurchased] [bit] NULL,
	[IseBook] [bit] NULL,
	[IsPaperBook] [bit] NULL,
	[Qauntity] [int] NULL,
 CONSTRAINT [PK_tbl_CustomerCart] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tbl_CustomerCart] ADD  DEFAULT ((0)) FOR [IsPurchased]
GO


CREATE TABLE [dbo].[tbl_city](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[countryid] [int] NULL,
	[city] [nvarchar](max) NULL,
	[isactive] [bit] NULL,
	[isdelete] [bit] NULL,
	[createdate] [datetime] NULL,
	[modifydate] [datetime] NULL,
 CONSTRAINT [PK_tbl_city] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[tbl_BookPurchase](
	[PurchaseID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NULL,
	[OrderID] [varchar](50) NULL,
	[IsApprove] [bit] NULL,
	[TransactionID] [varchar](50) NULL,
	[Amount] [varchar](50) NULL,
	[PurchaseDate] [datetime] NULL,
	[Domain] [varchar](100) NULL,
	[Status] [varchar](100) NULL,
	[AppCode] [varchar](100) NULL,
	[SessionID] [varchar](100) NULL,
	[BookID] [bigint] NULL,
	[IseBook] [bit] NULL,
	[IsPaperBook] [bit] NULL,
	[Qauntity] [int] NULL,
	[OrderStatus] [nvarchar](50) NULL,
	[DeliveryAddress] [nvarchar](20) NULL,
	[ShippingCharge] [nvarchar](20) NULL,
	[ShippingType] [nvarchar](200) NULL,
	[PaymentFrom] [nvarchar](100) NULL,
 CONSTRAINT [PK_tbl_MagazinePurchase] PRIMARY KEY CLUSTERED 
(
	[PurchaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[tbl_AccessType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[AccessType] [varchar](100) NULL,
 CONSTRAINT [PK_tbl_AccessType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[Special_SERVICE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From_Weight] [numeric](18, 2) NULL,
	[To_Weight] [numeric](18, 2) NULL,
	[Price] [numeric](18, 2) NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ShippingSetting](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SiteID] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[ShipperAccountNumber] [nvarchar](50) NULL,
	[BillingAccountNumber] [nvarchar](50) NULL,
	[DutyAccountNumber] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](100) NULL,
	[AddressLine1] [nvarchar](50) NULL,
	[AddressLine2] [nvarchar](50) NULL,
	[AddressLine3] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[CountryCode] [nvarchar](5) NULL,
	[CountryName] [nvarchar](50) NULL,
	[PersonName] [nvarchar](200) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[PhoneExtension] [nvarchar](10) NULL,
	[FaxNumber] [nvarchar](20) NULL,
	[Telex] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[ShipperID] [nvarchar](50) NULL,
	[ShipperCompanyName] [nvarchar](100) NULL,
	[ShipperRegisteredAccount] [nvarchar](50) NULL,
	[ShipperAddressLine1] [nvarchar](50) NULL,
	[ShipperAddressLine2] [nvarchar](50) NULL,
	[ShipperCity] [nvarchar](50) NULL,
	[ShipperPostalCode] [nvarchar](20) NULL,
	[ShipperCountryCode] [nvarchar](5) NULL,
	[ShipperCountryName] [nvarchar](50) NULL,
	[ShipperPersonName] [nvarchar](200) NULL,
	[ShipperPhoneNumber] [nvarchar](20) NULL,
	[ShipperPhoneExtension] [nvarchar](10) NULL,
	[ShipperFaxNumber] [nvarchar](20) NULL,
	[ShipperTelex] [nvarchar](20) NULL,
	[ShipperEmail] [nvarchar](50) NULL,
 CONSTRAINT [PK_ShippingSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ShippingCharge](
	[ShipperID] [bigint] IDENTITY(1,1) NOT NULL,
	[ShipperName] [nvarchar](50) NULL,
	[ShippingCharge] [nvarchar](100) NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ReviewRatting](
	[ReviewID] [bigint] IDENTITY(1,1) NOT NULL,
	[BookID] [int] NULL,
	[UserID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Summary] [nvarchar](500) NULL,
	[Review] [nvarchar](max) NULL,
	[Ratting] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Approve] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ReviewRatting] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ReviewRatting] ADD  DEFAULT ((0)) FOR [Approve]
GO

ALTER TABLE [dbo].[ReviewRatting] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO


CREATE TABLE [dbo].[Registration](
	[RegistrationID] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](200) NULL,
	[LastName] [nvarchar](200) NULL,
	[EmailAddress] [nvarchar](200) NULL,
	[Password] [nvarchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsNewsLetter] [bit] NULL,
	[UserType] [tinyint] NULL,
	[LoginDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[NewIssues] [bit] NULL,
	[Renewals] [bit] NULL,
	[AppUpdates] [bit] NULL,
	[FacebookEmail] [nvarchar](200) NULL,
	[ActivationID] [varchar](500) NULL,
	[UserName] [nvarchar](100) NULL,
	[GenderID] [bigint] NULL,
	[BirthdayDate] [nvarchar](200) NULL,
	[Countryid] [bigint] NULL,
	[LanguageID] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[AlternetEmailAddress] [nvarchar](200) NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[RegistrationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Region_Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RId] [int] NULL,
	[Country_Id] [int] NULL,
 CONSTRAINT [PK_Region_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Region](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Region] [nvarchar](200) NULL,
	[Created_date] [datetime] NULL,
	[Modify_date] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Payment](
	[PaymentID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NULL,
	[BookID] [bigint] NULL,
	[PaymentDate] [datetime] NULL,
	[Amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[OrderHistoryDetail](
	[OrderHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseID] [bigint] NULL,
	[OrderStatus] [nvarchar](50) NULL,
	[StatusDate] [datetime] NULL,
 CONSTRAINT [PK_OrderHistoryDetail] PRIMARY KEY CLUSTERED 
(
	[OrderHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[National_Shipping_Cost](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[city] [nvarchar](300) NULL,
	[shipping_cost] [decimal](18, 2) NULL,
	[IsActive] [bit] NULL,
	[createdate] [datetime] NULL,
	[modifydate] [datetime] NULL,
 CONSTRAINT [PK_National_Shipping_Cost] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Localization1](
	[LocalizationID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Value] [nvarchar](max) NULL,
	[LanguageID] [int] NULL,
 CONSTRAINT [PK_Localization1] PRIMARY KEY CLUSTERED 
(
	[LocalizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Localization](
	[LocalizationID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Value] [nvarchar](max) NULL,
	[LanguageID] [int] NULL,
 CONSTRAINT [PK_Localization] PRIMARY KEY CLUSTERED 
(
	[LocalizationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Language](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Language] [varchar](100) NULL,
	[CultureCode] [nvarchar](50) NULL,
 CONSTRAINT [PK_Language] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[International_SERVICE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From_Weight] [numeric](18, 2) NULL,
	[To_Weight] [numeric](18, 2) NULL,
	[Price] [numeric](18, 2) NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[International_currier_SERVICE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From_Weight] [numeric](18, 2) NULL,
	[Price] [numeric](18, 2) NULL,
	[Region_Id] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[ECONOMIC_SERVICE](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From_Weight] [numeric](18, 2) NULL,
	[To_Weight] [numeric](18, 2) NULL,
	[Price] [numeric](18, 2) NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[DHLQuoteSetting](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SiteID] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FromCountryCode] [nvarchar](50) NULL,
	[FromCodeCity] [int] NULL,
	[FromPostalcode] [nvarchar](50) NULL,
	[FromCity] [nvarchar](50) NULL,
	[PaymentCountryCode] [nvarchar](50) NULL,
	[ReadyTime] [nvarchar](50) NULL,
	[ReadyTimeGMTOffset] [nvarchar](50) NULL,
	[DimensionUnit] [nvarchar](50) NULL,
	[WeightUnit] [nvarchar](50) NULL,
	[PaymentAccountNumber] [nvarchar](50) NULL,
	[IsDutiable] [nvarchar](50) NULL,
	[NetworkTypeCode] [nvarchar](50) NULL,
	[GlobalProductCode] [nvarchar](50) NULL,
	[LocalProductCode] [nvarchar](50) NULL,
	[SpecialServiceType] [nvarchar](50) NULL,
	[DeclaredCurrency] [nvarchar](50) NULL,
	[DeclaredValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_DHLQuoteSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CMS_Locale_Bkp_1_7_15](
	[CMSID_Locale] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](50) NULL,
	[MetaDescription] [nvarchar](100) NULL,
	[MetaKeyWord] [nvarchar](100) NULL,
	[CMSID] [bigint] NULL,
	[LanguageID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CMS_Locale](
	[CMSID_Locale] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](50) NULL,
	[MetaDescription] [nvarchar](100) NULL,
	[MetaKeyWord] [nvarchar](100) NULL,
	[CMSID] [bigint] NULL,
	[LanguageID] [int] NULL,
 CONSTRAINT [PK_CMS_Locale] PRIMARY KEY CLUSTERED 
(
	[CMSID_Locale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[CMS_Local_Backup](
	[CMSID_Locale] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[MetaTitle] [nvarchar](50) NULL,
	[MetaDescription] [nvarchar](100) NULL,
	[MetaKeyWord] [nvarchar](100) NULL,
	[CMSID] [bigint] NULL,
	[LanguageID] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[cms_Bkp_1_7_15](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NULL
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[CMS](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_cms] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Category_Locale](
	[CategoryID_Locale] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryID] [bigint] NULL,
	[LanguageID] [int] NULL,
	[CategoryName] [nvarchar](200) NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category_Locale] PRIMARY KEY CLUSTERED 
(
	[CategoryID_Locale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Category](
	[CategoryID] [bigint] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NULL,
	[CImagePath] [nvarchar](200) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Bool_Locale](
	[BookID_Locale] [bigint] IDENTITY(1,1) NOT NULL,
	[BookID] [bigint] NULL,
	[LanguageID] [int] NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [nvarchar](max) NULL,
	[DescriptionImages] [nvarchar](100) NULL,
 CONSTRAINT [PK_Bool_Locale] PRIMARY KEY CLUSTERED 
(
	[BookID_Locale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[BookShippingDetails](
	[BookShippingID] [bigint] IDENTITY(1,1) NOT NULL,
	[BookID] [bigint] NULL,
	[CountryID] [bigint] NULL,
	[ShippingCharge] [nvarchar](10) NULL,
 CONSTRAINT [PK_BookShippingDetails] PRIMARY KEY CLUSTERED 
(
	[BookShippingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Bookmark](
	[BookmarkID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NULL,
	[BookID] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[PageNumber] [int] NULL,
 CONSTRAINT [PK_Bookmark] PRIMARY KEY CLUSTERED 
(
	[BookmarkID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Bookmark] ADD  CONSTRAINT [DF_Bookmark_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Bookmark]  WITH CHECK ADD  CONSTRAINT [FK_Bookmark_Registration] FOREIGN KEY([UserID])
REFERENCES [dbo].[Registration] ([RegistrationID])
GO

ALTER TABLE [dbo].[Bookmark] CHECK CONSTRAINT [FK_Bookmark_Registration]
GO


CREATE TABLE [dbo].[BookDeliveryAddressDetail](
	[BookDeliveryAddID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NULL,
	[IsDefault] [bit] NULL,
	[Name] [nvarchar](100) NULL,
	[StreetAddress] [nvarchar](250) NULL,
	[Landmark] [nvarchar](100) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Pincode] [nvarchar](10) NULL,
	[PhoneNumber] [nvarchar](15) NULL,
 CONSTRAINT [PK_BookDeliveryAddressDetail] PRIMARY KEY CLUSTERED 
(
	[BookDeliveryAddID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[Book_Image](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BookID] [bigint] NULL,
	[ImagePath] [nvarchar](max) NULL,
	[IsTitled] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Magazine_Issue_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Book_Desc_Image](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[BookID] [bigint] NULL,
	[ImagePath] [nvarchar](max) NULL,
	[IsTitled] [bit] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_MagazineIssueDesc_Image] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Book](
	[BookID] [bigint] IDENTITY(1,1) NOT NULL,
	[CountryID] [int] NULL,
	[Title] [nvarchar](200) NULL,
	[Price] [float] NULL,
	[Description] [varchar](max) NULL,
	[Language] [varchar](500) NULL,
	[IsActive] [bit] NULL,
	[IsFree] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[Published] [bit] NULL,
	[IsFeatured] [bit] NULL,
	[CategoryID] [bigint] NULL,
	[LanguageID] [bigint] NULL,
	[DiscountPrice] [float] NULL,
	[PublishDate] [datetime] NULL,
	[IsSpecial] [bit] NULL,
	[ExplorerPdfStartNo] [int] NULL,
	[ExplorerPdfEndNo] [int] NULL,
	[DescriptionImages] [nvarchar](100) NULL,
	[Autoher] [varchar](30) NULL,
	[FinalPrice] [varchar](50) NULL,
	[IsDelete] [bit] NULL,
	[OrderIndex] [bigint] NULL,
	[SpecialOffer] [bit] NULL,
	[SpecialOfferStart] [datetime] NULL,
	[SpecialOfferEnd] [datetime] NULL,
	[DealerEmail] [nvarchar](50) NULL,
	[PaperBookPrice] [float] NULL,
	[PaperBookDiscount] [float] NULL,
	[PaperBookFinalPrice] [float] NULL,
	[IseBook] [bit] NULL,
	[IsPaperBook] [bit] NULL,
	[IsFreePaper] [bit] NULL,
	[Quantity] [int] NULL,
	[PartnerID] [int] NULL,
	[Weight] [nvarchar](10) NULL,
	[DimWeight] [nvarchar](10) NULL,
	[Width] [nvarchar](10) NULL,
	[Height] [nvarchar](10) NULL,
	[Depth] [nvarchar](10) NULL,
 CONSTRAINT [PK_Magazine_Issue] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Book] ADD  CONSTRAINT [DF_Magazine_Issue_IsSpecial]  DEFAULT ((0)) FOR [IsSpecial]
GO

CREATE TABLE [dbo].[Blog_Local](
	[BlogLocalID] [bigint] IDENTITY(1,1) NOT NULL,
	[BlogID] [bigint] NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [varchar](max) NULL,
	[LanguageID] [int] NULL,
 CONSTRAINT [PK_Blog_Local] PRIMARY KEY CLUSTERED 
(
	[BlogLocalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Blog](
	[BolgID] [bigint] IDENTITY(1,1) NOT NULL,
	[BlogImage] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[BolgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[bannermaster_Locale](
	[ID_Locale] [bigint] IDENTITY(1,1) NOT NULL,
	[BannerID] [bigint] NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[LanguageID] [int] NULL,
 CONSTRAINT [PK_bannermaster_Locale] PRIMARY KEY CLUSTERED 
(
	[ID_Locale] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[bannermaster](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NULL,
	[ImagePath] [varchar](500) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_bannermaster] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


CREATE TABLE [dbo].[Advertisements_Local](
	[AdvertisementLocalID] [bigint] IDENTITY(1,1) NOT NULL,
	[AdvertisementID] [bigint] NULL,
	[Title] [nvarchar](200) NULL,
	[Description] [varchar](max) NULL,
	[LanguageID] [int] NULL,
 CONSTRAINT [PK_Advertisements_Local] PRIMARY KEY CLUSTERED 
(
	[AdvertisementLocalID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Advertisements](
	[AdvertisementID] [bigint] IDENTITY(1,1) NOT NULL,
	[AdvertisementImage] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[LinkUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Advertisements] PRIMARY KEY CLUSTERED 
(
	[AdvertisementID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

