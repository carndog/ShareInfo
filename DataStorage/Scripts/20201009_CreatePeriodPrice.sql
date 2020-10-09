CREATE TABLE [dbo].[PeriodPrice](
       [Id] [int] IDENTITY(1,1) NOT NULL,
       CONSTRAINT [PK_PeriodPrice] PRIMARY KEY CLUSTERED
           (
            [Id] ASC
               ),
       [PeriodPriceId] uniqueidentifier NOT NULL,
       [PeriodType] nvarchar (16) NOT NULL,
       [Symbol] nvarchar(8) NOT NULL,
       [Open] money NOT NULL,
       [High] money NOT NULL,
       [Low] money NOT NULL,
       [Close] money NOT NULL,
       [Volume] money NOT NULL,
       [Date] date NOT NULL
)