INSERT INTO OccupationLevels (
    ID,
    LevelName,
    Description,
    Rank,
    IsDeleted,
    CreatedDate,
    UpdatedDate,
    CreatedBy,
    UpdatedBy
)
VALUES 
(
    '11111111-1111-1111-1111-111111111111',
    'Internship',
    'Positions for interns with limited experience.',
    7,
    0,
    GETDATE(),
    GETDATE(),
    NULL,
    NULL
);
