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
SET imp.roles_id = mt.id
FROM [local_laciahub].[dbo].[roles]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.id = imp.role_uuid
WHERE 1=1
AND imp.nok IS NULL
;

