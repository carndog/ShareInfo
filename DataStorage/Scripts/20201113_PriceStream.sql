CREATE TABLE [dbo].[PriceStream](
       [Id] [int] IDENTITY(1,1) NOT NULL,
       CONSTRAINT [PK_PriceStream] PRIMARY KEY CLUSTERED
           (
            [Id] ASC
            ),
       [PriceStreamId] uniqueidentifier NOT NULL,
       [Exchange] nvarchar (80) NOT NULL,
       [Symbol] nvarchar(16) NOT NULL,
       [Price] money NOT NULL,
       [OriginalPrice] money NOT NULL,
       [Date] datetime2 NOT NULL
)
GO

Create NonClustered Index IX_PriceStream_Symbol On PriceStream
    (Symbol Asc)

Create NonClustered Index IX_PriceStream_SymbolExchange On PriceStream
    (Symbol Asc, Exchange Asc)

Create NonClustered Index IX_PriceStream_SymbolDate On PriceStream
    (Symbol Asc, Date Asc)