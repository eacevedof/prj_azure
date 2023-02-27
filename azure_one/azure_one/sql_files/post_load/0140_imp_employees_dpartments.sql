UPDATE [local_staging].[dbo].[imp_employees_departments] SET employees_departments_id=NULL;

UPDATE imp
SET employees_departments_id = mt.id
FROM [local_laciahub].[dbo].[employees_departments]  mt
INNER JOIN [local_staging].[dbo].[imp_employees_departments] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.department_name = CONVERT(VARCHAR(45), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[employees_departments]  mt
INNER JOIN [local_staging].[dbo].[imp_employees_departments] imp
ON mt.id = imp.employees_departments_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[employees_departments]
(
 department_name,
 department_token,
 department_status,
 created_at
)
SELECT
    CONVERT(VARCHAR(45),imp.val) department_name,
    (SELECT UPPER(CONVERT(VARCHAR(25), REPLACE(NEWID(), '-',''))) ) department_token,
    1 department_status,
    GETDATE()
FROM [local_staging].[dbo].[imp_employees_departments] imp
LEFT JOIN [local_laciahub].[dbo].[employees_departments] mt
ON mt.id = imp.employees_departments_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.employees_departments_id = mt.id
FROM [local_laciahub].[dbo].[employees_departments]  mt
INNER JOIN [local_staging].[dbo].[imp_employees_departments] imp
ON mt.department_name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.employees_departments_id IS NULL;
;