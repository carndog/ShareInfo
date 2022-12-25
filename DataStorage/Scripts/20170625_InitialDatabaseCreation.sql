IF NOT EXISTS (select * from sys.objects where name = 'Prices' and type_desc = 'USER_TABLE')
    BEGIN
        CREATE TABLE [dbo].[Prices](
                                       [Id] [int] IDENTITY(1,1) NOT NULL,
                                       CONSTRAINT [PK_Share] PRIMARY KEY CLUSTERED
                                           (
                                            [Id] ASC
                                               ),
                                       [AssetId] uniqueidentifier NOT NULL,
                                       [Symbol] nvarchar(8) NOT NULL,
                                       [Name] nvarchar(30) NULL,
                                       [Price] money NOT NULL,
                                       [OriginalPrice] money NOT NULL,
                                       [Exchange] nvarchar(20),
                                       [AssetType] nvarchar(20),
                                       [Change] money NULL,
                                       [ChangePercentage] money NULL,
                                       [Open] money NULL,
                                       [High] money NULL,
                                       [Low] money NULL,
                                       [Volume] money NULL,
                                       [TradingDay] nvarchar(20) NULL,
                                       [Date] date NOT NULL
        )
    END

GO
IF NOT EXISTS (select * from sys.objects where name = 'Progress' and type_desc = 'USER_TABLE')
    BEGIN
        CREATE TABLE [dbo].[Progress](
                                         [Id] [int] IDENTITY(1,1) NOT NULL,
                                         CONSTRAINT [PK_Progress] PRIMARY KEY CLUSTERED
                                             (
                                              [Id] ASC
                                                 ),
                                         [processedCount] [int] NOT NULL default 0,
                                         [Date] date NOT NULL default getdate()
        )

        insert into dbo.Progress (processedCount, Date) values (0, GETDATE())
    END
GO

IF NOT EXISTS (select * from sys.all_columns where name = 'timezone')
    BEGIN
        ALTER TABLE dbo.Prices DROP COLUMN [Date]

        ALTER TABLE dbo.Prices
            ADD TimeZone nvarchar(80);

        ALTER TABLE dbo.Prices
            ADD CurrentDateTime datetime2
    END
GO

