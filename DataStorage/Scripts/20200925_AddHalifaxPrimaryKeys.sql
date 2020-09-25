ALTER TABLE [dbo].[HalifaxDividend]
    DROP CONSTRAINT PK_HalifaxDividend
GO

ALTER TABLE [dbo].[HalifaxDividend]
    ADD CONSTRAINT PK_HalifaxDividend PRIMARY KEY CLUSTERED (ExDividendDate,Stock);
GO

ALTER TABLE [dbo].[HalifaxTransaction]
    DROP CONSTRAINT PK_HalifaxTransaction
GO

ALTER TABLE [dbo].[HalifaxTransaction]
    ADD CONSTRAINT PK_HalifaxTransaction PRIMARY KEY CLUSTERED (Reference);