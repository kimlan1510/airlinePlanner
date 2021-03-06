USE [airline]
GO
/****** Object:  Table [dbo].[airline_services]    Script Date: 6/12/2017 4:59:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[airline_services](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[flights]    Script Date: 6/12/2017 4:59:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[flying_from] [varchar](255) NULL,
	[flying_to] [varchar](255) NULL,
	[depart] [varchar](255) NULL,
	[arrival] [varchar](255) NULL,
	[status] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[summary]    Script Date: 6/12/2017 4:59:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[summary](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[airline_services_id] [int] NULL,
	[flights_id] [int] NULL
) ON [PRIMARY]

GO
