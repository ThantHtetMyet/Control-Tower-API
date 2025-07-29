-- Insert common access levels into AccessLevel table
INSERT INTO AccessLevels (
    ID,
    LevelName,
    Description,
    IsDeleted,
    CreatedDate,
    UpdatedDate,
    CreatedBy,
    UpdatedBy
)
VALUES 
(
    '11111111-1111-1111-1111-111111111111',
    'Admin',
    'Full administrative access with all permissions',
    0,
    GETDATE(),
    GETDATE(),
    NULL,
    NULL
),
(
    '22222222-2222-2222-2222-222222222222',
    'User',
    'Standard user access with basic permissions',
    0,
    GETDATE(),
    GETDATE(),
    NULL,
    NULL
);