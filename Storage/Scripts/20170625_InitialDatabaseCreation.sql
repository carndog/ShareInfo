USE [Shares]
GO

CREATE TABLE [dbo].[ShareExtract](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	 CONSTRAINT [PK_ShareExtract] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	),
	[Index] nvarchar(10) NULL,
	[Symbol] nvarchar(10) NOT NULL,
	[Name] nvarchar(30) NULL,
	[Price] money NULL,
	[Change] money NULL,
	[ChangePercentage] money NULL
)


GO
