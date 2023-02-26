UPDATE [local_staging].[dbo].[imp_user_types] SET user_types_id=NULL;

UPDATE imp
SET user_types_id = mt.id
FROM [local_laciahub].[dbo].[user_types]  mt
INNER JOIN [local_staging].[dbo].[imp_user_types] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[user_types]  mt
INNER JOIN [local_staging].[dbo].[imp_user_types] imp
ON mt.id = imp.user_types_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[user_types]
(
 name, 
 created_at
)
SELECT
    CONVERT(VARCHAR(255),imp.val) name,
    GETDATE()
FROM [local_staging].[dbo].[imp_user_types] imp
LEFT JOIN [local_laciahub].[dbo].[user_types] mt
ON mt.id = imp.user_types_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.user_types_id = mt.id
FROM [local_laciahub].[dbo].[user_types]  mt
INNER JOIN [local_staging].[dbo].[imp_user_types] imp
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.user_types_id IS NULL;
;