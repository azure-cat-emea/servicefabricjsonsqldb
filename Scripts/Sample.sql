USE IoTDb
GO

-- Insert a single row with eventId = 1
EXEC sp_InsertJsonEvents N'{"eventId":1,"deviceId":17,"value":35,"timestamp":"2016-04-26 12:45:37.1237"}'
SELECT * FROM Events
GO

-- Insert an array of two events. Note that the first record has the same eventId as the row inserted by the previous stored procedure call. 
-- In this case, the store procedure simply updates the existing record to guarantee idempotency and avoid duplicates.
EXEC sp_InsertJsonEvents N'[{"eventId":1,"deviceId":17,"value":35,"timestamp":"2016-04-26 12:45:37.1237"},{"eventId":2,"deviceId":17,"value":35,"timestamp":"2016-04-26 12:45:37.1237"}]'
SELECT * FROM Events
GO

GO