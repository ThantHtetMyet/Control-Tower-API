INSERT INTO SubDepartments (
    ID,
    DepartmentID,
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
VALUES 
-- HR SubDepartments
(
    '11111111-1111-1111-1111-111111111111',
    '11111111-1111-1111-1111-111111111111', -- HR Department ID
    'Recruitment',
    'Handles employee recruitment and hiring processes.',
    'Critical for talent acquisition.',
    5,
    0,
    GETDATE(),
    GETDATE(),
    NULL,
    NULL
);