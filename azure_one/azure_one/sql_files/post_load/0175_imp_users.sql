UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.employee_surname_1),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[users]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
;

-- crear usuarios
INSERT INTO [local_laciahub].[dbo].[users]
(
    user_types_id, tenant_id, user_code, name, email, password, password_expire_in, remember_token,
    user_active, user_blocked, max_attempts, language, multisession_allowed, 
    created_at
)
SELECT
    user_types_id, company_id, 
    
    CONVERT(VARCHAR(9),(SELECT CONVERT(VARCHAR,id)+CONVERT(VARCHAR(45),CONVERT(INT,RAND()*1000000000)))) user_code,
    CONVERT(VARCHAR(45), employee_surname_1) name, 
    CONVERT(VARCHAR(45), employee_email) email,
    '$2y$10$0H1h9ZbDLulNhSLMX2SpkOSQwv44U7vADYWBRM6QTgDfOXl/pFMiG' password, NULL password_expire_in, 
    NULL remember_token, 1 user_active, 1 user_blocked, 3 max_attempts, language_uuid language, 0 multisession_allowed,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_employees] imp
LEFT JOIN [local_laciahub].[dbo].[users] mt
ON mt.email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;


UPDATE imp
SET imp.users_id = mt.id,
imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[users]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
AND imp.employees_id IS NULL;
;