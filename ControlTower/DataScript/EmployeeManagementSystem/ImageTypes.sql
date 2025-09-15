-- Insert default ImageType for User Profile Images
INSERT INTO ImageTypes (ID, ImageTypeName, IsDeleted, CreatedDate, UpdatedDate)
VALUES (
    '11111111-1111-1111-1111-111111111111',
    'User Profile Image',
    0,
    GETDATE(),
    GETDATE()
),
(
    '22222222-2222-2222-2222-222222222222',
    'Service Report Form Image',
    0,
    GETDATE(),
    GETDATE()
);
