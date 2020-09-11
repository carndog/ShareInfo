USE [Shares]

GO
IF NOT EXISTS (select * from sys.objects where name = 'EtoroClosedPosition' and type_desc = 'USER_TABLE')
    BEGIN
        CREATE TABLE [dbo].[EtoroClosedPosition](
                                       [Id] [int] IDENTITY(1,1) NOT NULL,
                                       CONSTRAINT [PK_EtoroClosedPosition] PRIMARY KEY CLUSTERED
                                           (
                                            [Id] ASC
                                            ),
                                       [PositionId] bigint NOT NULL,
                                       [Action] nvarchar(20) NOT NULL,
                                       [Amount] money NOT NULL,
                                       [Units] money NOT NULL,
                                       [OpenRate] money NOT NULL,
                                       [CloseRate] money NOT NULL,
                                       [Spread] money NOT NULL,
                                       [Profit] money NOT NULL,
                                       [OpenDate] datetime2 NOT NULL,
                                       [ClosedDate] datetime2 NOT NULL,
                                       [TakeProfitRate] money NOT NULL,
                                       [StopLossRate] money NOT NULL,
                                       [RollOverFees] money NOT NULL
        )
    END
GO
IF NOT EXISTS (select * from sys.objects where name = 'EtoroTransaction' and type_desc = 'USER_TABLE')
    BEGIN
        CREATE TABLE [dbo].[EtoroTransaction](
                                                    [Id] [int] IDENTITY(1,1) NOT NULL,
                                                    CONSTRAINT [PK_EtoroTransaction] PRIMARY KEY CLUSTERED
                                                        (
                                                         [Id] ASC
                                                            ),
                                                    [PositionId] bigint NOT NULL,
                                                    [AccountBalance] money NOT NULL,
                                                    [Type] nvarchar(80) NOT NULL,
                                                    [Details] nvarchar(80) NOT NULL,
                                                    [Amount] money NOT NULL,
                                                    [RealizedEquityChange] money NOT NULL,
                                                    [RealizedEquity] money NOT NULL,
                                                    [Date] datetime2 NOT NULL
        )
    END
GO
IF NOT EXISTS (select * from sys.objects where name = 'HalifaxTransaction' and type_desc = 'USER_TABLE')
    BEGIN
        CREATE TABLE [dbo].[HalifaxTransaction](
                                                 [Id] [int] IDENTITY(1,1) NOT NULL,
                                                 CONSTRAINT [PK_HalifaxTransaction] PRIMARY KEY CLUSTERED
                                                     (
                                                      [Id] ASC
                                                         ),
                                                 [Date] datetime2 NOT NULL,
                                                 [Type] nvarchar(80) NOT NULL,
                                                 [CompanyCode] nvarchar(20) NOT NULL,
                                                 [Exchange] nvarchar(20) NOT NULL,
                                                 [Quantity] int NOT NULL,
                                                 [ExecutedPrice] money NOT NULL,
                                                 [NetConsideration] money NOT NULL,
                                                 [Reference] nvarchar(20) NOT NULL
        )
    END
GO
IF NOT EXISTS (select * from sys.objects where name = 'HalifaxDividend' and type_desc = 'USER_TABLE')
    BEGIN
        CREATE TABLE [dbo].[HalifaxDividend](
                                                   [Id] [int] IDENTITY(1,1) NOT NULL,
                                                   CONSTRAINT [PK_HalifaxDividend] PRIMARY KEY CLUSTERED
                                                       (
                                                        [Id] ASC
                                                           ),
                                                   [IssueDate] datetime2 NOT NULL,
                                                   [ExDividendDate] datetime2 NOT NULL,
                                                   [Stock] nvarchar(80) NOT NULL,
                                                   [SharesHeld] int NOT NULL,
                                                   [Amount] money NOT NULL,
                                                   [HandlingOption] nvarchar(20) NOT NULL
        )
    END
GO


