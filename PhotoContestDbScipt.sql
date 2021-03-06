USE [master]
GO
/****** Object:  Database [PhotoContestDB]    Script Date: 6/7/2021 6:00:40 PM ******/
CREATE DATABASE [PhotoContestDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PhotoContestDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PhotoContestDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PhotoContestDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\PhotoContestDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PhotoContestDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PhotoContestDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PhotoContestDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PhotoContestDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PhotoContestDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PhotoContestDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PhotoContestDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PhotoContestDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PhotoContestDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PhotoContestDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PhotoContestDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PhotoContestDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PhotoContestDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PhotoContestDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PhotoContestDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PhotoContestDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PhotoContestDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PhotoContestDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PhotoContestDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PhotoContestDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PhotoContestDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PhotoContestDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PhotoContestDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [PhotoContestDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PhotoContestDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PhotoContestDB] SET  MULTI_USER 
GO
ALTER DATABASE [PhotoContestDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PhotoContestDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PhotoContestDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PhotoContestDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PhotoContestDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PhotoContestDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PhotoContestDB] SET QUERY_STORE = OFF
GO
USE [PhotoContestDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/7/2021 6:00:41 PM ******/
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
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NOT NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[LastName] [nvarchar](20) NOT NULL,
	[RankId] [uniqueidentifier] NOT NULL,
	[OverallPoints] [int] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [uniqueidentifier] NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contests]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contests](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[StatusId] [uniqueidentifier] NOT NULL,
	[IsOpen] [bit] NOT NULL,
	[Phase1] [datetime2](7) NOT NULL,
	[Phase2] [datetime2](7) NOT NULL,
	[Finished] [datetime2](7) NOT NULL,
	[IsCalculated] [bit] NOT NULL,
 CONSTRAINT [PK_Contests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Juries]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Juries](
	[UserId] [uniqueidentifier] NOT NULL,
	[ContestId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Juries] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ContestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[PhotoUrl] [nvarchar](max) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ContestId] [uniqueidentifier] NOT NULL,
	[AllPoints] [float] NOT NULL,
	[IsInWrongCategory] [bit] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ranks]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ranks](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Ranks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reviews]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reviews](
	[Id] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[ModifiedOn] [datetime2](7) NOT NULL,
	[DeletedOn] [datetime2](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[Comment] [nvarchar](50) NOT NULL,
	[Score] [float] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[PhotoId] [uniqueidentifier] NOT NULL,
	[WrongCategory] [bit] NOT NULL,
 CONSTRAINT [PK_Reviews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserContests]    Script Date: 6/7/2021 6:00:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserContests](
	[UserId] [uniqueidentifier] NOT NULL,
	[ContestId] [uniqueidentifier] NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[Points] [int] NOT NULL,
	[IsAdded] [bit] NOT NULL,
	[IsInvited] [bit] NOT NULL,
	[HasUploadedPhoto] [bit] NOT NULL,
 CONSTRAINT [PK_UserContests] PRIMARY KEY CLUSTERED 
(
	[ContestId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210607103812_initial', N'3.1.15')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8a73d7c7-c092-4281-8cde-6dd9a9dd747c', N'Organizer', N'ORGANIZER', N'25fdc58f-b7d9-4c33-b011-80f5fccf56eb')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a117e076-855e-401a-aeab-844fee43a0a2', N'User', N'USER', N'6ef7a759-5dab-4766-84d2-bb436e4c130a')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'd0a458f4-cba3-4e49-a779-f79a9de41268', N'Admin', N'ADMIN', N'01a97938-dfc8-44ec-ba99-f005eccfdbab')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e240edfc-64b9-4358-a869-5aadb719e128', N'8a73d7c7-c092-4281-8cde-6dd9a9dd747c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'8a73d7c7-c092-4281-8cde-6dd9a9dd747c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a890fe35-c840-4484-bd80-67dbc94ab581', N'8a73d7c7-c092-4281-8cde-6dd9a9dd747c')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'8a20e519-66ad-46b8-b6c3-18c36fa50a1d', N'a117e076-855e-401a-aeab-844fee43a0a2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'56763358-b113-4f96-9a4a-5190c421f1fb', N'a117e076-855e-401a-aeab-844fee43a0a2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'743f0e66-af28-48b9-8322-61395c10207f', N'a117e076-855e-401a-aeab-844fee43a0a2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7cc9804e-2106-4943-994d-91be3d1fab8e', N'a117e076-855e-401a-aeab-844fee43a0a2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c463712b-e235-4fe5-840e-a99736c3fb76', N'a117e076-855e-401a-aeab-844fee43a0a2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'021fa300-ffd4-48e2-a93f-d40c17d014f3', N'a117e076-855e-401a-aeab-844fee43a0a2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'71cd9097-0c95-4af2-9e43-da7324880583', N'a117e076-855e-401a-aeab-844fee43a0a2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'1d4c48e4-8870-417b-8ac6-e78efe1aaab5', N'd0a458f4-cba3-4e49-a779-f79a9de41268')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'8a20e519-66ad-46b8-b6c3-18c36fa50a1d', N'georgi.ivanov@mail.com', N'GEORGI.IVANOV@MAIL.COM', N'georgi.ivanov@mail.com', N'GEORGI.IVANOV@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEMCO2ANpt3kzJ8pHq3j04457k+GlcVRArIJmwqbKnQq2DgP2U6d7KNl3u2og0g0OlQ==', N'DC6E275DD1E24957A7781D42BB68292B', N'8174298b-1152-4348-909e-3e47fb3c68ee', NULL, 0, 0, NULL, 1, 0, N'Georgi', N'Ivanov', N'41c8e397-f768-48ed-b8f1-f8a238c739b1', 51, CAST(N'2021-06-07T10:38:12.2355758' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'56763358-b113-4f96-9a4a-5190c421f1fb', N'sam.stevens@mail.com', N'SAM.STEVENS@MAIL.COM', N'sam.stevens@mail.com', N'SAM.STEVENS@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEJx/M5QdYM+IUzO54onLfmkXPMv40BoMkv7gqrTb3gOOzZy9LY6z5qH3CB10pCMQ+Q==', N'DC6E375DD1E25957A7981D48BB68399B', N'9ff34874-6e61-4bb8-9a0d-780f1af43202', NULL, 0, 0, NULL, 1, 0, N'Sam', N'Stevens', N'a9576301-3157-454f-86ce-85bb5eb2dfc9', 287, CAST(N'2021-06-07T10:38:12.2355826' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'e240edfc-64b9-4358-a869-5aadb719e128', N'eric.berg@mail.com', N'ERIC.BERG@MAIL.COM', N'eric.berg@mail.com', N'ERIC.BERG@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEK0AeLECINAtVnkLhL63Xr3WEDLa3eGifPH7WfXLWE0Jqou7/MCWhhlZRIkxVLuB4Q==', N'LI6YBFWUC775KZAW6NDZPBAJSSESA5KX', N'2efd25b9-bad0-4eed-acd5-45b36622572b', N'0898636022', 0, 0, NULL, 1, 0, N'Eric', N'Berg', N'0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce', 0, CAST(N'2021-06-07T10:38:12.2355724' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'743f0e66-af28-48b9-8322-61395c10207f', N'steven.king@mail.com', N'STEVEN.KING@MAIL.COM', N'steven.king@mail.com', N'STEVEN.KING@MAIL.COM', 0, N'AQAAAAEAACcQAAAAECiYYSUvMdm72zbPzwUKw91lRRsJL7u1cD3Ary8A+WfwmCN+MUURcVZKsarakG/udQ==', N'DC6E375DD1E25957A7781D42BB68299B', N'6d5474ff-b9b5-4993-a363-01d086efba01', NULL, 0, 0, NULL, 1, 0, N'Steven', N'King', N'41c8e397-f768-48ed-b8f1-f8a238c739b1', 87, CAST(N'2021-06-07T10:38:12.2355779' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'sara.smith@mail.com', N'SARA.SMITH@MAIL.COM', N'sara.smith@mail.com', N'SARA.SMITH@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEIK37g2G3RuhXWZDTlB6VCi/V8U2ftZYo6AqYQLev1l6YmcAwlKS05KYqiLIvpC4jQ==', N'DC6E275DD1E24917A7781D42BB68293B', N'a34e30f2-b301-498b-89e5-4d976ad13f62', NULL, 0, 0, NULL, 1, 0, N'Sara', N'Smith', N'0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce', 0, CAST(N'2021-06-07T10:38:12.2356314' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'a890fe35-c840-4484-bd80-67dbc94ab581', N'jane.beck@mail.com', N'JANE.BECK@MAIL.COM', N'jane.beck@mail.com', N'JANE.BECK@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEKvdXWgfjZziBbYt+mSeXlgpqkk3gs2iwDkyaDcAi5RZ5sz8C7BWW4ExleUsCcUUDw==', N'DC6E275DD1E24917A7781D42BB64293B', N'1ddcf709-072f-4290-aa27-4a2f0baa4522', NULL, 0, 0, NULL, 1, 0, N'Jane', N'Beck', N'0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce', 0, CAST(N'2021-06-07T10:38:12.2356327' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'7cc9804e-2106-4943-994d-91be3d1fab8e', N'jimmy.brown@mail.com', N'JIMMY.BROWN@MAIL.COM', N'jimmy.brown@mail.com', N'JIMMY.BROWN@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEOrPMC3iG47l8QlmKWR8lNDodvPRUiz8od5aysTEPGhSymTVlbl4NXWtSyqtz9tI5A==', N'DC6E375DD1E25957A7981D42BB68399B', N'7bc1d264-302c-4bea-8778-5df591473a9c', NULL, 0, 0, NULL, 1, 0, N'Jimmy', N'Brown', N'acca215b-d737-406c-b87c-696fb22ce001', 36, CAST(N'2021-06-07T10:38:12.2355813' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'c463712b-e235-4fe5-840e-a99736c3fb76', N'kyle.sins@mail.com', N'KYLE.SINS@MAIL.COM', N'kyle.sins@mail.com', N'KYLE.SINS@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEI6HbqVv/yxLwmk9vy2bY6RuZt9YD4Yx4zNRTsHuweBObTES+n86tVWrNwIzQelncw==', N'DC6E375DD2E25957A7981D48BB68399B', N'de1028dd-4516-47cd-86ee-fea6c60445bc', NULL, 0, 0, NULL, 1, 0, N'Kyle', N'Sins', N'0b1728c7-5582-4958-9e97-52c9b1d44cdb', 1323, CAST(N'2021-06-07T10:38:12.2356275' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'021fa300-ffd4-48e2-a93f-d40c17d014f3', N'john.smith@mail.com', N'JOHN.SMITH@MAIL.COM', N'john.smith@mail.com', N'JOHN.SMITH@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEGtlT3Ao02+bGZYgHYhAaPY0V1HtEYL8us7n3a/QBobkrnNpyfUcNpbaM5F7m4uElQ==', N'DC6E275DD1E25957A7781D42BB68299B', N'0f04daae-d762-481d-89ff-70a0e7887165', NULL, 0, 0, NULL, 1, 0, N'John', N'Smith', N'41c8e397-f768-48ed-b8f1-f8a238c739b1', 127, CAST(N'2021-06-07T10:38:12.2355771' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'71cd9097-0c95-4af2-9e43-da7324880583', N'robert.scott@mail.com', N'ROBERT.SCOTT@MAIL.COM', N'robert.scott@mail.com', N'ROBERT.SCOTT@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEMLHtlzGqU7OTsROjpEHGgxB3lW9HpxFt36gNFddfMwCCgSw3MpYisvmjsUCOSJULw==', N'DC6E375DD1E25957A7981D42BB68299B', N'dd053f34-565d-44ec-a2ee-206a73053fdd', NULL, 0, 0, NULL, 1, 0, N'Robert', N'Scott', N'acca215b-d737-406c-b87c-696fb22ce001', 36, CAST(N'2021-06-07T10:38:12.2355805' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [RankId], [OverallPoints], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted]) VALUES (N'1d4c48e4-8870-417b-8ac6-e78efe1aaab5', N'admin@mail.com', N'ADMIN@MAIL.COM', N'admin@mail.com', N'ADMIN@MAIL.COM', 0, N'AQAAAAEAACcQAAAAEBooFqmj7pXP1ueCHUPcPGjIMJzY0ZZlIi1guRWDu+JWkdYeX/zvXxqPxJLPDOLG8w==', N'DC6E275DD1E24957A7781D42BB68299B', N'1d10dcda-950a-4659-be25-233553e62d34', NULL, 0, 0, NULL, 1, 0, N'Admin', N'Admin', N'a036e464-8996-4e40-9a81-39239cf72402', 0, CAST(N'2021-06-07T10:38:12.2353816' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0)
GO
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (N'fad09db4-8187-4777-9e68-3ba40218c7d3', CAST(N'2021-06-07T10:38:12.3286938' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Motorcycles')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (N'28f87c5a-b02a-4422-8b71-4821306279d5', CAST(N'2021-06-07T10:38:12.3286951' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Sports')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (N'e43364e1-28d4-48c3-b4e2-5bd5f43b89b0', CAST(N'2021-06-07T10:38:12.3286276' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Cars')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (N'a8c71a51-79f2-46ef-8a88-7983cbb7259a', CAST(N'2021-06-07T10:38:12.3286947' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'People')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (N'af4ea8a0-8e69-4746-bbc8-aa4593a11828', CAST(N'2021-06-07T10:38:12.3286938' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Architecture')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (N'729b970a-ee54-4852-8ac7-d9b3146e886b', CAST(N'2021-06-07T10:38:12.3286904' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Animals')
INSERT [dbo].[Categories] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name]) VALUES (N'ad729c24-eda2-4209-93c8-f80d1f47172c', CAST(N'2021-06-07T10:38:12.3286934' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Nature')
GO
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'f36e97ee-98af-4f26-93ef-066895d94b2a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Wild cats', N'729b970a-ee54-4852-8ac7-d9b3146e886b', N'27c7d81e-eb1c-469b-8919-a532322273cc', 1, CAST(N'2021-05-10T09:00:00.0000000' AS DateTime2), CAST(N'2021-06-07T10:38:12.3296415' AS DateTime2), CAST(N'2021-06-08T10:38:12.3296415' AS DateTime2), 0)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'fd4ec4c7-3976-4a44-9dfa-06967ab471c1', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Snakes', N'729b970a-ee54-4852-8ac7-d9b3146e886b', N'9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186', 1, CAST(N'2021-06-07T10:38:12.3296372' AS DateTime2), CAST(N'2021-06-15T09:00:00.0000000' AS DateTime2), CAST(N'2021-06-15T19:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'b2ba5698-bc3a-45ee-8dcb-06e6a2e99a09', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Muscle cars', N'e43364e1-28d4-48c3-b4e2-5bd5f43b89b0', N'9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186', 1, CAST(N'2021-06-07T10:38:12.3296385' AS DateTime2), CAST(N'2021-06-16T09:00:00.0000000' AS DateTime2), CAST(N'2021-06-16T19:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'c5d76f38-f3f9-4aaa-931f-0d91e0207a0a', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Lakes', N'ad729c24-eda2-4209-93c8-f80d1f47172c', N'9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186', 1, CAST(N'2021-06-07T10:38:12.3296393' AS DateTime2), CAST(N'2021-06-17T09:00:00.0000000' AS DateTime2), CAST(N'2021-06-17T19:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'd7b46312-7197-4c79-8384-1ec2b8577f8d', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Historic landmarks', N'af4ea8a0-8e69-4746-bbc8-aa4593a11828', N'cf6bf4fb-655e-47cc-8dac-4a39cbff74b6', 1, CAST(N'2021-05-10T09:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T12:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T09:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'548873db-705b-46e7-b88d-230c5f06fd35', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Best look', N'fad09db4-8187-4777-9e68-3ba40218c7d3', N'cf6bf4fb-655e-47cc-8dac-4a39cbff74b6', 1, CAST(N'2021-05-15T09:00:00.0000000' AS DateTime2), CAST(N'2021-05-25T12:00:00.0000000' AS DateTime2), CAST(N'2021-05-26T09:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'42541f52-8d30-4828-bf66-4eda82735edd', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Mansions', N'af4ea8a0-8e69-4746-bbc8-aa4593a11828', N'cf6bf4fb-655e-47cc-8dac-4a39cbff74b6', 1, CAST(N'2021-05-10T09:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T12:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T09:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'17578ed3-fdfc-4616-9cdb-55a5ff762caf', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Couples', N'a8c71a51-79f2-46ef-8a88-7983cbb7259a', N'9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186', 1, CAST(N'2021-06-07T10:38:12.3296406' AS DateTime2), CAST(N'2021-06-18T09:00:00.0000000' AS DateTime2), CAST(N'2021-06-18T19:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'd2acdd9c-9427-4fc2-897e-5f52da2190dc', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Olympics', N'28f87c5a-b02a-4422-8b71-4821306279d5', N'cf6bf4fb-655e-47cc-8dac-4a39cbff74b6', 1, CAST(N'2021-05-10T09:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T12:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T09:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'06e8bf71-fc93-42ff-8c99-a5265a8ea2e9', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Sunsets', N'ad729c24-eda2-4209-93c8-f80d1f47172c', N'cf6bf4fb-655e-47cc-8dac-4a39cbff74b6', 1, CAST(N'2021-05-10T09:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T12:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T09:00:00.0000000' AS DateTime2), 1)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'f8d22e50-664b-483f-a542-ab26135e6772', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Skyscrapers', N'af4ea8a0-8e69-4746-bbc8-aa4593a11828', N'9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186', 1, CAST(N'2021-06-07T10:38:12.3296338' AS DateTime2), CAST(N'2021-06-14T09:00:00.0000000' AS DateTime2), CAST(N'2021-06-14T19:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'e2450bf8-c019-4442-a2c3-ed0210586eed', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Birds', N'729b970a-ee54-4852-8ac7-d9b3146e886b', N'9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186', 1, CAST(N'2021-06-07T10:38:12.3294405' AS DateTime2), CAST(N'2021-06-10T09:00:00.0000000' AS DateTime2), CAST(N'2021-06-10T19:00:00.0000000' AS DateTime2), 0)
INSERT [dbo].[Contests] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Name], [CategoryId], [StatusId], [IsOpen], [Phase1], [Phase2], [Finished], [IsCalculated]) VALUES (N'f9f89a56-448a-43c4-a098-fe5b13605999', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Bridges', N'af4ea8a0-8e69-4746-bbc8-aa4593a11828', N'cf6bf4fb-655e-47cc-8dac-4a39cbff74b6', 1, CAST(N'2021-05-10T09:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T12:00:00.0000000' AS DateTime2), CAST(N'2021-05-20T09:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'05505a4d-f749-46e9-928c-039dad92c808', CAST(N'2021-06-07T10:38:12.3300306' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Michael Phelps', N'Michael Phelps winning gold', N'/Images/71a3e79d-ee0e-4d4b-9a03-2890c42be96a_olympics2.jpg', N'71cd9097-0c95-4af2-9e43-da7324880583', N'd2acdd9c-9427-4fc2-897e-5f52da2190dc', 6, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'0fdb02e1-91e1-4132-9ccc-1f73c7f716b9', CAST(N'2021-06-07T10:38:12.3300221' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Tiger', N'Going for the kill.', N'/Images/da80d3a0-2aaa-4360-871e-699b5507277f_tiger1.jpg', N'56763358-b113-4f96-9a4a-5190c421f1fb', N'f36e97ee-98af-4f26-93ef-066895d94b2a', 0, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'94499cdd-e18c-4743-b0c4-2e1b7564c46c', CAST(N'2021-06-07T10:38:12.3300272' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Eagle', N'Scanning for prey.', N'/Images/1d2dfd54-7d9b-41c4-b442-995baa1ac289_eagle1.jpg', N'71cd9097-0c95-4af2-9e43-da7324880583', N'e2450bf8-c019-4442-a2c3-ed0210586eed', 0, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'08f2b9a9-37a8-4852-86c1-4058a62848a9', CAST(N'2021-06-07T10:38:12.3300417' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Erasmusbrug', N'Man made beauty', N'/Images/b364f4aa-a6af-4570-966f-f1980c6eb636_bridges1.jpeg', N'8a20e519-66ad-46b8-b6c3-18c36fa50a1d', N'f9f89a56-448a-43c4-a098-fe5b13605999', 9, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'fd4b4d23-a4db-4e8b-be63-4af3c4b45757', CAST(N'2021-06-07T10:38:12.3300238' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Kawasaki', N'Parked for the day.', N'/Images/16bb1fa0-8f61-4717-bba1-e14f8c47b616_kawasaki1.jpg', N'021fa300-ffd4-48e2-a93f-d40c17d014f3', N'548873db-705b-46e7-b88d-230c5f06fd35', 9, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'7780497c-f5e4-43e2-8e90-5067f91475ce', CAST(N'2021-06-07T10:38:12.3300298' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Usain Bolt', N'Usain Bolt breaking the record', N'/Images/cb2acc2c-2576-4707-86ae-754e983e1f55_olympics1.jpg', N'56763358-b113-4f96-9a4a-5190c421f1fb', N'd2acdd9c-9427-4fc2-897e-5f52da2190dc', 7.5, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'723b8f6d-95de-487f-a4b2-52f8be99ce11', CAST(N'2021-06-07T10:38:12.3300435' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Palm house', N'Great looking family house', N'/Images/0eaa5bc4-a7b1-41e2-8fe9-543cf9fa2e46_mansions.jpg', N'743f0e66-af28-48b9-8322-61395c10207f', N'42541f52-8d30-4828-bf66-4eda82735edd', 8.5, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'8a178e81-d2c3-44bb-a3d7-79d84f5188b2', CAST(N'2021-06-07T10:38:12.3300400' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'The Colloseum', N'The mighty arena of gladiators', N'/Images/a6e65cfd-81dd-4c64-aa05-11946f97b102_landmarks1.jpg', N'743f0e66-af28-48b9-8322-61395c10207f', N'd7b46312-7197-4c79-8384-1ec2b8577f8d', 5, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'5edcd3ee-3033-43fe-a222-97fdac5922ab', CAST(N'2021-06-07T10:38:12.3300281' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'African sunset', N'Beautiful sunset in Kenya', N'/Images/ccf1a5e0-b616-4e99-a524-6691e79ca16b_sunset1.jpg', N'c463712b-e235-4fe5-840e-a99736c3fb76', N'06e8bf71-fc93-42ff-8c99-a5265a8ea2e9', 9, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'2b7fb35a-9840-4d4b-b86e-a5e3040117e4', CAST(N'2021-06-07T10:38:12.3300426' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Goldengate', N'Engineering genius', N'/Images/52dc2b1e-112c-4914-98b9-13137abd989a_bridges2.jpeg', N'c463712b-e235-4fe5-840e-a99736c3fb76', N'f9f89a56-448a-43c4-a098-fe5b13605999', 7, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'e165b91f-03bf-414e-88b7-c51b87775683', CAST(N'2021-06-07T10:38:12.3300007' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Lion King', N'Roaring of a lion.', N'/Images/c4baabc4-bd02-4bd6-bb33-955556530c8e_lion1.jpg', N'8a20e519-66ad-46b8-b6c3-18c36fa50a1d', N'f36e97ee-98af-4f26-93ef-066895d94b2a', 0, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'44e5ce67-461c-4082-9acb-c5aceae13a0c', CAST(N'2021-06-07T10:38:12.3300289' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'New York sunset', N'Sunset over the Statue of Liberty', N'/Images/7ef91ce8-58f5-416d-a8c2-56d0218e6ebf_sunset2.jpg', N'7cc9804e-2106-4943-994d-91be3d1fab8e', N'06e8bf71-fc93-42ff-8c99-a5265a8ea2e9', 6.5, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'507c5f65-497b-4a3c-95f6-cfbc86692ca5', CAST(N'2021-06-07T10:38:12.3300246' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Honda CBR', N'Takaing a small break from the road.', N'/Images/dcc53eb5-2024-4242-877c-423e9c0d751f_honda1.jpg', N'c463712b-e235-4fe5-840e-a99736c3fb76', N'548873db-705b-46e7-b88d-230c5f06fd35', 3, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'94da69ad-71c2-4dca-87a9-def9154ec7b0', CAST(N'2021-06-07T10:38:12.3300443' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Mountain villa', N'Nothing like fresh mountain air', N'/Images/c77ff246-1712-4ab3-84cf-15b25966aebd_mansions2.jpg', N'56763358-b113-4f96-9a4a-5190c421f1fb', N'42541f52-8d30-4828-bf66-4eda82735edd', 5.5, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'59dd9540-a1d8-4360-99d5-ed8302aae5e2', CAST(N'2021-06-07T10:38:12.3300255' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Hummingbird', N'Hummingbird on a branch.', N'/Images/3a425bea-85d7-4473-850b-afb9162dfe7e_colibri1.jpg', N'7cc9804e-2106-4943-994d-91be3d1fab8e', N'e2450bf8-c019-4442-a2c3-ed0210586eed', 0, 0)
INSERT [dbo].[Photos] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Title], [Description], [PhotoUrl], [UserId], [ContestId], [AllPoints], [IsInWrongCategory]) VALUES (N'351b9dbe-4142-4283-bbc4-f90a8a503925', CAST(N'2021-06-07T10:38:12.3300409' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'The Pyramids', N'The last standing wonder of the old world', N'/Images/a562ac79-7f5e-48e7-a364-43cc656227bb_landmarks2.jpg', N'021fa300-ffd4-48e2-a93f-d40c17d014f3', N'd7b46312-7197-4c79-8384-1ec2b8577f8d', 5.5, 0)
GO
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'a036e464-8996-4e40-9a81-39239cf72402', N'Admin')
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'0b1728c7-5582-4958-9e97-52c9b1d44cdb', N'Wise and Benevolent Photo Dictator')
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'acca215b-d737-406c-b87c-696fb22ce001', N'Junkie')
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'a9576301-3157-454f-86ce-85bb5eb2dfc9', N'Master')
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'0e4ac61d-7d3b-4dcb-9ed0-d47cf1c247ce', N'Organizer')
INSERT [dbo].[Ranks] ([Id], [Name]) VALUES (N'41c8e397-f768-48ed-b8f1-f8a238c739b1', N'Enthusiast')
GO
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'df3841f6-5e8c-4cfe-88eb-09b0e0daaa35', CAST(N'2021-06-07T10:38:12.3304313' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Great place, would love to be there.', 10, N'e240edfc-64b9-4358-a869-5aadb719e128', N'723b8f6d-95de-487f-a4b2-52f8be99ce11', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'73fe1a7a-e31c-4b4e-a6fa-1ae65e7e1f28', CAST(N'2021-06-07T10:38:12.3304159' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Perfect shot.', 10, N'e240edfc-64b9-4358-a869-5aadb719e128', N'0fdb02e1-91e1-4132-9ccc-1f73c7f716b9', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'0fb89447-6c1d-4fc7-a93d-29a7c17b5458', CAST(N'2021-06-07T10:38:12.3304326' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Picture is taken for very far.', 4, N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'94da69ad-71c2-4dca-87a9-def9154ec7b0', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'b8e942fb-9e23-48b2-b15f-32a1e2c06315', CAST(N'2021-06-07T10:38:12.3304168' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Shadows are in the way.', 3, N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'0fdb02e1-91e1-4132-9ccc-1f73c7f716b9', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'2ab3c5bb-a6fc-4c08-b9d6-3e093d4a20f0', CAST(N'2021-06-07T10:38:12.3304300' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Very nice colors.', 8, N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'2b7fb35a-9840-4d4b-b86e-a5e3040117e4', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'98deadcb-ffb0-47c7-ac06-4dd5c8fc22fa', CAST(N'2021-06-07T10:38:12.3304219' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Great quality.', 8, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'7780497c-f5e4-43e2-8e90-5067f91475ce', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'd9beb14d-56c8-42d1-a6b8-5c2a5d0c1615', CAST(N'2021-06-07T10:38:12.3304228' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Very good shot.', 7, N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'7780497c-f5e4-43e2-8e90-5067f91475ce', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'958d719a-7112-4116-bdbb-5eb6f374f4cf', CAST(N'2021-06-07T10:38:12.3304292' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Spectacular shot', 10, N'e240edfc-64b9-4358-a869-5aadb719e128', N'08f2b9a9-37a8-4852-86c1-4058a62848a9', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'a15576fd-9e09-4b2a-b961-60b9204e803b', CAST(N'2021-06-07T10:38:12.3304194' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Unique colors.', 8, N'e240edfc-64b9-4358-a869-5aadb719e128', N'5edcd3ee-3033-43fe-a222-97fdac5922ab', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'7e2c2b98-9616-479a-bd1f-61d0510ccb15', CAST(N'2021-06-07T10:38:12.3304211' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Great perspective.', 8, N'e240edfc-64b9-4358-a869-5aadb719e128', N'44e5ce67-461c-4082-9acb-c5aceae13a0c', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'e1529b05-cc4d-4eef-9bae-718b5acc4784', CAST(N'2021-06-07T10:38:12.3304258' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Quality of the picture is not very good.', 4, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'8a178e81-d2c3-44bb-a3d7-79d84f5188b2', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'55cf8205-bfb9-4d8c-8ac1-7861a458bb10', CAST(N'2021-06-07T10:38:12.3304176' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Great shot.', 9, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'fd4b4d23-a4db-4e8b-be63-4af3c4b45757', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'f55244de-da0f-4a9c-b8d9-7940a2f97083', CAST(N'2021-06-07T10:38:12.3304010' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Great angle.', 6, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'e165b91f-03bf-414e-88b7-c51b87775683', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'ef107b97-bc8c-44c1-9b54-80744a8e9ec5', CAST(N'2021-06-07T10:38:12.3304335' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Nice colors.', 7, N'e240edfc-64b9-4358-a869-5aadb719e128', N'94da69ad-71c2-4dca-87a9-def9154ec7b0', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'9c211c22-7b53-4395-a6ab-8df8ea3cf774', CAST(N'2021-06-07T10:38:12.3304322' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Great shot.', 7, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'723b8f6d-95de-487f-a4b2-52f8be99ce11', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'46687750-d00c-419c-933e-9b8e8d6f1db6', CAST(N'2021-06-07T10:38:12.3304241' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Not a very clean shot.', 5, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'05505a4d-f749-46e9-928c-039dad92c808', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'8198e13a-30cb-4f4b-99f0-acf31a70b02d', CAST(N'2021-06-07T10:38:12.3304146' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Perfect timing.', 8, N'e240edfc-64b9-4358-a869-5aadb719e128', N'e165b91f-03bf-414e-88b7-c51b87775683', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'9bf16f23-4764-4e31-9716-ad756a0761ff', CAST(N'2021-06-07T10:38:12.3304249' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Good setting.', 6, N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'8a178e81-d2c3-44bb-a3d7-79d84f5188b2', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'18d39ed7-120f-4239-8920-b0b826dd3d0a', CAST(N'2021-06-07T10:38:12.3304189' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Mesmerizing shot.', 10, N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'5edcd3ee-3033-43fe-a222-97fdac5922ab', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'3969dc5b-b149-41aa-a17c-b5ec903aa264', CAST(N'2021-06-07T10:38:12.3304283' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Unique setting', 8, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'08f2b9a9-37a8-4852-86c1-4058a62848a9', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'5405c1fb-ab2e-4ada-95f2-c37436d3e24c', CAST(N'2021-06-07T10:38:12.3304202' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Not really shot of a sunset.', 5, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'44e5ce67-461c-4082-9acb-c5aceae13a0c', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'c625e60d-0daa-4b2f-8c9c-d2733495293b', CAST(N'2021-06-07T10:38:12.3304236' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Great timing.', 7, N'e240edfc-64b9-4358-a869-5aadb719e128', N'05505a4d-f749-46e9-928c-039dad92c808', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'9d520a28-1e1b-4477-9894-e0da97fa982e', CAST(N'2021-06-07T10:38:12.3304275' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Not the greatest quality.', 6, N'5d608fdc-f7d4-40f2-b052-61a7ea812a23', N'351b9dbe-4142-4283-bbc4-f90a8a503925', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'96ea7398-6e64-4735-a252-e1a6a18094a7', CAST(N'2021-06-07T10:38:12.3304266' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'The pyramids are not the center of the picture.', 5, N'e240edfc-64b9-4358-a869-5aadb719e128', N'351b9dbe-4142-4283-bbc4-f90a8a503925', 0)
INSERT [dbo].[Reviews] ([Id], [CreatedOn], [ModifiedOn], [DeletedOn], [IsDeleted], [Comment], [Score], [UserId], [PhotoId], [WrongCategory]) VALUES (N'6c104a76-98ee-4bc5-9cd9-fde937d155e9', CAST(N'2021-06-07T10:38:12.3304309' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'Marvelous shot.', 6, N'a890fe35-c840-4484-bd80-67dbc94ab581', N'2b7fb35a-9840-4d4b-b86e-a5e3040117e4', 0)
GO
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (N'cf6bf4fb-655e-47cc-8dac-4a39cbff74b6', N'Finished')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (N'27c7d81e-eb1c-469b-8919-a532322273cc', N'Phase 2')
INSERT [dbo].[Statuses] ([Id], [Name]) VALUES (N'9dd48e5a-f5f5-4b90-ad93-e0a5ad62e186', N'Phase 1')
GO
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'8a20e519-66ad-46b8-b6c3-18c36fa50a1d', N'f36e97ee-98af-4f26-93ef-066895d94b2a', N'be9c8856-5df8-4577-a7c9-f8f62f8de22c', 0, 0, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'56763358-b113-4f96-9a4a-5190c421f1fb', N'f36e97ee-98af-4f26-93ef-066895d94b2a', N'61f45846-09fd-4112-b3b8-5aaf029e8a9f', 0, 0, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'743f0e66-af28-48b9-8322-61395c10207f', N'd7b46312-7197-4c79-8384-1ec2b8577f8d', N'863aa575-bc89-4de7-9895-ff8d4f70fea8', 36, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'021fa300-ffd4-48e2-a93f-d40c17d014f3', N'd7b46312-7197-4c79-8384-1ec2b8577f8d', N'd8343994-bcae-4096-ba78-d8b669be0049', 51, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'c463712b-e235-4fe5-840e-a99736c3fb76', N'548873db-705b-46e7-b88d-230c5f06fd35', N'f933eff8-9a79-4937-801a-a80aaa8d4b19', 36, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'021fa300-ffd4-48e2-a93f-d40c17d014f3', N'548873db-705b-46e7-b88d-230c5f06fd35', N'd00fb4ba-c05c-4a48-8042-0db3b747b226', 76, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'56763358-b113-4f96-9a4a-5190c421f1fb', N'42541f52-8d30-4828-bf66-4eda82735edd', N'5d33bf17-933f-4fd4-93ba-8fa2a7944bd1', 36, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'743f0e66-af28-48b9-8322-61395c10207f', N'42541f52-8d30-4828-bf66-4eda82735edd', N'95afb371-52c1-4aa3-85f6-c7341b3155bc', 51, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'56763358-b113-4f96-9a4a-5190c421f1fb', N'd2acdd9c-9427-4fc2-897e-5f52da2190dc', N'd10fb8e0-d659-4cd9-8da9-d84b98e4e8f1', 51, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'71cd9097-0c95-4af2-9e43-da7324880583', N'd2acdd9c-9427-4fc2-897e-5f52da2190dc', N'00aa09e3-43d0-46b0-a213-45d4a72a7b4c', 36, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'7cc9804e-2106-4943-994d-91be3d1fab8e', N'06e8bf71-fc93-42ff-8c99-a5265a8ea2e9', N'ed1dd103-4469-4699-ba7e-59b14805a8a8', 36, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'c463712b-e235-4fe5-840e-a99736c3fb76', N'06e8bf71-fc93-42ff-8c99-a5265a8ea2e9', N'0ea3f579-a3ec-499e-82d8-35ab5c0af28f', 51, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'7cc9804e-2106-4943-994d-91be3d1fab8e', N'e2450bf8-c019-4442-a2c3-ed0210586eed', N'1e1008e0-63f6-437a-8c86-347dcf905b7d', 0, 0, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'71cd9097-0c95-4af2-9e43-da7324880583', N'e2450bf8-c019-4442-a2c3-ed0210586eed', N'bb047135-03e9-4957-8248-306eaf8600cc', 0, 0, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'8a20e519-66ad-46b8-b6c3-18c36fa50a1d', N'f9f89a56-448a-43c4-a098-fe5b13605999', N'3ada8ce7-5c5d-4892-a676-b9d035de1d42', 51, 1, 0, 0)
INSERT [dbo].[UserContests] ([UserId], [ContestId], [Id], [Points], [IsAdded], [IsInvited], [HasUploadedPhoto]) VALUES (N'c463712b-e235-4fe5-840e-a99736c3fb76', N'f9f89a56-448a-43c4-a098-fe5b13605999', N'6b50dbde-50d9-4b5c-9425-1ea4c97a4146', 36, 1, 0, 0)
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUsers_Email]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AspNetUsers_Email] ON [dbo].[AspNetUsers]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AspNetUsers_RankId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUsers_RankId] ON [dbo].[AspNetUsers]
(
	[RankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUsers_UserName]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AspNetUsers_UserName] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Categories_Name]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Categories_Name] ON [dbo].[Categories]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contests_CategoryId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_Contests_CategoryId] ON [dbo].[Contests]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Contests_Name]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Contests_Name] ON [dbo].[Contests]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Contests_StatusId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_Contests_StatusId] ON [dbo].[Contests]
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Juries_ContestId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_Juries_ContestId] ON [dbo].[Juries]
(
	[ContestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Photos_ContestId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_Photos_ContestId] ON [dbo].[Photos]
(
	[ContestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Photos_UserId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_Photos_UserId] ON [dbo].[Photos]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_PhotoId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_PhotoId] ON [dbo].[Reviews]
(
	[PhotoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reviews_UserId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_Reviews_UserId] ON [dbo].[Reviews]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserContests_UserId]    Script Date: 6/7/2021 6:00:41 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserContests_UserId] ON [dbo].[UserContests]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Ranks_RankId] FOREIGN KEY([RankId])
REFERENCES [dbo].[Ranks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Ranks_RankId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Contests]  WITH CHECK ADD  CONSTRAINT [FK_Contests_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Contests] CHECK CONSTRAINT [FK_Contests_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Contests]  WITH CHECK ADD  CONSTRAINT [FK_Contests_Statuses_StatusId] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[Contests] CHECK CONSTRAINT [FK_Contests_Statuses_StatusId]
GO
ALTER TABLE [dbo].[Juries]  WITH CHECK ADD  CONSTRAINT [FK_Juries_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Juries] CHECK CONSTRAINT [FK_Juries_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Juries]  WITH CHECK ADD  CONSTRAINT [FK_Juries_Contests_ContestId] FOREIGN KEY([ContestId])
REFERENCES [dbo].[Contests] ([Id])
GO
ALTER TABLE [dbo].[Juries] CHECK CONSTRAINT [FK_Juries_Contests_ContestId]
GO
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Contests_ContestId] FOREIGN KEY([ContestId])
REFERENCES [dbo].[Contests] ([Id])
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK_Photos_Contests_ContestId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Reviews]  WITH CHECK ADD  CONSTRAINT [FK_Reviews_Photos_PhotoId] FOREIGN KEY([PhotoId])
REFERENCES [dbo].[Photos] ([Id])
GO
ALTER TABLE [dbo].[Reviews] CHECK CONSTRAINT [FK_Reviews_Photos_PhotoId]
GO
ALTER TABLE [dbo].[UserContests]  WITH CHECK ADD  CONSTRAINT [FK_UserContests_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserContests] CHECK CONSTRAINT [FK_UserContests_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[UserContests]  WITH CHECK ADD  CONSTRAINT [FK_UserContests_Contests_ContestId] FOREIGN KEY([ContestId])
REFERENCES [dbo].[Contests] ([Id])
GO
ALTER TABLE [dbo].[UserContests] CHECK CONSTRAINT [FK_UserContests_Contests_ContestId]
GO
USE [master]
GO
ALTER DATABASE [PhotoContestDB] SET  READ_WRITE 
GO
