USE [Software_License_Service]
GO
/****** Object:  Table [dbo].[Application]    Script Date: 9/3/2023 11:04:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Application](
	[ApplicationID] [uniqueidentifier] NOT NULL,
	[ApplicationName] [nvarchar](100) NOT NULL,
	[ApplicationVersion] [varchar](20) NULL,
 CONSTRAINT [PK_Application] PRIMARY KEY CLUSTERED 
(
	[ApplicationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[License]    Script Date: 9/3/2023 11:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[License](
	[LicenseID] [uniqueidentifier] NOT NULL,
	[ApplicationID] [uniqueidentifier] NOT NULL,
	[LicenseVendorID] [uniqueidentifier] NOT NULL,
	[SpaceID] [int] NOT NULL,
	[TestStationPool] [varchar](200) NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[ExpiredOn] [datetime] NULL,
 CONSTRAINT [PK_License] PRIMARY KEY CLUSTERED 
(
	[LicenseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseOrderBook]    Script Date: 9/3/2023 11:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseOrderBook](
	[LicenseOrderBookID] [uniqueidentifier] NOT NULL,
	[TestStationName] [varchar](50) NOT NULL,
	[TestCaseID] [int] NOT NULL,
	[Orchestrator] [varchar](50) NOT NULL,
	[ReservationTime] [datetime] NOT NULL,
	[EstCompletionTime] [datetime] NOT NULL,
	[CompletionTime] [datetime] NULL,
	[ReservedBy] [varchar](50) NOT NULL,
	[Framework] [varchar](50) NOT NULL,
	[LicenseID] [uniqueidentifier] NOT NULL,
	[TestStationPool] [varchar](50) NOT NULL,
	[SpaceID] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenseVendor]    Script Date: 9/3/2023 11:04:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenseVendor](
	[LicenseVendorID] [uniqueidentifier] NOT NULL,
	[VendorName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_LicenseVendor] PRIMARY KEY CLUSTERED 
(
	[LicenseVendorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[License]  WITH CHECK ADD  CONSTRAINT [FK_License_Application] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Application] ([ApplicationID])
GO
ALTER TABLE [dbo].[License] CHECK CONSTRAINT [FK_License_Application]
GO
ALTER TABLE [dbo].[License]  WITH CHECK ADD  CONSTRAINT [FK_License_LicenseVendor] FOREIGN KEY([LicenseVendorID])
REFERENCES [dbo].[LicenseVendor] ([LicenseVendorID])
GO
ALTER TABLE [dbo].[License] CHECK CONSTRAINT [FK_License_LicenseVendor]
GO
ALTER TABLE [dbo].[LicenseOrderBook]  WITH CHECK ADD  CONSTRAINT [FK_LicenseOrderBook_License] FOREIGN KEY([LicenseID])
REFERENCES [dbo].[License] ([LicenseID])
GO
ALTER TABLE [dbo].[LicenseOrderBook] CHECK CONSTRAINT [FK_LicenseOrderBook_License]
GO
