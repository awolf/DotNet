﻿USE [master]
GO
/****** Object:  Database [Pixel]    Script Date: 2/4/2013 10:29:46 AM ******/
CREATE DATABASE [Pixel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Pixel', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Pixel.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Pixel_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Pixel_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Pixel] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Pixel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Pixel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Pixel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Pixel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Pixel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Pixel] SET ARITHABORT OFF 
GO
ALTER DATABASE [Pixel] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Pixel] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Pixel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Pixel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Pixel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Pixel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Pixel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Pixel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Pixel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Pixel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Pixel] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Pixel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Pixel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Pixel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Pixel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Pixel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Pixel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Pixel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Pixel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Pixel] SET  MULTI_USER 
GO
ALTER DATABASE [Pixel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Pixel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Pixel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Pixel] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Pixel]
GO
/****** Object:  Table [dbo].[Requests]    Script Date: 2/4/2013 10:29:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Requests](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[Url] [varchar](2000) NOT NULL,
	[Code] [int] NULL,
	[UserAgent] [varchar](2000) NOT NULL,
	[UserId] [varchar](500) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_Requests] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Requests] ADD  CONSTRAINT [DF_Requests_Created]  DEFAULT (getutcdate()) FOR [Created]
GO
USE [master]
GO
ALTER DATABASE [Pixel] SET  READ_WRITE 
GO
