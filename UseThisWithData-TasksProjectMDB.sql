USE [master]
GO
/****** Object:  Database [TasksProjectM]    Script Date: 11/17/2024 7:46:52 AM ******/
CREATE DATABASE [TasksProjectM]
Go
USE [TasksProjectM]
GO
/****** Object:  Table [dbo].[TaskGroups]    Script Date: 11/17/2024 7:46:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskGroups](
	[TaskGroupId] [int] IDENTITY(1,1) NOT NULL,
	[TaskGroupName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 11/17/2024 7:46:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[TaskGroupId] [int] NULL,
	[TaskName] [nvarchar](100) NOT NULL,
	[TaskDescription] [nvarchar](max) NULL,
	[TaskStatusId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStatus]    Script Date: 11/17/2024 7:46:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStatus](
	[TaskStatusId] [int] IDENTITY(1,1) NOT NULL,
	[TaskStatusName] [nvarchar](100) NOT NULL,
	[StatusColor] [nvarchar](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TaskStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/17/2024 7:46:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TaskGroups] ON 
GO
INSERT [dbo].[TaskGroups] ([TaskGroupId], [TaskGroupName]) VALUES (1, N'gggg')
GO
INSERT [dbo].[TaskGroups] ([TaskGroupId], [TaskGroupName]) VALUES (3, N'fff')
GO
SET IDENTITY_INSERT [dbo].[TaskGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 
GO
INSERT [dbo].[Tasks] ([TaskId], [TaskGroupId], [TaskName], [TaskDescription], [TaskStatusId]) VALUES (1, 1, N'hh', N'hh', NULL)
GO
INSERT [dbo].[Tasks] ([TaskId], [TaskGroupId], [TaskName], [TaskDescription], [TaskStatusId]) VALUES (3, 1, N'gggg', N'ggg', NULL)
GO
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [UserName], [Email], [Password]) VALUES (1, N'DevMohamed', N'DevMohamed@gmail.com', N'123')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Users__A9D10534386FBF68]    Script Date: 11/17/2024 7:46:52 AM ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaskStatus] ADD  DEFAULT ('#FFFFFF') FOR [StatusColor]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([TaskGroupId])
REFERENCES [dbo].[TaskGroups] ([TaskGroupId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD FOREIGN KEY([TaskStatusId])
REFERENCES [dbo].[TaskStatus] ([TaskStatusId])
ON DELETE SET NULL
GO
USE [master]
GO
ALTER DATABASE [TasksProjectM] SET  READ_WRITE 
GO
