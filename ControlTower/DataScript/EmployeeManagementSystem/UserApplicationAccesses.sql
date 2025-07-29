INSERT INTO UserApplicationAccesses (
    ID,
    EmployeeID,
    ApplicationID,
    AccessLevelID,
    GrantedDate,
    IsRevoked,
    RevokedDate,
    GrantedBy,
    Remark,
    IsDeleted,
    CreatedDate,
    UpdatedDate,
    CreatedBy,
    UpdatedBy
) VALUES (
    'C1B2A3D4-E5F6-7890-1234-56789ABCDEFA', -- Example new GUID for this access record
    '566FD088-AA3D-4907-8974-BB07AB1231B9', -- Employee/User ID
    'D3E7A18F-2A6F-4D29-BF2B-C5B5A81A1234', -- Application ID
    '11111111-1111-1111-1111-111111111111', -- Example Access Level ID (Replace this!)
    CURRENT_TIMESTAMP,                    -- GrantedDate
    0,                                -- IsRevoked
    NULL,                                 -- RevokedDate
    '566FD088-AA3D-4907-8974-BB07AB1231B9', -- GrantedBy
    'Initial access granted to HR Lead', -- Remark
    0,                                -- IsDeleted
    CURRENT_TIMESTAMP,                    -- CreatedDate
    CURRENT_TIMESTAMP,                    -- UpdatedDate
    '566FD088-AA3D-4907-8974-BB07AB1231B9', -- CreatedBy
    '566FD088-AA3D-4907-8974-BB07AB1231B9'  -- UpdatedBy
);
