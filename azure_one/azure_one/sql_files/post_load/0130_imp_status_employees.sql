UPDATE [local_staging].[dbo].[imp_status_employees] SET status_employees_id=NULL;

UPDATE imp
SET status_employees_id = mt.employee_status
FROM [local_laciahub].[dbo].[status_employees]  mt
INNER JOIN [local_staging].[dbo].[imp_status_employees] imp
ON mt.employee_status = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.status_name = CONVERT(VARCHAR(255), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[status_employees]  mt
INNER JOIN [local_staging].[dbo].[imp_status_employees] imp
ON mt.employee_status = imp.status_employees_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[status_employees]
(
 status_name, 
 created_at
)
SELECT
    CONVERT(VARCHAR(255),imp.val) status_name,
    GETDATE()
FROM [local_staging].[dbo].[imp_status_employees] imp
LEFT JOIN [local_laciahub].[dbo].[status_employees] mt
ON mt.employee_status = imp.status_employees_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.employee_status IS NULL
;

UPDATE imp
SET imp.status_employees_id = mt.employee_status
FROM [local_laciahub].[dbo].[status_employees]  mt
INNER JOIN [local_staging].[dbo].[imp_status_employees] imp
ON mt.status_name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.status_employees_id IS NULL;
;