﻿USE [Shares]

GO
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
                                       [TradingDay] date NULL,
                                       [Date] date NOT NULL
        )
    END

GO
