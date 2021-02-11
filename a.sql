USE [TaskManager]
GO

/****** Object:  Table [dbo].[Task]    Script Date: 2/10/2021 12:54:50 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Task]') AND type in (N'U'))
DROP TABLE [dbo].[Task]
GO

