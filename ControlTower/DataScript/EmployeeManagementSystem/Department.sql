INSERT INTO Departments (
    ID,
    Name,
    Description,
    Remark,
    Rating,
    IsDeleted,
    CreatedDate,
    UpdatedDate,
    CreatedBy,
    UpdatedBy
)
VALUES (
    '11111111-1111-1111-1111-111111111111', -- Use your existing DepartmentID
    'Human Resources',
    'Handles employee recruitment, onboarding, records, and policy enforcement.',
    'Key department for workforce operations.',
    5,
    0,
    GETDATE(),
    GETDATE(),
    NULL, -- CreatedBy (can be filled later with EmployeeID)
    NULL  -- UpdatedBy (can be filled later)
);

INSERT INTO Departments (
    ID,
    Name,
    Description,
    Remark,
    Rating,
    IsDeleted,
    CreatedDate,
    UpdatedDate,
    CreatedBy,
    UpdatedBy
)
VALUES (
    '22222222-2222-2222-2222-222222222222', -- Use your existing DepartmentID
    'Software',
    'Handles software development, testing, and deployment.',
    'Key department for software operations.',
    5,
    0,
    GETDATE(),
    GETDATE(),
    NULL, -- CreatedBy (can be filled later with EmployeeID)
    NULL  -- UpdatedBy (can be filled later)
);
