DELETE 
FROM [local_laciahub].[dbo].[departments_has_employees]
WHERE 1=1
AND NOT EXISTS (
    SELECT imp.employees_id, imp.employees_departments_id
    FROM [local_staging].[dbo].[imp_employees] imp
    WHERE 1=1
    AND imp.nok IS NULL
    AND [local_laciahub].[dbo].[departments_has_employees].departments_id = imp.employees_departments_id
    AND [local_laciahub].[dbo].[departments_has_employees].employees_id = imp.employees_id
)
;

/*
UPDATE mt
  SET 
    mt.departments_id = imp.employees_departments_id,
    mt.updated_at = GETDATE()

-- SELECT mt.departments_id, mt.employees_id, imp.employees_id, imp.employees_departments_id, mt.updated_at
FROM [local_laciahub].[dbo].[departments_has_employees]  mt
INNER JOIN [local_staging].[dbo].[imp_employees] imp
ON mt.employees_id = imp.employees_id
AND mt.departments_id != imp.employees_departments_id
WHERE 1=1
AND imp.nok IS NULL
-- ORDER BY mt.employees_id, mt.departments_id
;
*/


INSERT INTO [local_laciahub].[dbo].[departments_has_employees]
(departments_id, employees_id, created_at)
SELECT 
    DISTINCT 
    imp.employees_departments_id, 
    imp.employees_id, 
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_employees] imp
WHERE 1=1
AND imp.nok IS NULL
AND NOT EXISTS (
  SELECT departments_id, employees_id
  FROM [local_laciahub].[dbo].[departments_has_employees] mt
  WHERE 1=1
  AND mt.departments_id = imp.employees_departments_id
  AND mt.employees_id = imp.employees_id
)
;