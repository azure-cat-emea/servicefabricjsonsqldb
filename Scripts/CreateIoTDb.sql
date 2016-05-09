USE IoTDB
GO

-- Drop sp_InsertJsonEvents stored procedure
DROP PROCEDURE IF EXISTS [dbo].[sp_InsertJsonEvents]
GO

-- Drop Events table
DROP TABLE IF EXISTS [dbo].[Events]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Create Events table
CREATE TABLE [dbo].[Events](
	[EventId] [int] NOT NULL,
	[DeviceId] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[Timestamp] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Create sp_InsertEvents stored procedure
CREATE PROCEDURE dbo.sp_InsertJsonEvents  
    @Events NVARCHAR(MAX)
AS  
BEGIN 
    MERGE INTO dbo.Events AS A 
    USING (SELECT *
		   FROM OPENJSON(@Events) 
		   WITH ([eventId] int, [deviceId] int, [value] int, [timestamp] datetime2(7))) B
       ON (A.EventId = B.EventId) 
    WHEN MATCHED THEN 
        UPDATE SET A.DeviceId = B.DeviceId, 
                   A.Value = B.Value, 
                   A.Timestamp = B.Timestamp 
    WHEN NOT MATCHED THEN 
        INSERT (EventId, DeviceId, Value, Timestamp)  
        VALUES(B.EventId, B.DeviceId, B.Value, B.Timestamp); 
END 
GO
