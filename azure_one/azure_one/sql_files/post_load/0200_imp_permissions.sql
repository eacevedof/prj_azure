UPDATE [local_staging].[dbo].[imp_permissions] SET permissions_id=NULL;
UPDATE [local_staging].[dbo].[imp_permissions] SET roles_id=NULL;

UPDATE imp
SET imp.permissions_id = mt.id
FROM [local_laciahub].[dbo].[permissions]  mt
INNER JOIN [local_staging].[dbo].[imp_permissions] imp
ON mt.slug = imp.slug
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_permissions] SET nok=1 WHERE permissions_id IS NULL;

UPDATE imp
SET imp.roles_id = mt.id
FROM [local_laciahub].[dbo].[roles]  mt
INNER JOIN [local_staging].[dbo].[imp_permissions] imp
ON mt.id = imp.roles_uuid
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_permissions] SET nok=1 WHERE roles_id IS NULL;