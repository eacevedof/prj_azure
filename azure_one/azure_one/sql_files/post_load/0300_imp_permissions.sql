SELECT * FROM [local_staging].[dbo].[imp_permissions]
;

UPDATE [local_staging].[dbo].[imp_permissions] SET permissions_id=NULL;
UPDATE [local_staging].[dbo].[imp_permissions] SET entity_id=NULL;

UPDATE imp
SET imp.permissions_id = mt.id
FROM [local_laciahub].[dbo].[permissions]  mt
INNER JOIN [local_staging].[dbo].[imp_permissions] imp
ON mt.slug = imp.permissions_slug
WHERE 1=1
AND imp.nok IS NULL
;

UPDATE [local_staging].[dbo].[imp_permissions] SET nok=1 WHERE permissions_id IS NULL
;

UPDATE imp
SET imp.entity_id = mt.id
FROM [local_laciahub].[dbo].[roles]  mt
INNER JOIN [local_staging].[dbo].[imp_permissions] imp
ON mt.id = imp.entity_uuid
WHERE 1=1
AND imp.nok IS NULL
AND imp.permissions_type = 'by-role'
;

UPDATE imp
SET imp.entity_id = mt.id
FROM [local_laciahub].[dbo].[assets_type]  mt
INNER JOIN [local_staging].[dbo].[imp_permissions] imp
ON mt.id = imp.entity_uuid
WHERE 1=1
AND imp.nok IS NULL
AND imp.permissions_type = 'by-asset-type'
;

UPDATE [local_staging].[dbo].[imp_permissions] SET nok=1 WHERE entity_id IS NULL
;