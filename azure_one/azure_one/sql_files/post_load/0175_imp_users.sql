UPDATE [local_staging].[dbo].[imp_employees] SET employees_id=NULL;

UPDATE imp
SET imp.employees_id = mt.id
FROM [local_laciahub].[dbo].[users]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.user_types_id = mt.id
FROM [local_laciahub].[dbo].[user_types]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.id = imp.user_types_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.employees_departments_id = mt.id
FROM [local_laciahub].[dbo].[employees_departments]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.id = imp.department_uuid
WHERE 1=1
AND imp.nok IS NULL
;


UPDATE imp
SET imp.employees_positions_id = mt.id
FROM [local_laciahub].[dbo].[employees_positions]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.id = imp.position_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE imp
SET imp.employees_positions_id = mt.id
FROM [local_laciahub].[dbo].[employees_positions]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.id = imp.position_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[users]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.id = imp.employees_id
WHERE 1=1
AND imp.nok IS NULL
;

-- [name][guard_name][description]
INSERT INTO [local_laciahub].[dbo].[users]
(
 name,
 guard_name,
 -- description, varchar(max)
 created_at
)
SELECT
    CONVERT(VARCHAR(255),imp.val) name,
    'Api' guard_name,
    GETDATE()
FROM [local_staging].[dbo].[imp_employees] imp
LEFT JOIN [local_laciahub].[dbo].[users] mt
ON mt.id = imp.employees_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.employees_id = mt.id,
imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[users]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.employees_id IS NULL;
;