INSERT INTO Occupations (
    ID,
    OccupationName,
    Description,
    Remark,
    Rating,
    OccupationLevelID,
    IsDeleted,
    CreatedDate,
    UpdatedDate,
    CreatedBy,
    UpdatedBy
)
VALUES (
    '11111111-1111-1111-1111-111111111111',                         -- ID (auto-generated GUID)
    'HR Manager',                   -- OccupationName
    'Handles HR operations and staff management', -- Description
    'Must be familiar with labor law', -- Remark
    7,                              -- Rating (e.g., 1–5 scale)
    '11111111-1111-1111-1111-111111111111',                         -- OccupationLevelID (FK to OccupationLevel)
    0,                              -- IsDeleted (false)
    GETDATE(),                      -- CreatedDate
    GETDATE(),                      -- UpdatedDate
    NULL,                           -- CreatedBy (optional, FK to Employee)
    NULL                            -- UpdatedBy (optional, FK to Employee)
);