-- Insert data for ReportFormImageType table
INSERT INTO ReportFormImageTypes (ID, ImageTypeName, IsDeleted, CreatedDate, UpdatedDate)
VALUES 
(
    '11111111-1111-1111-1111-111111111111',
    'Before',
    0,
    GETDATE(),
    GETDATE()
),
(
    '22222222-2222-2222-2222-222222222222',
    'After',
    0,
    GETDATE(),
    GETDATE()
);