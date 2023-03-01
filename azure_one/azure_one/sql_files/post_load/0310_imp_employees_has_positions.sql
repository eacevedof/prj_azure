DELETE 
FROM [local_laciahub].[dbo].[employees_has_positions]
WHERE 1=1
AND NOT EXISTS (
    SELECT 
      imp.employees_id, 
      imp.employees_positions_id
    FROM [local_staging].[dbo].[imp_employees] imp
    WHERE 1=1
    AND imp.nok IS NULL
    AND [local_laciahub].[dbo].[employees_has_positions].positions_id = imp.employees_positions_id
    AND [local_laciahub].[dbo].[employees_has_positions].employees_id = imp.employees_id
)
;


INSERT INTO [local_laciahub].[dbo].[employees_has_positions]
(positions_id, employees_id, created_at)
SELECT 
    DISTINCT 
    imp.employees_positions_id, 
    imp.employees_id, 
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_employees] imp
WHERE 1=1
AND imp.nok IS NULL
AND NOT EXISTS (
  SELECT positions_id, employees_id
  FROM [local_laciahub].[dbo].[employees_has_positions] mt
  WHERE 1=1
  AND mt.positions_id = imp.employees_positions_id
  AND mt.employees_id = imp.employees_id
)
;