-- Insert data for ReportFormTypes table
INSERT INTO ReportFormTypes (ID, Name, IsDeleted, CreatedDate, UpdatedDate, CreatedBy, UpdatedBy)
VALUES 
    (NEWID(), 'Corrective Maintenance', 0, GETDATE(), GETDATE(), NULL, NULL),
    (NEWID(), 'Preventative Maintenance', 0, GETDATE(), GETDATE(), NULL, NULL),
    (NEWID(), 'Incident Report', 0, GETDATE(), GETDATE(), NULL, NULL),
    (NEWID(), 'Other', 0, GETDATE(), GETDATE(), NULL, NULL);
