/*
id, owner_id, path, is_default, crated_at, deleted_at
select * from assets_files_thumbnails

*/
INSERT INTO [local_laciahub].[dbo].[assets_files_thumbnails]
(owner_id, path, is_default, created_at)
SELECT
    1 owner_id, path_thumbnail path, 1, GETDATE()
FROM [local_staging].[dbo].[imp_assets_types] imp
WHERE 1=1
AND NOT EXISTS
(
    SELECT path 
    FROM [local_laciahub].[dbo].[assets_files_thumbnails] mt
    WHERE 1=1
    AND imp.path_thumbnail = mt.path
)

-- actualizo los ids de los thumbnails
UPDATE imp
SET imp.assets_files_thumbnails_id = mt.id;
FROM [local_staging].[dbo].[imp_assets_types] imp
INNER JOIN [local_laciahub].[dbo].[assets_files_thumbnails] mt
ON imp.path_thumbnail = mt.path
;

-- 
UPDATE imp
SET assets_types_id = mt.id
FROM [local_laciahub].[dbo].[assets_types]  mt
INNER JOIN [local_staging].[dbo].[imp_assets_types] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
;



UPDATE mt
SET
    mt.name = CONVERT(VARCHAR(255), imp.val),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_types]  mt
INNER JOIN [local_staging].[dbo].[imp_assets_types] imp
ON mt.id = imp.assets_types_id
WHERE 1=1
AND imp.nok IS NULL
;

INSERT INTO [local_laciahub].[dbo].[assets_types]
(
 name, 
 created_at
)
SELECT
    CONVERT(VARCHAR(255),imp.val) name,
    GETDATE()
FROM [local_staging].[dbo].[imp_assets_types] imp
LEFT JOIN [local_laciahub].[dbo].[assets_types] mt
ON mt.id = imp.assets_types_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.assets_types_id = mt.id
FROM [local_laciahub].[dbo].[assets_types]  mt
INNER JOIN [local_staging].[dbo].[imp_assets_types] imp
ON mt.name = imp.val
WHERE 1=1
AND imp.nok IS NULL
AND imp.assets_types_id IS NULL;
;