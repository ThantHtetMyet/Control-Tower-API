-- Insert data for ResultStatus table
INSERT INTO ResultStatuses (ID, Name, IsDeleted)
VALUES 
    (NEWID(), 'Pass', 0),
    (NEWID(), 'Fail', 0);