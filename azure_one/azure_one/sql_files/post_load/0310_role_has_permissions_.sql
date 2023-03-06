
UPDATE imp
SET imp.permissions_id = mt.id
FROM [local_laciahub].[dbo].[permissions]  mt
INNER JOIN [local_staging].[dbo].[imp_permissions] imp
ON mt.slug = imp.slug
WHERE 1=1
AND imp.nok IS NULL
;


UPDATE imp
SET imp.roles_id = mt.id
FROM [local_laciahub].[dbo].[roles]  mt
INNER JOIN [local_staging].[dbo].[imp_permissions] imp
ON mt.id = imp.roles_uuid
WHERE 1=1
AND imp.nok IS NULL
;

