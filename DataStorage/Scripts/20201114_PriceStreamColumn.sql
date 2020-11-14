ALTER TABLE dbo.PriceStream
    ADD TimeZone nvarchar (255) NOT NULL;

EXEC sp_rename 'dbo.PriceStream.Date', 'CurrentDateTime', 'COLUMN';