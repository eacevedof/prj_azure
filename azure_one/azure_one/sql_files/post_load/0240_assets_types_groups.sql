/*
select id uuid, companies_id company_uuid, asset_type_name name, '' description, '' path_thumbnail, '' color  
from assets_types_groups
*/
UPDATE mt
SET
    mt.companies_id = imp.company_id,
    mt.assets_files_thumbnails_id = imp.assets_files_thumbnails_id,
    mt.asset_type_name = CONVERT(VARCHAR(255), imp.val),
    mt.asset_type_description = CONVERT(VARCHAR(255), imp.description),
    mt.color = CONVERT(VARCHAR(10), imp.color),
    mt.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_types_groups]  mt
INNER JOIN [local_staging].[dbo].[imp_imp_keys_and_values] imp
ON mt.id = imp.assets_types_groups_id
WHERE 1=1
AND imp.nok IS NULL
AND imp.entity_type = 'assets'
;

UPDATE [local_staging].[dbo].[imp_assets_types_groups] SET nok=1 WHERE company_id IS NULL
;

SET IDENTITY_INSERT [local_laciahub].[dbo].[assets_types_groups] ON;

INSERT INTO [local_laciahub].[dbo].[assets_types_groups]
(
    id, companies_id, assets_files_thumbnails_id, asset_type_name, asset_type_description,
    created_at
)
SELECT
    imp.uuid, imp.company_id, imp.assets_files_thumbnails_id, 
    CONVERT(VARCHAR(255), imp.val) asset_type_name,
    CONVERT(VARCHAR(255), imp.description) description,
    GETDATE() created_at
FROM [local_staging].[dbo].[imp_imp_keys_and_values] imp
LEFT JOIN [local_laciahub].[dbo].[assets_types_groups] mt
ON mt.id = imp.assets_types_groups_id
WHERE 1=1
AND imp.nok IS NULL
AND mt.id IS NULL
;

SET IDENTITY_INSERT [local_laciahub].[dbo].[assets_types_groups] OFF;

-- actualizo los ids de los nuevos insertados
UPDATE imp
SET imp.assets_types_groups_id = mt.id,
imp.updated_at = GETDATE()
FROM [local_laciahub].[dbo].[assets_types_groups]  mt
INNER JOIN [local_staging].[dbo].[imp_imp_keys_and_values] imp
ON mt.id = imp.uuid
WHERE 1=1
AND imp.nok IS NULL
AND imp.assets_types_groups_id IS NULL;
;