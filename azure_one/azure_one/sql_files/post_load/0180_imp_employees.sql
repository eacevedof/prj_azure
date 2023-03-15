UPDATE mt
SET
    mt.employee_name = CONVERT(VARCHAR(45), imp.employee_name),
    mt.employee_surname_1 = CONVERT(VARCHAR(45), imp.employee_surname_1),
    mt.employee_surname_2 = CONVERT(VARCHAR(45), imp.employee_surname_2),
    -- mt.employee_phone = CONVERT(VARCHAR(255), imp.employee_phone),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[employees]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.employee_email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
;

-- crear empleados
INSERT INTO [local_laciahub].[dbo].[employees]
(
    users_id, companies_id, employee_token, employee_code, employee_photo, employee_name, 
    employee_surname_1, employee_surname_2, 
    -- employee_phone, 
    employee_email, employee_status, email_notification,
    created_at
)
SELECT
    imp.users_id, imp.company_id companies_id, 
    (SELECT UPPER(CONVERT(VARCHAR(25), REPLACE(NEWID(), '-','')))) employee_token,
    CONVERT(VARCHAR(9),(SELECT CONVERT(VARCHAR,imp.id)+CONVERT(VARCHAR(45),CONVERT(INT,RAND()*1000000000)))) employee_code,
    NULL employee_photo,
    CONVERT(VARCHAR(45), imp.employee_name) employee_name, 
    CONVERT(VARCHAR(45), imp.employee_surname_1) employee_surname_1, 
    CONVERT(VARCHAR(45), imp.employee_surname_2) employee_surname_2,
    -- CONVERT(VARCHAR(45), employee_phone) employee_phone,
    CONVERT(VARCHAR(45), imp.employee_email) employee_email,
    1 employee_status,
    1 email_notification,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_employees] imp
LEFT JOIN [local_laciahub].[dbo].[employees] mt
ON mt.employee_email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND mt.id IS NULL
;


UPDATE imp
SET imp.employees_id = mt.id,
imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[employees]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.employee_email = imp.employee_email
WHERE 1=1
AND imp.nok IS NULL
AND imp.remove IS NULL
AND imp.employees_id IS NULL
;