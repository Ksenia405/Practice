USE [master]
GO
/****** Object:  Database [Companies]    Script Date: 21.08.2023 15:12:09 ******/
CREATE DATABASE [Companies]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Companies', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SEREVERMSSQL\MSSQL\DATA\Companies.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Companies_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SEREVERMSSQL\MSSQL\DATA\Companies_log.ldf' , SIZE = 8855552KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Companies] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Companies].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Companies] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Companies] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Companies] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Companies] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Companies] SET ARITHABORT OFF 
GO
ALTER DATABASE [Companies] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Companies] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Companies] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Companies] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Companies] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Companies] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Companies] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Companies] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Companies] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Companies] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Companies] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Companies] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Companies] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Companies] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Companies] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Companies] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Companies] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Companies] SET RECOVERY FULL 
GO
ALTER DATABASE [Companies] SET  MULTI_USER 
GO
ALTER DATABASE [Companies] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Companies] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Companies] SET FILESTREAM( NON_TRANSACTED_ACCESS = FULL ) 
GO
ALTER DATABASE [Companies] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Companies] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Companies] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Companies', N'ON'
GO
ALTER DATABASE [Companies] SET QUERY_STORE = ON
GO
ALTER DATABASE [Companies] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Companies]
GO
/****** Object:  UserDefinedFunction [dbo].[SelectIdsId]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[SelectIdsId]
(
    @type nvarchar(max),
    @number nvarchar(max)
)
RETURNS int
AS
BEGIN
    DECLARE @id_ids int;

    SELECT TOP 1 @id_ids = id_ids FROM ids WHERE [type] = @type AND [number] = @number;

    RETURN @id_ids;
END
GO
/****** Object:  Table [dbo].[results]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[results](
	[id_result] [int] IDENTITY(1,1) NOT NULL,
	[remarks] [nvarchar](max) NULL,
	[name] [nvarchar](max) NULL,
	[type] [nvarchar](max) NULL,
	[entity_number] [bigint] NULL,
	[source] [nvarchar](max) NULL,
	[source_information_url] [nvarchar](max) NULL,
	[source_list_url] [nvarchar](max) NULL,
	[call_sign] [nvarchar](max) NULL,
	[end_date] [datetime] NULL,
	[federal_register_notice] [nvarchar](max) NULL,
	[gross_registered_tonnage] [nvarchar](max) NULL,
	[gross_tonnage] [nvarchar](max) NULL,
	[license_policy] [nvarchar](max) NULL,
	[license_requirement] [nvarchar](max) NULL,
	[standard_order] [nvarchar](max) NULL,
	[start_date] [datetime] NULL,
	[title] [nvarchar](max) NULL,
	[vessel_flag] [nvarchar](max) NULL,
	[vessel_owner] [nvarchar](max) NULL,
	[vessel_type] [nvarchar](max) NULL,
	[id] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_results] PRIMARY KEY CLUSTERED 
(
	[id_result] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[addresses]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[addresses](
	[id_address] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NULL,
	[address] [nvarchar](max) NULL,
	[city] [nvarchar](max) NULL,
	[state] [nvarchar](max) NULL,
	[postal_code] [nvarchar](max) NULL,
	[country] [nvarchar](max) NULL,
 CONSTRAINT [PK_addresses] PRIMARY KEY CLUSTERED 
(
	[id_address] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[programs]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[programs](
	[id_program] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NULL,
	[program] [nvarchar](max) NULL,
 CONSTRAINT [PK_programs] PRIMARY KEY CLUSTERED 
(
	[id_program] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[places_of_birth]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[places_of_birth](
	[id_places_of_birth] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NULL,
	[place_of_birth] [nvarchar](max) NULL,
 CONSTRAINT [PK_places_of_birth] PRIMARY KEY CLUSTERED 
(
	[id_places_of_birth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nationalities]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[nationalities](
	[id_nationalities] [int] IDENTITY(1,1) NOT NULL,
	[nationality] [nvarchar](max) NULL,
	[id_result] [int] NULL,
 CONSTRAINT [PK_nationalities] PRIMARY KEY CLUSTERED 
(
	[id_nationalities] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ids]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ids](
	[id_ids] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](max) NULL,
	[number] [nvarchar](max) NULL,
	[country] [nvarchar](max) NULL,
 CONSTRAINT [PK_ids] PRIMARY KEY CLUSTERED 
(
	[id_ids] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[alt_names]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[alt_names](
	[id_alt_names] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NULL,
	[alt_name] [nvarchar](max) NULL,
 CONSTRAINT [PK_alt_names] PRIMARY KEY CLUSTERED 
(
	[id_alt_names] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[list]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[list](
	[id_list] [int] IDENTITY(1,1) NOT NULL,
	[id_ids] [int] NULL,
	[id_result] [int] NULL,
 CONSTRAINT [PK_list] PRIMARY KEY CLUSTERED 
(
	[id_list] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dates_of_birth]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dates_of_birth](
	[id_dates_of_birth] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NULL,
	[date_of_birth] [nvarchar](max) NULL,
 CONSTRAINT [PK_dates_of_birth] PRIMARY KEY CLUSTERED 
(
	[id_dates_of_birth] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetResultsForId]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetResultsForId](@id nvarchar(max))
RETURNS TABLE
AS
RETURN (
    SELECT 
        [results].[id_result],
        [results].[id],
        [remarks], [name],
        [results].[type] AS [result_type], 
        [entity_number], 
        [source],
        [source_information_url], [source_list_url], [call_sign], [end_date],
        [federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
        [license_policy], [license_requirement], [standard_order], [start_date],
        [title], [vessel_flag], [vessel_owner], [vessel_type],
        [address], [city], [state], [postal_code], [addresses].[country] AS [addresses_country],
        [alt_name], [date_of_birth], [nationality],
        [place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country] AS [ids_country]
    FROM results
    LEFT JOIN alt_names ON alt_names.id_result=results.id_result
    LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
    LEFT JOIN nationalities ON nationalities.id_result=results.id_result
    LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
    LEFT JOIN programs ON programs.id_result=results.id_result
    LEFT JOIN list ON list.id_result=results.id_result
    LEFT JOIN ids ON ids.id_ids=list.id_ids
    LEFT JOIN addresses ON addresses.id_result=results.id_result
    WHERE results.id = @id
);
GO
/****** Object:  Table [dbo].[x_dates_of_birth_process]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[x_dates_of_birth_process](
	[id_x_dates_of_birth_process] [int] IDENTITY(1,1) NOT NULL,
	[id_dates_od_birth] [int] NOT NULL,
 CONSTRAINT [PK_x_dates_of_birth_process] PRIMARY KEY CLUSTERED 
(
	[id_x_dates_of_birth_process] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetDatesOfB]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetDatesOfB] 
(
)
RETURNS TABLE
AS
RETURN (SELECT [results].[id_result],
[results].[id],
[remarks], [name],
[results].[type] AS [result_type], 
[entity_number], 
[source],
[source_information_url], [source_list_url], [call_sign], [end_date],
[federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
[license_policy], [license_requirement], [standard_order], [start_date],
[title], [vessel_flag], [vessel_owner], [vessel_type],
[address], [city], [state], [postal_code], [addresses].[country] AS [addresses_country],
[alt_name], [date_of_birth], [nationality],
[place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country] AS [ids_country]
FROM results
LEFT JOIN alt_names ON alt_names.id_result=results.id_result
LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
LEFT JOIN nationalities ON nationalities.id_result=results.id_result
LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
LEFT JOIN programs ON programs.id_result=results.id_result
LEFT JOIN list ON list.id_result=results.id_result
LEFT JOIN ids ON ids.id_ids=list.id_ids
LEFT JOIN addresses ON addresses.id_result=results.id_result
INNER JOIN x_dates_of_birth_process ON x_dates_of_birth_process.id_dates_od_birth=dates_of_birth.id_dates_of_birth);
GO
/****** Object:  Table [dbo].[x_address_process]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[x_address_process](
	[id_x_address_process] [int] IDENTITY(1,1) NOT NULL,
	[id_address] [int] NOT NULL,
 CONSTRAINT [PK_x_address_process] PRIMARY KEY CLUSTERED 
(
	[id_x_address_process] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetAddress]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetAddress]
(
)
RETURNS TABLE
AS
RETURN (SELECT
[results].[id_result],
[results].[id],
[remarks], [name],
[results].[type] AS [result_type], 
[entity_number], 
[source],
[source_information_url], [source_list_url], [call_sign], [end_date],
[federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
[license_policy], [license_requirement], [standard_order], [start_date],
[title], [vessel_flag], [vessel_owner], [vessel_type],
[address], [city], [state], [postal_code], [addresses].[country] AS [addresses_country],
[alt_name], [date_of_birth], [nationality],
[place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country] AS [ids_country]
FROM results LEFT JOIN addresses ON addresses.id_result=results.id_result
LEFT JOIN alt_names ON alt_names.id_result=results.id_result
LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
LEFT JOIN nationalities ON nationalities.id_result=results.id_result
LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
LEFT JOIN programs ON programs.id_result=results.id_result
LEFT JOIN list ON list.id_result=results.id_result
LEFT JOIN ids ON ids.id_ids=list.id_ids
INNER JOIN 
x_address_process ON x_address_process.id_address=addresses.id_address );
GO
/****** Object:  UserDefinedFunction [dbo].[GetAll]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetAll] 
(
)
RETURNS TABLE
AS
RETURN (SELECT [results].[id_result],
[results].[id],
[remarks], [name],
[results].[type] AS [result_type], 
[entity_number], 
[source],
[source_information_url], [source_list_url], [call_sign], [end_date],
[federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
[license_policy], [license_requirement], [standard_order], [start_date],
[title], [vessel_flag], [vessel_owner], [vessel_type],
[address], [city], [state], [postal_code], [addresses].[country] AS [addresses_country],
[alt_name], [date_of_birth], [nationality],
[place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country] AS [ids_country]
FROM results
LEFT JOIN addresses ON addresses.id_result=results.id_result
LEFT JOIN alt_names ON alt_names.id_result=results.id_result
LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
LEFT JOIN nationalities ON nationalities.id_result=results.id_result
LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
LEFT JOIN programs ON programs.id_result=results.id_result
LEFT JOIN list ON list.id_result=results.id_result
LEFT JOIN ids ON ids.id_ids=list.id_ids);
GO
/****** Object:  Table [dbo].[x_result_license]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[x_result_license](
	[id_x_result_license] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NOT NULL,
 CONSTRAINT [PK_x_result_license] PRIMARY KEY CLUSTERED 
(
	[id_x_result_license] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetLicense]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetLicense]
(
)
RETURNS TABLE
AS
RETURN (SELECT [results].[id_result],
[results].[id],
[remarks], [name],
[results].[type] AS [result_type], 
[entity_number], 
[source],
[source_information_url], [source_list_url], [call_sign], [end_date],
[federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
[license_policy], [license_requirement], [standard_order], [start_date],
[title], [vessel_flag], [vessel_owner], [vessel_type],
[address], [city], [state], [postal_code], [addresses].[country] AS [addresses_country],
[alt_name], [date_of_birth], [nationality],
[place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country] AS [ids_country]
FROM results
LEFT JOIN alt_names ON alt_names.id_result=results.id_result
LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
LEFT JOIN nationalities ON nationalities.id_result=results.id_result
LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
LEFT JOIN programs ON programs.id_result=results.id_result
LEFT JOIN list ON list.id_result=results.id_result
LEFT JOIN ids ON ids.id_ids=list.id_ids
LEFT JOIN addresses ON addresses.id_result=results.id_result
INNER JOIN x_result_license ON x_result_license.id_result=results.id_result);
GO
/****** Object:  Table [dbo].[x_result_program]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[x_result_program](
	[id_x_result_program] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NOT NULL,
 CONSTRAINT [PK_x_result_program] PRIMARY KEY CLUSTERED 
(
	[id_x_result_program] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetProgram]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetProgram]
(
)
RETURNS TABLE
AS
RETURN (SELECT [results].[id_result],
[results].[id],
[remarks], [name],
[results].[type] AS [result_type], 
[entity_number], 
[source],
[source_information_url], [source_list_url], [call_sign], [end_date],
[federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
[license_policy], [license_requirement], [standard_order], [start_date],
[title], [vessel_flag], [vessel_owner], [vessel_type],
[address], [city], [state], [postal_code], [addresses].[country] AS [addresses_country],
[alt_name], [date_of_birth], [nationality],
[place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country] AS [ids_country]
FROM results
LEFT JOIN alt_names ON alt_names.id_result=results.id_result
LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
LEFT JOIN nationalities ON nationalities.id_result=results.id_result
LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
LEFT JOIN programs ON programs.id_result=results.id_result
LEFT JOIN list ON list.id_result=results.id_result
LEFT JOIN ids ON ids.id_ids=list.id_ids
LEFT JOIN addresses ON addresses.id_result=results.id_result
INNER JOIN x_result_program ON x_result_program.id_result=results.id_result);
GO
/****** Object:  Table [dbo].[x_results_date]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[x_results_date](
	[id_x_result_date] [int] IDENTITY(1,1) NOT NULL,
	[id_result] [int] NOT NULL,
 CONSTRAINT [PK_x_results_date] PRIMARY KEY CLUSTERED 
(
	[id_x_result_date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetEndD]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetEndD]
(
)
RETURNS TABLE
AS
RETURN(SELECT [results].[id_result],
[results].[id],
[remarks], [name],
[results].[type] AS [result_type], 
[entity_number], 
[source],
[source_information_url], [source_list_url], [call_sign], [end_date],
[federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
[license_policy], [license_requirement], [standard_order], [start_date],
[title], [vessel_flag], [vessel_owner], [vessel_type],
[address], [city], [state], [postal_code], [addresses].[country],
[alt_name], [date_of_birth], [nationality],
[place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country] AS [ids_country]
FROM results
LEFT JOIN alt_names ON alt_names.id_result=results.id_result
LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
LEFT JOIN nationalities ON nationalities.id_result=results.id_result
LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
LEFT JOIN programs ON programs.id_result=results.id_result
LEFT JOIN list ON list.id_result=results.id_result
LEFT JOIN ids ON ids.id_ids=list.id_ids
LEFT JOIN addresses ON addresses.id_result=results.id_result
INNER JOIN x_results_date ON x_results_date.id_result=results.id_result);
GO
/****** Object:  Table [dbo].[x_result_ids]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[x_result_ids](
	[id_x_result_ids] [int] IDENTITY(1,1) NOT NULL,
	[id_list] [int] NOT NULL,
 CONSTRAINT [PK_x_result_ids] PRIMARY KEY CLUSTERED 
(
	[id_x_result_ids] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  UserDefinedFunction [dbo].[GetIDS]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[GetIDS]
(
)
RETURNS TABLE
AS
RETURN(SELECT [results].[id_result],
[results].[id],
[remarks], [name],
[results].[type] AS [result_type], 
[entity_number], 
[source],
[source_information_url], [source_list_url], [call_sign], [end_date],
[federal_register_notice], [gross_registered_tonnage], [gross_tonnage],
[license_policy], [license_requirement], [standard_order], [start_date],
[title], [vessel_flag], [vessel_owner], [vessel_type],
[address], [city], [state], [postal_code], [addresses].[country] AS [addresses_country],
[alt_name], [date_of_birth], [nationality],
[place_of_birth], [program], [ids].[type] AS [ids_type], [ids].[number], [ids].[country]  AS [ids_country]
FROM results
LEFT JOIN alt_names ON alt_names.id_result=results.id_result
LEFT JOIN dates_of_birth ON dates_of_birth.id_result=results.id_result
LEFT JOIN nationalities ON nationalities.id_result=results.id_result
LEFT JOIN places_of_birth ON places_of_birth.id_result=results.id_result
LEFT JOIN programs ON programs.id_result=results.id_result
LEFT JOIN list ON list.id_result=results.id_result
LEFT JOIN ids ON ids.id_ids=list.id_ids
LEFT JOIN addresses ON addresses.id_result=results.id_result
INNER JOIN x_result_ids ON x_result_ids.id_list=list.id_list);
GO
/****** Object:  UserDefinedFunction [dbo].[SelectStrtDOB]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[SelectStrtDOB]()
RETURNS TABLE
AS
RETURN (SELECT date_of_birth, id_dates_of_birth FROM dates_of_birth);
GO
ALTER TABLE [dbo].[addresses]  WITH CHECK ADD  CONSTRAINT [FK_addresses_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[addresses] CHECK CONSTRAINT [FK_addresses_results]
GO
ALTER TABLE [dbo].[alt_names]  WITH CHECK ADD  CONSTRAINT [FK_alt_names_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[alt_names] CHECK CONSTRAINT [FK_alt_names_results]
GO
ALTER TABLE [dbo].[dates_of_birth]  WITH CHECK ADD  CONSTRAINT [FK_dates_of_birth_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[dates_of_birth] CHECK CONSTRAINT [FK_dates_of_birth_results]
GO
ALTER TABLE [dbo].[list]  WITH CHECK ADD  CONSTRAINT [FK_list_ids] FOREIGN KEY([id_ids])
REFERENCES [dbo].[ids] ([id_ids])
GO
ALTER TABLE [dbo].[list] CHECK CONSTRAINT [FK_list_ids]
GO
ALTER TABLE [dbo].[list]  WITH CHECK ADD  CONSTRAINT [FK_list_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[list] CHECK CONSTRAINT [FK_list_results]
GO
ALTER TABLE [dbo].[nationalities]  WITH CHECK ADD  CONSTRAINT [FK_nationalities_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[nationalities] CHECK CONSTRAINT [FK_nationalities_results]
GO
ALTER TABLE [dbo].[places_of_birth]  WITH CHECK ADD  CONSTRAINT [FK_places_of_birth_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[places_of_birth] CHECK CONSTRAINT [FK_places_of_birth_results]
GO
ALTER TABLE [dbo].[programs]  WITH CHECK ADD  CONSTRAINT [FK_programs_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[programs] CHECK CONSTRAINT [FK_programs_results]
GO
ALTER TABLE [dbo].[x_address_process]  WITH CHECK ADD  CONSTRAINT [FK_x_address_process_addresses] FOREIGN KEY([id_address])
REFERENCES [dbo].[addresses] ([id_address])
GO
ALTER TABLE [dbo].[x_address_process] CHECK CONSTRAINT [FK_x_address_process_addresses]
GO
ALTER TABLE [dbo].[x_dates_of_birth_process]  WITH CHECK ADD  CONSTRAINT [FK_x_dates_of_birth_process_dates_of_birth1] FOREIGN KEY([id_dates_od_birth])
REFERENCES [dbo].[dates_of_birth] ([id_dates_of_birth])
GO
ALTER TABLE [dbo].[x_dates_of_birth_process] CHECK CONSTRAINT [FK_x_dates_of_birth_process_dates_of_birth1]
GO
ALTER TABLE [dbo].[x_result_ids]  WITH CHECK ADD  CONSTRAINT [FK_x_result_ids_list] FOREIGN KEY([id_list])
REFERENCES [dbo].[list] ([id_list])
GO
ALTER TABLE [dbo].[x_result_ids] CHECK CONSTRAINT [FK_x_result_ids_list]
GO
ALTER TABLE [dbo].[x_result_license]  WITH CHECK ADD  CONSTRAINT [FK_x_result_license_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[x_result_license] CHECK CONSTRAINT [FK_x_result_license_results]
GO
ALTER TABLE [dbo].[x_result_program]  WITH CHECK ADD  CONSTRAINT [FK_x_result_program_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[x_result_program] CHECK CONSTRAINT [FK_x_result_program_results]
GO
ALTER TABLE [dbo].[x_results_date]  WITH CHECK ADD  CONSTRAINT [FK_x_results_date_results] FOREIGN KEY([id_result])
REFERENCES [dbo].[results] ([id_result])
GO
ALTER TABLE [dbo].[x_results_date] CHECK CONSTRAINT [FK_x_results_date_results]
GO
/****** Object:  StoredProcedure [dbo].[Delete_XAddress]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_XAddress]
AS
BEGIN
DELETE x_address_process
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_XDate]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_XDate]
AS
BEGIN
DELETE x_results_date
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_XDate_Birth]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_XDate_Birth]
AS
BEGIN
DELETE x_dates_of_birth_process
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_XIDS]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_XIDS]
AS
BEGIN
DELETE x_result_ids
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_XLisence]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_XLisence]
AS
BEGIN
DELETE x_result_license
END
GO
/****** Object:  StoredProcedure [dbo].[Delete_XProg]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Delete_XProg]
AS
BEGIN
DELETE x_result_program
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAllData]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteAllData]
AS
BEGIN
DELETE addresses
DELETE alt_names
DELETE list
DELETE ids
DELETE programs
DELETE dates_of_birth
DELETE nationalities
DELETE places_of_birth
DELETE results
END
GO
/****** Object:  StoredProcedure [dbo].[InsertAddress]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertAddress]
	@id_result int,
	@address nvarchar(max),
	@city nvarchar(max),
	@state nvarchar(max),
	@postal_code nvarchar(max),
	@country nvarchar(max)
AS
BEGIN
	INSERT INTO addresses (
	[id_result], 
	[address], 
	[city],
	[state],
	[postal_code], 
	[country]
	) VALUES (
	@id_result,
	@address, @city,
	@state,
	@postal_code,
	@country)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertAlt_names]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertAlt_names]
	@id_result int,
	@alt_name nvarchar(max)
AS
BEGIN
	INSERT INTO alt_names
	(
	[id_result],
	[alt_name]
	) VALUES (
	@id_result,
	@alt_name)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertDate_of_birth]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertDate_of_birth]
	@id_result int,
	@date_of_birth nvarchar(max)
AS
BEGIN
	INSERT INTO dates_of_birth (
	[id_result], 
	[date_of_birth]
	) VALUES (
	@id_result,
	@date_of_birth)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertIDS]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertIDS]
@id_ids INT OUTPUT,
	@type nvarchar(max),
	@number nvarchar(max),
	@country nvarchar(max)
AS
BEGIN
SET NOCOUNT ON
	INSERT INTO ids (
	[type],
	[number],
	[country]
	) VALUES (
	@type,
	@number,
	@country)
	SET  @id_ids=SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[InsertList]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertList]
	@id_ids int,
	@id_result int
AS
BEGIN
	INSERT INTO list (
	[id_ids],
	[id_result]
	) VALUES (
	@id_ids,
	@id_result)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertNationalities]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertNationalities] 
	@id_result int,
	@nationality nvarchar(max)
AS
BEGIN
	INSERT INTO nationalities (
	[id_result],
	[nationality]
	) VALUES (
	@id_result,
	@nationality)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertPlaces_of_birth]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertPlaces_of_birth]
	@id_result int,
	@place_of_birth nvarchar(max)
AS
BEGIN
	INSERT INTO places_of_birth (
	[id_result], 
	[place_of_birth]
	) VALUES (
	@id_result,
	@place_of_birth)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertProgram]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertProgram] 
	@id_result int,
	@program nvarchar(max)
AS
BEGIN
	INSERT INTO programs (
	[id_result],
	[program]
	) VALUES (
	@id_result,
	@program)
END
GO
/****** Object:  StoredProcedure [dbo].[InsertResult]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertResult] 
@id_result INT OUTPUT,
	@remarks nvarchar(max),
	@name nvarchar(max),
	@type nvarchar(max),
	@entity_number bigint,
	@source nvarchar(max),
	@source_information_url nvarchar(max),
	@source_list_url nvarchar(max),
	@call_sign nvarchar(max),
	@end_date datetime,
	@federal_register_notice nvarchar(max),
	@gross_registered_tonnage nvarchar(max),
	@gross_tonnage nvarchar(max),
	@license_policy nvarchar(max),
	@license_requirement nvarchar(max),
    @standard_order nvarchar(max),
	@start_date datetime,
	@title nvarchar(max),
	@vessel_flag nvarchar(max),
	@vessel_owner nvarchar(max),
	@vessel_type nvarchar(max),
	@id nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON
	INSERT INTO results(
	[remarks], [name], [type], 
	[entity_number], [source], [source_information_url],
    [source_list_url], [call_sign], [end_date], [federal_register_notice],
	[gross_registered_tonnage], [gross_tonnage], [license_policy], [license_requirement],
    [standard_order], [start_date], [title], [vessel_flag], [vessel_owner], [vessel_type], [id]
	)VALUES(
	@remarks, @name, @type, @entity_number, @source,
	@source_information_url, @source_list_url, @call_sign,
	@end_date, @federal_register_notice, @gross_registered_tonnage,
	@gross_tonnage, @license_policy,
	@license_requirement, @standard_order, @start_date,
	@title, @vessel_flag, @vessel_owner, @vessel_type,
	@id
	)
	SET  @id_result=SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[SelectIdenticalIds]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SelectIdenticalIds]
	@type nvarchar(max),
	@number nvarchar(max)
AS
BEGIN
	SELECT id_ids FROM ids WHERE [type] = @type AND [number] = @number
END
GO
/****** Object:  StoredProcedure [dbo].[XSort_Address]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[XSort_Address] 
	@country nvarchar(max)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO x_address_process (id_address)
	SELECT id_address
	FROM addresses WHERE country=@country
END
GO
/****** Object:  StoredProcedure [dbo].[XSort_dates_of_birth]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[XSort_dates_of_birth] 
	@id_dates_od_birth int
AS
BEGIN
	INSERT INTO x_dates_of_birth_process
	(
	id_dates_od_birth
	) VALUES (
	@id_dates_od_birth)
END
GO
/****** Object:  StoredProcedure [dbo].[XSort_license]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[XSort_license]
@include binary
AS
BEGIN
IF @include=1
BEGIN
SET NOCOUNT ON;
	INSERT INTO x_result_license (id_result)
	SELECT id_result
	FROM results WHERE license_policy IS NOT NULL
END
	ELSE
	BEGIN
	SET NOCOUNT ON;
	INSERT INTO x_result_license (id_result)
	SELECT id_result
	FROM results WHERE license_policy IS NULL
	END
END
GO
/****** Object:  StoredProcedure [dbo].[XSort_result_date]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[XSort_result_date] 
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO x_results_date (id_result)
	SELECT id_result
	FROM results WHERE ((end_date BETWEEN '2023-01-01' AND '2024-01-01'))
END
GO
/****** Object:  StoredProcedure [dbo].[XSort_result_ids]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[XSort_result_ids]
@type nvarchar(max),
@include binary
AS
BEGIN
IF @include=1
BEGIN
	SET NOCOUNT ON;
	INSERT INTO x_result_ids (id_list)
	SELECT id_list
	FROM list INNER JOIN ids ON ids.id_ids=list.id_ids INNER JOIN results ON results.id_result=list.id_result
	WHERE ids.type=@type
	END
    ELSE
    BEGIN
	INSERT INTO x_result_ids (id_list)
        SELECT id_list
        FROM list
        WHERE list.id_result NOT IN (
            SELECT list.id_result
            FROM list
            INNER JOIN ids ON ids.id_ids = list.id_ids
            WHERE ids.type = @type)
	END
END
GO
/****** Object:  StoredProcedure [dbo].[XSort_result_programs]    Script Date: 21.08.2023 15:12:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[XSort_result_programs]
@type nvarchar(max),
@include binary
AS
BEGIN
IF @include=1
BEGIN
	SET NOCOUNT ON;
	INSERT INTO x_result_program (id_result)
	SELECT id_result
	FROM  programs 
	WHERE  programs.program=@type
	END
    ELSE
	 BEGIN
	INSERT INTO x_result_program (id_result)
        SELECT id_result
	FROM programs
	WHERE id_result NOT IN (
            SELECT programs.id_result
            FROM programs
            WHERE program = @type)
     END
END
GO
USE [master]
GO
ALTER DATABASE [Companies] SET  READ_WRITE 
GO
