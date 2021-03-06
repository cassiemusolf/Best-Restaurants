USE [bestrestaurants]
GO
/****** Object:  Table [dbo].[cuisine]    Script Date: 2/23/2017 4:43:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuisine](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[restaurants]    Script Date: 2/23/2017 4:43:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[restaurants](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[location] [varchar](255) NULL,
	[price] [varchar](255) NULL,
	[cuisine_id] [int] NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[cuisine] ON 

INSERT [dbo].[cuisine] ([id], [name]) VALUES (17, N'Morrocan')
INSERT [dbo].[cuisine] ([id], [name]) VALUES (10, N'Thai Food')
INSERT [dbo].[cuisine] ([id], [name]) VALUES (11, N'Italian')
INSERT [dbo].[cuisine] ([id], [name]) VALUES (12, N'French')
INSERT [dbo].[cuisine] ([id], [name]) VALUES (13, N'Chinese')
INSERT [dbo].[cuisine] ([id], [name]) VALUES (14, N'Greek')
INSERT [dbo].[cuisine] ([id], [name]) VALUES (15, N'Seafood')
INSERT [dbo].[cuisine] ([id], [name]) VALUES (16, N'American')
SET IDENTITY_INSERT [dbo].[cuisine] OFF
SET IDENTITY_INSERT [dbo].[restaurants] ON 

INSERT [dbo].[restaurants] ([id], [name], [location], [price], [cuisine_id]) VALUES (13, N'Billy''s', N'Seattle', N'Low', 17)
INSERT [dbo].[restaurants] ([id], [name], [location], [price], [cuisine_id]) VALUES (8, N'Salty''s', N'Alki Beach, WA', N'Expensive', 15)
INSERT [dbo].[restaurants] ([id], [name], [location], [price], [cuisine_id]) VALUES (9, N'McDonalds', N'Everywhere', N'Low', 16)
INSERT [dbo].[restaurants] ([id], [name], [location], [price], [cuisine_id]) VALUES (10, N'Bang Bar', N'West Seattle', N'Medium', 10)
INSERT [dbo].[restaurants] ([id], [name], [location], [price], [cuisine_id]) VALUES (11, N'Olive Garden', N'Tukwila', N'Medium', 11)
INSERT [dbo].[restaurants] ([id], [name], [location], [price], [cuisine_id]) VALUES (12, N'Wild Ginger', N'Seattle', N'Medium', 10)
SET IDENTITY_INSERT [dbo].[restaurants] OFF
