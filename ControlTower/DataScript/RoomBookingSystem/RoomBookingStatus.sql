-- Script to initialize RoomBookingStatus table

-- Generate GUIDs for each status
DECLARE @PendingId UNIQUEIDENTIFIER = NEWID();
DECLARE @ApprovedId UNIQUEIDENTIFIER = NEWID();
DECLARE @RejectedId UNIQUEIDENTIFIER = NEWID();
DECLARE @CancelledId UNIQUEIDENTIFIER = NEWID();
DECLARE @CompletedId UNIQUEIDENTIFIER = NEWID();

-- Check if data already exists to avoid duplicates
IF NOT EXISTS (SELECT 1 FROM RoomBookingStatus WHERE Name = 'Pending')
BEGIN
    INSERT INTO RoomBookingStatus (ID, Name, IsDeleted)
    VALUES (@PendingId, 'Pending', 0);
    PRINT 'Added Pending status with ID: ' + CAST(@PendingId AS NVARCHAR(36));
END

IF NOT EXISTS (SELECT 1 FROM RoomBookingStatus WHERE Name = 'Approved')
BEGIN
    INSERT INTO RoomBookingStatus (ID, Name, IsDeleted)
    VALUES (@ApprovedId, 'Approved', 0);
    PRINT 'Added Approved status with ID: ' + CAST(@ApprovedId AS NVARCHAR(36));
END

IF NOT EXISTS (SELECT 1 FROM RoomBookingStatus WHERE Name = 'Rejected')
BEGIN
    INSERT INTO RoomBookingStatus (ID, Name, IsDeleted)
    VALUES (@RejectedId, 'Rejected', 0);
    PRINT 'Added Rejected status with ID: ' + CAST(@RejectedId AS NVARCHAR(36));
END

IF NOT EXISTS (SELECT 1 FROM RoomBookingStatus WHERE Name = 'Cancelled')
BEGIN
    INSERT INTO RoomBookingStatus (ID, Name, IsDeleted)
    VALUES (@CancelledId, 'Cancelled', 0);
    PRINT 'Added Cancelled status with ID: ' + CAST(@CancelledId AS NVARCHAR(36));
END

IF NOT EXISTS (SELECT 1 FROM RoomBookingStatus WHERE Name = 'Completed')
BEGIN
    INSERT INTO RoomBookingStatus (ID, Name, IsDeleted)
    VALUES (@CompletedId, 'Completed', 0);
    PRINT 'Added Completed status with ID: ' + CAST(@CompletedId AS NVARCHAR(36));
END