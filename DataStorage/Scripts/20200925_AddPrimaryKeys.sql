ALTER TABLE [dbo].[EtoroClosedPosition]
DROP CONSTRAINT PK_EtoroClosedPosition
GO

ALTER TABLE [dbo].[EtoroClosedPosition]
    ADD CONSTRAINT PK_EtoroClosedPosition PRIMARY KEY CLUSTERED (PositionId);
GO

ALTER TABLE [dbo].[EtoroTransaction]
    DROP CONSTRAINT PK_EtoroTransaction
GO

ALTER TABLE [dbo].[EtoroTransaction]
    ADD CONSTRAINT PK_EtoroTransaction PRIMARY KEY CLUSTERED (PositionId,Type,Amount,RealizedEquity);