INSERT INTO Users (
    ID,
    CompanyID,
    SubDepartmentID,
    OccupationID,
    StaffCardID,
    StaffRFIDCardID,
    FirstName,
    LastName,
    Email,
    MobileNo,
    Gender,
    LoginPassword,
    Remark,
    Rating,
    IsDeleted,
    CreatedDate,
    UpdatedDate,
    CreatedBy,
    UpdatedBy,
    LastLogin,
    StartWorkingDate,
    LastWorkingDate,
    WorkPermit,
    Nationality,
    Religion,
    DateOfBirth,
    WorkPassCardNumber,
    WorkPassCardIssuedDate,
    WorkPassCardExpiredDate,
    EmergencyContactName,
    EmergencyContactNumber,
    EmergencyRelationship
)
VALUES (
    '11111111-1111-1111-1111-111111111111',
    '11111111-1111-1111-1111-111111111111', -- CompanyID
    '11111111-1111-1111-1111-111111111111', -- SubDepartmentID (changed from DepartmentID)
    '11111111-1111-1111-1111-111111111111', -- OccupationID
    '1001',                                 -- StaffCardID (now string)
    '2001',                                 -- StaffRFIDCardID (now string)
    'System',
    'Account',
    'system@gmail.com',
    '91234567',
    'Female',
    '12345',
    'HR Lead with 5 years experience',
    4,
    0,
    GETDATE(),
    GETDATE(),
    NULL,
    NULL,
    '1900-01-01',   -- LastLogin
    '2020-03-01',
    NULL,
    'WP20201234',
    'Singaporean',
    'Buddhist',
    '1990-06-15',
    'WP123456789',                          -- WorkPassCardNumber
    '2020-01-01',                           -- WorkPassCardIssuedDate
    '2025-01-01',                           -- WorkPassCardExpiredDate
    'Jane Doe',                             -- EmergencyContactName
    98765432,                               -- EmergencyContactNumber
    'Sister'                                -- EmergencyRelationship
);