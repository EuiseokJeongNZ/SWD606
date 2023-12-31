USE [master]
GO
/****** Object:  Database [Bella_Internet_Cafe]    Script Date: 13/12/2023 11:41:48 am ******/
CREATE DATABASE [Bella_Internet_Cafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Bella_Internet_Cafe', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Bella_Internet_Cafe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Bella_Internet_Cafe_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\Bella_Internet_Cafe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Bella_Internet_Cafe] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Bella_Internet_Cafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Bella_Internet_Cafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET RECOVERY FULL 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET  MULTI_USER 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Bella_Internet_Cafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Bella_Internet_Cafe] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Bella_Internet_Cafe', N'ON'
GO
ALTER DATABASE [Bella_Internet_Cafe] SET QUERY_STORE = ON
GO
ALTER DATABASE [Bella_Internet_Cafe] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Bella_Internet_Cafe]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 13/12/2023 11:41:49 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[C_PID] [int] IDENTITY(1,1) NOT NULL,
	[C_Name] [varchar](50) NULL,
	[C_EmailAddress] [varchar](50) NULL,
	[C_Password] [varchar](20) NULL,
	[C_Gender] [varchar](10) NULL,
	[C_Age] [int] NULL,
	[C_HomeAddress] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[C_PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 13/12/2023 11:41:49 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[I_PID] [int] IDENTITY(1,1) NOT NULL,
	[C_Name] [varchar](50) NULL,
	[I_Usage] [time](7) NULL,
	[I_UsagePrice] [decimal](10, 2) NULL,
	[I_PrintAmount] [int] NULL,
	[I_PrintPrice] [decimal](10, 2) NULL,
	[I_UploadAmount] [int] NULL,
	[I_DownloadAmount] [int] NULL,
	[I_UpDownloadPrice] [decimal](10, 2) NULL,
	[I_Total] [decimal](10, 2) NULL,
	[I_InvoiceTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[I_PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Printing]    Script Date: 13/12/2023 11:41:49 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Printing](
	[P_PID] [int] IDENTITY(1,1) NOT NULL,
	[P_Name] [varchar](50) NULL,
	[P_Price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[P_PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpDownload]    Script Date: 13/12/2023 11:41:49 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpDownload](
	[UD_PID] [int] IDENTITY(1,1) NOT NULL,
	[UD_Name] [varchar](50) NULL,
	[UD_Price] [decimal](10, 2) NULL,
	[Datas] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UD_PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usage]    Script Date: 13/12/2023 11:41:49 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usage](
	[U_PID] [int] IDENTITY(1,1) NOT NULL,
	[U_Name] [varchar](50) NULL,
	[U_Price] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[U_PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([C_PID], [C_Name], [C_EmailAddress], [C_Password], [C_Gender], [C_Age], [C_HomeAddress]) VALUES (3, N'NICKYOUNG', N'1234@gmail.com', N'1234', N'Male', 30, N'238QUEENSTREET')
INSERT [dbo].[Customer] ([C_PID], [C_Name], [C_EmailAddress], [C_Password], [C_Gender], [C_Age], [C_HomeAddress]) VALUES (10, N'TEST', N'test@email.com', N'1234', N'Female', 0, N'')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Printing] ON 

INSERT [dbo].[Printing] ([P_PID], [P_Name], [P_Price]) VALUES (1, N'Price of Printing', CAST(1.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Printing] OFF
GO
SET IDENTITY_INSERT [dbo].[UpDownload] ON 

INSERT [dbo].[UpDownload] ([UD_PID], [UD_Name], [UD_Price], [Datas]) VALUES (1, N'Data1', CAST(1.00 AS Decimal(10, 2)), 100)
INSERT [dbo].[UpDownload] ([UD_PID], [UD_Name], [UD_Price], [Datas]) VALUES (2, N'Data2', CAST(2.00 AS Decimal(10, 2)), 500)
INSERT [dbo].[UpDownload] ([UD_PID], [UD_Name], [UD_Price], [Datas]) VALUES (3, N'Data3', CAST(3.00 AS Decimal(10, 2)), 1000)
SET IDENTITY_INSERT [dbo].[UpDownload] OFF
GO
SET IDENTITY_INSERT [dbo].[Usage] ON 

INSERT [dbo].[Usage] ([U_PID], [U_Name], [U_Price]) VALUES (1, N'Price of Usage', CAST(3.50 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[Usage] OFF
GO
USE [master]
GO
ALTER DATABASE [Bella_Internet_Cafe] SET  READ_WRITE 
GO
