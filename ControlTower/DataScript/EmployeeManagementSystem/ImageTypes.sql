-- Insert default ImageType for User Profile Images
INSERT INTO ImageTypes (ID, ImageTypeName, IsDeleted, CreatedDate, UpdatedDate)
VALUES (
    NEWID(),
    'User Profile Image',
    0,
    GETDATE(),
    GETDATE()
);
