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
    '41fa119d-49d4-4950-8a22-f99293ec218e', -- Use your existing DepartmentID
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
