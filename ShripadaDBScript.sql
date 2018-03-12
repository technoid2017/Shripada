USE [master]
GO
/****** Object:  Database [Shripada]    Script Date: 11/18/2017 10:57:56 ******/
CREATE DATABASE [Shripada] ON  PRIMARY 
( NAME = N'Shripada', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Shripada.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Shripada_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Shripada_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Shripada] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Shripada].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Shripada] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Shripada] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Shripada] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Shripada] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Shripada] SET ARITHABORT OFF
GO
ALTER DATABASE [Shripada] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Shripada] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Shripada] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Shripada] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Shripada] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Shripada] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Shripada] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Shripada] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Shripada] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Shripada] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Shripada] SET  DISABLE_BROKER
GO
ALTER DATABASE [Shripada] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Shripada] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Shripada] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Shripada] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Shripada] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Shripada] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Shripada] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Shripada] SET  READ_WRITE
GO
ALTER DATABASE [Shripada] SET RECOVERY FULL
GO
ALTER DATABASE [Shripada] SET  MULTI_USER
GO
ALTER DATABASE [Shripada] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Shripada] SET DB_CHAINING OFF
GO
USE [Shripada]
GO
/****** Object:  Table [dbo].[ServiceData]    Script Date: 11/18/2017 10:57:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceData](
	[serviceName] [nchar](50) NOT NULL,
	[serviceDescription] [nchar](150) NULL,
	[servicePrice] [decimal](7, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[serviceName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[serialNumber]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[serialNumber]
(
@columnName nchar(20),
@tablename nchar(20)
)
As
begin
Exec('select MAX('+@columnName+')from'+@tablename)
END
GO
/****** Object:  StoredProcedure [dbo].[selectAllProcedure]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[selectAllProcedure]
@tableName varchar(50),
@columnName varchar(50),
@columnValue nchar(50)

AS

begin
declare @cmd as nvarchar(max)
set @cmd = N'select * from ' + @tableName + ' where ' +@columnName+ '= '''+@columnValue+ ''''

exec sp_executesql @cmd

end
GO
/****** Object:  StoredProcedure [dbo].[selectAllLikeProcedure]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[selectAllLikeProcedure]
@tableName varchar(50),
@columnName varchar(50),
@columnValue nchar(50)

AS

begin
declare @cmd as nvarchar(max)
set @cmd = N'select * from ' + @tableName + ' where ' +@columnName+ 'like % '''+@columnValue+ '''%'

exec sp_executesql @cmd

end
GO
/****** Object:  StoredProcedure [dbo].[selectAll8]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[selectAll8]
@tableName varchar(50),
@columnName varchar(50),
@columnValue nchar(50)

AS

begin
declare @cmd as nvarchar(max)
set @cmd = N'select * from ' + @tableName + ' where ' +@columnName+ '= '''+@columnValue+ ''''

exec sp_executesql @cmd

end
GO
/****** Object:  StoredProcedure [dbo].[selectAll7]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[selectAll7]
@tableName varchar(50),
@columnName varchar(50),
@columnValue nchar(50)

AS

begin
declare @cmd as nvarchar(max)
set @cmd = N'select * from ' + @tableName + ' where ' +@columnName+ '= "'+@columnValue+ '"'

exec sp_executesql @cmd

end
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patients](
	[patientID] [nchar](20) NOT NULL,
	[patientName] [nchar](50) NULL,
	[address] [nchar](150) NULL,
	[celnumber] [nchar](25) NULL,
	[age] [int] NULL,
	[sex] [nchar](10) NULL,
	[mediclaim] [nchar](30) NULL,
	[dateOfRegister] [date] NULL,
	[noOfVisits] [int] NULL,
	[srNo] [int] NULL,
	[currentStatus] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[patientID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MedicineData]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineData](
	[medicineName] [nchar](100) NOT NULL,
	[medicineDescription] [nchar](200) NULL,
	[medicinePrice] [decimal](7, 2) NULL,
	[manufacturer] [nchar](50) NULL,
	[batchNo] [nchar](30) NULL,
	[registerDate] [date] NULL,
	[expiryDate] [date] NULL,
	[quantity] [decimal](7, 2) NULL,
	[amount] [decimal](7, 2) NULL,
	[reminderType] [nchar](30) NULL,
	[reminderDate] [date] NULL,
	[quantityThreshold] [decimal](7, 2) NULL,
	[lastStockUpdate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[medicineName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[userName] [nchar](20) NULL,
	[password] [nchar](10) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorsData]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorsData](
	[doctorName] [nchar](30) NOT NULL,
	[specialization] [nchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[doctorName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WardData]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WardData](
	[wardType] [nchar](30) NOT NULL,
	[stayCharges] [decimal](7, 2) NULL,
	[operationdelivery] [decimal](7, 2) NULL,
	[anaesthesia] [decimal](7, 2) NULL,
	[OTCharge] [decimal](7, 2) NULL,
	[assistantCharge] [decimal](7, 2) NULL,
	[nursing] [decimal](7, 2) NULL,
	[consultantCharge] [decimal](7, 2) NULL,
	[roundCharge] [decimal](7, 2) NULL,
	[miscellaneousCharge] [decimal](7, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[wardType] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[visitData]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[visitData](
	[srNo] [int] NOT NULL,
	[patientID] [nchar](20) NULL,
	[dateOfAdmission] [date] NULL,
	[timeOfAdmission] [nchar](10) NULL,
	[broughtBy] [nchar](30) NULL,
	[relation] [nchar](30) NULL,
	[inchargeDoctor] [nchar](30) NULL,
	[deposit] [decimal](7, 2) NULL,
	[medicalHistory] [nchar](30) NULL,
	[diagnosis] [nchar](30) NULL,
	[pulse] [nchar](30) NULL,
	[bp] [nchar](30) NULL,
	[temperature] [nchar](30) NULL,
	[pWeight] [nchar](30) NULL,
	[examCustom1] [nchar](50) NULL,
	[examCustom2] [nchar](50) NULL,
	[wardType] [nchar](30) NULL,
	[courseInWard] [nchar](250) NULL,
	[treatmentGiven] [nchar](250) NULL,
	[treatmentAdvanced] [nchar](250) NULL,
	[medicineBill] [decimal](18, 2) NULL,
	[serviceBill] [decimal](18, 2) NULL,
	[dateOfDischarge] [date] NULL,
	[timeOfDischarge] [nchar](10) NULL,
	[totalCharges] [decimal](18, 2) NULL,
	[discounts] [decimal](18, 2) NULL,
	[payable] [decimal](18, 2) NULL,
	[visitStatus] [nchar](30) NULL,
	[wardCharges] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[srNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WardOrder]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WardOrder](
	[visitID] [int] NULL,
	[patientID] [nchar](20) NULL,
	[wardType] [nchar](30) NULL,
	[hospitalStay] [int] NULL,
	[operationalDelivery] [int] NULL,
	[aneasthesia] [int] NULL,
	[OTCharge] [int] NULL,
	[assistantCharge] [int] NULL,
	[nursing] [int] NULL,
	[padiatricianCharge] [int] NULL,
	[roundCharge] [int] NULL,
	[miscellaneousCharge] [int] NULL,
	[admissionDate] [date] NULL,
	[dischargeDate] [date] NULL,
	[noOfdays] [int] NULL,
	[stayAmount] [decimal](7, 2) NULL,
	[operationAmount] [decimal](7, 2) NULL,
	[aneasthesiaAmount] [decimal](7, 2) NULL,
	[oTChargeAmount] [decimal](7, 2) NULL,
	[assistantAmount] [decimal](7, 2) NULL,
	[nursingAmount] [decimal](7, 2) NULL,
	[paediatricianAmount] [decimal](7, 2) NULL,
	[roundAmount] [decimal](7, 2) NULL,
	[miscellaneousAmount] [decimal](7, 2) NULL,
	[totalWardAmount] [decimal](7, 2) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceOrder]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceOrder](
	[srNo] [int] NOT NULL,
	[ServiceName] [nchar](30) NULL,
	[noOfDays] [int] NULL,
	[totalAmount] [decimal](7, 2) NULL,
	[patientID] [nchar](20) NULL,
	[visitID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[srNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MedicineOrder]    Script Date: 11/18/2017 10:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicineOrder](
	[srNo] [int] NOT NULL,
	[medicineName] [nchar](50) NULL,
	[quantity] [int] NULL,
	[totalAmount] [decimal](7, 2) NULL,
	[patientID] [nchar](20) NULL,
	[visitID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[srNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK__visitData__patie__060DEAE8]    Script Date: 11/18/2017 10:58:05 ******/
ALTER TABLE [dbo].[visitData]  WITH CHECK ADD FOREIGN KEY([patientID])
REFERENCES [dbo].[Patients] ([patientID])
GO
/****** Object:  ForeignKey [FK__WardOrder__patie__31EC6D26]    Script Date: 11/18/2017 10:58:05 ******/
ALTER TABLE [dbo].[WardOrder]  WITH CHECK ADD FOREIGN KEY([patientID])
REFERENCES [dbo].[Patients] ([patientID])
GO
/****** Object:  ForeignKey [FK__WardOrder__visit__30F848ED]    Script Date: 11/18/2017 10:58:05 ******/
ALTER TABLE [dbo].[WardOrder]  WITH CHECK ADD FOREIGN KEY([visitID])
REFERENCES [dbo].[visitData] ([srNo])
GO
/****** Object:  ForeignKey [FK__ServiceOr__patie__239E4DCF]    Script Date: 11/18/2017 10:58:05 ******/
ALTER TABLE [dbo].[ServiceOrder]  WITH CHECK ADD FOREIGN KEY([patientID])
REFERENCES [dbo].[Patients] ([patientID])
GO
/****** Object:  ForeignKey [FK__ServiceOr__visit__33D4B598]    Script Date: 11/18/2017 10:58:05 ******/
ALTER TABLE [dbo].[ServiceOrder]  WITH CHECK ADD FOREIGN KEY([visitID])
REFERENCES [dbo].[visitData] ([srNo])
GO
/****** Object:  ForeignKey [FK__MedicineO__patie__1ED998B2]    Script Date: 11/18/2017 10:58:05 ******/
ALTER TABLE [dbo].[MedicineOrder]  WITH CHECK ADD FOREIGN KEY([patientID])
REFERENCES [dbo].[Patients] ([patientID])
GO
/****** Object:  ForeignKey [FK__MedicineO__visit__32E0915F]    Script Date: 11/18/2017 10:58:05 ******/
ALTER TABLE [dbo].[MedicineOrder]  WITH CHECK ADD FOREIGN KEY([visitID])
REFERENCES [dbo].[visitData] ([srNo])
GO
