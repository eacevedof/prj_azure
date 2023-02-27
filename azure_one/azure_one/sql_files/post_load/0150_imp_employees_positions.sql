UPDATE [local_staging].[dbo].[imp_employees_positions] SET employees_positions_id=NULL;

UPDATE imp
SET employees_positions_id = mt.id
FROM [local_laciahub].[dbo].[employees_positions]  mt
INNER JOIN [local_staging].[dbo].[imp_employees_positions] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.position_name = CONVERT(VARCHAR(255), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[employees_positions]  mt
INNER JOIN [local_staging].[dbo].[imp_employees_positions] imp
ON mt.id = imp.employees_positions_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[employees_positions]
(
 position_name,
 position_token,
 position_status,
 created_at
)
SELECT
    CONVERT(VARCHAR(255),imp.val) position_name,
    (SELECT UPPER(CONVERT(VARCHAR(25), REPLACE(NEWID(), '-',''))) ) position_token,
    1 position_status,
    GETDATE()
FROM [local_staging].[dbo].[imp_employees_positions] imp
LEFT JOIN [local_laciahub].[dbo].[employees_positions] mt
ON mt.id = imp.employees_positions_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.employees_positions_id = mt.id
FROM [local_laciahub].[dbo].[employees_positions]  mt
INNER JOIN [local_staging].[dbo].[imp_employees_positions] imp
ON mt.position_name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.employees_positions_id IS NULL;
;