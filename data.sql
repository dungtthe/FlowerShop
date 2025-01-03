USE [master]
GO
/****** Object:  Database [FlowerShopDB]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE DATABASE [FlowerShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FlowerShopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FlowerShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FlowerShopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\FlowerShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FlowerShopDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FlowerShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FlowerShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FlowerShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FlowerShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FlowerShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FlowerShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FlowerShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FlowerShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FlowerShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FlowerShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FlowerShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FlowerShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FlowerShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FlowerShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FlowerShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FlowerShopDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FlowerShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FlowerShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FlowerShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FlowerShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FlowerShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FlowerShopDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FlowerShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FlowerShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [FlowerShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [FlowerShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FlowerShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FlowerShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FlowerShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FlowerShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FlowerShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FlowerShopDB', N'ON'
GO
ALTER DATABASE [FlowerShopDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [FlowerShopDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FlowerShopDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Addresses]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[AppUserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetails]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetails](
	[CartId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_CartDetails] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carts]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Carts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[ParentCategoryId] [int] NULL,
	[IsCategorySell] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conversations]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [nvarchar](450) NOT NULL,
	[StaffId] [nvarchar](450) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Conversations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedBackResponses]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedBackResponses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FeedBackId] [int] NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[SendingTime] [datetime2](7) NOT NULL,
	[IsCustomer] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_FeedBackResponses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FeedBacks]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeedBacks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaleInvoiceDetailId] [int] NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[SendingTime] [datetime2](7) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_FeedBacks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConversationId] [int] NOT NULL,
	[SendingTime] [datetime2](7) NOT NULL,
	[IsCustomer] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Packaging]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packaging](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Packaging] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParameterConfigurations]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParameterConfigurations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AllowedFeedbackDay] [int] NOT NULL,
	[ShippingCostPerKilometer] [int] NOT NULL,
 CONSTRAINT [PK_ParameterConfigurations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethods]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_PaymentMethods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTokens]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](20) NOT NULL,
	[TimeCreate] [datetime2](7) NOT NULL,
	[SaleInvoiceId] [int] NOT NULL,
 CONSTRAINT [PK_PaymentTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[CategoryId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductItems]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ImportPrice] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Images] [nvarchar](1500) NULL,
	[Description] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_ProductItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPrices]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPrices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Priority] [tinyint] NOT NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_ProductPrices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductProductItems]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductProductItems](
	[ProductId] [int] NOT NULL,
	[ProductItemId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_ProductProductItems] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[ProductItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Quantity] [int] NOT NULL,
	[Images] [nvarchar](1500) NULL,
	[PackagingId] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleClaims]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleInvoiceDetails]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleInvoiceDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SaleInvoiceId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_SaleInvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SaleInvoices]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SaleInvoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDay] [datetime2](7) NOT NULL,
	[CustomerId] [nvarchar](450) NOT NULL,
	[PaymentMethodId] [int] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Note] [nvarchar](2000) NULL,
	[IsPaid] [bit] NOT NULL,
	[ShippingCost] [int] NOT NULL,
	[NameRecipient] [nvarchar](255) NOT NULL,
	[PhoneNumberRecipient] [nvarchar](30) NOT NULL,
	[DeliveryAddress] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_SaleInvoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierInvoiceDetails]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierInvoiceDetails](
	[SupplierInvoiceId] [int] NOT NULL,
	[ProductItemId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [int] NOT NULL,
 CONSTRAINT [PK_SupplierInvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[SupplierInvoiceId] ASC,
	[ProductItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierInvoices]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierInvoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDay] [datetime2](7) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[Note] [nvarchar](2000) NULL,
	[Status] [tinyint] NOT NULL,
	[SupplierInvoiceTokenId] [int] NULL,
 CONSTRAINT [PK_SupplierInvoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierInvoiceTokens]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierInvoiceTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TokenEmail] [nvarchar](1000) NOT NULL,
	[TokenAccept] [nvarchar](1006) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[SupplierInvoiceId] [int] NOT NULL,
 CONSTRAINT [PK_SupplierInvoiceTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [nvarchar](500) NOT NULL,
	[TaxCode] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Type] [int] NOT NULL,
	[Images] [nvarchar](1500) NULL,
	[Description] [nvarchar](max) NULL,
	[Industry] [nvarchar](300) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaims]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[BirthDay] [datetime2](7) NULL,
	[Images] [nvarchar](1500) NULL,
	[IsLock] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CartId] [int] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTokens]    Script Date: 12/27/2024 1:43:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241226065749_FinalDBVer1', N'6.0.35')
GO
INSERT [dbo].[CartDetails] ([CartId], [ProductId], [Quantity], [IsDeleted]) VALUES (2, 2, 1, 1)
INSERT [dbo].[CartDetails] ([CartId], [ProductId], [Quantity], [IsDeleted]) VALUES (2, 3, 1, 1)
INSERT [dbo].[CartDetails] ([CartId], [ProductId], [Quantity], [IsDeleted]) VALUES (2, 5, 1, 1)
INSERT [dbo].[CartDetails] ([CartId], [ProductId], [Quantity], [IsDeleted]) VALUES (2, 9, 1, 1)
INSERT [dbo].[CartDetails] ([CartId], [ProductId], [Quantity], [IsDeleted]) VALUES (2, 10, 1, 0)
INSERT [dbo].[CartDetails] ([CartId], [ProductId], [Quantity], [IsDeleted]) VALUES (2, 20, 1, 1)
INSERT [dbo].[CartDetails] ([CartId], [ProductId], [Quantity], [IsDeleted]) VALUES (2, 21, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Carts] ON 

INSERT [dbo].[Carts] ([Id]) VALUES (1)
INSERT [dbo].[Carts] ([Id]) VALUES (2)
SET IDENTITY_INSERT [dbo].[Carts] OFF
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (1, N'CHỦ ĐỀ', NULL, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (2, N'ĐỐI TƯỢNG', NULL, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (3, N'KIỂU DÁNG', NULL, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (4, N'HOA TƯƠI', NULL, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (5, N'MÀU SẮC', NULL, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (6, N'BỘ SƯU TẬP', NULL, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (7, N'QUÀ TẶNG KÈM', NULL, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (8, N'Hoa Sinh Nhật', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (9, N'Hoa Khai Trương', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (10, N'Hoa Chúc Mừng', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (11, N'Hoa Chia Buồn', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (12, N'Hoa Chúc Sức Khỏe', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (13, N'Hoa Tình Yêu', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (14, N'Hoa Cảm Ơn', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (15, N'Hoa Mừng Tốt Nghiệp', 1, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (16, N'Hoa Tặng Người Yêu', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (17, N'Hoa Tặng Bạn Bè', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (18, N'Hoa Tặng Vợ', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (19, N'Hoa Tặng Chồng', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (20, N'Hoa Tặng Mẹ', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (21, N'Hoa Tặng Trẻ Em', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (22, N'Hoa Tặng Cho Nữ', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (23, N'Hoa Tặng Cho Nam', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (24, N'Hoa Tặng Sếp', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (25, N'Hoa Tặng Đồng Nghiệp', 2, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (26, N'Bó Hoa Tươi', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (27, N'Giỏ Hoa Tươi', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (28, N'Hộp Hoa Tươi', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (29, N'Bình Hoa Tươi', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (30, N'Hoa Thả Bình', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (31, N'Lẵng Hoa Khai Trương', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (32, N'Lẵng Hoa Chia Buồn', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (33, N'Hộp Mica', 3, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (34, N'Only Rose', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (35, N'Hoa Hồng', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (36, N'Hoa Hướng Dương', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (37, N'Hoa Đồng Tiền', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (38, N'Lan Hồ Điệp', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (39, N'Cẩm Chướng', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (40, N'Hoa Cát Tường', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (41, N'Hoa Ly', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (42, N'Baby Flower', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (43, N'Hoa Cúc', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (44, N'Sen Đá', 4, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (45, N'Màu Trắng', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (46, N'Màu Đỏ', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (47, N'Màu Hồng', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (48, N'Màu Cam', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (49, N'Màu Tím', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (50, N'Màu Vàng', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (51, N'Màu Xanh (Xanh Dương, Xanh Lá)', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (52, N'Kết Hợp Màu', 5, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (53, N'Hoa Cao Cấp', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (54, N'Hoa Sinh Viên', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (55, N'Mẫu Hoa Mới', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (56, N'Khuyến Mãi', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (57, N'Ngày Phụ Nữ Việt Nam (20/10)', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (58, N'Ngày Nhà Giáo', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (59, N'Giáng Sinh', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (60, N'Hoa Tết', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (61, N'Hoa Sự Kiện', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (62, N'Hoa Tình Yêu', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (63, N'Ngày Quốc Tế Phụ Nữ', 6, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (64, N'Bánh Kem', 7, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (65, N'Chocolate', 7, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (66, N'Trái Cây', 7, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (67, N'Gấu Bông', 7, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (68, N'Nến Thơm', 7, 1, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (69, N'HOA HỒNG', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (70, N'HOA LAN', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (71, N'HOA HƯỚNG DƯƠNG', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (72, N'HOA CÚC', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (73, N'BÁNH KEM', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (74, N'CHOCOLATE', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (75, N'GẤU BÔNG', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (76, N'TRÁI CÂY', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (77, N'NẾN THƠM', NULL, 0, 0)
INSERT [dbo].[Categories] ([Id], [Name], [ParentCategoryId], [IsCategorySell], [IsDelete]) VALUES (78, N'KHÔNG XÁC ĐỊNH', NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[FeedBacks] ON 

INSERT [dbo].[FeedBacks] ([Id], [SaleInvoiceDetailId], [Content], [SendingTime], [IsDelete]) VALUES (1, 11, N'Great product! Will buy again.', CAST(N'2024-12-25T10:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[FeedBacks] ([Id], [SaleInvoiceDetailId], [Content], [SendingTime], [IsDelete]) VALUES (4, 18, N'Great product! Will buy again.', CAST(N'2024-12-25T10:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[FeedBacks] ([Id], [SaleInvoiceDetailId], [Content], [SendingTime], [IsDelete]) VALUES (5, 19, N'Very satisfied with the quality and delivery speed.', CAST(N'2024-12-26T14:30:00.0000000' AS DateTime2), 0)
INSERT [dbo].[FeedBacks] ([Id], [SaleInvoiceDetailId], [Content], [SendingTime], [IsDelete]) VALUES (6, 20, N'The product arrived damaged, very disappointed.', CAST(N'2024-12-27T09:15:00.0000000' AS DateTime2), 0)
INSERT [dbo].[FeedBacks] ([Id], [SaleInvoiceDetailId], [Content], [SendingTime], [IsDelete]) VALUES (7, 21, N'Customer service was very helpful, but the delivery was late.', CAST(N'2024-12-28T16:45:00.0000000' AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[FeedBacks] OFF
GO
SET IDENTITY_INSERT [dbo].[Packaging] ON 

INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (1, N'Bó', N'Hoa được bó đẹp với giấy gói cao cấp, phù hợp tặng sinh nhật, kỷ niệm', 0)
INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (2, N'Giỏ', N'Hoa được cắm trong giỏ mây, thích hợp cho các dịp khai trương, tân gia', 0)
INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (3, N'Bình', N'Hoa được cắm trong bình thủy tinh, phù hợp để bàn, trang trí nhà', 0)
INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (4, N'Hộp', N'Hoa được xếp trong hộp thiết kế sang trọng, là món quà tặng độc đáo', 0)
INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (5, N'Lẵng', N'Hoa được cắm trong lẵng lớn, thích hợp cho các dịp chúc mừng, sự kiện', 0)
INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (6, N'Hộp Gỗ', N'Hộp gỗ tự nhiên được thiết kế tinh tế, độc đáo', 0)
INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (7, N'Giỏ Mây Đan', N'Giỏ mây thủ công với kiểu đan độc đáo', 0)
INSERT [dbo].[Packaging] ([Id], [Name], [Description], [IsDelete]) VALUES (8, N'Hộp Nhung', N'Hộp nhung sang trọng, phù hợp cho hoa cao cấp', 0)
SET IDENTITY_INSERT [dbo].[Packaging] OFF
GO
SET IDENTITY_INSERT [dbo].[ParameterConfigurations] ON 

INSERT [dbo].[ParameterConfigurations] ([Id], [AllowedFeedbackDay], [ShippingCostPerKilometer]) VALUES (1, 7, 5000)
SET IDENTITY_INSERT [dbo].[ParameterConfigurations] OFF
GO
SET IDENTITY_INSERT [dbo].[PaymentMethods] ON 

INSERT [dbo].[PaymentMethods] ([Id], [Name], [Description], [Price], [Status], [IsDelete]) VALUES (1, N'Thanh toán khi nhận hàng', N'Thanh toán trực tiếp khi nhận hàng', CAST(0.00 AS Decimal(18, 2)), 1, 0)
INSERT [dbo].[PaymentMethods] ([Id], [Name], [Description], [Price], [Status], [IsDelete]) VALUES (2, N'Thanh toán online', N'Thanh toán qua thẻ hoặc ví điện tử', CAST(0.00 AS Decimal(18, 2)), 1, 0)
SET IDENTITY_INSERT [dbo].[PaymentMethods] OFF
GO
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (8, 2, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (8, 5, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (9, 4, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (9, 8, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (14, 1, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (14, 3, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (16, 1, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (25, 9, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (27, 1, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (27, 2, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (27, 3, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (27, 7, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (28, 4, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (28, 5, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (29, 6, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (30, 1, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (30, 3, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (30, 5, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (30, 6, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (30, 9, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (31, 2, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (31, 5, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (31, 7, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (33, 4, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (33, 8, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (37, 9, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (39, 1, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (45, 2, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (45, 5, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (45, 8, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (46, 1, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (46, 3, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (46, 4, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (46, 6, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (46, 8, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (47, 2, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (47, 7, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (48, 7, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (56, 4, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (64, 14, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (64, 15, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (64, 16, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (65, 10, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (65, 11, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (65, 13, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (65, 16, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (66, 17, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (66, 18, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (66, 19, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (66, 20, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (67, 11, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (67, 12, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (67, 15, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (67, 16, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (68, 21, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (68, 22, 0)
INSERT [dbo].[ProductCategories] ([CategoryId], [ProductId], [IsDelete]) VALUES (68, 23, 0)
GO
SET IDENTITY_INSERT [dbo].[ProductItems] ON 

INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (1, N'Hoa Hồng Đỏ', 15000, 69, 100, N'["no_img.png"]', N'Hoa hồng đỏ tươi - Biểu tượng của tình yêu', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (2, N'Hoa Hồng Trắng', 20000, 69, 100, N'["no_img.png"]', N'Hoa hồng trắng tinh khôi - Tượng trưng cho sự thuần khiết', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (3, N'Hoa Hồng Cam', 18000, 69, 100, N'["no_img.png"]', N'Hoa hồng cam rực rỡ - Màu của nhiệt huyết', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (4, N'Hoa Hồng Phấn', 20000, 69, 100, N'["no_img.png"]', N'Hoa hồng phấn nhẹ nhàng - Vẻ đẹp dịu dàng', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (5, N'Hoa Hồng Vàng', 17000, 69, 100, N'["no_img.png"]', N'Hoa hồng vàng tươi sáng - Màu của sự thịnh vượng', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (6, N'Hoa Hồng Ecuador', 35000, 69, 100, N'["no_img.png"]', N'Hoa hồng Ecuador size đại - Sang trọng và độc đáo', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (7, N'Hoa Hồng Sapphire', 30000, 69, 50, N'["no_img.png"]', N'Hoa hồng xanh sapphire - Quý hiếm và đặc biệt', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (8, N'Hoa Hồng Tím Pastel', 22000, 69, 100, N'["no_img.png"]', N'Hoa hồng tím nhạt - Vẻ đẹp lãng mạn', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (9, N'Hoa Hồng Juliet', 28000, 69, 80, N'["no_img.png"]', N'Hoa hồng juliet - Màu cam đào quyến rũ', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (10, N'Hoa Hồng Ohara', 25000, 69, 80, N'["no_img.png"]', N'Hoa hồng ohara garden - Phong cách cổ điển', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (11, N'Hồng Trà', 16000, 69, 100, N'["no_img.png"]', N'Hoa hồng nâu đỏ - Hương thơm đặc trưng', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (12, N'Hồng Cabbage', 23000, 69, 70, N'["no_img.png"]', N'Hoa hồng bắp cải - Cánh hoa xếp lớp độc đáo', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (13, N'Lan Hồ Điệp Trắng', 100000, 70, 50, N'["no_img.png"]', N'Lan hồ điệp trắng - Sang trọng và quý phái', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (14, N'Lan Hồ Điệp Tím', 120000, 70, 50, N'["no_img.png"]', N'Lan hồ điệp tím - Màu sắc hoàng gia', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (15, N'Lan Hồ Điệp Hồng', 110000, 70, 50, N'["no_img.png"]', N'Lan hồ điệp hồng - Nhẹ nhàng và tinh tế', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (16, N'Lan Mokara Đỏ', 80000, 70, 50, N'["no_img.png"]', N'Lan mokara đỏ - Màu sắc nổi bật', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (17, N'Lan Mokara Vàng', 85000, 70, 50, N'["no_img.png"]', N'Lan mokara vàng - Tươi sáng', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (18, N'Lan Vũ Nữ', 150000, 70, 30, N'["no_img.png"]', N'Lan vũ nữ - Quý phái và kiêu sa', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (19, N'Lan Dendro Berry', 90000, 70, 40, N'["no_img.png"]', N'Lan dendro berry - Màu tím mộng mơ', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (20, N'Lan Phi Điệp Tím', 200000, 70, 20, N'["no_img.png"]', N'Lan phi điệp tím - Đặc biệt quý hiếm', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (21, N'Lan Ngọc Điểm', 180000, 70, 25, N'["no_img.png"]', N'Lan ngọc điểm - Nhỏ xinh và thanh tao', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (22, N'Lan Cattleya', 160000, 70, 30, N'["no_img.png"]', N'Lan cattleya - Màu sắc rực rỡ', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (23, N'Hướng Dương Vàng Lớn', 25000, 71, 100, N'["no_img.png"]', N'Hoa hướng dương vàng size lớn - Rực rỡ', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (24, N'Hướng Dương Vàng Nhỏ', 15000, 71, 150, N'["no_img.png"]', N'Hoa hướng dương vàng size nhỏ - Xinh xắn', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (25, N'Hướng Dương Cam', 28000, 71, 80, N'["no_img.png"]', N'Hoa hướng dương màu cam - Độc đáo', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (26, N'Hướng Dương Đỏ', 30000, 71, 70, N'["no_img.png"]', N'Hoa hướng dương đỏ - Hiếm có', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (27, N'Hướng Dương Teddy', 35000, 71, 60, N'["no_img.png"]', N'Hoa hướng dương teddy - Nhỏ xinh đáng yêu', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (28, N'Cúc Họa Mi', 12000, 72, 200, N'["no_img.png"]', N'Cúc họa mi - Tinh khôi và thanh tao', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (29, N'Cúc Mẫu Đơn', 18000, 72, 150, N'["no_img.png"]', N'Cúc mẫu đơn - Sang trọng và quý phái', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (30, N'Cúc Ping Pong', 10000, 72, 200, N'["no_img.png"]', N'Cúc ping pong - Nhỏ xinh tròn trĩnh', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (31, N'Cúc Đại Đóa', 20000, 72, 100, N'["no_img.png"]', N'Cúc đại đóa - To và rực rỡ', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (32, N'Cúc Calimero', 15000, 72, 150, N'["no_img.png"]', N'Cúc calimero - Nhỏ nhắn đáng yêu', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (33, N'Cúc Rossi', 16000, 72, 120, N'["no_img.png"]', N'Cúc rossi - Màu hồng phấn nhẹ nhàng', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (34, N'Cúc Vàng', 12000, 72, 200, N'["no_img.png"]', N'Cúc vàng - Truyền thống và phổ biến', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (35, N'Bánh Kem Chocolate', 150000, 73, 50, N'["no_img.png"]', N'Bánh kem socola cao cấp - 20cm', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (36, N'Bánh Red Velvet', 180000, 73, 30, N'["no_img.png"]', N'Bánh red velvet thơm ngon - 20cm', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (37, N'Bánh Kem Vanilla', 150000, 73, 50, N'["no_img.png"]', N'Bánh kem vanilla truyền thống - 20cm', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (38, N'Ferrero Rocher 16 viên', 180000, 74, 100, N'["no_img.png"]', N'Hộp chocolate Ferrero Rocher 16 viên', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (39, N'Chocolate Godiva', 350000, 74, 50, N'["no_img.png"]', N'Hộp chocolate Godiva cao cấp', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (40, N'Chocolate Lindor', 250000, 74, 80, N'["no_img.png"]', N'Hộp chocolate Lindor thượng hạng', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (41, N'Gấu Teddy 30cm', 120000, 75, 100, N'["no_img.png"]', N'Gấu bông teddy màu nâu - 30cm', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (42, N'Gấu Love 25cm', 100000, 75, 100, N'["no_img.png"]', N'Gấu bông ôm tim Love you - 25cm', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (43, N'Thỏ Bông 35cm', 150000, 75, 80, N'["no_img.png"]', N'Thỏ bông dễ thương - 35cm', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (44, N'Táo Đỏ', 120000, 76, 50, N'["no_img.png"]', N'Táo đỏ nhập khẩu tươi ngon', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (45, N'Cam Vàng', 150000, 76, 40, N'["no_img.png"]', N'Cam vàng thơm ngon, cung cấp vitamin C', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (46, N'Nho Đen', 200000, 76, 30, N'["no_img.png"]', N'Nho đen không hạt nhập khẩu', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (47, N'Dâu Tây', 250000, 76, 20, N'["no_img.png"]', N'Dâu tây tươi Đà Lạt, đóng hộp đẹp mắt', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (48, N'Nến Thơm Lavender', 120000, 77, 50, N'["no_img.png"]', N'Nến thơm hương Lavender thư giãn', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (49, N'Nến Thơm Vani', 100000, 77, 40, N'["no_img.png"]', N'Nến thơm hương Vani ngọt ngào', 0)
INSERT [dbo].[ProductItems] ([Id], [Name], [ImportPrice], [CategoryId], [Quantity], [Images], [Description], [IsDelete]) VALUES (50, N'Nến Thơm Cam', 110000, 77, 20, N'["no_img.png"]', N'Nến thơm hương cam tươi mát', 0)
SET IDENTITY_INSERT [dbo].[ProductItems] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductPrices] ON 

INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (1, 1, CAST(750000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (2, 2, CAST(650000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (3, 3, CAST(1200000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (4, 4, CAST(2500000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (5, 5, CAST(850000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (6, 6, CAST(1500000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (7, 7, CAST(350000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (8, 8, CAST(2800000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (9, 9, CAST(750000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (10, 10, CAST(799000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (11, 11, CAST(449000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (12, 12, CAST(399000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (13, 13, CAST(899000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (14, 14, CAST(499000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (15, 15, CAST(599000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (16, 16, CAST(999000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (17, 17, CAST(150000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (18, 18, CAST(180000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (19, 19, CAST(200000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (20, 20, CAST(250000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (21, 21, CAST(120000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (22, 22, CAST(100000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
INSERT [dbo].[ProductPrices] ([Id], [ProductId], [Price], [Priority], [StartDate], [EndDate], [IsDelete]) VALUES (23, 23, CAST(110000.00 AS Decimal(18, 2)), 1, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[ProductPrices] OFF
GO
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (1, 6, 30, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (2, 4, 15, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (2, 25, 5, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (2, 31, 10, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (3, 2, 10, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (3, 6, 40, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (3, 8, 5, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (4, 6, 24, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (4, 13, 3, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (4, 14, 3, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (5, 4, 20, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (5, 25, 3, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (5, 31, 10, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (6, 2, 12, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (6, 6, 36, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (7, 25, 3, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (7, 28, 10, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (8, 6, 50, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (8, 11, 5, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (8, 13, 5, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (9, 4, 12, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (9, 31, 10, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (9, 33, 5, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (10, 39, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (10, 40, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (11, 38, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (11, 43, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (12, 41, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (12, 42, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (13, 38, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (13, 39, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (13, 40, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (14, 35, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (14, 37, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (15, 36, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (15, 42, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (16, 35, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (16, 39, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (16, 41, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (17, 44, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (18, 45, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (19, 46, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (20, 47, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (21, 48, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (22, 49, 1, 0)
INSERT [dbo].[ProductProductItems] ([ProductId], [ProductItemId], [Quantity], [IsDelete]) VALUES (23, 50, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (1, N'Love Of Rose', N'Bó hoa hồng đỏ Ecuador 30 bông - Biểu tượng của tình yêu mãnh liệt', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11590_luxury-bloom.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11590_luxury-bloom-2.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11590_luxury-bloom-3.jpg",
  "no_img.png"]', 1, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (2, N'Happy Birthday Bloom', N'Bó hoa sinh nhật kết hợp hồng và hướng dương - Tươi vui và rực rỡ', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/12479_rising-sun.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/12479_rising-sun-2.jpg",
  "no_img.png"]', 1, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (3, N'Premium Love', N'Bó hoa hồng cao cấp 40 bông kết hợp các loại hoa nhập khẩu', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/13308_premium-blend.jpg",
  "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/13308_premium-blend-2.jpg",
  "no_img.png"]', 1, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (4, N'Thành Công', N'Giỏ hoa khai trương sang trọng kết hợp hồng và lan hồ điệp', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13398_success-2.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13398_success-3.jpg",
 "no_img.png"]', 2, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (5, N'Happy Day', N'Giỏ hoa sinh nhật tươi tắn với hồng, cúc và hướng dương', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13391_happy-day.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/gio-hoa/13391_happy-day-2.jpg",
 "no_img.png"]', 2, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (6, N'Luxury Box', N'Hộp hoa hồng premium với thiết kế sang trọng', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/hop-hoa/13397_elegance-bloom.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/hop-hoa/13397_elegance-bloom-2.jpg",
 "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (7, N'Sweet Day', N'Bó hoa nhỏ xinh với hướng dương và cúc họa mi, phù hợp sinh viên', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11569_sunshine.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/bo-hoa/11569_sunshine-2.jpg",
 "no_img.png"]', 1, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (8, N'Congratulations', N'Lẵng hoa chúc mừng khai trương hoành tráng với lan và hồng', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/lang-hoa/13372_successful-2.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/lang-hoa/13372_successful-3.jpg",
 "no_img.png"]', 5, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (9, N'Office Charm', N'Bình hoa tươi trang trí văn phòng với cúc và hồng', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/binh-hoa/13266_blushing-room.jpg",
 "https://hoayeuthuong.com/hinh-hoa-tuoi/binh-hoa/13266_blushing-room-2.jpg",
 "no_img.png"]', 3, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (10, N'Premium Chocolate Collection', N'Bộ sưu tập chocolate cao cấp Godiva và Lindor', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/so-co-la-d-art/12752_chocolate-truffle-nau-9.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (11, N'Bunny Sweet Box', N'Set quà thỏ bông xinh xắn và Ferrero Rocher', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/gau-bong/13043_bup-be-non-phu-thuy.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (12, N'Double Love Bears', N'Cặp gấu Teddy và Love siêu dễ thương', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/gau-bong/13273_mini-teddy-bear.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (13, N'Chocolate Paradise', N'Combo 3 loại chocolate cao cấp nhất', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/so-co-la-d-art/12893_chocolatle-big-box-36.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (14, N'Sweet Cake Duo', N'Bộ đôi bánh kem chocolate và vanilla', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/banh-kem-brodard/6862_banh-kem-sua-tuoi-20cm-m13.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (15, N'Love Bear Cake Set', N'Set quà gấu Love và bánh Red Velvet', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/banh-kem-brodard/15518_banh-passio-cheese-mousse-16cm.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (16, N'Cake White', N'Combo bánh kem, chocolate và gấu bông cao cấp', 50, N'["https://hoayeuthuong.com/hinh-hoa-tuoi/thumb/banh-kem-brodard/15516_banh-kem-trai-cay-sua-tuoi-20cm-m14.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (17, N'Hộp Táo Đỏ', N'Hộp quà trái cây với táo đỏ nhập khẩu tươi ngon', 50, N'["https://bizweb.dktcdn.net/100/107/356/products/ho-p-ta-o-do-ha-n-quo-c-samsung-1k.jpg?v=1598241579597", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (18, N'Hộp Cam Vàng', N'Hộp quà trái cây với cam vàng thơm ngon, bổ dưỡng', 50, N'["https://www.lottemart.vn/media/catalog/product/cache/0x0/0/4/0400229220003-3.jpg.webp", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (19, N'Hộp Nho Đen', N'Hộp quà trái cây với nho đen không hạt nhập khẩu', 50, N'["https://www.lottemart.vn/media/catalog/product/cache/0x0/0/7/0770795264146-1.jpg.webp", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (20, N'Hộp Dâu Tây', N'Hộp quà trái cây với dâu tây tươi từ Đà Lạt', 50, N'["https://dalatfarm.net/wp-content/uploads/2022/01/dau-nhat-mix-nhieu-loai.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (21, N'Nến Lavender', N'Nến thơm hương Lavender thư giãn, cao cấp', 50, N'["https://natudar.com/wp-content/uploads/2024/08/nen-thom-oai-huong-lavender-img-1.jpg", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (22, N'Nến Vani', N'Nến thơm hương Vani ngọt ngào, lãng mạn', 50, N'["https://file.hstatic.net/200000455983/file/huong-vani_5ce16635b8d7422ea5e6666b64f6ff5b_grande.png", "no_img.png"]', 4, 0)
INSERT [dbo].[Products] ([Id], [Title], [Description], [Quantity], [Images], [PackagingId], [IsDelete]) VALUES (23, N'Nến Cam', N'Nến thơm hương cam tươi mát, dễ chịu', 50, N'["https://bizweb.dktcdn.net/100/419/636/products/cam-que.jpg?v=1712983395237", "no_img.png"]', 4, 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2e2c4402-6c7e-41d1-a21c-47a32537b091', N'Staff', N'STAFF', N'fa6cb6ac-aa41-4041-b5a1-89ed90469b34')
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'44b092a7-08ad-44d5-895b-730d4e953bea', N'Admin', N'ADMIN', N'6c5e895c-4e09-4bec-8c12-a55541db507a')
INSERT [dbo].[Roles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'70f49d11-006f-49a4-95c7-c8ac053d74ad', N'Customer', N'CUSTOMER', N'80567731-67ff-4beb-9edd-931aaa1f9b57')
GO
SET IDENTITY_INSERT [dbo].[SaleInvoiceDetails] ON 

INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (11, 1, 2, 1, 650000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (12, 2, 2, 1, 650000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (13, 3, 2, 1, 650000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (14, 4, 2, 1, 650000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (15, 4, 3, 1, 1200000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (16, 5, 5, 1, 850000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (17, 6, 2, 1, 650000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (18, 7, 9, 1, 750000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (19, 7, 10, 1, 799000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (20, 7, 20, 1, 250000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (21, 7, 21, 1, 120000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (22, 8, 3, 1, 1200000, 0)
INSERT [dbo].[SaleInvoiceDetails] ([Id], [SaleInvoiceId], [ProductId], [Quantity], [UnitPrice], [IsDelete]) VALUES (23, 8, 9, 1, 750000, 0)
SET IDENTITY_INSERT [dbo].[SaleInvoiceDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[SaleInvoices] ON 

INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (1, CAST(N'2024-12-27T00:38:56.3958787' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 5, 0, N'', 0, 24900, N'Nguyen Van Customer', N'0397487360', N'Không rõ đường, Thủ Đức')
INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (2, CAST(N'2024-12-27T00:39:45.7831513' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 1, 0, N'', 0, 22900, N'Nguyen Van Customer', N'0397487360', N'Đường Song Hành Xa lộ Hà Nội, Thủ Đức')
INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (3, CAST(N'2024-12-27T00:40:06.1555543' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 1, 0, N'', 0, 15100, N'Nguyen Van Customer', N'0397487360', N'Đường số 17, Thủ Đức')
INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (4, CAST(N'2024-12-27T00:44:58.1279024' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 1, 0, N'f', 0, 23100, N'Nguyen Van Customer', N'0397487360', N'Không rõ đường, Thủ Đức')
INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (5, CAST(N'2024-12-27T00:45:28.4076834' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 1, 0, N'', 0, 28550, N'Nguyen Van Customer', N'0397487360', N'Đường Dương Đình Nghệ, Thủ Đức')
INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (6, CAST(N'2024-12-27T00:46:01.3594763' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 1, 0, N'', 0, 23150, N'Nguyen Van Customer', N'0397487360', N'Lâm Viên, Thủ Đức')
INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (7, CAST(N'2024-12-27T00:46:25.1907512' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 5, 0, N'', 0, 14650, N'Nguyen Van Customer', N'0397487360', N'Đường số 1, Dĩ An')
INSERT [dbo].[SaleInvoices] ([Id], [CreateDay], [CustomerId], [PaymentMethodId], [Status], [IsDelete], [Note], [IsPaid], [ShippingCost], [NameRecipient], [PhoneNumberRecipient], [DeliveryAddress]) VALUES (8, CAST(N'2024-12-27T01:37:33.9641822' AS DateTime2), N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', 1, 1, 0, N'', 0, 138300, N'Nguyen Van Customer', N'0397487360', N'Không rõ đường, Ho Chi Minh City')
SET IDENTITY_INSERT [dbo].[SaleInvoices] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([Id], [CompanyName], [TaxCode], [Email], [Phone], [Type], [Images], [Description], [Industry], [Address], [IsDelete]) VALUES (1, N'KHÔNG XÁC ĐỊNH', N'KHÔNG XÁC ĐỊNH', N'KHÔNG XÁC ĐỊNH', N'KHÔNG XÁC ĐỊNH', 0, N'["no_img.png"]', N'KHÔNG XÁC ĐỊNH', N'KHÔNG XÁC ĐỊNH', N'KHÔNG XÁC ĐỊNH', 0)
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'd58c7582-fe21-4a5a-8dfc-40956df5fed7', N'44b092a7-08ad-44d5-895b-730d4e953bea')
INSERT [dbo].[UserRoles] ([UserId], [RoleId]) VALUES (N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', N'70f49d11-006f-49a4-95c7-c8ac053d74ad')
GO
INSERT [dbo].[Users] ([Id], [FullName], [BirthDay], [Images], [IsLock], [IsDelete], [CartId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'0e51f5f6-fd3c-4f13-9bf9-0e44f293a473', N'Nguyen Van Customer', CAST(N'1995-01-01T00:00:00.0000000' AS DateTime2), N'["no_img.png"]', 0, 0, 2, N'customer', N'CUSTOMER', N'customer@example.com', N'CUSTOMER@EXAMPLE.COM', 1, N'AQAAAAEAACcQAAAAEAlq7Fz7k9QBtvPgy5RgayBo7Wwr6KJHSfu2oxnSr8TWOT1jl7ERfNiU0JLww/1c0w==', N'7RTCPQMZAWBYAR4DRQOQH26XOSH3HL6C', N'975bb0ea-dfda-4906-aba3-cd6666489651', N'0397487360', 1, 0, NULL, 1, 0)
INSERT [dbo].[Users] ([Id], [FullName], [BirthDay], [Images], [IsLock], [IsDelete], [CartId], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd58c7582-fe21-4a5a-8dfc-40956df5fed7', N'Dung Ne', CAST(N'2003-04-30T00:00:00.0000000' AS DateTime2), N'["no_img.png"]', 0, 0, 1, N'admin', N'ADMIN', N'dungtienthe1920@gmail.com', N'DUNGTIENTHE1920@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEHiStpL59WqOAHjq72czsIllofPbg7OrX+FqaaJq/tKLpDVQZjK3/jkPpZsG/k7Q7A==', N'54IPCZVUJPS3GUYCSLQIOBFEU74U7BPC', N'0586ab4f-24c5-48fa-9d08-a117f2f891a9', N'0397487360', 1, 0, NULL, 1, 0)
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Addresses_AppUserId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Addresses_AppUserId] ON [dbo].[Addresses]
(
	[AppUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartDetails_ProductId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_CartDetails_ProductId] ON [dbo].[CartDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Categories_ParentCategoryId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Categories_ParentCategoryId] ON [dbo].[Categories]
(
	[ParentCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Conversations_CustomerId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Conversations_CustomerId] ON [dbo].[Conversations]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Conversations_StaffId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Conversations_StaffId] ON [dbo].[Conversations]
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FeedBackResponses_FeedBackId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_FeedBackResponses_FeedBackId] ON [dbo].[FeedBackResponses]
(
	[FeedBackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_FeedBacks_SaleInvoiceDetailId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_FeedBacks_SaleInvoiceDetailId] ON [dbo].[FeedBacks]
(
	[SaleInvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Messages_ConversationId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Messages_ConversationId] ON [dbo].[Messages]
(
	[ConversationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaymentTokens_SaleInvoiceId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_PaymentTokens_SaleInvoiceId] ON [dbo].[PaymentTokens]
(
	[SaleInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductCategories_ProductId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductCategories_ProductId] ON [dbo].[ProductCategories]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductItems_CategoryId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductItems_CategoryId] ON [dbo].[ProductItems]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductPrices_ProductId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductPrices_ProductId] ON [dbo].[ProductPrices]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductProductItems_ProductItemId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_ProductProductItems_ProductItemId] ON [dbo].[ProductProductItems]
(
	[ProductItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_PackagingId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Products_PackagingId] ON [dbo].[Products]
(
	[PackagingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleClaims_RoleId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId] ON [dbo].[RoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SaleInvoiceDetails_ProductId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SaleInvoiceDetails_ProductId] ON [dbo].[SaleInvoiceDetails]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SaleInvoiceDetails_SaleInvoiceId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SaleInvoiceDetails_SaleInvoiceId] ON [dbo].[SaleInvoiceDetails]
(
	[SaleInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_SaleInvoices_CustomerId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SaleInvoices_CustomerId] ON [dbo].[SaleInvoices]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SaleInvoices_PaymentMethodId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SaleInvoices_PaymentMethodId] ON [dbo].[SaleInvoices]
(
	[PaymentMethodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierInvoiceDetails_ProductItemId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SupplierInvoiceDetails_ProductItemId] ON [dbo].[SupplierInvoiceDetails]
(
	[ProductItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierInvoices_SupplierId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SupplierInvoices_SupplierId] ON [dbo].[SupplierInvoices]
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierInvoices_SupplierInvoiceTokenId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SupplierInvoices_SupplierInvoiceTokenId] ON [dbo].[SupplierInvoices]
(
	[SupplierInvoiceTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierInvoiceTokens_SupplierInvoiceId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_SupplierInvoiceTokens_SupplierInvoiceId] ON [dbo].[SupplierInvoiceTokens]
(
	[SupplierInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserClaims_UserId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId] ON [dbo].[UserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserLogins_UserId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId] ON [dbo].[UserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserRoles_RoleId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId] ON [dbo].[UserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[Users]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_CartId]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE NONCLUSTERED INDEX [IX_Users_CartId] ON [dbo].[Users]
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 12/27/2024 1:43:02 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[Users]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductItems] ADD  DEFAULT (N'["no_img.png"]') FOR [Images]
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (N'["no_img.png"]') FOR [Images]
GO
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT (N'["no_img.png"]') FOR [Images]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'["no_img.png"]') FOR [Images]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Users_AppUserId] FOREIGN KEY([AppUserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Users_AppUserId]
GO
ALTER TABLE [dbo].[CartDetails]  WITH CHECK ADD  CONSTRAINT [FK_CartDetails_Carts_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Carts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetails] CHECK CONSTRAINT [FK_CartDetails_Carts_CartId]
GO
ALTER TABLE [dbo].[CartDetails]  WITH CHECK ADD  CONSTRAINT [FK_CartDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartDetails] CHECK CONSTRAINT [FK_CartDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Categories_ParentCategoryId] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Categories_ParentCategoryId]
GO
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_Users_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_Users_CustomerId]
GO
ALTER TABLE [dbo].[Conversations]  WITH CHECK ADD  CONSTRAINT [FK_Conversations_Users_StaffId] FOREIGN KEY([StaffId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Conversations] CHECK CONSTRAINT [FK_Conversations_Users_StaffId]
GO
ALTER TABLE [dbo].[FeedBackResponses]  WITH CHECK ADD  CONSTRAINT [FK_FeedBackResponses_FeedBacks_FeedBackId] FOREIGN KEY([FeedBackId])
REFERENCES [dbo].[FeedBacks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FeedBackResponses] CHECK CONSTRAINT [FK_FeedBackResponses_FeedBacks_FeedBackId]
GO
ALTER TABLE [dbo].[FeedBacks]  WITH CHECK ADD  CONSTRAINT [FK_FeedBacks_SaleInvoiceDetails_SaleInvoiceDetailId] FOREIGN KEY([SaleInvoiceDetailId])
REFERENCES [dbo].[SaleInvoiceDetails] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FeedBacks] CHECK CONSTRAINT [FK_FeedBacks_SaleInvoiceDetails_SaleInvoiceDetailId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Conversations_ConversationId] FOREIGN KEY([ConversationId])
REFERENCES [dbo].[Conversations] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Conversations_ConversationId]
GO
ALTER TABLE [dbo].[PaymentTokens]  WITH CHECK ADD  CONSTRAINT [FK_PaymentTokens_SaleInvoices_SaleInvoiceId] FOREIGN KEY([SaleInvoiceId])
REFERENCES [dbo].[SaleInvoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentTokens] CHECK CONSTRAINT [FK_PaymentTokens_SaleInvoices_SaleInvoiceId]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ProductCategories]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategories_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductItems]  WITH CHECK ADD  CONSTRAINT [FK_ProductItems_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductItems] CHECK CONSTRAINT [FK_ProductItems_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ProductPrices]  WITH CHECK ADD  CONSTRAINT [FK_ProductPrices_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductPrices] CHECK CONSTRAINT [FK_ProductPrices_Products_ProductId]
GO
ALTER TABLE [dbo].[ProductProductItems]  WITH CHECK ADD  CONSTRAINT [FK_ProductProductItems_ProductItems_ProductItemId] FOREIGN KEY([ProductItemId])
REFERENCES [dbo].[ProductItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductProductItems] CHECK CONSTRAINT [FK_ProductProductItems_ProductItems_ProductItemId]
GO
ALTER TABLE [dbo].[ProductProductItems]  WITH CHECK ADD  CONSTRAINT [FK_ProductProductItems_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductProductItems] CHECK CONSTRAINT [FK_ProductProductItems_Products_ProductId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Packaging_PackagingId] FOREIGN KEY([PackagingId])
REFERENCES [dbo].[Packaging] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Packaging_PackagingId]
GO
ALTER TABLE [dbo].[RoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RoleClaims] CHECK CONSTRAINT [FK_RoleClaims_Roles_RoleId]
GO
ALTER TABLE [dbo].[SaleInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoiceDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SaleInvoiceDetails] CHECK CONSTRAINT [FK_SaleInvoiceDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[SaleInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoiceDetails_SaleInvoices_SaleInvoiceId] FOREIGN KEY([SaleInvoiceId])
REFERENCES [dbo].[SaleInvoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SaleInvoiceDetails] CHECK CONSTRAINT [FK_SaleInvoiceDetails_SaleInvoices_SaleInvoiceId]
GO
ALTER TABLE [dbo].[SaleInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoices_PaymentMethods_PaymentMethodId] FOREIGN KEY([PaymentMethodId])
REFERENCES [dbo].[PaymentMethods] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SaleInvoices] CHECK CONSTRAINT [FK_SaleInvoices_PaymentMethods_PaymentMethodId]
GO
ALTER TABLE [dbo].[SaleInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SaleInvoices_Users_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SaleInvoices] CHECK CONSTRAINT [FK_SaleInvoices_Users_CustomerId]
GO
ALTER TABLE [dbo].[SupplierInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoiceDetails_ProductItems_ProductItemId] FOREIGN KEY([ProductItemId])
REFERENCES [dbo].[ProductItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupplierInvoiceDetails] CHECK CONSTRAINT [FK_SupplierInvoiceDetails_ProductItems_ProductItemId]
GO
ALTER TABLE [dbo].[SupplierInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoiceDetails_SupplierInvoices_SupplierInvoiceId] FOREIGN KEY([SupplierInvoiceId])
REFERENCES [dbo].[SupplierInvoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupplierInvoiceDetails] CHECK CONSTRAINT [FK_SupplierInvoiceDetails_SupplierInvoices_SupplierInvoiceId]
GO
ALTER TABLE [dbo].[SupplierInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoices_SupplierInvoiceTokens_SupplierInvoiceTokenId] FOREIGN KEY([SupplierInvoiceTokenId])
REFERENCES [dbo].[SupplierInvoiceTokens] ([Id])
GO
ALTER TABLE [dbo].[SupplierInvoices] CHECK CONSTRAINT [FK_SupplierInvoices_SupplierInvoiceTokens_SupplierInvoiceTokenId]
GO
ALTER TABLE [dbo].[SupplierInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoices_Suppliers_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupplierInvoices] CHECK CONSTRAINT [FK_SupplierInvoices_Suppliers_SupplierId]
GO
ALTER TABLE [dbo].[SupplierInvoiceTokens]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoiceTokens_SupplierInvoices_SupplierInvoiceId] FOREIGN KEY([SupplierInvoiceId])
REFERENCES [dbo].[SupplierInvoices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupplierInvoiceTokens] CHECK CONSTRAINT [FK_SupplierInvoiceTokens_SupplierInvoices_SupplierInvoiceId]
GO
ALTER TABLE [dbo].[UserClaims]  WITH CHECK ADD  CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaims] CHECK CONSTRAINT [FK_UserClaims_Users_UserId]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Users_UserId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles_RoleId]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Carts_CartId] FOREIGN KEY([CartId])
REFERENCES [dbo].[Carts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Carts_CartId]
GO
ALTER TABLE [dbo].[UserTokens]  WITH CHECK ADD  CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserTokens] CHECK CONSTRAINT [FK_UserTokens_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [FlowerShopDB] SET  READ_WRITE 
GO
