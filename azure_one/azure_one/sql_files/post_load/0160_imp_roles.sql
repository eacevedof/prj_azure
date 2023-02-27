UPDATE [local_staging].[dbo].[imp_roles] SET roles_id=NULL;

UPDATE imp
SET roles_id = mt.id
FROM [local_laciahub].[dbo].[roles]  mt
INNER JOIN [local_staging].[dbo].[imp_roles] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[roles]  mt
INNER JOIN [local_staging].[dbo].[imp_roles] imp
ON mt.id = imp.roles_id
WHERE 1=1
AND imp.nok IS NULL
;

-- [name][guard_name][description]
INSERT INTO [local_laciahub].[dbo].[roles]
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
FROM [local_staging].[dbo].[imp_roles] imp
LEFT JOIN [local_laciahub].[dbo].[roles] mt
ON mt.id = imp.roles_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.roles_id = mt.id,
imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[roles]  mt
INNER JOIN [local_staging].[dbo].[imp_roles] imp
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.roles_id IS NULL;
;