IF EXISTS (select * from sys.objects where name = 'EtoroClosedPosition' and type_desc = 'USER_TABLE')
    BEGIN
        DROP TABLE [dbo].[EtoroClosedPosition]
        
        CREATE TABLE [dbo].[EtoroClosedPosition](
                                                    [Id] [int] IDENTITY(1,1) NOT NULL,
                                                    CONSTRAINT [PK_EtoroClosedPosition] PRIMARY KEY CLUSTERED
                                                        (
                                                         [Id] ASC
                                                            ),
                                                    [PositionId] bigint NOT NULL,
                                                    [Action] nvarchar(80) NOT NULL,
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