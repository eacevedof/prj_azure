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
